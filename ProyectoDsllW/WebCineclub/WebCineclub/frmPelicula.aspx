<%@ Page Title="" Language="C#" MasterPageFile="~/frmPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPelicula.aspx.cs" Inherits="WebCineclub.Formulario_web11" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Cuerpo" runat="server">
    <table align="center" class="auto-style8">
        <tr>
            <td class="auto-style9" colspan="2" style="font-family: 'Bell MT'; font-size: x-large; font-weight: bold">Info. Películas</td>
        </tr>
        <tr>
            <td class="auto-style11">Nacionalidad:</td>
            <td class="auto-style17">
                <asp:DropDownList ID="ddlNac" runat="server" Height="20px" Width="127px" OnSelectedIndexChanged="ddlNac_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">Productora:</td>
            <td class="auto-style17">
                <asp:DropDownList ID="ddlProductora" runat="server" Height="20px" Width="125px" OnSelectedIndexChanged="ddlProductora_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style16" style="width: 50%; font-family: 'bell MT';">Código :</td>
            <td class="auto-style12">
                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ibtnBuscar" runat="server" OnClick="ibtnBuscar_Click" Height="18px" ImageAlign="Top" ImageUrl="~/Imagenes/buscar.png" Width="20px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style13" style="width: 50%; font-family: 'bell MT';">Título:</td>
            <td class="auto-style14">
                <asp:TextBox ID="txtTitulo" runat="server" Width="217px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style13" style="width: 50%; font-family: 'bell MT';">Fecha de Estreno:</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style13" style="width: 50%; font-family: 'bell MT';">Id Empleado:</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtEmpl" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style11" style="width: 50%; font-family: 'bell MT';">&nbsp;</td>
            <td class="auto-style17">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Menu ID="mnuOpciones" runat="server" BackColor="#CCCCCC" BorderColor="#FF6600" BorderStyle="Solid" BorderWidth="1px" DynamicHorizontalOffset="2" Font-Bold="True" Font-Size="Medium" OnMenuItemClick="mnuOpciones_MenuItemClick" Orientation="Horizontal" RenderingMode="Table" Width="100%">
                    <Items>
                        <asp:MenuItem Text="Buscar" Value="opcBuscar"></asp:MenuItem>
                        <asp:MenuItem Text="Agregar" Value="opcAgregar"></asp:MenuItem>
                        <asp:MenuItem Text="Modificar" Value="opcModificar"></asp:MenuItem>
                        <asp:MenuItem Text="Grabar" Value="opcGrabar"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="#FF6600" BorderStyle="Dotted" BorderWidth="2px" ForeColor="White" />
                </asp:Menu>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grvDatos" runat="server" Width="100%" OnRowCommand="grvDatos_RowCommand">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style18" colspan="2">
                <asp:Label ID="lblMsj" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style8 {
            width: 90%;
        }
        .auto-style9 {
            height: 23px;
            text-align: center;
            color: #FFFFCC;
            background-color: #FF6600;
        }
        .auto-style11 {
            width: 228px;
            text-align: right;
        }
        .auto-style12 {
            text-align: left;
            width: 50%;
        }
        .auto-style13 {
            width: 228px;
            text-align: right;
            height: 26px;
        }
        .auto-style14 {
            text-align: left;
            height: 26px;
            width: 50%;
        }
        .auto-style15 {
            height: 26px;
            width: 50%;
        }
        .auto-style16 {
            width: 228px;
            text-align: right;
            font-size: large;
        }
        .auto-style17 {
            width: 50%;
        }
    .auto-style18 {
        text-align: center;
    }
    </style>
</asp:Content>

