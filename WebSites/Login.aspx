<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<!--	 
<script type="text/javascript" src="js/jquery-latest.min.js"></script>
<script type="text/javascript" src="js/jqDnR.js"></script>
<script type="text/javascript" src="js/jkpanel.js"></script>
-->
    <title>P3FMCS Report - Login</title>
    <style type="text/css">
        .style1
        {
            font-family: Tahoma;
        }
        .style2
        {
            font-family: Tahoma;
            font-weight: bold;
        }
/*
	.jqDnR {z-index: 3;position: relative;width: 180px;font-size: 0.77em;color: #618d5e;margin: 5px 10px 10px 10px;padding: 8px;background-color: #EEE;border: 1px solid #CCC;top: 0px;left: 189px;}
	.jqDrag {width: 20%;cursor: move; }
    #dropdownpanel{position: absolute;width: 100%;left: 0;top: 0;visibility:hidden;}
	#dropdownpanel .contentdiv{background: black;color: white;padding: 10px;}
	#dropdownpanel .control{border-top: 5px solid black;color: white;	font-weight: bold;text-align: center;background: transparent url("img/panel.gif") center center no-repeat;	padding-bottom: 3px;height: 21px;line-height: 21px;}
*/
	    .style3
        {
            height: 77px;
        }

	</style>
<!--
	<script type="text/javascript">
	 	$(function(){$('#ex2').css('opacity', 0.6).jqDrag();});
        $(function(){jkpanel.controltext = "熱門消息";jkpanel.init('panelcontent.htm', '200px', 500);});
    </script>
-->
</head>
<body bgcolor="#a0a0c0">
		<form id="frmlogin" method="post" runat="server">
		<!-- |||||	Login Form	||||| -->
			<table id="mainTable" align="center">
				<tr>
					<td>
						<table id="loginTable" cellspacing="15" cellpadding="0" align="center">
							<tr>
								<td><b><span class="style1">Login ID:</span> </b>
								</td>
								<td><asp:textbox id="txtUserName" runat="server" width="160px" 
                                        style="font-family: Tahoma"></asp:textbox><asp:requiredfieldvalidator id="rvUserValidator" runat="server" display="None" errormessage="請輸入USER ID!" controltovalidate="txtUserName"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="style2" >Password: 
								</td>
								<td><asp:textbox id="txtPassword" runat="server" width="160px" textmode="Password" 
                                        style="font-family: Tahoma">mitac</asp:textbox><asp:requiredfieldvalidator id="rvPasswordValidator" runat="server" display="None" errormessage="密碼不可為空值" controltovalidate="txtPassword"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td align="center" colspan="2" class="style3">
                                    <asp:button id="btnLogon" runat="server" 
                                        Text="登入" onclick="btnLogon_Click" 
                                        style="font-size: large; font-weight: 700"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="text-align: center">
						<table id="messageDisplay">
							<tr>
								<td><asp:validationsummary id="Validationsummary1" runat="server" width="381px" Font-Size="Larger"></asp:validationsummary></td>
							</tr>
							<tr>
								<td style="text-align: center">&nbsp;</td>
							</tr>
						</table>
						<asp:label id="lblMessage" runat="server" width="184px" forecolor="Red" font-size="Larger"> </asp:label></td>
				</tr>
			</table>
		<!--	|||||    End of Form	|||||    -->
		</form>
<!--		
		<div id="ex2" class="jqDnR jqDrag">
            我是一個展示拖移用的區塊<br />
            您可以隨意的&quot;移動&quot;我<br />
            注意唷~我本身的透明度效果一樣是存在的 
        </div>
-->           
    	</body>
</html>



