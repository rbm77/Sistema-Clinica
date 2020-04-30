<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="expediente.aspx.cs" Inherits="Sistema_Pediatrico.expediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Bootstrap/dist/js/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid padding-contenedorExp">

        <h3 class="titulo">Expediente</h3>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <div id="contenedorDatos" runat="server">

            <form id="formularioExpediente" class="needs-validation"
                novalidate style="margin-top: 15px" runat="server">


                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <a class="nav-item nav-link active" id="datosPacienteTab" data-toggle="tab" href="#datosPaciente" role="tab" aria-controls="nav-paciente" aria-selected="true">Paciente</a>
                        <a class="nav-item nav-link" id="datosEncargadoTab" data-toggle="tab" href="#datosEncargado" role="tab" aria-controls="nav-encargado" aria-selected="false">Encargado</a>
                        <a class="nav-item nav-link" id="datosDestinatarioTab" data-toggle="tab" href="#datosDestinatario" role="tab" aria-controls="nav-destinatario" aria-selected="false">Destinatario de Factura</a>
                        <a class="nav-item nav-link" id="historiaClinicaTab" data-toggle="tab" href="#historiaClinica" role="tab" aria-controls="nav-historia" aria-selected="false">Historia Clínica</a>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">

                    <%-- DATOS DE PACIENTE --%>

                    <div class="tab-pane fade show active padding-tab-exp" id="datosPaciente" role="tabpanel" aria-labelledby="nav-paciente-tab">


                        <div class="form-row">
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelCedulaPaciente" runat="server" Text="Cédula"></asp:Label>
                                <asp:TextBox ID="cedulaPaciente" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelNombrePaciente" runat="server" Text="Nombre"></asp:Label>
                                <asp:TextBox ID="nombrePaciente" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPrimerApellidoPaciente" runat="server" Text="Primer Apellido"></asp:Label>
                                <asp:TextBox ID="primerApellidoPaciente" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*" required></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelSegundoApellidoPaciente" runat="server" Text="Segundo Apellido"></asp:Label>
                                <asp:TextBox ID="segundoApellidoPaciente" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelFechaNacimiento" runat="server" Text="Fecha de Nacimiento"></asp:Label>
                                <asp:TextBox ID="fechaNacimientoPaciente" runat="server" class="form-control" placeholder="formato: dd/mm/aaaa" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelSexoPaciente" runat="server" Text="Sexo"></asp:Label>
                                <asp:DropDownList ID="sexoPaciente" runat="server" class="form-control" ClientIDMode="Static" required>
                                    <asp:ListItem Selected="True" Value="nulo">Seleccionar...</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="masculino">Masculino</asp:ListItem>
                                    <asp:ListItem Selected="False" Value="femenino">Femenino</asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:Label ID="labelFechaActual" runat="server" Text="Fecha actual"></asp:Label>
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

                    <%-- DATOS DE ENCARGADO --%>

                    <div class="tab-pane fade padding-tab-exp" id="datosEncargado" role="tabpanel" aria-labelledby="nav-encargado-tab">

                        <div class="form-row">
                            <div class="form-group col-md-3 col-sm-4">
                                <asp:Label ID="labelCedulaEncargado" runat="server" Text="Cédula"></asp:Label>
                                <asp:TextBox ID="cedulaEncargado" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
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
                                    <asp:TextBox ID="cedulaSolicitante" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
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

                    <%-- DATOS DE DESTINATARIO DE FACTURA --%>

                    <div class="tab-pane fade padding-tab-exp" id="datosDestinatario" role="tabpanel" aria-labelledby="nav-destinatario-tab">


                        <div class="form-row">
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelCedulaDestinatario" runat="server" Text="Cédula"></asp:Label>
                                <asp:TextBox ID="cedulaDestinatario" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelNombreDestinatario" runat="server" Text="Nombre"></asp:Label>
                                <asp:TextBox ID="nombreDestinatario" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPrimerApellidoDestinatario" runat="server" Text="Primer Apellido"></asp:Label>
                                <asp:TextBox ID="primerApellidoDestinatario" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelSegundoApellidoDestinatario" runat="server" Text="Segundo Apellido"></asp:Label>
                                <asp:TextBox ID="segundoApellidoDestinatario" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-zñÑáéíóúÁÉÍÓÚ ]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelTelefono" runat="server" Text="Teléfono"></asp:Label>
                                <asp:TextBox ID="telefonoDestinatario" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]*"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelCorreoDestinatario" runat="server" Text="Correo Electrónico"></asp:Label>
                                <asp:TextBox ID="correoDestinatario" type="email" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <hr />

                        <div class="form-row">

                            <h5 class="titulo col-12">Dirección</h5>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="label4" runat="server" ClientIDMode="Static" Text="Provincia"></asp:Label>
                                <asp:DropDownList onchange="ObtenerCantonesDestinatario(this)" ID="inputProvinciaDestinatario" runat="server" class="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="provinciaDValue" value="nulo" runat="server" />
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="label5" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                                <asp:DropDownList onchange="ObtenerDistritosDestinatario(this)" ID="inputCantonDestinatario" runat="server" class="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="cantonDValue" value="nulo" runat="server" />
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="label6" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                                <asp:DropDownList onchange="AsignarDistritoDestinatario(this)" ID="inputDistritoDestinatario" runat="server" class="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input type="hidden" clientidmode="Static" id="distritoDValue" value="nulo" runat="server" />
                            </div>

                        </div>

                        <div class="form-row">

                            <div class="form-group col-md-12 col-sm-12">
                                <asp:Label ID="labelDireccionExactaDestinatario" runat="server" Text="Dirección exacta"></asp:Label>
                                <textarea class="form-control txtArea-size" runat="server" clientidmode="Static" id="direccionExactaDestinatario" rows="2"></textarea>
                            </div>
                        </div>


                    </div>



                    <%-- DATOS DE HISTORIA CLÍNICA--%>

                    <div class="tab-pane fade padding-tab-exp" id="historiaClinica" role="tabpanel" aria-labelledby="nav-historia-tab">

                        <div class="form-row">

                            <h4 class="titulo col-12" style="padding-bottom: 10px">Datos de Nacimiento</h4>


                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelTallaNacimiento" runat="server" Text="Talla"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="tallaNacimiento" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">cm</span>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPesoNacimiento" runat="server" Text="Peso"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="pesoNacimiento" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">kg</span>
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
                                <asp:Label ID="labelApgar" runat="server" Text="Puntuación APGAR"></asp:Label>
                                <asp:TextBox ID="apgar" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]*"></asp:TextBox>
                            </div>



                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelEdadGestacional" runat="server" Text="Edad Gestacional"></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="edadGestacional" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">semanas</span>
                                    </div>
                                </div>
                            </div>


                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelClasificacionUniversal" runat="server" Text="Clasificación Universal"></asp:Label>
                                <asp:TextBox ID="clasificacionUniversal" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>

                        </div>

                        <hr />

                        <div class="form-row">

                            <h4 class="titulo col-12" style="padding-bottom: 10px">Antecedentes</h4>

                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Perinatales</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="PerinatalNormal(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="perinatal" id="normalPerinatal" value="normal">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Normal</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="PerinatalAnormal(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="perinatal" id="anormalPerinatal" value="anormal">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Anormal</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionPerinatal" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>


                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Patológicos</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="PatologicoNegativo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="patologico" id="negativoPatologico" value="negativo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Negativos</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="PatologicoPositivo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="patologico" id="positivoPatologico" value="positivo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Positivos</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionPatologico" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>


                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Quirúrgicos</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="QuirurgicoNegativo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="quirurgico" id="negativoQuirurgico" value="negativo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Negativos</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="QuirurgicoPositivo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="quirurgico" id="positivoQuirurgico" value="positivo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Positivos</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionQuirurgico" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>


                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Traumáticos</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="TraumaticoNegativo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="traumatico" id="negativoTraumatico" value="negativo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Negativos</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="TraumaticoPositivo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="traumatico" id="positivoTraumatico" value="positivo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Positivos</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionTraumatico" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>

                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Heredo Familiares</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="HeredoFamiliarNegativo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="heredoFamiliares" id="negativoHeredoFamiliar" value="negativo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Negativos</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="HeredoFamiliarPositivo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="heredoFamiliares" id="positivoHeredoFamiliar" value="positivo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Positivos</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionHeredoFamiliar" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>

                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Alergias</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="AlergiaNegativo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="alergias" id="negativoAlergias" value="negativo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Negativas</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="AlergiaPositivo(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="alergias" id="positivoAlergias" value="positivo">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Positivas</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionAlergia" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>

                            <div class="form-group col-md-6 col-sm-12">

                                <h6 class="titulo col-12" style="padding-left: 0px !important;">Vacunas</h6>

                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                        <div style="margin-left: 5px !important;">

                                            <div class="form-check form-check-inline">
                                                <input onclick="VacunaAlDia(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="vacunas" id="aldiaVacunas" value="aldia">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Al día con esquema básico</label>
                                            </div>
                                            <div class="form-check form-check-inline">
                                                <input onclick="VacunaPendiente(this)" class="form-check-input" clientidmode="Static" runat="server" type="radio" name="vacunas" id="pendientesVacunas" value="pendientes">
                                                <label class="form-check-label" clientidmode="Static" runat="server">Pendientes</label>
                                            </div>
                                        </div>
                                    </li>

                                    <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                        <div class="padding-der-izq-list col-md-12 col-sm-12">
                                            <textarea class="form-control" runat="server" clientidmode="Static" id="descripcionVacuna" rows="3"></textarea>
                                        </div>
                                    </li>

                                </ul>

                            </div>


                        </div>


                        <div class="row justify-content-center" style="margin-top: 30px">
                            <div class="col-md-2 col-sm-5">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-general btn-guardar" OnClick="btnGuardar_Click" />
                            </div>
                            <div class="col-md-2 col-sm-5">
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-general btn-regresar" />
                            </div>
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
                ObtenerProvincias();
                esSolicitanteCita();
                esDestinatarioFactura();
            }, false);
        })();


        // Add the following code if you want the name of the file appear on select
        $(".custom-file-input").on("change", function () {
            var fileName = $(this).val().split("\\").pop();
            $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        });


    </script>

</asp:Content>
