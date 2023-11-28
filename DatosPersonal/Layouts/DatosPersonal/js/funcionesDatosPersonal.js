/*funciones RegistroProcesos*/
function OcultaDivs() {
    $('.divConyuge').hide();
    $('.divHijos').hide();
    $('#divAdjuntoPrimarios').hide();
    $('#divAdjuntoSecundarios').hide();
    $('#divAdjuntoTerciarios').hide();
    $('#divAdjuntoUniversitarios').hide();
}

function MuestraDivConyuge() {
    $('.divConyuge').show();
    $('.divHijos').hide();
}
function OcultaDivConyuge() {
    $('.divConyuge').hide();
    $('.divHijos').hide();
}

function MuestraDivHijos(e) {
    if (e != "0") {
        if (e == "1") {
            $('.divHijos1').show();
            $('.divHijos2').hide();
            $('.divHijos3').hide();
            $('.divHijos4').hide();
            $('.divHijos5').hide();
            $('.divHijos6').hide();
        }
        if (e == "2") {
            $('.divHijos1').show();
            $('.divHijos2').show();
            $('.divHijos3').hide();
            $('.divHijos4').hide();
            $('.divHijos5').hide();
            $('.divHijos6').hide();
        }
        if (e == "3") {
            $('.divHijos1').show();
            $('.divHijos2').show();
            $('.divHijos3').show();
            $('.divHijos4').hide();
            $('.divHijos5').hide();
            $('.divHijos6').hide();
        }
        if (e == "4") {
            $('.divHijos1').show();
            $('.divHijos2').show();
            $('.divHijos3').show();
            $('.divHijos4').show();
            $('.divHijos5').hide();
            $('.divHijos6').hide();
        }
        if (e == "5") {
            $('.divHijos1').show();
            $('.divHijos2').show();
            $('.divHijos3').show();
            $('.divHijos4').show();
            $('.divHijos5').show();
            $('.divHijos6').hide();
        }
        if (e == "6") {
            $('.divHijos1').show();
            $('.divHijos2').show();
            $('.divHijos3').show();
            $('.divHijos4').show();
            $('.divHijos5').show();
            $('.divHijos6').show();
        }
    }
    else {
        $('.divHijos').hide();
    }
}
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
function validateCUIT(e) {
    var aMult = '5432765432';
    var aMult = aMult.split('');
    var sCUIT = $('.txtCuil' + e).val();

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
            $('.spanCUITMAL' + e).css('display', 'none');
            $('.spanBIEN' + e).css('display', 'block');
            $('.txtCuil' + e).css('border-color', 'green');
            return true;
        }
    }
    $('.spanCUITMAL' + e).css('display', 'block');
    $('.txtCuil' + e).css('border-color', 'red');
    $('.spanBIEN' + e).css('display', 'none');
    return false;
}
function validaCamposPerfil() {
    var retorna = true;
    var textoError = '';
    var nombreApellido = $(".txtNombreApellido").val();
    var legajo = $(".txtLegajo").val();
    var piso = $(".txtPisoPerfil").val();
    var gerencia = $(".txtGerencia").val();
    var posicion = $(".txtPosicion").val();


    if ($.isEmptyObject(nombreApellido) == true || nombreApellido == "") {
        textoError += "<p>Ingrese Nombre y Apellido</p>";
        $(".txtNombreApellido").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtNombreApellido").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(legajo) == true || legajo == "") {
        textoError += "<p>Ingrese Legajo</p>";
        $(".txtLegajo").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtLegajo").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(piso) == true || piso == "") {
        textoError += "<p>Ingrese Piso</p>";
        $(".txtPisoPerfil").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtPisoPerfil").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(gerencia) == true || gerencia == "") {
        textoError += "<p>Ingrese Gerencia</p>";
        $(".txtGerencia").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtGerencia").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(posicion) == true || posicion == "") {
        textoError += "<p>Ingrese Posición</p>";
        $(".txtPosicion").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtPosicion").css('border-color', '#ababab');
    }
    if (retorna == false) {
        $("#alertaCamposObligatorios").css('display', 'block');
        $("#alertaCamposObligatorios").html(textoError);
        return false;
    }
    else {
        //return true;
        $("#alertaCamposObligatorios").css('display', 'none');

        $("#pestaniaPerfil").removeClass("active");
        $("#pestaniaDatosPersonales").addClass("active");

        $("#perfil").removeClass("active");
        $("#perfil").addClass("fade");
        $("#datosPersonales").removeClass("fade");
        $("#datosPersonales").addClass("active");

        $("#btnGroupPerfil").css('display', 'none');
        $("#btnGroupDatosPersonales").css('display', 'inline-flex');
    }
}
function validaCamposDatosPersonales() {
    var retorna = true;
    var textoError = '';
    var sociedad = $(".cboSociedad").val();

    var fecha = $(".dtFecha").val();
    var genero = $(".cboGenero").val();
    var cuil = $(".txtCuil").val();
    var provincia = $(".cboProvincia").val();
    var ciudad = $(".txtCiudad").val();
    var calle = $(".txtCalle").val();
    var altura = $(".txtAltura").val();
    var piso = $(".txtPiso").val();
    var depto = $(".txtDepto").val();
    var cp = $(".txtCodPostal").val();
    var celularPersonal = $(".txtCelularPersonal").val();
    var celularEmpresa = $(".txtCelularEmpresa").val();
    var companiaEmpresa = $(".txtCompaniaCelular").val();
    if (sociedad == 'Seleccione') {
        textoError += "<p>Seleccione Sociedad</p>";
        $(".cboSociedad").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboSociedad").css('border-color', '#ababab');
    }

    if ($.isEmptyObject(fecha) == true || fecha == "") {
        textoError += "<p>Ingrese Fecha de Nacimiento</p>";
        $(".dtFecha").css('border-color', 'red');
        retorna = false;
    } else {
        $(".dtFecha").css('border-color', '#ababab');
    }
    if (genero == 'Seleccione') {
        textoError += "<p>Seleccione Género</p>";
        $(".cboGenero").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboGenero").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(cuil) == true || cuil == "") {
        textoError += "<p>Ingrese CUIL</p>";
        $(".txtCuil").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCuil").css('border-color', '#ababab');
    }
    if (provincia == 'Seleccione') {
        textoError += "<p>Seleccione Provincia</p>";
        $(".cboProvincia").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboProvincia").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(ciudad) == true || ciudad == "") {
        textoError += "<p>Ingrese Ciudad</p>";
        $(".txtCiudad").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCiudad").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(calle) == true || calle == "") {
        textoError += "<p>Ingrese Calle</p>";
        $(".txtCalle").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCalle").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(altura) == true || altura == "") {
        textoError += "<p>Ingrese Altura</p>";
        $(".txtAltura").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtAltura").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(piso) == true || piso == "") {
        textoError += "<p>Ingrese Piso</p>";
        $(".txtPiso").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtPiso").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(depto) == true || depto == "") {
        textoError += "<p>Ingrese Departamento</p>";
        $(".txtDepto").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtDepto").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(cp) == true || cp == "") {
        textoError += "<p>Ingrese Código Postal</p>";
        $(".txtCodPostal").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCodPostal").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(celularPersonal) == true || celularPersonal == "") {
        textoError += "<p>Ingrese Celular Personal</p>";
        $(".txtCelularPersonal").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCelularPersonal").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(celularEmpresa) == true || celularEmpresa == "") {
        textoError += "<p>Ingrese Celular Empresa</p>";
        $(".txtCelularEmpresa").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCelularEmpresa").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(companiaEmpresa) == true || companiaEmpresa == "") {
        textoError += "<p>Ingrese Compañia Celular de la empresa</p>";
        $(".txtCompaniaCelular").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtCompaniaCelular").css('border-color', '#ababab');
    }
    if (retorna == false) {
        $("#alertaCamposObligatorios").css('display', 'block');
        $("#alertaCamposObligatorios").html(textoError);
        return false;
    }
    else {
        //return true;
        $("#alertaCamposObligatorios").css('display', 'none');

        $("#pestaniaDatosPersonales").removeClass("active");
        $("#pestaniaEstructura").addClass("active");

        $("#datosPersonales").removeClass("active");
        $("#datosPersonales").addClass("fade");
        $("#estructura").removeClass("fade");
        $("#estructura").addClass("active");

        $("#btnGroupDatosPersonales").css('display', 'none');
        $("#btnGroupEstructura").css('display', 'inline-flex');
    }
}
function validaCamposEstructura() {
    var retorna = true;
    var textoError = '';
    var puestoActual = $(".txtPuestoActual").val();

    if ($.isEmptyObject(puestoActual) == true || puestoActual == "") {
        textoError += "<p>Ingrese Puesto Actual</p>";
        $(".txtPuestoActual").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtPuestoActual").css('border-color', '#ababab');
    }

    if (retorna == false) {
        $("#alertaCamposObligatorios").css('display', 'block');
        $("#alertaCamposObligatorios").html(textoError);
        return false;
    }
    else {
        //return true;
        $("#alertaCamposObligatorios").css('display', 'none');

        $("#pestaniaEstructura").removeClass("active");
        $("#pestaniaGrupoFamiliar").addClass("active");

        $("#grupoFamiliar").removeClass("fade");
        $("#grupoFamiliar").addClass("active");
        $("#estructura").removeClass("active");
        $("#estructura").addClass("fade");

        $("#btnGroupEstructura").css('display', 'none');
        $("#btnGroupGrupoFliar").css('display', 'inline-flex');
    }
}
function validaCamposGrupoFliar() {
    var retorna = true;
    var textoError = '';
    var nombreConyuge = $(".txtNombreConyuge").val();
    var cuilConyuge = $(".txtCuil1").val();
    var fechaNacimientoConyuge = $(".dtFechaNacimientoConyuge").val();
    var cantHijos = $('.cboCantidadHijos').val()

    if ($('.cboEstadoCivil').val() == 'Casado/a') {

        if ($.isEmptyObject(nombreConyuge) == true || nombreConyuge == "") {
            textoError += "<p>Ingrese Nombre y Apellido Conyuge</p>";
            $(".txtNombreConyuge").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtNombreConyuge").css('border-color', '#ababab');
        }
        if ($.isEmptyObject(fechaNacimientoConyuge) == true || fechaNacimientoConyuge == "") {
            textoError += "<p>Ingrese Fecha de Nacimiento Conyuge</p>";
            $(".dtFechaNacimientoConyuge").css('border-color', 'red');
            retorna = false;
        } else {
            $(".dtFechaNacimientoConyuge").css('border-color', '#ababab');
        }
        if ($.isEmptyObject(cuilConyuge) == true || cuilConyuge == "") {
            textoError += "<p>Ingrese CUIL Conyuge</p>";
            $(".txtCuil1").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtCuil1").css('border-color', '#ababab');
        }
    }
    if ($('.cboEstadoCivil').val() == 'Concubinato') {
        if ($.isEmptyObject(nombreConyuge) == true || nombreConyuge == "") {
            textoError += "<p>Ingrese Nombre y Apellido Conviviente</p>";
            $(".txtNombreConyuge").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtNombreConyuge").css('border-color', '#ababab');
        }
        if ($.isEmptyObject(fechaNacimientoConyuge) == true || fechaNacimientoConyuge == "") {
            textoError += "<p>Ingrese Fecha de Nacimiento Conviviente</p>";
            $(".dtFechaNacimientoConyuge").css('border-color', 'red');
            retorna = false;
        } else {
            $(".dtFechaNacimientoConyuge").css('border-color', '#ababab');
        }
        if ($.isEmptyObject(cuilConyuge) == true || cuilConyuge == "") {
            textoError += "<p>Ingrese CUIL Conviviente</p>";
            $(".txtCuil1").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtCuil1").css('border-color', '#ababab');
        }
    }
    for (var i = cantHijos; i > 0; i--) {
        if ($.isEmptyObject($(".txtNombreApellidoHijos" + i).val()) == true || $(".txtNombreApellidoHijos" + i).val() == "") {
            textoError += "<p>Ingrese Nombre y Apellido Hijo" + i + "</p>";
            $('.txtNombreApellidoHijos' + i).css('border-color', 'red');
            var nombreHijo = "Hijo " + i;
            retorna = false;
        } else {
            $('.txtNombreApellidoHijos' + i).css('border-color', '#ababab');
            var nombreHijo = $('.txtNombreApellidoHijos' + i).val();
        }
        if ($.isEmptyObject($(".dtFechaNacimientoHijos" + i).val()) == true || $(".dtFechaNacimientoHijos" + i).val() == "") {
            textoError += "<p>Ingrese Fecha Nacimiento de " + nombreHijo + "</p>";
            $('.dtFechaNacimientoHijos' + i).css('border-color', 'red');
            retorna = false;
        } else {
            $('.dtFechaNacimientoHijos' + i).css('border-color', '#ababab');
        }
        if ($.isEmptyObject($(".txtNacionalidadHijos" + i).val()) == true || $(".txtNacionalidadHijos" + i).val() == "") {
            textoError += "<p>Ingrese Nacionalidad de " + nombreHijo + "</p>";
            $('.txtNacionalidadHijos' + i).css('border-color', 'red');
            retorna = false;
        } else {
            $('.txtNacionalidadHijos' + i).css('border-color', '#ababab');
        }
        if ($.isEmptyObject($("#txtCuilHijos" + i).val()) == true || $("#txtCuilHijos" + i).val() == "") {
            textoError += "<p>Ingrese CUIL de " + nombreHijo + "</p>";
            $("#txtCuilHijos" + i).css('border-color', 'red');
            retorna = false;
        } else {
            $("#txtCuilHijos" + i).css('border-color', '#ababab');
        }
    }

    if (retorna == false) {
        $("#alertaCamposObligatorios").css('display', 'block');
        $("#alertaCamposObligatorios").html(textoError);
        return false;
    }
    else {
        //return true;
        $("#alertaCamposObligatorios").css('display', 'none');

        $("#pestaniaGrupoFamiliar").removeClass("active");
        $("#pestaniaEstudiosCursados").addClass("active");

        $("#estudiosCursados").removeClass("fade");
        $("#estudiosCursados").addClass("active");
        $("#grupoFamiliar").removeClass("active");
        $("#grupoFamiliar").addClass("fade");

        $("#btnGroupGrupoFliar").css('display', 'none');
        $("#btnGroupEstudios").css('display', 'inline-flex');
    }
}
function validaCamposEstudios() {
    var retorna = true;
    var textoError = '';
    var estudiosPrimarios = $(".cboEstuidosPrimarios").val();
    var intitucionPrimarios = $(".txtInstitucionPrimarios").val();
    var tituloPrimario = $(".txtTituloPrimarios").val();
    var estudiosSecundarios = $(".cboEstuidosSecundarios").val();
    var intitucionSecundarios = $(".txtInstitucionSecundarios").val();
    var tituloSecundarios = $(".txtTituloSecundarios").val();
    var estudiosTerciarios = $(".cboEstuidosTerciarios").val();
    var intitucionTerciarios = $(".txtInstitucionTerciarios").val();
    var tituloTerciarios = $(".txtTituloTerciarios").val();
    var estudiosUniversitarios = $(".cboEstuidosUniversitarios").val();
    var intitucionUniversitarios = $(".txtInstitucionUniversitarios").val();
    var tituloUniversitarios = $(".txtTituloUniversitarios").val();

    if (estudiosPrimarios == 'Seleccione') {
        textoError += "<p>Seleccione estado Estudios Primarios</p>";
        $(".cboEstuidosPrimarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboEstuidosPrimarios").css('border-color', '#ababab');
    }

    if ($.isEmptyObject(intitucionPrimarios) == true || intitucionPrimarios == "") {
        textoError += "<p>Ingrese Institución Estudios Primarios</p>";
        $(".txtInstitucionPrimarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtInstitucionPrimarios").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(tituloPrimario) == true || tituloPrimario == "") {
        textoError += "<p>Ingrese Título Estudios Primarios</p>";
        $(".txtTituloPrimarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtTituloPrimarios").css('border-color', '#ababab');
    }
    if (estudiosSecundarios == 'Seleccione') {
        textoError += "<p>Seleccione estado Estudios Secundarios</p>";
        $(".cboEstuidosSecundarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboEstuidosSecundarios").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(intitucionSecundarios) == true || intitucionSecundarios == "") {
        textoError += "<p>Ingrese Institución Estudios Secundarios</p>";
        $(".txtInstitucionSecundarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtInstitucionSecundarios").css('border-color', '#ababab');
    }
    if ($.isEmptyObject(tituloSecundarios) == true || tituloSecundarios == "") {
        textoError += "<p>Ingrese Título Estudios Secundarios</p>";
        $(".txtTituloSecundarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".txtTituloSecundarios").css('border-color', '#ababab');
    }
    if (estudiosTerciarios == 'Seleccione') {
        textoError += "<p>Seleccione estado Estudios Terciarios</p>";
        $(".cboEstuidosTerciarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboEstuidosTerciarios").css('border-color', '#ababab');
    }
    if (estudiosTerciarios != 'NO CORRESPONDE') {
        if ($.isEmptyObject(intitucionTerciarios) == true || intitucionTerciarios == "") {
            textoError += "<p>Ingrese Institución Estudios Terciarios</p>";
            $(".txtInstitucionTerciarios").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtInstitucionTerciarios").css('border-color', '#ababab');
        }
        if ($.isEmptyObject(tituloTerciarios) == true || tituloTerciarios == "") {
            textoError += "<p>Ingrese Título Estudios Terciarios</p>";
            $(".txtTituloTerciarios").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtTituloTerciarios").css('border-color', '#ababab');
        }
    }
    if (estudiosUniversitarios == 'Seleccione') {
        textoError += "<p>Seleccione estado Estudios Universitarios</p>";
        $(".cboEstuidosUniversitarios").css('border-color', 'red');
        retorna = false;
    } else {
        $(".cboEstuidosUniversitarios").css('border-color', '#ababab');
    }
    if (estudiosUniversitarios != 'NO CORRESPONDE') {
        if ($.isEmptyObject(intitucionUniversitarios) == true || intitucionUniversitarios == "") {
            textoError += "<p>Ingrese Institución Estudios Universitarios</p>";
            $(".txtInstitucionUniversitarios").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtInstitucionUniversitarios").css('border-color', '#ababab');
        }
        if ($.isEmptyObject(tituloUniversitarios) == true || tituloUniversitarios == "") {
            textoError += "<p>Ingrese Título Estudios Universitarios</p>";
            $(".txtTituloUniversitarios").css('border-color', 'red');
            retorna = false;
        } else {
            $(".txtTituloUniversitarios").css('border-color', '#ababab');
        }
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
function volverAPerfil() {
    $("#alertaCamposObligatorios").css('display', 'none');


    $("#pestaniaPerfil").addClass("active");
    $("#pestaniaDatosPersonales").removeClass("active");

    $("#perfil").removeClass("fade");
    $("#perfil").addClass("active");
    $("#datosPersonales").removeClass("active");
    $("#datosPersonales").addClass("fade");

    $("#btnGroupPerfil").css('display', 'inline-flex');
    $("#btnGroupDatosPersonales").css('display', 'none');
}
function volverADatosPersonales() {
    $("#alertaCamposObligatorios").css('display', 'none');


    $("#pestaniaDatosPersonales").addClass("active");
    $("#pestaniaEstructura").removeClass("active");

    $("#datosPersonales").removeClass("fade");
    $("#datosPersonales").addClass("active");
    $("#estructura").removeClass("active");
    $("#estructura").addClass("fade");

    $("#btnGroupDatosPersonales").css('display', 'inline-flex');
    $("#btnGroupEstructura").css('display', 'none');
}
function volverAEstructura() {
    $("#alertaCamposObligatorios").css('display', 'none');

    $("#pestaniaEstructura").addClass("active");
    $("#pestaniaGrupoFamiliar").removeClass("active");

    $("#estructura").removeClass("fade");
    $("#estructura").addClass("active");
    $("#grupoFamiliar").removeClass("active");
    $("#grupoFamiliar").addClass("fade");

    $("#btnGroupEstructura").css('display', 'inline-flex');
    $("#btnGroupGrupoFliar").css('display', 'none');
}
function volverAGrupoFliar() {
    $("#alertaCamposObligatorios").css('display', 'none');

    $("#pestaniaGrupoFamiliar").addClass("active");
    $("#pestaniaEstudiosCursados").removeClass("active");

    $("#grupoFamiliar").removeClass("fade");
    $("#grupoFamiliar").addClass("active");
    $("#estudiosCursados").removeClass("active");
    $("#estudiosCursados").addClass("fade");

    $("#btnGroupGrupoFliar").css('display', 'inline-flex');
    $("#btnGroupEstudios").css('display', 'none');
}
function MensajeGuarda() {
    $(".modal").css('display', 'block');
    $(".modal-body").text('La información se guardó correctamente.');
    $(".modalBtnRRHH").css('display', 'none');
    $(".modalBtnCerrar").css('display', 'none');
}
function MensajeModificado(sec) {
    $(".modal").css('display', 'block');
    $(".modal-body").text('La información se modificó correctamente.');
    if (sec == '1') {
        $(".modalBtnUsuario").css('display', 'none');
        $(".modalBtnCerrar").css('display', 'none');
    }
    if (sec == '2') {
        $(".modalBtnRRHH").css('display', 'none');
        $(".modalBtnCerrar").css('display', 'none');
    }
}
function cerrar() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/Lists/DatosPersonal.aspx';
}
function cerrarHome() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/SitePages/HOME.aspx';
}
function cerrarRRHH() {
    location.href = window.location.protocol + '//' + window.location.hostname + '/INFO/RRHH.aspx';
}
$(function () {
    $(".cboEstadoCivil").on("change", function () {
        $('.divConyuge').hide();
        if ($(this).val() == "Casado/a") {
            $('.divConyuge').show();
            $('#txtConyugeConcubinato').html('Cónyuge')
        }
        if ($(this).val() == "Concubinato") {
            $('.divConyuge').show();
            $('#txtConyugeConcubinato').html('Conviviente')
        }
    });
    $(".cboAlergias").on("change", function () {
        if ($(this).val() == "SI") {
            $('.divAlergiaNO').show();
        };
        if ($(this).val() == "NO") {
            $('.divAlergiaNO').hide();
            $('.txtEspecificar').html('');
        };
    });
    $(".cboLicenciaConducir").on("change", function () {
        if ($(this).val() == "SI") {
            $('.divLicenciaNO').show();
        };
        if ($(this).val() == "NO") {
            $('.divLicenciaNO').hide();
            $('.txtCategoria').html('');
        };
    });
    $(".cboCantidadHijos").on("change", function () {
        if ($(this).val() != "0") {
            $('.divHijosTitulo').show();
            if ($(this).val() == "1") {
                $('.divHijos1').show();
                $('.divHijos2').hide();
                $('.divHijos3').hide();
                $('.divHijos4').hide();
                $('.divHijos5').hide();
                $('.divHijos6').hide();
            }
            if ($(this).val() == "2") {
                $('.divHijos1').show();
                $('.divHijos2').show();
                $('.divHijos3').hide();
                $('.divHijos4').hide();
                $('.divHijos5').hide();
                $('.divHijos6').hide();
            }
            if ($(this).val() == "3") {
                $('.divHijos1').show();
                $('.divHijos2').show();
                $('.divHijos3').show();
                $('.divHijos4').hide();
                $('.divHijos5').hide();
                $('.divHijos6').hide();
            }
            if ($(this).val() == "4") {
                $('.divHijos1').show();
                $('.divHijos2').show();
                $('.divHijos3').show();
                $('.divHijos4').show();
                $('.divHijos5').hide();
                $('.divHijos6').hide();
            }
            if ($(this).val() == "5") {
                $('.divHijos1').show();
                $('.divHijos2').show();
                $('.divHijos3').show();
                $('.divHijos4').show();
                $('.divHijos5').show();
                $('.divHijos6').hide();
            }
            if ($(this).val() == "6") {
                $('.divHijos1').show();
                $('.divHijos2').show();
                $('.divHijos3').show();
                $('.divHijos4').show();
                $('.divHijos5').show();
                $('.divHijos6').show();
            }
        }
        else {
            $('.divHijos').hide();
        }
    });

    $(".cboEstuidosPrimarios").on("change", function () {
        $('#divAdjuntoPrimarios').hide();
        if ($(this).val() == "CURSANDO") {
            $('#divAdjuntoPrimarios').show();
            $('#txtPrimarios').html('Adjuntar constancia alumno regular')
        }
        if ($(this).val() == "COMPLETO") {
            $('#divAdjuntoPrimarios').show();
            $('#txtPrimarios').html('Adjuntar constancia estudios completo')
        }
    });
    $(".cboEstuidosSecundarios").on("change", function () {
        $('#divAdjuntoSecundarios').hide();
        if ($(this).val() == "CURSANDO") {
            $('#divAdjuntoSecundarios').show();
            $('#txtSecundarios').html('Adjuntar constancia alumno regular')
        }
        if ($(this).val() == "COMPLETO") {
            $('#divAdjuntoSecundarios').show();
            $('#txtSecundarios').html('Adjuntar constancia estudios completo')
        }
    });
    $(".cboEstuidosTerciarios").on("change", function () {
        $('#divAdjuntoTerciarios').hide();
        if ($(this).val() == "CURSANDO") {
            $('#divAdjuntoTerciarios').show();
            $('#txtTerciarios').html('Adjuntar constancia alumno regular')
        }
        if ($(this).val() == "COMPLETO") {
            $('#divAdjuntoTerciarios').show();
            $('#txtTerciarios').html('Adjuntar constancia estudios completo')
        }
        if ($(this).val() == "NO CORRESPONDE") {
            $('#rowTerciario').hide();
        } else {
            $('#rowTerciario').show();
        }
    });
    $(".cboEstuidosUniversitarios").on("change", function () {
        $('#divAdjuntoUniversitarios').hide();
        if ($(this).val() == "CURSANDO") {
            $('#divAdjuntoUniversitarios').show();
            $('#txtUniversitarios').html('Adjuntar constancia alumno regular')
        }
        if ($(this).val() == "COMPLETO") {
            $('#divAdjuntoUniversitarios').show();
            $('#txtUniversitarios').html('Adjuntar constancia estudios completo')
        }
        if ($(this).val() == "NO CORRESPONDE") {
            $('#rowUniversitario').hide();
        } else {
            $('#rowUniversitario').show();
        }
    });
    $(".cboCursos").on("change", function () {
        $('#divAdjuntoCursos').hide();
        if ($(this).val() == "COMPLETO") {
            $('#divAdjuntoCursos').show();
            $('#txtSecundarios').html('Adjuntar constancia curso completo')
        }
    });
});
function fotoPerfilVer() {
    if ($("#FotoPerfil").is(':visible')) {
        $("#FotoPerfil").css('display', 'none');
    }
    else {
        $("#FotoPerfil").css('display', 'block');
    }
};

function mensajeCerrar() {
    $(".modal").css('display', 'block');
    $(".modal-body").text('Si confirma se perderán los datos no guardados.');
    $(".modalBtnRRHH").css('display', 'none');
    $(".modalBtnUsuario").css('display', 'none');
};
function agregarCurso(divId, botonId) {
    var div = document.getElementById(divId);
    var boton = document.getElementById(botonId);
    if (div.style.display === 'none' || div.style.display === '') {
        div.style.display = 'block'; // Mostrar el div
        boton.style.display = 'none'
    } 
}

function quitarCurso(divId, botonId) {
    var div = document.getElementById(divId);
    var boton = document.getElementById(botonId);
    if (div.style.display === 'block' || div.style.display === '') {
        div.style.display = 'none'; // Mostrar el div
        boton.style.display = 'block'
    } 
}
function cerrarModal() {
    $(".modal").css('display', 'none');
}