<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="administracion.aspx.cs" Inherits="Sistema_Pediatrico.administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedor">

        <h1 class="titulo">Administración de Cuentas</h1>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="confirmacionFoto" runat="server" Visible="false"></asp:Literal>


        <div class="table-responsive">
            <table class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th style="width: 28%" scope="col">Nombre</th>
                        <th style="width: 15%" scope="col">Cédula</th>
                        <th style="width: 32%" scope="col">Correo</th>
                        <th style="width: 15%" scope="col">Teléfono</th>
                        <th style="width: 10%; text-align: center" scope="col">Activa</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Robert Gerardo Moya Vasquez</td>
                        <td>207850434</td>
                        <td>clinicapediatricapalmares@hotmail.com</td>
                        <td>89712014</td>
                        <td style="text-align: center">
                            <input id="Checkbox1" type="checkbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>Jacob</td>
                        <td>@mdo</td>
                        <td>Thornton</td>
                        <td>@fat</td>
                        <td style="text-align: center">
                            <input id="Checkbox" type="checkbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>Larry the Bird</td>
                        <td>@mdo</td>
                        <td>Relleno</td>
                        <td>@twitter</td>
                        <td style="text-align: center">
                            <input id="Checkbo" type="checkbox" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


    </div>


</asp:Content>
