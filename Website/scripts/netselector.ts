
// Depends on commonTabs

module netSelector {

    var netCoreOnlyBlocks: JQuery;
    var netFrameworkOnlyBlocks: JQuery;
    var netSelector: JQuery;

    // framework: net-core or net-framework
    export function setFrameworkCookie(framework: string) {
        Cookies.set('netselector_currentOption', framework, { expires: 365 });
    }

    export function init() {
        netCoreOnlyBlocks = $('.net-core-only');
        netFrameworkOnlyBlocks = $('.net-framework-only');
        netSelector = $('.net-selector');

        if (netSelector.length == 0) {
            // If there is no net selector, return now
            return;
        }

        var needSelector: boolean =
            ($('.commontabs').length > 0) ||
            (netCoreOnlyBlocks.length > 0) ||
            (netFrameworkOnlyBlocks.length > 0);

        if (!needSelector) {
            netSelector.hide();
            return;
        }

        netSelector.show();

        var currentOption = Cookies.get('netselector_currentOption');
        if (!currentOption) {
            currentOption = 'net-framework';
        }

        showNetSpecificBlocks(currentOption);

        setRadioOption(currentOption);

        $('input[type=radio][name=net-selector-radio]').change(function () {
            setFrameworkCookie(this.value);
            showNetSpecificBlocks(this.value);
        });
    }

    function showNetSpecificBlocks(checkedOption: string): void {
        if (checkedOption == 'net-core') {
            netCoreOnlyBlocks.show();
            netFrameworkOnlyBlocks.hide();

            commonTabs.setTab('JsnlogConfiguration');
            commonTabs.hideTab('Web.config');
        } else {
            netCoreOnlyBlocks.hide();
            netFrameworkOnlyBlocks.show();

            commonTabs.showTab('Web.config');
        }
    }

    function setRadioOption(option: string): void {
        $("input[name=net-selector-radio][value=" + option +"]").prop("checked", true);
    }
}

$(function () {
    netSelector.init();
});
