function Actualizar(rol) {
    var rolSeleccionado = rol.value;
    if (rolSeleccionado == "medico") {

        document.getElementById("inputCodigoMedico").disabled = false;
        document.getElementById("inputEspecialidad").disabled = false;
        document.getElementById("inputCodigoAsistente").disabled = true;

    } else if (rolSeleccionado == "asistente") {

        document.getElementById("inputCodigoMedico").disabled = true;
        document.getElementById("inputEspecialidad").disabled = true;
        document.getElementById("inputCodigoAsistente").disabled = false;

    } else if (rolSeleccionado == "administrador") {

        document.getElementById("inputCodigoMedico").disabled = true;
        document.getElementById("inputEspecialidad").disabled = true;
        document.getElementById("inputCodigoAsistente").disabled = true;
    }
    else if (rolSeleccionado == "nulo") {

        document.getElementById("inputCodigoMedico").disabled = true;
        document.getElementById("inputEspecialidad").disabled = true;
        document.getElementById("inputCodigoAsistente").disabled = true;

    }
}