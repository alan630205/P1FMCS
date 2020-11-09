<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_chk_main.aspx.cs" Inherits="STK_chk_main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .divBar{text-align: center; font-size: x-large; height: 35px; background-color: #333399; font-family: 細明體; color: #00FFFF;line-height:35px}
        .divmenu1{width:33%; height:50px; float:left; display:inline; text-align: center; font-size: x-large; font-family: 細明體; color: #AAB5CD;line-height:50px}
        .divspace{width:100%; height:100px; float:left; display:inline; text-align: center; font-size: x-large; font-family: 細明體;}
        .divmenupict{width:33%; height:250px; float:left; display:inline; text-align: center; font-size: x-large; font-family: 細明體; color: #AAB5CD;}    
    </style>
</head>
<body style="height: 415px">
    <form id="form1" runat="server">
    <div class="divBar">
        物料管理盤點作業
    </div>
    <div class="divspace" ></div>
    <div class="divmenu1" >
        <asp:Label ID="Label1" runat="server" Font-Names="細明體" Font-Size="XX-Large" ForeColor="#0033CC" Text="盤點資料產生"></asp:Label>
    </div>
    <div class="divmenu1" >
        <asp:Label ID="Label2" runat="server" Font-Names="細明體" Font-Size="XX-Large" ForeColor="#0033CC" Text="差異編輯.查詢"></asp:Label>
    </div>
    <div class="divmenu1" >
        <asp:Label ID="Label3" runat="server" Font-Names="細明體" Font-Size="XX-Large" ForeColor="#0033CC" Text="庫存盤點調整"></asp:Label>
    </div>

    <div class="divmenupict" >
        <asp:ImageButton ID="ImageButton1" runat="server" Height="200px" ImageAlign="Middle" ImageUrl="~/img/chk_Stk_data.png" Width="200px" PostBackUrl="~/STK_ChkData_Generate.aspx" />
    </div>
    <div class="divmenupict" >
        <asp:ImageButton ID="ImageButton5" runat="server" Height="200px" ImageAlign="Middle" ImageUrl="~/img/chk_stk_list.png" Width="200px" PostBackUrl="~/STK_CHK_EDIT.aspx" />
    </div>
    <div class="divmenupict" >
        <asp:ImageButton ID="ImageButton9" runat="server" Height="200px" ImageAlign="Middle" ImageUrl="~/img/chk_stk_ok.png" Width="200px" PostBackUrl="~/STK_CHK_STK_ADJUST.aspx" />
    </div>

    </form>
</body>
</html>
