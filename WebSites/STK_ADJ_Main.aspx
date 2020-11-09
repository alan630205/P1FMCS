<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_ADJ_Main.aspx.cs" Inherits="STK_ADJ_Main" %>

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
        .auto-style1 {
            font-family: 細明體; font-size: Medium; text-align: right;width: 100px;
        }
        .auto-style2 {
            font-family: 細明體; font-size: Medium; text-align: left;width: 200px;
        }
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
            物料庫存調帳作業
       </div>
       <div id="div1" class="Button">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:113px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style3" >
                    <asp:Button ID="Btn_add" runat="server" Font-Names="細明體" Font-Size="Medium" Text="新增" OnClick="Btn_add_Click" />&nbsp;
                    <asp:Button ID="Btn_query" runat="server" Font-Names="細明體" Font-Size="Medium" Text="查詢" OnClick="Btn_query_Click" />&nbsp;
                    <asp:Button ID="Btn_del" runat="server" Font-Names="細明體" Font-Size="Medium" Text="刪除" />
                    </td>
                <td colspan="2" class="auto-style3">
                    <asp:Button ID="Btn_ok" runat="server" Font-Names="細明體" Font-Size="Medium" Text="確定" OnClick="Btn_ok_Click" Enabled="False" />&nbsp;
                    <asp:Button ID="Btn_Cancel" runat="server" Font-Names="細明體" Font-Size="Medium" Text="取消" Enabled="False" OnClick="Btn_Cancel_Click" />
                    </td>
            </tr>
            <tr style="height:30px">
                <td class="auto-style1" >系統別：</td>
                <td class="auto-style2" >
                    <asp:DropDownList ID="DDL_depa" runat="server" Font-Names="細明體" Font-Size="Medium" Width="55px" Enabled="False">
                       <asp:ListItem Value="CDS">CDS</asp:ListItem>
                        <asp:ListItem Value="SDS">SDS</asp:ListItem>
                        <asp:ListItem Value="GAS">GAS</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style1" >單據號：</td>
                <td  class="auto-style2">
                   <asp:TextBox ID="txb_PK_serialno" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100px" Enabled="False"></asp:TextBox>
                </td>
                <td class="auto-style1" >系統狀態：</td>
                <td class="auto-style2" >
                    <asp:Label ID="Lab_SYS_STATUS" runat="server" Text="查修" ForeColor="#CC3300"></asp:Label>
                </td>
            </tr>
            <tr style="height:30px">
                <td class="auto-style1">日期：</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txb_pk_date" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100px" Enabled="False"></asp:TextBox>
                </td>
                <td class="auto-style1">備註：</td>
                <td class="auto-style2">
                    <asp:TextBox ID="txb_pk_memo" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100%" Enabled="False"></asp:TextBox>
                </td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr style="height:30px">
                <td class="auto-style1">建檔者:</td>
                <td class="auto-style2">
                    <asp:Label ID="Lab_Create_User" runat="server" Font-Names="細明體" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    <asp:Label ID="Lab_Create_DateTime" runat="server"></asp:Label>
                </td>
                <td class="auto-style1">修改者：</td>
                <td class="auto-style2">
                    <asp:Label ID="Lab_Modi_User" runat="server"></asp:Label>
                    <asp:Label ID="Lab_Modi_DateTime" runat="server"></asp:Label>
                </td>
                <td class="auto-style1" >單據數量：</td>
                <td class="auto-style2" >
                    <asp:Label ID="lab_qty" runat="server" Font-Names="細明體" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
            </Triggers>
            </asp:UpdatePanel>
           <div style="height:70%;">
               <div class="browser_left">

                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                   <ContentTemplate>
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" Font-Names="細明體" Font-Size="Medium" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCreated="GridView1_RowCreated" DataKeyNames="Pk_Serialno" Width="100%" ForeColor="#333333">
                       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                       <Columns>
                           <asp:BoundField DataField="depa" HeaderText="系統別" ReadOnly="True" />
                           <asp:BoundField DataField="pk_date" HeaderText="日期" ReadOnly="True" >
                           <HeaderStyle HorizontalAlign="Left" />
                           <ItemStyle HorizontalAlign="Left" />
                           </asp:BoundField>
                           <asp:ButtonField DataTextField="Pk_Serialno" HeaderText="單據號" ShowHeader="True" CommandName="SELECT" >
                           <HeaderStyle HorizontalAlign="Left" />
                           <ItemStyle HorizontalAlign="Left" />
                           </asp:ButtonField>
                           <asp:BoundField DataField="Pk_Memo" HeaderText="備註" ReadOnly="True" />
                           <asp:BoundField DataField="pk_qty" HeaderText="數量">
                           <HeaderStyle HorizontalAlign="Center" />
                           <ItemStyle HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField DataField="Pk_Create_User" HeaderText="建檔者" ReadOnly="True" />
                           <asp:BoundField DataField="Pk_Create_DateTime" HeaderText="建檔日期" ReadOnly="True" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
                           <asp:BoundField DataField="Pk_Modi_User" HeaderText="修改者" ReadOnly="True" />
                           <asp:BoundField DataField="Pk_Modi_Datetime" HeaderText="修改日期" ReadOnly="True" />
                           <asp:BoundField DataField="Pk_List" HeaderText="單據別" ReadOnly="True" />
                       </Columns>
                       <EditRowStyle BackColor="#999999" />
                       <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                       <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                   </asp:GridView>
                   </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="Btn_ok" EventName="Click" />
                       </Triggers>
                   </asp:UpdatePanel>

               </div>
               <div class="browser_right">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                   <ContentTemplate>

                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
    OnRowEditing="gv_RowEditing" OnRowCancelingEdit="gv_RowCancelingEdit"
    OnRowUpdating="gv_RowUpdating" BackColor="#DDDDDD" BorderStyle="None"
    BorderWidth="1px" CellPadding="5" CellSpacing="1" GridLines="None"
    Style="line-height: 22px; width: 100%;table-layout:fixed;" onrowdeleting="gv_RowDeleting" 
    AllowPaging="True" onpageindexchanging="gv_PageIndexChanging" EnableModelValidation="True">
    <RowStyle BackColor="#ffffff" ForeColor="Black" />
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <PagerStyle BackColor="#ffffff" HorizontalAlign="left" />
    <HeaderStyle BackColor="#efefef" Font-Bold="True" />
    <AlternatingRowStyle BackColor="#f7fafe" />
    <EmptyDataTemplate>
        Sorry, No any data.
    </EmptyDataTemplate>
    <Columns>
        <asp:TemplateField >
            <HeaderTemplate>
                <asp:LinkButton ID="lbInsert" runat="server" Width="70px" 
                onclick="lbInsert_Click">新增</asp:LinkButton>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="lbEdit" runat="server" 
                CommandName="Edit">編輯</asp:LinkButton> 
                |
                <asp:LinkButton ID="lbDelete" runat="server" 
                OnClientClick="javascript:return confirm('確定刪除?')" 
                CommandName="Delete">刪除</asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="lbUpdate" runat="server" 
                CommandName="Update">更新</asp:LinkButton>
                |
                <asp:LinkButton ID="lbCancelUpdate" runat="server" 
                CommandName="Cancel">取消</asp:LinkButton>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:LinkButton ID="lbSave" runat="server" 
                onclick="lbSave_Click">儲存</asp:LinkButton>
                |
                <asp:LinkButton ID="lbCancelSave" runat="server" 
                onclick="lbCancelSave_Click">取消</asp:LinkButton>
            
            </FooterTemplate>
            <HeaderStyle Width="15%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="序" SortExpression="pk_seq">
            <FooterTemplate>
                <asp:Label ID="lblpk_seqFooter" runat="server" Text='<%# Eval("pk_seq") %>'></asp:Label>
            </FooterTemplate>
            <ItemTemplate>
                <asp:Label ID="lblpk_seq" runat="server" 
                Text='<%# Eval("pk_seq") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="lblpk_seqEdit" runat="server" 
                Text='<%# Eval("pk_seq") %>'></asp:Label>
            </EditItemTemplate>
            <HeaderStyle Width="5%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="條碼" SortExpression="pk_barcode">
            <ItemTemplate>
                <asp:Label ID="lblpk_barcode" runat="server" 
                Text='<%# Eval("pk_barcode") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="tbpk_barcodeEdit" runat="server" 
                Text='<%# Eval("pk_barcode") %>' Height="100%" Width="100%" ></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="tbpk_barcodeFooter" runat="server" 
                Text="" Height="100%" Width="100%"></asp:TextBox>
            </FooterTemplate>
            <HeaderStyle Width="30%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PartNo" SortExpression="partno">
            <ItemTemplate>
                <asp:Label ID="lblpk_partno" runat="server" Text='<%# Eval("partno") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="20%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Vendorbatch" SortExpression="vendorbatch">
            <ItemTemplate>
                <asp:Label ID="lblpk_Vendorbatch" runat="server" Text='<%# Eval("Vendorbatch") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="20%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="數量" SortExpression="pk_qty">
            <EditItemTemplate>
                <asp:TextBox ID="tbpk_qtyEdit" runat="server" Text='<%# Eval("pk_qty") %>' Height="100%" Width="100%"></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="tbpk_qtyFooter" runat="server" Text="" Height="100%" Width="100%"></asp:TextBox>
            </FooterTemplate>
            <ItemTemplate>
                <asp:Label ID="lblpk_qty" runat="server" Text='<%# Eval("pk_qty") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="10%" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>

                   </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
                           <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="DataBinding" />
                       </Triggers>
                   </asp:UpdatePanel>
               </div>
               
           </div>
       </div>
    </div>
    </form>
</body>
</html>
