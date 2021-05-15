using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace _20MCA121.Filters
{
    public class Auth : AuthorizationFilterAttribute // extend AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext) // method Call 
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Forbidden);
                httpResponse.Content = new StringContent("Authorization Data is missing!!!");
                // If UnAuthorized Persone try to Login in 
                httpResponse.ReasonPhrase = "No Authorization!!";
                actionContext.Response = httpResponse;
            }
            else
            {
                String encodedData = actionContext.Request.Headers.Authorization.Parameter; // Encode Data 
                String decodeData = Encoding.UTF8.GetString(Convert.FromBase64String(encodedData)); // Decode Data 
                String[] user = decodeData.Split(':'); // Splite In Arry 

                int uid = Convert.ToInt32(user[0]);
                String upss = user[1];

                dbAppointmentEntities db = new dbAppointmentEntities();// db object 
                User_Master u1 = db.User_Master.Where(u => u.User_id == uid && u.User_Password.Equals(upss)).FirstOrDefault();
                // Check User Id and pass in database
                if (u1 != null)
                {

                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(u1.User_name), null);
                }
                else
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    httpResponse.Content = new StringContent("Authorization Data is invalid !!!");
                    httpResponse.ReasonPhrase = "No Authorization!!";
                    actionContext.Response = httpResponse;
                }

            }
        }
    }
}
