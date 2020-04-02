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

function ObtenerCantonesPaciente(provincia) {

    var codigoProvincia = provincia.value;
    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/cantones.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {
 
            if (status == "success") {

                var canton = document.getElementById("inputCantonPaciente");
                var distrito = document.getElementById("inputDistritoPaciente");

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

function ObtenerDistritosPaciente(canton) {

    var codigoCanton = canton.value;
    var codigoProvincia = document.getElementById("inputProvinciaPaciente").value;

    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/canton/' + codigoCanton + '/distritos.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {

            if (status == "success") {

                var distrito = document.getElementById("inputDistritoPaciente");

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

                var provinciaPaciente = document.getElementById("inputProvinciaPaciente");

                var length = provinciaPaciente.options.length;
                for (i = length - 1; i >= 0; i--) {
                    provinciaPaciente.options[i] = null;
                }

                CargarDatos(provinciaPaciente, response);

                var provinciaEncargado = document.getElementById("inputProvinciaEncargado");

                var length = provinciaEncargado.options.length;
                for (i = length - 1; i >= 0; i--) {
                    provinciaEncargado.options[i] = null;
                }

                CargarDatos(provinciaEncargado, response);

                var provinciaDestinatario = document.getElementById("inputProvinciaDestinatario");

                var length = provinciaDestinatario.options.length;
                for (i = length - 1; i >= 0; i--) {
                    provinciaDestinatario.options[i] = null;
                }

                CargarDatos(provinciaDestinatario, response);
            }
        },
    });
}


function ObtenerCantonesEncargado(provincia) {

    var codigoProvincia = provincia.value;
    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/cantones.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {

            if (status == "success") {

                var canton = document.getElementById("inputCantonEncargado");
                var distrito = document.getElementById("inputDistritoEncargado");

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

function ObtenerDistritosEncargado(canton) {

    var codigoCanton = canton.value;
    var codigoProvincia = document.getElementById("inputProvinciaEncargado").value;

    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/canton/' + codigoCanton + '/distritos.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {

            if (status == "success") {

                var distrito = document.getElementById("inputDistritoEncargado");

                var lengthD = distrito.options.length;
                for (i = lengthD - 1; i >= 0; i--) {
                    distrito.options[i] = null;
                }

                CargarDatos(distrito, response);
            }
        },
    });
}


function ObtenerCantonesDestinatario(provincia) {

    var codigoProvincia = provincia.value;
    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/cantones.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {

            if (status == "success") {

                var canton = document.getElementById("inputCantonDestinatario");
                var distrito = document.getElementById("inputDistritoDestinatario");

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

function ObtenerDistritosDestinatario(canton) {

    var codigoCanton = canton.value;
    var codigoProvincia = document.getElementById("inputProvinciaDestinatario").value;

    var destino = 'https://ubicaciones.paginasweb.cr/provincia/' + codigoProvincia + '/canton/' + codigoCanton + '/distritos.json'

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: destino,
        success: function (response, status) {

            if (status == "success") {

                var distrito = document.getElementById("inputDistritoDestinatario");

                var lengthD = distrito.options.length;
                for (i = lengthD - 1; i >= 0; i--) {
                    distrito.options[i] = null;
                }

                CargarDatos(distrito, response);
            }
        },
    });
}