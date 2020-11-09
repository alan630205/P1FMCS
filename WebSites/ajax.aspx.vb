
Partial Class ajax
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Label1.Text = "Panel refreshed at " & _
        DateTime.Now.ToString()

        'UpdatePanel2.Update()
    End Sub
End Class
