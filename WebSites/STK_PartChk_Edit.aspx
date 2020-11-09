<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_PartChk_Edit.aspx.cs" Inherits="STK_PartChk_Edit" %>

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
        
        .browser_left {border: 1px solid #000000; overflow-x: scroll; width:60%; height:100%; float:left; display:inline; font-size: Medium; background-color: #AAB5CD; font-family: 細明體; color: #AAB5CD; }
        .browser_right {border: 1px solid #000000; overflow-y: scroll; width:39%; height:100%; float:left; display:inline; font-size: Medium; background-color: #AAB5CD; font-family: 細明體; color: #AAB5CD;}
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
    <form id="form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="outer">
       <div class="TOP">
            物料核帳差異調整作業
       </div>
       <div id="div1" class="Button">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:50px;border-color:#bd77db">
            <tr style="height:30px;">
                <td class="auto-style3" >
                    <asp:Label ID="Label1" runat="server" Text="庫位：" Font-Names="細明體" Font-Size="Small"></asp:Label>
                    <asp:DropDownList ID="DDL_WS" runat="server" Font-Names="細明體" Font-Size="Small" Height="26px" Width="102px">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="核帳日期：" Font-Names="細明體" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="txb_date" runat="server" Font-Names="細明體" Font-Size="Small" MaxLength="10" Width="135px" Height="24px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="..." Width="31px" />
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Btn_Query" runat="server" Text="查詢" OnClick="Btn_Query_Click" />
                </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
           <div style="height:90%;">
               <div class="browser_left">

                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                   <ContentTemplate>
                   <asp:GridView ID="GridView1" runat="server" Style="line-height: 20px; table-layout:fixed; " AutoGenerateColumns="False" Font-Names="細明體" Font-Size="Small" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1" OnRowCreated="GridView1_RowCreated" DataKeyNames="chk_date,chk_ws,chk_drumcode,adjust_ok" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowCommand="GridView1_RowCommand">
                       <Columns>
                           <asp:TemplateField>
                               <HeaderTemplate>
                                   <asp:LinkButton ID="LB_all_adj" runat="server" ForeColor="#FF0066" CommandName="ALL_ADJUST" OnClientClick="return confirm('再一次確認 \n 你要調整全部核帳資料嗎？')">全部調整</asp:LinkButton>
                               </HeaderTemplate>
                               <ItemTemplate>
                                   <asp:LinkButton ID="LB_adj" runat="server" CommandName="ADJUST" OnClientClick="return confirm('再一次確認 \n 你要調整這筆核帳資料嗎？')" CommandArgument="<%# Container.DataItemIndex %>">調整</asp:LinkButton>
                               </ItemTemplate>
                               <HeaderStyle Width="50px" Height="30px" />
                               <ItemStyle Height="30px" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="調整">
                               <ItemTemplate>
                                   <asp:CheckBox ID="cb_adjust_ok" runat="server" Checked='<%# Eval("adjust_ok") %>' Enabled="False" />
                               </ItemTemplate>
                               <HeaderStyle Width="20px" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="核帳日期">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_chk_date" runat="server" Text='<%# Eval("chk_date") %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="50px" HorizontalAlign="Left" />
                               <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="庫位">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_chk_ws" runat="server" Text='<%# Eval("chk_ws") %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="30px" HorizontalAlign="Left" />
                               <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                           <asp:ButtonField DataTextField="chk_drumcode" HeaderText="DRUM" ShowHeader="True" CommandName="SELECT" Text="DRUM" >
                           <HeaderStyle Width="50px" HorizontalAlign="Left" />
                           <ItemStyle HorizontalAlign="Left" />
                           </asp:ButtonField>
                           <asp:TemplateField HeaderText="Chemname">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_chemname" runat="server" Text='<%# Eval("chemname") %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="60px" HorizontalAlign="Left" />
                               <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="庫存量">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_stk_qty" runat="server" Text='<%# Eval("stk_qty") %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="30px" HorizontalAlign="Right" />
                               <ItemStyle HorizontalAlign="Right" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="實盤數">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_chk_qty" runat="server" Text='<%# Eval("chk_qty") %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="30px" HorizontalAlign="Right" />
                               <ItemStyle HorizontalAlign="Right" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Userid">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_userid" runat="server" Text='<%# Eval("userid") %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="30px" HorizontalAlign="Left" />
                               <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="核帳時間">
                               <ItemTemplate>
                                   <asp:Label ID="lbl_rectime" runat="server" Text='<%# String.Format("{0:HH:mm:ss}", Eval("rectime")) %>' Width="95%"></asp:Label>
                               </ItemTemplate>
                               <HeaderStyle Width="50px" HorizontalAlign="Center" />
                               <ItemStyle HorizontalAlign="Center" />
                           </asp:TemplateField>
                       </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" />
                       <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                       <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                       <RowStyle ForeColor="#000066" />
                       <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                   </asp:GridView>
                   </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
                       </Triggers>
                   </asp:UpdatePanel>

               </div>
               <div class="browser_right">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                   <ContentTemplate>

                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" GridLines="Vertical"
    Style="line-height: 20px; table-layout:fixed;" Font-Names="細明體" Font-Size="Small" ForeColor="Black" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px">
    <RowStyle BackColor="#F7F7DE" />
    <FooterStyle BackColor="#CCCC99" />
    <PagerStyle BackColor="#F7F7DE" HorizontalAlign="Right" ForeColor="Black" />
    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <EmptyDataTemplate>
        Sorry, No any data.
    </EmptyDataTemplate>
    <Columns>
        <asp:TemplateField HeaderText="Drumcode" >
            <ItemTemplate>
                <asp:Label ID="lbl_chk_drumcode" runat="server" Text='<%# Eval("chk_drumcode") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="100px" Height="30px" />
            <ItemStyle HorizontalAlign="Left" Height="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="VendorBatch">
            <ItemTemplate>
                <asp:Label ID="chk_vendorbatch" runat="server" Text='<%# Eval("chk_vendorbatch") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="100px" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Barcode">
            <ItemTemplate>
                <asp:Label ID="lbl_chk_barcode" runat="server" Text='<%# Eval("chk_barcode") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="150px" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="庫存量">
            <ItemTemplate>
                <asp:Label ID="lbl_stk_qty" runat="server" Text='<%# Eval("Stk_qty") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" Width="50px" />
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="實點數">
            <ItemTemplate>
                <asp:Label ID="lbl_chk_qty" runat="server" Text='<%# Eval("chk_qty") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" Width="50px" />
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="差異數">
            <ItemTemplate>
                <asp:Label ID="lbl_diff" runat="server" Text='<%# string.Format("{0}", int.Parse(Eval("chk_qty").ToString()) - int.Parse(Eval("stk_qty").ToString())) %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" Width="50px" />
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateField>
    </Columns>
                           <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
</asp:GridView>

                   </ContentTemplate>
                   </asp:UpdatePanel>
               </div>
               
           </div>
       </div>
    </div>
    </form>
</body>
</html>
