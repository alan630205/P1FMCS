<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_SUP_WarningValue_Edit.aspx.cs" Inherits="T_SUP_WarningValue_Edit" %>

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

        form {
            height: 100%;
            margin: 0 0 0 0;
            margin-bottom: 0px;
        }

        .outer {
            height: 100%;
            padding: 100px 0 0 0;
            box-sizing: border-box;
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
            margin: 0 0 0 0;
        }

        .browser_left {
            border: 1px solid #000000;
            overflow: scroll;
            width: 35%;
            height: 100%;
            float: left;
            display: inline;
            font-size: Medium;
            background-color: #AAB5CD;
            font-family: 細明體;
            color: #AAB5CD;
        }

        .browser_right {
            border: 1px solid #000000;
            overflow-y: scroll;
            width: 64%;
            height: 100%;
            float: left;
            display: inline;
            font-size: Medium;
            background-color: #AAB5CD;
            font-family: 細明體;
            color: #AAB5CD;
        }

        .browser_message {
            border: 1px solid #000000;
            width: 100%;
            height: 35Px;
            float: left;
            display: inline;
            font-size: Medium;
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
            font-size: Medium;
            text-align: left;
            width: 35%;
            height: 30px;
        }

        .auto-styleb {
            font-family: 細明體;
            font-size: Medium;
            text-align: left;
            width: 64%;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="outer">
            <div class="TOP">
                <span>T_SUP 警戒值設定作業</span>
            </div>
            <div id="div1" class="Bottom">

                <table style="width: 100%; font-family: 細明體; font-size: Medium; height: 45px; border-color: #bd77db">
                    <tr style="height: 45px;">
                        <td colspan="4" class="auto-style3">

                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label1" runat="server" Text="NODE："></asp:Label>
                                <asp:DropDownList ID="DDL_NODE" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DDL_NODE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label2" runat="server" Text="SYSTEM："></asp:Label>
                                <asp:DropDownList ID="DDL_SYSTEM" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100px" OnSelectedIndexChanged="DDL_SYSTEM_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label3" runat="server" Text="VALVA："></asp:Label>
                                <asp:DropDownList ID="DDL_VALVE" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DDL_VALVE_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 250px; float: left">
                                <asp:Label ID="Label4" runat="server" Text="POU："></asp:Label>
                                <asp:DropDownList ID="DDL_POU" runat="server" Font-Names="細明體" Font-Size="Medium" Width="100px">
                                </asp:DropDownList>
                            </div>

                        </td>
                        <td colspan="2" class="auto-style3">
                            <asp:Button ID="Btn_ok" runat="server" Font-Names="細明體" Font-Size="Medium" Text="確定" OnClick="Btn_ok_Click" />
                        </td>
                    </tr>
                </table>
                <div style="height: 70%; width: 100%">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                        OnRowEditing="gv_RowEditing" OnRowCancelingEdit="gv_RowCancelingEdit"
                        OnRowUpdating="gv_RowUpdating" BackColor="#DDDDDD" BorderStyle="None"
                        BorderWidth="1px" CellPadding="5" CellSpacing="1" GridLines="None"
                        Style="line-height: 22px; width: 100%; table-layout: fixed;" OnRowDeleting="gv_RowDeleting"
                        AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowCreated="GridView2_RowCreated" PageSize="12">
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
                                    <asp:LinkButton ID="lbInsert" runat="server" Width="70px" OnClick="lbInsert_Click">新增</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit">編輯</asp:LinkButton>
                                    -
                                    <asp:LinkButton ID="lbDelete" runat="server"
                                        OnClientClick="javascript:return confirm('確定刪除?')" CommandName="Delete">刪除</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update">更新</asp:LinkButton>
                                    -
                                    <asp:LinkButton ID="lbCancelUpdate" runat="server"
                                        CommandName="Cancel">取消</asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbSave" runat="server" OnClick="lbSave_Click">儲存</asp:LinkButton>
                                    -
                                    <asp:LinkButton ID="lbCancelSave" runat="server"
                                        OnClick="lbCancelSave_Click">取消</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="70px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enable">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="cbenable_Edit" runat="server" Checked='<%# Bind("chk_enable") %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="cbenable_footer" runat="server" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbenable" runat="server" Checked='<%# Bind("chk_enable") %>' Enabled="False" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Node">
                                <ItemTemplate>
                                    <asp:Label ID="lblnode" runat="server" Text='<%# Eval("F_NODE_NAME") %>' Width="90%"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbnode_Footer" runat="server" Text="" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="70px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="System">
                                <ItemTemplate>
                                    <asp:Label ID="lblsystem" runat="server" Text='<%# Eval("F_SYSTEM_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbsystem_Footer" runat="server" Text="" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="70px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALVE">
                                <ItemTemplate>
                                    <asp:Label ID="lblvalve" runat="server" Text='<%# Eval("F_VALVE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbvalve_Footer" runat="server" Text="" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="50px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="POU">
                                <FooterTemplate>
                                    <asp:TextBox ID="tbpou_Footer" runat="server" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblpou" runat="server" Text='<%# Eval("F_POU_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" Width="100px" />
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TagName">
                                <FooterTemplate>
                                    <asp:TextBox ID="tbtagname_Footer" runat="server" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbltagname" runat="server" Text='<%# Eval("F_TAGNAME") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" Width="100px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Times">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbtimes_Edit" runat="server" OnKeyPress="return txtKeyNumber();" Text='<%# Eval("Times_value") %>' Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbtimes_Footer" runat="server" OnKeyPress="return txtKeyNumber();" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbtimes" runat="server" Text='<%# Eval("Times_value") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" Width="30px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seconds">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbsecond_Edit" runat="server" OnKeyPress="return txtKeyNumber();" Text='<%# Eval("Second_Value") %>' Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbsecond_Footer" runat="server" OnKeyPress="return txtKeyNumber();" Wrap="False" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblseconds" runat="server" Text='<%# Eval("Second_Value") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="True" />
                                <HeaderStyle Wrap="True" Width="30px" />
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AREANAME" SortExpression="AreaName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbAreaName_Edit" runat="server" Text='<%# Eval("AreaName") %>' Width="100%" Height="90%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbAreaName_Footer" runat="server" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("AreaName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PU" SortExpression="PU_Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbPUName_Edit" runat="server" Text='<%# Eval("PU_Name") %>' Width="100%" Height="90%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbPUName_Footer" runat="server" Width="100%" Height="90%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPUName" runat="server" Text='<%# Eval("PU_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
