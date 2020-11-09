<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sys_Reports.aspx.vb" Inherits="Sys_Reports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            text-align: center;
            font-family: 標楷體;
            font-size: x-large;
            color: #0000FF;
            font-weight: bold;
        }
        .style1
        {
            font-family: 標楷體;
            font-size: xx-large;
            font-weight: bold;
            text-align: center;
        }
        .style4
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style4">
    
        <div class="style4">
            <span class="style1"> 系統報表管理</span><br />
        </div>
    
        <table style="width: 1076px">
            <tr height="50px">
                <td colspan="2" class="style2">
                    子系統</td>
                <td class="style2" colspan="6">
                    報表及網頁</td>
            </tr>
            <tr>
                <td width="140px" align="right">
                    排列序號：</td>
                <td width="220px" style="text-align: left">
                    <asp:TextBox ID="txtSys_No" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="40px"></asp:TextBox>
                </td>
                <td width="100px" align="right">
                    排列序號：</td>
                <td width="60px" align="left">
                    <asp:TextBox ID="txtTask_No" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="40px"></asp:TextBox>
                </td>
                <td width="100px" align="right">
                    程序名稱：</td>
                <td width="200px" align="left">
                    <asp:TextBox ID="txtFile_Name" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="197px" MaxLength="40"></asp:TextBox>
                </td>
                 <td width="80px" align="right">
                    副檔名：</td>
                <td width="60px" align="left">
                    <asp:TextBox ID="txtFile_Ext" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="60px" MaxLength="10"></asp:TextBox>
           </tr>
            <tr>
                <td width="140px" align="right">
                    系統代號：</td>
                <td width="220px" style="text-align: left">
                    <asp:TextBox ID="txtSys_ID" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="60px" MaxLength="20"></asp:TextBox>
                    <asp:Label ID="lblSysCheck" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td width="100px" align="right">
                    作業代號：</td>
                <td colspan="5" style="text-align: left">
                    <asp:TextBox ID="txtTask_ID" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="262px" MaxLength="30"></asp:TextBox>
                    <asp:Label ID="lblTaskCheck" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="140px" align="right">
                    顯示名稱：</td>
                <td width="220px" align="left">
                    <asp:TextBox ID="txtSys_Name" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="180px" MaxLength="30"></asp:TextBox>
                </td>
                <td width="100px" align="right">
                    顯示名稱：</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txtTask_Name" runat="server" Font-Names="Tahoma" Font-Size="Medium" 
                        Width="356px" style="text-align: left" MaxLength="40"></asp:TextBox>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:CheckBox ID="chkEnable" runat="server" Text="啟用" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSysNew" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="新增" Width="64px" />
&nbsp;<asp:Button ID="btnSysModify" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="修改" Width="64px" />
&nbsp;<asp:Button ID="btnSysDelete" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="刪除" Width="64px" 
                        
                        onclientclick="return confirm(&quot;是否確認要刪除所選的子系統 (同時會刪除此子系統所有的作業)？&quot;)" />
&nbsp;<asp:Button ID="btnSysCancel" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="取消" Width="64px" />
                </td>
                <td colspan="6">
                    <asp:Button ID="btnTaskNew" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="新增" Width="72px" />
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnTaskModify" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="修改" Width="72px" />
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnTaskDelete" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="刪除" Width="72px" 
                        onclientclick="return confirm(&quot;是否確認要刪除所選的作業？&quot;)" />
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnTaskCancel" runat="server" Font-Names="新細明體" Font-Size="Medium" 
                        Height="30px" Text="取消" Width="72px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" align="left">
                    子系統列表：(請點擊系統代號連結，再進行修改或刪除作業)<br />
                   <DIV style="OVERFLOW-Y:scroll; WIDTH:300px; HEIGHT:280px">
                    <asp:GridView ID="gvSys" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="ID" Width="260px">
                        <Columns>
                            <asp:BoundField DataField="Order_No" HeaderText="排列序號">
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:ButtonField DataTextField="ID" HeaderText="系統代號" CommandName="Select">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="Name" HeaderText="顯示名稱">
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#3366FF" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                    </DIV>
                </td>
                <td colspan="6" valign="top">
                    作業列表：(請點擊作業代號連結，再進行修改或刪除作業)<br />
                    <br />
                   <DIV style="OVERFLOW-Y:scroll; WIDTH:683px; HEIGHT:337px">
                    <asp:GridView ID="gvTask" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                        EmptyDataText="尚未選擇子系統或所選子系統尚未有任何作業!" Width="643px">
                        <Columns>
                            <asp:BoundField DataField="Order_No" HeaderText="排列序號">
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:ButtonField DataTextField="ID" HeaderText="作業代號" CommandName="Select">
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="Name" HeaderText="顯示名稱">
                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="File_Name" HeaderText="程序名稱">
                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="File_Ext" HeaderText="副檔名">
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="Enable_Flag" HeaderText="啟用">
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:CheckBoxField>
                        </Columns>
                        <HeaderStyle BackColor="#3366FF" ForeColor="White" />
                    </asp:GridView>
                    </DIV>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
