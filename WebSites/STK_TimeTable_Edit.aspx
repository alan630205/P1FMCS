<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STK_TimeTable_Edit.aspx.cs" Inherits="STK_TimeTable_Edit" %>

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
                TCGM 班別<span> 設定作業</span>
            </div>
            <div id="div1" class="Bottom">
                <div style="height: 70%; width: 100%">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                        OnRowEditing="Gv_RowEditing" OnRowCancelingEdit="Gv_RowCancelingEdit"
                        OnRowUpdating="Gv_RowUpdating" BackColor="#DDDDDD" BorderStyle="None"
                        BorderWidth="1px" CellPadding="5" CellSpacing="1" GridLines="None"
                        Style="line-height: 22px; width: 100%; table-layout: fixed; margin-right: 0px;" OnRowDeleting="Gv_RowDeleting"
                        AllowPaging="True" OnPageIndexChanging="Gv_PageIndexChanging" OnRowCreated="GridView2_RowCreated" PageSize="12" Font-Size="Small">
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
                                    -<asp:LinkButton ID="LbCancelSave" runat="server"
                                        OnClick="LbCancelSave_Click">取消</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="75px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="RowId" HeaderText="RowId" ReadOnly="True">
                            <HeaderStyle Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="班別代號">
                                <ItemTemplate>
                                    <asp:Label ID="lbTimeId" runat="server" Text='<%# Eval("TimeId") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbTimeId_edit" runat="server" Text='<%# Eval("TimeId") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbTimeId_Footer" runat="server" Text="" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="班別名稱">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbTimeName_Edit" runat="server" Text='<%# Eval("TimeName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbTimeName_Footer" runat="server" Text="" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbTimeName" runat="server" Text='<%# Eval("TimeName") %>' Width="90%"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Width="50px" HorizontalAlign="Left" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="起算(時)">
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbS_Hour_edit" runat="server" OnKeyPress="return txtKeyNumber();" Text='<%# Eval("S_Hour") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbS_Hour_Footer" runat="server" OnKeyPress="return txtKeyNumber();" Wrap="False" Text=""></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbS_Hour" runat="server" Text='<%# Eval("S_Hour") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="截止(時)">
                                <ItemTemplate>
                                    <asp:Label ID="lbE_Hour" runat="server" Text='<%# Eval("E_Hour") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbE_Hour_edit" runat="server" OnKeyPress="return txtKeyNumber();" Text='<%# Eval("E_Hour") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbE_Hour_Footer" runat="server" OnKeyPress="return txtKeyNumber();" Wrap="False"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Wrap="False" />
                                <HeaderStyle Wrap="True" Width="100px" HorizontalAlign="Left" />
                                <ItemStyle Wrap="True" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="認列日">
                                <ItemTemplate>
                                    <asp:Label ID="lbRec_Day" runat="server" Text='<%# Eval("Rec_Day") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbRec_Day_Edit" runat="server" OnKeyPress="return txtKeyNumber();" Text='<%# Eval("Rec_Day") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbRec_Day_Footer" runat="server" OnKeyPress="return txtKeyNumber();"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
