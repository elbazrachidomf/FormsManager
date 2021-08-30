using Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forms.Controllers
{
    public class FormsManagerController : Controller
    {
        private FormsEntities dbContext = new FormsEntities();
        // GET: FormsManager
        public ActionResult Index()
        {
            return View();
        }
        //*************************** prepared field *****************************
        #region Prepared Field
        public ActionResult GetPreparedFields(int? idGroup)
        {
            var res = from F in dbContext.PreparedFiled
                      where (!idGroup.HasValue || idGroup.Value==F.PreparedGroupFieldID)
                      select new PreparedFieldViewModel
                      {
                         ID=F.ID,
                          Name=F.Name,
                           PreparedGroupFieldID=F.PreparedGroupFieldID
                      };
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreatePreparedField(PreparedFieldViewModel preparedFieldViewModel)
        {
            PreparedFiled preparedField = new PreparedFiled
            {
                Name = preparedFieldViewModel.Name,
            };
            dbContext.PreparedFiled.Add(preparedField);
            dbContext.SaveChanges();
            preparedFieldViewModel.ID = preparedField.ID;
            return Json(preparedFieldViewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdatePreparedField(PreparedFieldViewModel preparedFieldViewModel)
        {
            var res = dbContext.PreparedFiled.FirstOrDefault(fg => fg.ID == preparedFieldViewModel.ID);
            res.Name = preparedFieldViewModel.Name;
            dbContext.SaveChanges();
            return Json(preparedFieldViewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePreparedField(PreparedFieldViewModel preparedFieldViewModel)
        {
            PreparedFiled preparedField = dbContext.PreparedFiled.FirstOrDefault(fg => fg.ID == preparedFieldViewModel.ID);
            dbContext.PreparedFiled.Remove(preparedField);
            dbContext.SaveChanges();
            return Json(preparedFieldViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //*************************** prepared field group *****************************
        #region field group
        public ActionResult GetPreparedFieldGroups()
        {
            var res = from FG in dbContext.PreparedGroupField
                      select new PreparedFieldGroupViewModel {
                          ID=FG.ID,
                          Name=FG.Name,
                          QuestionID =FG.PreparedQuestionId
                       };
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreatePreparedFieldGroup(PreparedFieldGroupViewModel preparedFieldGroupViewModel)
        {
            PreparedGroupField preparedGroupField = new PreparedGroupField
            {
                Name = preparedFieldGroupViewModel.Name,
            };
            dbContext.PreparedGroupField.Add(preparedGroupField);
            dbContext.SaveChanges();
            preparedFieldGroupViewModel.ID = preparedGroupField.ID;
            return Json(preparedFieldGroupViewModel,JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdatePreparedFieldGroup(PreparedFieldGroupViewModel preparedFieldGroupViewModel)
        {
            var res = dbContext.PreparedGroupField.FirstOrDefault(fg => fg.ID == preparedFieldGroupViewModel.ID);
            res.Name = preparedFieldGroupViewModel.Name;           
            dbContext.SaveChanges();
            return Json(preparedFieldGroupViewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePreparedFieldGroup(PreparedFieldGroupViewModel preparedFieldGroupViewModel)
        {
            PreparedGroupField preparedGroupField = dbContext.PreparedGroupField.FirstOrDefault(fg=>fg.ID==preparedFieldGroupViewModel.ID);
            dbContext.PreparedGroupField.Remove(preparedGroupField);
            dbContext.SaveChanges();
            return Json(preparedFieldGroupViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //************************ prepared Questions***********************************
        #region Questions

        public ActionResult GetQuestionType()
        {
            var res = from QT in dbContext.QType
                      select new
                      {
                         Id=QT.ID,
                         Value=QT.VALUE
                      };
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreparedQuestions()
        {
            var res = from PrQ in dbContext.PreparedQuestion
                      select new PreparedQuestionViewModel
                      {
                          Desc = PrQ.Desc
                      };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetQuestion(int? idSection)
        //{
        //    var res = from q in dbContext.Question
        //              where (!idSection.HasValue || q.SectionID == idSection.Value)
        //              join s in dbContext.Section on
        //              q.ID equals s.ID
        //              select new QuestionViewModel
        //              {
        //                  Desc = q.Desc,
        //                  //SectionID = q.SectionID,
        //                  //SectionTitle = s.Title
        //              };
        //    return Json(res, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult UpdatePreparedQuestion(PreparedQuestionViewModel preparedQuestionViewModel)
        {

            var newQ = dbContext.PreparedQuestion.FirstOrDefault(q => q.ID == preparedQuestionViewModel.ID);
            newQ.Desc = preparedQuestionViewModel.Desc;
            //newQ.SectionID = questionViewModel.SectionID;
            dbContext.SaveChanges();


            //questionViewModel.SectionID = newQ.Section.ID;

            return Json(preparedQuestionViewModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeletePreparedQuestion(PreparedQuestionViewModel preparedQuestionViewModel)
        {

            PreparedQuestion Q = dbContext.PreparedQuestion.FirstOrDefault(q => q.ID == preparedQuestionViewModel.ID);
            dbContext.PreparedQuestion.Remove(Q);
            dbContext.SaveChanges();

            return Json(preparedQuestionViewModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreatePreparedQuestion(PreparedQuestionViewModel preparedQuestionViewModel)
        {
            if (!string.IsNullOrEmpty(preparedQuestionViewModel.Desc))
            {
                PreparedQuestion newQ = new PreparedQuestion()
                {
                    Desc = preparedQuestionViewModel.Desc,
                };
                dbContext.PreparedQuestion.Add(newQ);
                dbContext.SaveChanges();
                preparedQuestionViewModel.ID = newQ.ID;
                return Json(preparedQuestionViewModel, JsonRequestBehavior.AllowGet);
            }


            return PartialView("_CreateQuestion");   
        }


        #endregion
        //******************************SECTION********************************************
        #region Section

        public ActionResult GetSection(int? idForm)
        {
            var res = from s in dbContext.Section
                      where (!idForm.HasValue || s.FormId == idForm.Value)
                      join f in dbContext.Form on
                      s.FormId equals f.ID
                      select new SectionViewModel
                      {
                          ID = s.ID,
                          Title = s.Title,
                          Desc = s.Desc,
                          fTitle = f.Title,
                          Number = s.Number,
                          FormId = s.FormId,

                      };
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSection(SectionViewModel sectionViewModel)
        {

            var newS = dbContext.Section.FirstOrDefault(s => s.ID == sectionViewModel.ID);
            newS.Desc = sectionViewModel.Desc;
            newS.Title = sectionViewModel.Title;
            newS.Number = sectionViewModel.Number;
            newS.FormId = sectionViewModel.FormId;
            dbContext.SaveChanges();

            sectionViewModel.FormId = newS.Form.ID;

            return Json(sectionViewModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteSection(SectionViewModel sectionViewModel)
        {

            Section S = dbContext.Section.FirstOrDefault(s => s.ID == sectionViewModel.ID);
            dbContext.Section.Remove(S);
            dbContext.SaveChanges();

            return Json(sectionViewModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateSection(SectionViewModel sectionViewModel)
        {
            if (!string.IsNullOrEmpty(sectionViewModel.Title) && !string.IsNullOrEmpty(sectionViewModel.Desc))
            {
                Section newS = new Section()
                {
                    Desc = sectionViewModel.Desc,
                    Title = sectionViewModel.Title,
                    FormId = sectionViewModel.FormId,
                    Number = sectionViewModel.Number
                };

                dbContext.Section.Add(newS);
                dbContext.SaveChanges();
                sectionViewModel.ID = newS.ID;
                sectionViewModel.fTitle = dbContext.Section.FirstOrDefault(s => s.ID == newS.ID).Title;
                //return Json(sectionViewModel, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_CreateSection");
        }
        #endregion
        //************************form**********************************
        #region Form
        public ActionResult GetForms(int? idGroupe)
        {
            var res = from f in dbContext.Form
                      where (!idGroupe.HasValue || f.IdGroup == idGroupe.Value)
                      join fg in dbContext.FormGroup
                      on f.IdGroup equals fg.ID
                      select new FormViewModel
                      {
                          id = f.ID,
                          title = f.Title,
                          desc = f.Desc,
                          duration = f.Duration,
                          IdGroup = f.IdGroup,
                          groupName = fg.Name

                      };
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateForm(FormViewModel formViewModel)
        {

            var newF = dbContext.Form.FirstOrDefault(f => f.ID == formViewModel.id);
            newF.Desc = formViewModel.desc;
            newF.Title = formViewModel.title;
            newF.Duration = formViewModel.duration;
            newF.IdGroup = formViewModel.IdGroup;

            dbContext.SaveChanges();

            formViewModel.groupName = newF.FormGroup.Name;

            return Json(formViewModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteForm(FormViewModel formViewModel)
        {

            Form newF = dbContext.Form.FirstOrDefault(Nf => Nf.ID == formViewModel.id);
            dbContext.Form.Remove(newF);
            dbContext.SaveChanges();

            return Json(formViewModel, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateForm(FormViewModel formViewModel)
        {
            Form newF = new Form()
            {
                Desc = formViewModel.desc,
                Duration = formViewModel.duration,
                Title = formViewModel.title,
                IdGroup = formViewModel.IdGroup,

            };

            dbContext.Form.Add(newF);
            dbContext.SaveChanges();
            formViewModel.id = newF.ID;
            return Json(formViewModel, JsonRequestBehavior.AllowGet);
        }
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
        #endregion
        //************************Group form**********************************
        #region Group Form

        public ActionResult GetFormGroups()
        {

            var ressult = from gf in dbContext.FormGroup
                          select new FormGroupsViewModel
                          {
                              id = gf.ID,
                              name = gf.Name,
                              desc = gf.Desc
                          };
            return Json(ressult, JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpdateGroup(FormGroupsViewModel fgrvm)
        {

            var newGRP = dbContext.FormGroup.FirstOrDefault(gr => gr.ID == fgrvm.id);
            newGRP.Desc = fgrvm.desc;
            newGRP.Name = fgrvm.name;

            dbContext.SaveChanges();

            return Json(fgrvm, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteGroup(FormGroupsViewModel fgrvm)
        {

            FormGroup Nfgrvm = dbContext.FormGroup.FirstOrDefault(Nf => Nf.ID == fgrvm.id);
            dbContext.FormGroup.Remove(Nfgrvm);
            dbContext.SaveChanges();

            return Json(fgrvm, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateGroup(FormGroupsViewModel fgrvm)
        {
            FormGroup Nfgrvm = new FormGroup()
            {
                Name = fgrvm.name,
                Desc = fgrvm.desc
            };

            dbContext.FormGroup.Add(Nfgrvm);
            dbContext.SaveChanges();
            fgrvm.id = Nfgrvm.ID;
            return Json(fgrvm, JsonRequestBehavior.AllowGet);
        }
        #endregion


       

         
    }
}