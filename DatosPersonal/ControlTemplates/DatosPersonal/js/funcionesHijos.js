/*funciones RegistroProcesos*/
 function soloCUIT(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 0123456789";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
function soloNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 0123456789";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }

}
function validateCUIT() {
    var aMult = '5432765432';
    var aMult = aMult.split('');
    var sCUIT = $('.txtCuil').val();

    if (sCUIT.length == 11) {
        aCUIT = sCUIT.split('');
        var iResult = 0;
        for (i = 0; i <= 9; i++) {
            iResult += aCUIT[i] * aMult[i];
        }
        iResult = (iResult % 11);
        iResult = 11 - iResult;
        if (iResult == 11) iResult = 0;
        if (iResult == 10) iResult = 9;

        if (iResult == aCUIT[10]) {
            $('.spanCUITMAL').css('display', 'none');
            $('.spanBIEN').css('display', 'block');
            $('.txtCuil').css('border-color', 'green');
            return true;
        } 
    }
    $('.spanCUITMAL').css('display', 'block');
    $('.txtCuil').css('border-color', 'red');
    $('.spanBIEN').css('display', 'none');
    return false;
}
function validaCamposObligatorios() {
    var retorna = true;
    var textoError = '';
    var nombreApellido = $(".txtNombreApellido").val();
    var fecha = $(".dtFechaNacimiento").val();
    var nacionalidad = $(".txtNacionalidad").val();
    var cuil = $(".txtCuil").val();
    

    
    if ($.isEmptyObject(nombreApellido) == true || nombreApellido == "") {
        textoError += "<p>Ingrese Nombre y Apellido</p>";
        retorna = false;
    }
    if ($.isEmptyObject(fecha) == true || fecha == "") {
        textoError += "<p>Ingrese Fecha de Nacimiento</p>";
        retorna = false;
    }
    if ($.isEmptyObject(nacionalidad) == true || nacionalidad == "") {
        textoError += "<p>Ingrese Nacionalidad</p>";
        retorna = false;
    }
    if ($.isEmptyObject(cuil) == true || cuil == "") {
        textoError += "<p>Ingrese CUIL</p>";
        retorna = false;
    }
    
    
    if (retorna == false) {
        $("#alertaCamposObligatorios").css('display', 'block');
        $("#alertaCamposObligatorios").html(textoError);
        return false;
    }
    else {
        return true;
        $("#alertaCamposObligatorios").css('display', 'none');
    }

}
function MensajeGuarda() {
    $(".modal").css('display', 'block');
}
function cerrar() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/Lists/DatosPersonal.aspx';
}

