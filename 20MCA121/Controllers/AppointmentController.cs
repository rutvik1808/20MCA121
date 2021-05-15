using _20MCA121.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace _20MCA121.Controllers
{
    public class AppointmentController : ApiController
    {
        dbAppointmentEntities db = new dbAppointmentEntities(); //Object For DATABASE Entities

         [Auth] // Authentication For User If User Are in Data base then New appointment book

        public HttpResponseMessage PostAppointment(Appointment_Master appointment_Master)
        {
            db.Appointment_Master.Add(appointment_Master);// Add data in Appointment table
            db.SaveChanges();
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Created);
            //httpResponse create for If Data Successfully insert That httpResponse show Message
            httpResponse.Content = new StringContent("Insert Successfully!!");
            return httpResponse;

        }
        public HttpResponseMessage GetAp_id(int ap_id)
        {
            Appointment_Master appointment = db.Appointment_Master.Find(ap_id); //Find Appointment Form Tada base 
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK); //Call Response 
            httpResponse.Content = new ObjectContent(typeof(Appointment_Master), appointment, new JsonMediaTypeFormatter()); //Data In json from 
            return httpResponse;
        }

        public HttpResponseMessage GetAllApp()
        {
            List<Appointment_Master> account_Holders = db.Appointment_Master.ToList(); // Get All Data from Appointment
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponse.Content = new ObjectContent(typeof(List<Appointment_Master>), account_Holders, new JsonMediaTypeFormatter()); // in json formatter  
            return httpResponse;

        }


    }
}
