using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MasterDataController : Controller
    {
        SysDbEntities entity = new SysDbEntities();
        // GET: MasterData
        public ActionResult Index()
        {
            var data = new List<MasterDataModel>();
            try
            {
                var employeeUserTypeId = EmployeeUserTypeId();
                var dataFromDb = entity.UserProfiles.Where(x => x.UserTypeId == employeeUserTypeId);

                if (dataFromDb.Any())
                {
                    foreach (var d in dataFromDb)
                    {
                        data.Add(new MasterDataModel()
                        {
                            Email = d.Email,
                            FirstName = d.FirstName,
                            Id = d.Id,
                            LastName = d.LastName,
                            UserTypeId = d.UserTypeId
                        }) ;
                    }
                }

            }
            catch (Exception)
            {

                
            }
            return View(data);
        }

        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var profile = entity.UserProfiles.Where(x => x.Id.ToString() == id).FirstOrDefault() ;
                if (profile != null)
                {

                    entity.UserProfiles.Remove(profile);
                    entity.SaveChanges();
                }

            }
            return RedirectToAction("Index", "MasterData");
        }

        public ActionResult EditEmployee(string id)
        {
            var model = new MasterDataModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var profile = entity.UserProfiles.Where(x => x.Id.ToString() == id).FirstOrDefault();
                if (profile != null)
                {
                    model.Email = profile.Email;
                    model.FirstName = profile.FirstName;
                    model.LastName = profile.LastName;
                    model.UserTypeId = profile.UserTypeId;
                    model.Password = profile.Password;
                }
            }
            else
            {
                model.UserTypeId = EmployeeUserTypeId();
            }
            return View(model);
        }

        public ActionResult Save(MasterDataModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var profile = new UserProfile();
                    profile.LastName = model.LastName;
                    profile.FirstName = model.FirstName;
                    profile.Email = model.Email;
                    profile.CreatedBy = $"{model.FirstName} {model.LastName}";
                    profile.CreatedOn = DateTime.Now;
                    profile.UserTypeId = EmployeeUserTypeId();
                    profile.Password = model.Password;
                    entity.UserProfiles.Add(profile);
                }
                else
                {
                    var profile = entity.UserProfiles.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (profile != null)
                    {
                        profile.LastName = model.LastName;
                        profile.FirstName = model.FirstName;
                        profile.Email = model.Email;
                        profile.Password = model.Password;
                        profile.ModifiedBy = $"{model.FirstName} {model.LastName}";
                        profile.ModofiedOn = DateTime.Now;
                    }
                }
                entity.SaveChanges();
            }
            catch (Exception)
            {

            }

            return RedirectToAction("Index", "MasterData");
        }

        private long EmployeeUserTypeId()
        {
            var type = entity.UserTypes.Where(x => x.Name.ToLower().Contains("employee")).FirstOrDefault();
            if (type != null)
            {
                return type.Id;
            }
            return 0;
        }
    }
}