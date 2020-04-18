<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio_sesion.aspx.cs" Inherits="Sistema_Pediatrico.inicio_sesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">



    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <title>Bienvenido</title>

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script>window.jQuery || document.write('<script src="Bootstrap/dist/js/jquery-3.3.1.slim.min.js"><\/script>')</script>

    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script>window.jQuery.fn.modal || document.write('<script src="Bootstrap/dist/js/bootstrap.min.js"><\/script>')</script>

    <link onerror="CargarLocal()" rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <script>
        function CargarLocal() {
            $('head').prepend('<link rel="stylesheet" href="Bootstrap/dist/css/bootstrap.min.css" />');
        }
    </script>

    <link rel="icon" type="image/png" href="Recursos/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="Bootstrap/dist/css/inicioSesion.css" />


</head>

<body style="background-color: #666666;">

    <div class="limiter">

        <div class="container-login100">

            <div class="wrap-login100">
                <form class="login100-form validate-form" runat="server" style="padding-top: 30px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <div style="text-align: center">
                        <span style="margin: auto">
                            <asp:Image ID="Image1" runat="server" ImageUrl="Recursos/Logo_letras_azules.png"
                                Width="275px" AlternateText="Imagen no disponible" ImageAlign="TextTop" Height="125px" />
                        </span>
                    </div>

                    <br />

                    <br />
                    <span class="login100-form-title p-b-43">Inicio Sesión</span><br />

                    <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

                    <div class="wrap-input100 validate-input" data-validate="La cédula es requerida">

                        <asp:TextBox ID="inputCedula" class="input100" type="text"  runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="label-input100">Cédula</span>
                    </div>


                    <div class="wrap-input100 validate-input" data-validate="La contraseña es requerida">
                        <asp:TextBox ID="inputContrasenna" class="input100" type="password" name="pass" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="label-input100" id="txtContraseña">Contraseña</span>
                    </div>

                    <div class="flex-sb-m w-full p-t-3 p-b-32">

                        <div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <a href="" class="txt1">¿Olvidó su contraseña?
                            </a>
                        </div>
                    </div>


                    <div class="container-login100-form-btn">
                        <asp:Button ID="botonLogin" class="login100-form-btn" runat="server" Text="Ingresar" OnClick="botonLogin_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             
                            <br />

                        <br />
                        &nbsp;
                    </div>
                </form>

                <div class="login100-more" style="background-image: url('Recursos/bg-01.jpg');">
                </div>
            </div>
        </div>
    </div>


    <%--<div id="footer">
        <div class="footer-copyright text-center py-3">
            <a href="http://www.freepik.com">Imagen diseñada por jcomp / Freepik</a>
        </div>
    </div>--%>

    <script src="Bootstrap/dist/js/main.js"></script>

</body>
</html>
