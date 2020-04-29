<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="expedientes.aspx.cs" Inherits="Sistema_Pediatrico.expedientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Expedientes</title>

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

            $("#listaExpedientes").DataTable({
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
                $("#listaExpedientes").DataTable().columns.adjust().draw()
            }, 300);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid padding-contenedorExp">

        <h2 class="titulo">Expedientes</h2>

        <hr />

        <asp:Literal ID="mensajeConfirmacion" runat="server" Visible="false"></asp:Literal>

        <form runat="server">

            <div class="row justify-content-end">
                <div class="col-md-3 col-sm-6">

                    <asp:Button ID="btnCrear" runat="server" Text="Crear Expediente" OnClick="btnCrear_Click" CssClass="btn btn-general btn-crear" />

                </div>
            </div>

            <asp:GridView ID="listaExpedientes" runat="server" ClientIDMode="Static" AutoGenerateColumns="false"
                CssClass="table table-bordered table-hover" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="Cédula" DataField="Cedula" HeaderStyle-Width="19%" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" HeaderStyle-Width="23%" />
                    <asp:BoundField HeaderText="Primer Apellido" DataField="PrimerApellido" HeaderStyle-Width="23%" />
                    <asp:BoundField HeaderText="Segundo Apellido" DataField="SegundoApellido" HeaderStyle-Width="23%" />


                    <asp:TemplateField HeaderText="Acción" HeaderStyle-CssClass="text-center"
                        HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <div class="row">
                                <div class="col-6">
                                    <asp:LinkButton ID="consultar" runat="server"> 

                                      


                                    <i class="fas fa-eye fa-lg" style="color: #7199d0;"></i>

                                               
                                    </asp:LinkButton>
                                </div>

                                <div class="col-6">
                                    <asp:LinkButton ID="eliminar" runat="server">
                                        
                                        <i class="fas fa-trash-alt fa-lg" style="color: Tomato;"></i>
                                            
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField Visible="false" DataField="IdExpediente"></asp:BoundField>

                </Columns>

            </asp:GridView>

        </form>
    </div>

</asp:Content>
