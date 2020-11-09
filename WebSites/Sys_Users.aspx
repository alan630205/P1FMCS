<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sys_Users.aspx.vb" Inherits="Sys_Users" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .TABLE { table-layout:fixed; }
        .style2
        {
            text-align: center;
        }
        .style1
        {
            font-family: 標楷體;
            font-size: xx-large;
            font-weight: bold;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div class="style2">
            <span class="style1"> 使用者帳號管理<br />
            </span>
        </div>
    
        <table style="height: 630px">
            <tr>
               <td width="120px" align="right">
                    登入帳號：</td>
                <td width="100px">
                    <asp:TextBox ID="txtLoginName" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="100px" CausesValidation="True" MaxLength="10"></asp:TextBox>
                </td>
                <td width="120px" align="right">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="lblCheck" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td rowspan="6" valign="top">
                    <asp:Button ID="btnCheckAll" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="24px" Text="全選" Width="72px" />
&nbsp;
                    <asp:Button ID="btnClearAll" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="24px" Text="全不選" Width="72px" />
                        <br />
                        <br />
                        可使用報表：<br />
                    <DIV style="overflow-y:scroll; WIDTH:539px; HEIGHT:551px">
                   <asp:GridView ID="gvUser_Task" runat="server"  AutoGenerateColumns="False" 
                            EmptyDataText="目前尚無任何畫面資料" CellPadding="4" ForeColor="#333333" 
                            GridLines="None">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="ID1" HeaderText="ID"  ></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="報表名稱" ></asp:BoundField>
                            
                          <asp:TemplateField >
                                 <HeaderTemplate><b> 讀 </b> </HeaderTemplate>
						        <ItemTemplate> <asp:checkbox runat="server" id="Read1" checked="False"></asp:checkbox></ItemTemplate>
						        <ItemStyle   HorizontalAlign="Center" />
					      </asp:TemplateField>
                          <asp:TemplateField >
                                 <HeaderTemplate><b> 寫 </b> </HeaderTemplate>
						        <ItemTemplate> <asp:checkbox runat="server" id="Write1" checked="False"></asp:checkbox></ItemTemplate>
						        <ItemStyle    HorizontalAlign="Center" />
					      </asp:TemplateField>
					
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </DIV>
                </td>
            </tr>
            <tr>
                <td width="120px" align="right">
                    姓名：</td>
                <td width="100px">
                    <asp:TextBox ID="txtName" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="100px" MaxLength="10"></asp:TextBox>
                </td>
                <td width="120px" align="right">
                    登入密碼：</td>
                <td colspan="2">
                    <asp:TextBox ID="txtPassword" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="100px" MaxLength="10"></asp:TextBox>
                &nbsp;
                    <asp:CheckBox ID="chkAD" runat="server" Text="採用AD密碼認證" Visible="False" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right">
                    群組：</td>
                <td width="100px">
                    <asp:TextBox ID="txtDept" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="100px" MaxLength="20"></asp:TextBox>
                </td>
                <td width="120px" align="right">
                    角色：</td>
                <td width="240px">
                    <asp:RadioButton ID="rdbManager" runat="server" Text="系統管理員" GroupName="Role" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdbUser" runat="server" Text="一般使用者" GroupName="Role" />
                </td>
                <td width="100px">
                    <asp:CheckBox ID="chkEnable" runat="server" Text="啟用" />
                </td>
            </tr>
            <tr>
                <td width="120px" align="right" >
                    備註：</td>
                <td colspan="4">
                    <asp:TextBox ID="txtEmail" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="460px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" height="40px" align="center">
                    <asp:Button ID="btnNew" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="新增" Width="72px" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnModify" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="修改" Width="72px" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="刪除" Width="72px" 
                        onclientclick='return confirm("是否確認要刪除所選的帳號資料？")' />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="取消" Width="72px" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    使用者帳號列表：(請點擊登入帳號連結，再進行修改或刪除作業)<br />

                   <DIV style="overflow-y:scroll; WIDTH:700px; HEIGHT:405px">
                   <asp:GridView ID="gvUsers" runat="server" Width="680px" 
                        AutoGenerateColumns="False" DataKeyNames="LoginName" EmptyDataText="目前尚無任何使用者帳號">
                        <Columns>
                            <asp:ButtonField CommandName="Select" DataTextField="LoginName" HeaderText="登入帳號" />
                            <asp:BoundField DataField="UserName" HeaderText="姓名" >
                            </asp:BoundField>
                            <asp:BoundField DataField="Dept" HeaderText="群組" />
                            <asp:BoundField DataField="Role" HeaderText="角色" />
                            <asp:CheckBoxField DataField="Enable_Flag" HeaderText="啟用">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="AD_Flag" HeaderText="AD密碼">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="Email" HeaderText="備註" />
                        </Columns>
                        <HeaderStyle BackColor="#3366FF" />
                    </asp:GridView>
                    </DIV>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
