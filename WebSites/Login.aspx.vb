Imports System.DirectoryServices

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnLogon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogon.Click

        If IsNothing(Session("REMOTE_ADDR")) Then
            lblMessage.Text = "請關閉網頁,重新登入!"
            Exit Sub
        End If

        '由SQL取得user資訊, 辨識密碼是由AD或SQL掌控


        Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
        Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

        v_dbConn.Open()

        Dim v_strSQL As String = "SELECT * FROM Users WHERE LoginName = '" & txtUserName.Text & "' "

        Dim v_dtUser As SqlDataReader = Nothing
        Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
        v_dtUser = v_dtCmd.ExecuteReader()

        Try
            If v_dtUser.HasRows Then
                v_dtUser.Read()
                If v_dtUser("AD_Flag").ToString = "True" Then
                    Dim de As DirectoryEntry = New DirectoryEntry
                    '                    de.Path = "LDAP://fe12az01/CN=fmcs,CN=Users,DC=fe12a,DC=fmcs,DC=com"
                    de.Path = "LDAP://fmcsdc/DC=fmcs,DC=corp"

                    de.Username = txtUserName.Text.Trim
                    de.Password = txtPassword.Text.Trim
                    Dim ds As DirectorySearcher = New DirectorySearcher(de)
                    Try
                        ds.FindOne()
                    Catch ex1 As Exception
                        lblMessage.Text = "AD: " & ex1.Message
                        v_dtCmd.Cancel()
                        v_dtUser.Close()
                        v_dbConn.Close()
                        v_dbConn.Dispose()
                        Exit Sub
                    End Try
                ElseIf Not (v_dtUser("Password") = EncryptPassword(txtPassword.Text.Trim) OrElse v_dtUser("Password") = txtPassword.Text.Trim) Then
                    v_dtCmd.Cancel()
                    v_dtUser.Close()
                    v_dbConn.Close()
                    v_dbConn.Dispose()
                    lblMessage.Text = "SQL: 密碼錯誤!"
                    Exit Sub
                End If

                Session("UserName") = v_dtUser("UserName")
                Session("LoginName") = v_dtUser("LoginName").ToString.Trim
                Session("Dept") = v_dtUser("Dept").ToString.Trim
                Session("IsLogin") = True
                Session("Role") = v_dtUser("Role")

                v_dtCmd.Cancel()
                v_dtUser.Close()

                v_dtCmd.CommandText = String.Format("insert into WebLog(PTime,REMOTE_ADDR,LoginName) values (getdate(),'{0}','{1}')", Session("REMOTE_ADDR").ToString, Session("LoginName").ToString)
                v_dtCmd.ExecuteNonQuery()


                v_dbConn.Close()
                v_dbConn.Dispose()


                Response.Redirect("ReportMain.htm")
            Else

                v_dtCmd.Cancel()
                v_dtUser.Close()
                v_dbConn.Close()
                v_dbConn.Dispose()
                lblMessage.Text = "SQL: 帳號不存在!"
            End If

        Catch ex As Exception

        End Try

        v_dtCmd = Nothing
        v_dtUser = Nothing
        v_dbConn = Nothing
    End Sub

    Public Function EncryptPassword(ByVal sourcePassword As String) As String
        Dim sourcePasswordToBytes As Byte() = New System.Text.UnicodeEncoding().GetBytes(sourcePassword)
        Dim hashedBytes As Byte() = New System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(sourcePasswordToBytes)
        Dim vHashed As String = Convert.ToBase64String(hashedBytes)
        Return vHashed
    End Function


    'Protected Sub btnLogon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogon.Click

    '    Dim v_strConn() As String = ConfigurationManager.AppSettings.GetValues("gStrConn")
    '    Dim v_dbConn As SqlConnection = New SqlConnection(v_strConn(0))

    '    v_dbConn.Open()

    '    Dim v_strSQL As String = "SELECT * FROM Users " & _
    '        "WHERE LoginName = '" & txtUserName.Text & _
    '        "' AND Password = '" & EncryptPassword(txtPassword.Text.Trim) & "'"

    '    Dim v_dtUser As SqlDataReader = Nothing
    '    Dim v_dtCmd As SqlCommand = New SqlCommand(v_strSQL, v_dbConn)
    '    v_dtUser = v_dtCmd.ExecuteReader()

    '    Try
    '        If v_dtUser.HasRows Then
    '            v_dtUser.Read()
    '            Session("UserID") = v_dtUser("ID")
    '            Session("UserName") = v_dtUser("UserName")
    '            Session("LoginName") = v_dtUser("LoginName")
    '            Session("IsLogin") = True
    '            Session("Role") = v_dtUser("Role")

    '            v_dtCmd.Cancel()
    '            v_dtUser.Close()
    '            v_dbConn.Close()
    '            v_dbConn.Dispose()


    '            Response.Redirect("ReportMain.htm")
    '        Else
    '            v_dtCmd.Cancel()
    '            v_dtUser.Close()
    '            v_dbConn.Close()
    '            v_dbConn.Dispose()
    '            lblMessage.Text = "所輸入的帳號或密碼有錯誤! 請重新輸入!"
    '        End If

    '    Catch ex As Exception

    '    End Try

    '    v_dtCmd = Nothing
    '    v_dtUser = Nothing
    '    v_dbConn = Nothing
    'End Sub

End Class
