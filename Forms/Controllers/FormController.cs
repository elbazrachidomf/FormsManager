using Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forms.Controllers
{
    public class FormController : Controller
    {
        private GlobalViewModel GViewModel = new GlobalViewModel();
        private FormsEntities dbContext = new FormsEntities();
        private FormTitleViewModel formTitleViewModel = new FormTitleViewModel();
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FormTitle()
        {
            return PartialView("_FormTitle");
        }
        [HttpPost]
        public ActionResult FormTitle(FormTitleViewModel model)
        {
            dbContext.From.Add(new From() { Desc = model.Desc, Duration = model.Duration, Title = model.Title });
            try
            {
                dbContext.SaveChanges();
            }
            catch
            {

            }

            return PartialView("_FormTitle");
        }

        public ActionResult GET_DataIconFieldGroup()
        {
            var result = from DataIcon in dbContext.DataIconFieldGroup
                         select new
                         {
                             key = DataIcon.key,
                             label = DataIcon.Label,
                             className=DataIcon.ClassName,
                             Gp=DataIcon.Group   
                         };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

         public ActionResult GET_DataField()
        {
            var result = from DataField in dbContext.DataField
                         select new
                         {
                            key= DataField.Key,
                            desc= DataField.Desc,
                            type= DataField.Type
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}