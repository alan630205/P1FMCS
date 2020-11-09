<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Title.aspx.vb" Inherits="Title" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 388px;
        }
        .style3
        {
            font-size: xx-large;
        }
        .style4
        {
            font-size: x-large;
        }
    </style>
</head>
<body style="background-color: #AAB5CD">
    <form id="form1" runat="server">
    <div>
        <table class="style1" border="0" style="background-color: #F7F7F7">
        <tr>
            <td class="style2" valign="top">
                <img alt="" src="img/Title.JPG" style="width: 317px; height: 43px" /><br />
                <b>
                <span class="style3"> &nbsp;&nbsp; </span>
                <span class="style4"> FE12A_1智慧ｅ化平台</span></b></td>
            <td align="right" width="90%">
                <img alt="" src="img/pic.JPG" style="width: 520px; height: 90px" /></td>
        </tr>
    </table>
        <asp:HyperLink ID="HyperLink1" runat="server" Target="main" Visible="False">HyperLink</asp:HyperLink>
        <br />
    </div>
    </form>
</body>
</html>
