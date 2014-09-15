
Partial Class HTS2_PassMenu
    Inherits System.Web.UI.UserControl

    Public Sub refresh()
        DataList1.DataSourceID = "ds_sumpos"
        DataList1.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            refresh()
        End If
    End Sub
End Class
