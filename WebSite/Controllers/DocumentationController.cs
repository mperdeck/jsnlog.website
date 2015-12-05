using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewExtensions;

namespace MainSite.Controllers
{
    public class DocumentationController : Controller
    {
        public ActionResult Index(string pathInfo)
        {
            string url = HttpContext.Request.Url.AbsolutePath.Trim(new char[] {'/'});

            // Make sure that old links in Google still work
            url = url.Replace("/GetStartedLogging/", "/HowTo/");
            url = url.Replace("/WebConfig", "/Configuration");

            // If page cannot be found, redirect to home page
            try
            {
                string view = Views.ByUrl(url).ViewPath;
                return View(view);
            }
            catch
            {
                return RedirectPermanent("/");
            }
        }

    }
}
