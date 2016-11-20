/*
function initMap(place, text) {
    var mapDiv = document.getElementById('map');
    var map = new google.maps.Map(mapDiv, {
        center: place,
        zoom: 18
    });

    var marker = new google.maps.Marker({
        position: place,
        title: text
    });

    marker.setMap(map);
}*/

$(document).ready(function () {

    $("input.argumento").on("keyup", function () {
        if ($(this).val()) {
            $("#agregar").addClass("call-to-action");
        } else {
            $("#agregar").removeClass("call-to-action");
        }
    });


    $("#agregar").on("click", function () {
        var ultimoArg = $(".argumento:not(.bloqueado)");
        ultimoArg.addClass("bloqueado");
        ultimoArg.attr("disabled", "disabled");
        var idUltimoArg = parseInt(ultimoArg.attr("name").split("-")[1]);
        $("#palabraClave").val($("#palabraClave").val() + ultimoArg.val() + ",");

        var proxArg = idUltimoArg + 1;
        ultimoArg.after("<span class='ion-ios-close-outline delete-arg' id='argumento-" + idUltimoArg + "'></span>");
        $("#argumentos").prepend("<input type='text' class='argumento' autofocus placeholder='¿Qué buscás?' name='argumento-" + proxArg + "' style='display:none'/><br>");
        $("input[name='argumento-" + proxArg + "']").slideDown(500);
    });

    $("#argumentos").on("click",".delete-arg", function () {
        var idUltimoArg = $(this).attr("id").split("-")[1];
        $(this).remove();
        $("input[name='argumento-" + idUltimoArg + "']").remove();
    });

    $(".poi").on("click", function () {
        $(".poi").not($(this)).children(".detalles").slideUp(500);
        $(".poi").not($(this)).removeClass("selected");
        $(this).children(".detalles").slideToggle(500);
        $(this).toggleClass("selected")
    });


});