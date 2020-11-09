<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_CHK_EDIT.aspx.cs" Inherits="STK_CHK_EDIT" %>

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
            盤點資料編輯作業
       </div>
       <div id="div1" class="Button">

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style3" >
                    <asp:Label ID="Label1" runat="server" Text="庫位" Font-Names="細明體" Font-Size="Medium"></asp:Label>
                    <asp:DropDownList ID="DDL_WS" runat="server" Font-Names="細明體" Font-Size="Medium" Width="79px" AutoPostBack="True" Height="24px" OnSelectedIndexChanged="DDL_ws_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="盤點日期" Font-Names="細明體" Font-Size="Medium"></asp:Label>
                    <asp:DropDownList ID="DDL_CHK_DATE" runat="server" Font-Names="細明體" Font-Size="Medium" Width="156px" AutoPostBack="True" Height="26px">
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Font-Names="細明體" Font-Size="Medium" Text="ChemName"></asp:Label>
                    <asp:DropDownList ID="DDL_CHEMNAME" runat="server" Font-Names="細明體" Font-Size="Medium" Width="156px" Height="26px">
                    </asp:DropDownList>
                    <asp:Label ID="Label4" runat="server" Font-Names="細明體" Font-Size="Medium" Text="VendorBatch"></asp:Label>
                    <asp:TextBox ID="txb_vendorbatch" runat="server" Font-Names="細明體" Font-Size="Medium" Width="182px"></asp:TextBox>
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
                               <asp:LinkButton ID="lbadj" runat="server" CommandName="Edit">修改</asp:LinkButton>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="ChemName">
                           <ItemTemplate>
                               <asp:Label ID="lbl_chemname" runat="server" Text='<%# Eval("chemname") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="130px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Barcode">
                           <ItemTemplate>
                               <asp:Label ID="lbl_barcode" runat="server" Text='<%# Eval("barcode") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="200px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Partno">
                           <ItemTemplate>
                               <asp:Label ID="lbl_partno" runat="server" Text='<%# Eval("partno") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="150px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Vendorbatch">
                           <ItemTemplate>
                               <asp:Label ID="lbl_vendorbatch" runat="server" Text='<%# Eval("vendorbatch") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="150px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="期初">
                           <ItemTemplate>
                               <asp:Label ID="lbl_fir_qty" runat="server" Text='<%# Eval("fir_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="領料">
                           <ItemTemplate>
                               <asp:Label ID="lbl_in_qty" runat="server" Text='<%# Eval("in_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="退料">
                           <ItemTemplate>
                               <asp:Label ID="lbl_out_qty" runat="server" Text='<%# Eval("out_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="上料">
                           <ItemTemplate>
                               <asp:Label ID="lbl_using_qty" runat="server" Text='<%# Eval("using_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="調整">
                           <ItemTemplate>
                               <asp:Label ID="lbl_adj_qty" runat="server" Text='<%# Eval("adj_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="盤點(1)">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_chk_qty_edit" runat="server" Text='<%# Eval("chk_qty") %>' Width="95%" Wrap="False"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_chk_qty" runat="server" Text='<%# Eval("chk_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="帳上(-2)">
                           <ItemTemplate>
                               <asp:Label ID="lbl_acc_qty" runat="server" Text='<%# Eval("acc_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="當日上料(+3)">
                           <ItemTemplate>
                               <asp:Label ID="lbl_done_using" runat="server" Text='<%# Eval("done_using") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="當日領退(-4)">
                           <ItemTemplate>
                               <asp:Label ID="lbl_done_arrival" runat="server" Text='<%# Eval("done_arrival") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="差異(=5)">
                           <ItemTemplate>
                               <asp:Label ID="lbl_dis_qty" runat="server" Text='<%# Eval("dis_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Right" Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
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
