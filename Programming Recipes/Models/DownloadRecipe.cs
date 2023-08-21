using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Programming_Recipes.Models
{
    public class DownloadRecipe
    {
        public int Id { get; set; }

        [AllowHtml]
        [DisplayName("إسم الملف")]
        public string Message { get; set; }

        [DisplayName("تاريخ التحميل")]
        public DateTime DownloadDate { get; set; }
        public int RecipeId { get; set; }
        public string UserId { get; set; }

        public virtual Recipe recipe { get; set; } //نتيجة لعلاقة كثير لكثير بين جدول الأكواد وجدول المستخدميين
        public virtual ApplicationUser user { get; set; }
    }
}