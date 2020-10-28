using _1274_RECUITE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _1274_RECUITE.Controllers
{
    public class IndexController : Controller
    {
        public ActionResult Index()
        {

            //string x_api_key = ConfigurationManager.AppSettings["x_api_key"];
            //if (Request.Headers["x-api-key"] != x_api_key)
            //{
            //    ViewBag.error = "Unauthorized";
            //    return View("Error");
            //}

            string user_ssn = Request.QueryString["ssn"];
            string station_number = Request.QueryString["station_number"];
            string ssn_regex = @"^[\d]{8,9}$";


            if (string.IsNullOrEmpty(user_ssn) || string.IsNullOrEmpty(station_number))
            {
                ViewBag.error = "תז או מספר תחנה לא תקינים";
                return View("Error");
            }


            else
            {
                int s_num = Convert.ToInt32(station_number);

                if (!(s_num > 0 && s_num < 500))
                {
                    ViewBag.error = "Invalid station";
                    return View("Error");
                }
                if (!Regex.IsMatch(user_ssn, ssn_regex))
                {
                    ViewBag.error = "Invalid ssn";
                    return View("Error");

                }

            }

            ///if validation success 
            ///
            Session["ssn"] = user_ssn;
            Session["station_number"] = station_number;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Index");
        }

        //
         public async Task<ActionResult> SubmitForm(string PhoneNumber)

       // public ActionResult SubmitForm(string PhoneNumber)
        {
            string phoneRegex = @"^\+?[0-9]{3}-?[0-9]{6,7}$";
            if (!string.IsNullOrEmpty(PhoneNumber) &&  (Regex.IsMatch(PhoneNumber, phoneRegex)) )
            { 
          
                string url = "http://128.1.20.117:7018/1274_Recruite_API/api/Recruite";
            using (var client = new HttpClient())
            {
                //set up client
                var values = new Dictionary<string, string>
                       {
                            { "SSN", Session["ssn"].ToString() },
                            { "StationNumber",   Session["station_number"].ToString() },
                            { "PhoneNumber", Request["PhoneNumber"] }

                        };

             
                    var content = new FormUrlEncodedContent(values);
                    client.DefaultRequestHeaders.Add("api-key", "application/json");
                    client.DefaultRequestHeaders.Add("api-key", @"Po!2k983Loa37b@z6");
                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    ViewBag.SubmitMessage = responseString;
                    return View("Index");

                }

                
            }
            else
            {
                ViewBag.SubmitMessage = "אחד מהפרטים שהוזנו שגויים";
                return View("Index");
            }

        }
    }
}