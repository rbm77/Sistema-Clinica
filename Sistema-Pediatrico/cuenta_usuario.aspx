<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="cuenta_usuario.aspx.cs" Inherits="Sistema_Pediatrico.cuenta_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid padding-contenedor">

        <h1 class="titulo">Cuenta de Usuario</h1>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>


        <form id="formularioUsuario" class="needs-validation"
            novalidate style="margin-top: 30px" runat="server">

            <div class="form-row">
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelCedula" runat="server" Text="Cédula"></asp:Label>
                    <asp:TextBox ID="inputCedula" runat="server" class="form-control" ClientIDMode="Static" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="inputNombre" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-z ]*" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelPrimerApellido" runat="server" Text="Primer Apellido"></asp:Label>
                    <asp:TextBox ID="inputPrimerApellido" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-z ]*" required></asp:TextBox>
                </div>
                <div class="form-group col-md-3 col-sm-4">
                    <asp:Label ID="labelSegundoApellido" runat="server" Text="Segundo Apellido"></asp:Label>
                    <asp:TextBox ID="inputSegundoApellido" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-z ]*" required></asp:TextBox>
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
                    <asp:TextBox ID="inputEspecialidad" runat="server" class="form-control" ClientIDMode="Static" pattern="[A-Za-z ]*" required></asp:TextBox>
                </div>
            </div>

            <div class="row justify-content-center" style="margin-top: 30px">
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-guardar" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnRegresar" runat="server" Text="Cancelar" CssClass="btn btn-regresar" OnClick="btnRegresar_Click" />
                </div>
            </div>



            <%-- CODIGO DIRECCION, VA EN OTRA PAGINA --%>

            <div class="form-row">
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



    </script>
</asp:Content>
