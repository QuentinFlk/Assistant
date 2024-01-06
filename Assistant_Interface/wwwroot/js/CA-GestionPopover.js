$(document).ready(function () {
    $(window).on("resize", function (e) {
        checkScreenSizePopover();
    });
    checkScreenSizePopover();
});

//~~~~ Fonctions jQuery
function checkScreenSizePopover() {

    var newWindowWidth = $(window).width();
    if (newWindowWidth > 992) {

        /* ~~ Popover Service utilisateur ~~ */
        $('.popover-isDataBaseExiste').each(function () {
            var idisComplexeApplication = $(this).attr('id').split('_')[1];
            if ($("#popover-content-isDataBaseExiste_" + idisComplexeApplication).length > 0) {
                $('#popover-isDataBaseExiste_' + idisComplexeApplication).popover({
                    html: true,
                    trigger: 'hover',
                    container: "body",
                    placement: 'right',
                    content: function () {
                        return $("#popover-content-isDataBaseExiste_" + idisComplexeApplication).html();
                    }
                });
            }
        });

    } else {

    }
}