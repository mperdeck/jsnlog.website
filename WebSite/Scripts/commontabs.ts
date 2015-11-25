/// <reference path="TypeScriptDefinitions/jquery.d.ts" />

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

module commonTabs {

    var eCommontabs: JQuery;
    var eCommontabsChildren: JQuery;
    var eCommontabsTabGroup: JQuery;
    var eCommontabsTabs: JQuery;

    function setTab(tabCaption: string): void {

        // Deactivate all tabs
        eCommontabsChildren.hide();
        eCommontabsTabGroup.show();
        eCommontabsTabs.removeClass('active');

        // Activate boxes

        eCommontabsChildren.each((index: number, element: Element) => {
            var tabText = $(element).data('tab');
            if (tabText == tabCaption) {
                $(element).show();
            }
        });

        // Activate tabs
        
        eCommontabsTabs.each((index: number, element: Element) => {
            var tabText = $(element).text();
            if (tabText == tabCaption) {
                $(element).addClass('active');
            }
        });

        // Store current tab

        (<any>localStorage).commontabs_currentTabCaption = tabCaption;
    }

    export function init() {

        var firstTab;
        eCommontabs = $(".commontabs");

        // Create the tab group and tabs, and insert at the top of the div with class commontabs.
        eCommontabs.each((index: number, element: Element) => {

            var tabGroup = $("<div class='commontabs-tabsgroup'></div>");
            $(element).children().each((index: number, element: Element) => {

                var tabCaption = $(element).data('tab');
                if (!firstTab) { firstTab = tabCaption; }

                var tab = $("<a href='#' class='commontabs-tab'>" + tabCaption + "</a>");
                tab.click(onTabClick);
                tabGroup.append(tab); 
            });
            
            $(element).prepend(tabGroup);
        });

        eCommontabsChildren = $('.commontabs > div');
        eCommontabsTabGroup = $('.commontabs > div.commontabs-tabsgroup');
        eCommontabsTabs = $('.commontabs > div.commontabs-tabsgroup > a');

        // Get current tab and make that visible everywhere
        var currentTabCaption = (<any>localStorage).commontabs_currentTabCaption || firstTab;
        setTab(currentTabCaption);
    }

    export function onTabClick(e: Event): boolean {
        var currentTabCaption = $(e.target).text();
        setTab(currentTabCaption);
        return false;
    }
}

$(function () {
    commonTabs.init();
});


