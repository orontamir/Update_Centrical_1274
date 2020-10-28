using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace _1274_RECUITE.Controllers
{
    public class HomeController : Controller
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

            #region Victor tasks
            /*
             1. add validation on ssn and station_number
             2. create a model with: ssn, station_number, phone
             3. create error and success page
             4. change func on click submit to return some func controller (dont use js)
            */
            #endregion


            return View("Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SubmitForm()
        {
            if (ModelState.IsValid)
            {
                string phoneRegex = @"^\+?[0 - 9]{ 3} -?[0 - 9]{ 6,12}$";


                ViewBag.error = "נשלח בהצלחה";
                return View("Home");


            }
            else
            {
                ViewBag.error = "error!!!!!";
                return View("Error");
            }
        }
    }
}