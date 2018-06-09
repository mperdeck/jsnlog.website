

module netSelector {

    var netCoreOnlyBlocks: JQuery;
    var netFrameworkOnlyBlocks: JQuery;
    var netSelector: JQuery;

    export function init() {
        netCoreOnlyBlocks = $('.net-core-only');
        netFrameworkOnlyBlocks = $('.net-framework-only');
        netSelector = $('.net-selector');

        if (netSelector.length == 0) {
            // If there is no net selector, return now
            return;
        }

        var needSelector: boolean = (netCoreOnlyBlocks.length > 0) || (netFrameworkOnlyBlocks.length > 0);

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
            Cookies.set('netselector_currentOption', this.value, { expires: 365 });
            showNetSpecificBlocks(this.value);
        });
    }

    function showNetSpecificBlocks(checkedOption: string): void {
        if (checkedOption == 'net-core') {
            netCoreOnlyBlocks.show();
            netFrameworkOnlyBlocks.hide();
        } else {
            netCoreOnlyBlocks.hide();
            netFrameworkOnlyBlocks.show();
        }
    }

    function setRadioOption(option: string): void {
        $("input[name=net-selector-radio][value=" + option +"]").prop("checked", true);
    }
}

$(function () {
    netSelector.init();
});
