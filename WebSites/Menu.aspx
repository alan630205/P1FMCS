<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Menu.aspx.vb" Inherits="Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table width="200px">
            <tr>
                <td width="130px" colspan="2" style="width: 200px">
    
        <asp:Label ID="lblLoginTime" runat="server" Text="" 
            style="font-family: Tahoma; font-size: small"></asp:Label>
                </td>
            </tr>            <tr>
                <td width="130px">
                    <asp:Label ID="lblUserName" runat="server" Text="" 
                        style="font-family: Tahoma; font-size: small"></asp:Label>
                </td>
                <td width="70px" align="right">
                    <asp:Button ID="btnLogout" runat="server" 
                        style="font-family: Tahoma; font-size: small" Text="登出" Width="64px" 
                        Height="24px" />
                </td>
            </tr>
         </table>
    
<br/>
        
        <asp:TreeView ID="tvMenu" runat="server" 
            CollapseImageUrl="~/img/menu/BOOK_Open.ICO" ExpandDepth="1" 
            ExpandImageUrl="~/img/menu/BOOK_Close.ICO">
            <HoverNodeStyle Font-Bold="True" Font-Italic="True" ForeColor="Blue" />
            <RootNodeStyle Font-Size="Medium" Font-Underline="True" HorizontalPadding="8px" 
                NodeSpacing="6px" />
            <LeafNodeStyle Font-Size="Small" ForeColor="Black" HorizontalPadding="8px" 
                ImageUrl="~/img/menu/NOTE12.ICO" />
        </asp:TreeView>
    
    </div>
    </form>
</body>
</html>
