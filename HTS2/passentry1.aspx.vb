Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Data.OleDb
Imports System.Web.Script.Serialization

Partial Class HTS2_passentry1
    Inherits System.Web.UI.Page

    Public _shf As Integer

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Dim jam As Integer = DateTime.Now.Hour
        'Dim shft As Integer = 1
        'Dim tglKmaren As Integer = 0
        'Dim tglToday As Integer = DateTime.Today.Day

        'If (jam >= 0 And jam <= 5) Then
        '    shft = 2
        '    _shf = shft
        'ElseIf jam >= 6 And jam <= 18 Then
        '    shft = 1
        '    _shf = shft
        'ElseIf (jam >= 18) Then
        '    shft = 2
        '    _shf = shft
        'End If

        Dim v As String = Request.QueryString("v")
        Select Case v
            Case "", "0"
                passPanel1.Visible = True

                If Request.QueryString("ins") = "1" Then
                    lbl_info1.Text = "Data inserted"
                End If

                If Request.QueryString("clos") = "1" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.opener.location.reload(true);window.close();", True)
                End If

            Case "1"
                pan_compar1.Visible = True

        End Select
    End Sub

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

            ElseIf _dtm.Hour > 17 Then
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

    Public Function GetSchedulled(ByVal truck As String, ByVal dtm As String) As String
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM [dbo].[v_Schedule] WHERE (TruckNo = '" & truck & "') " & _
                                           "AND (CONVERT(nvarchar, UnitDate, 101) = CONVERT(nvarchar, CONVERT(smalldatetime, '" + dtm + "'), 101)) ", conn)
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

    Public Function CheckDuplicate(ByVal unt As String, ByVal dtm As String) As Integer
        Try
            Dim jam As Integer = DateTime.Now.Hour
            Dim jam1 As Integer

            Select Case jam
                Case 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22
                    jam1 = jam + 1
                Case 23, 5
                    jam1 = jam
                Case 0, 1, 2, 3, 4
                    jam1 = jam + 1
            End Select

            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM tbl_TruckPass WHERE (TruckNo = '" & unt & "') " & _
                                           "AND (Convert(nvarchar, TransactionDate, 101)=CONVERT(nvarchar, CONVERT(smalldatetime,'" & dtm & "'), 101)) " & _
                                           "AND (DatePart(hh, TransactionDate) BETWEEN " & jam.ToString() & " AND " & jam1.ToString() & ") ", conn)
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

    Public Function GetCompany(ByVal unt As String) As String
        Dim kon As String = "ADARO"
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn As New SqlConnection(connect)
            Dim cmd As New SqlCommand()
            cmd.Connection = conn

            cmd.CommandText = "SELECT TOP 1 KontraktorKode FROM dbo.v_unit v WHERE (TruckNo = '" & unt & "') "

            cmd.CommandType = CommandType.Text
            conn.Open()
            kon = cmd.ExecuteScalar()
            conn.Close()
        Catch ex As Exception
            kon = ""

        End Try
        Return kon
    End Function

    Protected Sub btn_entryData_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            If String.IsNullOrEmpty(txtSearch.Text) = True Then
                WeightBridge.csclass.mymsg("All Input Cannot Empty", "#")
                Exit Sub
            End If

            Dim kodeK As String = "ADARO"
            Dim seamCargo As String = ddl_raw1.SelectedValue.ToString()
            Dim rom As String = ddl_rom1.SelectedValue.ToString()
            Dim truckNo As String = txtSearch.Text.ToUpper()
            Dim idTd As String = "NULL"

            Dim dtm As DateTime
            Dim tgl As String = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")
            Dim jam As Integer = DateTime.Now.Hour

            If (jam >= 0 And jam < 6) Then
                dtm = DateAdd(DateInterval.Day, -1, DateTime.Now)
                tgl = dtm.ToString("yyyy-MM-dd h:mm:ss tt")
                ' shift = 2
            ElseIf (jam >= 6) Then
                tgl = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")
                ' shift = 1            
            End If
            Dim jam1 As String = jam + 2

            Dim shift As String = GetShift(tgl)

            Dim cekDuplicate As Integer = CheckDuplicate(truckNo, tgl)
            If cekDuplicate = 0 Then
                WeightBridge.csclass.mymsg(":: " & truckNo & " data on " & tgl & " already exist, entry canceled", "#")
                Exit Sub
            End If

            Dim cektruck As Integer = CheckTruck(truckNo)
            If cektruck = 0 Then
                WeightBridge.csclass.mymsg(":: Truck not found, entry canceled", "#")
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(lbl_idtd.Text) Then
                idTd = lbl_idtd.Text
            End If
            Dim iblok As String = "0"
            If String.IsNullOrEmpty(lbl_idtd.Text) Then
                iblok = "0"
            Else
                If seamCargo <> lbl_raw1.Text Or rom <> lbl_rom1.Text Then
                    iblok = "1"
                End If
            End If

            kodeK = GetCompany(truckNo)
            Dim schd As String = GetSchedulled(truckNo, tgl)

            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn1 As SqlConnection = New SqlConnection(connect)
            Dim cmd1 As SqlCommand = New SqlCommand()
            cmd1.Connection = conn1

            cmd1.CommandText = "INSERT INTO tbl_TruckPass (TruckNo, MaterialKode, KontraktorKode, SourceKode, TransactionDate, Shift, KM_67Pass, " & _
                                "KM_29Pass, schedulled, inQueue, isBlock67, ID_RomDist) " & _
                        "VALUES('" & truckNo & "','" & seamCargo & "','" & kodeK & "','" & rom & "','" & tgl & "'," & shift & ", 1 , 0, " & _
                         schd & ", 0, " & iblok & ", " & idTd & ")"

            cmd1.CommandType = CommandType.Text
            conn1.Open()
            cmd1.ExecuteNonQuery()
            conn1.Close()

            conn1 = Nothing
            connect = Nothing

            If chb_clos1.Checked = True Then
                Response.Redirect("passentry1.aspx?v=0&clos=1")
            Else
                Response.Redirect("passentry1.aspx?v=0&ins=1")
            End If

        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: unknown error, entry canceled", "#")
            Exit Sub

        End Try
        'lbl_info1.Text = "Data Inserted"
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        'Page.DataBind()
    End Sub

    Protected Sub LinkButton2_Load(sender As Object, e As EventArgs) Handles LinkButton2.Load
        LinkButton2.Attributes.Add("onClick", "window.close();return false;")
    End Sub

    Protected Sub btn_src1_Click(sender As Object, e As EventArgs) Handles btn_src1.Click
        Dim truckNo As String = txtSearch.Text
        Dim kon As String = GetCompany(truckNo)
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Dim plancek As String = "SELECT TOP 1 v.TruckNo, v.MaterialKode, v.SourceKode, v.ID_RomDist FROM dbo.tbl_TruckRomDist v " & _
                "WHERE (v.TruckNo = '" & truckNo & "') AND (CONVERT(nvarchar, v.TransactionDate, 101) = CONVERT(nvarchar, GETDATE(), 101))"
        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand(plancek, conn)
                Using dr = cmd.ExecuteReader()
                    If dr.HasRows Then
                        While dr.Read
                            lbl_raw1.Text = dr(1).ToString()
                            lbl_rom1.Text = dr(2).ToString()
                            lbl_idtd.Text = dr(3).ToString()
                        End While

                        ddl_raw1.SelectedValue = lbl_raw1.Text
                        ddl_rom1.SelectedValue = lbl_rom1.Text
                    Else
                        lbl_raw1.Text = ""
                        lbl_rom1.Text = ""
                        lbl_idtd.Text = ""

                        ddl_raw1.SelectedValue = GetFirstMaterial(kon)
                        ddl_rom1.SelectedValue = GetFirstROM()
                    End If
                    dr.Close()
                End Using
            End Using
            conn.Close()
        End Using

        'WeightBridge.csclass.mymsg("tes " & GetFirstMaterial(kon) & "", "#")
        'Dim mat As String = GetFirstMaterial(kon)
        'ddl_raw1.SelectedItem.Text = lbl_raw1.Text
    End Sub

    Protected Sub txtSearch_Load(sender As Object, e As EventArgs) Handles txtSearch.Load
        txtSearch.Attributes.Add("onblur", "CallClick()")
    End Sub

    Protected Function GetNullMaterial() As String
        Dim mat As String

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "_sp_ql_lasmap1"
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        mat = cmd.ExecuteScalar()
        conn.Close()
        conn = Nothing

        Return mat
    End Function

    Protected Function GetFirstMaterial(ByVal kon As String) As String
        Dim mat As String
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn As SqlConnection = New SqlConnection(connect)
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandText = "_sp_ql_lasmapchek1"
            cmd.Parameters.AddWithValue("kon", kon)
            cmd.CommandType = CommandType.StoredProcedure
            conn.Open()
            mat = cmd.ExecuteScalar()
            conn.Close()
            conn = Nothing

        Catch ex As Exception
            mat = GetNullMaterial()

        End Try

        Return mat
    End Function

    Protected Function GetFirstROM() As String
        Dim rom As String

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP 1 [Kode] FROM vwrom_active1 ORDER BY Kode"
        cmd.CommandType = CommandType.Text
        conn.Open()
        rom = cmd.ExecuteScalar()
        conn.Close()

        conn = Nothing
        connect = Nothing

        Return rom

    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'SqlDataSource4.SelectParameters.Clear()
        'ddl_raw1.DataBind()

    End Sub
End Class
