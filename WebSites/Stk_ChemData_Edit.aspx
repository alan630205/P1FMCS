<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stk_ChemData_Edit.aspx.cs" Inherits="Stk_ChemData_Edit" %>

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
        .Bottom { height:115% ;font-family: 細明體;padding:0px 0px 0px 0px ;margin:0 0 0 0; }
        
        .browser_left {border: 1px solid #000000; overflow: scroll; width:35%; height:100%; float:left; display:inline; font-size: small; background-color: #AAB5CD; font-family: 細明體; color: #AAB5CD;}
        .browser_right {border: 1px solid #000000; overflow-y: scroll; width:64%; height:100%; float:left; display:inline; font-size: small; background-color: #AAB5CD; font-family: 細明體; color: #AAB5CD;}
        .browser_message{border: 1px solid #000000; width:100%; height:35Px; float:left; display:inline; font-size: small; background-color: #FFD4D4; font-family: 細明體; color: #AAB5CD;}
        .auto-style3 {
            height: 45px;background-color:pink;
        }
        .auto-stylea {
            font-family: 細明體; font-size: small; text-align: left;width: 35%;height:30px;
        }
        .auto-styleb {
            font-family: 細明體; font-size: small; text-align: left;width: 64%;height:30px;
        }

        
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div class="outer">
       <div class="TOP">
            <span>ChemName化學品使用料號編輯作業</span>
       </div>
       <div id="div1" class="Bottom">

            <table style="width:100%; font-family: 細明體; font-size: small; height:45px;border-color:#bd77db">
            <tr style="height:45px;">
                <td colspan="4" class="auto-style3" >
                    <asp:Label ID="Label1" runat="server" Text="系統別："></asp:Label>
                    <asp:DropDownList ID="DDL_depa" runat="server" Font-Names="細明體" Font-Size="small" Width="55px">
                        <asp:ListItem Value="CDS">CDS</asp:ListItem>
                        <asp:ListItem Value="SDS">SDS</asp:ListItem>
                        <asp:ListItem Value="GMS">GMS</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                <td colspan="2" class="auto-style3">
                    <asp:Button ID="Btn_ok" runat="server" Font-Names="細明體" Font-Size="small" Text="確定" OnClick="Btn_ok_Click" />
                    </td>
            </tr>
            </table>
           <div style="height:70%; width:100%">
                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                OnRowEditing="gv_RowEditing" OnRowCancelingEdit="gv_RowCancelingEdit"
                                OnRowUpdating="gv_RowUpdating" BackColor="#DDDDDD" BorderStyle="None"
                                BorderWidth="1px" CellPadding="5" CellSpacing="1" GridLines="None"
                                Style="line-height: 22px; width: 100%;table-layout:fixed;" onrowdeleting="gv_RowDeleting" 
                                AllowPaging="True" onpageindexchanging="gv_PageIndexChanging" EnableModelValidation="True" OnRowCreated="GridView2_RowCreated" PageSize="12" Font-Size="Small">
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
                                    <asp:LinkButton ID="lbInsert" runat="server" Width="70px" onclick="lbInsert_Click">新增</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit" Text='編輯'></asp:LinkButton> - <asp:LinkButton ID="lbDelete" runat="server" 
                                        OnClientClick="javascript:return confirm('確定刪除?')" CommandName="Delete">刪除</asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update">更新</asp:LinkButton> - <asp:LinkButton ID="lbCancelUpdate" runat="server" 
                                        CommandName="Cancel">取消</asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbSave" runat="server" onclick="lbSave_Click">儲存</asp:LinkButton> - <asp:LinkButton ID="lbCancelSave" runat="server" 
                                        onclick="lbCancelSave_Click">取消</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="False" />
                                    <HeaderStyle Wrap="False" Width="100px" />
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="庫位">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblws_edit" runat="server" Text='<%# Eval("ws") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbws_Footer" runat="server" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblws" runat="server" Text='<%# Eval("ws") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="ChemName" SortExpression="ChemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchemname" runat="server" Text='<%# Eval("chemname") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblchemname_Edit" runat="server" Text='<%# Eval("chemname") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="tbchemname_Footer" runat="server" Text="" Wrap="False" Width="80%"></asp:TextBox>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="False" />
                                    <HeaderStyle Wrap="False" Width="150px" />
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DrumCode" SortExpression="DrumCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldrumcode" runat="server" Text='<%# Eval("drumcode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbldrumcode_Edit" runat="server" Text='<%# Eval("drumcode") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="tbdrumcode_Footer" runat="server" Text="" Wrap="False" Width="80%"></asp:TextBox>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="False" />
                                    <HeaderStyle Wrap="False" Width="100px" />
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PartNo" SortExpression="PartNo" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartno" runat="server" Text='<%# Eval("partno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblpartno_Edit" runat="server" Text='<%# Eval("partno") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="tbpartno_Footer" runat="server" Text="" Wrap="False" Width="80%"></asp:TextBox>
                                    </FooterTemplate>
                                    <FooterStyle Wrap="False" />
                                    <HeaderStyle Wrap="False" Width="100px" />
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="示警(1)" SortExpression="D_Day_1">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbDay1_Edit" runat="server" Text='<%# Eval("D_Day_1") %>' Wrap="False" Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbDay1_Footer" runat="server" Wrap="False" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbDay1" runat="server" Text='<%# Eval("D_Day_1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" />
                                        <HeaderStyle Wrap="True" Width="30px" />
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="示警(2)" SortExpression="D_Day_2">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbDay2_Edit" runat="server" Text='<%# Eval("D_Day_2") %>' Wrap="False" Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbDay2_Footer" runat="server" Wrap="False" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Day2" runat="server" Text='<%# Eval("D_Day_2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" />
                                        <HeaderStyle Wrap="True" Width="30px" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="示警(3)" SortExpression="D_Day_3">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbDay3_Edit" runat="server" Text='<%# Eval("D_Day_3") %>' Wrap="False" Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbDay3_Footer" runat="server" Wrap="False" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lb_Day3" runat="server" Text='<%# Eval("D_Day_3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False" />
                                        <HeaderStyle Wrap="True" Width="30px" />
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DrumCode比對順序" SortExpression="Chk_Seq" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbChkseq_Edit" runat="server" Text='<%# Eval("Chk_Seq") %>' Wrap="False" Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbChkSeq_Footer" runat="server" Wrap="False" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbChkseq" runat="server" Text='<%# Eval("Chk_Seq") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="True" />
                                        <HeaderStyle Wrap="True" Width="10px" />
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="批號位置" SortExpression="VendorBatch_Seq">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbVendorbatchseq_Edit" runat="server" Text='<%# Eval("VendorBatch_seq") %>' Wrap="False" Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbVendorbatchseq_Footer" runat="server" Wrap="False" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbVendorbatchseq" runat="server" Text='<%# Eval("Vendorbatch_seq") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="True" />
                                        <HeaderStyle Wrap="True" Width="30px" />
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserID" SortExpression="userid">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbuserid_Edit" runat="server" Text='<%# Eval("userid") %>' Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbuserid_Footer" runat="server" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Barcode" SortExpression="barcode">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbbarcode_Edit" runat="server" Text='<%# Eval("barcode") %>' Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbbarcode_Footer" runat="server" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblbarcode" runat="server" Text='<%# Eval("Barcode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AREANAME" SortExpression="AreaName">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbAreaName_Edit" runat="server" Text='<%# Eval("AreaName") %>' Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbAreaName_Footer" runat="server" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("AreaName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PU" SortExpression="PU_Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbPUName_Edit" runat="server" Text='<%# Eval("PU_Name") %>' Width="80%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbPUName_Footer" runat="server" Width="80%"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPUName" runat="server" Text='<%# Eval("PU_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                       </asp:GridView>
               </div>
       </div>
    </div>
    </form>
</body>
</html>
