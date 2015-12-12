using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.App_Code
{
    public static class SiteConstants
    {
        public static string CurrentVersion = Generated.Version;
        public const string JsnlogJsFileSize = "2kb";
        public const string HttpHeaderRequestIdName = "JSNLog-RequestId";
        public const string GlobalMethodCalledAfterJsnlogJsLoaded = "__jsnlog_configure";
        
        public const string HandlerExtension = ".logger";
        public const string DefaultDefaultAjaxUrl = "/jsnlog" + HandlerExtension;

        public const string DemoAspNet5GithubUrl = "https://github.com/mperdeck/jsnlog.AspNet5Demo";
        public const string DemoGithubUrl = "https://github.com/mperdeck/jsnlogSimpleWorkingDemos";
        public const string AngularJsDemoGithubUrl = "https://github.com/mperdeck/JSNLog.AngularJS";
        public const string LicenceUrl = "https://github.com/mperdeck/jsnlog/blob/master/LICENSE";
        public const string LicenceName = "MIT";
        public const string CdnJsUrl = "https://cdnjs.com/libraries/jsnlog";
        public const string CdnJsDownloadUrl = "https://cdnjs.cloudflare.com/ajax/libs/jsnlog/2.12.1/jsnlog.min.js";

        // This causes NuGet to search for all packages with "JSNLog" - so the user will see
        // JSNLog.NLog, etc. as well.
        public const string NugetDownloadUrl = "http://www.nuget.org/packages?q=jsnlog";

        public static string DownloadLinkJsnlogJs 
        {
            get { return string.Format("https://raw.github.com/mperdeck/jsnlog.js/{0}/jsnlog.min.js", CurrentVersion); }
        }
    }
}

