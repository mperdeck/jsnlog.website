/// <reference path="TypeScriptDefinitions/jquery.d.ts" />
/// <reference path="TypeScriptDefinitions/js-cookie.d.ts" />
// This JavaScript supplies the tabbing behaviour for the language specific examples.
//
// Use code of this format:
//
// <div class="commontabs">
//   <div data-tab="Web.config">
//      ... example for web.config
//   </div>
//   <div data-tab="JsnlogConfiguration">
//      ... example for JsnlogConfiguration
//   </div>
// </div>
//
// This JS runs after the page has loaded and makes this html into tabbed examples, by adding this:
//
// <div class="commontabs">
//
//   <div class="commontabs-tabsgroup">
//     <a href='#' class="commontabs-tab active">Web.config</a>
//     <a href='#' class="commontabs-tab">JsnlogConfiguration</a>
//   </div>
//
//   <div data-tab="Web.config">
//      ... example for web.config
//   </div>
//   <div data-tab="JsnlogConfiguration">
//      ... example for JsnlogConfiguration
//   </div>
// </div>
//
// Also, when user clicks tab in one tab group, the tabs in all tab groups change, and when they go to another page
// or close and reopen the browser, that same tab is opened.
var commonTabs;
(function (commonTabs) {
    var eCommontabs;
    var eCommontabsChildren;
    var eCommontabsTabGroup;
    var eCommontabsTabs;
    var eBody;
    function tabsPresentOnPage() {
        return !!eCommontabsTabs;
    }
    function setCookie(tabCaption) {
        Cookies.set('commontabs_currentTabCaption', tabCaption, { expires: 365 });
    }
    // Show all tabs with the given caption
    function showTab(tabCaption) {
        if (!eCommontabsTabs)
            return;
        eCommontabsTabs.each(function (index, element) {
            var tabText = $(element).text();
            if (tabText == tabCaption) {
                $(element).show();
            }
        });
    }
    commonTabs.showTab = showTab;
    // Hide all tabs with the given caption
    function hideTab(tabCaption) {
        if (!eCommontabsTabs)
            return;
        eCommontabsTabs.each(function (index, element) {
            var tabText = $(element).text();
            if (tabText == tabCaption) {
                $(element).hide();
            }
        });
    }
    commonTabs.hideTab = hideTab;
    // Make the tab with the given tab active
    function setTab(tabCaption) {
        if (!eCommontabsTabs)
            return;
        // Deactivate all tabs
        eCommontabsChildren.hide();
        eCommontabsTabGroup.show();
        eCommontabsTabs.removeClass('active');
        // Activate boxes
        eCommontabsChildren.each(function (index, element) {
            var tabText = $(element).data('tab');
            if (tabText == tabCaption) {
                $(element).show();
            }
        });
        // Activate tabs
        eCommontabsTabs.each(function (index, element) {
            var tabText = $(element).text();
            if (tabText == tabCaption) {
                $(element).addClass('active');
            }
        });
        // Store current tab
        setCookie(tabCaption);
    }
    commonTabs.setTab = setTab;
    function setWebConfigTab() {
        setCookie('Web.config');
    }
    commonTabs.setWebConfigTab = setWebConfigTab;
    function setJsnlogConfigurationTab() {
        setCookie('JsnlogConfiguration');
    }
    commonTabs.setJsnlogConfigurationTab = setJsnlogConfigurationTab;
    function init() {
        var firstTab;
        eCommontabs = $(".commontabs");
        // Create the tab group and tabs, and insert at the top of the div with class commontabs.
        eCommontabs.each(function (index, element) {
            var tabGroup = $("<div class='commontabs-tabsgroup'></div>");
            $(element).children().each(function (index, element) {
                var tabCaption = $(element).data('tab');
                if (!firstTab) {
                    firstTab = tabCaption;
                }
                var tab = $("<a href='#' class='commontabs-tab'>" + tabCaption + "</a>");
                tab.click(onTabClick);
                tabGroup.append(tab);
            });
            $(element).prepend(tabGroup);
        });
        eCommontabsChildren = $('.commontabs > div');
        eCommontabsTabGroup = $('.commontabs > div.commontabs-tabsgroup');
        eCommontabsTabs = $('.commontabs > div.commontabs-tabsgroup > a');
        eBody = $('body');
        // Get current tab and make that visible everywhere
        var currentTabCaption = Cookies.get('commontabs_currentTabCaption');
        if ((!currentTabCaption) || (currentTabCaption === 'undefined')) {
            currentTabCaption = firstTab;
        }
        setTab(currentTabCaption);
    }
    commonTabs.init = init;
    function onTabClick(e) {
        var targetElement = e.target;
        var beforeTop = targetElement.getBoundingClientRect().top;
        var currentTabCaption = $(targetElement).text();
        setTab(currentTabCaption);
        var newTop = targetElement.getBoundingClientRect().top;
        var currrentScrollTop = eBody.scrollTop();
        var newScrollTop = currrentScrollTop + (newTop - beforeTop);
        eBody.scrollTop(newScrollTop);
        return false;
    }
    commonTabs.onTabClick = onTabClick;
})(commonTabs || (commonTabs = {}));
//# sourceMappingURL=commontabs.js.map