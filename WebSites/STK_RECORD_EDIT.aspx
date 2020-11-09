<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_RECORD_EDIT.aspx.cs" Inherits="STK_RECORD_EDIT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script type="text/javascript">
        function txtKeyNumber()
        {
            if (!(((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) ||
                (window.event.keyCode == 13) || (window.event.keyCode == 46) ||
                (window.event.keyCode == 45)))
                //這段是判斷如果輸入的不是數字或小數點!那將無法輸入文字
            {
                return false;
            }
            return true;
        }
        </script>
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
            物料庫存異動紀錄編修作業
       </div>
       <div id="div1" class="Button">

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style3" >
                    <asp:Label ID="Label2" runat="server" Font-Names="細明體" Font-Size="Small" Text="庫位"></asp:Label>
                    <asp:DropDownList ID="DDL_WS" runat="server" AutoPostBack="True" Font-Names="細明體" Font-Size="Small" Height="26px" Width="114px">
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
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="line-height: 22px; width: 100%;table-layout:fixed;" BackColor="#DDDDDD" CellPadding="5" EnableModelValidation="True" GridLines="None" PageSize="12" AllowPaging="True" BorderWidth="1px" CellSpacing="1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Font-Names="細明體" Font-Size="Small" OnRowDataBound="GridView1_RowDataBound">
                   <AlternatingRowStyle BackColor="White" />
                   <Columns>
                       <asp:TemplateField HeaderText="PartNo" SortExpression="partno">
                           <ItemTemplate>
                               <asp:Label ID="lblpartno" runat="server" Text='<%# Eval("partno") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="80px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Barcode" SortExpression="barcode">
                           <ItemTemplate>
                               <asp:Label ID="lblbarcode" runat="server" Text='<%# Eval("barcode") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="170px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Serialno Chemname">
                           <ItemTemplate>
                               <asp:Label ID="lblserialno" runat="server" Text='<%# Eval("serialno") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="80px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Seq EQName">
                           <ItemTemplate>
                               <asp:Label ID="lblseq" runat="server" Text='<%# Eval("seq") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="異動時間">
                           <ItemTemplate>
                               <asp:Label ID="lbldatetime" runat="server" Text='<%# String.Format("{0:yyyy/MM/dd HH:mm:ss}", Eval("datetime")) %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="140px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="領料數" SortExpression="stk_in_qty">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_in_qty_edit" runat="server" Text='<%# Eval("in_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblin_qty" runat="server" Text='<%# Eval("in_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="退料數" SortExpression="stk_out_qty">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_out_qty_edit" runat="server" Text='<%# Eval("out_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblout_qty" runat="server" Text='<%# Eval("out_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="上架數" SortExpression="stk_using_qty">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_using_qty_edit" runat="server" Text='<%# Eval("using_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblusing_qty" runat="server" Text='<%# Eval("using_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="下架數">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_takeoff_qty_edit" runat="server" Text='<%# Eval("takeoff_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbltakeoff_qty" runat="server" Text='<%# Eval("takeoff_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="轉入數">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_tranin_qty_edit" runat="server" Text='<%# Eval("tranin_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbltranin_qty" runat="server" Text='<%# Eval("tranin_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="轉出數">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_tranout_qty_edit" runat="server" Text='<%# Eval("tranout_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbltranout_qty" runat="server" Text='<%# Eval("tranout_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="調整數" SortExpression="stk_adj_qty">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_adj_qty_edit" runat="server" Text='<%# Eval("adj_qty") %>' Width="90%" style="text-align: right" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbladj_qty" runat="server" Text='<%# Eval("adj_qty") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="庫存數">
                           <ItemTemplate>
                               <asp:Label ID="lblstk_qty" runat="server" Text='<%# Eval("stk_qty") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Right" BackColor="Aqua" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="作廢" SortExpression="data_cancel">
                           <EditItemTemplate>
                               <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" />
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:CheckBox ID="chb_data_cancel" runat="server" Checked='<%# Eval("data_cancel") %>' Enabled="False" />
                           </ItemTemplate>
                           <HeaderStyle Width="30px" />
                       </asp:TemplateField>
<asp:TemplateField>
    <EditItemTemplate>
        <asp:LinkButton ID="lbupdate" runat="server" CommandName="Update">確認</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="lbcancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:LinkButton ID="lbtn_cancel" runat="server">作廢</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit">修改</asp:LinkButton>
    </ItemTemplate>
    <HeaderStyle Width="70px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="調整說明">
                           <ItemTemplate>
                               <asp:Label ID="lbl_descr" runat="server" Text='<%# Eval("descr") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="150px" />
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
