using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NotOtomasyonu.Controllers;
using NotOtomasyonu.Identity;
using NotOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotOtomasyonu.Areas.Ogretmen.Controllers
{
    [Authorize(Roles = "Ogretmen")]
    public class HomeController : BaseController
    {
        // GET: Ogretmen/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("index", "Home", new { Area = "" });
        }

        public ActionResult OgrenciNotlariniGoruntule(string dersId)
        {
            var notlar = (from not in db.NotDbs where not.DersId == dersId select not).ToList();
            var ogretmen = db.OgretmenDbs.FirstOrDefault(i => i.Id == User.Identity.Name);
            if(ogretmen.DersDbs.Count(i=>i.DersId==dersId)==0)
            {
                return RedirectToAction("DersListele","Home");
            }
           
            return View(notlar);
        }
        public ActionResult DersListele()
        {
            string id = User.Identity.Name;
            OgretmenDb aUser = db.OgretmenDbs.FirstOrDefault(i=>i.Id==User.Identity.Name);
            return View(aUser);
        }
        public ActionResult OgrenciNotlariniGuncelle(string notId, string dersId)
        {
            NotDb not = db.NotDbs.FirstOrDefault(i => i.NotId == notId);
            var ders = db.DersDbs.FirstOrDefault(i => i.DersId == dersId);

            if (ders.NotDbs.Count(i => i.NotId == notId) == 0)
            {
                return RedirectToAction("DersListele", "Home");
            }
            
            return View(not);
        }
        [HttpPost]
        public ActionResult OgrenciNotlariniGuncelle(NotDb model)
        {
            NotOtomasyonuEntities db = new NotOtomasyonuEntities();
            var notupdate = db.NotDbs.FirstOrDefault(i => i.NotId == model.NotId);
            if (notupdate != null)
            {
                notupdate.Sinav1 = model.Sinav1;
                notupdate.Sinav2 = model.Sinav2;
                notupdate.Sinav3 = model.Sinav3;
                notupdate.Sozlu1 = model.Sozlu1;
                notupdate.Sozlu2 = model.Sozlu2;
                notupdate.Sozlu3 = model.Sozlu3;
                db.SaveChanges();
            }
            return RedirectToAction("OgrenciNotlariniGoruntule",new { dersId=notupdate.DersId});
        }
    }
}