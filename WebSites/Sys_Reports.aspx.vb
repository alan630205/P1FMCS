
Partial Class Sys_Reports
    Inherits System.Web.UI.Page

    Private Sub Sys_DisableEdit()
        txtSys_ID.Enabled = False
        txtSys_Name.Enabled = False
        txtSys_No.Enabled = False
    End Sub

    Private Sub Sys_EnableEdit()
        txtSys_ID.Enabled = True
        txtSys_Name.Enabled = True
        txtSys_No.Enabled = True
    End Sub

    Private Sub Task_DisableEdit()
        txtFile_Ext.Enabled = False
        txtFile_Name.Enabled = False
        txtTask_ID.Enabled = False
        txtTask_Name.Enabled = False
        txtTask_No.Enabled = False
    End Sub

    Private Sub Task_EnableEdit()
        txtFile_Ext.Enabled = True
        txtFile_Name.Enabled = True
        txtTask_ID.Enabled = True
        txtTask_Name.Enabled = True
        txtTask_No.Enabled = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then Call LoadSys()
        Call Sys_DisableEdit()
        Call Task_DisableEdit()

        If Not hasWriteRight("Admin") Then
            btnSysNew.Visible = False
            btnSysModify.Visible = False
            btnSysDelete.Visible = False
            btnSysCancel.Visible = False
            btnTaskNew.Visible = False
            btnTaskModify.Visible = False
            btnTaskDelete.Visible = False
            btnTaskCancel.Visible = False
        End If

    End Sub

    Private Sub LoadSys()
        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Subsys WHERE RTRIM(ID) <> 'SYS' ORDER BY Order_No"
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            Dim v_dtAdapter As New SqlDataAdapter(v_dtCmd)

            ' Fill the DataSet.
            Dim v_ds As New DataSet()
            v_dtAdapter.Fill(v_ds, "Sys")

            ' Perform the binding.
            gvSys.DataSource = v_ds
            gvSys.DataBind()

            v_dtAdapter.Dispose()
            v_dtCmd.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadTask(ByVal pSys_ID As String)
        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Tasks WHERE RTRIM(Sys_ID) = '" & pSys_ID & "' ORDER BY Order_No"
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            Dim v_dtAdapter As New SqlDataAdapter(v_dtCmd)

            ' Fill the DataSet.
            Dim v_ds As New DataSet()
            v_dtAdapter.Fill(v_ds, "Task")

            ' Perform the binding.
            gvTask.DataSource = v_ds
            gvTask.DataBind()

            v_dtAdapter.Dispose()
            v_dtCmd.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvSys_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSys.SelectedIndexChanged
        Dim row As GridViewRow = gvSys.SelectedRow

        txtSys_No.Text = row.Cells(0).Text.Trim
        txtSys_ID.Text = gvSys.SelectedDataKey.Value
        txtSys_Name.Text = row.Cells(2).Text.Trim

        txtTask_No.Text = ""
        txtTask_ID.Text = ""
        txtTask_Name.Text = ""
        txtFile_Name.Text = ""
        txtFile_Ext.Text = ""
        chkEnable.Checked = False

        Session("OldSysNo") = row.Cells(0).Text.Trim

        Call LoadTask(gvSys.SelectedDataKey.Value)

    End Sub

    Protected Sub gvTask_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvTask.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvTask.SelectedRow

            txtTask_No.Text = row.Cells(0).Text.Trim
            txtTask_ID.Text = gvTask.SelectedDataKey.Value
            txtTask_Name.Text = row.Cells(2).Text.Trim
            txtFile_Name.Text = row.Cells(3).Text.Trim
            txtFile_Ext.Text = row.Cells(4).Text.Trim

            Dim v_ChkEnable As CheckBox = CType(row.Cells(5).Controls(0), CheckBox)
            chkEnable.Checked = v_ChkEnable.Checked

            Session("OldTaskNo") = row.Cells(0).Text.Trim
        Catch ex As Exception

        End Try

    End Sub

    Private Function CheckSysNo(ByVal p_Modify As Boolean) As Boolean
        If p_Modify And txtSys_No.Text = Session("OldSysNo") Then Return False
        If Not IsNumeric(txtSys_No.Text.Trim) Then Return True

        Try
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Subsys WHERE Order_No = '" & txtSys_No.Text.Trim & "'"

            Dim v_dtSys As SqlDataReader = Nothing
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dtSys = v_dtCmd.ExecuteReader()

            CheckSysNo = IIf(v_dtSys.HasRows, True, False)

        Catch ex As Exception

        End Try
    End Function

    Private Function CheckSysID() As Boolean
        Try
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Subsys WHERE RTRIM(ID) = '" & txtSys_ID.Text.Trim & "'"

            Dim v_dtSys As SqlDataReader = Nothing
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dtSys = v_dtCmd.ExecuteReader()

            CheckSysID = IIf(v_dtSys.HasRows, True, False)

        Catch ex As Exception

        End Try
    End Function

    Protected Sub btnSysNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSysNew.Click
        If btnSysNew.Text = "新增" Then
            txtSys_No.Text = ""
            txtSys_ID.Text = ""
            txtSys_Name.Text = ""

            Call LoadTask("")

            btnSysNew.Text = "存檔"
            btnSysModify.Enabled = False
            btnSysDelete.Enabled = False

            btnTaskNew.Enabled = False
            btnTaskModify.Enabled = False
            btnTaskDelete.Enabled = False
            btnTaskCancel.Enabled = False
            Call Sys_EnableEdit()
            gvSys.Enabled = False
        Else
            Try
                If CheckSysNo(False) Then
                    lblSysCheck.Text = "此排列序號已存在或不是數字, 請重新輸入!"
                    Call Sys_EnableEdit()
                    Exit Sub
                End If

                If CheckSysID() Then
                    lblSysCheck.Text = "此系統代號已存在, 請重新輸入!"
                    Call Sys_EnableEdit()
                    Exit Sub
                End If

                lblSysCheck.Text = ""
                ' Define the ADO.NET objects.
                Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
                Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

                v_dbConn.Open()

                Dim v_strSQL As String = "INSERT INTO Subsys VALUES ('" & txtSys_No.Text.Trim & "', '" & txtSys_ID.Text.Trim & "', '" & _
                    txtSys_Name.Text.Trim & "')"

                Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
                v_dbComm.ExecuteNonQuery()

                v_dbComm.Dispose()
                v_dbConn.Close()
                v_dbConn.Dispose()

                Call LoadSys()

                btnSysNew.Text = "新增"
                btnSysModify.Enabled = True
                btnSysDelete.Enabled = True

                btnTaskNew.Enabled = True
                btnTaskModify.Enabled = True
                btnTaskDelete.Enabled = True
                btnTaskCancel.Enabled = True
                Call Sys_DisableEdit()
                gvSys.Enabled = True

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnSysModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSysModify.Click
        If txtSys_ID.Text.Trim = "" Then Exit Sub

        If btnSysModify.Text = "修改" Then
            btnSysModify.Text = "存檔"
            btnSysNew.Enabled = False
            btnSysDelete.Enabled = False

            btnTaskNew.Enabled = False
            btnTaskModify.Enabled = False
            btnTaskDelete.Enabled = False
            btnTaskCancel.Enabled = False
            Call Sys_EnableEdit()
            txtSys_ID.Enabled = False
            gvSys.Enabled = False
            gvTask.Enabled = False
        Else
            Try
                If CheckSysNo(True) Then
                    lblSysCheck.Text = "此排列序號已存在或不是數字, 請重新輸入!"
                    Call Sys_EnableEdit()
                    txtSys_ID.Enabled = False
                    Exit Sub
                End If

                lblSysCheck.Text = ""
                ' Define the ADO.NET objects.
                Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
                Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

                v_dbConn.Open()

                Dim v_strSQL As String = "UPDATE Subsys SET Order_No = '" & txtSys_No.Text.Trim & "', Name = '" & txtSys_Name.Text.Trim & "' " & _
                    "WHERE RTRIM(ID) = '" & txtSys_ID.Text.Trim & "'"

                Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
                v_dbComm.ExecuteNonQuery()

                Call LoadSys()

                v_dbComm.Dispose()
                v_dbConn.Close()
                v_dbConn.Dispose()

                btnSysModify.Text = "修改"
                btnSysNew.Enabled = True
                btnSysDelete.Enabled = True

                btnTaskNew.Enabled = True
                btnTaskModify.Enabled = True
                btnTaskDelete.Enabled = True
                btnTaskCancel.Enabled = True
                Call Sys_DisableEdit()
                gvSys.Enabled = True
                gvTask.Enabled = True

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnSysDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSysDelete.Click
        If txtSys_ID.Text.Trim = "" Then Exit Sub

        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "DELETE FROM Subsys WHERE RTRIM(ID) = '" & txtSys_ID.Text.Trim & "'"

            Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dbComm.ExecuteNonQuery()

            v_strSQL = "DELETE FROM User_Task WHERE RTRIM(Task_ID) IN (SELECT RTRIM(ID) FROM Tasks WHERE RTRIM(Sys_ID) = '" & _
                txtSys_ID.Text.Trim & "')"
            v_dbComm.CommandText = v_strSQL
            v_dbComm.ExecuteNonQuery()

            v_strSQL = "DELETE FROM Tasks WHERE RTRIM(Sys_ID) = '" & txtSys_ID.Text.Trim & "'"
            v_dbComm.CommandText = v_strSQL
            v_dbComm.ExecuteNonQuery()

            Call LoadSys()

            txtSys_No.Text = ""
            txtSys_ID.Text = ""
            txtSys_Name.Text = ""

            txtTask_No.Text = ""
            txtTask_ID.Text = ""
            txtTask_Name.Text = ""
            txtFile_Ext.Text = ""
            txtFile_Name.Text = ""
            chkEnable.Checked = False

            Call LoadTask("")

            v_dbComm.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSysCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSysCancel.Click
        txtSys_No.Text = ""
        txtSys_ID.Text = ""
        txtSys_Name.Text = ""

        Call LoadTask("")

        txtTask_ID.Text = ""
        txtTask_Name.Text = ""
        txtTask_No.Text = ""
        txtFile_Ext.Text = ""
        txtFile_Name.Text = ""

        lblSysCheck.Text = ""

        btnSysNew.Text = "新增"
        btnSysModify.Text = "修改"
        btnSysNew.Enabled = True
        btnSysModify.Enabled = True
        btnSysDelete.Enabled = True

        btnTaskNew.Enabled = True
        btnTaskModify.Enabled = True
        btnTaskDelete.Enabled = True
        btnTaskCancel.Enabled = True

        Call Sys_DisableEdit()
        gvSys.Enabled = True
        gvTask.Enabled = True
    End Sub

    Private Function CheckTaskNo(ByVal p_Modify As Boolean) As Boolean
        If p_Modify And txtTask_No.Text = Session("OldTaskNo") Then Return False
        If Not IsNumeric(txtTask_No.Text.Trim) Then Return True

        Try
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Tasks WHERE Order_No = '" & txtTask_No.Text & "' AND RTRIM(Sys_ID) = '" & txtSys_ID.Text.Trim & "'"

            Dim v_dtTask As SqlDataReader = Nothing
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dtTask = v_dtCmd.ExecuteReader()

            CheckTaskNo = IIf(v_dtTask.HasRows, True, False)

        Catch ex As Exception

        End Try
    End Function

    Private Function CheckTaskID() As Boolean
        Try
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "SELECT * FROM Tasks WHERE RTRIM(ID) = '" & txtTask_ID.Text.Trim & "'"

            Dim v_dtTask As SqlDataReader = Nothing
            Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dtTask = v_dtCmd.ExecuteReader()

            CheckTaskID = IIf(v_dtTask.HasRows, True, False)

        Catch ex As Exception

        End Try
    End Function

    Protected Sub btnTaskNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTaskNew.Click
        If txtSys_ID.Text.Trim = "" Then Exit Sub

        If btnTaskNew.Text = "新增" Then
            txtTask_No.Text = ""
            txtTask_ID.Text = ""
            txtTask_Name.Text = ""
            txtFile_Ext.Text = ""
            txtFile_Name.Text = ""
            chkEnable.Checked = True

            btnTaskNew.Text = "存檔"
            btnTaskModify.Enabled = False
            btnTaskDelete.Enabled = False

            btnSysNew.Enabled = False
            btnSysModify.Enabled = False
            btnSysDelete.Enabled = False
            btnSysCancel.Enabled = False
            Call Task_EnableEdit()
            gvSys.Enabled = False
            gvTask.Enabled = False
        Else
            Try
                If CheckTaskNo(False) Then
                    lblTaskCheck.Text = "此排列序號已存在或不是數字, 請重新輸入!"
                    Call Task_EnableEdit()
                    Exit Sub
                End If

                If CheckTaskID() Then
                    lblTaskCheck.Text = "此系統代號已存在, 請重新輸入!"
                    Call Task_EnableEdit()
                    Exit Sub
                End If

                lblTaskCheck.Text = ""
                ' Define the ADO.NET objects.
                Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
                Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

                v_dbConn.Open()

                Dim v_strSQL As String = "INSERT INTO Tasks(Order_No,ID,Name,[File_Name],File_Ext,Sys_ID,Enable_Flag) VALUES ('" & txtTask_No.Text.Trim & "', '" & txtTask_ID.Text.Trim & "', '" & _
                    txtTask_Name.Text.Trim & "', '" & txtFile_Name.Text.Trim & "', '" & txtFile_Ext.Text.Trim & "', '" & _
                    txtSys_ID.Text.Trim & "', " & IIf(chkEnable.Checked, 1, 0) & ")"

                Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
                v_dbComm.ExecuteNonQuery()

                v_dbComm.Dispose()
                v_dbConn.Close()
                v_dbConn.Dispose()

                Call LoadTask(txtSys_ID.Text.Trim)

                btnTaskNew.Text = "新增"
                btnTaskModify.Enabled = True
                btnTaskDelete.Enabled = True

                btnSysNew.Enabled = True
                btnSysModify.Enabled = True
                btnSysDelete.Enabled = True
                btnSysCancel.Enabled = True
                Call Task_DisableEdit()
                gvSys.Enabled = True
                gvTask.Enabled = True

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnTaskCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTaskCancel.Click
        txtTask_No.Text = ""
        txtTask_ID.Text = ""
        txtTask_Name.Text = ""
        txtFile_Ext.Text = ""
        txtFile_Name.Text = ""
        chkEnable.Checked = False

        btnTaskNew.Text = "新增"
        btnTaskModify.Text = "修改"
        btnTaskNew.Enabled = True
        btnTaskModify.Enabled = True
        btnTaskDelete.Enabled = True

        btnSysNew.Enabled = True
        btnSysModify.Enabled = True
        btnSysDelete.Enabled = True
        btnSysCancel.Enabled = True
        Call Task_DisableEdit()
        gvSys.Enabled = True
        gvTask.Enabled = True
    End Sub

    Protected Sub btnTaskModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTaskModify.Click
        If txtSys_ID.Text.Trim = "" Or txtTask_ID.Text.Trim = "" Then Exit Sub

        If btnTaskModify.Text = "修改" Then
            btnTaskModify.Text = "存檔"
            btnTaskNew.Enabled = False
            btnTaskDelete.Enabled = False

            btnSysNew.Enabled = False
            btnSysModify.Enabled = False
            btnSysDelete.Enabled = False
            btnSysCancel.Enabled = False
            Call Task_EnableEdit()
            txtTask_ID.Enabled = False
            gvSys.Enabled = False
            gvTask.Enabled = False
        Else
            Try
                If CheckTaskNo(True) Then
                    lblTaskCheck.Text = "此排列序號已存在或不是數字, 請重新輸入!"
                    Call Task_EnableEdit()
                    txtTask_ID.Enabled = False
                    Exit Sub
                End If

                lblTaskCheck.Text = ""
                ' Define the ADO.NET objects.
                Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
                Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

                v_dbConn.Open()

                Dim v_strSQL As String = "UPDATE Tasks SET Order_No = '" & txtTask_No.Text.Trim & "', Name = '" & _
                    txtTask_Name.Text.Trim & "', File_Name = '" & txtFile_Name.Text.Trim & "', File_Ext = '" & _
                    txtFile_Ext.Text.Trim & "', Enable_Flag = " & IIf(chkEnable.Checked, 1, 0) & _
                    " WHERE RTRIM(ID) = '" & txtTask_ID.Text.Trim & "'"

                Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
                v_dbComm.ExecuteNonQuery()

                v_dbComm.Dispose()
                v_dbConn.Close()
                v_dbConn.Dispose()

                Call LoadTask(txtSys_ID.Text.Trim)

                btnTaskModify.Text = "修改"
                btnTaskNew.Enabled = True
                btnTaskDelete.Enabled = True

                btnSysNew.Enabled = True
                btnSysModify.Enabled = True
                btnSysDelete.Enabled = True
                btnSysCancel.Enabled = True
                Call Task_DisableEdit()
                gvSys.Enabled = True
                gvTask.Enabled = True

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnTaskDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTaskDelete.Click
        If txtSys_ID.Text.Trim = "" Or txtTask_ID.Text.Trim = "" Then Exit Sub

        lblTaskCheck.Text = ""
        Try
            ' Define the ADO.NET objects.
            Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
            Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

            v_dbConn.Open()

            Dim v_strSQL As String = "DELETE FROM User_Task WHERE RTRIM(Task_ID) = '" & txtTask_ID.Text.Trim & "'"

            Dim v_dbComm As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
            v_dbComm.ExecuteNonQuery()

            v_strSQL = "DELETE FROM Tasks WHERE RTRIM(ID) = '" & txtTask_ID.Text.Trim & "'"
            v_dbComm.CommandText = v_strSQL
            v_dbComm.ExecuteNonQuery()

            v_dbComm.Dispose()
            v_dbConn.Close()
            v_dbConn.Dispose()

            Call LoadTask(txtSys_ID.Text.Trim)

            txtTask_No.Text = ""
            txtTask_ID.Text = ""
            txtTask_Name.Text = ""
            txtFile_Ext.Text = ""
            txtFile_Name.Text = ""
            chkEnable.Checked = False
        Catch ex As Exception

        End Try
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
