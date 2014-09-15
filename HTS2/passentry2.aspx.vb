Imports System.Data.SqlClient
Imports System.Data
Imports WeightBridge.csclass

Partial Class passentry2
    Inherits System.Web.UI.Page

    Protected Function CheckDate(dtm As String) As Boolean
        Try
            If dtm IsNot Nothing Then
                Dim dt As DateTime = DateTime.Parse(dtm)
                Return True
            Else
                Return False

            End If

        Catch
            Return False

        End Try
    End Function

    Protected Function GetShift(dtm As String) As String
        Try
            Dim cdtm As Boolean = CheckDate(dtm)
            Dim _dtm As DateTime

            _dtm = DateTime.Parse(dtm)
            Dim shf As String

            If _dtm.Hour >= 0 AndAlso _dtm.Hour < 6 Then
                shf = "2"
            ElseIf _dtm.Hour >= 18 AndAlso _dtm.Hour >= 23 Then
                shf = "2"
            Else
                shf = "1"
            End If
            Return shf

        Catch
            Return "1"

        End Try

    End Function

    Public Function CheckTruck(ByVal truck As String) As Integer
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM dbo.v_Unit WHERE (TruckNo = '" & truck & "') ", conn)
                Using dr = cmd.ExecuteReader()
                    If dr.HasRows Then
                        Return 1
                    Else
                        Return 0
                    End If
                    dr.Close()
                End Using
            End Using
            conn.Close()
        End Using
        connect = Nothing
    End Function

    Public Function GetSchedulled(ByVal truck As String) As String
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM [dbo].[v_Schedule] WHERE (TruckNo = '" & truck & "') " & _
                                           "AND (CONVERT(nvarchar, UnitDate, 101) = CONVERT(nvarchar, GETDATE(), 101)) ", conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.HasRows Then
                            Return "1"
                        Else
                            Return "0"
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect = Nothing

        Catch ex As Exception
            Return "0"

        End Try
    End Function

    Public Function CheckDuplicate(ByVal unt As String, ByVal dtm As DateTime) As Integer
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TruckNo FROM dbo.tbl_TruckRomDist WHERE (TruckNo = '" & unt & "') " & _
                                           " AND (CONVERT(nvarchar, TransactionDate, 101) = CONVERT(nvarchar, '" & dtm & "', 101)) " & _
                                           " AND (DatePart(hh, TransactionDate)= DatePart(hh, '" & dtm & "'))", conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.HasRows Then
                            Return 0
                        Else
                            Return 1
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect = Nothing

        Catch ex As Exception
            Return 0

        End Try
    End Function

    Public Function TruckMatch(ByVal unt As String, ByVal kon As String) As Integer
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM v_Unit WHERE (TruckNo = '" & unt & "') AND (KontraktorKode='" & kon & "')", conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.HasRows Then
                            Return 1
                        Else
                            Return 0
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect = Nothing

        Catch ex As Exception
            Return 0

        End Try
    End Function

    Protected Function GetCurrentDate() As DateTime
        Dim dtm As DateTime = DateTime.Now.ToString()

        Try
            Dim jam As Integer = DateTime.Now.Hour

            If (jam >= 0 And jam <= 5) Then
                dtm = DateAdd(DateInterval.Day, -1, DateTime.Now)

            ElseIf (jam >= 6 And jam <= 18) Then
                dtm = DateTime.Now.ToString()

            ElseIf (jam >= 18) Then
                dtm = DateTime.Now.ToString()

            End If

            Return dtm

        Catch
            Return dtm

        End Try
    End Function

    Protected Sub btn_entry1_Click(sender As Object, e As EventArgs) Handles btn_entry1.Click
        'Dim kodeK As String = Request.Cookies("usrcomp").ToString()

        If String.IsNullOrEmpty(txtSearch1.Text) = True Then
            mymsg("All Input Cannot Empty", "passentry2.aspx")
            Exit Sub
        End If

        Dim kodeK As String = Request.Cookies("htspub")("comname").ToString()
        'Dim kodeK As String = "SIS"

        Dim seamCargo As String = ddl_raw2.SelectedValue.ToString()
        Dim rom As String = ddl_rom2.SelectedValue.ToString()
        Dim truckNo As String = txtSearch1.Text
        Dim dtm As DateTime = GetCurrentDate()
        Dim shift As String = GetShift(dtm)

        Dim cekdup As Integer = CheckDuplicate(truckNo, dtm)
        If cekdup = 0 Then
            mymsg(":: Duplicate data detected, entry canceled.", "passentry2")
            Exit Sub
        End If

        Dim cekmatch As Integer = TruckMatch(truckNo, kodeK)
        If cekmatch = 0 Then
            mymsg(":: Truck is unregistered in this company, entry canceled", "passentry2")
            Exit Sub
        End If

        Dim cektruck As Integer = CheckTruck(truckNo)
        If cektruck = 0 Then
            mymsg(":: Unit not found, entry canceled", "passentry2")
            Exit Sub
        End If

        Dim schd As String = GetSchedulled(truckNo)

        SqlConnection.ClearAllPools()
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn1 As SqlConnection = New SqlConnection(connect)
        Dim cmd1 As SqlCommand = New SqlCommand()
        cmd1.Connection = conn1

        cmd1.CommandText = "INSERT INTO tbl_TruckROMDist (TransactionDate, KontraktorKode, TruckNo, MaterialKode, SourceKode, Shift) " & _
                    "VALUES('" & dtm & "','" & kodeK & "','" & truckNo & "','" & seamCargo & "','" & rom & "'," & shift & ")"

        cmd1.CommandType = CommandType.Text
        conn1.Open()
        cmd1.ExecuteNonQuery()
        conn1.Close()

        conn1 = Nothing
        connect = Nothing

        If chb_clos2.Checked = True Then
            Response.Redirect("passentry2.aspx?v=1&clos=1")
        Else
            Response.Redirect("passentry2.aspx?v=1&ins=1")
        End If
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Request.QueryString("ins") = "1" Then
            lbl_info2.Text = "Data inserted"
        End If

        If Request.QueryString("clos") = "1" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.opener.location.reload(true);window.close();", True)
        End If

    End Sub

    'Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    SqlDataSource7.SelectParameters.Clear()
    '    ddl_raw2.DataBind()
    'End Sub
End Class
