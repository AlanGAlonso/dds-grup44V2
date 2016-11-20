$(document).ready(function () {

    var dni, password, password2, nombre, email;
    $("input[type='submit']").prop("disabled", true);

    //validación DNI
    $("#dni").on("blur", function () {
        if ($(this).val().length < 9 && parseInt($(this).val()) > 0) {
            dni = true;
        } else {
            dni = false;
            alert("El DNI debe tener hasta 8 dígitos y mayor que cero.");
        }
        validar();
    });

    $("#password").on("blur", function () {
        //validación Contraseña 1
        if (dni) {
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
        if (password) {
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

        if (dni && password && password2 && nombre && email) {
            $("input[type='submit']").prop("disabled", false);
        } else {
            $("input[type='submit']").prop("disabled", true);
        }

    }
});