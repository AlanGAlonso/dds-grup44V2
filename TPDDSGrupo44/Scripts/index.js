﻿function initMap(place, text) {
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
}

$(document).ready(function () {

    $("[type='submit']").prop("disabled", true);

    $("input.argumento").on("keyup", function () {
        if ($(this).val()) {
            $("#agregar").addClass("call-to-action");
        } else {
            $("#agregar").removeClass("call-to-action");
        }
    });


    $("#agregar").on("click", function () {
        var ultimoArg = $(".argumento:not(.bloqueado)");

        if (ultimoArg.val() != "") {
            ultimoArg.addClass("bloqueado");
            ultimoArg.attr("disabled", "disabled");
            var idUltimoArg = parseInt(ultimoArg.attr("name").split("-")[1]);
            $("#palabraClave").val($("#palabraClave").val() + ultimoArg.val() + ",");
            ultimoArg.after("<span class='ion-ios-close-outline delete-arg' id='argumento-" + idUltimoArg + "'></span>");

            var proxArg = idUltimoArg + 1;
        
            $("#argumentos").prepend("<input type='text' class='argumento' autofocus placeholder='¿Qué buscás?' name='argumento-" + proxArg + "' style='display:none'/><br>");
            $("input[name='argumento-" + proxArg + "']").slideDown(500);
        } else {
            alert("Por favor, ingresá un argumento de búsqueda.")
        }

        if ($(".bloqueado").length > 0) {
            $("[type='submit']").prop("disabled", false);
            $("[type='submit']").addClass("call-to-action");
        } else {
            $("[type='submit']").prop("disabled", true);
            $("[type='submit']").removeClass("call-to-action");
        }
    });

    $("#argumentos").on("click",".delete-arg", function () {
        var idUltimoArg = $(this).attr("id").split("-")[1];
        $(this).remove();
        $("input[name='argumento-" + idUltimoArg + "'], input[name='argumento-" + idUltimoArg + "']+br").remove();

        if ($(".bloqueado").length > 0) {
            $("[type='submit']").prop("disabled", false);
            $("[type='submit']").addClass("call-to-action");
        } else {
            $("[type='submit']").prop("disabled", true);
            $("[type='submit']").removeClass("call-to-action");
        }
    });

    $(".poi").on("click", function () {
        $(".poi").not($(this)).children(".detalles").slideUp(500);
        $(".poi").not($(this)).removeClass("selected");
        $(this).children(".detalles").slideToggle(500);
        $(this).toggleClass("selected")

        if ($(this).hasClass("selected")) {
            var text = $(this).children("h3").text();
            var place = {
                lat: parseFloat($(this).find(".latitude").text()),
                lng: parseFloat($(this).find(".longitude").text())
            };
            
            initMap(place, text);
        }

    });


});