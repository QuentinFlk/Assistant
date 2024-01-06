$(document).ready(function () {

    /* ~~ Gestion du logo du type de plateforme ~~ */
    if ($('.logo_type_plateforme').length) {
        var logo_type_plateforme = {
            init: function (el, str) {
                var element = document.querySelector(el);
                var text = str ? str : element.innerHTML;
                element.innerHTML = '';
                for (var i = 0; i < text.length; i++) {
                    var letter = text[i];
                    var span = document.createElement('span');
                    var node = document.createTextNode(letter);
                    var r = (360 / text.length) * (i);
                    var x = (Math.PI / text.length).toFixed(0) * (i);
                    var y = (Math.PI / text.length).toFixed(0) * (i);
                    span.appendChild(node);
                    span.style.webkitTransform = 'rotateZ(' + r + 'deg) translate3d(' + x + 'px,' + y + 'px,0)';
                    span.style.transform = 'rotateZ(' + r + 'deg) translate3d(' + x + 'px,' + y + 'px,0)';
                    element.appendChild(span);
                }
            }
        };
        logo_type_plateforme.init('.logo_type_plateforme');
        $('.logo_type_plateforme').prepend('<span id="external_circle"></span><span id="internal_circle"></span>');
    }
    /* ~~ Fin de gestion du logo du type de plateforme ~~ */

    $(document).on('click', '#show_hide_change_commune', function () {
        $('#change_commune').toggle();
    });

    // ~~~~ Ajouts class dans le body si container de la page contient la classe "body-container"
    $('.container-fluid.body-container').closest('body').addClass('body-interne');

    // ~~~~ Ajouts class pour sidebar Admin
    $('#accordionAdministration .collapse').removeAttr("data-parent");
    $('#accordionAdministration .collapse').collapse('show');
    $('#arrow-open-sidebar-admin, #cross-open-sidebar-admin').on("click",
        function () {
            $("body").toggleClass("active-admin-sidebar");
        });

    // ~~~~ Au click sur l'icône d'ouverture de sidebar
    $('.openLateralBar').on('click',
        function (e) {
            e.preventDefault();
            // On affiche l'icône de cloture
            $('.closeLateralBar').show();
            // On masque l'icône d'ouverture
            $(this).hide();
            $('.col-10-accueil').toggleClass('col-10', 50, "easeOutSine");
            $('.col-10-accueil').toggleClass('col-12', 50, "easeOutSine");
            $('.col-2-accueil').toggleClass('col-2', 50, "easeOutSine");
            $('.col-2-accueil').toggle();
        });
    // ~~~~ Au click sur l'icône de fermeture de sidebar
    $('.closeLateralBar').on('click',
        function (e) {
            e.preventDefault();
            // On masque l'icône de fermeture
            $(this).hide();
            $('.openLateralBar').show();
            $('.col-10-accueil').toggleClass('col-10', 50, "easeOutSine");
            $('.col-10-accueil').toggleClass('col-12', 50, "easeOutSine");
            $('.col-2-accueil').toggleClass('col-2', 50, "easeOutSine");
            $('.col-2-accueil').toggle();
        });
    // ~~~~ Fin class pour sidebar Admin
})