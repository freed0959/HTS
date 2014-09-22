Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports WeightBridge.csclass
'For converting HTML TO PDF- START
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.xml
Imports iTextSharp.text.html.simpleparser
Imports System.Text.RegularExpressions
'For converting HTML TO PDF- END

Partial Class Default2
    Inherits System.Web.UI.Page

    Public fileLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileLoc").ToString()

#Region "function and subs"


    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Public Sub xlsport(ByVal ds As String, ByVal grd As GridView, ByVal _fnm As String)
        grd.Visible = True
        gridstrip(grd)

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

    Public Sub MergeRowsWithSameContent(ByVal gvw As GridView, ByVal target As Integer, ByVal ref As Integer)
        For rowIndex As Integer = gvw.Rows.Count - 2 To 0 Step -1
            Dim gvRow As GridViewRow = gvw.Rows(rowIndex)
            Dim gvPreviousRow As GridViewRow = gvw.Rows(rowIndex + 1)

            For cellCount As Integer = target To target
                If (gvRow.Cells(ref).Text = gvPreviousRow.Cells(ref).Text) Then
                    If gvPreviousRow.Cells(cellCount).RowSpan < 2 Then
                        gvRow.Cells(cellCount).RowSpan = 2
                    Else
                        gvRow.Cells(cellCount).RowSpan = gvPreviousRow.Cells(cellCount).RowSpan + 1
                    End If
                    gvPreviousRow.Cells(cellCount).Visible = False
                End If

            Next
        Next
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

    Public Sub changePz(ByVal grd As GridView, ByVal amt As Integer, ByVal ds As SqlDataSource, ByVal sq1 As String)
        ds.SelectCommand = sq1
        grd.PageSize = amt
        grd.DataBind()
    End Sub

    Public Function getLastPortalMon() As Integer
        Try
            Dim mon As Integer

            SqlConnection.ClearAllPools()
            Dim connect1 As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim porck As String = "SELECT TOP 1 MONTH(dtm_qualportal) as mon FROM dbo.tbl_qualportal ORDER BY  dtm_qualportal DESC"
            Using conn = New SqlConnection(connect1)
                conn.Open()
                Using cmd = New SqlCommand(porck, conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.HasRows Then
                            While dr.Read
                                mon = dr(0).ToString()
                            End While
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect1 = Nothing

            Return mon

        Catch ex As Exception
            Return Integer.Parse(DateTime.Now.ToString("MM"))

        End Try

    End Function

    Public Function getLastPortalYer() As Integer
        Try
            Dim yer As Integer

            SqlConnection.ClearAllPools()
            Dim connect1 As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim porck As String = "SELECT TOP 1 YEAR(dtm_qualportal) as yer FROM dbo.tbl_qualportal ORDER BY  dtm_qualportal DESC"
            Using conn = New SqlConnection(connect1)
                conn.Open()
                Using cmd = New SqlCommand(porck, conn)
                    Using dr = cmd.ExecuteReader()
                        If dr.HasRows Then
                            While dr.Read
                                yer = dr(0).ToString()
                            End While
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect1 = Nothing

            Return yer

        Catch ex As Exception
            Return Integer.Parse(DateTime.Now.ToString("yyyy"))

        End Try

    End Function

    Protected Function valid(myreader As OleDbDataReader, stval As Integer) As String
        'if any columns are found null then they are replaced by zero
        Dim val As Object = myreader(stval)
        If Not IsDBNull(val) Then
            Return val.ToString()
        Else
            Return Convert.ToString(0)
        End If
    End Function


    Public Function GetLastWeproId() As Integer
        Dim _id As Integer

        SqlConnection.ClearAllPools()
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()

        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP 1 id_qualplan_containr FROM dbo.tbl_qualplan_containr ORDER BY plan_enddate"
        conn.Open()
        _id = cmd.ExecuteScalar()
        conn.Close()
        conn = Nothing

        Return _id
    End Function

    Public Sub ann(ByVal idp As String)
        Try
            If Not String.IsNullOrEmpty(idp) Then
                Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
                Dim conn As SqlConnection = New SqlConnection(connect)
                Dim cmd As SqlCommand = New SqlCommand()

                cmd.Connection = conn
                cmd.CommandText = "_sp_ql_uwpnot1"
                cmd.Parameters.AddWithValue("@idp", System.Data.DbType.Int64).Value = Int64.Parse(idp)
                cmd.CommandType = CommandType.StoredProcedure
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Else
                WeightBridge.csclass.mymsg(":: failed to send email", "#")
            End If
        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: failed to send email", "#")

        End Try

    End Sub

    Public Sub ann1(ByVal dtm As String)
        Try
            If Not String.IsNullOrEmpty(dtm) Then
                SqlConnection.ClearAllPools()
                Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
                Dim conn As SqlConnection = New SqlConnection(connect)
                Dim cmd As SqlCommand = New SqlCommand()

                cmd.Connection = conn
                cmd.CommandText = "_sp_ql_uqpnot1"
                cmd.Parameters.AddWithValue("@dtm", System.Data.DbType.String).Value = dtm
                cmd.CommandType = CommandType.StoredProcedure
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Else
                WeightBridge.csclass.mymsg(":: failed to send email", "#")
            End If
        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: failed to send email", "#")

        End Try

    End Sub

    Public Function cekpor1(ByVal dtm As String, ByVal kon As String, ByVal mat As String, ByVal prod As String) As Integer

        Dim idprod As String = cekprod(prod)

        Dim connect1 As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim prdcheck As String = "SELECT TOP 1 dtm_qualportal FROM dbo.tbl_qualportal por WHERE " & _
                "(YEAR(dtm_qualportal) =  YEAR(CONVERT(smalldatetime, '" & dtm & "'))) " & _
                "AND (MONTH(dtm_qualportal) =  MONTH(CONVERT(smalldatetime, '" & dtm & "'))) " & _
                " AND (KontraktorKode='" & kon & "') AND (MaterialKode='" & mat & "') AND (productKode=" & idprod & ")"
        Using conn = New SqlConnection(connect1)
            conn.Open()
            Using cmd = New SqlCommand(prdcheck, conn)
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
        connect1 = Nothing
    End Function

    Public Function cekprod(ByVal prod As String) As String
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("select top 1 id_ref_master from tbl_ref_master where ref_name = '" & prod & "'", conn)
                Using dr = cmd.ExecuteReader()
                    If dr.HasRows Then
                        Dim _pro As String
                        While dr.Read
                            _pro = dr(0).ToString()
                        End While
                        Return _pro
                    Else
                        Return Convert.ToString(0)
                    End If
                    dr.Close()
                End Using
            End Using
            conn.Close()
        End Using
        connect = Nothing
    End Function

    Public Function GetProduct(ByVal prod As String) As String
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()

        cmd.Connection = conn
        cmd.CommandText = "select id_ref_master from tbl_ref_master where ref_name = '" & prod & "'"
        conn.Open()
        Return cmd.ExecuteScalar()
        conn.Close()
    End Function


    Public Function cekmat(ByVal mat As String) As Integer
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("select TOP 1 Kode from dbo.v_Material where Kode = '" & mat & "'", conn)
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

    Public Function cekcomp(ByVal kon As String) As Integer
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("select TOP 1 Kode from dbo.v_Kontraktor where Kode = '" & kon & "'", conn)
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

    Public Function cekdup(ByVal kon As String, ByVal mat As String, ByVal idw As String) As Integer
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand("SELECT TOP 1 [id_qualwekplan] FROM [HTSdb].[dbo].[tbl_qualwekplan] WHERE " & _
                                " ([id_qualplan_containr]=" & idw & ") AND (MaterialKode='" & mat & "') AND (KontraktorKode='" & kon & "')", conn)
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

    Public Function CheckFloat(ByVal strvalue As String) As Double
        Try
            If Not String.IsNullOrEmpty(strvalue) Then
                Dim _val As Double
                _val = Double.Parse(strvalue)

                Return _val
            Else
                Return 0
            End If
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

    Public Function GetIdProd(ByVal mat As String) As String
        Dim _id As String

        SqlConnection.ClearAllPools()
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()

        cmd.Connection = conn
        cmd.CommandText = "SELECT TOP 1 productKode FROM dbo.tbl_qualportal Where MaterialKode = '" & mat & "' ORDER BY dtm_qualportal DESC"
        conn.Open()
        _id = cmd.ExecuteScalar()
        conn.Close()
        conn = Nothing

        Return _id
    End Function

#End Region


    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        Dim ugrup As Integer = 5
        If Request.Cookies("htspub") IsNot Nothing Then
            ugrup = Convert.ToInt16(Request.Cookies("htspub")("idgroup").ToString())
        Else
            Response.Redirect("../usrlog.aspx")
        End If

        If Not Request.QueryString("v") = Nothing Then
            Select Case Request.QueryString("v").ToString()
                Case "", "0"
                    MultiView1.ActiveViewIndex = 0
                    Page.Title = "Quality Portal "

                    If Page.IsPostBack = False Then
                        '==========> Requery Quality Portal ===
                        Dim mon1 As Integer = getLastPortalMon() : Dim yer1 As Integer = getLastPortalYer()

                        ddlMonth.SelectedValue = CInt(mon1)
                        ddlYear.SelectedValue = CInt(yer1)

                        ds_por1.SelectParameters.Clear()
                        ds_por1.SelectParameters.Add("mon", dbType:=Data.DbType.String, value:=mon1)
                        ds_por1.SelectParameters.Add("yer", dbType:=Data.DbType.String, value:=yer1)
                        ds_por1.DataBind()
                        grid_opor1.DataBind()
                        ' ===========> End Requery Quality Portal ===
                  
                    End If

                    Select Case ugrup
                        Case 2, 12
                            div_upload1.Visible = True
                            div_btn1.Visible = True
                            chb_edit_por1.Visible = True

                        Case Else
                            loadPan1.Visible = False

                    End Select

                Case "1"
                    MultiView1.ActiveViewIndex = 1
                    Page.Title = "Weekly Projections "

                    'txt_sched.Text = DateTime.Today.ToString("MM/dd/yyyy")
                    If (Page.IsPostBack = False) Then
                        ds_pln1.SelectParameters.Clear()
                        grid_plan1.DataBind()

                        Dim _id As Integer = GetLastWeproId()
                        ds_pln2.SelectParameters.Clear()
                        ds_pln2.SelectParameters.Add("idwp", _id)
                        grid_plan2.DataBind()
                    End If

                    'Dim qs As String = Request.QueryString("cek")
                    'If (qs = "1") Then
                    '    ds_pln1.SelectCommand = sqlkeep1.Text
                    '    grid_plan1.DataBind()
                    'End If

                    'Dim ss As String = Request.QueryString("updt")
                    'If (ss = "1") Then
                    '    pan_nlis1.Visible = True
                    '    ds_pln1.SelectCommand = sqlkeep1.Text
                    '    grid_plan1.DataBind()

                    'End If

                    Select Case ugrup
                        Case 2, 12
                            div_uploadWpr1.Visible = True
                            'addContainer1.Visible = True
                            grid_plan1.Columns(24).Visible = True
                            grid_plan1.Columns(25).Visible = True

                        Case Else
                            pan_wpj1.Visible = False
                    End Select

                Case "2"
                    MultiView1.ActiveViewIndex = 2

                    Dim sql1 As String = "select m.ID,convert(nvarchar,m.DateHistory, 111) as tgl ,m.Kode,m.Location,m.TM,m.TS,m.ASH,m.CalDaf,m.CalAdb,m.CalAr from v_MaterialHistory m, v_Material l where m.Kode = l.Kode ORDER BY M.DateHistory DESC"

                    SqlDataSource1.SelectCommand = sql1
                    sqlKeeper.Text = sql1

                Case "3"
                    MultiView1.ActiveViewIndex = 3
                    If Not Request.QueryString("map") = Nothing Then
                        WeightBridge.ExportFile.xlsport(grid_map1, "Material_Mapping", Me)
                        Dim scrpt As String = "window.close();"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", scrpt, True)
                    End If
            End Select
        End If


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
      
    End Sub

    Protected Sub grid_opor1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_opor1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'MergeRowsWithSameContent(grid_opor1, 2, 2)
            'MergeRowsWithSameContent(grid_opor1, 5, 6)
            'MergeRowsWithSameContent(grid_opor1, 6, 6)

            'If IsDBNull(DataBinder.Eval(e.Row.DataItem, "productKode")) = True Then
            '    e.Row.Cells.RemoveAt(0)
            '    e.Row.Cells.RemoveAt(1)
            '    e.Row.Cells(2).ColumnSpan = 3

            '    'e.Row.Cells.RemoveAt(4)
            '    'e.Row.Cells(4).ColumnSpan = 1

            '    e.Row.Cells(0).BorderStyle = BorderStyle.None
            '    e.Row.Cells(1).BorderStyle = BorderStyle.None
            '    e.Row.Cells(2).BorderStyle = BorderStyle.None
            '    e.Row.Cells(3).BorderStyle = BorderStyle.None
            '    e.Row.Cells(4).BorderStyle = BorderStyle.None

            '    DirectCast(e.Row.FindControl("DropDownlist7"), DropDownList).Visible = False
            '    DirectCast(e.Row.FindControl("cbRows"), CheckBox).Visible = False
            'End If

            Select Case DataBinder.Eval(e.Row.DataItem, "productname").ToString()
                Case "E5000"
                    e.Row.Cells(6).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(6).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(6).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(6).ForeColor = System.Drawing.Color.AliceBlue

            End Select

            '' if it's an autogenerated delete-LinkButton: '
            'Dim LnkBtnDelete As ImageButton = DirectCast(e.Row.FindControl("ImageButton2"), ImageButton)
            'LnkBtnDelete.OnClientClick = "return confirm('Are you certain you want to delete?');"
        End If

    End Sub

    Protected Sub btn_sub2_Click(sender As Object, e As EventArgs) Handles btn_sub2.Click
        Dim idwp As String = ddl_wpl1.SelectedValue.ToString()
        Dim idwp_1 As String = (ddl_wpl1.SelectedValue - 1).ToString()
        Dim mat As String = ddl_mat2.SelectedValue.ToString()
        Dim prod As String = ddl_prod2.SelectedItem.Text.ToString()

        ds_pln1.SelectParameters.Clear()
        ds_pln1.SelectParameters.Add("idwp", idwp)
        If mat.ToLower().Trim() <> "all" Then
            ds_pln1.SelectParameters.Add("mat", mat)
        End If
        If prod.ToLower().Trim() <> "all" Then
            ds_pln1.SelectParameters.Add("prod", prod)
        End If
        grid_plan1.DataBind()

        ds_pln2.SelectParameters.Clear()
        ds_pln2.SelectParameters.Add("idwp", idwp_1)
        If mat.ToLower().Trim() <> "all" Then
            ds_pln2.SelectParameters.Add("mat", mat)
        End If
        If prod.ToLower().Trim() <> "all" Then
            ds_pln2.SelectParameters.Add("prod", prod)
        End If
        grid_plan2.DataBind()

    End Sub


    Protected Sub grid_plan1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grid_plan1.RowCommand
        'If e.CommandName = "Select" Then
        '    Dim dataKeyValue As Object = grid_plan1.DataKeys(Integer.Parse(e.CommandArgument.ToString())).Value

        '    pan_ndet1.Visible = True
        '    pan_nlis1.Visible = False
        '    pan_wpj1.Visible = False

        '    ds_dtView.DataBind()
        '    dtv_plan1.DataBind()
        'End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim tahun As String = ddl_year.SelectedValue.ToString()
        Dim mtrl As String = ddl_mtrl.SelectedValue.ToString()

        Dim sql As [String] = "select m.ID,convert(nvarchar,m.DateHistory, 111) as tgl ,m.Kode,m.Location,m.TM,m.TS,m.ASH,m.CalDaf,m.CalAdb,m.CalAr from v_MaterialHistory m, v_Material l where m.Kode = l.Kode "

        If mtrl <> "ALL" Then
            sql = sql & " and m.Kode ='" & mtrl & "' "
        End If

        If tahun <> "YEAR" Then
            sql = sql & " and Year(m.DateHistory)='" & tahun & "' "
        End If

        sql = sql & " ORDER BY m.DateHistory DESC "

        SqlDataSource1.SelectCommand = sql
        GridView1.DataBind()
        sqlKeeper.Text = SqlDataSource1.SelectCommand.ToString()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        SqlDataSource1.SelectCommand = sqlKeeper.Text
        GridView1.DataBind()

    End Sub

#Region "Upload - Import weekly projection"

    Protected Sub LinkButton5_Click(sender As Object, e As EventArgs) Handles LinkButton5.Click

        LinkButton5.Enabled = False
        Dim PictureUpload As FileUpload = FileUpload1
        Dim id_qualplan_containr As String = ddl_wpl1.SelectedValue.ToString()

        Try
            If PictureUpload.HasFile Then
                If String.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", True) <> 0 Then
                    LinkButton5.Enabled = True
                    WeightBridge.csclass.mymsg(":: Only MS-Excel 2007 or above (.xlsx) files are accepted", "#")
                    Exit Sub
                End If

                Dim fileNameWoExt As String = Left(System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName), 2)
                Dim ext As String = System.IO.Path.GetExtension(PictureUpload.FileName).ToString()
                Dim picPath As String = String.Concat(fileLoc, fileNameWoExt, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName))

                PictureUpload.SaveAs(picPath)

                'WeightBridge.csclass.mymsg("--> " & picPath, "#")

                Dim oconn As New OleDbConnection
                If File.Exists(picPath) Then
                    If ext.ToString() = ".xlsx" Then
                        oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & picPath.ToString() & ";Extended Properties=" & Chr(34) & "Excel 12.0;HDR=YES" & Chr(34) & ";"
                    Else
                        LinkButton5.Enabled = True
                        WeightBridge.csclass.mymsg("Invalid File", "#")
                        Exit Sub
                    End If
                End If

                Try
                    oconn.Open()
                    Dim dtExcelSchema As DataTable

                    dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
                    ' WeightBridge.csclass.mymsg("sheet: " & SheetName & "", "#")
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
                            Dim kon As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim cekvalue As Integer = cekcomp(kon)
                            If cekvalue = 0 Then
                                LinkButton5.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & kon & " is unknown, upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton5.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking company", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim mat As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim cekvalue As Integer = cekmat(mat)
                            If cekvalue = 0 Then
                                LinkButton5.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & mat & " is unknown, upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton5.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking material", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim rom As String = ds.Tables(0).Rows(i)(15).ToString()
                            Dim cekvalue As Integer = CheckROM(rom)
                            If cekvalue = 0 Then
                                LinkButton5.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & rom & " is unknown, upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton5.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking rom", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim kon As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim mat As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim cekvalue As Integer = cekdup(kon, mat, id_qualplan_containr)
                            If cekvalue = 0 Then
                                LinkButton5.Enabled = True
                                WeightBridge.csclass.mymsg(":: Duplicate data detected, use edit. Upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton5.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on duplicate checking", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim KontraktorKode As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim MaterialKode As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim KontraktorCap As String = CheckFloat(ds.Tables(0).Rows(i)(2).ToString())
                            Dim ROMCap As String = CheckFloat(ds.Tables(0).Rows(i)(3).ToString())
                            Dim deviatCap As String = CheckFloat(ds.Tables(0).Rows(i)(4).ToString())
                            Dim blendNeed As String = CheckFloat(ds.Tables(0).Rows(i)(5).ToString())
                            Dim dayminington As String = CheckFloat(ds.Tables(0).Rows(i)(6).ToString())
                            Dim dayhaulton As String = CheckFloat(ds.Tables(0).Rows(i)(7).ToString())
                            Dim TM As String = CheckFloat(ds.Tables(0).Rows(i)(8).ToString())
                            Dim IM As String = CheckFloat(ds.Tables(0).Rows(i)(9).ToString())
                            Dim ASH_ADB As String = CheckFloat(ds.Tables(0).Rows(i)(10).ToString())
                            Dim ASH_AR As String = CheckFloat(ds.Tables(0).Rows(i)(11).ToString())
                            Dim TS_ADB As String = CheckFloat(ds.Tables(0).Rows(i)(12).ToString())
                            Dim TS_AR As String = CheckFloat(ds.Tables(0).Rows(i)(13).ToString())
                            Dim CV_AR As String = CheckFloat(ds.Tables(0).Rows(i)(14).ToString())

                            Dim Rom As String = ds.Tables(0).Rows(i)(15).ToString()
                            Dim pit_pr As String = CheckFloat(ds.Tables(0).Rows(i)(16).ToString())
                            Dim rom_pr As String = CheckFloat(ds.Tables(0).Rows(i)(17).ToString())
                            Dim HGI As String = CheckFloat(ds.Tables(0).Rows(i)(18).ToString())
                            Dim CalAdb As String = CheckFloat(ds.Tables(0).Rows(i)(19).ToString())
                            Dim CalDaf As String = CheckFloat(ds.Tables(0).Rows(i)(20).ToString())
                            Dim idprod As String = GetIdProd(MaterialKode)

                            Dim cekvalue As Integer = cekdup(KontraktorKode, MaterialKode, id_qualplan_containr)
                            If cekvalue = 1 Then
                                insertWeepro(KontraktorKode, MaterialKode, KontraktorCap, ROMCap, deviatCap, blendNeed, dayminington, dayhaulton, _
                                        TM, IM, ASH_ADB, ASH_AR, TS_ADB, TS_AR, CV_AR, id_qualplan_containr, Rom, pit_pr, rom_pr, HGI, CalAdb, CalDaf, idprod)
                                '    'WeightBridge.csclass.mymsg("" & id_qualplan_containr, "#")    
                            End If

                        Next

                        If chb_weepro1.Checked = True Then
                            ann(id_qualplan_containr)
                        End If
                        WeightBridge.csclass.mymsg("Weekly Projection Data Upload Complete", "Default2?v=1")
                    Catch
                        LinkButton5.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on inserting data", "#")
                        Exit Sub
                    End Try

                Catch eq As SqlException
                    LinkButton5.Enabled = True
                    WeightBridge.csclass.mymsg(":: error sql", "#")
                    Exit Sub

                Catch ee As DataException
                    LinkButton5.Enabled = True
                    WeightBridge.csclass.mymsg(":: error data", "#")
                    Exit Sub

                End Try

            Else
                LinkButton5.Enabled = True
                WeightBridge.csclass.mymsg(":: There is no file to upload", "#")

            End If

        Catch
            LinkButton5.Enabled = True
            WeightBridge.csclass.mymsg(":: unknown error", "#")

        End Try
        LinkButton5.Enabled = True
    End Sub

    

    Public Sub insertWeepro(ByVal kon As String, ByVal mat As String, ByVal ca1 As String, ByVal ca2 As String, ByVal ca3 As String,
                            ByVal ned As String, ByVal ton1 As String, ByVal ton2 As String, ByVal qua1 As String, ByVal qua2 As String, ByVal qua3 As String, _
                            ByVal qua4 As String, ByVal qua5 As String, ByVal qua6 As String, ByVal qua7 As String, ByVal cont As String, ByVal rom As String, _
                            pit_pr As String, rom_pr As String, hgi As String, cadb As String, cdaf As String, ByVal idprod As String)

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As SqlConnection = New SqlConnection(connect)
        Dim cmd As SqlCommand = New SqlCommand()

        cmd.Connection = conn
        cmd.CommandText = "INSERT INTO [HTSdb].[dbo].[tbl_qualwekplan] (KontraktorKode, MaterialKode, KontraktorCap, ROMCap, deviatCap, " & _
                    " blendNeed, dayminington, dayhaulton, TM, IM, ASH_ADB, ASH_AR, TS_ADB, TS_AR, CV_AR, id_qualplan_containr, SourceKode, " & _
                    " pitPercent, romPercent, HGI, Caladb, CalDaf, id_product) " & _
                    "VALUES (@KontraktorKode, @MaterialKode,@KontraktorCap, @ROMCap, @deviatCap, @blendNeed, @dayminington, @dayhaulton, " & _
                    "@TM, @IM, @ASH_ADB, @ASH_AR, @TS_ADB, @TS_AR, @CV_AR, @id_qualplan_containr, @rom, @pit_pr, @rom_pr, @hgi, @cadb, @cdaf, @idprod) "
        cmd.Parameters.AddWithValue("@KontraktorKode", kon)
        cmd.Parameters.AddWithValue("@MaterialKode", mat)
        cmd.Parameters.AddWithValue("@KontraktorCap", Convert.ToDouble(ca1))
        cmd.Parameters.AddWithValue("@ROMCap", Convert.ToDouble(ca2))
        cmd.Parameters.AddWithValue("@deviatCap", Convert.ToDouble(ca3))
        cmd.Parameters.AddWithValue("@blendNeed", Convert.ToDouble(ned))
        cmd.Parameters.AddWithValue("@dayminington", Convert.ToDouble(ton1))
        cmd.Parameters.AddWithValue("@dayhaulton", Convert.ToDouble(ton2))
        cmd.Parameters.AddWithValue("@TM", Convert.ToDouble(qua1))
        cmd.Parameters.AddWithValue("@IM", Convert.ToDouble(qua2))
        cmd.Parameters.AddWithValue("@ASH_ADB", Convert.ToDouble(qua3))
        cmd.Parameters.AddWithValue("@ASH_AR", Convert.ToDouble(qua4))
        cmd.Parameters.AddWithValue("@TS_ADB", Convert.ToDouble(qua5))
        cmd.Parameters.AddWithValue("@TS_AR", Convert.ToDouble(qua6))
        cmd.Parameters.AddWithValue("@CV_AR", Convert.ToDouble(qua7))
        cmd.Parameters.AddWithValue("@id_qualplan_containr", Convert.ToInt64(cont))
        cmd.Parameters.AddWithValue("@rom", rom)
        cmd.Parameters.AddWithValue("@pit_pr", pit_pr)
        cmd.Parameters.AddWithValue("@rom_pr", rom_pr)
        cmd.Parameters.AddWithValue("@hgi", hgi)
        cmd.Parameters.AddWithValue("@cadb", cadb)
        cmd.Parameters.AddWithValue("@cdaf", cdaf)
        cmd.Parameters.AddWithValue("@idprod", idprod)

        cmd.CommandType = CommandType.Text
        conn.Open()
        'cmd.ExecuteScalar()
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

#End Region

#Region "Upload- Import Quality Portal"

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        LinkButton1.Enabled = False
        Dim PictureUpload As FileUpload = FileUpload2

        Try
            If PictureUpload.HasFile Then
                If String.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", True) <> 0 Then
                    LinkButton1.Enabled = True
                    WeightBridge.csclass.mymsg(":: Only MS-Excel 2007 or above (*.xlsx) files are accepted", "#")
                    Exit Sub
                End If

                Dim fileNameWoExt As String = Left(System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName), 2)
                Dim ext As String = System.IO.Path.GetExtension(PictureUpload.FileName).ToString()
                Dim picPath As String = String.Concat(fileLoc, fileNameWoExt, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName))

                PictureUpload.SaveAs(picPath)

                'WeightBridge.csclass.mymsg("--> " & picPath, "#")

                Dim oconn As New OleDbConnection
                If File.Exists(picPath) Then
                    If ext.ToString() = ".xlsx" Then
                        oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & picPath.ToString() & ";Extended Properties=" & Chr(34) & "Excel 12.0;HDR=YES" & Chr(34) & ";"
                    Else
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: File invalid", "#")
                        Exit Sub
                    End If
                End If

                Try

                    oconn.Open()
                    Dim dtExcelSchema As DataTable
                    dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
                    oconn.Close()

                    'WeightBridge.csclass.mymsg("--> " & SheetName, "#")

                    oconn.Open()
                    Dim ocmd As New OleDbCommand("select * from [" & SheetName & "] ", oconn)
                    'Dim ode As OleDbDataReader = ocmd.ExecuteReader()

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
                            Dim dtm As String = ds.Tables(0).Rows(i)(0).ToString()

                            Try
                                DateTime.Parse(dtm)
                            Catch ex As Exception
                                LinkButton1.Enabled = True
                                WeightBridge.csclass.mymsg(":: null or invalid date (line " & i + 1 & "), upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End Try
                        Next
                    Catch
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking date", "#")
                        Exit Sub
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim kon As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim cekvalue As Integer = cekcomp(kon)
                            If cekvalue = 0 Then
                                LinkButton1.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & kon & " (line " & i + 1 & ") is unknown, upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking company", "#")
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim mat As String = ds.Tables(0).Rows(i)(2).ToString()
                            Dim cekvalue As Integer = cekmat(mat)
                            If cekvalue = 0 Then
                                LinkButton1.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & mat & " (line " & i + 1 & ") is unknown, upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking raw cargo", "#")
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim prod As String = ds.Tables(0).Rows(i)(3).ToString()
                            Dim cekvalue As Integer = cekprod(prod)
                            If cekvalue = "0" Then
                                LinkButton1.Enabled = True
                                WeightBridge.csclass.mymsg(":: " & prod & " (line " & i + 1 & ") is unknown, upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking product", "#")
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim dt As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim kon As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim mat As String = ds.Tables(0).Rows(i)(2).ToString()
                            Dim pd As String = ds.Tables(0).Rows(i)(3).ToString()

                            Dim cekvalue As Integer = cekpor1(dt, kon, mat, pd)
                            If cekvalue = 0 Then
                                LinkButton1.Enabled = True
                                WeightBridge.csclass.mymsg(":: Duplicate data detected, use edit. Upload canceled", "#")
                                oconn = Nothing
                                Exit Sub
                            End If
                        Next
                    Catch
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on checking duplication", "#")
                    End Try

                    Try
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim dt As String = ds.Tables(0).Rows(i)(0).ToString()
                            Dim kon As String = ds.Tables(0).Rows(i)(1).ToString()
                            Dim mat As String = ds.Tables(0).Rows(i)(2).ToString()
                            Dim pd As String = cekprod(ds.Tables(0).Rows(i)(3).ToString())

                            Dim cekvalue As Integer = cekpor1(dt, kon, mat, pd)
                            If cekvalue = 1 Then
                                insertPortal(dt, kon, mat, pd)
                            End If
                        Next

                        If chb_qpor1.Checked = True Then
                            Dim dt As DateTime = DateTime.Parse(ds.Tables(0).Rows(1)(0).ToString())
                            ann1(dt.ToString("MM/dd/yyyy"))
                        End If

                        WeightBridge.csclass.mymsg("Quality Portal data upload complete", "Default2?v=0")
                    Catch
                        LinkButton1.Enabled = True
                        WeightBridge.csclass.mymsg(":: error on inserting data", "#")


                    End Try

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
                    LinkButton1.Enabled = True
                    WeightBridge.csclass.mymsg(":: error sql", "#")
                    Exit Sub

                Catch ee As DataException
                    LinkButton1.Enabled = True
                    WeightBridge.csclass.mymsg(":: error data", "#")
                    Exit Sub

                End Try

            Else
                LinkButton1.Enabled = True
                WeightBridge.csclass.mymsg(":: There is no file to upload", "#")
                Exit Sub

            End If
        Catch
            WeightBridge.csclass.mymsg(":: unknown error", "#")

        End Try
        LinkButton1.Enabled = True
    End Sub

    Public Sub insertPortal(ByVal dtm As String, ByVal kon As String, ByVal mat As String, ByVal prod As String)
        Try
            Dim id_prod As String = cekprod(prod)

            SqlConnection.ClearAllPools()
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn As SqlConnection = New SqlConnection(connect)
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO tbl_qualportal (dtm_qualportal, KontraktorKode, MaterialKode, productKode) VALUES (@dtm_qualportal, @KontraktorKode, @MaterialKode, @productKode) "
            cmd.Parameters.Add("@dtm_qualportal", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtm)
            cmd.Parameters.Add("@KontraktorKode", SqlDbType.VarChar).Value = kon
            cmd.Parameters.Add("@MaterialKode", SqlDbType.VarChar).Value = mat
            cmd.Parameters.Add("@productKode", SqlDbType.BigInt).Value = Convert.ToInt64(prod)
            cmd.CommandType = CommandType.Text
            conn.Open()
            'cmd.ExecuteScalar()
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            conn.Close()

        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: error on inserting data", "#")

        End Try

    End Sub

#End Region


    Protected Sub templt3_Click(sender As Object, e As ImageClickEventArgs) Handles templt3.Click
        WeightBridge.csclass.dtemplt("qualportal_upload1.xlsx", Me)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim mon As String = ddlMonth.SelectedValue.ToString()
        Dim yer As String = ddlYear.SelectedValue.ToString()
        Dim kon As String = ddl_kon1.SelectedValue.ToString()
        Dim prod As String = ddl_prod1.SelectedValue.ToString()

        ds_por1.SelectParameters.Clear()
        ds_por1.SelectParameters.Add("mon", dbType:=Data.DbType.String, value:=mon)
        ds_por1.SelectParameters.Add("yer", dbType:=Data.DbType.String, value:=yer)
        If kon <> " ALL" Then
            ds_por1.SelectParameters.Add("kon", dbType:=Data.DbType.String, value:=kon)
        End If
        If Not String.IsNullOrEmpty(prod) Then
            ds_por1.SelectParameters.Add("prd", dbType:=Data.DbType.String, value:=prod)
        End If
        ds_por1.DataBind()
        grid_opor1.DataBind()
    End Sub

    Protected Sub rcpXls1_Click(sender As Object, e As ImageClickEventArgs) Handles rcpXls1.Click
        Select Case Request.QueryString("v").ToString()
            Case "0"
                grid_opor1.Columns(7).Visible = False
                WeightBridge.ExportFile.xlsport(grid_opor1, "quality_portal_", Me)
                'xlsport("ds_por1", grid_opor1, "qltypor")'
            Case "1"
                grid_plan1.Columns(4).Visible = True
                grid_plan1.Columns(24).Visible = False
                grid_plan1.Columns(25).Visible = False
                WeightBridge.ExportFile.xlsport(grid_plan1, "weekly_rtk_prjection_", Me)
                'xlsport("ds_pln1", grid_plan1, "wklyprjection")'
            Case "2"
                SqlDataSource1.SelectCommand = sqlKeeper.Text
                WeightBridge.ExportFile.xlsport(GridView1, "Material_History_", Me)
            Case "3"
                WeightBridge.ExportFile.xlsport(grid_map1, "Material_Mapping", Me)
        End Select

    End Sub

    Protected Sub ds_por1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles ds_por1.Selected
        If e.Exception IsNot Nothing Then
            'tes
        Else
            totlbl1.Text = String.Format("Total record: {0} found", e.AffectedRows)
        End If
    End Sub

    Protected Sub ddl_pag1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_pag1.SelectedIndexChanged
        changePz(GridView1, ddl_pag1.SelectedValue, SqlDataSource1, sqlKeeper.Text)
    End Sub

    Protected Sub rcpPdf1_Click(sender As Object, e As ImageClickEventArgs) Handles rcpPdf1.Click
          Select Request.QueryString("v").ToString()
            Case "0"
                grid_opor1.Columns(7).Visible = False
                WeightBridge.ExportFile.pdfport(grid_opor1, "Quality_Portal", 1, Me)
                'pdfPort(grid_opor1, 1, "qualpor")'
            Case "1"
                grid_plan1.Columns(4).Visible = True
                grid_plan1.Columns(24).Visible = False
                grid_plan1.Columns(25).Visible = False
                WeightBridge.ExportFile.pdfport(grid_plan1, "Weekly_Projection_RTK_", 1, Me)
                'pdfPort(GridView1, 1, "mathis")'
            Case "2"
                SqlDataSource1.SelectCommand = sqlKeeper.Text
                WeightBridge.ExportFile.pdfport(GridView1, "Material_History_", 1, Me)
                'pdfPort(GridView1, 1, "mathis")'
            Case "3"
                WeightBridge.ExportFile.pdfport(grid_map1, "Material_Mapping_", 1, Me)
                'pdfPort(grid_map1, 0, "MappingKode")'
        End Select
    End Sub

    Protected Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected
        If e.Exception IsNot Nothing Then
            'tes
        Else
            LabelTot.Text = String.Format("Total record: {0} found", e.AffectedRows)
        End If
    End Sub

    Protected Sub ds_pln1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles ds_pln1.Selected
        If e.Exception IsNot Nothing Then
            'tes
        Else
            lbl_info2.Text = String.Format("Total record: {0} found", e.AffectedRows)
        End If
    End Sub

    Protected Sub grid_plan1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_plan1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "prod").ToString()

                Case "E5000"
                    e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(4).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(4).ForeColor = System.Drawing.Color.AliceBlue

            End Select

            If DataBinder.Eval(e.Row.DataItem, "KontraktorKode").ToString().ToLower.Trim() = "total" Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(3).ColumnSpan = 4
                e.Row.Cells(4).Visible = False
                e.Row.Cells(5).Visible = False

                For i As Integer = 25 To 12 Step -1
                    e.Row.Cells(i).ForeColor = Drawing.Color.Transparent
                Next
                DirectCast(e.Row.FindControl("cbWpr1"), CheckBox).Visible = False
                DirectCast(e.Row.FindControl("HyperLink1"), HyperLink).Visible = False

                e.Row.BackColor = Drawing.Color.Yellow
            End If

            'Find Template Field Delete button with Control ID
            Dim deleteTemplateField As ImageButton = TryCast(e.Row.FindControl("ImageButton1"), ImageButton)
            'Set OnClientClick attribute for Delete Button
            deleteTemplateField.OnClientClick = "return confirm('Are You Sure to Delete This Data?');"
        End If
    End Sub


    Protected Sub templt1_Click(sender As Object, e As ImageClickEventArgs) Handles templt1.Click
        WeightBridge.csclass.dtemplt("weepro_upload1.xlsx", Me)
    End Sub

    Public Function cektgl(ByVal tgl As String) As Integer

        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim tgcheck As String = "SELECT TOP 1 [id_qualplan_containr] FROM [HTSdb].[dbo].[tbl_qualplan_containr] WHERE ('" & tgl & "' BETWEEN [plan_startdate] AND [plan_enddate]) "

        Using conn = New SqlConnection(connect)
            conn.Open()
            Using cmd = New SqlCommand(tgcheck, conn)
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

    

    Protected Sub ds_pln1_Deleted(sender As Object, e As SqlDataSourceStatusEventArgs) Handles ds_pln1.Deleted
        ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Data Deleted');</script>")
        ds_pln1.SelectCommand = sqlkeep1.Text
        grid_plan1.DataBind()
    End Sub

    Protected Sub Label3_Load(sender As Object, e As EventArgs) Handles Label3.Load
        If ddl_wpl1.SelectedItem IsNot Nothing Then
            Dim wpl As String = ddl_wpl1.SelectedItem.Text.ToString()
            Label3.Text = String.Concat("You are about to upload <br />Projection Data for the selected Weekly Plan <br />above : ", wpl)
        Else
            Label3.Text = String.Concat("You are about to upload Projection <br />Data for the selected Weekly Plan above")
        End If
    End Sub


    Protected Sub grid_opor1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grid_opor1.RowCommand
        'If e.CommandName = "Select" Then
        '    editpor1.Visible = True

        '    ds_por2.SelectParameters.Clear()
        '    ds_por2.SelectParameters.Add("cql", dbType:=DbType.Int64, value:=Int64.Parse(e.CommandArgument.ToString()))
        '    dtv_por1.DataBind()
        'End If

    End Sub

    Protected Sub ds_por1_Deleted(sender As Object, e As SqlDataSourceStatusEventArgs) Handles ds_por1.Deleted
        WeightBridge.csclass.mymsg("Data Deleted", "#")
        grid_opor1.DataBind()
    End Sub

    Protected Sub grid_opor1_DataBound(sender As Object, e As EventArgs) Handles grid_opor1.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_opor1, 2, 2)
        WeightBridge.csclass.mergeOnDatabound(grid_opor1, 5, 6)
        WeightBridge.csclass.mergeOnDatabound(grid_opor1, 6, 6)
    End Sub

    Protected Sub chb_edit_por1_CheckedChanged(sender As Object, e As EventArgs) Handles chb_edit_por1.CheckedChanged
        If chb_edit_por1.Checked = True Then
            grid_opor1.Columns(5).Visible = False
            grid_opor1.Columns(6).Visible = False
            grid_opor1.Columns(7).Visible = True
            grid_opor1.Columns(8).Visible = True
        Else
            grid_opor1.Columns(5).Visible = True
            grid_opor1.Columns(6).Visible = True
            grid_opor1.Columns(7).Visible = False
            grid_opor1.Columns(8).Visible = False
        End If


    End Sub

    Protected Sub ibt_update_por1_Click(sender As Object, e As ImageClickEventArgs)

        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn As New SqlConnection(connect)
            Dim cmd As New SqlCommand()
            cmd.Connection = conn
            Dim i As Integer = 0

            For Each row As GridViewRow In grid_opor1.Rows
                Dim ddl1 As DropDownList = DirectCast(row.FindControl("DropDownList7"), DropDownList)
                Dim lbl_idpor As Label = DirectCast(row.FindControl("lbl_idpor1"), Label)
                Dim lbl_porlama As Label = DirectCast(row.FindControl("lbl_porlama1"), Label)


                If lbl_porlama.Text.ToString().Trim().ToLower() <> ddl1.SelectedValue.ToString().Trim().ToLower() Then
                    cmd.CommandText = "UPDATE dbo.tbl_qualportal SET productKode = @prod WHERE id_qualportal = @idpor"
                    cmd.Parameters.AddWithValue("@idpor", Integer.Parse(lbl_idpor.Text))
                    cmd.Parameters.AddWithValue("@prod", Integer.Parse(ddl1.SelectedValue.ToString()))
                    cmd.CommandType = CommandType.Text
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    cmd.Parameters.Clear()

                    i += 1
                End If
            Next
            conn = Nothing
            SqlConnection.ClearAllPools()

            If i > 0 Then
                'ann1(DateTime.Today.ToString("MM/dd/yyyy"))
                WeightBridge.csclass.mymsg("Data updated", "Default2?v=0")
                grid_opor1.DataBind()
            End If
        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#")
        End Try

    End Sub

    Protected Sub cbSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim objChkAll As CheckBox = DirectCast(grid_opor1.HeaderRow.FindControl("CbSelectAll"), CheckBox)
        If objChkAll.Checked Then
            For Each objGVR As GridViewRow In grid_opor1.Rows
                Dim objChkIndividual As CheckBox = DirectCast(objGVR.FindControl("CbRows"), CheckBox)
                objChkIndividual.Checked = True
            Next
        Else
            For Each objGVR As GridViewRow In grid_opor1.Rows
                Dim objChkIndividual As CheckBox = DirectCast(objGVR.FindControl("CbRows"), CheckBox)
                objChkIndividual.Checked = False
            Next
        End If
    End Sub

    Protected Sub ibt_delete_por1_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_delete_por1.Click
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn As New SqlConnection(connect)
            Dim cmd As New SqlCommand()
            cmd.Connection = conn
            Dim i As Integer = 0

            For Each row As GridViewRow In grid_opor1.Rows
                Dim lbl_idpor As Label = DirectCast(row.FindControl("lbl_idpor1"), Label)
                Dim cbrows As CheckBox = DirectCast(row.FindControl("cbRows"), CheckBox)

                If cbrows.Checked = True Then
                    cmd.CommandText = "DELETE tbl_qualportal WHERE id_qualportal = @idpor"
                    cmd.Parameters.AddWithValue("@idpor", Integer.Parse(lbl_idpor.Text))
                    cmd.CommandType = CommandType.Text
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    cmd.Parameters.Clear()

                    i += 1
                End If
            Next
            conn = Nothing
            SqlConnection.ClearAllPools()

            If i > 0 Then
                'ann1(DateTime.Today.ToString("MM/dd/yyyy"))
                WeightBridge.csclass.mymsg("Data Deleted", "Default2?v=0")
                grid_opor1.DataBind()
            End If
        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#")
        End Try
    End Sub

    Protected Sub grid_plan1_DataBound(sender As Object, e As EventArgs) Handles grid_plan1.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_plan1, 3, 3)
    End Sub

    Protected Sub ibt_send_wpr1_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_send_wpr1.Click, ibt_update_por1.Click
        Try
            Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
            Dim conn As New SqlConnection(connect)
            Dim cmd As New SqlCommand()
            cmd.Connection = conn
            Dim i As Integer = 0

            For Each row As GridViewRow In grid_plan1.Rows
                Dim lbl_idwpr As Label = DirectCast(row.FindControl("lbl_idwpr1"), Label)
                Dim cbrows As CheckBox = DirectCast(row.FindControl("cbWpr1"), CheckBox)

                If cbrows.Checked = True Then
                    cmd.CommandText = "_sp_ql_sendQualToAlamo"
                    cmd.Parameters.AddWithValue("@idwp", Integer.Parse(lbl_idwpr.Text))
                    cmd.CommandType = CommandType.StoredProcedure
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()
                    cmd.Parameters.Clear()

                    i += 1
                End If

                ' WeightBridge.csclass.mymsg("Quality data sent to Alamo " & lbl_idwpr.Text, "Default2?v=1")
            Next
            conn = Nothing
            SqlConnection.ClearAllPools()

            If i > 0 Then
                'ann1(DateTime.Today.ToString("MM/dd/yyyy"))
                WeightBridge.csclass.mymsg("Quality data sent to Alamo", "Default2?v=1")
                grid_plan1.DataBind()
            End If
        Catch ex As Exception
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#")
        End Try
    End Sub

    Protected Sub cbSelAllWpr1_CheckedChanged(sender As Object, e As EventArgs)
        Dim objChkAll As CheckBox = DirectCast(grid_plan1.HeaderRow.FindControl("cbSelAllWpr1"), CheckBox)
        If objChkAll.Checked Then
            For Each objGVR As GridViewRow In grid_plan1.Rows
                Dim objChkIndividual As CheckBox = DirectCast(objGVR.FindControl("CbWpr1"), CheckBox)
                objChkIndividual.Checked = True
            Next
        Else
            For Each objGVR As GridViewRow In grid_plan1.Rows
                Dim objChkIndividual As CheckBox = DirectCast(objGVR.FindControl("CbWpr1"), CheckBox)
                objChkIndividual.Checked = False
            Next
        End If
    End Sub

    Protected Sub ibt_del_wpr1_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_del_wpr1.Click
        ' Try
        Dim connect As String = ConfigurationManager.ConnectionStrings("HTSdbConn").ToString()
        Dim conn As New SqlConnection(connect)
        Dim cmd As New SqlCommand()
        cmd.Connection = conn
        Dim i As Integer = 0

        For Each row As GridViewRow In grid_plan1.Rows
            Dim lbl_idwpr As Label = DirectCast(row.FindControl("lbl_idwpr1"), Label)
            Dim cbrows As CheckBox = DirectCast(row.FindControl("cbWpr1"), CheckBox)

            If cbrows.Checked = True Then
                cmd.CommandText = "DELETE dbo.tbl_qualwekplan WHERE id_qualwekplan = @idwp1"
                cmd.Parameters.AddWithValue("@idwp1", Integer.Parse(lbl_idwpr.Text))
                cmd.CommandType = CommandType.Text
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                cmd.Parameters.Clear()

                i += 1
            End If

            ' WeightBridge.csclass.mymsg("Quality data sent to Alamo " & lbl_idwpr.Text, "Default2?v=1")
        Next
        conn = Nothing
        SqlConnection.ClearAllPools()

        If i > 0 Then
            'ann1(DateTime.Today.ToString("MM/dd/yyyy"))
            WeightBridge.csclass.mymsg("Data deleted", "Default2?v=1")
            grid_plan1.DataBind()
        End If
        'Catch ex As Exception
        '    WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#")
        'End Try
    End Sub
   
End Class
