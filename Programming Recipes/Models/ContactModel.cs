using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Programming_Recipes.Models
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "الإسم")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "عنوان الرسالة")]
        public string Subject { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "نص الرسالة")]
        public string Massage { get; set; }
    }
}