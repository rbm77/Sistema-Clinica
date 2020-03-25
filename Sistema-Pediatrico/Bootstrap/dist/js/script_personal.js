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





function CargarDatos(input, datos) {

    var seleccionar = document.createElement('option');

    seleccionar.appendChild(document.createTextNode("Seleccionar..."));

    seleccionar.value = "nulo";

    input.appendChild(seleccionar);

    input.value = "nulo";

    $.each(datos, function (key, val) {

        var option = document.createElement('option');

        option.appendChild(document.createTextNode(val));

        option.value = key;

        input.appendChild(option);

    });

}

function ObtenerCantones(provincia) {

    var codigoProvincia = provincia.value;
    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/cantones.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {
 
            if (status == "success") {

                var canton = document.getElementById("inputCanton");
                var distrito = document.getElementById("inputDistrito");

                var lengthC = canton.options.length;
                for (i = lengthC - 1; i >= 0; i--) {
                    canton.options[i] = null;
                }

                var lengthD = distrito.options.length;
                for (i = lengthD - 1; i >= 0; i--) {
                    distrito.options[i] = null;
                }

                CargarDatos(canton, response);
            }
        },
    });
}

function ObtenerDistritos(canton) {

    var codigoCanton = canton.value;
    var codigoProvincia = document.getElementById("inputProvincia").value;

    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/canton/' + codigoCanton + '/distritos.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {

            if (status == "success") {

                var distrito = document.getElementById("inputDistrito");

                var lengthD = distrito.options.length;
                for (i = lengthD - 1; i >= 0; i--) {
                    distrito.options[i] = null;
                }

                CargarDatos(distrito, response);
            }
        },
    });
}

function ObtenerProvincias() {

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: 'https://ubicaciones.paginasweb.cr/provincias.json',
        success: function (response, status) {

            if (status == "success") {

                var provincia = document.getElementById("inputProvincia");

                var length = provincia.options.length;
                for (i = length - 1; i >= 0; i--) {
                    provincia.options[i] = null;
                }

                CargarDatos(provincia, response);
            }
        },
    });
}

