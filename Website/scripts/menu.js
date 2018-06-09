/// <reference path="TypeScriptDefinitions/jquery.d.ts" />
// This JavaScript supplies the dynamic behaviour of the left hand side menu.
var menu;
(function (menu) {
    function init() {
        // Scroll the selected item into view, if the screen is wide enough to show
        // menu and main content side by side.
        var windowWidth = window.innerWidth;
        if (windowWidth > 768) {
            var selectedElements = document.getElementsByClassName('selected');
            if (selectedElements.length > 0) {
                selectedElements[0].scrollIntoView(true);
            }
        }
    }
    menu.init = init;
})(menu || (menu = {}));
$(function () {
    menu.init();
});
//# sourceMappingURL=menu.js.map