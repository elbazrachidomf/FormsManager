using Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forms.Controllers
{
    //public class ModelForm
    //{
    //    public int id { get; set; }
    //    public string title { get; set; }
    //    public string desc { get; set; }
    //    public int? duration { get; set; }
    //}

    public class FormController : Controller
    {
        private FormsEntities dbContext = new FormsEntities();

        private GlobalViewModel GViewModel = new GlobalViewModel();

        private FormTitleViewModel formTitleViewModel = new FormTitleViewModel();
        // GET: Form
        public ActionResult Index()
        {

            return View(GViewModel);
        }




        [HttpGet]
        public ActionResult FormTitle(int ID = 0)
        {
            if (ID > 0)
            {
                var result = dbContext.Form.Include("Section").FirstOrDefault(x => x.ID == ID);

                GViewModel.formTitleViewModel.ID = result.ID;
                GViewModel.formTitleViewModel.sections = result.Section;
                GViewModel.formTitleViewModel.Title = result.Title;
                GViewModel.formTitleViewModel.IDGroup = result.IdGroup;
                GViewModel.formTitleViewModel.FormGroup = result.FormGroup;
                GViewModel.formTitleViewModel.Duration = result.Duration;
                GViewModel.formTitleViewModel.Desc = result.Desc;
            }

            //GViewModel.formTitleViewModel = formTitleViewModel;

            return PartialView("_FormTitle", GViewModel);
        }
        [HttpPost]
        public ActionResult FormTitle(FormTitleViewModel model)
        {
            Form form = new Form();
            form.Desc = model.Desc;
            form.Duration = model.Duration;
            form.Title = model.Title;

            if (model.ID != 0)
            {
                form = dbContext.Form.FirstOrDefault(x => x.ID == model.ID);
                form.Desc = model.Desc;
                form.Duration = model.Duration;
                form.Title = model.Title;

            }
            else
            {
                dbContext.Form.Add(form);
            }
            dbContext.SaveChanges();
            //try
            //{

            GViewModel.formTitleViewModel = new FormTitleViewModel()
            {
                ID = form.ID,
                Desc = form.Desc,
                Duration = form.Duration,
                Title = form.Title
            };
            //}
            //catch
            //{

            //}

            return RedirectToAction("FormTitle", new { ID = GViewModel.formTitleViewModel.ID });
        }

        [HttpGet]
        public ActionResult createSection(int? formID = 0)
        {

            var vMModel = new List<SectionViewModel>();
            var sections = dbContext.Section.Where(x => x.FormId == formID).ToList();
            foreach (var section in sections)
            {
                SectionViewModel s = new SectionViewModel()
                {
                    ID = section.ID,
                    Desc = section.Desc,
                    FormId = section.FormId,
                    Title = section.Title
                };

                vMModel.Add(s);
            }

            return PartialView("_createSection", vMModel);
        }
        [HttpPost]
        public ActionResult createSection(SectionViewModel model)
        {
            int formId = int.Parse(HttpContext.Request.Form["formId"]);
            if (formId > 0 || model.FormId > 0)
            {
                Section section = new Section();
                section.Desc = model.Desc;
                section.Title = model.Title;
                section.FormId = model.FormId;
                if (model.ID > 0)
                {
                    dbContext.Section.Add(section);
                }
                else
                {
                    section = dbContext.Section.FirstOrDefault(x => x.ID == section.ID);
                    section.Desc = model.Desc;
                    section.Title = model.Title;
                }

                dbContext.SaveChanges();
                return RedirectToAction("createSection", model.ID);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }


        }

       

        public ActionResult GET_DataIconFieldGroup()
        {
            var result = from DataIcon in dbContext.DataIconFieldGroup
                         select new
                         {
                             key = DataIcon.key,
                             label = DataIcon.Label,
                             className = DataIcon.ClassName,
                             Gp = DataIcon.Group
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GET_DataField()
        {
            var result = from DataField in dbContext.DataField
                         select new 
                         {
                             key = DataField.Key,
                             desc = DataField.Desc,
                             type = DataField.Type
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ReadForms()
        //{
        //    var res = from f in dbContext.Form
        //              select new ModelForm
        //              {
        //                  id = f.ID,
        //                  title = f.Title,
        //                  desc = f.Desc,
        //                  duration = f.Duration,
        //              };
        //    return Json(res, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult UpdateForm([Bind(Prefix = "models")] List<ModelForm> forms)
        //{
        //    foreach (var form in forms)
        //    {
        //        var formDB = dbContext.Form.FirstOrDefault(f => f.ID == form.id);
        //        formDB.Title = form.title;
        //        formDB.Desc = form.desc;
        //        formDB.Duration = form.duration;                
        //    }
        //    dbContext.SaveChanges();
        //    return Json(forms, JsonRequestBehavior.AllowGet);
        //}

    }
}