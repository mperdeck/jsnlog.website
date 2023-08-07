using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

// ----------------------------------------------------
// To test sub domains, create domains in 
// C:\WINDOWS\system32\drivers\etc\hosts
// ----------------------------------------------------

namespace ViewExtensions
{
    public static class PageVersions
    {
        private static IEnumerable<VersionInfo> _versionInfos = null;
        private static bool _useCookies = false;

        public class VersionInfo
        {
            public string VersionUrlName { get; set; } // Used in url

            // Overrides VersionUrlName. If this is not null, the link in a version switcher always has this url.
            // If you're on a page with this url and use the version switcher, this version will be shown as the current version.
            public string VersionUrlOverride { get; set; } 
            
            public string VersionName { get; set; } // Used in C# code
            public string SubVersionName { get; set; } // Used in C# code
            public string Caption { get; set; } // Used in version switcher
            public bool IsDefault { get; set; }
            public int ButtonWidth { get; set; } = 100;
        }

        private const string VersionUrlParam = "version";
        private const string CookieName = "version";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="versionInfos"></param>
        /// <param name="useCookies">
        /// If true, the current version choice is stored in a cookie.
        /// When no version choice can be determined, the contents of the cookie is used.
        /// 
        /// If false, no cookies are used.
        /// </param>
        /// <param name="useSubDomain">
        /// If true, loading a new version means loading a sub domain.
        /// And the current version is determined from the currrent sub domain (if there is one).
        /// 
        /// If false, a query string param is used.
        /// </param>
        public static void Load(IEnumerable<VersionInfo> versionInfos, bool useCookies)
        {
            _versionInfos = versionInfos;
            _useCookies = useCookies;
        }

        public static void GetPageVersion(out bool isNetCore, out bool isNetFramework)
        {
            var currentVersion = CurrentVersionInfo();
            isNetCore = (currentVersion == ViewExtensionsConfig.NetCoreJs);
            isNetFramework = (currentVersion == ViewExtensionsConfig.NetFrameworkJs);
        }

        public static void GetPageVersion(out bool isNetCore, out bool isNetFramework, out bool isJs)
        {
            var currentVersion = CurrentVersionInfo();
            isNetCore = (currentVersion == ViewExtensionsConfig.NetCoreJs);
            isNetFramework = (currentVersion == ViewExtensionsConfig.NetFrameworkJs);
            isJs = (!isNetCore) && (!isNetFramework);
        }

        /// <summary>
        /// Returns the name of the currently selected version.
        /// </summary>
        /// <returns></returns>
        public static string CurrentVersion()
        {
            return CurrentVersionInfo()?.VersionName;
        }

        public static string CurrentSubVersion()
        {
            return CurrentVersionInfo()?.SubVersionName;
        }

        public static VersionInfo CurrentVersionInfo()
        {
            if (_versionInfos == null)
            {
                return null;
            }

            // First try get version from url

            Uri url = HttpContext.Current.Request.Url;
            VersionInfo versionInfo = GetCurrentVersionInfoFromUrl(url);

            if (versionInfo != null)
            {
                if (_useCookies)
                {
                    // Set cookie, so when other pages are opened user gets same version
                    HttpContext.Current.Response.Cookies[CookieName].Value = versionInfo.VersionUrlName;
                    HttpContext.Current.Response.Cookies[CookieName].Expires = DateTime.Now.AddYears(1);
                }

                return versionInfo;
            }

            if (_useCookies)
            {
                // Then try cookie

                string versionUrlName = HttpContext.Current.Request.Cookies[CookieName]?.Value;
                if (!String.IsNullOrEmpty(versionUrlName))
                {
                    versionInfo = _versionInfos.FirstOrDefault(v => v.VersionUrlName == versionUrlName);
                }
            }

            // If no cookie, use default

            if (versionInfo == null)
            {
                versionInfo = _versionInfos.Single(v => v.IsDefault);
            }

            return versionInfo;
        }

        public static MvcHtmlString VersionSwitcher(this HtmlHelper htmlHelper)
        {
            if (_versionInfos == null)
            {
                return null;
            }

            var currentVersionInfo = CurrentVersionInfo();
            var sb = new StringBuilder();

            foreach (var versionInfo in _versionInfos)
            {
                if (versionInfo == currentVersionInfo)
                {
                    // Class is used for bootstrap styling
                    sb.AppendFormat(@"<span style=""width: {1}px"" class=""btn btn-primary"">{0}</span>", versionInfo.Caption, versionInfo.ButtonWidth);
                }
                else if (versionInfo.VersionUrlOverride != null)
                {
                    // Class is used for bootstrap styling
                    sb.AppendFormat(
                        @"<a style=""width: {2}px"" class=""btn btn-default"" href=""{0}"">{1}</a>", versionInfo.VersionUrlOverride, versionInfo.Caption, versionInfo.ButtonWidth);
                }
                else
                {
                    // Class is used for bootstrap styling
                    sb.AppendFormat(
                        @"<a style=""width: {2}px"" class=""btn btn-default"" href=""{0}"">{1}</a>", UrlWithVersionUrlName(versionInfo.VersionUrlName), versionInfo.Caption, versionInfo.ButtonWidth);
                }
            }

            return new MvcHtmlString(sb.ToString());
        }

        private static VersionInfo GetCurrentVersionInfoFromUrl(Uri url)
        {
            string versionUrlName = null;

            // Check versions that have a url override

            string currentUrl = url.ToString();
            string currentUrlWithoutWww = currentUrl.Replace("http://www.", "http://").Replace("https://www.", "https://");
            VersionInfo versionInfo = _versionInfos.SingleOrDefault(v => (v.VersionUrlOverride == currentUrlWithoutWww));

            if (versionInfo != null)
            {
                return versionInfo;
            }

            // No direct match with url override found. Try to use version url name.

            versionUrlName = (string)HttpContext.Current.Request.QueryString[VersionUrlParam];

            if (versionUrlName == null)
            {
                versionUrlName = RequestSubdomain(url);
            }

            if (versionUrlName == null)
            {
                return null;
            }

            versionInfo = _versionInfos.SingleOrDefault(v => (v.VersionUrlName == versionUrlName));
            return versionInfo;
        }

        public static string UrlWithVersionUrlName(string versionUrlName)
        {
            string url = string.Format("?{0}={1}", VersionUrlParam, versionUrlName);
            return url;
        }

        /// <summary>
        /// Returns the sub domain in the passed in URL.
        /// 
        /// Returns null if there is no sub domain.
        /// 
        /// Assumes that the domain is not country specific. 
        /// For example, jsnlog.com but not jsnlog.com.au.
        /// 
        /// Note that the domain could be localhost, in which case you can
        /// have js.localhost, etc.
        /// </summary>
        /// <returns></returns>
        private static string RequestSubdomain(Uri url)
        {
            string[] uriParts = url.Host.Split(new[] { '.' });

            // If there are only 2 or 1 parts in the host name, you can't have a sub domain.
            // However, if the last part is localhost, you can have 2 parts.

            int nbrParts = uriParts.Length;
            int minimumNbrParts = (uriParts[nbrParts - 1].ToLower() == "localhost") ? 2 : 3;

            if (uriParts.Length < minimumNbrParts)
            {
                return null;
            }

            // Assume that the first part is the sub domain. For example,
            // js.jsnlog.com

            return uriParts[0];
        }
    }
}

