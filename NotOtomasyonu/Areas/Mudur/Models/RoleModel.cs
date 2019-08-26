using Microsoft.AspNet.Identity.EntityFramework;
using NotOtomasyonu.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotOtomasyonu.Areas.Mudur.Models
{
        public class RoleEditModel
        {
            public IdentityRole Role { get; set; }
            public IEnumerable<ApplicationUser> Members { get; set; }
            public IEnumerable<ApplicationUser> NonMembers { get; set; }
        }
        public class RoleUpdateModel
        {
            [Required]
            public string RoleName { get; set; }
            public string RoleId { get; set; }
            public string[] IdsToAdd { get; set; }
            public string[] IdsToDelete { get; set; }
        }

        public class dersModel
        {
            public string ogretmenId { get; set; }
            public string dersId { get; set; }
        }
}   