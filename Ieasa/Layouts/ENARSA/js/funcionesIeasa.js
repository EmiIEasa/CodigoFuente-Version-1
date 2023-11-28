/*funciones RegistroProcesos*/
 function MensajeGuarda() {
     $(".modal").css('display', 'block');
     $(".modalBtnC").css('display', 'none');
     $(".modalBtnAN").css('display', 'none');
     $(".modalBtn").css('display', 'none');
     
     $("#btnGroupGrales").css('display', 'none');
     $("#btnGroupRubros").css('display', 'none');
 }
 function MensajeGuardaAN() {
     $(".modal").css('display', 'block');
     $(".modalBtnC").css('display', 'none');
     $(".modalBtn").css('display', 'none');
     $(".modalBtnDatos").css('display', 'none');
     $("#btnGroupGrales").css('display', 'none');
     $("#btnGroupRubros").css('display', 'none');
     $("#guardar").css('display', 'none');
     $("#anonimo").css('display', 'block');
 }
 function MensajeExisteEmpresa() {
     $(".modal").css('display', 'block');
     $(".modalBtnC").css('display', 'none');
     $(".modalBtn").css('display', 'none');
     $(".modalBtnDatos").css('display', 'none');
     $("#btnGroupGrales").css('display', 'none');
     $("#btnGroupRubros").css('display', 'none');
     $("#guardar").css('display', 'none');
     $("#existeEmpresa").css('display', 'block');
 }
function MensajeModificado(sec) {
    $(".modal").css('display', 'block');
    if (sec == '1') {
        $(".modalBtnC").css('display', 'none');
        $(".modalBtnAN").css('display', 'none');
        $(".modalBtnDatos").css('display', 'none');
        $("#btnGroupGrales").css('display', 'none');
        $("#btnGroupRubros").css('display', 'none');
    }
    if (sec == '2') {
        $(".modalBtn").css('display', 'none');
        $(".modalBtnAN").css('display', 'none');
        $(".modalBtnDatos").css('display', 'none');
        $("#btnGroupGrales").css('display', 'none');
        $("#btnGroupRubros").css('display', 'none');
    }
    if (sec == '4') {
        $(".modalBtn").css('display', 'none');
        $(".modalBtnC").css('display', 'none');
        $(".modalBtnAN").css('display', 'none');
        $("#btnGroupGrales").css('display', 'none');
        $("#btnGroupRubros").css('display', 'none');
    }

}
function MensajeCompra(mensaje) {
    $(".modal").css('display', 'block');
    $("#guardar").html(mensaje);
    $(".modalBtnC").css('display', 'block');
    $(".modalBtn").css('display', 'none');
    $(".modalBtnAN").css('display', 'none');
    $(".modalBtnDatos").css('display', 'none');
    $("#btnGroupGrales").css('display', 'none');
    $("#btnGroupRubros").css('display', 'none');
}

function buscadorRubro() {
    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen(
        { allow_single_deselect: true }
     );
}
function cerrar() {
    //location.href = 'http://ibiza:2222/sites/formularios/Lists/AltaProvisoria/AllItems.aspx';
    //location.href = 'https://proveedores-desa.ieasa.com.ar/Lists/AltaProvisoria/AllItems.aspx';
    location.href = 'https://portalproveedores.energia-argentina.com.ar/Lists/AltaProvisoria/AllItems.aspx';
}
function cerrarHome() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/SitePages/ENARSA_MP.aspx';
}
function cerrarCompras() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/INFO/Compras.aspx';
}
function cerrarAnonimo() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/SitePages/ENARSA_MP.aspx';
}

function cerrarIrDatos() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/INFO/MISDATOS.aspx';
}


function volverGrales() {
    $("#alertaBigData").css('display', 'none');
    $("#home").removeClass("fade");
    $("#home").addClass("active");
    $("#rubros").removeClass("active");
    $("#rubros").addClass("fade");

    $("#pestaniaDatosGrales").addClass("active");
    $("#pestaniaRubros").removeClass("active");
    
    $("#btnGroupGrales").css('display', 'inline-flex');
    $("#btnGroupRubros").css('display', 'none');
}
function volverRubros() {
    $("#alertaBigData").css('display', 'none');
    $("#rubros").removeClass("fade");
    $("#rubros").addClass("active");
    $("#menu1").removeClass("active");
    $("#menu1").addClass("fade");

    $("#pestaniaRubros").addClass("active");
    $("#pestaniaDatosContacto").removeClass("active");

    $("#btnGroupRubros").css('display', 'inline-flex');
    $("#btnGroupContacto").css('display', 'none');
}

function volverContacto() {
    $("#alertaBigData").css('display', 'none');
    $("#menu1").removeClass("fade");
    $("#menu1").addClass("active");
    $("#menu2").removeClass("active");
    $("#menu2").addClass("fade");
    $("#pestaniaDatosContacto").addClass("active");
    $("#pestaniaDatosImpositivos").removeClass("active");
    $("#btnGroupContacto").css('display', 'inline-flex');
    $("#btnGroupImpositivo").css('display', 'none');
}

function divBlock() {
    $("#btnGroupID").css('display', 'inline-flex');
    $("#btnGroupGrales").css('display', 'none');
    var textbtnPC = $(".cboPais").val();
    if (textbtnPC == "Argentina") {
        $(".cboProvincia").css('display', 'block');
        $(".txtProvincia").css('display', 'none');
    }
    var estado = $(".txtEstado").val();
    var proveedor = $(".cboProveedor").val();
    var CIE = $(".txtCIE").val();
    if ($.isEmptyObject(CIE) == true || CIE == "") {
        $(".divCIE").css('display', 'none');
        $(".divNacional").css('display', 'none');
        $(".divCertExencion").css('display', 'block');
    }
    else {
        $(".divCIE").css('display', 'block');
        $(".divCertExencion").css('display', 'block');
        $(".divNacional").css('display', 'none');
    }
    if (proveedor == "Nacional") {
        $(".divCIE").css('display', 'none');
        $(".divCertExencion").css('display', 'block');
        $(".divNacional").css('display', 'block');
    }
 

    
    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
}

function validaCamposCompras() {
    var retorna = true;
    var textoError = '';
    var observaciones = $('.txtObsCompras').val();
    if ($.isEmptyObject(observaciones) == true || observaciones == "") {
        textoError += "<p>Ingrese Observaciones</p>";
        retorna = false;
    }
    if (retorna == false) {
        $("#alertaBigData").css('display', 'block');
        $("#alertaBigData").html(textoError);
        return false;
    }
    else {
        return true;
    }
}

function validaCamposGenerales() {
    var retorna = true;
    var textoError = '';
    var razonSocial = $(".txtRazonSocial").val();
    
    var personeria = $(".cboPersoneria").val();
    var adjPersoneria = $(".fuPersoneriaJurAdj").val();
    var actividad = $(".txtActPrinc").val();
    
    var pais = $(".cboPais").val();
    var provincia = $(".txtProvincia").val();
    var cboprovincia = $(".cboProvincia").val();
    var ciudad = $(".txtCiudad").val();
    var codPost = $(".txtCodPostal").val();
    var calle = $(".txtCalle").val();
    var altura = $(".txtAltura").val();
    var depto = $(".txtDepto").val();
    var piso = $(".txtPiso").val();
    


    if ($.isEmptyObject(razonSocial) == true || razonSocial == "") {
        textoError += "<p>Ingrese Razón Social</p>";
        retorna = false;
    }
        
    if (personeria == 'Seleccione') {
        textoError += "<p'>Seleccione Personería</p>";
        retorna = false;
    }
    if ($(".fuPersoneriaJur").css('display') == 'block') {
        if ($.isEmptyObject(adjPersoneria) == true || adjPersoneria == "") {
            if ($(".cboPersoneria").val() == "Física")
            {
                textoError += "<p>Adjunte DNI</p>";
                
            } else {
                textoError += "<p>Adjunte Estatuto/Contrato Social</p>";
            }
            retorna = false;
        }
    }
    if ($.isEmptyObject(actividad) == true || actividad == "") {
        textoError += "<p>Ingrese Actividad Principal</p>";
        retorna = false;
    }
    //if (cboRama1 == 'Seleccione') {
    //    textoError += "<p'>Seleccione Rama 1</p>";
    //    retorna = false;
    //}
    //if (cboRama2 == 'Seleccione') {
    //    textoError += "<p'>Seleccione Rama 2</p>";
    //    retorna = false;
    //}
    //if (cboRama3 == 'Seleccione') {
    //    textoError += "<p'>Seleccione Rama 3</p>";
    //    retorna = false;
    //}
    //if (cboRama4 == 'Seleccione') {
    //    textoError += "<p'>Seleccione Rama 4</p>";
    //    retorna = false;
    //}
    if (pais == 'Seleccione') {
        textoError += "<p'>Seleccione País</p>";
        retorna = false;
    }
    if ($(".txtProvincia").css('display') == 'block') {
        if ($.isEmptyObject(provincia) == true || provincia == "") {
            textoError += "<p>Ingrese Provincia</p>";
            retorna = false;
        }
    }
    if ($(".cboProvincia").css('display') == 'block') {
        if ($(".cboProvincia").val() == 'Seleccione') {
            textoError += "<p'>Seleccione Provincia</p>";
            retorna = false;
        }
    }
    if ($.isEmptyObject(ciudad) == true || ciudad == "") {
        textoError += "<p>Ingrese Ciudad</p>";
        retorna = false;
    }
    if ($.isEmptyObject(codPost) == true || codPost == "") {
        textoError += "<p>Ingrese Código Postal</p>";
        retorna = false;
    }
    if ($.isEmptyObject(calle) == true || calle == "") {
        textoError += "<p>Ingrese Calle</p>";
        retorna = false;
    }
    if ($.isEmptyObject(altura) == true || altura == "") {
        textoError += "<p>Ingrese Altura</p>";
        retorna = false;
    }
    
    
    if (retorna == false) {
        $("#alertaBigData").css('display', 'block');
        $("#alertaBigData").html(textoError);
        return false;
    }
    else {
        //return true;
        $("#alertaBigData").css('display', 'none');
        $("#home").removeClass("active");
        $("#home").addClass("fade");
        $("#rubros").removeClass("fade");
        $("#rubros").addClass("active");

        $("#pestaniaDatosGrales").removeClass("active");
        $("#pestaniaRubros").addClass("active");

        $("#btnGroupGrales").css('display', 'none');
        $("#btnGroupRubros").css('display', 'inline-flex');

        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    }
    
}
function validaCamposRubros(ind) {
    if (ind == "1") {
        $("#alertaBigData").css('display', 'block');
        $("#alertaBigData").html("<p'>Seleccione Rubro</p>");
        $("#btnGroupRubros").css('display', 'inline-flex');
    }
    else {
        //return true;
        $("#alertaBigData").css('display', 'none');
        $("#rubros").removeClass("active");
        $("#rubros").addClass("fade");
        $("#menu1").removeClass("fade");
        $("#menu1").addClass("active");
        $("#home").removeClass("active");
        $("#home").addClass("fade");

        $("#pestaniaDatosGrales").removeClass("active");
        $("#pestaniaRubros").removeClass("active");
        $("#pestaniaDatosContacto").addClass("active");

        $("#btnGroupGrales").css('display', 'none');
        $("#btnGroupRubros").css('display', 'none');
        $("#btnGroupContacto").css('display', 'inline-flex');
    }
}

function validaCamposContacto() {
    var retorna = true;
    var textoError = '';
    var apoderado = $(".txtApoderado").val();
    var adjApoderado = $(".fuApoderado").val();
    var ventasContac = $(".txtVentasContacto").val();
    var ventasCorreo = $(".txtVentasCorreo").val();
    var adminContac = $(".txtAdministracionContacto").val();
    var adminCorreo = $(".txtAdministracionCorreo").val();
    var Telefono = $(".txtTelefono").val();
    var fax = $(".txtFax").val();
    var adjDDJJ = $(".fuDDJJ").val();

    if ($.isEmptyObject(apoderado) == true || apoderado == "") {
        textoError += "<p>Ingrese Apoderado/Representante Legal</p>";
        retorna = false;
    }
    if ($(".cboPersoneria").val() != "Física") {
        if ($.isEmptyObject(adjApoderado) == true || adjApoderado == "") {
            textoError += "<p>Adjunte Poder</p>";
            retorna = false;
        }
    }
    
    if ($.isEmptyObject(ventasContac) == true || ventasContac == "") {
        textoError += "<p>Ingrese Contacto - Ventas</p>";
        retorna = false;
    }
    if ($.isEmptyObject(ventasCorreo) == true || ventasCorreo == "") {
        textoError += "<p>Ingrese Correo - Ventas</p>";
        retorna = false;
    } else {
        re=/^([\da-z_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/
        if(!re.exec(ventasCorreo)){
            textoError += "<p>Ingrese Correo - Ventas válido</p>";
        }
    }
    if ($.isEmptyObject(adminContac) == true || adminContac == "") {
        textoError += "<p>Ingrese Contacto - Administración</p>";
        retorna = false;
    }
    if ($.isEmptyObject(adminCorreo) == true || adminCorreo == "") {
        textoError += "<p>Ingrese Correo - Administración</p>";
        retorna = false;
    } else {
        re = /^([\da-z_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/
        if (!re.exec(adminCorreo)) {
            textoError += "<p>Ingrese Correo - Administración válido</p>";
        }
    }
    if ($.isEmptyObject(Telefono) == true || Telefono == "") {
        textoError += "<p>Ingrese Teléfono</p>";
        retorna = false;
    }
   
    
        if ($.isEmptyObject(adjDDJJ) == true || adjDDJJ == "") {
            textoError += "<p>Adjunte archivo DDJJ del 202</p>";
            retorna = false;
        }
    

    
    if (retorna == false) {
        $("#alertaBigData").css('display', 'block');
        $("#alertaBigData").html(textoError);
        return false;
    }
    else {
        $("#alertaBigData").css('display', 'none');
        //return true;
        $("#menu1").removeClass("active");
        $("#menu1").addClass("fade");
        $("#menu2").removeClass("fade");
        $("#menu2").addClass("active");
        $("#pestaniaDatosContacto").removeClass("active");
        $("#pestaniaDatosImpositivos").addClass("active");
        $("#btnGroupContacto").css('display', 'none');
        $("#btnGroupImpositivo").css('display', 'inline-flex');
    }
    
}

function validaCamposImpositivos() {
    var retorna = true;
    var textoError = '';
    
    var cuit = $(".txtCuit").val();
    
    var cboProveedor = $(".cboProveedor").val();
    var cboConImpG = $(".cboConImpG").val();
    var cboCondImIVA = $("#cboCondImIVA").val();
    var cboIngBr = $(".cboIngresosBrutos").val();
    var adjIngreBr = $(".fuIngresosBrutos").val();
    var adjConvenioMultilateral = $(".fuConvenioMultilateral").val();
    var ingBr = $(".txtIngresosBrutos").val();
    
    if (cboProveedor == 'Seleccione') {
        textoError += "<p'>Seleccione Proveedor</p>";
        retorna = false;
    }
    if (cboProveedor == 'Nacional') {
        if ($.isEmptyObject(cuit) == true || cuit == "") {
            textoError += "<p>Ingrese CUIT</p>";
            retorna = false;
        } else {
            var aMult = '5432765432';
            var aMult = aMult.split('');
            var sCUIT = cuit;

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

                if (iResult != aCUIT[10]) {
                    textoError += "<p>Ingrese CUIT Válido</p>";
                    retorna = false;
                }
            }
            else {
                textoError += "<p>Ingrese CUIT Válido</p>";
                retorna = false;
            }
        }
        if (cboConImpG == 'Seleccione') {
            textoError += "<p'>Seleccione Condición Impositiva (Impuesto a las Ganancias)</p>";
            retorna = false;
        }
        if (cboCondImIVA == 'Seleccione') {
            textoError += "<p'>Seleccione Condición Impositiva (IVA)</p>";
            retorna = false;
        }
    
        if (cboIngBr == 'Seleccione') {
            textoError += "<p'>Seleccione Tipo Ingresos Brutos</p>";
            retorna = false;
        }
    
        if (cboIngBr == 'CM Convenio Multilateral') {
            if ($(".adjuntoItem").length == 0) {
                if ($.isEmptyObject(adjConvenioMultilateral) == true || adjConvenioMultilateral == "") {
                    textoError += "<p>Adjunte Convenio Multilateral</p>";
                    retorna = false;
                }
            }
        }
        if ($(".adjuntoItem").length == 0) {
            if ($.isEmptyObject(adjIngreBr) == true || adjIngreBr == "") {
                textoError += "<p>Adjunte Constancia Ingresos Brutos</p>";
                retorna = false;
            }
        }
        if ($.isEmptyObject(ingBr) == true || ingBr == "") {
            textoError += "<p>Ingrese Nº Ingresos Brutos</p>";
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
   }
function validateCUIT() {

    var aMult = '5432765432';
    var aMult = aMult.split('');
    var sCUIT = $('.txtCuit').val();

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
    $(".cboPais").on("change", function () {
        if ($(this).val() != "Seleccione") {
            
            if ($(this).val() == "Argentina") {
                $(".cboProvincia").css('display', 'block');
                $(".txtProvincia").css('display', 'none');
            }
            else {
                $(".txtProvincia").css('display', 'block');
                $(".cboProvincia").css('display', 'none');
            }
        }
        
        
    });
    $(document).ready(function () {
        $(".nav-tabs a").click(function () {
            $(this).tab('show');
        });
    });
    $(".cboPersoneria").on("change", function () {
        if ($(this).val() == "Seleccione") {
            $(".fuPersoneriaJur").css('display', 'none');
       
        } else if ($(this).val() == "Física") {
            $(".fuPersoneriaJur").css('display', 'block');
            $("#alertDNI").css('display', 'block');
            $("#alertEstatuto").css('display', 'none');
        }
        else {
            $(".fuPersoneriaJur").css('display', 'block');
            $("#alertDNI").css('display', 'none');
            $("#alertEstatuto").css('display', 'block');
        }
    });
    $(".cboIngresosBrutos").on("change", function () {
        if ($(this).val() == "CM Convenio Multilateral") {
            $(".adjuntoConvenioMulti").css('display', 'block');

        } else {
            $(".adjuntoConvenioMulti").css('display', 'none');
        }
    });
    $(".cboProveedor").on("change", function () {
        if ($(this).val() == "Nacional") {
            $(".divNacional").css('display', 'block');
            $(".divCertExencion").css('display', 'block');
        } else if ($(this).val() == "Extranjero") {
            $(".divNacional").css('display', 'none');
            $(".divCertExencion").css('display', 'block');
            $(".txtCuit").html('');
        }
        else {
            $(".divNacional").css('display', 'none');
            $(".divCertExencion").css('display', 'none');
            $(".txtCuit").html('');
        }
    });
    
});

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



