<%@ Page Title="" Language="C#" MasterPageFile="~/frmPrincipal.Master" AutoEventWireup="true" CodeBehind="frmActor.aspx.cs" Inherits="WebCineclub.Formulario_web13" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Cuerpo" runat="server">
    <table align="center" class="auto-style8">
    <tr>
        <td class="auto-style7" colspan="2" style="font-family: 'bell MT'"><strong>Actores</strong></td>
    </tr>
    <tr>
        <td class="auto-style13" style="width: 50%">&nbsp;</td>
        <td class="auto-style9" style="width: 50%">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style11" style="width: 50%">Nacionalidad :</td>
        <td class="auto-style9" style="width: 50%">
            <asp:DropDownList ID="ddlNac" runat="server" Height="16px" Width="134px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style10" style="width: 50%">Código del Actor :</td>
        <td style="width: 50%">
            <asp:TextBox ID="txtCodA" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style10" style="width: 50%">Nombre :</td>
        <td style="width: 50%">
            <asp:TextBox ID="txtNombre" runat="server" Width="188px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style10" style="width: 50%">Sexo :</td>
        <td class="auto-style12" style="width: 50%">
            <asp:RadioButtonList ID="rblSexo" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                <asp:ListItem Value="opcF">F</asp:ListItem>
                <asp:ListItem Value="opcM">M</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="auto-style10" style="width: 50%">Empleado :</td>
        <td style="width: 50%">
            <asp:TextBox ID="txtEmpl" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style10" style="width: 50%">&nbsp;</td>
        <td style="width: 50%">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Menu ID="mnuOpciones" runat="server" Orientation="Horizontal" RenderingMode="Table" Width="100%">
                <Items>
                    <asp:MenuItem Text="Buscar" Value="opcBuscar"></asp:MenuItem>
                    <asp:MenuItem Text="Agregar" Value="opcAgregar"></asp:MenuItem>
                    <asp:MenuItem Text="Modificar" Value="opcModificar"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="grvDatos" runat="server" Width="100%">
            </asp:GridView>
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
    }
    .auto-style10 {
            text-align: right;
            width: 266px;
        }
    .auto-style11 {
        height: 23px;
        text-align: right;
        width: 266px;
    }
    .auto-style12 {
        text-align: center;
    }
    .auto-style13 {
        height: 23px;
        width: 266px;
    }
</style>
</asp:Content>

