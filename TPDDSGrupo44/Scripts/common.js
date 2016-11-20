$(document).ready(function () {
    recalcularAlturas();
});

$(window).resize(function () {
    recalcularAlturas();
});

function recalcularAlturas () {
    var headerHeight = $("header").outerHeight();
    var mainHeight = $("main").outerHeight();
    var footerHeight = $("footer").outerHeight();

    var windowHeight = $(window).height();

    if ((headerHeight + mainHeight+ footerHeight) < windowHeight) {
        var difference = windowHeight - (headerHeight + mainHeight + footerHeight);

        $("main").css("min-height", ($("main").height() + difference) +"px" );

    }
}