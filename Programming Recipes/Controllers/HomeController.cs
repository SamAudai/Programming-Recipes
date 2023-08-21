using Microsoft.AspNet.Identity;
using Programming_Recipes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        public ActionResult Details(int RecipeId)
        {
            var recipe = db.Recipes.Find(RecipeId);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            Session["RecipeId"] = RecipeId;
            return View(recipe);
        }

        [Authorize]
        public ActionResult Comment()
        {
            return View();
        }

     
        //[HttpPost]
        //public ActionResult Comment(string Message) //تحديد البيانات التي نريد إرسالها عند الضغط على الزر
        //{
        //    var UserId = User.Identity.GetUserId();
        //    var RecirpeId = (int)Session["RecipeId"];

        //    var check = db.DownloadRecipes.Where(a => a.RecipeId == RecirpeId && a.UserId == UserId).ToList();
        //    if (check.Count < 1)
        //    {
        //        var Recipe = new DownloadRecipe();
        //        Recipe.UserId = UserId;
        //        Recipe.RecipeId = RecirpeId;
        //        Recipe.Message = Message;
               
        //        Recipe.DownloadDate = DateTime.Now;

        //        db.DownloadRecipes.Add(Recipe);
        //        db.SaveChanges();
        //        ViewBag.Result = "تمت إضافة تعليقك بنجاح";
        //}
        //    else
        //    {
        //        ViewBag.Result = "المعذرة لقد قمت بالتعليق على هذه الوصفة";
        //    }

        //    return View();
        //}

        [Authorize]
        public ActionResult Downloads()
        {
            var dir = new DirectoryInfo(Server.MapPath("~/App_Data/Uploads/"));
            FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }

        public FileResult Download(string ImageName)
        {
            var UserId = User.Identity.GetUserId();
            var RecirpeId = (int)Session["RecipeId"];

            var Recipe = new DownloadRecipe();
            Recipe.UserId = UserId;
            Recipe.RecipeId = RecirpeId;
            Recipe.DownloadDate = DateTime.Now;
            Recipe.Message = ImageName;

            db.DownloadRecipes.Add(Recipe);
            db.SaveChanges();

            var FileVirtualPath = "~/App_Data/Uploads/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        [Authorize]
        public ActionResult GetRecipeByUser() //الحصول على المستخدم المسجل للدخول
        {
            var UserId = User.Identity.GetUserId();
            var Recipes = db.DownloadRecipes.Where(a => a.UserId == UserId);
            return View(Recipes.ToList());
        }

        [Authorize]
        public ActionResult GetRecipeByPublisher()
        {
            var UserID = User.Identity.GetUserId();
            var Recipes = from app in db.DownloadRecipes
                          join recipe in db.Recipes
                          on app.RecipeId equals recipe.Id
                          where recipe.User.Id==UserID
                          select app;

            return View(Recipes.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("audai123@yahoo.com", "Aud@i010rama");
            mail.From = new MailAddress(contact.Email);//معلومات بريد المرسل للرسالة
            mail.To.Add(new MailAddress("audai123@yahoo.com"));//البريد المراد الإرسال له
            mail.IsBodyHtml = true; //من أجل التعامل مع أوامر الهتمل قي الصفحة
            mail.Subject = contact.Subject;  //موضوع الرسالة
            string body = "إسم المرسل : " + contact.Name + "<br>" +
                        "بريد المرسل :" + contact.Email + "<br>" +
                        "عنوان الرسالة : " + contact.Subject + "<br>" +
                        "نص الرسالة : " + contact.Massage;
            mail.Body = contact.Massage;    //نص الرسالة

            //عملية الإرسال
            var smtpClient = new SmtpClient("smtp.mail.yahoo.com", 587);//The dafault Yahoo mail smtp port is 587
            
            smtpClient.EnableSsl = true; //تمكين الوضع الآمن
            smtpClient.Credentials = loginInfo;  ///مغلومات الإرسال
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Recipes.Where(a => a.RecipeTitle.Contains(searchName)
             || a.RecipeContent.Contains(searchName)
             || a.Language.LanguageName.Contains(searchName)).ToList();

            return View(result);
        }
    }
}