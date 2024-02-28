$(document).ready(function () {
    $('#nomAssistant').on('keyup',
        function () {
            var nom = $(this).val();
            var model = $('#choixModel').val();
            var instruction = $('#instructionAssistant').val();
            if (nom !== '' && model !== '' && instruction !== '')
                $('#ajout-assistant').prop('disabled', false);
            else
                $('#ajout-assistant').prop('disabled', true);
        });

    $('#choixModel').on('change',
        function () {
            var nom = $('#nomAssistant').val();
            var model = $(this).val();
            var instruction = $('#instructionAssistant').val();
            if (nom !== '' && model !== '' && instruction !== '')
                $('#ajout-assistant').prop('disabled', false);
            else
                $('#ajout-assistant').prop('disabled', true);
        });

    $('#instructionAssistant').on('keyup',
        function () {
            var nom = $('#nomAssistant').val();
            var model = $('#choixModel').val();
            var instruction = $(this).val();
            if (nom !== '' && model !== '' && instruction !== '')
                $('#ajout-assistant').prop('disabled', false);
            else
                $('#ajout-assistant').prop('disabled', true);
        });

    $('#tag-assistant').on('click',
        function () {
            $(this).prop('disabled', true);
            GetTagForNewAssistant();
        });

    $('#ajout-assistant').on('click',
        function () {
            $(this).prop('disabled', true);
            AjoutAssistant();
        });
});

function GetTagForNewAssistant() {
    var prompt = $('#instructionAssistant').val();
    $.ajax({
        url: "/AjoutAssistant/GetTagForNewAssistant",
        type: "GET",
        data: "prompt=" + prompt,
        success: function (reponse) {
            $('#tag-assistant').prop('disabled', false);
            $('#tagAssistant').val(reponse);
        },
        error: function (error) {
            $('#tag-assistant').prop('disabled', false);
            $.notify(error.responseJSON,
                {
                    style: 'errorAssistantNotification',
                    autoHideDelay: 5000,
                    globalPosition: 'bottom right'
                });
        }
    });
}

function AjoutAssistant() {
    var data = new Object();
    data["NomAssistant"] = $('#nomAssistant').val();
    data["InstructionAssistant"] = $('#instructionAssistant').val();
    data["DescriptionAssistant"] = $('#descriptionAssistant').val();
    data["IdCreateurAssistant"] = $('#idCreateurAssistant').val();
    data["ChoixModel"] = $('#choixModel').val();
    data["IsCodeInterpreterEnable"] = $('#isCodeInterpreterEnable').is(':checked');

    var array = new Array();
    $('#tagAssistant').val().split(', ').forEach(function (tag) {
        array.push(tag);
    });

    data["LisTagAssistant"] = array;

    var jsonData = JSON.stringify(data);

    $.ajax({
        url: "/AjoutAssistant/AjoutAssistant",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: jsonData,
        success: function (reponse) {
            var message = reponse.split('_')[0];
            var idAssistant = reponse.split('_')[1];
            $.notify(message,
                {
                    style: 'successAssistantNotification',
                    autoHideDelay: 4000,
                    globalPosition: 'bottom right'
                });

            setTimeout(function () {
                location.replace(location.origin + "/DetailAssistant/Index?idAssistant=" + idAssistant);
            }, 3000);
        },
        error: function (error) {
            $('#ajout-assistant').prop('disabled', false);
            $.notify(error.responseJSON,
                {
                    style: 'errorAssistantNotification',
                    autoHideDelay: 5000,
                    globalPosition: 'bottom right'
                });
        }
    });
}