<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="expediente.aspx.cs" Inherits="Sistema_Pediatrico.expediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Bootstrap/dist/js/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid padding-contenedorExp">

        <h3 class="titulo">Expediente</h3>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>


        <form id="formularioExpediente" class="needs-validation"
            novalidate style="margin-top: 15px" runat="server">


            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="datosPacienteTab" data-toggle="tab" href="#datosPaciente" role="tab" aria-controls="nav-paciente" aria-selected="true">Paciente</a>
                    <a class="nav-item nav-link" id="datosEncargadoTab" data-toggle="tab" href="#datosEncargado" role="tab" aria-controls="nav-encargado" aria-selected="false">Encargado</a>
                    <a class="nav-item nav-link" id="datosDestinatarioTab" data-toggle="tab" href="#datosDestinatario" role="tab" aria-controls="nav-destinatario" aria-selected="false">Destinatario de Factura</a>
                    <a class="nav-item nav-link" id="datosSolicitanteTab" data-toggle="tab" href="#datosSolicitante" role="tab" aria-controls="nav-solicitante" aria-selected="false">Solicitante de Cita</a>
                    <a class="nav-item nav-link" id="historiaClinicaTab" data-toggle="tab" href="#historiaClinica" role="tab" aria-controls="nav-historia" aria-selected="false">Historia Clínica</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active padding-tab-exp" id="datosPaciente" role="tabpanel" aria-labelledby="nav-paciente-tab">


                    <div class="form-row">
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label1" runat="server" Text="Cédula"></asp:Label>
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label2" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="TextBox2" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label3" runat="server" Text="Primer Apellido"></asp:Label>
                            <asp:TextBox ID="TextBox3" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label4" runat="server" Text="Segundo Apellido"></asp:Label>
                            <asp:TextBox ID="TextBox4" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label9" runat="server" Text="Fecha de Nacimiento"></asp:Label>
                            <asp:TextBox ID="TextBox9" runat="server" class="form-control" placeholder="formato: dd/mm/aaaa" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label10" runat="server" Text="Sexo"></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" ClientIDMode="Static" required>
                                <asp:ListItem Selected="True" Value="nulo">Seleccionar...</asp:ListItem>
                                <asp:ListItem Selected="False" Value="masculino">Masculino</asp:ListItem>
                                <asp:ListItem Selected="False" Value="femenino">Femenino</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                    <hr />


                    <div class="form-row">

                        <h5 class="titulo col-12">Dirección</h5>

                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="labelProvincia" runat="server" ClientIDMode="Static" Text="Provincia"></asp:Label>
                            <select onchange="ObtenerCantones(this)" class="custom-select" id="inputProvincia" clientidmode="Static" runat="server" required>
                            </select>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="labelCanton" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                            <select onchange="ObtenerDistritos(this)" class="custom-select" id="inputCanton" clientidmode="Static" runat="server" required>
                            </select>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="labelDistritos" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                            <select class="custom-select" id="inputDistrito" clientidmode="Static" runat="server" required>
                            </select>
                        </div>

                    </div>

                    <div class="form-row">

                        <div class="form-group col-md-6 col-sm-12">
                            <asp:Label ID="label12" runat="server" Text="Dirección exacta"></asp:Label>
                            <textarea class="form-control txtArea-size" id="exampleFormControlTextarea6" rows="2"></textarea>
                        </div>

                        <div class="form-group col-md-6 col-sm-12">
                            <asp:Label ID="label11" runat="server" Text="URL vinculante a expediente antiguo"></asp:Label>
                            <textarea class="form-control txtArea-size" id="exampleFormControlTextarea7" rows="2"></textarea>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label13" runat="server" Text="Fecha actual"></asp:Label>
                            <asp:TextBox ID="TextBox10" runat="server" class="form-control" placeholder="formato: dd/mm/aaaa" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-9 col-sm-12">
                            <asp:Label ID="label14" runat="server" Text="Foto del paciente"></asp:Label>
                            <div class="custom-file">
                                <input type="file" class="custom-file-input" id="inputFotoPaciente" lang="es">
                                <label class="custom-file-label" for="inputFotoPaciente">Seleccionar archivo...</label>
                            </div>
                        </div>
                    </div>


                </div>


                <div class="tab-pane fade padding-tab-exp" id="datosEncargado" role="tabpanel" aria-labelledby="nav-encargado-tab">

                    <div class="form-row">
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label5" runat="server" Text="Cédula"></asp:Label>
                            <asp:TextBox ID="TextBox5" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label6" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="TextBox6" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label7" runat="server" Text="Primer Apellido"></asp:Label>
                            <asp:TextBox ID="TextBox7" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3 col-sm-4">
                            <asp:Label ID="label8" runat="server" Text="Segundo Apellido"></asp:Label>
                            <asp:TextBox ID="TextBox8" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="tab-pane fade" id="datosDestinatario" role="tabpanel" aria-labelledby="nav-destinatario-tab">
                </div>
                <div class="tab-pane fade" id="datosSolicitante" role="tabpanel" aria-labelledby="nav-solicitante-tab">
                </div>
                <div class="tab-pane fade" id="historiaClinica" role="tabpanel" aria-labelledby="nav-historia-tab">
                </div>
            </div>


            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <hr />

            <br />


            <br />
            <br />
            <br />


            <div class="form-row">
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelCedula" runat="server" Text="Cédula"></asp:Label>
                    <asp:TextBox ID="inputCedula" runat="server" class="form-control" ClientIDMode="Static" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="inputNombre" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelPrimerApellido" runat="server" Text="Primer Apellido"></asp:Label>
                    <asp:TextBox ID="inputPrimerApellido" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelSegundoApellido" runat="server" Text="Segundo Apellido"></asp:Label>
                    <asp:TextBox ID="inputSegundoApellido" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelTelefono" runat="server" Text="Teléfono"></asp:Label>
                    <asp:TextBox ID="inputTelefono" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]*" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelCorreo" runat="server" Text="Correo Electrónico"></asp:Label>
                    <asp:TextBox ID="inputCorreo" type="email" runat="server" class="form-control" ClientIDMode="Static" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelContrasenna" runat="server" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="inputContrasenna" type="password" runat="server" class="form-control" ClientIDMode="Static" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelConfirmar" runat="server" Text="Confirmar Contraseña"></asp:Label>
                    <asp:TextBox ID="inputConfirmar" type="password" runat="server" class="form-control" ClientIDMode="Static" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelRol" runat="server" ClientIDMode="Static" Text="Rol"></asp:Label>
                    <select onchange="Actualizar(this)" class="custom-select" id="inputRol" runat="server" required>
                        <option value="nulo" selected>Seleccionar...</option>
                        <option value="medico">Médico</option>
                        <option value="asistente">Asistente</option>
                        <option value="administrador">Administrador</option>
                    </select>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelCodAsistente" runat="server" Text="Código de Asistente" data-toggle="tooltip" data-placement="top" title="Debe ingresar el código de médico al cual asiste"></asp:Label>
                    <asp:DropDownList ID="inputCodigoAsistente" runat="server" class="form-control" ClientIDMode="Static" data-toggle="tooltip" data-placement="top" title="Debe ingresar el código de médico al cual asiste" required>
                        <asp:ListItem Selected="True" Value="nulo">Seleccionar...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelCodigoMedico" runat="server" Text="Código de Médico"></asp:Label>
                    <asp:TextBox ID="inputCodigoMedico" runat="server" class="form-control" ClientIDMode="Static" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelEspecialidad" runat="server" Text="Especialidad"></asp:Label>
                    <asp:TextBox ID="inputEspecialidad" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                </div>
            </div>

            <div class="row justify-content-center" style="margin-top: 30px">
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-guardar" />
                </div>
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnRegresar" runat="server" Text="Cancelar" CssClass="btn btn-regresar" />
                </div>
            </div>



            <%-- CODIGO DIRECCION, VA EN OTRA PAGINA --%>

            <div class="form-row">
            </div>


            <%-- HASTA AQUI LO DE DIRECCION --%>
        </form>




    </div>

    <script>
        // Example starter JavaScript for disabling form submissions if there are invalid fields
        (function () {
            'use strict';
            window.addEventListener('load', function () {
                // Fetch all the forms we want to apply custom Bootstrap validation styles to
                var forms = document.getElementsByClassName('needs-validation');
                // Loop over them and prevent submission
                var validation = Array.prototype.filter.call(forms, function (form) {
                    form.addEventListener('submit', function (event) {
                        if (form.checkValidity() === false) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        form.classList.add('was-validated');
                    }, false);
                });
                ObtenerProvincias();
            }, false);
        })();


        // Add the following code if you want the name of the file appear on select
        $(".custom-file-input").on("change", function () {
            var fileName = $(this).val().split("\\").pop();
            $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        });

    </script>






</asp:Content>
