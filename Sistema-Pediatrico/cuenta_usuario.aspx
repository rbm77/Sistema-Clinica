<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="cuenta_usuario.aspx.cs" Inherits="Sistema_Pediatrico.cuenta_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid padding-contenedor">


        <form id="formulario_cuenta_usuario" runat="server">

            <div class="form-row">
                <div class="form-group col-md-3">
                    <label for="inputCedula">Cédula</label>
                    <input type="text" class="form-control" id="inputCedula">
                </div>
                <div class="form-group col-md-3">
                    <label for="inputNombre">Nombre</label>
                    <input type="text" class="form-control" id="inputNombre">
                </div>
                <div class="form-group col-md-3">
                    <label for="inputPrimerApellido">Primer Apellido</label>
                    <input type="text" class="form-control" id="inputPrimerApellido">
                </div>
                <div class="form-group col-md-3">
                    <label for="inputSegundoApellido">Segundo Apellido</label>
                    <input type="text" class="form-control" id="inputSegundoApellido">
                </div>
            </div>

        </form>

    </div>



</asp:Content>
