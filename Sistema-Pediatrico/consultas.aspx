<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="consultas.aspx.cs" Inherits="Sistema_Pediatrico.consultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Consultas</title>

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script>window.jQuery || document.write('<script src="Bootstrap/dist/js/jquery-3.3.1.js"><\/script>')</script>

    <link onerror="CargarLocalDT()" href="https://cdn.datatables.net/1.10.20/css/dataTables.bootstrap4.min.css" rel="stylesheet" />

    <script>
        function CargarLocalDT() {
            $('head').prepend('<link rel="stylesheet" href="Bootstrap/dist/css/dataTables.bootstrap4.min.css" />');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid padding-contenedorExp">

        <h2 class="titulo">Consultas</h2>

        <h6 style="display: inline">Expediente #</h6>
        <h6 id="expedienteEncabezado" runat="server" style="color: #007bff; display: inline; margin-right: 20px"></h6>

        <h6 style="display: inline">Cédula:</h6>
        <h6 id="cedulaEncabezado" runat="server" style="color: #007bff; display: inline; margin-right: 20px"></h6>

        <h6 style="display: inline">Nombre:</h6>
        <h6 id="nombreEncabezado" runat="server" style="color: #007bff; display: inline"></h6>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <form runat="server">

            <div class="row justify-content-end">
                <div class="col-md-3 col-sm-6">

                    <asp:Button ID="btnCrear" runat="server" Text="Crear Consulta" OnClick="btnCrear_Click" CssClass="btn btn-general btn-crear" />

                </div>
            </div>

            <asp:GridView ID="listaConsultas" runat="server" ClientIDMode="Static" AutoGenerateColumns="false"
                CssClass="table table-bordered table-hover" Width="100%" OnRowCommand="listaConsultas_RowCommand"
                DataKeyNames="Fecha">
                <Columns>
                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" HeaderStyle-Width="42%" />
                    <asp:BoundField HeaderText="Hora" DataField="Hora" HeaderStyle-Width="42%" />


                    <asp:TemplateField HeaderText="Acción" HeaderStyle-CssClass="text-center"
                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <div class="row">
                                <div class="col-6">
                                    <asp:LinkButton ID="consultar" runat="server" CommandName="consultar"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"> 
                                    <i class="fas fa-eye fa-lg" style="color: #7199d0;"></i> 
                                    </asp:LinkButton>
                                </div>

                                <div class="col-6">
                                    <asp:LinkButton ID="eliminar" runat="server" CommandName="eliminar"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex%>">
                                        <i class="fas fa-trash-alt fa-lg" style="color: Tomato;"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </form>
    </div>

</asp:Content>
