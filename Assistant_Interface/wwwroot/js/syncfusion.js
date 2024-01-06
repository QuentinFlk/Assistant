$(document).ready(function () {
    LoadTraduction();

    // ~~ On a besoin de savoir si le tableau fait plus de 800px de haut pour définir un scroll avec un sticky header, sinon les filtres excel risquent de ne pas être beau
    // ~~ On vérifie au chargement de la page
    //if ($('.tableau-syncfusion-cob').is(":visible")) {
    //    if ($('.tableau-syncfusion-cob').height() >= 800) {
    //        $('.tableau-syncfusion-cob').css({ 'max-height': '800px', 'overflow-y': 'auto' })
    //    } else {
    //        $('.tableau-syncfusion-cob').css({ 'max-height': 'none', 'overflow-y': 'initial' })
    //    }
    //} else {
    //    /*alert("Le tableau n'est pas affiché");*/
    //}
    // ~~ On vérifie au changement d'onglet
    //$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    //    var tab = $(e.target);
    //    var contentId = tab.attr("href");
    //    if ($(contentId).find('.tableau-syncfusion-cob').is(":visible")) {
    //        if ($(contentId).find('.tableau-syncfusion-cob').height() >= 800) {
    //            $(contentId).find('.tableau-syncfusion-cob').css({ 'max-height': '800px', 'overflow-y': 'auto' })
    //        } else {
    //            $(contentId).find('.tableau-syncfusion-cob').css({ 'max-height': 'none', 'overflow-y': 'initial' })
    //        }
    //        console.log($(contentId).find('.tableau-syncfusion-cob').height());
    //    } else {
    //        /*alert("Le tableau n'est pas affiché");*/
    //    }
    //});
    // ~~ Fin

    $.fn.isInViewport = function () {
        var elementTop = $(this).offset().top;
        var elementBottom = elementTop + $(this).outerHeight();

        var viewportTop = $(window).scrollTop();
        var viewportBottom = viewportTop + $(window).height();

        return elementBottom > viewportTop && elementTop < viewportBottom;
    };

});

function onGridComplete1(args) {
    /*console.log('Aloa : ' + $('.' + this.element.classList[0]).height());*/
    console.log('Aloa');
}

function onFocusDatepicker(args) {
    var datepickerObject = document.getElementById("dateDebActivite").ej2_instances[0];
    $('.' + args.model.element.classList[1]).parent().parent().find('label').addClass('label-datepicker-focused');
}

function onFocusOutDatepicker(args) {
    var datepickerObject = document.getElementById("dateDebActivite").ej2_instances[0];
    if (document.getElementById("dateDebActivite").ej2_instances[0].value == null) {
        $('.' + args.model.element.classList[1]).parent().parent().find('label').removeClass('label-datepicker-focused');
    }
}

function LoadTraduction() {
    ej.base.L10n.load({
        "fr-FR": {
            "grid": {
                "EmptyRecord": "Aucun enregistrement à afficher",
                "True": "vrai",
                "False": "faux",
                "InvalidFilterMessage": "Données de filtre non valides",
                "GroupDropArea": "Faites glisser un en-tête de colonne ici pour regrouper sa colonne",
                "UnGroup": "Cliquez ici pour dissocier",
                "GroupDisable": "Le regroupement est désactivé pour cette colonne",
                "FilterbarTitle": "Cellule de barre de filtre \"s",
                "EmptyDataSourceError": "DataSource ne doit pas être vide lors du chargement initial car les colonnes sont générées à partir de dataSource dans AutoGenerate Column Grid",
                "Add": "Ajouter",
                "Edit": "Éditer",
                "Cancel": "Annuler",
                "Update": "Mise à jour",
                "Delete": "Supprimer",
                "Print": "Impression",
                "Pdfexport": "Exportation PDF",
                "Excelexport": "Exportation Excel",
                "Wordexport": "Exportation de mots",
                "Csvexport": "Exportation CSV",
                "Search": "Chercher",
                "Columnchooser": "Colonnes",
                "Save": "sauvegarder",
                "Item": "article",
                "Items": "articles",
                "EditOperationAlert": "Aucun enregistrement sélectionné pour l'opération d'édition",
                "DeleteOperationAlert": "Aucun enregistrement sélectionné pour l'opération de suppression",
                "SaveButton": "sauvegarder",
                "OKButton": "Appliquer",
                "CancelButton": "Annuler",
                "EditFormTitle": "Les détails de",
                "AddFormTitle": "Ajouter un nouvel enregistrement",
                "BatchSaveConfirm": "Voulez-vous vraiment enregistrer les modifications ?",
                "BatchSaveLostChanges": "Les modifications non enregistrées seront perdues. Es-tu sur de vouloir continuer ?",
                "ConfirmDelete": "Voulez-vous vraiment supprimer l'enregistrement ?",
                "CancelEdit": "Voulez-vous vraiment annuler les modifications ?",
                "ChooseColumns": "Choisissez la colonne",
                "SearchColumns": "colonnes de recherche",
                "Matchs": "Aucun résultat",
                "FilterButton": "Filtre",
                "ClearButton": "Clair",
                "StartsWith": "Commence avec",
                "EndsWith": "Se termine par",
                "Contains": "Contient",
                "Equal": "Égal à",
                "NotEqual": "Différent de",
                "LessThan": "Moins que",
                'SortAtoZ': 'Trier par nom croissant',
                'SortZtoA': 'Trier par nom décroissant',
                'SortByOldest': 'Trier par le plus ancien',
                'SortByNewest': 'Trier par le plus récent',
                'SortSmallestToLargest': 'Trier par ordre croissant',
                'SortLargestToSmallest': 'Trier par ordre décroissant',
                "LessThanOrEqual": "Inférieur ou égal",
                "GreaterThan": "Plus grand que",
                "GreaterThanOrEqual": "Meilleur que ou égal",
                "ChooseDate": "Choisissez une date",
                "EnterValue": "Entrez la valeur",
                "Copy": "Copie",
                "Group": "Regrouper par cette colonne",
                "Ungroup": "Dissocier par cette colonne",
                "autoFitAll": "Ajuster automatiquement toutes les colonnes",
                "autoFit": "Ajuster automatiquement cette colonne",
                "Export": "Exportation",
                "FirstPage": "Première page",
                "LastPage": "Dernière page",
                "PreviousPage": "Page précédente",
                "NextPage": "Page suivante",
                "SortAscending": "Trier par ordre croissant",
                "SortDescending": "Trier par ordre décroissant",
                "EditRecord": "Modifier l'enregistrement",
                "DeleteRecord": "Supprimer l'enregistrement",
                "FilterMenu": "Filtre",
                "SelectAll": "Tout sélectionner",
                "Blanks": "Blancs",
                "FilterTrue": "Vrai",
                "FilterFalse": "Faux",
                "NoResult": "Aucun résultat",
                "ClearFilter": "Effacer le filtre",
                "NumberFilter": "Filtres numériques",
                "TextFilter": "Filtres de texte",
                "DateFilter": "Filtres de date",
                "DateTimeFilter": "Filtres DateTime",
                "MatchCase": "Cas de correspondance",
                "Between": "Entre",
                "CustomFilter": "Filtre personnalisé",
                "CustomFilterPlaceHolder": "Entrez la valeur",
                "CustomFilterDatePlaceHolder": "Choisissez une date",
                "AND": "ET",
                "OR": "OU",
                "ShowRowsWhere": "Afficher les lignes où :"
            },
            "pager": {
                "currentPageInfo": "{0} sur {1} pages",
                "totalItemsInfo": "({0} éléments)",
                "firstPageTooltip": "Aller à la première page",
                "lastPageTooltip": "Aller à la dernière page",
                "nextPageTooltip": "Aller à la page suivante",
                "previousPageTooltip": "Aller à la page précédente",
                "nextPagerTooltip": "Aller au téléavertisseur suivant",
                "previousPagerTooltip": "Aller au téléavertisseur précédent",
                "pagerDropDown": "objets par page",
                "pagerAllDropDown": "Articles",
                "All": "Tout",
                "totalItemInfo": "({0} élément(s))"
            },
            //"schedule": {
            //    "day": "Nap",
            //    "week": "Hét",
            //    "workWeek": "Munkahét",
            //    "month": "Hónap"
            //},
        }
    });
}