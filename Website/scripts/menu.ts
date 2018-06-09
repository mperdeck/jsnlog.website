/// <reference path="TypeScriptDefinitions/jquery.d.ts" />

// This JavaScript supplies the dynamic behaviour of the left hand side menu.

module menu {

    export function init() {

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
}

$(function () {
    menu.init();
});


