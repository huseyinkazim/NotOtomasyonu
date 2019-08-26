using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NotOtomasyonu.Identity;
using NotOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotOtomasyonu.Controllers
{

    public class HomeController : BaseController
    {
        public HomeController()
        {
            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 7,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = true
            };
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        // GET: Home
        public ActionResult Index()
        {
            if (User.IsInRole("Mudur"))
            {
                return RedirectToAction("Index", "Home", new { Area = "Mudur" });
            }
            else if (User.IsInRole("Ogrenci"))
            {
                return RedirectToAction("OgrenciBilgisi", "Home", new { Area = "Ogrenci" });
            }
            else if (User.IsInRole("Ogretmen"))
            {
                return RedirectToAction("DersListele", "Home", new { Area = "Ogretmen" });
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginMudur(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            string name = User.Identity.Name;
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LoginMudur(LoginMudur model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.MudurId, model.Sifre);
                if (user != null) { 
                    var roles = userManager.GetRoles(user.Id);

                    if (roles.Count(i => i == "Mudur") != 0)
                    {
                        var authManager = HttpContext.GetOwinContext().Authentication;

                        var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        var authProperties = new AuthenticationProperties()
                        {
                            IsPersistent = true
                        };
                        authManager.SignOut();
                        authManager.SignIn(authProperties, identity);
                        return RedirectToAction("index", "home", new { area = "Mudur"});
                    }
                    else
                    {
                        ModelState.AddModelError("error", "Yanlış Kullanıcı Adı veya Şifre");
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "Yanlış Kullanıcı Adı veya Şifre");
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginOgrenci(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            string name = User.Identity.Name;
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LoginOgrenci(LoginOgrenci model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.OgrenciNo, model.Sifre);
                if (user != null)
                {
                    var roles = userManager.GetRoles(user.Id);

                    if (roles.Count(i => i == "Ogrenci") != 0)
                    {
                        var authManager = HttpContext.GetOwinContext().Authentication;

                        var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        var authProperties = new AuthenticationProperties()
                        {
                            IsPersistent = true
                        };
                        authManager.SignOut();
                        authManager.SignIn(authProperties, identity);

                        return RedirectToAction("OgrenciBilgisi", "home", new { area = "Ogrenci" });
                    }
                    else
                    {
                        ModelState.AddModelError("error", "Yanlış Kullanıcı Adı veya Şifre");
                    }
                }
                else
                {
                    ModelState.AddModelError("error","Yanlış Kullanıcı Adı veya Şifre");
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginOgretmen(string returnUrl)
        {
            string name = User.Identity.Name;
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LoginOgretmen(LoginOgretmen model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.OgretmenId, model.Sifre);
                if (user != null)
                {
                    var roles = userManager.GetRoles(user.Id);

                    if (roles.Count(i => i == "Ogretmen") != 0)
                    {
                        var authManager = HttpContext.GetOwinContext().Authentication;

                        var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        var authProperties = new AuthenticationProperties()
                        {
                            IsPersistent = true
                        };
                        authManager.SignOut();
                        authManager.SignIn(authProperties, identity);
                        return RedirectToAction("DersListele", "home", new { area = "Ogretmen"});
                    }
                    else
                    {
                        ModelState.AddModelError("error", "Yanlış Kullanıcı Adı veya Şifre");
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "Yanlış Kullanıcı Adı veya Şifre");
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
    }
}
