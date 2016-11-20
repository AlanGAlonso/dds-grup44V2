$(document).ready(function () {

    var password, password2, nombre, email;
    var iniPassword = $("#password").val();
    var iniPassword2 = $("#password2").val();
    var iniNombre = $("#nombre").val();
    var iniEmail = $("#email").val();
    $("input[type='submit']").prop("disabled", true);


    $("#password").on("blur", function () {
        //validación Contraseña 1
        if ($(this).val().length != 0) {
            if ($(this).val().length >= 5) {
                password = true;
            } else {
                password = false;
                alert("La contraseña debe tener al menos 5 caracteres.");
            }
        }
        validar();
    });

    $("#password2").on("blur", function () {
        //validación Contraseña 2
        if (password && $(this).val().length != 0) {
            if ($(this).val() == $("#password").val()) {
                password2 = true;
            } else {
                password2 = false;
                alert("La contraseña repetida debe coincidir con la original.");
            }
        }
        validar();
    });

    $("#nombre").on("blur", function () {
        //validación Nombre
        if (password2) {
            if ($(this).val().split(" ").length >= 2) {
                nombre = true;
            } else {
                nombre = false;
                alert("Por favor incluya su nombre y apellido.");
            }
        }
        validar();
    });

    $("#email").on("blur", function () {
        if (nombre) {
            //validación Email
            if ($(this).val().indexOf("@") > -1 && $(this).val().indexOf("@") < $(this).val().indexOf(".")) {
                email = true;
            } else {
                email = false;
                alert("Por favor ingrese un e-mail válido.");
            }
        }
        validar();
    });

    function validar() {
        if ($("#password").val() == iniPassword && $("#password2").val() == iniPassword2) {
            password = true;
            password2 = true;
        }

        if ($("#nombre").val() == iniNombre) {
            nombre = true;
        }

        if ($("#email").val() == iniEmail) {
            email = true;
        }

        if (password && password2 && nombre && email) {
            $("input[type='submit']").prop("disabled", false);
        } else {
            $("input[type='submit']").prop("disabled", true);
        }

    }
});