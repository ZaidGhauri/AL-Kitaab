using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using AlKitab.Models;
//using AlKitab.ViewModels;

namespace AlKitab.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db;

        public HomeController()
        {
            //db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            HomeVM vm = new HomeVM();
            vm.SliderItems = db.SliderItems.Where(d => d.Status == true).ToList();
            vm.FeaturedPrograms = db.Programs.Where(d => d.Status == true && d.Featured == true).OrderBy(d => d.DisplayOrder).ToList();
            vm.Events = db.Events.Where(d => d.Status == true).OrderBy(d => d.DisplayOrder).ToList();
            vm.RecentNews = db.RecentNews.Where(d => d.Status == true).OrderByDescending(d => d.RecentNewsDate).Take(4).ToList();
            return View(vm);
        }

        // POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult SubscribeUser(HomeVM model)
        //{
        //    string message = null;
        //    if (ModelState.IsValid)
        //    {
        //        var dbSubscribeUser = db.SubscribeUsers.FirstOrDefault(m => m.SubscribeUserEmail == model.SubscribeUser.SubscribeUserEmail);
        //        if (dbSubscribeUser != null)
        //        {
        //            message = "Email already Subscribed";
        //        }

        //        if (string.IsNullOrEmpty(message))
        //        {
        //            db.SubscribeUsers.Add(model.SubscribeUser);
        //            db.SaveChanges();
        //        }
        //    }

        //    return Json(message);
        //}

        //public ActionResult About()
        //{
        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    return View();
        //}

        //public FileResult DownloadPledgeForm()
        //{
        //    string rootpath = Server.MapPath("~/");
        //    string path = rootpath + "Content/files/Donation Form.pdf";
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
        //    var response = new FileContentResult(fileBytes, "application/octet-stream");
        //    response.FileDownloadName = "Donation Form.pdf";
        //    return response;
        //}
    }
}