Partial Class Sys_Users
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call DisableEdit()

        If Not Page.IsPostBack Then
            Call LoadUsers()
        End If

        If Not hasWriteRight("Admin") Then
            btnNew.Visible = False
            btnModify.Visible = False
            btnDelete.Visible = False
            btnCancel.Visible = False
        End If

    End Sub

    Private Sub LoadUsers()
        ' Define the ADO.NET objects.
        Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
        Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

        v_dbConn.Open()

        Dim v_strSQL As String = "SELECT * FROM Users order by LoginName"
        Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
        Dim v_dtAdapter As New SqlDataAdapter(v_dtCmd)

        ' Fill the DataSet.
        Dim v_ds As New DataSet()
        v_dtAdapter.Fill(v_ds, "Users")

        ' Perform the binding.
        gvUsers.DataSource = v_ds
        gvUsers.DataBind()

        v_dtAdapter.Dispose()
        v_dtCmd.Dispose()
        v_dbConn.Close()
        v_dbConn.Dispose()

    End Sub

    Private Sub EnableEdit()
        txtDept.Enabled = True
        txtEmail.Enabled = True
        txtLoginName.Enabled = True
        txtPassword.Enabled = True
        txtName.Enabled = True

        rdbManager.Enabled = True
        rdbUser.Enabled = True
        chkEnable.Enabled = True
        chkAD.Enabled = True

        gvUser_Task.Enabled = True
        btnCheckAll.Enabled = True
        btnClearAll.Enabled = True
    End Sub

    Private Sub DisableEdit()
        txtDept.Enabled = False
        txtEmail.Enabled = False
        txtLoginName.Enabled = False
        txtPassword.Enabled = False
        txtName.Enabled = False

        rdbManager.Enabled = False
        rdbUser.Enabled = False
        chkEnable.Enabled = False
        chkAD.Enabled = False

        'gvUser_Task.Enabled = False
        btnCheckAll.Enabled = False
        btnClearAll.Enabled = False
    End Sub

    Private Sub LoadTask(ByVal p_UID As String)
        ' Define the ADO.NET objects.
        Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
        Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

        v_dbConn.Open()

        Dim v_strSQL As String = "select a.ID as ID1,a.Name, cast(case when b.Task_ID is null then 0 else 1 end as bit) as Used, cast(coalesce(b.Write_Flag,0) as bit) as Write_Flag " & _
            "from Tasks a left join User_Task b on (a.ID=b.Task_ID and b.User_ID='" & p_UID & "') " & _
            "inner join subsys c on (a.Sys_ID=c.ID) " & _
            "where a.Enable_Flag=1 " & _
            "order by c.Order_No,a.Order_No,a.ID"
        Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
        Dim v_dtAdapter As New SqlDataAdapter(v_dtCmd)

        ' Fill the DataSet.
        Dim v_ds As New DataSet()
        v_dtAdapter.Fill(v_ds, "Task")

        gvUser_Task.DataSource = v_ds
        gvUser_Task.DataBind()

        'gvUser_Task.Items.Clear()

        'Dim i As Integer
        'For i = 0 To v_ds.Tables("Task").Rows.Count - 1
        '    Dim v_Item As New ListItem
        '    v_Item.Text = v_ds.Tables("Task").Rows(i).Item("Name")
        '    v_Item.Value = v_ds.Tables("Task").Rows(i).Item("ID")
        '    If v_ds.Tables("Task").Rows(i).Item("Used") = 1 Then
        '        v_Item.Selected = True
        '    Else
        '        v_Item.Selected = False
        '    End If

        '    gvUser_Task.Items.Add(v_Item)
        'Next

        'gvUser_Task.Columns(0).ItemStyle.Wrap = False
        'gvUser_Task.Columns(1).ItemStyle.Wrap = False
        'gvUser_Task.Columns(2).ItemStyle.Wrap = False
        'gvUser_Task.Columns(3).ItemStyle.Wrap = False

        'gvUser_Task.Columns(0).ItemStyle.Width = 10
        'gvUser_Task.Columns(1).ItemStyle.Width = 10
        'gvUser_Task.Columns(2).ItemStyle.Width = 200
        'gvUser_Task.Columns(3).ItemStyle.Width = 200




        Dim i As Integer
        For i = 0 To v_ds.Tables("Task").Rows.Count - 1
            CType(gvUser_Task.Rows(i).Cells(2).Controls(1), CheckBox).Checked = bBIT(v_ds.Tables("Task").Rows(i)(2).ToString)
            CType(gvUser_Task.Rows(i).Cells(3).Controls(1), CheckBox).Checked = bBIT(v_ds.Tables("Task").Rows(i)(3).ToString)
        Next

        v_dtAdapter.Dispose()
        v_dtCmd.Dispose()
        v_dbConn.Close()
        v_dbConn.Dispose()
    End Sub

    Private Function bBIT(ByVal s1 As String) As Boolean
        If s1.ToUpper = "TRUE" Then Return True
    End Function

    Protected Sub gvUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvUsers.SelectedIndexChanged

        Dim row As GridViewRow = gvUsers.SelectedRow

        txtLoginName.Text = gvUsers.SelectedDataKey.Value
        txtName.Text = row.Cells(1).Text.Trim.Replace("amp;", "").Replace("nbsp;", "").Replace("&", "")
        txtDept.Text = row.Cells(2).Text.Trim.Replace("amp;", "").Replace("nbsp;", "").Replace("&", "")
        txtEmail.Text = row.Cells(6).Text.Trim.Replace("amp;", "").Replace("nbsp;", "").Replace("&", "")
        txtPassword.Text = ""

        If row.Cells(3).Text = "Admin" Then
            rdbUser.Checked = False
            rdbManager.Checked = True
        Else
            rdbManager.Checked = False
            rdbUser.Checked = True
        End If

        Dim v_ChkEnable As CheckBox = CType(row.Cells(4).Controls(0), CheckBox)
        chkEnable.Checked = v_ChkEnable.Checked

        Dim v_ChkAD As CheckBox = CType(row.Cells(5).Controls(0), CheckBox)
        chkAD.Checked = v_ChkAD.Checked


        Call LoadTask(gvUsers.SelectedDataKey.Value)
    End Sub

    Private Function CheckName() As Boolean
        Try
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Users WHERE RTRIM(LoginName) = '" & txtLoginName.Text.Trim & "'"

            Dim v_dtUser As SqlDataReader = Nothing
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dtUser = v_dtCmd.ExecuteReader()

            CheckName = IIf(v_dtUser.HasRows, True, False)

        Catch ex As Exception

        End Try

    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        If btnNew.Text = "新增" Then
            txtDept.Text = ""
            txtEmail.Text = ""
            txtLoginName.Text = ""
            txtPassword.Text = ""
            txtName.Text = ""
            rdbUser.Checked = True
            chkEnable.Checked = True
            chkAD.Checked = False
            gvUser_Task.Enabled = True

            Call LoadTask("")

            btnNew.Text = "存檔"
            btnModify.Enabled = False
            btnDelete.Enabled = False
            Call EnableEdit()
        Else
            Try
                If CheckName() Then
                    lblCheck.Text = "此帳號已存在, 請重新輸入!"
                    Call EnableEdit()
                    Exit Sub
                End If
                lblCheck.Text = ""
                ' Define the ADO.NET objects.
                Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
                Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

                v_dbConn.Open()
                Dim v_strSQL As String = "INSERT INTO Users(Dept,UserName,Email,LoginName,Password,Role,Enable_Flag,AD_Flag,CreateTime) " & _
                    " VALUES ('" & txtDept.Text.Trim & "', '" & _
                    txtName.Text.Trim & "', '" & txtEmail.Text.Trim.Replace("amp;", "").Replace("nbsp;", "").Replace("&", "") & "', '" & txtLoginName.Text.Trim & "', '" & EncryptPassword(txtPassword.Text.Trim) & _
                    "', '" & IIf(rdbManager.Checked, "Admin", "User") & "', " & IIf(chkEnable.Checked, 1, 0) & ", " & IIf(chkAD.Checked, 1, 0) & ", '" & _
                    Now.ToString("yyyy/MM/dd HH:mm:ss") & "')"

                Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
                v_dbComm.ExecuteNonQuery()

                Call SaveTask(txtLoginName.Text.Trim)

                Call LoadUsers()

                v_dbComm.Dispose()
                v_dbConn.Close()
                v_dbConn.Dispose()

                btnNew.Text = "新增"
                btnModify.Enabled = True
                btnDelete.Enabled = True
                Call DisableEdit()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Public Function EncryptPassword(ByVal sourcePassword As String) As String
        Dim sourcePasswordToBytes As Byte() = New System.Text.UnicodeEncoding().GetBytes(sourcePassword)
        Dim hashedBytes As Byte() = New System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(sourcePasswordToBytes)
        Dim vHashed As String = Convert.ToBase64String(hashedBytes)
        Return vHashed
    End Function

    Private Sub DeleteTask(ByVal p_UID As String)
        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            ' 先刪除舊的對應
            Dim v_strSQL As String = "DELETE FROM User_Task WHERE RTRIM(User_ID) = '" & p_UID & "'"
            Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dbComm.ExecuteNonQuery()

            v_dbComm.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveTask(ByVal p_UID As String)
        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            ' 先刪除舊的對應
            Dim v_strSQL As String = "DELETE FROM User_Task WHERE RTRIM(User_ID) = '" & p_UID & "'"
            Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dbComm.ExecuteNonQuery()

            ' 插入新的對應
            Dim i As Integer
            For i = 0 To gvUser_Task.Rows.Count - 1
                If CType(gvUser_Task.Rows(i).Cells(2).Controls(1), CheckBox).Checked Then
                    v_strSQL = String.Format("INSERT INTO User_Task VALUES ('{0}', '{1}', {2})", p_UID, gvUser_Task.Rows(i).Cells(0).Text, IIf(CType(gvUser_Task.Rows(i).Cells(3).Controls(1), CheckBox).Checked, 1, 0))
                    v_dbComm.CommandText = v_strSQL
                    v_dbComm.ExecuteNonQuery()
                End If
            Next

            v_dbComm.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtDept.Text = ""
        txtEmail.Text = ""
        txtLoginName.Text = ""
        txtPassword.Text = ""
        txtName.Text = ""

        lblCheck.Text = ""

        btnNew.Text = "新增"
        btnModify.Text = "修改"
        btnNew.Enabled = True
        btnModify.Enabled = True
        btnDelete.Enabled = True
        Call LoadTask("")
        Call DisableEdit()
    End Sub

    Protected Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If btnModify.Text = "修改" Then
            If txtLoginName.Text.Trim = "" Then Exit Sub
            Call LoadTask(gvUsers.SelectedDataKey.Value)

            btnModify.Text = "存檔"
            btnNew.Enabled = False
            btnDelete.Enabled = False
            Call EnableEdit()

            txtLoginName.Enabled = False
        Else
            Try
                ' Define the ADO.NET objects.
                Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
                Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

                v_dbConn.Open()

                Dim v_strSQL As String = "UPDATE Users SET Dept = '" & txtDept.Text.Trim & "', UserName = '" & _
                    txtName.Text.Trim & "', Email = '" & txtEmail.Text.Trim.Replace("amp;", "").Replace("nbsp;", "").Replace("&", "") & "', Password = '" & EncryptPassword(txtPassword.Text.Trim) & _
                    "', Role = '" & IIf(rdbManager.Checked, "Admin", "User") & "', Enable_Flag = " & IIf(chkEnable.Checked, 1, 0) & ", AD_Flag = " & IIf(chkAD.Checked, 1, 0) & _
                    " WHERE RTRIM(LoginName) = '" & txtLoginName.Text.Trim & "'"

                If txtPassword.Text.Trim = "" Then
                    v_strSQL = "UPDATE Users SET Dept = '" & txtDept.Text.Trim & "', UserName = '" & _
                    txtName.Text.Trim & "', Email = '" & txtEmail.Text.Trim & _
                    "', Role = '" & IIf(rdbManager.Checked, "Admin", "User") & "', Enable_Flag = " & IIf(chkEnable.Checked, 1, 0) & ", AD_Flag = " & IIf(chkAD.Checked, 1, 0) & _
                    " WHERE RTRIM(LoginName) = '" & txtLoginName.Text.Trim & "'"
                End If

                Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
                v_dbComm.ExecuteNonQuery()

                Call SaveTask(txtLoginName.Text.Trim)

                Call LoadUsers()

                v_dbComm.Dispose()
                v_dbConn.Close()
                v_dbConn.Dispose()

                btnModify.Text = "修改"
                btnNew.Enabled = True
                btnDelete.Enabled = True
                Call DisableEdit()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtLoginName.Text.Trim = "" Then Exit Sub

        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "DELETE FROM Users WHERE RTRIM(LoginName) = '" & txtLoginName.Text.Trim & "'"

            Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dbComm.ExecuteNonQuery()

            Call DeleteTask(txtLoginName.Text.Trim)

            Call LoadUsers()
            Call LoadTask("")

            txtDept.Text = ""
            txtEmail.Text = ""
            txtLoginName.Text = ""
            txtPassword.Text = ""
            txtName.Text = ""

            v_dbComm.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckAll.Click
        Dim i As Integer

        For i = 0 To gvUser_Task.Rows.Count - 1
            CType(gvUser_Task.Rows(i).Cells(2).Controls(1), CheckBox).Checked = True
        Next

        Call EnableEdit()
    End Sub

    Protected Sub btnClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        Dim i As Integer

        For i = 0 To gvUser_Task.Rows.Count - 1
            CType(gvUser_Task.Rows(i).Cells(2).Controls(1), CheckBox).Checked = False
            CType(gvUser_Task.Rows(i).Cells(3).Controls(1), CheckBox).Checked = False
        Next
        Call EnableEdit()
    End Sub

    Private Function hasWriteRight(ByVal sTaskID As String) As Boolean
        'Session("LoginName") = "231"
        'Return True
        If IsNothing(Session("LoginName")) Then Return False
        If IsNothing(Session("Dept")) Then Return False

        Dim sSQL As String = String.Format("if exists (select * from Users where LoginName in ('{0}','{1}') and Role='{2}') select 1 else select 0 ", Session("LoginName").ToString, Session("Dept").ToString, sTaskID)
        Try
            Dim oConn As New SqlClient.SqlConnection(ConfigurationManager.AppSettings.GetValues("gStrConn")(0))
            oConn.Open()
            Dim oCmd As New SqlClient.SqlCommand(sSQL, oConn)
            Dim i As Integer = oCmd.ExecuteScalar()
            oConn.Close()
            Return IIf(i = 1, True, False)
        Catch ex As Exception
        End Try
    End Function

End Class
