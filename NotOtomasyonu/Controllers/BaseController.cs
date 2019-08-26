using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NotOtomasyonu.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotOtomasyonu.Controllers
{
    public class BaseController : Controller
    {
        public UserManager<ApplicationUser> userManager;
        public RoleManager<IdentityRole> roleManager;
        public NotOtomasyonuEntities db = new NotOtomasyonuEntities();
        public BaseController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));
        }
       
    }
}