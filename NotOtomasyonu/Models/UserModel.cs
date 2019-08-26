using Microsoft.AspNet.Identity.EntityFramework;
using NotOtomasyonu.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotOtomasyonu.Models
{
    public class LoginOgrenci
    {
        [Required]
        public string OgrenciNo { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
    public class RegisterOgrenci
    {
        [Required]
        public string OgrenciNo { get; set; }
        [Required]
        public string OgrenciIsim { get; set; }
        [Required]
        public string OgrenciSoyisim { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
    public class LoginOgretmen
    {
        [Required]
        public string OgretmenId { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
    public class RegisterOgretmen
    {
        [Required]
        public string OgretmenId { get; set; }
        [Required]
        public string OgretmenIsim { get; set; }
        [Required]
        public string OgretmenSoyisim { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
    public class LoginMudur
    {
        [Required]
        public string MudurId { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
    public class RegisterMudur
    {
        [Required]
        public string MudurId { get; set; }
        [Required]
        public string MudurIsim { get; set; }
        [Required]
        public string MudurSoyisim { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
}