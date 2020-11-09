<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_ChkData_Generate.aspx.cs" Inherits="STK_ChkData_Generate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .divspace{width:100%; height:100px; float:left; display:inline; text-align: center; font-size: x-large; font-family: 細明體;line-height:35px;}
        .divTAC{width:48%;
height:400px;margin:0 auto;background:#999; }
        .divBar{text-align: center; font-size: x-large; height: 35px; background-color: #333399; font-family: 細明體; color: #00FFFF;line-height:35px;}
        .auto-style1 {width: 50%;border: 1px inset #000000; text-align: right;        }
        .auto-style2 {width: 50%;border: 1px inset #000000; text-align: left;        }

        #form1 {
            height: 467px;
        }
    </style>
</head>
<body style="height: 440px; background-color: #AAB5CD">
    <form id="form1" runat="server">
    <div class="divBar">
        物料管理盤點作業 - 盤點資料產生
    </div>
    <div class="divTAC" style="background-color: #AAB5CD">

        <div class="divspace" ></div>

        <table style="border: thin inset #000000; width:100%; text-align:center; height:140px; font-family: 細明體; font-size: large;">
            <tr>
                <td class="auto-style1" >
                    <asp:Label ID="Label1" runat="server" Text="系統別：" Font-Names="細明體" Font-Size="Large"></asp:Label>
                </td>
                <td class="auto-style2" >
                    <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="細明體" Font-Size="Large" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True" Width="114px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" style="border: 1px inset #000000;">
                    <asp:Label ID="Label2" runat="server" Text="上次盤點日：" Font-Names="細明體" Font-Size="Large"></asp:Label>
                </td>
                <td class="auto-style2" >
                    <asp:TextBox ID="TextBox2" runat="server" Font-Names="細明體" Font-Size="Large" Width="137px" ReadOnly="True" BackColor="Silver"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label3" runat="server" Text="本次盤點日："></asp:Label>
                </td>
                <td class="auto-style2" >
                    <asp:TextBox ID="TextBox1" runat="server" Font-Names="細明體" Font-Size="Large" MaxLength="10" Width="138px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="..." Width="31px" />
                </td>
            </tr>
        </table>
        <div style="height:60px;"></div>
        <div class="divspace" >
             <asp:Button ID="Button1" runat="server" Text="盤點資料產生" Font-Size="Large" Height="36px" Width="152px" OnClick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
