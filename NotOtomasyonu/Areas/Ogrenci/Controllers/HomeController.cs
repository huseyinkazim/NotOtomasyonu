﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NotOtomasyonu.Controllers;
using NotOtomasyonu.Identity;
using NotOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotOtomasyonu.Areas.Ogrenci.Controllers
{
    [Authorize(Roles = "Ogrenci")]
    public class HomeController : BaseController
    {
        // GET: Ogrenci/Home
        public ActionResult Index(string id)
        {
            ViewBag.ogrenciNo = id;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("index", "Home", new { Area = "" });
        }

        public ActionResult OgrenciBilgisi()
        {
            string id = User.Identity.Name;
            if (id != User.Identity.Name)
            {
                return RedirectToAction("OgrenciBilgisi", "Home", new { id = User.Identity.Name });
            }
            var OgrenciNo = User.Identity.Name;
            var ogrenci = db.OgrenciDbs.FirstOrDefault(w => w.No == OgrenciNo);
            return View(ogrenci);
        }

        public ActionResult OgrenciNotuListele()
        {
            var OgrenciNo = User.Identity.Name;
            var ogrenci = db.OgrenciDbs.FirstOrDefault(w => w.No == OgrenciNo);
            return View(ogrenci);
        }

        public ActionResult DersSecimi()
        {
            List<DersDb> MevcutOlmayanDersler = new List<DersDb>();
            NotOtomasyonuEntities db = new NotOtomasyonuEntities();
            var dersler = from ders in db.DersDbs select ders;
            var mevcutDersler = from ders in db.DersDbs
                                join not in db.NotDbs on ders.DersId
                                equals not.DersId
                                where not.OgrenciNo == User.Identity.Name
                                select ders;
            foreach (DersDb aDers in dersler)
            {
                MevcutOlmayanDersler.Add(aDers);
            }
            foreach (DersDb aDers in dersler)
            {
                foreach (DersDb aMevcutDers in mevcutDersler)
                {
                    if (aDers.DersId == aMevcutDers.DersId)
                    {
                        MevcutOlmayanDersler.Remove(aDers);
                    }
                }
            }
            ViewBag.mevcut = mevcutDersler;
            ViewBag.mevcutOlmayan = MevcutOlmayanDersler;
            return View(dersler);
        }
        public ActionResult SecilenDers(string dersId)
        {
            string ogrId = User.Identity.Name;
            string notId = string.Concat(dersId, ogrId);
            NotDb not = new NotDb
            {
                DersId = dersId,
                OgrenciNo = ogrId,
                NotId = notId
            };
            db.NotDbs.Add(not);
            db.SaveChanges();
            return RedirectToAction("DersSecimi");
        }
        public ActionResult SilinecekDers(string dersId)
        {
            string ogrId = User.Identity.Name;
            var not = (from aNot in db.NotDbs join aDers in db.DersDbs on aNot.DersId equals aDers.DersId join aOgrenci in db.OgrenciDbs on aNot.OgrenciNo equals aOgrenci.No where aNot.OgrenciNo == ogrId && aDers.DersId == dersId select aNot).FirstOrDefault();
            db.NotDbs.Remove(not);
            db.SaveChanges();
            return RedirectToAction("DersSecimi");
        }
    }
}