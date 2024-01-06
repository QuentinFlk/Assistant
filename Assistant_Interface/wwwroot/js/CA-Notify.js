$(document).ready(function () {
    $.notify.addStyle('successAssistantNotification', {
        html: "<div>" +
            "<table class='table mb-0' style='height: 100px;'>" +
            "<tbody>" +
            "<tr>" +
            "<td class='border-0 py-0' style='vertical-align: middle;background-color: rgba(255,255,255,.3); border-right: 1px solid #fff !important; border-top-left-radius: 4px; border-bottom-left-radius: 4px;'>" +
            "<img src='../images/icones/check-circle-regular.svg' width='50' />" +
            "</td>" +
            "<td class='border-0 py-0' style='vertical-align: middle;padding-left: 2rem; padding-right: 2rem;'>" +
            "<span style='color: #fff;' data-notify-text/>" +
            "</td>" +
            "</tr>" +
            "</tbody>" +
            "</table>" +
            "</div>",
        classes: {
            base: {
                "white-space": "nowrap",
                "border-radius": "4px",
                "color": "#fff",
                "background-color": "#56b98a",
                "font-family": "'Roboto', sans-serif",
                "font-size": "16px",
                "min-height": "100px",
                "display": "flex",
                "align-items": "center",
                "justify-content": "center"
            }
        }
    });

    $.notify.addStyle('errorAssistantNotification', {
        html: "<div>" +
            "<table class='table mb-0' style='height: 100px;'>" +
            "<tbody>" +
            "<tr>" +
            "<td class='border-0 py-0' style='vertical-align: middle;background-color: rgba(255,255,255,.3); border-right: 1px solid #fff !important; border-top-left-radius: 4px; border-bottom-left-radius: 4px;'>" +
            "<img src='../images/icones/times-circle-regular.svg' width='50' />" +
            "</td>" +
            "<td class='border-0 py-0' style='vertical-align: middle;padding-left: 2rem; padding-right: 2rem;'>" +
            "<span style='color: #fff;' data-notify-text/>" +
            "</td>" +
            "</tr>" +
            "</tbody>" +
            "</table>" +
            "</div>",
        classes: {
            base: {
                "white-space": "nowrap",
                "border-radius": "4px",
                "color": "#fff",
                "background-color": "#ff6c6c",
                "font-family": "'Roboto', sans-serif",
                "font-size": "16px",
                "min-height": "100px",
                "display": "flex",
                "align-items": "center",
                "justify-content": "center"
            }
        }
    });

    $.notify.addStyle('waitAssistantNotification', {
        html: "<div>" +
            "<table class='table mb-0' style='height: 100px;'>" +
            "<tbody>" +
            "<tr>" +
            "<td class='border-0 py-0' style='vertical-align: middle;background-color: rgba(255,255,255,.3); border-right: 1px solid #fff !important; border-top-left-radius: 4px; border-bottom-left-radius: 4px;'>" +
            "<img src='../images/icones/clock-regular.svg' width='50' />" +
            "</td>" +
            "<td class='border-0 py-0' style='vertical-align: middle;padding-left: 2rem; padding-right: 2rem;'>" +
            "<span style='color: #fff;' data-notify-text/>" +
            "</td>" +
            "</tr>" +
            "</tbody>" +
            "</table>" +
            "</div>",
        classes: {
            base: {
                "white-space": "nowrap",
                "border-radius": "4px",
                "color": "#fff",
                "background-color": "#f2a051",
                "font-family": "'Roboto', sans-serif",
                "font-size": "16px",
                "min-height": "100px",
                "display": "flex",
                "align-items": "center",
                "justify-content": "center"
            }
        }
    });
});