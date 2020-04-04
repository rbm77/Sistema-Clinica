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
                            <asp:TextBox ID="fechaNacimiento" runat="server" class="form-control" placeholder="formato: dd/mm/aaaa" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
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
                            <select onchange="ObtenerCantonesPaciente(this)" class="custom-select" id="inputProvinciaPaciente" clientidmode="Static" runat="server" required>
                            </select>
                        </div>
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelCantonPaciente" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                            <select onchange="ObtenerDistritosPaciente(this)" class="custom-select" id="inputCantonPaciente" clientidmode="Static" runat="server" required>
                            </select>
                        </div>
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelDistritosPaciente" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                            <select class="custom-select" id="inputDistritoPaciente" clientidmode="Static" runat="server" required>
                            </select>
                        </div>

                    </div>

                    <div class="form-row">

                        <div class="form-group col-md-6 col-sm-12">
                            <asp:Label ID="labelDireccionExactaPaciente" runat="server" Text="Dirección exacta"></asp:Label>
                            <textarea class="form-control txtArea-size" runat="server" clientidmode="Static" id="direccionExactaPaciente" rows="2"></textarea>
                        </div>

                        <div class="form-group col-md-6 col-sm-12">
                            <asp:Label ID="labelURLExpediente" runat="server" Text="URL vinculante a expediente antiguo"></asp:Label>
                            <textarea class="form-control txtArea-size" clientidmode="Static" runat="server" id="urlExpedienteAntiguo" rows="2"></textarea>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelFechaActual" runat="server" Text="Fecha actual"></asp:Label>
                            <asp:TextBox ID="fechaActual" runat="server" class="form-control" placeholder="formato: dd/mm/aaaa" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
                        </div>
                        <div class="form-group col-md-8 col-sm-12">
                            <asp:Label ID="label14" runat="server" Text="Foto del paciente"></asp:Label>
                            <div class="custom-file">
                                <asp:FileUpload class="custom-file-input" ID="inputFotoPaciente" runat="server" ClientIDMode="Static" />
                                <label class="custom-file-label" for="inputFotoPaciente">Seleccionar archivo...</label>
                            </div>
                        </div>
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
                            <asp:Label ID="labelProvinciaEncargado" runat="server" ClientIDMode="Static" Text="Provincia"></asp:Label>
                            <select onchange="ObtenerCantonesEncargado(this)" class="custom-select" id="inputProvinciaEncargado" clientidmode="Static" runat="server">
                            </select>
                        </div>
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelCantonEncargado" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                            <select onchange="ObtenerDistritosEncargado(this)" class="custom-select" id="inputCantonEncargado" clientidmode="Static" runat="server">
                            </select>
                        </div>
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelDistritoEncargado" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                            <select class="custom-select" id="inputDistritoEncargado" clientidmode="Static" runat="server">
                            </select>
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

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelCorreoSolicitante" runat="server" Text="Correo Electrónico"></asp:Label>
                                <asp:TextBox ID="correoSolicitante" type="email" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-6 col-sm-6">
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
                            <asp:Label ID="labelProvinciaDestinatario" runat="server" ClientIDMode="Static" Text="Provincia"></asp:Label>
                            <select onchange="ObtenerCantonesDestinatario(this)" class="custom-select" id="inputProvinciaDestinatario" clientidmode="Static" runat="server">
                            </select>
                        </div>
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelCantonDestinatario" runat="server" ClientIDMode="Static" Text="Cantón"></asp:Label>
                            <select onchange="ObtenerDistritosDestinatario(this)" class="custom-select" id="inputCantonDestinatario" clientidmode="Static" runat="server">
                            </select>
                        </div>
                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelDistritoDestinatario" runat="server" ClientIDMode="Static" Text="Distrito"></asp:Label>
                            <select class="custom-select" id="inputDistritoDestinatario" clientidmode="Static" runat="server">
                            </select>
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
                                <asp:TextBox ID="tallaNacimiento" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                <div class="input-group-append">
                                    <span class="input-group-text">cm</span>
                                </div>
                            </div>
                        </div>


                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelPesoNacimiento" runat="server" Text="Peso"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox ID="pesoNacimiento" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                <div class="input-group-append">
                                    <span class="input-group-text">kg</span>
                                </div>
                            </div>
                        </div>




                        <div class="form-group col-md-4 col-sm-4">
                            <asp:Label ID="labelPerimetroCefalico" runat="server" Text="Perímetro Cefálico"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox ID="perimetroCefalico" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
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
                                <asp:TextBox ID="edadGestacional" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
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
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1">
                                            <label class="form-check-label" for="inlineRadio1">Normal</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
                                            <label class="form-check-label" for="inlineRadio2">Anormal</label>
                                        </div>
                                    </div>
                                </li>

                                <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                    <div class="padding-der-izq-list col-md-12 col-sm-12">
                                        <textarea class="form-control" runat="server" clientidmode="Static" id="Textarea1" rows="3"></textarea>
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
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio3" value="option1">
                                            <label class="form-check-label" for="inlineRadio1">Normal</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio4" value="option2">
                                            <label class="form-check-label" for="inlineRadio2">Anormal</label>
                                        </div>
                                    </div>
                                </li>

                                <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                    <div class="padding-der-izq-list col-md-12 col-sm-12">
                                        <textarea class="form-control" runat="server" clientidmode="Static" id="Textarea2" rows="3"></textarea>
                                    </div>
                                </li>

                            </ul>

                        </div>


                        
                        <div class="form-group col-md-6 col-sm-12">

                            <h6 class="titulo col-12" style="padding-left: 0px !important;">Perinatales</h6>

                            <ul class="list-group list-group-flush">
                                <li class="list-group-item padding-der-izq-list padding-arr-aba-list list-group-item-light">

                                    <div style="margin-left: 5px !important;">

                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio7" value="option1">
                                            <label class="form-check-label" for="inlineRadio1">Normal</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio8" value="option2">
                                            <label class="form-check-label" for="inlineRadio2">Anormal</label>
                                        </div>
                                    </div>
                                </li>

                                <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                    <div class="padding-der-izq-list col-md-12 col-sm-12">
                                        <textarea class="form-control" runat="server" clientidmode="Static" id="Textarea3" rows="3"></textarea>
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
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio5" value="option1">
                                            <label class="form-check-label" for="inlineRadio1">Normal</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio6" value="option2">
                                            <label class="form-check-label" for="inlineRadio2">Anormal</label>
                                        </div>
                                    </div>
                                </li>

                                <li class="list-group-item padding-der-izq-list padding-arr-aba-list">
                                    <div class="padding-der-izq-list col-md-12 col-sm-12">
                                        <textarea class="form-control" runat="server" clientidmode="Static" id="Textarea4" rows="3"></textarea>
                                    </div>
                                </li>

                            </ul>

                        </div>


                    </div>


















                    <div class="row justify-content-center" style="margin-top: 30px">
                        <div class="col-md-2 col-sm-5">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-guardar" OnClick="btnGuardar_Click" />
                        </div>
                        <div class="col-md-2 col-sm-5">
                            <asp:Button ID="btnRegresar" runat="server" Text="Cancelar" CssClass="btn btn-regresar" />
                        </div>
                    </div>


                </div>
            </div>
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


    <%--            <div class="form-row">
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
            </div>--%>
</asp:Content>
