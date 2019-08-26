using System.Web.Mvc;

namespace NotOtomasyonu.Areas.Ogretmen
{
    public class OgretmenAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ogretmen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Ogretmen_default",
                "Ogretmen/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NotOtomasyonu.Areas.Ogretmen.Controllers" }
            );
        }
    }
}