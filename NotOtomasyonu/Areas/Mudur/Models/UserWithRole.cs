using NotOtomasyonu.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotOtomasyonu.Areas.Mudur.Models
{
    public class UserWithRole
    {
        public ApplicationUser user { get; set; }
        public IList<string> Roles { get; set; }
    }
}