<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GAS_Usage_EDIT.aspx.cs" Inherits="GAS_Usage_EDIT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function txtKeyNumber() {
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
        html, body {
            height: 100%;
            padding: 0 0 0 0;
            margin: 0;
            background-color: #AAB5CD;
        }

        .outer {
            height: 100%;
            padding: 100px 0 0 0;
            box-sizing: content-box;
        }

        .TOP {
            font-size: x-large;
            height: 35px;
            font-family: 細明體;
            line-height: 35px;
            text-align: center;
            margin: -100px 0 0 0;
            background-color: #333399;
            color: #00FFFF;
        }

        .Bottom {
            height: 115%;
            font-family: 細明體;
            padding: 0px 0px 0px 0px;
            margin: 0;
        }

        .browser_left {
            border: 1px solid #000000;
            overflow: scroll;
            width: 35%;
            height: 100%;
            float: left;
            display: inline;
            font-size: small;
            background-color: #AAB5CD;
            font-family: 細明體;
            color: #AAB5CD;
            box-sizing: content-box;
        }

        .browser_right {
            border: 1px solid #000000;
            overflow-y: scroll;
            width: 64%;
            height: 100%;
            float: left;
            display: inline;
            font-size: small;
            background-color: #AAB5CD;
            font-family: 細明體;
            color: #AAB5CD;
            box-sizing: content-box;
        }

        .browser_message {
            border: 1px solid #000000;
            width: 100%;
            height: 35Px;
            float: left;
            display: inline;
            font-size: small;
            background-color: #FFD4D4;
            font-family: 細明體;
            color: #AAB5CD;
        }

        .auto-style3 {
            height: 45px;
            background-color: pink;
        }

        .auto-stylea {
            font-family: 細明體;
            font-size: small;
            text-align: left;
            width: 35%;
            height: 30px;
        }

        .auto-styleb {
            font-family: 細明體;
            font-size: small;
            text-align: left;
            width: 64%;
            height: 30px;
        }

        .Btn_ok {
            box-shadow: 0px 0px 0px 2px #9fb4f2;
            background: linear-gradient(to bottom, #7892c2 5%, #476e9e 100%);
            background-color: #7892c2;
            border-radius: 10px;
            border: 1px solid #4e6096;
            display: inline-block;
            cursor: pointer;
            color: #ffffff;
            font-family: Arial;
            font-size: 19px;
            padding: 12px 37px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #283966;
        }
        .Btn_ok:hover {
            background: linear-gradient(to bottom, #476e9e 5%, #7892c2 100%);
            background-color: #476e9e;
        }

        .Btn_ok:active {
            position: relative;
            top: 1px;
        }
        
        input[type="text"] {
            border-radius: 5px;
            width: 100%;
            height: 100%;
            font-size: small;
            margin:0;
            padding:0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="outer">
            <div class="TOP">
                <span>GAS Usage Tag 設定作業</span>
            </div>
            <div id="div1" class="Bottom">

                <table style="width: 100%; font-family: 細明體; font-size: small; height: 45px; border-color: #bd77db">
                    <tr style="height: 45px; width:100%">
                        <td class="auto-style3">

                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label1" runat="server" Text="廠區："></asp:Label>
                                <asp:DropDownList ID="DDL_plant" runat="server" Font-Names="細明體" Font-Size="small" Width="100px" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label2" runat="server" Text="分類："></asp:Label>
                                <asp:DropDownList ID="DDL_category" runat="server" Font-Names="細明體" Font-Size="small" Width="100px">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label4" runat="server" Text="物料："></asp:Label>
                                <asp:DropDownList ID="DDL_gastype" runat="server" Font-Names="細明體" Font-Size="small" Width="100px">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label3" runat="server" Text="UNIT："></asp:Label>
                                <asp:DropDownList ID="DDL_equipname" runat="server" Font-Names="細明體" Font-Size="small" Width="100px" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                        </td>
                        <td class="auto-style3">
                            <asp:Button ID="Btn_ok" runat="server" Font-Names="細明體" Font-Size="small" Text="確定" OnClick="Btn_ok_Click" />
                        </td>
                    </tr>
                </table>
                <div style="height: 70%; width: 100%">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                        OnRowEditing="Gv_RowEditing" OnRowCancelingEdit="Gv_RowCancelingEdit"
                        OnRowUpdating="Gv_RowUpdating" BackColor="#DDDDDD" BorderStyle="None"
                        BorderWidth="1px" CellPadding="5" CellSpacing="1" GridLines="None"
                        Style="line-height: 22px; width: 100%; table-layout: fixed; margin-right: 0px;" OnRowDeleting="Gv_RowDeleting"
                        AllowPaging="True" OnPageIndexChanging="Gv_PageIndexChanging" OnRowCreated="GridView2_RowCreated" PageSize="12" Font-Size="Small" OnRowDataBound="GridView2_RowDataBound">
                        <RowStyle BackColor="#ffffff" ForeColor="Black" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#ffffff" HorizontalAlign="left" />
                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#f7fafe" />
                        <EmptyDataTemplate>
                            Sorry, No any data.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lbInsert" runat="server" Width="70px" OnClick="LbInsert_Click">新增</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit">編輯</asp:LinkButton>
                                    -<asp:LinkButton ID="lbDelete" runat="server"
                                        OnClientClick="javascript:return confirm('確定刪除?')" CommandName="Delete">刪除</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update">更新</asp:LinkButton>
                                    -<asp:LinkButton ID="lbCancelUpdate" runat="server"
                                        CommandName="Cancel">取消</asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbSave" runat="server" OnClick="LbSave_Click">儲存</asp:LinkButton>
                                    -<asp:LinkButton ID="lbCancelSave" runat="server"
                                        OnClick="lbCancelSave_Click">取消</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="75px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Run">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="cbenable_Edit" runat="server" Checked='<%# Bind("rec_enable") %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="cbenable_footer" runat="server" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbenable" runat="server" Checked='<%# Bind("rec_enable") %>' Enabled="False" />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="廠區">
                                <ItemTemplate>
                                    <asp:Label ID="lbplant" runat="server" Text='<%# Eval("plant") %>' Width="90%"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lbplant_edit" runat="server" Text='<%# Eval("plant") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbplant_Footer" runat="server" Text="" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="分類">
                                <ItemTemplate>
                                    <asp:Label ID="lbcategory" runat="server" Text='<%# Eval("category") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lbcategory_edit" runat="server" Text='<%# Eval("category") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbcategory_Footer" runat="server" Text="" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料">
                                <EditItemTemplate>
                                    <asp:Label ID="lbgastype_edit" runat="server" Text='<%# Eval("gas_type") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbgastype_Footer" runat="server" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgastype" runat="server" Text='<%# Eval("gas_type") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" Width="100px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="True" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UNIT">
                                <ItemTemplate>
                                    <asp:Label ID="lbequipname" runat="server" Text='<%# Eval("equipname") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lbequipname_edit" runat="server" Text='<%# Eval("equipname") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbequipname_Footer" runat="server" Text="" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="75px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="計值型態">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlvaluetype" runat="server" Width="100%" OnSelectedIndexChanged="ddlvaluetype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbvaluetype" runat="server" Text='<%# Eval("value_type") %>' Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlvaluetype_footer" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlvaluetype" runat="server" Width="100%" Enabled="False">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbvaluetype" runat="server" Text='<%# Eval("value_type") %>' Visible="False"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="85px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="L1_Value_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbl1valuetagname_Edit" runat="server" Text='<%# Eval("l1_value_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbl1valuetagname_Footer" runat="server" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl1valuetagname" runat="server" Text='<%# Eval("l1_value_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" HorizontalAlign="Left" Width="160px" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="L2_Value_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbl2valuetagname_Edit" runat="server" Text='<%# Eval("l2_value_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbl2valuetagname_Footer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbl2valuetagname" runat="server" Text='<%# Eval("l2_value_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="L_Status_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tblstatustagname_Edit" runat="server" Text='<%# Eval("l_status_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tblstatustagname_Footer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblstatustagname" runat="server" Text='<%# Eval("l_status_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R1_Value_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbr1valuetagname_Edit" runat="server" Text='<%# Eval("r1_value_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbr1valuetagname_Footer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbr1valuetagname" runat="server" Text='<%# Eval("r1_value_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R2_Value_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbr2valuetagname_Edit" runat="server" Text='<%# Eval("r2_value_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbr2valuetagname_Footer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbr2valuetagname" runat="server" Text='<%# Eval("r2_value_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R_Status_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbrstatustagname_Edit" runat="server" Text='<%# Eval("r_status_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbrstatustagname_Footer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbrstatustagname" runat="server" Text='<%# Eval("r_status_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M1_Value_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbm1valuetagname_Edit" runat="server" Text='<%# Eval("m1_value_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbm1valuetagname_Footer" runat="server" Text='<%# Eval("r2_value_tagname") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbm1valuetagname" runat="server" Text='<%# Eval("m1_value_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="160px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M2_Value_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbm2valuetagname_Edit" runat="server" Text='<%# Eval("m2_value_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbm2valuetagname_Footer" runat="server" Text='<%# Eval("r2_value_tagname") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbm2valuetagname" runat="server" Text='<%# Eval("m2_value_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="160px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M_Status_Tag">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbmstatustagname_Edit" runat="server" Text='<%# Eval("m_status_tagname") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbmstatustagname_Footer" runat="server" Text='<%# Eval("r2_value_tagname") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbmstatustagname" runat="server" Text='<%# Eval("m_status_tagname") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="160px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
