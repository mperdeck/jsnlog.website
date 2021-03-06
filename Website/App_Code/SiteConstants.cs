﻿using System;
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

        public const string DemoAspNetCoreGithubUrl = "https://github.com/mperdeck/jsnlogSimpleWorkingDemos/tree/master/jsnlogSimpleWorkingDemos/JSNLogDemo_Core_NetCoreApp2";
        public const string DemoGithubUrl = "https://github.com/mperdeck/jsnlogSimpleWorkingDemos/tree/master/jsnlogSimpleWorkingDemos";
        public const string AngularJsDemoGithubUrl = "https://github.com/mperdeck/JSNLog.AngularJS";
        public const string Angular2CoreDemoGithubUrl = "https://github.com/mperdeck/jsnlog.AngularCoreDemo";
        public const string LicenceUrl = "https://github.com/mperdeck/jsnlog/blob/master/LICENSE.md";
        public const string LicenceName = "MIT";
        public const string CdnJsUrl = "https://cdnjs.com/libraries/jsnlog";
        public static string CdnJsDownloadUrl = "https://cdnjs.cloudflare.com/ajax/libs/jsnlog/" + Generated.Version + "/jsnlog.min.js";
        public static string CdnJsScriptTag = @"<script crossorigin=""anonymous"" src=""" + CdnJsDownloadUrl + @"""></script>";

        // This causes NuGet to search for all packages with "JSNLog" - so the user will see
        // JSNLog.NLog, etc. as well.
        public const string NugetDownloadUrl = "https://www.nuget.org/packages?q=jsnlog";

        public static string CurrentYear
        {
            get { return DateTime.Now.ToString("yyyy"); }
        }

        public static string DownloadLinkJsnlogJs 
        {
            get { return string.Format("https://raw.github.com/mperdeck/jsnlog.js/{0}/jsnlog.min.js", CurrentVersion); }
        }
    }
}

