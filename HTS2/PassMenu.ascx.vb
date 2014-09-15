
Partial Class HTS2_PassMenu
    Inherits System.Web.UI.UserControl

    Public Sub refresh()
        DataList1.DataSourceID = "ds_sumpos"
        DataList1.DataBind()
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim ugrup As Integer = 5
        If Request.Cookies("htspub")("idgroup") IsNot Nothing Then
            ugrup = Convert.ToInt16(Request.Cookies("htspub")("idgroup").ToString())
        End If
        If ugrup = 38 Then
            MultiView1.ActiveViewIndex = 1
        Else
            MultiView1.ActiveViewIndex = 0

        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            refresh()
        End If
    End Sub
End Class
