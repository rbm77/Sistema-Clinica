<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="consultas_dia.aspx.cs" Inherits="Sistema_Pediatrico.consultas_dia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Consultas del Día</title>

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script>window.jQuery || document.write('<script src="Bootstrap/dist/js/jquery-3.3.1.js"><\/script>')</script>

    <link onerror="CargarLocalDT()" href="https://cdn.datatables.net/1.10.20/css/dataTables.bootstrap4.min.css" rel="stylesheet" />

    <script>
        function CargarLocalDT() {
            $('head').prepend('<link rel="stylesheet" href="Bootstrap/dist/css/dataTables.bootstrap4.min.css" />');
        }
    </script>

    <script type="text/javascript" charset="utf-8">

        $(document).ready(function () {

            $("#listaConsultasDia").DataTable({
                "scrollX": true,
                "autoWidth": true,
                "paging": true,
                "responsive": true,
                "ordering": false,
                "info": true,
            });
        });

        function AlinearData() {
            $('.dataTables_scrollHeadInner').css('width', '100%');
            setTimeout(function () {
                $("#listaConsultasDia").DataTable().columns.adjust().draw()
            }, 300);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedorExp">

        <h2 class="titulo">Consultas del Día</h2>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <form runat="server">

            <asp:GridView ID="listaConsultasDia" runat="server" ClientIDMode="Static" AutoGenerateColumns="false"
                CssClass="table table-bordered table-hover" Width="100%" OnRowCommand="listaConsultasDia_RowCommand"
                DataKeyNames="IdExpediente">
                <Columns>
                    <asp:BoundField HeaderText="Cédula" DataField="Cedula" HeaderStyle-Width="19%" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" HeaderStyle-Width="20%" />
                    <asp:BoundField HeaderText="Primer Apellido" DataField="PrimerApellido" HeaderStyle-Width="20%" />
                    <asp:BoundField HeaderText="Segundo Apellido" DataField="SegundoApellido" HeaderStyle-Width="20%" />
                    <asp:BoundField HeaderText="Hora" DataField="Hora" HeaderStyle-Width="13%" />

                    <asp:TemplateField HeaderText="Acción" HeaderStyle-CssClass="text-center"
                        HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="consultar" runat="server" CommandName="consultar"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"> 
                                    <i class="fas fa-eye fa-lg" style="color: #7199d0;"></i> 
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField Visible="false" DataField="IdExpediente"></asp:BoundField>

                </Columns>

            </asp:GridView>
            <div class="row justify-content-center" style="margin-top: 30px">
                <div class="col-md-2 col-sm-5">
                    <a href="inicio.aspx" class="btn btn-general btn-regresar">Regresar</a>
                </div>
            </div>
        </form>
    </div>

</asp:Content>
