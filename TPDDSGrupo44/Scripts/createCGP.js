﻿$(document).ready(function () {
    var coordenada_Latitude, coordenada_Longitude, calle, numeroAltura, nombreDePOI, numeroDeComuna, zonaDelimitadaPorLaComuna, palabrasClave;

    $("input[type='submit']").prop("disabled", true);

    // validacion coordenada_Latitude
    $("#coordenada_Latitude").on("blur", function () {
        if ($(this).val().length > 0) {
            coordenada_Latitude = true;
        } else {
            coordenada_Latitude = false;
            alert("Por favor incluya la Latitud");
        }
        validar();
    });


    //validación coordenada_Longitude
    $("#coordenada_Longitude").on("blur", function () {
        if (coordenada_Latitude) {
            if ($(this).val().length > 0) {
                coordenada_Longitude = true;
            } else {
                coordenada_Longitude = false;
                alert("Por favor incluya la Longitud");
            }
        }
        validar();
    });

    $("#calle").on("blur", function () {
        //validación calle
        if (coordenada_Longitude) {
            if ($(this).val().length > 0) {
                calle = true;
            } else {
                calle = false;
                alert("Por favor incluya la calle");
            }
        }
        validar();
    });

    //validación numeroAltura
    $("#numeroAltura").on("blur", function () {
        if (calle) {
            if ($(this).val().length > 0 && parseInt($(this).val()) > 0) {
                numeroAltura = true;
            } else {
                numeroAltura = false;
                alert("Por favor incluya el numeroAltura");
            }
        }
        validar();
    });


    //validación nombreDePOI
    $("#nombreDePOI").on("blur", function () {
        if (numeroAltura) {
            if ($(this).val().length > 0) {
                nombreDePOI = true;
            } else {
                nombreDePOI = false;
                alert("Por favor incluya el Nombre del POI");
            }
        }
        validar();
    });

    //validación numeroDeComuna
    $("#numeroDeComuna").on("blur", function () {
        if (nombreDePOI) {
            if ($(this).val().length > 0 && parseInt($(this).val()) > 0) {
                numeroDeComuna = true;
            } else {
                numeroDeComuna = false;
                alert("Por favor ingrese el numero de comuna");
            }
        }
        validar();
    });

    //validación zonaDelimitadaPorLaComuna
    $("#zonaDelimitadaPorLaComuna").on("blur", function () {
        if (numeroDeComuna) {
            if ($(this).val().length > 0 && parseInt($(this).val()) > 0) {
                zonaDelimitadaPorLaComuna = true;
            } else {
                zonaDelimitadaPorLaComuna = false;
                alert("Por favor Ingrese la Zona Delimitada");
            }
        }
        validar();
    });

    //validación palabrasClave
    $("[name='palabrasClave']").on("blur", function () {
        if (zonaDelimitadaPorLaComuna) {
            if ($(this).val().length > 0) {
                palabrasClave = true;
            } else {
                palabrasClave = false;
                alert("Tiene que tener por lo menos una Palabra Clave");
            }
        }
        validar();
    });


    function validar() {

        if (coordenada_Latitude && coordenada_Longitude && calle && numeroAltura && nombreDePOI && numeroDeComuna && zonaDelimitadaPorLaComuna && palabrasClave) {
            $("input[type='submit']").prop("disabled", false);
        } else {
            $("input[type='submit']").prop("disabled", true);
        }

    }
});