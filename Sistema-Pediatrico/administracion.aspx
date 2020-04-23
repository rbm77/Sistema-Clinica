<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="administracion.aspx.cs" Inherits="Sistema_Pediatrico.administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Administración de Cuentas</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedorExp">

        <h2 class="titulo">Administración de Cuentas</h2>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <form runat="server">

            <div class="row justify-content-end">
                <div class="col-md-3 col-sm-6">

                    <asp:Button ID="btnCrear" runat="server" Text="Crear Cuenta" OnClick="btnCrear_Click" CssClass="btn btn-general btn-crear" />

                </div>
            </div>


            <div class="table-responsive" style="margin-top: 10px">
                <asp:GridView ID="listaCuentas" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" HeaderStyle-Width="28%" />
                        <asp:BoundField HeaderText="Cédula" DataField="Cedula" HeaderStyle-Width="15%" />
                        <asp:BoundField HeaderText="Correo" DataField="Correo" HeaderStyle-Width="32%" />
                        <asp:BoundField HeaderText="Teléfono" DataField="Telefono" HeaderStyle-Width="15%" />

                        <asp:TemplateField HeaderText="Activa" HeaderStyle-CssClass="text-center"
                            HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="estado" Checked='<%#Convert.ToBoolean(Eval("Estado"))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

            <div class="row justify-content-center" style="margin-top: 30px">
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-general btn-guardar" />
                </div>
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-general btn-regresar" />
                </div>
            </div>

        </form>





    </div>


</asp:Content>
