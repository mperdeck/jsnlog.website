// Depends on commonTabs
var netSelector;
(function (netSelector_1) {
    var netCoreOnlyBlocks;
    var netFrameworkOnlyBlocks;
    var netSelector;
    // framework: net-core or net-framework
    function setFrameworkCookie(framework) {
        Cookies.set('netselector_currentOption', framework, { expires: 365 });
    }
    netSelector_1.setFrameworkCookie = setFrameworkCookie;
    function init() {
        netCoreOnlyBlocks = $('.net-core-only');
        netFrameworkOnlyBlocks = $('.net-framework-only');
        netSelector = $('.net-selector');
        if (netSelector.length == 0) {
            // If there is no net selector, return now
            return;
        }
        var needSelector = ($('.commontabs').length > 0) ||
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
    netSelector_1.init = init;
    function showNetSpecificBlocks(checkedOption) {
        if (checkedOption == 'net-core') {
            netCoreOnlyBlocks.show();
            netFrameworkOnlyBlocks.hide();
            commonTabs.setTab('JsnlogConfiguration');
            commonTabs.hideTab('Web.config');
        }
        else {
            netCoreOnlyBlocks.hide();
            netFrameworkOnlyBlocks.show();
            commonTabs.showTab('Web.config');
        }
    }
    function setRadioOption(option) {
        $("input[name=net-selector-radio][value=" + option + "]").prop("checked", true);
    }
})(netSelector || (netSelector = {}));
$(function () {
    netSelector.init();
});
//# sourceMappingURL=netselector.js.map