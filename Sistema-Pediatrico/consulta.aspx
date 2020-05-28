<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="consulta.aspx.cs" Inherits="Sistema_Pediatrico.consulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedorExp">

        <h3 runat="server" id="Titulo" class="titulo"></h3>

        <h6 style="display: inline">Expediente #</h6>
        <h6 id="expedienteEncabezado" runat="server" style="color: #007bff; display: inline; margin-right: 20px"></h6>

        <h6 style="display: inline">Cédula:</h6>
        <h6 id="cedulaEncabezado" runat="server" style="color: #007bff; display: inline; margin-right: 20px"></h6>

        <h6 style="display: inline">Nombre:</h6>
        <h6 id="nombreEncabezado" runat="server" style="color: #007bff; display: inline"></h6>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <div id="contenedorDatos" runat="server">

            <form id="formularioConsulta" class="needs-validation"
                novalidate style="margin-top: 15px" runat="server">

                <div class="form-row">
                    <div class="form-group col-md-3 col-sm-3">
                        <asp:Label ID="labelFecha" runat="server" Text="Fecha"></asp:Label>
                        <asp:TextBox ID="fecha" runat="server" class="form-control" ClientIDMode="Static" pattern="([0-2][0-9]|3[0-1])(\/)(0[1-9]|1[0-2])\2(\d{4})" required></asp:TextBox>
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
                        <textarea class="form-control" runat="server" clientidmode="Static" id="padecimientoActual" rows="5"></textarea>
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
                                <div class="input-group mb-3">
                                    <asp:TextBox ID="IMC" runat="server" class="form-control" ClientIDMode="Static" pattern="[0-9]+(,[0-9]{1,2})?%?"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button id="calcular" onclick="CalcularIMC()" class="btn btn-Calcular" type="button">Calcular</button>
                                    </div>
                                </div>
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

                            <h5 class="titulo col-12">Gráficas de Crecimiento</h5>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPCEdad" runat="server" Text="P. Cefálico / Edad"></asp:Label>
                                <asp:TextBox ID="perimetroCefalicoEdad" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPesoEdad" runat="server" Text="Peso / Edad"></asp:Label>
                                <asp:TextBox ID="pesoEdad" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelTallaEdad" runat="server" Text="Talla / Edad"></asp:Label>
                                <asp:TextBox ID="tallaEdad" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelPesoTalla" runat="server" Text="Peso / Talla"></asp:Label>
                                <asp:TextBox ID="pesoTalla" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelIMCEdad" runat="server" Text="IMC / Edad"></asp:Label>
                                <asp:TextBox ID="imcEdad" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>

                        </div>

                        <hr />

                        <div class="form-row">

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelRuidosCardiacos" runat="server" Text="Ruidos Cardiacos"></asp:Label>
                                <textarea class="form-control" runat="server" clientidmode="Static" id="ruidosCardiacos" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelCamposPulmonares" runat="server" Text="Campos Pulmonares"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="camposPulmonares" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelAbdomen" runat="server" Text="Abdomen"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="abdomen" rows="3"></textarea>
                            </div>


                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelFaringe" runat="server" Text="Faringe"></asp:Label>
                                <textarea class="form-control" runat="server" clientidmode="Static" id="faringe" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelNariz" runat="server" Text="Nariz"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="nariz" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelOidos" runat="server" Text="Oídos"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="oidos" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelSNC" runat="server" Text="SNC"></asp:Label>
                                <textarea class="form-control" runat="server" clientidmode="Static" id="snc" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelNeurodesarrollo" runat="server" Text="Neurodesarrollo"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="neurodesarrollo" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelSistemaOsteomuscular" runat="server" Text="Sistema Osteomuscular"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="sistemaOsteomuscular" rows="3"></textarea>
                            </div>


                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelPiel" runat="server" Text="Piel"></asp:Label>
                                <textarea class="form-control" runat="server" clientidmode="Static" id="piel" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelEstadoAlerta" runat="server" Text="Estado de Alerta"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="estadoAlerta" rows="3"></textarea>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <asp:Label ID="labelEstadoHidratacion" runat="server" Text="Estado de Hidratación"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="estadoHidratacion" rows="3"></textarea>
                            </div>
                            <div class="form-group col-12">
                                <asp:Label ID="labelOtros" runat="server" Text="Otros Hallazgos"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="otrosHallazgos" rows="3"></textarea>
                            </div>
                        </div>
                    </div>

                    <%-- DATOS DE CONCLUSIONES --%>

                    <div class="tab-pane fade padding-tab-exp" id="conclusiones" role="tabpanel" aria-labelledby="nav-conclusiones-tab">

                        <div class="form-row">
                            <div class="form-group col-12">
                                <asp:Label ID="labelAnalisis" runat="server" Text="Análisis"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="analisis" rows="5"></textarea>
                            </div>
                            <div class="form-group col-12">
                                <asp:Label ID="labelImpresionDiagnostica" runat="server" Text="Impresión Diagnóstica"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="impresionDiagnostica" rows="5"></textarea>
                            </div>
                            <div class="form-group col-12">
                                <asp:Label ID="labelPlan" runat="server" Text="Plan"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="plan" rows="5"></textarea>
                            </div>

                        </div>

                        <hr />

                        <div class="form-row">


                            <div class="form-group col-md-6 col-sm-6">

                                <asp:Label ID="labelEnfermedades" runat="server" Text="Enfermedades"></asp:Label>
                                <asp:DropDownList ID="enfermedades" runat="server" class="form-control" ClientIDMode="Static">
                                    
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-6 col-sm-6">
                                <small class="form-text text-muted" style="padding-top: 16px">Seleccionar sólo en caso de que deba 
                                    incluirse en el Registro Colectivo de Notificaciones de Enfermedades del Ministerio de Salud</small>
                            </div>

                        </div>

                        <hr />

                        <div class="form-row">

                            <h5 class="titulo col-12" style="margin-bottom: 15px">Referecia a Medicina Mixta</h5>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelFrecuencia" runat="server" Text="Frecuencia"></asp:Label>
                                <asp:DropDownList ID="frecuencia" runat="server" class="form-control" ClientIDMode="Static">
                                    <asp:ListItem Selected="True" Value="nulo">Seleccionar...</asp:ListItem>
                                    <asp:ListItem Value="primeraVezVida">Primera Vez (Vida)</asp:ListItem>
                                    <asp:ListItem Value="primeraVezAnno">Primera Vez (Año)</asp:ListItem>
                                    <asp:ListItem Value="primeraVezEspecializada">Primera Vez (Especializada)</asp:ListItem>
                                    <asp:ListItem Value="subsecuente">Subsecuente</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelReferidoA" runat="server" Text="Referido a"></asp:Label>
                                <asp:DropDownList ID="referidoA" runat="server" class="form-control" ClientIDMode="Static">
                                    <asp:ListItem Selected="True" Value="nulo">Seleccionar...</asp:ListItem>
                                    <asp:ListItem Value="especialista">Especialista</asp:ListItem>
                                    <asp:ListItem Value="hospitalizacion">Hospitalización</asp:ListItem>
                                    <asp:ListItem Value="otroCentro">Otro centro</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-4 col-sm-4">
                                <small class="form-text text-muted" style="padding-top: 16px">Seleccionar sólo en caso de que se deba 
                                    reportar una referencia a Medicina Mixta</small>
                            </div>

                        </div>

                        <hr />

                        <div class="form-row">

                            <h5 class="titulo col-12" style="margin-bottom: 15px">Referecia a Consulta Privada</h5>

                            <div class="form-group col-md-4 col-sm-4">
                                <asp:Label ID="labelEspecialidad" runat="server" Text="Especialidad"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="especialidad" rows="2"></textarea>
                            </div>

                            <div class="form-group col-md-8 col-sm-8">
                                <asp:Label ID="labelMotivo" runat="server" Text="Motivo"></asp:Label>
                                <textarea class="form-control" clientidmode="Static" runat="server" id="motivo" rows="2"></textarea>
                            </div>

                            <div class="form-group col-12">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="generarReferencia" runat="server" clientidmode="Static">
                                    <small class="form-text text-muted" style="margin-top: 0px">Generar PDF con la referencia al guardar la consulta</small>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="row justify-content-center" style="margin-top: 30px">
                        <div class="col-md-2 col-sm-5">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-general btn-guardar" />
                        </div>
                        <div class="col-md-2 col-sm-5">
                            <a href="" class="btn btn-general btn-regresar">Regresar</a>
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
