<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_CHK_STK_ADJUST.aspx.cs" Inherits="STK_CHK_STK_ADJUST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <style type="text/css">
        html,body { 
            height: 100%; padding: 0 0 0 0; margin: 0; background-color:#AAB5CD;
        }
        form{
           height:100%;margin:0 0 0 0;margin-bottom:0px;
        }
        .outer {height: 100%; padding: 100px 0 0 0; box-sizing: border-box ; }
        .TOP { font-size: x-large; height:35px ;font-family: 細明體;line-height:35px;text-align: center; margin: -100px 0 0 0; background-color:#333399;color: #00FFFF; }
        .Button { height:115% ;font-family: 細明體;padding:0px 0px 0px 0px ;margin:0 0 0 0; }
        
        .browser_left {border: 1px solid #000000; overflow: scroll; width:35%; height:100%; float:left; display:inline; font-size: Medium; background-color: #AAB5CD; font-family: 細明體; color: #AAB5CD;}
        .browser_right {border: 1px solid #000000; overflow-y: scroll; width:64%; height:100%; float:left; display:inline; font-size: Medium; background-color: #AAB5CD; font-family: 細明體; color: #AAB5CD;}
        .browser_message{border: 1px solid #000000; width:100%; height:35Px; float:left; display:inline; font-size: Medium; background-color: #FFD4D4; font-family: 細明體; color: #AAB5CD;}
        .auto-style3 {
            height: 45px;background-color:pink;
        }
        .auto-stylea {
            font-family: 細明體; font-size: Medium; text-align: left;width: 35%;height:30px;
        }
        .auto-styleb {
            font-family: 細明體; font-size: Medium; text-align: left;width: 64%;height:30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="outer">
       <div class="TOP">
            盤點資料調整作業
       </div>
       <div id="div1" class="Button">

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style3" >
                    <asp:Label ID="Label1" runat="server" Text="庫位" Font-Names="細明體" Font-Size="Medium"></asp:Label>
                    <asp:DropDownList ID="DDL_WS" runat="server" Font-Names="細明體" Font-Size="Medium" Width="79px" Height="24px">
                    </asp:DropDownList>
                    </td>
                <td colspan="2" class="auto-style3">
                    <asp:Button ID="Button1" runat="server" Font-Names="細明體" Font-Size="Medium" OnClick="Button1_Click" Text="查詢" />
                </td>
            </tr>
            </table>
           <div style="height:70%; width:100%">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="line-height: 22px; width: 100%;table-layout:fixed;" BackColor="#DDDDDD" CellPadding="5" EnableModelValidation="True" GridLines="None" PageSize="12" AllowPaging="True" BorderWidth="1px" CellSpacing="1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Font-Names="細明體" Font-Size="Small">
                   <AlternatingRowStyle BackColor="White" />
                   <Columns>
                       <asp:TemplateField>
                           <EditItemTemplate>
                               <asp:LinkButton ID="lbOK" runat="server" CommandName="Update">確定</asp:LinkButton>
                               &nbsp;-
                               <asp:LinkButton ID="lb_Cancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:LinkButton ID="lbadj" runat="server" CommandName="Edit">結案</asp:LinkButton>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="結案">
                           <ItemTemplate>
                               <asp:CheckBox ID="cb_chk_ok" runat="server" Checked='<%# Eval("chk_ok") %>' Enabled="False" />
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="上次盤點日">
                           <ItemTemplate>
                               <asp:Label ID="lbl_lst_chk_date" runat="server" Text='<%# Eval("lst_chk_date") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="本次盤點日">
                           <ItemTemplate>
                               <asp:Label ID="lbl_chk_date" runat="server" Text='<%# Eval("chk_date") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="帳上總數">
                           <ItemTemplate>
                               <asp:Label ID="lbl_acc_qty" runat="server" Text='<%# Eval("acc_qty") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" HorizontalAlign="Right" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="盤點總數">
                           <ItemTemplate>
                               <asp:Label ID="lbl_chk_qty" runat="server" Text='<%# Eval("chk_qty") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="差異總數">
                           <ItemTemplate>
                               <asp:Label ID="lbl_dis_qty" runat="server" Text='<%# Eval("dis_qty") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="結案日期">
                           <HeaderStyle Width="100px" />
                           <ItemTemplate>
                               <asp:Label ID="lbl_adj_date" runat="server" Text='<%# Eval("adj_date","{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="10%" HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="人員">
                           <HeaderStyle Width="50px" />
                           <ItemTemplate>
                               <asp:Label ID="lbl_adj_user" runat="server" Text='<%# Eval("adj_user") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="10%" HorizontalAlign="Right" />
                       </asp:TemplateField>
                   </Columns>
                   <EditRowStyle BackColor="#CCFFFF" />
                   <EmptyDataTemplate>
                       Sorry, No any data.
                   </EmptyDataTemplate>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                   <RowStyle BackColor="#EFF3FB" />
                   <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               </asp:GridView>
               </div>
       </div>  
    </div>
    </form>
</body>
</html>
