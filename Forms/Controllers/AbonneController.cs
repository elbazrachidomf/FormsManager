using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forms.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Forms.Controllers
{
    public class AbonneController : Controller
    {
        FormsEntities db = new FormsEntities();


        public UserManager<ApplicationUser> UserManager { get; private set; }
        // GET: Abonne
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExistMail(string email)
        {
            var exist = db.Abonne.Any(a => a.Email == email);
            return Json(exist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAbonne()
        {
            var res = from a in db.Abonne
                      select new ModelAbonne
                      {
                          id = a.Id,
                          lb = a.Libelle,
                          idCRM = a.IdCRM
                      };

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreateAbonne([Bind(Prefix = "models")] List<ModelAbonne> abonnes)
        {
            if (ModelState.IsValid)
            {
                foreach (var abonne in abonnes)
                {
                    Abonne abn = new Abonne { Libelle = abonne.lb };
                    db.Abonne.Add(abn);
                    db.SaveChanges();
                    abonne.id = abn.Id;
                }
            }
            return Json(abonnes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateAbonne([Bind(Prefix = "models")] List<ModelAbonne> abonnes)
        {
            if (ModelState.IsValid)
            {
                foreach (var abonne in abonnes)
                {
                    var abn = db.Abonne.FirstOrDefault(f => f.Id == abonne.id);
                    abn.Libelle = abonne.lb;
                }
                db.SaveChanges();
            }
            return Json(abonnes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadUser(int? idAbonne)
        {
            var currentAbonne = User.Identity.GetGroupe();

            if (idAbonne.HasValue)
                currentAbonne = idAbonne.Value;

            var res = from u in db.AspNetUsers
                     where u.Fk_Abonne == currentAbonne
                      select new ModelUser
                      {
                          id = u.Id,
                          name = u.UserName,
                          email = u.Email,
                          //civilite = u.Fk_Civilite
                          //password = u.PasswordHash
                      };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUser([Bind(Prefix = "models")] List<ModelUser> users, int idAbonne)
        {
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                { 
                    UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                    var user1 = new ApplicationUser { UserName = user.name, Email = user.email, Fk_Abonne = idAbonne /*, Fk_Civilite = user.civilite*/};
                    UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var result = UserManager.Create(user1, user.password);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.ToString());
                    }
                    else
                    {
                        ModifierUtilisateur(user1.Id, user);
                        user.id = user1.Id;
                    }
                }
            }
                return Json(new[] { users });
        }


        private void ModifierUtilisateur(string userID, ModelUser user)
        {
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var userDB = db.AspNetUsers.FirstOrDefault(x => x.Id == userID);
            userDB.PhoneNumber = user.tel;
            userDB.Email = user.email;
            userDB.UserName = user.name;
            userManager.RemovePassword(userID);
            userManager.AddPassword(userID, user.password);
            db.SaveChanges();
        }

        public ActionResult UpdatUser([Bind(Prefix = "models")] List<ModelUser> users)
        {
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var utilisateur = db.AspNetUsers.FirstOrDefault(f => f.Id == user.id);
                    utilisateur.UserName = user.name;
                    utilisateur.Email = user.email;

                }
                db.SaveChanges();
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser([Bind(Prefix = "models")] List<ModelUser> users)
        {
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var utilisateur = db.AspNetUsers.FirstOrDefault(f => f.Id == user.id);
                    db.AspNetUsers.Remove(utilisateur);
                }
                db.SaveChanges();
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }


    }
}