<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="enfermedades.aspx.cs" Inherits="Sistema_Pediatrico.enfermedades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Administración de Enfermedades</title>

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

            $("#listaEnfermedades").DataTable({
                "scrollX": true,
                "autoWidth": true,
                "paging": true,
                "responsive": true,
                "ordering": true,
                "info": true,
            });
        });

        function AlinearData() {
            $('.dataTables_scrollHeadInner').css('width', '100%');
            setTimeout(function () {
                $("#listaEnfermedades").DataTable().columns.adjust().draw()
            }, 300);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedorExp">

        <h2 class="titulo">Administración de Enfermedades</h2>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <form runat="server">

            <div class="row justify-content-end">
                <div class="col-md-4 col-sm-6">
                    <button type="button" class="btn btn-general btn-crear" data-toggle="modal" data-target="#exampleModal">
                        Ingresar Enfermedad
                    </button>
                </div>
            </div>


            <asp:GridView ID="listaEnfermedades" runat="server" ClientIDMode="Static" AutoGenerateColumns="false"
                CssClass="table table-bordered table-hover" Width="100%" OnRowCommand="listaEnfermedades_RowCommand" DataKeyNames="Enfermedad">
                <Columns>
                    <asp:BoundField HeaderText="Enfermedad" DataField="Enfermedad" HeaderStyle-Width="85%" />

                    <asp:TemplateField HeaderText="Acción" HeaderStyle-CssClass="text-center"
                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <div class="row">
                                <div class="col-12">
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

            <div class="row justify-content-center" style="margin-top: 30px">
                <div class="col-md-2 col-sm-5">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-general btn-regresar" />
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Nueva Enfermedad</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="enfermedadNueva" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn-Modal btn-regresar" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="guardarEnfermedad" runat="server" Text="Guardar" OnClick="guardarEnfermedad_Click" CssClass="btn-Modal btn-guardar" />
                        </div>
                    </div>
                </div>
            </div>

        </form>

    </div>

</asp:Content>
