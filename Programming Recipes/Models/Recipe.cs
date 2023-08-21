using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Programming_Recipes.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [DisplayName("إسم الوصفة")]
        public string RecipeTitle { get; set; }
        
        [DisplayName("شرح الوصفة")]
        [AllowHtml]
        public string RecipeContent { get; set; }

        [DisplayName("ملف الوصفة")]
        public string RecipeFile { get; set; }

        [DisplayName("لغة البرمجة")]
        public int LanguageId { get; set; }
        
        public string UserId { get; set; }

        public virtual Language Language { get; set; } //one

        //ناشر الوظيفة
        public virtual ApplicationUser User { get; set; }//واحد هنا والكثير في جدول المستخدم

    }
}