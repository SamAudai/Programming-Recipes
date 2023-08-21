using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Programming_Recipes.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="لغة البرمجة")]
        public string LanguageName { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } //many
    }
}