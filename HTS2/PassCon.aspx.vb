Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb

'For converting HTML TO PDF- START
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.xml
Imports iTextSharp.text.html.simpleparser
Imports System.Text.RegularExpressions
'For converting HTML TO PDF- END

Partial Class HTS2_PassCon
    Inherits System.Web.UI.Page

    Public _shf As Integer = 1
    Public fileLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileLoc").ToString()

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '           server control at run time. 
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

    Protected Sub GetLastSent(ByVal lbl As Label, ByVal type As String)
        Dim shf As String
        If type = "48" Then
            shf = "Day"
        Else
            shf = "Night"
        End If

        Try
            Dim connect As [String] = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Using conn As New SqlConnection(connect)
                conn.Open()
                Using cmd As New SqlCommand(" SELECT MAX([dtm_last_sent]) as data1 FROM [HTSdb].[dbo].[tbl_sys_notification_monitor] m " & _
                                                              " WHERE ([id_notmonitor_type] = " & type & ")", conn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.HasRows Then
                            While dr.Read()
                                Dim _dtm As DateTime = DateTime.Parse(dr("data1").ToString())
                                lbl.Text = String.Concat("Supply Passing Plan, " & shf & " Shift.<br /> Last broadcasted at: ", _dtm.ToString("dd MMM yy, HH:mm"))
                            End While
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect = Nothing
        Catch
            lbl.Text = String.Concat("Supply Passing Plan, " & shf & " Shift.")
        End Try

    End Sub

    Protected Function GetUsrCompany() As String
        Try
            Dim com As String = "ADARO"
            If Request.Cookies("htspub")("comname") IsNot Nothing Then
                com = Request.Cookies("htspub")("comname").ToString()
            End If
            Return com
        Catch ex As Exception
            Return "ADARO"
        End Try
    End Function

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Page.Title = "Truck Distribution to ROM  "
        Dim v As String = Request.QueryString("v")

        Dim jam As Integer = DateTime.Now.Hour
        Dim tglKmaren As DateTime = DateTime.Today
        Dim tglToday As DateTime = DateTime.Today

        If (jam >= 0 And jam <= 5) Then
            _shf = 2
            tglKmaren = DateAdd(DateInterval.Day, -1, tglToday)

        ElseIf (jam >= 6 And jam <= 18) Then
            _shf = 1

        ElseIf (jam >= 18) Then
            _shf = 2

        End If

        Dim ugrup As Int32 = 5
        Dim ucomp As String = "ADARO"
        'If Request.Cookies("htspub")("idgroup") IsNot Nothing Then
        '    ugrup = Int32.Parse(Request.Cookies("htspub")("idgroup"))
        '    ucomp = GetUsrCompany()
        'End If

        ucomp = "SIS"
        Select Case v
            Case "0", ""
                MultiView1.ActiveViewIndex = 0

                txt_date1.Text = tglToday.ToString("MM/dd/yyyy")

                ds_sp_planpass_rep1.SelectParameters.Clear()
                ds_sp_planpass_rep1.SelectParameters.Add("dtm", dbType:=DbType.String, value:=tglToday.ToString("MM/dd/yyyy"))
                ds_sp_planpass_rep1.SelectParameters.Add("shf", dbType:=DbType.String, value:="1")
                ds_sp_planpass_rep1.SelectParameters.Add("kon", dbType:=DbType.String, value:=ucomp)

                grid_plan1.DataBind()

                ds_sp_passplan_rep2.SelectParameters.Clear()
                ds_sp_passplan_rep2.SelectParameters.Add("dtm", dbType:=DbType.String, value:=tglToday.ToString("MM/dd/yyyy"))
                ds_sp_passplan_rep2.SelectParameters.Add("shf", dbType:=DbType.String, value:="2")
                ds_sp_passplan_rep2.SelectParameters.Add("kon", dbType:=DbType.String, value:=ucomp)

                grid_plan2.DataBind()

                GetLastSent(lbl_info1, "48")
                GetLastSent(lbl_info2, "49")


            Case "1"
                MultiView1.ActiveViewIndex = 1
                Dim dtm As DateTime

                If tglKmaren <> tglToday Then
                    txt_date3.Text = tglKmaren.ToString("MM/dd/yyyy")
                    dtm = tglKmaren
                Else
                    txt_date3.Text = tglToday.ToString("MM/dd/yyyy")
                    dtm = tglToday
                End If

                ds_dist1.SelectParameters.Clear()
                ds_dist1.SelectParameters.Add("dtm", dtm.ToString("MM/dd/yyyy"))
                ds_dist1.SelectParameters.Add("com", ucomp)
                GridView2.DataBind()

                pan_uc1.Visible = True

                SqlDS_truckpass.SelectParameters.Clear()
                SqlDS_truckpass.SelectParameters.Add("dtm", dtm.ToString("MM/dd/yyyy"))
                SqlDS_truckpass.SelectParameters.Add("kon", ucomp)
                grid_pass1.DataBind()


        End Select
    End Sub

    
    Protected Sub btn_fil2_Click(sender As Object, e As EventArgs) Handles btn_fil2.Click
        Dim dtm1 As String = txt_date3.Text
        Dim unt As String = txt_truck1.Text
        Dim raw As String = ddl_raw2.SelectedValue.ToString()
        Dim rom As String = ddl_rom1.SelectedValue.ToString()
        Dim ucomp As String = GetUsrCompany()

        SqlConnection.ClearAllPools()
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As New SqlConnection(connect)
        Dim cmd As New SqlCommand()
        cmd.Connection = conn

        'WeightBridge.csclass.mymsg(" cek ==> " + cek, "#");

        cmd.CommandText = "_sp_pascon_lis1"
        cmd.Parameters.AddWithValue("@dtm", dtm1)
        cmd.Parameters.AddWithValue("@com", ucomp)
        If Not String.IsNullOrEmpty(unt) Then
            cmd.Parameters.AddWithValue("@unt", unt)
        End If
        If raw <> "all" Then
            cmd.Parameters.AddWithValue("@mat", raw)
        End If
        If rom <> "all" Then
            cmd.Parameters.AddWithValue("@rom", rom)
        End If
        cmd.CommandType = CommandType.StoredProcedure
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
        cmd.Parameters.Clear()
        connect = Nothing
        
        GridView2.DataBind()

        SqlDS_truckpass.SelectParameters.Clear()
        SqlDS_truckpass.SelectParameters.Add("@dtm", dtm1)
        SqlDS_truckpass.SelectParameters.Add("@kon", ucomp)
        If Not String.IsNullOrEmpty(unt) Then
            cmd.Parameters.AddWithValue("@trc", unt)
        End If
        If raw <> "all" Then
            cmd.Parameters.AddWithValue("@mat", raw)
        End If
        grid_pass1.DataBind()


        
    End Sub

    Protected Sub lbt_uplTD_Click(sender As Object, e As EventArgs) Handles lbt_uplTD.Click
        lbt_uplTD.Enabled = False
        Dim PictureUpload As FileUpload = FileUpload1

        Try
            If PictureUpload.HasFile Then
                If String.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", True) <> 0 Then
                    ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Only MS-Excel 2007 and above (.xlsx) documents available for data impor');</script>")
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
                        ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('File invalid');</script>")
                        Exit Sub
                    End If
                End If

                Try

                    oconn.Open()
                    Dim dtExcelSchema As DataTable
                    dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
                    oconn.Close()

                    'oconn.Open()
                    'Dim ocmd As New OleDbCommand("select * from [" & SheetName & "] ", oconn)
                    'Dim ode As OleDbDataReader = ocmd.ExecuteReader()

                    'While ode.Read()
                    '    Dim connect1 As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
                    '    Dim prdcheck As String = "SELECT TOP 1 dtm_qualportal FROM dbo.tbl_qualportal por WHERE " & _
                    '            "(YEAR(dtm_qualportal) =  YEAR(CONVERT(smalldatetime, '" & ode(0).ToString() & "'))) " & _
                    '            "AND (MONTH(dtm_qualportal) =  MONTH(CONVERT(smalldatetime, '" & ode(0).ToString() & "'))) " & _
                    '            " AND (KontraktorKode='" & ode(1).ToString() & "') AND (MaterialKode='" & ode(2).ToString() & "') AND (productKode=" & ode(3).ToString() & ")"
                    '    Using conn = New SqlConnection(connect1)
                    '        conn.Open()
                    '        Using cmd = New SqlCommand(prdcheck, conn)
                    '            Using dr = cmd.ExecuteReader()
                    '                If dr.HasRows Then
                    '                    oconn.Close()
                    '                    oconn = Nothing
                    '                    delFile(picPath)
                    '                    ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Duplicated Periode of Quality Portal Found.\n Please Edit Data Individually, Import Canceled.');location.href='Quality'</script>")
                    '                    Exit Sub
                    '                End If
                    '                dr.Close()
                    '            End Using
                    '        End Using
                    '        conn.Close()
                    '    End Using
                    '    connect1 = Nothing
                    'End While
                    'oconn.Close()

                    oconn.Open()
                    Dim ocmd1 As New OleDbCommand("select * from [" & SheetName & "]", oconn)
                    Dim odr As OleDbDataReader = ocmd1.ExecuteReader()

                    While odr.Read()
                        Dim dtm As String = odr(0).ToString().Trim
                        Dim kon As String = odr(1).ToString().Trim
                        Dim unt As String = odr(2).ToString().Trim
                        Dim mat As String = odr(3).ToString().Trim
                        Dim src As String = odr(4).ToString().Trim
                        Dim shf As String = odr(5).ToString().Trim

                        insTD(dtm, kon, unt, mat, src, shf)
                    End While

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

                    oconn.Close()

                Catch eq As SqlException
                    ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('error sql');</script>")
                    Exit Sub

                Catch ee As DataException
                    ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('error data');</script>")
                    Exit Sub

                End Try

                '---- DELETE FILE EXCEL DARI SERVER ------
                delFile(picPath)
                ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Data upload sucessfull');location.href='PassCon?v=1'</script>")

            Else
                ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('There is no file to upload');</script>")

            End If
        Catch
        End Try
        lbt_uplTD.Enabled = True
    End Sub

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

    Protected Sub insTD(dtm As String, kon As String, unt As String, mat As String, src As String, shf As String)
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "INSERT INTO [tbl_TruckRomDist] ([TransactionDate],[KontraktorKode],[TruckNo],[MaterialKode],[SourceKode],[Shift]) " & _
            "VALUES (@TransactionDate,@KontraktorKode,@TruckNo,@MaterialKode,@SourcesKode,@Shift) "
        cmd.Parameters.Add("@TransactionDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtm)
        cmd.Parameters.Add("@KontraktorKode", SqlDbType.VarChar).Value = kon
        cmd.Parameters.Add("@TruckNo", SqlDbType.VarChar).Value = unt
        cmd.Parameters.Add("@MaterialKode", SqlDbType.VarChar).Value = mat
        cmd.Parameters.Add("@SourcesKode", SqlDbType.VarChar).Value = src
        cmd.Parameters.Add("@Shift", SqlDbType.TinyInt).Value = Convert.ToInt16(shf)
        cmd.CommandType = CommandType.Text

        conn.Open()
        'cmd.ExecuteScalar()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Protected Sub img_tplt2_Click(sender As Object, e As ImageClickEventArgs) Handles img_tplt2.Click
        Const fName As String = "E:\Websites\pts_online\storage\trudis_upload1.xlsx"
        Dim fi As FileInfo = New FileInfo(fName)
        Dim sz As Long = fi.Length

        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", String.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)))
        Response.AddHeader("Content-Length", sz.ToString("F0"))
        Response.TransmitFile(fName)
        Response.End()
    End Sub

    Protected Sub img_xls2_Click(sender As Object, e As ImageClickEventArgs) Handles img_xls2.Click
        GridView2.Columns(9).Visible = False
        'WeightBridge.ExportFile.xlsport(GridView2, "Truck_ROMDistribution_", Me)
    End Sub

    Public Function ranfn() As String
        Randomize()

        Dim value As Integer = CInt(Int((200 * Rnd()) + 1))
        Dim filename As String = String.Concat(value.ToString(), "_", DateTime.Now.ToString("yyMMHHmmss"), ".xls")

        Return filename
    End Function

    Protected Sub img_pdf2_Click(sender As Object, e As ImageClickEventArgs) Handles img_pdf2.Click
        ds_dist1.SelectCommand = sqlkeep1.Text
        GridView2.DataBind()
        'WeightBridge.ExportFile.pdfport(GridView2, "trudis", 1, Me)
    End Sub

    Protected Sub ds_dist1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles ds_dist1.Selected
        If e.Exception IsNot Nothing Then
            'tes
        Else
            lbl_tot2.Text = String.Format("Total record: {0} found", e.AffectedRows)
        End If
    End Sub

    Protected Sub ddl_pag2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_pag2.SelectedIndexChanged
        changePz(GridView2, ddl_pag2.SelectedValue, ds_dist1, sqlkeep1.Text)
    End Sub

    Public Sub changePz(ByVal grd As GridView, ByVal amt As Integer, ByVal ds As SqlDataSource, ByVal sq1 As String)
        ds.SelectCommand = sq1
        grd.PageSize = amt
        grd.DataBind()
    End Sub

    Protected Sub img_add2_Load(sender As Object, e As EventArgs) Handles img_add2.Load
        img_add2.Attributes.Add("onClick", "window.open('passentry2.aspx?v=1', '_new','status=0,scrollbars=1,width=691,height=400,left=500,top=270'); return false; window.focus();")
    End Sub

    Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        ds_dist1.SelectCommand = sqlkeep1.Text
        GridView2.DataBind()
    End Sub

    Protected Sub GridView2_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView2.Sorting
        ds_dist1.SelectCommand = sqlkeep1.Text
        GridView2.DataBind()
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        'Dim s As Object = GridView2.DataKeys(Integer.Parse(e.CommandArgument.ToString())).Value
        'dtkeep1.Text = s.ToString()
        'MultiView1.ActiveViewIndex = 2
        'ds_dist2.DataBind()

        'pan_uc1.Visible = False
    End Sub

    Protected Sub dtv_plan1_ModeChanging(sender As Object, e As DetailsViewModeEventArgs) Handles dtv_plan1.ModeChanging
        If e.NewMode = DetailsViewMode.Edit Then
            If e.CancelingEdit = True Then
                MultiView1.ActiveViewIndex = 1
                pan_uc1.Visible = True
            End If
        End If
    End Sub

    Protected Sub ds_dist2_Updated(sender As Object, e As SqlDataSourceStatusEventArgs) Handles ds_dist2.Updated
        MultiView1.ActiveViewIndex = 1
        ds_dist2.DataBind()
        pan_uc1.Visible = True
    End Sub

    Protected Sub img_xls1_Click(sender As Object, e As ImageClickEventArgs) Handles img_xls1.Click
        'WeightBridge.ExportFile.xlsport(grid_plan1, "passing_plan_shif1", Me)
        'xlsport("ds_sp_planpass_rep1", grid_plan1, "passing_plan_shif1")'
    End Sub

    Protected Sub img_xls3_Click(sender As Object, e As ImageClickEventArgs) Handles img_xls3.Click
        'WeightBridge.ExportFile.xlsport(grid_plan2, "passing_plan_shif2", Me)
        'xlsport("ds_sp_passplan_rep2", grid_plan2, "passing_plan_shif2")'

    End Sub

    Public Sub xlsport(ByVal ds As String, ByVal grd As GridView, ByVal _fnm As String)
        grd.Visible = True
        WeightBridge.csclass.gridstrip(grd)

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
            WeightBridge.csclass.gridstrip(grd)
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

 
    Protected Sub img_pdf1_Click(sender As Object, e As ImageClickEventArgs) Handles img_pdf1.Click
        ' WeightBridge.ExportFile.pdfport(grid_plan1, "passing_plan_shif1", 1, Me)
    End Sub

    Protected Sub img_pdf3_Click(sender As Object, e As ImageClickEventArgs) Handles img_pdf3.Click
        'WeightBridge.ExportFile.pdfport(grid_plan2, "passing_plan_shif2", 1, Me)
    End Sub

    Protected Sub grid_plan1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_plan1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            MergeRows(grid_plan1, 0, 0)
        End If
    End Sub

    Public Sub MergeRows(gvw As GridView, target As Integer, _ref As Integer)
        Dim rowIndex As Integer = gvw.Rows.Count - 2
        While rowIndex >= 0
            Dim gvRow As GridViewRow = gvw.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gvw.Rows(rowIndex + 1)

            For cellCount As Integer = target To target
                If (gvRow.Cells(_ref).Text = gvPreviousRow.Cells(_ref).Text) Then
                    If gvPreviousRow.Cells(cellCount).RowSpan < 2 Then
                        gvRow.Cells(cellCount).RowSpan = 2
                    Else
                        gvRow.Cells(cellCount).RowSpan = gvPreviousRow.Cells(cellCount).RowSpan + 1
                    End If
                    gvPreviousRow.Cells(cellCount).Visible = False

                End If
            Next
            rowIndex += -1
        End While
    End Sub

    Protected Sub grid_plan2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_plan2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            MergeRows(grid_plan2, 0, 0)
        End If
    End Sub

    Protected Sub btn_filter1_Click(sender As Object, e As EventArgs) Handles btn_filter1.Click
        ds_sp_planpass_rep1.SelectParameters.Clear()
        ds_sp_planpass_rep1.SelectParameters.Add("dtm", dbType:=DbType.String, value:=txt_date1.Text)
        ds_sp_planpass_rep1.SelectParameters.Add("shf", dbType:=DbType.String, value:="1")
        ds_sp_planpass_rep1.SelectParameters.Add("kon", dbType:=DbType.String, value:=Request.Cookies("htspub")("comname"))
        grid_plan1.DataBind()

        ds_sp_passplan_rep2.SelectParameters.Clear()
        ds_sp_passplan_rep2.SelectParameters.Add("dtm", dbType:=DbType.String, value:=txt_date1.Text)
        ds_sp_passplan_rep2.SelectParameters.Add("shf", dbType:=DbType.String, value:="2")
        ds_sp_passplan_rep2.SelectParameters.Add("kon", dbType:=DbType.String, value:=Request.Cookies("htspub")("comname"))
        grid_plan2.DataBind()
    End Sub

    Protected Sub SqlDS_truckpass_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDS_truckpass.Selected
        If e.Exception IsNot Nothing Then
            'tes
        Else
            lbl_tot3.Text = String.Format("Total record: {0} found", e.AffectedRows)
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

    Protected Sub cbSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim objChkAll As CheckBox = DirectCast(GridView2.HeaderRow.FindControl("chkSelectAll"), CheckBox)
        If objChkAll.Checked Then
            For Each objGVR As GridViewRow In GridView2.Rows
                Dim objChkIndividual As CheckBox = DirectCast(objGVR.FindControl("cbRows"), CheckBox)
                objChkIndividual.Checked = True
            Next
        Else
            For Each objGVR As GridViewRow In GridView2.Rows
                Dim objChkIndividual As CheckBox = DirectCast(objGVR.FindControl("cbRows"), CheckBox)
                objChkIndividual.Checked = False
            Next
        End If

    End Sub
End Class
