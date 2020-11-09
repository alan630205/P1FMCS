<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_ALARM_SETUP.aspx.cs" Inherits="STK_ALARM_SETUP" %>

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
                width: 49%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="outer">
       <div class="TOP">
            警報訊息設定作業
       </div>
       <div id="div1" class="Button">

            <table style="width:100%; font-family: 細明體; font-size: Medium; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style4" >
                    訊息標籤說明：<br />
&nbsp; (DATETIME) ：警報發送日期時間，格式為 時:分:秒<br />
&nbsp; (PARTNO) ：物料編號<br />
&nbsp; (VENDORBATCH) ：批號<br />
&nbsp; (EXPIRATIONDATE) ：物料效期<br />
&nbsp; (EXPDAYS) ：效期剩餘天數<br />
                </td>
                <td colspan="2" class="auto-style3">
&nbsp; (TAGNAME) ：TAGNAME，格式為 庫位_化學品名稱/GAS_EXPALARM_警報代號<br />
&nbsp; (CHEMNAME) ：化學品名稱/GAS名稱</td>
            </tr>
            </table>
           <div style="height:70%; width:100%">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="line-height: 22px; width: 100%;table-layout:fixed;" BackColor="#DDDDDD" CellPadding="5" EnableModelValidation="True" GridLines="None" PageSize="12" AllowPaging="True" BorderWidth="1px" CellSpacing="1" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCreated="GridView1_RowCreated">
                   <AlternatingRowStyle BackColor="White" />
                   <Columns>
                       <asp:TemplateField>
                           <EditItemTemplate>
                               <asp:LinkButton ID="lbOK" runat="server" CommandName="Update">確定</asp:LinkButton>
                               &nbsp;-
                               <asp:LinkButton ID="lb_Cancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:LinkButton ID="lbedit" runat="server" CommandName="Edit">修改</asp:LinkButton>
                           </ItemTemplate>
                           <HeaderStyle Width="10%" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="代號" SortExpression="alarm_no">
                           <EditItemTemplate>
                               <asp:Label ID="lblalarmno_Edit" runat="server" Text='<%# Eval("alarm_no") %>'></asp:Label>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblalarmno" runat="server" Text='<%# Eval("alarm_no") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="5%" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="警報觸發時機" SortExpression="alarm_desc">
                           <ItemTemplate>
                               <asp:Label ID="lblalarmdesc" runat="server" Text='<%# Eval("alarm_desc") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="10%" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="MAIL(主旨)" SortExpression="mail_subject">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_mailsubject" runat="server" Text='<%# Eval("mail_subject") %>' Width="97%" Font-Names="細明體" Font-Size="Medium" TextMode="MultiLine"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblmailsubject" runat="server" Text='<%# Eval("mail_subject") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="20%" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="MAIL(內文)" SortExpression="mail_body">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_mailbody" runat="server" Text='<%# Eval("mail_body") %>' Width="97%" Font-Names="細明體" Font-Size="Medium" TextMode="MultiLine"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblmailbody" runat="server" Text='<%# Eval("mail_body") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="30%" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="簡訊內文" SortExpression="sms">
                           <EditItemTemplate>
                               <asp:TextBox ID="txb_sms" runat="server" Text='<%# Eval("sms") %>' Width="97%" Font-Names="細明體" Font-Size="Medium" TextMode="MultiLine"></asp:TextBox>
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:Label ID="lblsms" runat="server" Text='<%# Eval("sms") %>' Width="100%"></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="20%" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Confirm" SortExpression="alarm_confirm">
                           <EditItemTemplate>
                               <asp:CheckBox ID="cbconfirm_EDIT" runat="server" Checked='<%# Eval("alarm_confirm") %>' />
                           </EditItemTemplate>
                           <ItemTemplate>
                               <asp:CheckBox ID="cbconfirm" runat="server" Checked='<%# Eval("alarm_confirm") %>' Enabled="False" />
                           </ItemTemplate>
                           <HeaderStyle Width="5%" />
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
