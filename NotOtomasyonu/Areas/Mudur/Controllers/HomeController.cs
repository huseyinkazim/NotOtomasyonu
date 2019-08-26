using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NotOtomasyonu.Areas.Mudur.Models;
using NotOtomasyonu.Controllers;
using NotOtomasyonu.Identity;
using NotOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NotOtomasyonu.Areas.Mudur.Controllers
{
    [Authorize(Roles = "Mudur")]
    public class HomeController : BaseController
    {
        public HomeController()
        {
           
        }
        // GET: Mudur/Home

        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            RegisterMudur aUser = new RegisterMudur();
            //ViewBag.id = id;

            var user = userManager.FindByName(id);
            if (user != null)
            {

            }
            else
            {
                // return RedirectToAction();
            }
            //foreach(ApplicationUser user in userManager.Users)
            //{
            //    if (user.UserName == id)
            //    {
            //        aUser.MudurId = id;
            //        aUser.MudurIsim = user.Name;
            //        aUser.MudurSoyisim = user.Surname;
            //    }
            //}
            return View(user);
        }
        public ActionResult SelectRoleForRegister()
        {
            return View();
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("index", "Home", new { Area = "" });
        }
        public ActionResult getUserList()
        {
            var roles = new List<ApplicationRole>();

            var users = userManager.Users.ToList().Select(i => new UserWithRole
            {
                user = i,
                Roles = userManager.GetRoles(i.Id)
            });
            return View(users);
        }

        public ActionResult DersAta()
        {
            return View();
        }
        public ActionResult OgretmeniAta(string ogretmenId, string dersId)
        {
            var updateDers = db.DersDbs.FirstOrDefault(i => i.DersId == dersId);
            if (updateDers != null)
            {
                updateDers.OgretmenId = ogretmenId;
                db.SaveChanges();
            }
            return Redirect("DersAta");
        }
        public ActionResult RegisterMudur()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterMudur(RegisterMudur model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.MudurId;
                user.Name = model.MudurIsim;
                user.Surname = model.MudurSoyisim;
                var result = userManager.Create(user, model.Sifre);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Mudur");
                    return RedirectToAction("Index", new { id = User.Identity.Name });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
        public ActionResult RegisterOgrenci()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterOgrenci(RegisterOgrenci model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.OgrenciNo;
                user.Name = model.OgrenciIsim;
                user.Surname = model.OgrenciSoyisim;
                var result = userManager.Create(user, model.Sifre);

                if (result.Succeeded)
                {
                    NotOtomasyonuEntities db = new NotOtomasyonuEntities();
                    OgrenciDb aOgrenci = new OgrenciDb();
                    aOgrenci.Ad = model.OgrenciIsim;
                    aOgrenci.Soyad = model.OgrenciSoyisim;
                    aOgrenci.No = model.OgrenciNo;
                    db.OgrenciDbs.Add(aOgrenci);
                    db.SaveChanges();
                    userManager.AddToRole(user.Id, "Ogrenci");
                    return RedirectToAction("Index", new { id = User.Identity.Name });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
        public ActionResult RegisterOgretmen()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterOgretmen(RegisterOgretmen model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.OgretmenId;
                user.Name = model.OgretmenIsim;
                user.Surname = model.OgretmenSoyisim;
                var result = userManager.Create(user, model.Sifre);

                if (result.Succeeded)
                {
                    NotOtomasyonuEntities db = new NotOtomasyonuEntities();
                    OgretmenDb aOgretmen = new OgretmenDb();
                    aOgretmen.Ad = model.OgretmenIsim;
                    aOgretmen.Soyad = model.OgretmenSoyisim;
                    aOgretmen.Id = model.OgretmenId;
                    db.OgretmenDbs.Add(aOgretmen);
                    db.SaveChanges();
                    userManager.AddToRole(user.Id, "Ogretmen");
                    return RedirectToAction("Index", new { id = User.Identity.Name });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
    }
}