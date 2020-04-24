<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="expedientes.aspx.cs" Inherits="Sistema_Pediatrico.expedientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Expedientes</title>

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


            <div class="table-responsive" style="margin-top: 10px">
                <asp:GridView ID="listaExpedientes" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover">
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

            </div>

        </form>
    </div>

</asp:Content>
