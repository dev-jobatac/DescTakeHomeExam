using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TimeLogController : Controller
    {
        SysDbEntities entity = new SysDbEntities();
        // GET: TimeLog
        public ActionResult Index(string email)
        {
            ViewBag.Email = email;
            return View();
        }
        public ActionResult SaveTimeLog(string type, string email)
        {
            var success = default(bool);
            var message = string.Empty;

            var profile = entity.UserProfiles.Where(user => user.Email == email).FirstOrDefault();

            if (profile is null)
            {
                message = "invalid email address";
                return Json(new { success, message }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                TimeLog timeLog = new TimeLog();
                switch (type)
                {
                    case "timein":
                        timeLog.CreatedBy = email;
                        timeLog.CreatedOn = DateTime.Now;
                        timeLog.Date = DateTime.Now;
                        timeLog.TimeLogTypeId = 1;
                        timeLog.UserProfileId = profile.Id;
                        break;
                    case "timeout":
                        timeLog.CreatedBy = email;
                        timeLog.CreatedOn = DateTime.Now;
                        timeLog.Date = DateTime.Now;
                        timeLog.TimeLogTypeId = 2;
                        timeLog.UserProfileId = profile.Id;
                        break;
                    default:
                        break;
                }

                entity.TimeLogs.Add(timeLog);
                entity.SaveChanges();

                message = "success";
                success = true;
                return Json(new { success, message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { success, message }, JsonRequestBehavior.AllowGet);
            }
         }
    }
}