<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="consulta.aspx.cs" Inherits="Sistema_Pediatrico.consulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedorExp">

        <h3 runat="server" id="Titulo" class="titulo"></h3>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <div id="contenedorDatos" runat="server">

            <form id="formularioConsulta" class="needs-validation"
                novalidate style="margin-top: 15px" runat="server">

                <div class="form-row">
                    <div class="form-group col-md-3 col-sm-3">
                        <asp:Label ID="labelFecha" runat="server" Text="Fecha"></asp:Label>
                        <asp:TextBox ID="fecha" runat="server" class="form-control" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
                        <small class="form-text text-muted">Formato: dd/mm/aaaa</small>
                    </div>

                    <div class="form-group col-md-3 col-sm-3">

                        <asp:Label ID="labelHora" runat="server" Text="Hora"></asp:Label>

                        <div class="input-group">

                            <asp:TextBox ID="hora" runat="server" class="form-control" ClientIDMode="Static" pattern="^(?:0?[1-9]|1[0-2])[:][0-5][0-9]"></asp:TextBox>
                            <div class="input-group-append">
                                <select class="form-control medioDia" runat="server" id="medioDia">
                                    <option selected>am</option>
                                    <option>pm</option>
                                </select>
                            </div>
                        </div>
                        <small class="form-text text-muted">Formato: hh:mm</small>
                    </div>

                </div>

                <div class="form-row">
                    <div class="form-group col-md-12 col-sm-12">
                        <asp:Label ID="labelPadecimientoActual" runat="server" Text="Padecimiento Actual"></asp:Label>
                        <textarea class="form-control" runat="server" clientidmode="Static" id="padecimientoActual" rows="3"></textarea>
                    </div>
                </div>

                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <a class="nav-item nav-link active" id="examenFisicoTab" data-toggle="tab" href="#examenFisico" role="tab" aria-controls="nav-examenFisico" aria-selected="true">Examen Físico</a>
                        <a class="nav-item nav-link" id="conclusionesTab" data-toggle="tab" href="#conclusiones" role="tab" aria-controls="nav-conclusiones" aria-selected="false">Conclusiones</a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">

                    <%-- DATOS DE EXAMEN FISICO --%>

                    <div class="tab-pane fade show active padding-tab-exp" id="examenFisico" role="tabpanel" aria-labelledby="nav-examenFisico-tab">

                        <div class="form-row">

                            <div class="form-group col-12">
                                <small class="form-text text-muted">Utilice una coma ( , ) en caso de ingresar valores decimales.</small>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPeso" runat="server" Text="Peso"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="peso" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">kg</span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelTalla" runat="server" Text="Talla"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="talla" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">cm</span>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPerimetroCefalico" runat="server" Text="Perímetro Cefálico"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="perimetroCefalico" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">cm</span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelIMC" runat="server" Text="IMC"></asp:Label>
                                <asp:TextBox ID="IMC" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelTemperatura" runat="server" Text="Temperatura"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="temperatura" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">°C</span>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelSO2" runat="server" Text="Saturación de Oxígeno"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="SO2" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">%</span>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <hr />


                        <div class="form-row">

                            <h5 class="titulo col-12">Dirección</h5>


                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelProvinciaPaciente" runat="server" ClientIDMode="Static" Text="Provincia"></asp:Label>
                                <asp:DropDownList onchange="ObtenerCantonesPaciente(this)" ID="inputProvinciaPaciente" runat="server" class="form-control" ClientIDMode="Static" required>
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="provinciaPValue" value="nulo" runat="server" />
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelCantonPaciente" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                                <asp:DropDownList onchange="ObtenerDistritosPaciente(this)" ID="inputCantonPaciente" runat="server" class="form-control" ClientIDMode="Static" required>
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="cantonPValue" value="nulo" runat="server" />
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelDistritosPaciente" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                                <asp:DropDownList onchange="AsignarDistritoPaciente(this)" ID="inputDistritoPaciente" runat="server" class="form-control" ClientIDMode="Static" required>
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="distritoPValue" value="nulo" runat="server" />
                            </div>

                        </div>

                        <div class="form-row">

                            <div class="form-group col-md-6 col-sm-12">
                                <asp:Label ID="labelDireccionExactaPaciente" runat="server" Text="Dirección exacta"></asp:Label>
                                <textarea class="form-control txtArea-size" runat="server" clientidmode="Static" id="direccionExactaPaciente" rows="2"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-12">
                                <asp:Label ID="labelURLExpediente" runat="server" Text="URL vinculante a expediente antiguo"></asp:Label>
                                <textarea class="form-control txtArea-size" clientidmode="Static" runat="server" id="urlExpedienteAntiguoPaciente" rows="2"></textarea>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelFechaActual" runat="server" Text="Fecha Creación"></asp:Label>
                                <asp:TextBox ID="fechaActual" runat="server" class="form-control" placeholder="formato: dd/mm/aaaa" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
                            </div>
                            <%--                        <div class="form-group col-md-8 col-sm-12">
                            <asp:Label ID="label14" runat="server" Text="Foto del paciente"></asp:Label>
                            <div class="custom-file">
                                <asp:FileUpload class="custom-file-input" ID="inputFotoPaciente" runat="server" ClientIDMode="Static" />
                                <label class="custom-file-label" for="inputFotoPaciente">Seleccionar archivo...</label>
                            </div>
                        </div>--%>
                        </div>


                    </div>

                    <%-- DATOS DE CONCLUSIONES --%>

                    <div class="tab-pane fade padding-tab-exp" id="conclusiones" role="tabpanel" aria-labelledby="nav-conclusiones-tab">

                        <div class="form-row">
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelCedulaEncargado" runat="server" Text="Cédula"></asp:Label>
                                <asp:TextBox placeholder="Formato: #0###0###" ID="cedulaEncargado" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelNombreEncargado" runat="server" Text="Nombre"></asp:Label>
                                <asp:TextBox ID="nombreEncargado" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelPrimerApellidoEncargado" runat="server" Text="Primer Apellido"></asp:Label>
                                <asp:TextBox ID="primerApellidoEncargado" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelSegundoApellidoEncargado" runat="server" Text="Segundo Apellido"></asp:Label>
                                <asp:TextBox ID="segundoApellidoEncargado" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelParentesco" runat="server" Text="Parentesco"></asp:Label>
                                <asp:TextBox ID="parentesco" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelTelefonoEncargado" runat="server" Text="Teléfono"></asp:Label>
                                <asp:TextBox ID="inputTelefonoEncargado" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-6 col-sm-4">
                                <asp:Label ID="labelCorreoEncargado" runat="server" Text="Correo Electrónico"></asp:Label>
                                <asp:TextBox ID="inputCorreoEncargado" type="email" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <hr />

                        <div class="form-row">

                            <h5 class="titulo col-12">Dirección</h5>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="label1" runat="server" ClientIDMode="Static" Text="Provincia"></asp:Label>
                                <asp:DropDownList onchange="ObtenerCantonesEncargado(this)" ID="inputProvinciaEncargado" runat="server" class="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="provinciaEValue" value="nulo" runat="server" />
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="label2" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                                <asp:DropDownList onchange="ObtenerDistritosEncargado(this)" ID="inputCantonEncargado" runat="server" class="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="cantonEValue" value="nulo" runat="server" />
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="label3" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                                <asp:DropDownList onchange="AsignarDistritoEncargado(this)" ID="inputDistritoEncargado" runat="server" class="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="distritoEValue" value="nulo" runat="server" />
                            </div>

                        </div>

                        <div class="form-row">

                            <div class="form-group col-md-12 col-sm-12">
                                <asp:Label ID="labelDireccionExactaEncargado" runat="server" Text="Dirección exacta"></asp:Label>
                                <textarea class="form-control txtArea-size" runat="server" clientidmode="Static" id="direccionExactaEncargado" rows="2"></textarea>
                            </div>
                        </div>

                        <hr />

                        <div class="form-row">

                            <h6 class="titulo col-12">El encargado será:</h6>


                            <div class="form-group col-12" style="padding-top: 15px">
                                <div class="form-check form-check-inline" style="padding-left: 15px">
                                    <input class="form-check-input" type="checkbox" onclick="esDestinatarioFactura()" id="esDestinatario" runat="server" clientidmode="Static">
                                    <label class="form-check-label" for="inlineCheckbox1">
                                        El destinatario al cual se le remitirá la factura electrónica al finalizar la consulta médica
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-12">
                                <div class="form-check form-check-inline" style="padding-left: 15px">
                                    <input class="form-check-input" type="checkbox" onclick="esSolicitanteCita()" id="esSolicitante" runat="server" clientidmode="Static">
                                    <label class="form-check-label" for="inlineCheckbox2">
                                        La persona autorizada para solicitar las citas médicas del paciente
                                    </label>
                                </div>
                            </div>

                        </div>


                        <div id="datosSolicitanteCita" clientidmode="Static" runat="server">

                            <hr />

                            <div class="form-row">

                                <h5 class="titulo col-12" style="margin-bottom: 15px">Datos del Solicitante de las Citas Médicas</h5>

                                <div class="form-group col-md-4 col-sm-4">
                                    <asp:Label ID="labelCedulaSolicitante" runat="server" Text="Cédula"></asp:Label>
                                    <asp:TextBox placeholder="Formato: #0###0###" ID="cedulaSolicitante" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-4">
                                    <asp:Label ID="labelCorreoSolicitante" runat="server" Text="Correo Electrónico"></asp:Label>
                                    <asp:TextBox ID="correoSolicitante" type="email" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-sm-4">
                                    <asp:Label ID="labelTelefonoSolicitante" runat="server" Text="Teléfono"></asp:Label>
                                    <asp:TextBox ID="telefonoSolicitante" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]*"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                    </div>




                    <div class="row justify-content-center" style="margin-top: 30px">
                        <div class="col-md-2 col-sm-5">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-general btn-guardar" />
                        </div>
                        <div class="col-md-2 col-sm-5">
                            <a href="expedientes.aspx" class="btn btn-general btn-regresar">Regresar</a>
                        </div>
                    </div>


                </div>


            </form>

        </div>


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
                esSolicitanteCita();
                esDestinatarioFactura();
            }, false);
        })();

    </script>


</asp:Content>
