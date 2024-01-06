$(document).ready(function () {

    //Notifications SUCCESS
    if ($("#successAlert").length > 0) {
        $.notify($("#successAlert").text(),
            {
                style: 'successAssistantNotification',
                autoHideDelay: 4000,
                globalPosition: 'bottom right'
            });
    }

    //Notifications ERROR
    if ($("#errorAlert").length > 0) {
        $.notify($("#errorAlert").text(),
            {
                style: 'errorAssistantNotification',
                //autoHideDelay: 4000,
                globalPosition: 'bottom right'
            });
    }

    //Gestion popover d'informations
    //Définition du template du Popover
    var popoverTemplate = ['<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header"></h3><div class="popover-body"></div></div>'].join('');
    //Définition du contenu du template du Popover
    var content = [' <div class="info-popover-title my-3"> <h5> Champs servant à la recherche: </h5> </div>',
        '<hr>',
        '<div id="popover-structure-part" class="mb-3 popover-part">',
        '<h5>Pour la recherche d\'une structure</h5>',
        '<ul>',
        '<li>N° Structure</li>',
        '<li>Nom de la structure</li>',
        '<li>Nom du Commercial ou de l\'enseigne</li>',
        '<li>Tous les champs de l\'adresse</li>',
        '<li>Email</li>',
        '<li>Numéros de téléphone</li>',
        '<li>N° CVI</li>',
        '<li>N° SIRET</li>',
        '<li>N° d\'Accise</li>',
        '<li>Mémo</li>',
        '<li>N° compte client CVO</li>',
        '<li>N° compte client matériel promotionnel</li>',
        '<li>N° compte fournisseur</li>',
        '</ul>',
        '</div>',
        '<div class="mb-3 popover-part" id="popover-contact-part">',
        '<h5>Pour la recherche d\'un contact</h5>',
        '<ul>',
        '<li>Identifiant du contact</li>',
        /*'<li>N° des structures liées</li>',*/
        '<li>Nom du contact</li>',
        '<li>Prénom du contact</li>',
/*        '<li>Fonction</li>',*/
        '<li>Email</li>',
        '<li>Numéros de téléphone</li>',
        '<li>Tous les champs de l\'adresse</li>',
        '</ul>',
        '</div>',
        '<div id="popover-user-part" class="mb-3 popover-part">',
        '<h5>Pour la recherche d\'un utilisateur</h5>',
        '<ul>',
        '<li>Nom de l\'utilisateur</li>',
        '<li>Prénom de l\'utilisateur</li>',
        '<li>Login</li>',
        '<li>Email</li>',
        '</ul>',
        '</div>',
    ].join('');

    //Trigger de la popover
    $('#AideRecherche .type-research-info').popover({
        content: content,
        container: "body",
        template: popoverTemplate,
        placement: "right",
        trigger: "hover",
        html: true
    });
});