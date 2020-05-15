<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="Sistema_Pediatrico.inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inicio</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedor">

        <h1 class="titulo">Inicio</h1>

        <hr />

        <!-- Icon Cards-->
        <div class="row clearfix">
            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 ">

                <%  if (Session["rol"] != null)
                    {
                        string rol = Session["rol"].ToString();
                        if (rol.Equals("administrador"))
                        { %>

                <div class="row">
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5">
                        <div class="row clearfix">
                            <div class="col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">
                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="administracion.aspx">
                                        <i style="color: #1d5e93" class="fas fa-user-cog fa-4x"></i>
                                    </a>

                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="administracion.aspx">
                                        <span class="float-left">Cuentas</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                            <div class="col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">
                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="enfermedades.aspx">
                                        <i style="color: #1d5e93" class="fas fa-notes-medical fa-4x"></i>
                                    </a>

                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="enfermedades.aspx">
                                        <span class="float-left">Enfermedadades</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>




                <% }
                    else
                    { %>

                <div class="row">
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5">
                        <div class="row clearfix">


                            <div class="col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">
                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="expedientes.aspx">
                                        <i style="color: #1d5e93" class="far fa-id-card fa-4x"></i>
                                    </a>

                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="expedientes.aspx">
                                        <span class="float-left">Expedientes</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                            <div class=" col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">
                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="expediente.aspx?accion=crear">
                                        <i style="color: #1d5e93" class="fas fa-plus-circle fa-4x"></i>
                                    </a>
                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="expediente.aspx?accion=crea">
                                        <span class="float-left">Nuevo Expediente</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                            <div class="col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">

                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="ListaConsultasActivas.aspx">
                                        <i style="color: #1d5e93" class="fas fa-user-check fa-4x"></i>
                                    </a>

                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="ListaConsultasActivas.aspx">
                                        <span class="float-left">Consultas del día</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                            <div class="col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">
                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="CrearReportes.aspx">
                                        <i style="color: #1d5e93" class="fas fa-chart-pie fa-4x"></i>
                                    </a>
                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="CrearReportes.aspx">
                                        <span class="float-left">Reportes</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="offset-xl-1 col-xl-5 offset-lg-1 col-lg-5 offset-md-1 col-md-5 offset-sm-1 col-sm-5">
                        <div class="row clearfix">

                            <div class=" col-sm-12 col-xl-6 col-lg-12 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">
                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="MiAgenda.aspx">
                                        <i style="color: #1d5e93" class="far fa-calendar-alt fa-4x"></i>
                                    </a>
                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="MiAgenda.aspx">
                                        <span class="float-left">Configurar Agenda</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>

                            <div class="col-sm-12 col-xl-6 col-lg-6 col-md-12 mb-20 padding-cards-inicio">
                                <div class="card text-white o-hidden h-100">


                                    <a class="card-body card-dash" style="background: #f7f7f7;" href="GestionarAgenda.aspx">
                                        <i style="color: #1d5e93" class="fas fa-calendar-check fa-4x"></i>
                                    </a>

                                    <a class="card-footer card-footer-inicio text-white clearfix z-1" href="GestionarAgenda.aspx">
                                        <span class="float-left">Citas</span>
                                        <span class="float-right">
                                            <i class="fas fa-angle-right"></i>
                                        </span>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <% }
                    }
                %>
            </div>
        </div>

    </div>
</asp:Content>
