function Actualizar(rol) {
    var rolSeleccionado = rol.value;
    if (rolSeleccionado == "medico") {

        document.getElementById("inputCodigoAsistente").value = "nulo";
        document.getElementById("inputCodigoMedico").disabled = false;
        document.getElementById("inputEspecialidad").disabled = false;
        document.getElementById("inputCodigoAsistente").disabled = true;
 
    } else if (rolSeleccionado == "asistente") {

        document.getElementById("inputCodigoMedico").value = "";
        document.getElementById("inputEspecialidad").value = "";
        document.getElementById("inputCodigoMedico").disabled = true;
        document.getElementById("inputEspecialidad").disabled = true;
        document.getElementById("inputCodigoAsistente").disabled = false;


    } else if (rolSeleccionado == "administrador") {
        document.getElementById("inputCodigoAsistente").value = "nulo";
        document.getElementById("inputCodigoMedico").value = "";
        document.getElementById("inputEspecialidad").value = "";
        document.getElementById("inputCodigoMedico").disabled = true;
        document.getElementById("inputEspecialidad").disabled = true;
        document.getElementById("inputCodigoAsistente").disabled = true;
    }
    else if (rolSeleccionado == "nulo") {

        document.getElementById("inputCodigoAsistente").value = "nulo";
        document.getElementById("inputCodigoMedico").value = "";
        document.getElementById("inputEspecialidad").value = ""
        document.getElementById("inputCodigoMedico").disabled = true;
        document.getElementById("inputEspecialidad").disabled = true;
        document.getElementById("inputCodigoAsistente").disabled = true;;
    }
}

function ValidarFormulario() {

    var cedula = document.getElementById("inputCedula").value;
    var nombre = document.getElementById("inputNombre").value;
    var primerApellido = document.getElementById("inputPrimerApellido").value;
    var segundoApellido = document.getElementById("inputSegundoApellido").value;
    var telefono = document.getElementById("inputTelefono").value;
    var correo = document.getElementById("inputCorreo").value;
    var contrasenna = document.getElementById("inputContrasenna").value;
    var confirmar = document.getElementById("inputConfirmar").value;
    var rol = document.getElementById("inputRol").value;
    var codigoAsistente = document.getElementById("inputCodigoAsistente").value;
    var codigoMedico = document.getElementById("inputCodigoMedico").value;
    var especialidad = document.getElementById("inputEspecialidad").value;

    if (cedula == null || cedula.length == 0 || /^\s+$/.test(cedula)
        || nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)
        || primerApellido == null || primerApellido.length == 0 || /^\s+$/.test(primerApellido)
        || segundoApellido == null || segundoApellido.length == 0 || /^\s+$/.test(segundoApellido)
        || telefono == null || telefono.length == 0 || /^\s+$/.test(telefono)
        || correo == null || correo.length == 0 || /^\s+$/.test(correo)
        || contrasenna == null || contrasenna.length == 0 || /^\s+$/.test(contrasenna)
        || confirmar == null || confirmar.length == 0 || /^\s+$/.test(confirmar)
        || rol == null || rol.length == 0 || /^\s+$/.test(rol)
        || codigoAsistente == null || codigoAsistente.length == 0 || /^\s+$/.test(codigoAsistente)
        || codigoMedico == null || codigoMedico.length == 0 || /^\s+$/.test(codigoMedico)
        || especialidad == null || especialidad.length == 0 || /^\s+$/.test(especialidad)) {
        alert("Esto es falso");
        return false;
    } else {
        alert("Esto es falso");
        return true;
    }
}
