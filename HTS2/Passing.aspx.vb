Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Data.OleDb
Imports System.Web.Script.Serialization
Imports WeightBridge.csclass
'For converting HTML TO PDF- START
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.xml
Imports iTextSharp.text.html.simpleparser
Imports System.Text.RegularExpressions
'For converting HTML TO PDF- END


Partial Class HTS2_Passing
    Inherits System.Web.UI.Page

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '           server control at run time. 
    End Sub

    Public _matTotal As Integer

    Public fileLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileLoc").ToString()

    Public maintctrl As Control

    Public Sub xlsport(ByVal ds As String, ByVal grd As GridView, ByVal _fnm As String)
        grd.Visible = True
        gridStrip(grd)

        grd.DataSourceID = ds
        grd.DataBind()

        Randomize()
        Dim value As Integer = CInt(Int((200 * Rnd()) + 1))
        Dim form As New HtmlForm()
        Dim attachment As String = "attachment; filename=" & _fnm & value & "_" & Format(Now(), "MMddyyHHmmss") & ".xls"

        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        EnableViewState = False
        Response.Charset = String.Empty

        Dim stw As New System.IO.StringWriter()
        Dim htextw As New HtmlTextWriter(stw)

        form.Controls.Add(grd)
        Me.Controls.Add(form)

        form.RenderControl(htextw)
        Response.Write(stw.ToString())
        Response.End()
    End Sub

    Public Sub pdfPort(ByVal grd As GridView, ByVal lnscp As Integer, _fnm As String)
        Randomize()
        Dim value As Integer = CInt(Int((200 * Rnd()) + 1))

        grd.Visible = True
        If TypeOf grd Is System.Web.UI.WebControls.GridView Then
            gridstrip(grd)
        End If

        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", _
         "attachment;filename=" & _fnm & value & "_" & Format(Now(), "MMddyyHHmmss") & ".pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim sw As New StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        grd.AllowPaging = False
        grd.DataBind()
        grd.RenderControl(hw)
        Dim sr As New StringReader(sw.ToString())
        Dim pdfDoc As New Document(PageSize.A3, 10.0F, 10.0F, 50.0F, 30.0F)
        If lnscp = 1 Then
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A3.Rotate())
        End If
        Dim htmlparser As New HTMLWorker(pdfDoc)
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream)

        pdfDoc.Open()
        htmlparser.Parse(sr)
        pdfDoc.Close()

        Response.Write(pdfDoc)
        Response.End()
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
       
        Dim ugrup As Integer = 5
        If Request.Cookies("htspub") IsNot Nothing Then

            maintctrl = CType(Page.LoadControl("RomCondition1.ascx"), Control)
            passmenu1.Controls.Add(maintctrl)

            Dim jam As Integer = DateTime.Now.Hour
            Dim shft As Integer = 1
            Dim dtm As DateTime = DateTime.Today()

            If (jam = 0) Or (jam >= 0 And jam <= 5) Then
                shft = 2
                dtm = DateAdd(DateInterval.Day, -1, DateTime.Today)

            ElseIf jam >= 6 And jam < 18 Then
                shft = 1

            ElseIf (jam >= 18) Then
                shft = 2

            End If

            txt_date.Text = dtm.ToString("MM/dd/yyyy")
            MultiView1.ActiveViewIndex = 0

            ugrup = Convert.ToInt16(Request.Cookies("htspub")("idgroup").ToString())

            If Page.IsPostBack = False Then
                Select Case Request.QueryString("v")
                    Case "0", ""
                        'Page.Title = "Passing Home Page"
                        'SqlDS_truckpass.SelectParameters.Clear()
                        'SqlDS_truckpass.SelectParameters.Add("dtm", dbType:=DbType.String, value:=dtm.ToString("MM/dd/yyyy"))
                        'grid_pass1.DataBind()
                        Response.Redirect("Passing?v=1")

                    Case "1"
                        Page.Title = String.Concat("Passing Km. 67, ", DateTime.Today.ToString("dd MMM yyyy"), " - Shift: ", shft.ToString())
                        Select Case ugrup
                            Case 2, 14, 45
                                grid_pass1.Columns(10).Visible = True
                                templt1.Visible = True
                                rcpUpd1.Visible = True
                                ImageButton2.Visible = True
                                chb_edit1.Visible = True
                            Case Else
                                pan_load67.Visible = False
                        End Select

                        SqlDS_truckpass.SelectParameters.Clear()
                        SqlDS_truckpass.SelectParameters.Add("dtm", dbType:=DbType.String, value:=dtm.ToString("MM/dd/yyyy"))
                        grid_pass1.DataBind()

                    Case "2"
                        Page.Title = String.Concat("Passing Km. 29, ", DateTime.Today.ToString("dd MMM yyyy"), " - Shift: ", shft.ToString())
                        txt_truck29.Attributes("onKeyDown") = "javascript: if (TestForReturn()) document.all.MainContent_btn_src29.click();"
                        Select Case ugrup
                            Case 2, 16, 45
                                pan_entry29.Visible = True
                                chb_edit1.Visible = True
                            Case Else
                                'hide entry panel
                        End Select
                        rcpUpd1.Visible = False
                        pan_load67.Visible = False

                        SqlDS_truckpass.SelectParameters.Clear()
                        SqlDS_truckpass.SelectParameters.Add("dtm", dbType:=DbType.String, value:=dtm.ToString("MM/dd/yyyy"))
                        SqlDS_truckpass.SelectParameters.Add("ord", dbType:=DbType.String, value:="TD_29Pass DESC, idx DESC, TransactionDate DESC")
                        grid_pass1.DataBind()

                    Case "3"
                        Page.Title = String.Concat("Kelanis, ", DateTime.Today.ToString("dd MMM yyyy"), " - Shift: ", shft.ToString())
                        txt_truckkls.Attributes("onKeyDown") = "javascript: if (TestForReturn()) document.all.MainContent_btn_srckls.click();"
                        Select Case ugrup
                            Case 2, 17, 45
                                pan_entry02.Visible = True
                                chb_edit1.Visible = True
                            Case Else
                                'hide entry table
                        End Select
                        rcpUpd1.Visible = False
                        pan_load67.Visible = False

                        SqlDS_truckpass.SelectParameters.Clear()
                        SqlDS_truckpass.SelectParameters.Add("dtm", dbType:=DbType.String, value:=dtm.ToString("MM/dd/yyyy"))
                        SqlDS_truckpass.SelectParameters.Add("ord", dbType:=DbType.String, value:="TD_inQueue DESC, idx DESC, TransactionDate DESC")
                        grid_pass1.DataBind()

                    Case "4"
                        Page.Title = String.Concat("Closed Transaction, ", DateTime.Today.ToString("dd MMM yyyy"), " - Shift: ", shft.ToString())
                        rcpUpd1.Visible = False
                        pan_load67.Visible = False

                        SqlDS_truckpass.SelectParameters.Clear()
                        SqlDS_truckpass.SelectParameters.Add("dtm", dbType:=DbType.String, value:=dtm.ToString("MM/dd/yyyy"))
                        SqlDS_truckpass.SelectParameters.Add("clos", dbType:=DbType.Int16, value:=1)
                        SqlDS_truckpass.SelectParameters.Add("ord", dbType:=DbType.String, value:="TD_inQueue DESC, idx DESC, TransactionDate DESC")
                        grid_pass1.DataBind()
                End Select

            End If
        Else
            Response.Redirect("../usrlog.aspx")
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.Cookies("htspub") Is Nothing Then
            'ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('You are violating an access');location.href='http://google.com'</script>")
            mymsg("You Are Violating an Access!", "http://google.com")
        Else
            Select Case Request.QueryString("v")
                Case "2"
                    txt_truck29.Focus()
                Case "3"
                    txt_truckkls.Focus()
            End Select
        End If
    End Sub

    Public Sub changePz(ByVal grd As GridView, ByVal amt As Integer, ByVal ds As SqlDataSource)
        ' ds.SelectCommand = sq1
        grd.PageSize = amt
        grd.DataBind()
    End Sub

    Protected Sub open_chart(ByVal ibt As ImageButton, ByVal dtm As String, ByVal ID As Int16)
        ibt.Attributes.Add("onClick", "window.open('passchart1.aspx?v=0&dtm=" & dtm & "', '" & ID & "','status=0,scrollbars=1,resizable=1,width=1204,height=750,left=50,top=10'); return false; window.focus();")
    End Sub

    'Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
    '    If e.CommandName = "Select" Then
    '        Dim dataKeyValue As Object = GridView1.DataKeys(Integer.Parse(e.CommandArgument.ToString())).Value
    '        idpaskeep.Text = dataKeyValue.ToString()
    '    End If
    'End Sub


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

    Public Function GetSchedulled(ByVal truck As String, ByVal dtm As String) As String
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM [dbo].[v_Schedule] WHERE (TruckNo = '" & truck & "') " & _
                                           "AND (CONVERT(nvarchar, UnitDate, 101) = CONVERT(nvarchar, '" & dtm & "', 101)) ", conn)
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

    Public Function GetCompany(ByVal trc As String) As String
        Try
            Dim kon As String = " "
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 KontraktorKode FROM dbo.v_unit v WHERE (TruckNo = '" & trc & "') ", conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.HasRows Then
                            While dr.Read
                                kon = dr(0).ToString()
                            End While
                            Return kon
                        Else
                            Return kon
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect = Nothing

        Catch ex As Exception
            Return " "

        End Try
    End Function

    Public Function CheckMaterial(ByVal mat As String) As Integer
        Try

            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 Kode FROM dbo.v_Material v WHERE (Kode = '" & mat & "') ", conn)
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

    Public Function CheckROM(ByVal rom As String) As Integer
        Try

            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 Kode FROM dbo.v_Sources v WHERE (Kode = '" & rom & "') ", conn)
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

    Public Function CheckDuplicate(ByVal unt As String) As Integer
        Try
            Dim jam As Integer = DateTime.Now.Hour
            Dim jam1 As Integer
            If jam < 22 Then
                jam1 = jam + 2
            Else
                jam1 = 23
            End If


            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn = New SqlConnection(connect)
                conn.Open()
                Using cmd = New SqlCommand("SELECT TOP 1 TruckNo FROM tbl_TruckPass WHERE (TruckNo = '" & unt & "') " & _
                                           "AND (Convert(nvarchar, TransactionDate, 101)=Convert(nvarchar, GETDATE(), 101)) " & _
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

    Public Sub delFile(picPath As String)
        Try
            Dim TheFile As FileInfo = New FileInfo(picPath)
            If TheFile.Exists Then
                File.Delete(picPath)
            Else
                Throw New FileNotFoundException()
            End If

        Catch ex As FileNotFoundException
            ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('file not found');</script>")

        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('file deletion error');</script>")

        End Try
    End Sub

    Public Function cekData(ByVal myValue1 As Object) As String
        If myValue1 Is Nothing Or IsDBNull(myValue1) Then
            Return ""
        Else
            If String.IsNullOrWhiteSpace(myValue1) = True Then
                Return ""
            Else
                Return String.Concat(myValue1.ToString(), " <br />")
            End If
        End If

    End Function

    Public Function cekTruck1(ByVal truck As String) As Integer
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

    Public Function cekTruck2(ByVal truck As String, ByVal km As String) As Integer
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim strcek As String = "SELECT TOP 1 TruckNo FROM dbo.tbl_TruckPass WHERE (TruckNo = '" & truck & "') " & _
                                "AND (CONVERT(nvarchar, [TD_29Pass], 101) = CONVERT(nvarchar, GETDATE(), 101)) " & _
                                "AND ([DateClosed] IS NULL)"
        If km = "29" Then
            strcek = strcek & "AND (([inQueue] = 1)"
            strcek = strcek & "OR (DATEPART(Hour, [TD_29Pass]) BETWEEN DATEPART(Hour, GETDATE()) AND DATEPART(Hour, DATEADD(HOUR, 2, GETDATE()))))"
        Else
            strcek = strcek & "AND (DATEPART(Hour, [TD_inQueue]) BETWEEN DATEPART(Hour, GETDATE()) AND DATEPART(Hour, DATEADD(HOUR, 2, GETDATE())))"
        End If

        Using conn = New SqlConnection(connect)
            conn.Open()

            Using cmd = New SqlCommand(strcek, conn)
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
    End Function

    Public Function checkJam() As DateTime
        Try
            Dim jam As Integer = DateTime.Now.Hour
            Dim shft As Integer = 1
            Dim tgl As DateTime = DateTime.Today()

            If (jam >= 0 And jam <= 5) Then
                tgl = DateAdd(DateInterval.Day, -1, DateTime.Today())
            Else
                tgl = tgl
            End If

            Return tgl

        Catch ex As Exception
            Return DateTime.Today()

        End Try

    End Function

    Public Sub datacek(ByVal errno As Integer, ByVal km As String, ByVal idp As String, ByVal lbl As Label, ByVal trc As String)
        Select Case errno
            Case 0
                updTruck(idp, km, 0)
                lbl.Text = "Data succesfully updated <br />"
                lbl.Attributes.Add("class", "")

            Case 1
                lbl.Text = "Data already exist or not closed yet<br />"
                lbl.Attributes.Add("class", "blink")

            Case 2
                updTruck(idp, km, 1)
                If km = "29" Then
                    lbl.Text = "Truck blocked by Km. 67, Hold Truck Please!<br />"
                    lbl.Attributes.Add("class", "blink")
                Else
                    lbl.Text = "Truck blocked by Km. 29, Hold Truck Please!<br />"
                    lbl.Attributes.Add("class", "blink")

                End If

            Case 3
                updTruck(idp, km, 1)
                lbl.Text = "Raw Cargo deviation, Hold Truck Please!<br />"
                lbl.Attributes.Add("class", "blink")

            Case 4
                lbl.Text = "Truck Not Found, Entry Canceled <br />"
                lbl.Attributes.Add("class", "")

            Case 5
                insTruck(trc, km)
                lbl.Text = "Truck missed by Km. 67, Dispatch Km. 67 has notified <br />"
                lbl.Attributes.Add("class", "blink")


        End Select
    End Sub

    Protected Sub btn_process_Click(sender As Object, e As EventArgs) Handles btn_process.Click
        Dim dtm As String = txt_date.Text
        Dim trc As String = truckNo_Txt.Text
        Dim mat As String = ddl_cargo.SelectedValue.ToString()
        Dim prod As String = ddl_prod.SelectedValue.ToString()
        Dim kon As String = ddl_cntr.SelectedValue.ToString()

        SqlDS_truckpass.SelectParameters.Clear()
        SqlDS_truckpass.SelectParameters.Add("dtm", dbType:=DbType.String, value:=dtm)
        If Not String.IsNullOrEmpty(trc) Then
            SqlDS_truckpass.SelectParameters.Add("trc", dbType:=DbType.String, value:=trc)
        End If
        If Not String.IsNullOrEmpty(mat) Then
            SqlDS_truckpass.SelectParameters.Add("mat", dbType:=DbType.String, value:=mat)
        End If
        If Not String.IsNullOrEmpty(prod) Then
            SqlDS_truckpass.SelectParameters.Add("prod", dbType:=DbType.String, value:=prod)
        End If
        If kon.Trim.ToLower() <> "all" Then
            SqlDS_truckpass.SelectParameters.Add("kon", dbType:=DbType.String, value:=kon)
        End If
        If Request.QueryString("v").ToString() = "4" Then
            SqlDS_truckpass.SelectParameters.Add("clos", dbType:=DbType.String, value:=1)
        End If
        Select Case rbl_block1.SelectedValue.ToString()
            Case "67"
                SqlDS_truckpass.SelectParameters.Add("b67", dbType:=DbType.String, value:=1)
            Case "29"
                SqlDS_truckpass.SelectParameters.Add("b29", dbType:=DbType.String, value:=1)
            Case "02"
                SqlDS_truckpass.SelectParameters.Add("b02", dbType:=DbType.String, value:=1)
        End Select
        Select Case Request.QueryString("v")
            Case "2"
                SqlDS_truckpass.SelectParameters.Add("ord", dbType:=DbType.String, value:="TD_29Pass DESC, idx DESC, TransactionDate DESC")
            Case "3"
                SqlDS_truckpass.SelectParameters.Add("ord", dbType:=DbType.String, value:="TD_inQueue DESC, idx DESC, TransactionDate DESC")
            Case "4"
                SqlDS_truckpass.SelectParameters.Add("ord", dbType:=DbType.String, value:="DateClosed DESC, idx DESC, TransactionDate DESC")
        End Select
        grid_pass1.DataBind()

    End Sub

    Protected Sub SqlDS_truckpass_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDS_truckpass.Selected
        If e.Exception IsNot Nothing Then
            'tes
        Else
            totlbl1.Text = String.Format("Total record: {0} found", e.AffectedRows)
        End If

    End Sub

    Protected Sub lbt_upl67_Click(sender As Object, e As EventArgs) Handles lbt_upl67.Click
        lbt_upl67.Enabled = False
        Dim PictureUpload As FileUpload = FileUpload1

        Try
            If PictureUpload.HasFile Then
                If String.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", True) <> 0 Then
                    lbt_upl67.Enabled = True
                    WeightBridge.csclass.mymsg(":: Only MS-Excel 2007 or above (.xlsx) files are accepted", "#")
                    Exit Sub
                End If

                Dim fileNameWoExt As String = Left(System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName), 2)
                Dim ext As String = System.IO.Path.GetExtension(PictureUpload.FileName).ToString()
                Dim picPath As String = String.Concat(fileLoc, fileNameWoExt, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName))

                PictureUpload.SaveAs(picPath)

                'ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('--> " & picPath & "');</script>")

                Dim oconn As New OleDbConnection
                If File.Exists(picPath) Then
                    If ext.ToString() = ".xlsx" Then
                        oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & picPath.ToString() & ";Extended Properties=" & Chr(34) & "Excel 12.0;HDR=YES" & Chr(34) & ";"
                    Else
                        lbt_upl67.Enabled = True
                        WeightBridge.csclass.mymsg("Invalid File", "#")
                        Exit Sub
                    End If
                End If

                Try

                    oconn.Open()
                    Dim dtExcelSchema As DataTable
                    dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
                    oconn.Close()

                    oconn.Open()
                    Dim ocmd As New OleDbCommand("select * from [" + SheetName + "]", oconn)
                    'OleDbDataReader ode = ocmd.ExecuteReader();

                    Dim oleda As New OleDbDataAdapter()
                    oleda.SelectCommand = ocmd

                    'Create a DataSet which will hold the data extracted from the worksheet.
                    Dim ds As New DataSet()

                    'Fill the DataSet from the data extracted from the worksheet.
                    oleda.Fill(ds)
                    oconn.Close()

                    '---- DELETE FILE EXCEL DARI SERVER ------
                    delFile(picPath)

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim unt As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim cekvalue As Integer = cekTruck1(unt)
                            If cekvalue = 0 Then
                                lbt_upl67.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & unt & " is unknown (line " & i + 1 & "), upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        lbt_upl67.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking TruckNo", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim mat As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim cekvalue As Integer = CheckMaterial(mat)
                            If cekvalue = 0 Then
                                lbt_upl67.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & mat & " is unknown (line " & i + 1 & "), upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        lbt_upl67.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking raw cargo", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim rom As String = ds.Tables(0).Rows(i)(2).ToString()
                            Dim cekvalue As Integer = CheckROM(rom)
                            If cekvalue = 0 Then
                                lbt_upl67.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & rom & " is unknown (line " & i + 1 & "), upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        lbt_upl67.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking ROM", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim unt As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim cekvalue As Integer = CheckDuplicate(unt)
                            If cekvalue = 0 Then
                                lbt_upl67.Enabled = True
                                WeightBridge.csclass.mymsg(":: Duplicate data detected, use edit. Upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        lbt_upl67.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on duplicate checking", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim unt As String = ds.Tables(0).Rows(i)(0).ToString().Trim
                            Dim mat As String = ds.Tables(0).Rows(i)(1).ToString().Trim
                            Dim src As String = ds.Tables(0).Rows(i)(2).ToString().Trim
                            Dim kon As String = GetCompany(unt)
                            Dim dtm As String = DateTime.Now()
                            Dim shf As String = GetShift(dtm)
                            Dim sch As String = GetSchedulled(unt, dtm)

                            Dim cekvalue As Integer = CheckDuplicate(unt)
                            If cekvalue = 1 Then
                                inspas67(unt, mat, src, kon, dtm, shf, sch)
                            End If

                        Next
                    Catch
                        lbt_upl67.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on inserting data", "#")
                        Exit Sub

                    End Try

                    WeightBridge.csclass.mymsg("Data upload complete", "Passing?v=1")

                    '>>>>>>>>>>> READ EXCEL DATA AND SHOW TO A GRIDVIEW (FOR DEBUGGING) <<<<<<<<<<<<<<<<
                    'Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()
                    'oleda.SelectCommand = ocmd1

                    '' Create a DataSet which will hold the data extracted from the worksheet.
                    'Dim ds As DataSet = New DataSet()

                    '' Fill the DataSet from the data extracted from the worksheet.
                    'oleda.Fill(ds)

                    '' Bind the data to the GridView
                    'grid_opor2.DataSource = ds.Tables(0).DefaultView
                    'grid_opor2.DataBind()

                Catch eq As SqlException
                    lbt_upl67.Enabled = True
                    WeightBridge.csclass.mymsg(":: error sql", "#")
                    Exit Sub

                Catch ee As DataException
                    lbt_upl67.Enabled = True
                    WeightBridge.csclass.mymsg(":: error data", "#")
                    Exit Sub

                End Try


            Else
                lbt_upl67.Enabled = True
                WeightBridge.csclass.mymsg(":: There is no file to upload", "#")
                Exit Sub

            End If
        Catch
            lbt_upl67.Enabled = True
            WeightBridge.csclass.mymsg(":: unknown error", "#")
            Exit Sub

        End Try

        lbt_upl67.Enabled = True
    End Sub

    Protected Sub inspas67(unt As String, mat As String, src As String, kon As String, dtm As String, shf As String, sch As String)

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "INSERT INTO [tbl_TruckPass] ([TruckNo],[MaterialKode],[SourceKode],[KontraktorKode],[TransactionDate],[KM_67Pass],[KM_29Pass],[Shift],[Schedulled],[inQueue]) " & _
            "VALUES (@TruckNo,@MaterialKode,@SourcesKode,@KontraktorKode,@TransactionDate,1,0,@Shift,@schedulled,0) "
        cmd.Parameters.Add("@TruckNo", SqlDbType.VarChar).Value = unt
        cmd.Parameters.Add("@MaterialKode", SqlDbType.VarChar).Value = mat
        cmd.Parameters.Add("@SourcesKode", SqlDbType.VarChar).Value = src
        cmd.Parameters.Add("@KontraktorKode", SqlDbType.VarChar).Value = kon
        cmd.Parameters.Add("@TransactionDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtm)
        cmd.Parameters.Add("@Shift", SqlDbType.TinyInt).Value = Convert.ToInt16(shf)
        cmd.Parameters.Add("@schedulled", SqlDbType.TinyInt).Value = Convert.ToInt16(sch)
        cmd.CommandType = CommandType.Text

        conn.Open()
        'cmd.ExecuteScalar()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub


    Public Sub updTruck(ByVal idp As String, ByVal km As String, ByVal blc As Integer)
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()

        cmd.Connection = conn
        If km = "29" Then
            If blc = 1 Then
                cmd.CommandText = "UPDATE tbl_TruckPass SET [KM_29Pass] = 1, [TD_29Pass] = GETDATE(), [isBlock29] = 1 WHERE ID_Pass = " & idp
            Else
                cmd.CommandText = "UPDATE tbl_TruckPass SET [KM_29Pass] = 1, [TD_29Pass] = GETDATE()  WHERE ID_Pass = " & idp
            End If

        Else
            If blc = 1 Then
                cmd.CommandText = "UPDATE tbl_TruckPass SET [inQueue] = 1, [TD_inQueue] = GETDATE(), [isBlockQueue] = 1  WHERE ID_Pass = " & idp
            Else
                cmd.CommandText = "UPDATE tbl_TruckPass SET [inQueue] = 1, [TD_inQueue] = GETDATE()  WHERE ID_Pass = " & idp
            End If
        End If
        cmd.CommandType = CommandType.Text
        conn.Open()
        cmd.ExecuteNonQuery()

        conn.Close()
        connect = Nothing
    End Sub

    Public Sub insTruck(ByVal trc As String, ByVal km As String)
        Dim kon As String = ""
        Dim shf As String = "1"
        Dim _sch As String = "0"

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using rconn = New SqlConnection(connect)
            rconn.Open()
            Using rcmd = New SqlCommand("SELECT TOP 1 KontraktorKode FROM dbo.v_unit v WHERE (TruckNo = '" & trc & "') ", rconn)
                Using dr = rcmd.ExecuteReader()
                    If dr.HasRows Then
                        While dr.Read
                            kon = dr(0).ToString()
                        End While
                    End If
                    dr.Close()
                End Using
            End Using
            rconn.Close()
        End Using

        Using rconn = New SqlConnection(connect)
            rconn.Open()
            Using rcmd = New SqlCommand("SELECT TOP 1 schedulled FROM vwPa_trucklist WHERE TruckNo = '" & trc & "'", rconn)
                Using dr = rcmd.ExecuteReader()
                    If dr.HasRows Then
                        _sch = "1"
                    End If
                    dr.Close()
                End Using
            End Using
            rconn.Close()
        End Using

        Dim dtm As DateTime
        Dim tgl As String = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")
        Dim jam As Integer = DateTime.Now.Hour

        If (jam >= 0 And jam <= 5) Then
            dtm = DateAdd(DateInterval.Day, -1, DateTime.Now)
            tgl = dtm.ToString("yyyy-MM-dd h:mm:ss tt")
            shf = "2"

        ElseIf (jam >= 6 And jam <= 18) Then
            tgl = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")
            shf = "1"

        ElseIf (jam >= 18) Then
            tgl = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt")
            shf = "2"
        End If

        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()

        If km = "29" Then
            cmd.Connection = conn
            cmd.CommandText = "INSERT tbl_TruckPass (TruckNo, KontraktorKode, TransactionDate, Shift, schedulled, KM_29Pass, TD_29Pass, isBlock29, miss67, KM_67Pass) " & _
                "VALUES (@TruckNo, @KontraktorKode, @TransactionDate, @Shift, @schedulled, 1, GETDATE(), 1, 1, 1) "
            cmd.Parameters.Add("@TruckNo", SqlDbType.VarChar).Value = trc
            cmd.Parameters.Add("@KontraktorKode", SqlDbType.VarChar).Value = kon
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = tgl
            cmd.Parameters.Add("@Shift", SqlDbType.TinyInt).Value = Convert.ToInt16(shf)
            cmd.Parameters.Add("@schedulled", SqlDbType.TinyInt).Value = Convert.ToInt16(_sch)
            cmd.CommandType = CommandType.Text
            conn.Open()
            cmd.ExecuteNonQuery()
        Else
            cmd.Connection = conn
            cmd.CommandText = "INSERT tbl_TruckPass (TruckNo, KontraktorKode, TransactionDate, Shift, schedulled, KM_29Pass, TD_29Pass, isBlockQueue, miss67, [inQueue], [TD_inQueue]) " & _
                "VALUES (@TruckNo, @KontraktorKode, @TransactionDate, @Shift, @schedulled, 1, GETDATE(), 1, 1, 1, GETDATE()) "
            cmd.Parameters.Add("@TruckNo", SqlDbType.VarChar).Value = trc
            cmd.Parameters.Add("@KontraktorKode", SqlDbType.VarChar).Value = kon
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = tgl
            cmd.Parameters.Add("@Shift", SqlDbType.TinyInt).Value = Convert.ToInt16(shf)
            cmd.Parameters.Add("@schedulled", SqlDbType.TinyInt).Value = Convert.ToInt16(_sch)
            cmd.CommandType = CommandType.Text
            conn.Open()
            cmd.ExecuteNonQuery()
        End If
        

        conn.Close()
        connect = Nothing
    End Sub

    Protected Sub btn_src29_Click(sender As Object, e As EventArgs) Handles btn_src29.Click
       
        Dim errno As Integer = 0
        Dim idp, m0, m1, kon, blc As String
        Dim truck As String = txt_truck29.Text

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("SELECT TOP 1 [ID_Pass], [isBlock67], [MaterialKode], [TimbMaterial], [KontraktorKode] FROM dbo.tbl_TruckPass " & _
                                            "WHERE (TruckNo = '" & truck & "') AND (CONVERT(nvarchar, TransactionDate, 101) = CONVERT(nvarchar, GETDATE(), 101)) " & _
                                            "AND ([DateClosed] IS NULL) ORDER BY TransactionDate DESC ", conn)
                Using dr = cmd.ExecuteReader()

                    If dr.HasRows Then
                        Dim cek2 As Integer = cekTruck2(truck, "29")
                        If cek2 = 0 Then
                            errno = 1
                        End If

                        While dr.Read()
                            idp = dr(0).ToString()
                            blc = dr(1).ToString()
                            m0 = dr(2).ToString()
                            m1 = dr(3).ToString()
                            kon = dr(4).ToString()

                            If blc = "1" Then
                                errno = 2
                            ElseIf m0 <> m1 Then
                                If kon <> "PAMA" Then
                                    errno = 3
                                End If
                            End If

                        End While

                        datacek(errno, "29", idp, lbl_info29, truck)

                    Else

                        Dim cek1 As Integer = cekTruck1(truck)
                        If cek1 = 0 Then errno = 4 Else errno = 5

                        datacek(errno, "29", idp, lbl_info29, truck)

                    End If

                    dr.Close()
                End Using
            End Using
            conn.Close()
        End Using
        connect = Nothing
        'GridView2.DataBind()
        txt_truck29.Focus()
        ucmenu.refresh()

    End Sub

    Protected Sub btn_srckls_Click(sender As Object, e As EventArgs) Handles btn_srckls.Click

        Dim errno As Integer = 0
        Dim idp, m0, m1, kon, blc As String
        Dim truck As String = txt_truckkls.Text

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("SELECT TOP 1 [ID_Pass], [isBlock29], [MaterialKode], [TimbMaterial] FROM dbo.tbl_TruckPass WHERE (TruckNo = '" & truck & "') " &
                                       "AND (CONVERT(nvarchar, TransactionDate, 101) = CONVERT(nvarchar, GETDATE(), 101)) AND ([DateClosed] IS NULL) ORDER BY " & _
                                       "TransactionDate DESC", conn)
                Using dr = cmd.ExecuteReader()

                    If dr.HasRows Then
                        Dim cek2 As Integer = cekTruck2(truck, "0")
                        If cek2 = 0 Then
                            errno = 0
                        End If

                        While dr.Read()
                            idp = dr(0).ToString()
                            blc = dr(1).ToString()
                            m0 = dr(2).ToString()
                            m1 = dr(3).ToString()

                            If blc = "1" Then
                                errno = 2
                            ElseIf dr(2).ToString() <> dr(3).ToString() Then
                                errno = 3
                            End If
                        End While

                        datacek(errno, "0", idp, lbl_info0, truck)


                    Else
                        Dim cek1 As Integer = cekTruck1(truck)
                        If cek1 = 0 Then errno = 4 Else errno = 5

                        datacek(errno, "0", idp, lbl_info0, truck)
                    End If
                    dr.Close()
                End Using
            End Using
            conn.Close()
        End Using
        connect = Nothing
        'GridView3.DataBind()
        txt_truckkls.DataBind()
        ucmenu.refresh()

    End Sub

    Protected Sub chb_edit1_CheckedChanged(sender As Object, e As EventArgs) Handles chb_edit1.CheckedChanged
        If chb_edit1.Checked = True Then
            Select Case Request.QueryString("v").ToString()
                Case "1"
                    MultiView1.ActiveViewIndex = 1
                Case "2"
                    MultiView1.ActiveViewIndex = 2
                Case "3"
                    MultiView1.ActiveViewIndex = 3
            End Select
        Else
            MultiView1.ActiveViewIndex = 0
        End If
    End Sub

    Protected Sub grid_edit67_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_edit67.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "isBlock").ToString() = "1" Then
                DirectCast(e.Row.FindControl("chb_k67"), CheckBox).Checked = True
            End If
            If DataBinder.Eval(e.Row.DataItem, "isBlock29").ToString() = "1" Then
                DirectCast(e.Row.FindControl("chb_k29"), CheckBox).Checked = True
            End If
            If DataBinder.Eval(e.Row.DataItem, "isBlockQ").ToString() = "1" Then
                DirectCast(e.Row.FindControl("chb_k02"), CheckBox).Checked = True
            End If
        End If
    End Sub

    Protected Sub grid_edit67_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grid_edit67.RowCommand
        If e.CommandName = "_passupdate" Then
            Try
                Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

                Dim idp As String = e.CommandArgument.ToString()
                Dim trc As String = DirectCast(grid_edit67.Rows(row.RowIndex).FindControl("txb_truck1"), TextBox).Text
                Dim mat As String = DirectCast(grid_edit67.Rows(row.RowIndex).FindControl("ddl_mat1"), DropDownList).SelectedValue.ToString()
                Dim rom As String = DirectCast(grid_edit67.Rows(row.RowIndex).FindControl("ddl_rom1"), DropDownList).SelectedValue.ToString()
                Dim b67 As String = "0" : Dim b29 As String = "0" : Dim b02 As String = "0"

                If DirectCast(grid_edit67.Rows(row.RowIndex).FindControl("chb_k67"), CheckBox).Checked = True Then
                    b67 = "1"
                End If

                If DirectCast(grid_edit67.Rows(row.RowIndex).FindControl("chb_k29"), CheckBox).Checked = True Then
                    b29 = "1"
                End If

                If DirectCast(grid_edit67.Rows(row.RowIndex).FindControl("chb_k02"), CheckBox).Checked = True Then
                    b02 = "1"
                End If

                Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
                Dim conn As SqlConnection = New SqlConnection(connect)
                Dim cmd As SqlCommand = New SqlCommand()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE tbl_TruckPass SET TruckNo=@trc, MaterialKode=@mat, SourceKode=@rom, isBlock67=@b67, isBlock29=@b29, isBlockQueue=@b02  WHERE ID_Pass = @idp "
                cmd.Parameters.AddWithValue("@trc", trc)
                cmd.Parameters.AddWithValue("@mat", mat)
                cmd.Parameters.AddWithValue("@rom", rom)
                cmd.Parameters.AddWithValue("@b67", Int16.Parse(b67))
                cmd.Parameters.AddWithValue("@b29", Int16.Parse(b29))
                cmd.Parameters.AddWithValue("@b02", Int16.Parse(b02))
                cmd.Parameters.AddWithValue("@idp", Int64.Parse(idp))

                cmd.CommandType = CommandType.Text
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                connect = Nothing

                WeightBridge.csclass.mymsg("Data Succesfully Updated ", "#")
            Catch ex As Exception
                WeightBridge.csclass.mymsg("Unknown Error :: " & Left(ex.ToString(), 50), "#")
            End Try
        End If
    End Sub

    Protected Sub grid_pass1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_pass1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "series_name").ToString()
                Case "E5000"
                    e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(3).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(3).ForeColor = System.Drawing.Color.AliceBlue
            End Select
        End If
    End Sub

    Protected Sub lbt_pasman1_Click(sender As Object, e As EventArgs) Handles lbt_pasman1.Click
        Const fName As String = "E:\Websites\pts_online\storage\doc_Passing_Truck.pdf"
        Dim fi As FileInfo = New FileInfo(fName)
        Dim sz As Long = fi.Length

        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", String.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)))
        Response.AddHeader("Content-Length", sz.ToString("F0"))
        Response.TransmitFile(fName)
        Response.End()
    End Sub

    Protected Sub rcpChart1_Load(sender As Object, e As EventArgs) Handles rcpChart1.Load
        Dim dtm As String = txt_date.Text
        open_chart(rcpChart1, dtm, 0)
    End Sub


    Protected Sub rcpXls1_Click(sender As Object, e As ImageClickEventArgs) Handles rcpXls1.Click
        WeightBridge.ExportFile.xlsport(grid_shadow1, "passing_data", Me)
    End Sub

    Protected Sub rcpPdf1_Click(sender As Object, e As ImageClickEventArgs) Handles rcpPdf1.Click
        WeightBridge.ExportFile.pdfport(grid_shadow1, "passing_data", 1, Me)
    End Sub

    Protected Sub ddl_pag1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_pag1.SelectedIndexChanged
        changePz(grid_pass1, ddl_pag1.SelectedValue, SqlDS_truckpass)
    End Sub

    Protected Sub templt1_Click(sender As Object, e As ImageClickEventArgs) Handles templt1.Click
        WeightBridge.csclass.dtemplt("pass67_upload1.xlsx", Me)
    End Sub

    Protected Sub ImageButton2_Load(sender As Object, e As EventArgs) Handles ImageButton2.Load
        ImageButton2.Attributes.Add("onClick", "window.open('passentry1.aspx?v=0', '_new','status=0,scrollbars=1,width=491,height=400,left=500,top=270'); return false; window.focus();")
    End Sub


    Protected Sub grid_edit29_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grid_edit29.RowCommand
        If e.CommandName = "_passupdate" Then
            Try
                Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

                Dim idp As String = e.CommandArgument.ToString()
                Dim trc As String = DirectCast(grid_edit29.Rows(row.RowIndex).FindControl("txb_truck2"), TextBox).Text
                Dim mat As String = DirectCast(grid_edit29.Rows(row.RowIndex).FindControl("ddl_mat2"), DropDownList).SelectedValue.ToString()
                Dim rom As String = DirectCast(grid_edit29.Rows(row.RowIndex).FindControl("ddl_rom2"), DropDownList).SelectedValue.ToString()
                Dim b29 As String = "0" : Dim b02 As String = "0"

                If DirectCast(grid_edit29.Rows(row.RowIndex).FindControl("chb_k69"), CheckBox).Checked = True Then
                    b29 = "1"
                End If

                If DirectCast(grid_edit29.Rows(row.RowIndex).FindControl("chb_k70"), CheckBox).Checked = True Then
                    b02 = "1"
                End If

                Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
                Dim conn As SqlConnection = New SqlConnection(connect)
                Dim cmd As SqlCommand = New SqlCommand()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE tbl_TruckPass SET TruckNo=@trc, MaterialKode=@mat, SourceKode=@rom, isBlock29=@b29, isBlockQueue=@b02  WHERE ID_Pass = @idp "
                cmd.Parameters.AddWithValue("@trc", trc)
                cmd.Parameters.AddWithValue("@mat", mat)
                cmd.Parameters.AddWithValue("@rom", rom)
                cmd.Parameters.AddWithValue("@b29", Int16.Parse(b29))
                cmd.Parameters.AddWithValue("@b02", Int16.Parse(b02))
                cmd.Parameters.AddWithValue("@idp", Int64.Parse(idp))

                cmd.CommandType = CommandType.Text
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                connect = Nothing

                WeightBridge.csclass.mymsg("Data Succesfully Updated ", "#")
            Catch ex As Exception
                WeightBridge.csclass.mymsg("Unknown Error :: " & Left(ex.ToString(), 50), "#")
            End Try
        End If
    End Sub

    Protected Sub grid_edit02_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grid_edit02.RowCommand
        If e.CommandName = "_passupdate" Then
            Try
                Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)

                Dim idp As String = e.CommandArgument.ToString()
                Dim trc As String = DirectCast(grid_edit02.Rows(row.RowIndex).FindControl("txb_truck3"), TextBox).Text
                Dim mat As String = DirectCast(grid_edit02.Rows(row.RowIndex).FindControl("ddl_mat3"), DropDownList).SelectedValue.ToString()
                Dim rom As String = DirectCast(grid_edit02.Rows(row.RowIndex).FindControl("ddl_rom3"), DropDownList).SelectedValue.ToString()
                Dim b02 As String = "0"

                If DirectCast(grid_edit02.Rows(row.RowIndex).FindControl("chb_k72"), CheckBox).Checked = True Then
                    b02 = "1"
                End If

                Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
                Dim conn As SqlConnection = New SqlConnection(connect)
                Dim cmd As SqlCommand = New SqlCommand()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE tbl_TruckPass SET TruckNo=@trc, MaterialKode=@mat, SourceKode=@rom, isBlockQueue=@b02  WHERE ID_Pass = @idp "
                cmd.Parameters.AddWithValue("@trc", trc)
                cmd.Parameters.AddWithValue("@mat", mat)
                cmd.Parameters.AddWithValue("@rom", rom)
                cmd.Parameters.AddWithValue("@b02", Int16.Parse(b02))
                cmd.Parameters.AddWithValue("@idp", Int64.Parse(idp))

                cmd.CommandType = CommandType.Text
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                connect = Nothing

                WeightBridge.csclass.mymsg("Data Succesfully Updated ", "#")
            Catch ex As Exception
                WeightBridge.csclass.mymsg("Unknown Error :: " & Left(ex.ToString(), 50), "#")
            End Try
        End If
    End Sub

    Protected Sub grid_edit29_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_edit29.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "isBlock29").ToString() = "1" Then
                DirectCast(e.Row.FindControl("chb_k69"), CheckBox).Checked = True
            End If
            If DataBinder.Eval(e.Row.DataItem, "isBlockQ").ToString() = "1" Then
                DirectCast(e.Row.FindControl("chb_k70"), CheckBox).Checked = True
            End If
        End If
    End Sub

    Protected Sub grid_edit02_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_edit02.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If DataBinder.Eval(e.Row.DataItem, "isBlockQ").ToString() = "1" Then
                DirectCast(e.Row.FindControl("chb_k72"), CheckBox).Checked = True
            End If
        End If
    End Sub
End Class