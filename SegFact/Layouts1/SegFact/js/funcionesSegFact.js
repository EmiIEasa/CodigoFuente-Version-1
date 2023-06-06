/*funciones RegistroProcesos*/
 function MensajeGuarda(sec) {
     $(".modalG").css('display', 'block');
     if (sec == '1') {
         $(".modalBtnT").css('display', 'none');
         $(".modalBtnC").css('display', 'none');
     }
     if (sec == '2') {
         $(".modalBtnT").css('display', 'none');
         $(".modalBtnG").css('display', 'none');
     }
     if (sec == '3') {
         $(".modalBtnC").css('display', 'none');
         $(".modalBtnG").css('display', 'none');
     }
 } 
 function MensajeModificado() {
     $(".modalG").css('display', 'block');
     $(".modalBtnT").css('display', 'none');
     $(".modalBtnC").css('display', 'none');
 }

function cerrarDatosGenerales() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/Info/MISFACTURAS.aspx';
}
function cerrarContabilidad() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/Info/CONTABILIDAD.aspx';
}
function cerrarTesoreria() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/Info/TESORERIA.aspx';
}
function divBlock() {
    var textbtnPC = $(".cboOC").val();
    if (textbtnPC == "Con OC") {
        $(".pnlSCOC").css('display', 'block');
        $(".pnlCOC").css('display', 'block');
        $(".pnlSOC").css('display', 'none');
    }
    else {
        $(".pnlSCOC").css('display', 'block');
        $(".pnlCOC").css('display', 'none');
        $(".pnlSOC").css('display', 'block');
    }
    
}

function validaCampos() {
    var retorna = true;
    var textoError = '';
    var numFact = $(".txtNroFact").val();
    var adjuntoFactura = $("#fuFactura").val();
    var cboOC = $(".cboOC").val();
    var numOC = $(".txtNumOC").val();
    var centroCosto = $(".txtCentroCosto").val();
    var certServ = $(".txtCertServ").val();
    var adjuntoCertServ = $("#fuCertServ").val();
    var monto = $(".txtMonto").val();
    var email = $(".txtEmail").val();
    var sociedad = $(".cboSociedad").val();
    var obsContab = $(".txtObsContabilidad").val();
    var estado = $(".txtEstado").val();

    var ordenPago = $(".txtOrdenPago").val();
    var numContabilidad = $(".txtNumContabilidad").val();
    var adjuntoTesoreria = $("#fuAdjunto").val();

    if (estado == '') {
        if ($.isEmptyObject(numFact) == true || numFact == "") {
            textoError += "<p>Ingrese Número de Factura</p>";
            retorna = false;
        }
    
            if ($.isEmptyObject(adjuntoFactura) == true || adjuntoFactura == "") {
                textoError += "<p>Debe adjuntar Factura</p>";
                retorna = false;
            }
    
        if (cboOC == '-') {
            textoError += "<p'>Seleccione Orden de Compra</p>";
            retorna = false;
        } else if (cboOC == 'Con OC') {
            if ($.isEmptyObject(numOC) == true || numOC == "") {
                textoError += "<p>Ingrese Número Orden de Compra</p>";
                retorna = false;
            }
            if ($.isEmptyObject(centroCosto) == true || centroCosto == "") {
                textoError += "<p>Ingrese Centro de Costo</p>";
                retorna = false;
            }
            if ($.isEmptyObject(certServ) == true || certServ == "") {
                textoError += "<p>Ingrese Certificado de Servicio</p>";
                retorna = false;
            }
            if ($.isEmptyObject(adjuntoCertServ) == true || adjuntoCertServ == "") {
                textoError += "<p>Debe adjuntar Certificado de Servicio</p>";
                retorna = false;
            }
            if ($.isEmptyObject(monto) == true || monto == "") {
                textoError += "<p>Ingrese Monto</p>";
                retorna = false;
            }
            if (sociedad == 'Seleccione') {
                textoError += "<p'>Seleccione Tipo de Sociedad</p>";
                retorna = false;
            }
            if ($.isEmptyObject(email) == true || email == "") {
                textoError += "<p>Ingrese Correo electrónico</p>";
                retorna = false;
            }
        } else if (cboOC == 'Sin OC') {
            if ($.isEmptyObject(monto) == true || monto == "") {
                textoError += "<p>Ingrese Monto</p>";
                retorna = false;
            }
            if (sociedad == 'Seleccione') {
                textoError += "<p'>Seleccione Tipo de Sociedad</p>";
                retorna = false;
            }
            if ($.isEmptyObject(email) == true || email == "") {
                textoError += "<p>Ingrese Correo electrónico</p>";
                retorna = false;
            }
        }
    }
    if (estado == 'CONTABILIDAD') {
        if ($.isEmptyObject(obsContab) == true || obsContab == "") {
            textoError += "<p>Ingrese Observaciones</p>";
            retorna = false;
        }
    }
    if (estado == 'RECIBIDO') {
        if ($.isEmptyObject(obsContab) == true || obsContab == "") {
            textoError += "<p>Ingrese Observaciones</p>";
            retorna = false;
        }
    }
    if (estado == 'TESORERIA') {
        if ($.isEmptyObject(ordenPago) == true || ordenPago == "") {
            textoError += "<p>Ingrese N° Orden de Pago</p>";
            retorna = false;
        }
        

    }
        
    
    if (retorna == false) {
        $("#alertaBigData").css('display', 'block');
        $("#alertaBigData").html(textoError);
        return false;
    }
    else {
        return true;
    }
    return true;
}
function validateCUIT() {

    var aMult = '5432765432';
    var aMult = aMult.split('');
    var sCUIT = $('.txtCuit').val();
    sCUIT = sCUIT.split('-')[0] + sCUIT.split('-')[1] + sCUIT.split('-')[2];
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
            $('.txtCuit').css('border-color', 'green');
            return true;
        } 
    }
    $('.spanCUITMAL').css('display', 'block');
    $('.txtCuit').css('border-color', 'red');
    $('.spanBIEN').css('display', 'none');
    return false;
}
$(function () {
    $(".cboOC").on("change", function () {
        if ($(this).val() != "-") {
            
            if ($(this).val() == "Con OC") {
                $(".pnlSCOC").css('display', 'block');
                $(".pnlCOC").css('display', 'block');
                $(".pnlSOC").css('display', 'none');
            }
            else {
                $(".pnlSCOC").css('display', 'block');
                $(".pnlCOC").css('display', 'none');
                $(".pnlSOC").css('display', 'block');
            }
        }
        else {
            $(".pnlSCOC").css('display', 'none');
            $(".pnlCOC").css('display', 'none');
            $(".pnlSOC").css('display', 'none');
        }
        
    });
    var settings = {
        locale: 'es',
        format: 'DD/MM/YYYY',
    };
    $('#datetimepicker1').datetimepicker(settings);
    $('#datetimepicker2').datetimepicker(settings);
    $('#datetimepicker3').datetimepicker(settings);
    $('#datetimepicker4').datetimepicker(settings);

    $(document).ready(function () {
        $(".nav-tabs a").click(function () {
            $(this).tab('show');
        });
    });
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
});

function validaDecimal(evt, element) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charcode == 8))
        return false;
    else {
        var len = $(element).val().length;
        var index = $(element).val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var CharAfterdot = (len + 1) - index;
            if (CharAfterdot > 3) {
                return false;
            }
        }

    }
    return true;
}

function soloCUIT(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 0123456789-";
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


