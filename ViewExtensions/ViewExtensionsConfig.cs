using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ViewExtensions;

namespace ViewExtensions
{
    public class ViewExtensionsConfig
    {
        public static PageVersions.VersionInfo NetCoreJs = new PageVersions.VersionInfo { VersionUrlName = "netjs", VersionName = "NetJs", SubVersionName = "NetCoreJs", Caption = ".Net Core + JS", IsDefault = true };
        public static PageVersions.VersionInfo NetFrameworkJs = new PageVersions.VersionInfo { VersionUrlName = "netframeworkjs", VersionName = "NetJs", SubVersionName = "NetFrameworkJs", Caption = ".Net Framework + JS", ButtonWidth = 130 };
        public static PageVersions.VersionInfo NodeJs = new PageVersions.VersionInfo { VersionUrlName = "nodejs", VersionName = "NodeJs", Caption = "Node + JS" };
        public static PageVersions.VersionInfo JsOnly = new PageVersions.VersionInfo { VersionUrlName = "js", VersionName = "JsOnly", Caption = "JS Only" };

        public static void RegisterViews()
        {
            Views.Load("/Views/Documentation");
        }

        public static void RegisterPageVersions()
        {
            var versionInfos = new[] 
            {
                NetCoreJs,
                NetFrameworkJs,
                NodeJs,
                JsOnly
            };

            PageVersions.Load(versionInfos, true);
        }
    }
}