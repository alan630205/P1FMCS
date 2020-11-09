
Partial Class Menu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUserName.Text = "使用者：" & Session("UserName")
        lblLoginTime.Text = "登入時間：" & Now().ToString("yyyy/MM/dd HH:mm:ss")
        'If Session("IsLogin") Then Call LoadMenu()
        Call LoadMenu()
    End Sub

    Private Sub LoadMenu()
        Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
        Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

        v_dbConn.Open()

        Dim v_strUserSQL As String = "declare @LoginName varchar(32),@Dept varchar(32) " & _
            "select @LoginName='{0}',@Dept='{1}' " & _
            "SELECT distinct t.Name,t.File_Name,t.File_Ext,s.Name AS Subname,s.Order_No, t.Order_No " & _
            "  FROM User_Task ut inner join Tasks t on (ut.Task_ID = t.ID) inner join Subsys s ON t.Sys_ID = s.ID " & _
            "  WHERE ut.User_ID in (@LoginName,@Dept) AND t.Sys_ID <> 'SYS' AND t.Enable_Flag = 1 " & _
            "  ORDER BY s.Order_No, t.Order_No "
        Dim v_strAdminSQL As String = "declare @LoginName varchar(32),@Dept varchar(32) " & _
            "select @LoginName='{0}',@Dept='{1}' " & _
            "SELECT distinct t.Name,t.File_Name,t.File_Ext,s.Name AS Subname,s.Order_No, t.Order_No " & _
            "  FROM User_Task ut inner join Tasks t on (ut.Task_ID = t.ID) inner join Subsys s ON t.Sys_ID = s.ID " & _
            "  WHERE ut.User_ID in (@LoginName,@Dept) AND t.Enable_Flag = 1 " & _
            "  ORDER BY s.Order_No, t.Order_No "


        'v_strUserSQL = "SELECT RTRIM(Tasks.Name) AS Name, RTRIM(Tasks.File_Name) AS File_Name, RTRIM(Tasks.File_Ext) AS File_Ext, RTRIM(Subsys.Name) AS Subname, '0' as Menu2 " & _
        '    "FROM User_Task LEFT JOIN Tasks ON User_Task.Task_ID = Tasks.ID " & _
        '    "LEFT JOIN Subsys ON Tasks.Sys_ID = Subsys.ID " & _
        '    "WHERE User_Task.User_ID = 'umc' " & _
        '    "AND Tasks.Enable_Flag = 1 ORDER BY Subsys.Order_No, 5,Tasks.Order_No"

        Dim v_dtMenu As SqlDataReader = Nothing
        Dim v_dtCmd As SqlCommand

        If Session("Role") = "Admin" Then
            v_dtCmd = New SqlCommand(String.Format(v_strAdminSQL, Session("LoginName"), Session("Dept")), v_dbConn)
        Else
            v_dtCmd = New SqlCommand(String.Format(v_strUserSQL, Session("LoginName"), Session("Dept")), v_dbConn)
        End If
        v_dtMenu = v_dtCmd.ExecuteReader()

        Dim v_strSystem As String = ""
        Dim sMenu2 As String = ""
        tvMenu.Nodes.Clear()
        Dim oMenu1 As TreeNode
        Dim oMenu2 As TreeNode
        Try
            While v_dtMenu.Read()
                If v_dtMenu("Subname") <> v_strSystem Then
                    Dim v_NewSystem As New TreeNode
                    v_NewSystem.Text = v_dtMenu("Subname")
                    v_NewSystem.Expanded = False
                    v_NewSystem.SelectAction = TreeNodeSelectAction.Expand

                    'v_NewSystem.NavigateUrl = "http://FE12AZ02:81/reportserver?/P3FMCS/" & v_dtMenu("File_Name") & "&rs:Command=Render&rs:Format=HTML4.0"
                    'v_NewSystem.Target = "main"

                    tvMenu.Nodes.Add(v_NewSystem)
                    oMenu1 = v_NewSystem
                    v_strSystem = v_dtMenu("Subname")


                    '    oMenu2 = Nothing
                    '    sMenu2 = ""
                    '    If v_dtMenu("Menu2").ToString.Trim <> "" Then
                    '        sMenu2 = v_dtMenu("Menu2").ToString.Trim
                    '        oMenu2 = New TreeNode
                    '        oMenu2.Text = sMenu2
                    '        oMenu2.Expanded = False
                    '        oMenu2.SelectAction = TreeNodeSelectAction.Expand
                    '        oMenu1.ChildNodes.Add(oMenu2)
                    '    End If
                    'ElseIf v_dtMenu("Menu2") <> sMenu2 Then
                    '    sMenu2 = v_dtMenu("Menu2").ToString.Trim
                    '    oMenu2 = New TreeNode
                    '    oMenu2.Text = sMenu2
                    '    oMenu2.Expanded = False
                    '    oMenu2.SelectAction = TreeNodeSelectAction.Expand
                    '    oMenu1.ChildNodes.Add(oMenu2)
                End If

                Dim v_Child As New TreeNode
                v_Child.Text = v_dtMenu("Name")
                If v_dtMenu("File_Name") = "#" OrElse v_dtMenu("File_Name").ToString.Trim = "" Then
                    v_Child.SelectAction = TreeNodeSelectAction.None
                Else
                    If v_dtMenu("File_Ext") = "rdl" Then
                        v_Child.NavigateUrl = "http://localhost:81/reportserver?/P1FMCS/" & v_dtMenu("File_Name") & "&rs:Command=Render&rs:Format=HTML4.0"
                        'v_Child.NavigateUrl = "http://localhost:81/ReportServer/Pages/ReportViewer.aspx?/P1FMCS/" & v_dtMenu("File_Name") & "&rs:Command=Render"
                        v_Child.Target = "main"
                    ElseIf v_dtMenu("File_Ext") = "aspx" Then
                        v_Child.NavigateUrl = v_dtMenu("File_Name") & "." & v_dtMenu("File_Ext")
                        v_Child.Target = "main"
                    ElseIf v_dtMenu("File_Ext") = "local" Then
                        v_Child.NavigateUrl = v_dtMenu("File_Name")
                        v_Child.Target = "_blank"
                    End If
                End If

                If IsNothing(oMenu2) Then
                    oMenu1.ChildNodes.Add(v_Child)
                Else
                    oMenu2.ChildNodes.Add(v_Child)
                End If

            End While
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Session("IsLogin") = False
        Session("UserName") = Nothing
        Session("LoginName") = Nothing
        Session("Dept") = Nothing
        Session("Role") = Nothing
        Response.Write("<script>")
        Response.Write("top.location.href='index.htm';")
        Response.Write("</script>")
    End Sub
End Class
