<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_STOCK_ADJ.aspx.cs" Inherits="STK_STOCK_ADJ" %>

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
            物料庫存手動調帳作業
       </div>
       <div id="div1" class="Button">

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style3" >
                    <asp:Label ID="Label2" runat="server" Font-Names="細明體" Font-Size="Small" Text="庫位"></asp:Label>
                    <asp:DropDownList ID="DDL_WS" runat="server" AutoPostBack="True" Font-Names="細明體" Font-Size="Small" Height="26px" OnSelectedIndexChanged="DDL_WS_SelectedIndexChanged" Width="114px">
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Font-Names="細明體" Font-Size="Small" Text="ChemName"></asp:Label>
                    <asp:DropDownList ID="DDL_ChemName" runat="server" Font-Names="細明體" Font-Size="Small" Width="224px" Height="26px">
                    </asp:DropDownList>
                    <asp:Label ID="Label4" runat="server" Font-Names="細明體" Font-Size="Small" Text="Vendorbatch"></asp:Label>
                    <asp:TextBox ID="TXB_Vendorbatch" runat="server" Font-Names="細明體" Font-Size="Small" Height="22px"></asp:TextBox>
                    </td>
                <td colspan="2" class="auto-style3">
                    <asp:Button ID="Button1" runat="server" Font-Names="細明體" Font-Size="Medium" OnClick="Button1_Click" Text="查詢" />
                </td>
            </tr>
            </table>
           <div style="height:70%; width:100%">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="line-height: 22px; width: 100%;table-layout:fixed;" BackColor="#DDDDDD" CellPadding="5" GridLines="None" PageSize="12" AllowPaging="True" BorderWidth="1px" CellSpacing="1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Font-Names="細明體" Font-Size="Small" OnRowCreated="GridView1_RowCreated">
                   <AlternatingRowStyle BackColor="White" />
                   <Columns>
                       <asp:TemplateField HeaderText="庫位" SortExpression="ws">
                           <ItemTemplate>
                               <asp:Label ID="lblws" runat="server" Text='<%# Eval("ws") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Barcode" SortExpression="barcode">
                           <ItemTemplate>
                               <asp:Label ID="lblbarcode" runat="server" Text='<%# Eval("barcode") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="200px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="DrumCode" SortExpression="Drumcode">
                           <ItemTemplate>
                               <asp:Label ID="lblDrumcode" runat="server" Text='<%# Eval("Drumcode") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Vendorbatch" SortExpression="vendorbatch">
                           <ItemTemplate>
                               <asp:Label ID="lblvendorbatch" runat="server" Text='<%# Eval("vendorbatch") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="效期" SortExpression="expirationdate">
                           <ItemTemplate>
                               <asp:Label ID="lblexpirationdate" runat="server" Text='<%# Eval("expirationdate") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="領料數" SortExpression="stk_in_qty">
                           <ItemTemplate>
                               <asp:Label ID="lblstk_in_qty" runat="server" Text='<%# Eval("stk_in_qty") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="退料數" SortExpression="stk_out_qty">
                           <ItemTemplate>
                               <asp:Label ID="lblstk_out_qty" runat="server" Text='<%# Eval("stk_out_qty") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="上架數" SortExpression="stk_using_qty">
                           <ItemTemplate>
                               <asp:Label ID="lblstk_using_qty" runat="server" Text='<%# Eval("stk_using_qty") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="轉入數">
                           <ItemTemplate>
                               <asp:Label ID="lblSTK_Transfer_IN_QTY" runat="server" Text='<%# Eval("STK_Transfer_IN_QTY") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="轉出數">
                           <ItemTemplate>
                               <asp:Label ID="lblSTK_Transfer_OUT_QTY" runat="server" Text='<%# Eval("STK_Transfer_OUT_QTY") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="調整數" SortExpression="stk_adj_qty">
                           <ItemTemplate>
                               <asp:Label ID="lblstk_adj_qty" runat="server" Text='<%# Eval("stk_adj_qty") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="庫存數">
                           <ItemTemplate>
                               <asp:Label ID="lblstk_qty" runat="server" Text='<%# Eval("stk_qty") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <EditItemTemplate>
                               <asp:LinkButton ID="lbOK" runat="server" CommandName="Update">確定</asp:LinkButton>
                               &nbsp;-
                               <asp:LinkButton ID="lb_Cancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:LinkButton ID="lbadj" runat="server" CommandName="Edit">調整</asp:LinkButton>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="調整數 / 調整說明">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_adjqty" runat="server" Height="100%" Width="10%"></asp:TextBox>
                               <asp:TextBox ID="txb_desc" runat="server" Height="100%" Width="75%"></asp:TextBox>
                           </EditItemTemplate>
                           <HeaderStyle Width="250px" />
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
