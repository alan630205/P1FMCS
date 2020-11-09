<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_ALARM_CONFIRM.aspx.cs" Inherits="STK_ALARM_CONFIRM" %>

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
            height: 45px;background-color:pink;width: 49%;
        }
        .auto-stylea {
            font-family: 細明體; font-size: Medium; text-align: left;width: 35%;height:30px;
        }
        .auto-styleb {
            font-family: 細明體; font-size: Medium; text-align: left;width: 64%;height:30px;
        }
            .auto-style4 {
                height: 45px;
                background-color: pink;
                width: 76%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="outer">
       <div class="TOP">
            警報訊息 CONFIRM 作業
       </div>
       <div id="div1" class="Button">

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style4" >
                    <asp:Label ID="Label1" runat="server" Text="系統別："></asp:Label>
                    <asp:DropDownList ID="DDL_depa" runat="server" Font-Names="細明體" Font-Size="Medium" Height="24px" Width="80px">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="警報代號："></asp:Label>
                    <asp:DropDownList ID="DDL_msgtype" runat="server" Font-Names="細明體" Font-Size="Medium" Height="24px" Width="300px">
                    </asp:DropDownList>
                    資料別：<asp:DropDownList ID="DLL_confirm" runat="server" Font-Names="細明體" Font-Size="Medium" Height="24px" Width="90px">
                        <asp:ListItem Value="0">未確認</asp:ListItem>
                        <asp:ListItem Value="1">已確認</asp:ListItem>
                        <asp:ListItem Value="ALL">全部</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="auto-style3">
                    <asp:Button ID="Button1" runat="server" Font-Names="細明體" Font-Size="Medium" OnClick="Button1_Click" Text="查詢" />
                </td>
            </tr>
            </table>
           <div style="height:70%; width:100%">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="line-height: 22px; width: 100%;table-layout:fixed;" BackColor="#DDDDDD" CellPadding="5" EnableModelValidation="True" GridLines="None" PageSize="12" AllowPaging="True" BorderWidth="1px" CellSpacing="1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Font-Names="細明體" Font-Size="Small" OnRowCreated="GridView1_RowCreated">
                   <AlternatingRowStyle BackColor="White" />
                   <Columns>
                       <asp:TemplateField>
                           <EditItemTemplate>
                               <asp:LinkButton ID="lbOK" runat="server" CommandName="Update">確定</asp:LinkButton>
                               &nbsp;-
                               <asp:LinkButton ID="lb_Cancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:LinkButton ID="lbedit" runat="server" CommandName="Edit">警報確認</asp:LinkButton>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="日期時間">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_datetime_edit" runat="server" Text='<%# String.Format("{0:yyyy/MM/dd HH:mm:ss}", Eval("alm_datetime")) %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_datetime" runat="server" Text='<%# String.Format("{0:yyyy/MM/dd HH:mm:ss}", Eval("alm_datetime")) %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="80px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="警報代號">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_alm_serialno_edit" runat="server" Text='<%# Eval("alm_serialno") %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_alm_serialno" runat="server" Text='<%# Eval("alm_serialno") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="警報說明">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_alm_desc" runat="server" Text='<%# Eval("alm_desc") %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_alm_desc" runat="server" Text='<%# Eval("alm_desc") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="100px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="警報主旨">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_mailsubject_edit" runat="server" Text='<%# Eval("alm_mailsubject") %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_mailsubject" runat="server" Text='<%# Eval("alm_mailsubject") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="150px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="警報內文">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_mailbody_edit" runat="server" Text='<%# Eval("alm_mailbody") %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_mailbody" runat="server" Text='<%# Eval("alm_mailbody") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="200px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="確認說明">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_confirm_description_edit" runat="server" Text='<%# Eval("Confirm_Description") %>' Width="95%"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_confirm_description" runat="server" Text='<%# Eval("Confirm_Description") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="150px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="確認時間">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_confirm_datetime_edit" runat="server" Text='<%# String.Format("{0:yyyy/MM/dd HH:mm:ss}", Eval("confirm_datetime")) %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_confirm_datetime" runat="server" Text='<%# String.Format("{0:yyyy/MM/dd HH:mm:ss}", Eval("confirm_datetime")) %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="80px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="確認人員">
                           <EditItemTemplate>
                               <asp:Label ID="lbl_confirm_user_edit" runat="server" Text='<%# Eval("confirm_user") %>' Width="95%"></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lbl_confirm_user" runat="server" Text='<%# Eval("confirm_user") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="重送次數">
                           <ItemTemplate>
                               <asp:Label ID="lbl_send_times" runat="server" Text='<%# Eval("send_times") %>' Width="95%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="50px" />
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
