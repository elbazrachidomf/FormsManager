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


        //public JsonResult GetFormGroups()
        //{
        //    using (var dbContext= new FormsEntities())
        //    {
        //        var ressult = from gf in dbContext.FormGroup
        //                      select new FormGroupsViewModel
        //                      {
        //                        id = gf.ID,
        //                         name = gf.Name,
        //                        desc = gf.Desc
        //                     };
        ////var ressult = dbcontext.FormGroup.ToList();
        //        return Json(ressult, JsonRequestBehavior.AllowGet);
        //    }    

        //}

        //public string GetFormGroups()
        //{
        //    List<FormGroupsViewModel> LFG = new List<FormGroupsViewModel>();
        //    using (var dbContext = new FormsEntities())
        //    {
        //        foreach (var item in dbContext.FormGroup.ToList())
        //        {
        //            FormGroupsViewModel fg = new FormGroupsViewModel { desc = item.Desc, name = item.Name, id = item.ID };
        //            LFG.Add(fg);
        //        }

        //    }
        //    return Serialiser.SerializeObject(new { Data = LFG });
        //}

        public ActionResult UpdateInline(FormGroupsViewModel fgrvm)
        {

            var newGRP = dbContext.FormGroup.FirstOrDefault(gr => gr.ID == fgrvm.id);
            newGRP.Desc = fgrvm.desc;
            newGRP.Name = fgrvm.name;

            dbContext.SaveChanges();

            return Json(fgrvm, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteInline(FormGroupsViewModel fgrvm)
        {

            FormGroup Nfgrvm = dbContext.FormGroup.FirstOrDefault(Nf => Nf.ID == fgrvm.id);
            dbContext.FormGroup.Remove(Nfgrvm);
            dbContext.SaveChanges();

            return Json(fgrvm, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateInline(FormGroupsViewModel fgrvm)
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



    }
}