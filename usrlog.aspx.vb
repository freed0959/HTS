Imports System.Data.SqlClient

Partial Class usrlog
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Request.QueryString("out") = "1" Then
            'ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('You are logging out');'</script>")
            Response.Cookies("htspub").Value = ""
            Response.Cookies("htspub").Expires = DateTime.Now.AddDays(-1)
            Response.Redirect("http://adaronet/")
            Exit Sub
        Else
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim reader As SqlDataReader

            Using conn = New SqlConnection(connect)
                Using cmd = New SqlCommand
                    cmd.Connection = conn
                    cmd.Connection.Open()

                    cmd.CommandType = Data.CommandType.StoredProcedure
                    cmd.CommandText = "_sp_usr_logid"

                    cmd.Parameters.Add("@adname", Data.SqlDbType.NVarChar)
                    cmd.Parameters("@adname").Value = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToLower

                    reader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            Dim grp As String = reader(2).ToString()
                            'If grp <> "38" Then
                            Response.Cookies("htspub").Value = 1
                            Response.Cookies("htspub")("idusr") = reader(0).ToString()
                            Response.Cookies("htspub")("nameusr") = reader(1).ToString()
                            Response.Cookies("htspub")("idgroup") = reader(2).ToString()
                            Response.Cookies("htspub")("namegroup") = reader(3).ToString()
                            Response.Cookies("htspub")("comname") = reader(4).ToString()
                            Response.Cookies("htspub").Expires = DateTime.Now.AddHours(8)
                            'Else
                            '    Response.Cookies("htsconusr").Value = 1
                            '    Response.Cookies("htsconusr")("idusr") = reader(0).ToString()
                            '    Response.Cookies("htsconusr")("nameusr") = reader(1).ToString()
                            '    Response.Cookies("htsconusr")("idgroup") = reader(2).ToString()
                            '    Response.Cookies("htsconusr")("namegroup") = reader(3).ToString()
                            '    Response.Cookies("htsconusr")("comname") = reader(4).ToString()
                            '    Response.Cookies("htsconusr").Expires = DateTime.Now.AddHours(8)
                            'End If

                        End While
                        Select Case Request.Cookies("htspub")("idgroup").ToString()
                            Case "14", "16", "17", "45"
                                Response.Redirect("HTS2/Passing?v=1")
                            Case "38"
                                Response.Redirect("HTS2/Passcon?v=1")
                            Case Else
                                Response.Redirect("HTS2/Default")
                        End Select

                    Else
                        ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Sorry You Are Not a Registered User');location.href='http://adaronet/Pages/Default.aspx'</script>")
                    End If
                    reader.Close()
                    cmd.Connection.Close()
                End Using
            End Using
        End If
    End Sub
End Class
