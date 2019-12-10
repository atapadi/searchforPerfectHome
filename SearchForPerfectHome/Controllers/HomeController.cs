using Microsoft.AspNet.Identity.Owin;
using SearchForPerfectHome.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SearchPerfectHome.ViewModels;

namespace SearchForPerfectHome.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string user1 = User.Identity.GetSecurityUserId();
            var imgPaths = new List<string>();
            var filepaths = Directory.GetFiles(Server.MapPath("~/images/Carousal/"), "*.jpg",SearchOption.TopDirectoryOnly);
            string imgPathSuffix = @"images/Carousal/";
            foreach (var filepath in filepaths)
            {
                var filename = Path.GetFileName(filepath);
                var imgPath = imgPathSuffix + filename;
                imgPaths.Add(imgPath);
            }

            var model = new HomeViewModel
            {
                ImgUrls = imgPaths
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}