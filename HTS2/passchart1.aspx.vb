Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing
Imports System.Data.SqlClient

Partial Class HTS2_passchart1
    Inherits System.Web.UI.Page

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '           server control at run time. 
    End Sub

    Protected Sub GetLastSent(ByVal grd As GridView, ByVal type As String)
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
                                grd.Caption = String.Concat("Supply Passing Plan, " & shf & " Shift<br /> Last broadcasted at: ", _dtm.ToString("dd MMM yy, HH:mm"))
                            End While
                        End If
                        dr.Close()
                    End Using
                End Using
                conn.Close()
            End Using
            connect = Nothing
        Catch
            grd.Caption = String.Concat("Supply Passing Plan, " & shf & " Shift.")
        End Try

    End Sub

    Protected Sub DataRefresh()
        Dim dtm As String = Request.QueryString("dtm")
        Select Case TabContainer1.ActiveTabIndex
            Case 0
                ds_sp_planpass_rep1.SelectParameters.Clear()
                ds_sp_planpass_rep1.SelectParameters.Add("dtm", dtm)
                ds_sp_planpass_rep1.SelectParameters.Add("shf", Convert.ToString(1))
                grid_plan1.DataBind()

                ds_sp_planpass_rep2.SelectParameters.Clear()
                ds_sp_planpass_rep2.SelectParameters.Add("dtm", dtm)
                ds_sp_planpass_rep2.SelectParameters.Add("shf", Convert.ToString(2))
                grid_plan2.DataBind()

            Case 1
                lbl_dtm1.Text = Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy")
                SqlDS_truckpass.SelectParameters.Clear()
                SqlDS_truckpass.SelectParameters.Add("dtm", dtm)
                grid_pass1.DataBind()

            Case 2
                Dim jam As Integer = DateTime.Now.Hour
                Dim shft As Integer = 1
                Dim tglKmaren As Integer = 0
                Dim tglToday As Integer = DateTime.Today.Day

                If (jam >= 0 And jam <= 5) Then
                    shft = 2
                    tglKmaren = tglToday - 1

                ElseIf jam >= 6 And jam <= 18 Then
                    shft = 1

                ElseIf (jam >= 18) Then
                    shft = 2
                End If
                If shft = 2 Then
                    lbl_shf1.Text = "NIGHT SHIFT"
                Else
                    lbl_shf1.Text = "DAY SHIFT"
                End If
                lbl_dtm2.Text = Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy")
                lbl_dtm3.Text = Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy")
                lbl_dtm4.Text = Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy")

                ds_t11.SelectParameters.Clear()
                ds_t11.SelectParameters.Add("dtm", dtm)
                ds_t11.SelectParameters.Add("shf", Convert.ToString(1))
                grid_rit1.DataBind()

                ds_t12.SelectParameters.Clear()
                ds_t12.SelectParameters.Add("dtm", dtm)
                ds_t12.SelectParameters.Add("shf", Convert.ToString(2))
                grid_rit2.DataBind()

            Case 3
                ds_t21.SelectParameters.Clear()
                ds_t21.SelectParameters.Add("dtm", dtm)
                ds_t21.SelectParameters.Add("kon", "PAMA")
                grid_mat1.DataBind()

                ds_t22.SelectParameters.Clear()
                ds_t22.SelectParameters.Add("dtm", dtm)
                ds_t22.SelectParameters.Add("kon", "SIS")
                grid_mat2.DataBind()


                ds_t23.SelectParameters.Clear()
                ds_t23.SelectParameters.Add("dtm", dtm)
                ds_t23.SelectParameters.Add("kon", "RA")
                grid_mat3.DataBind()

                ds_t24.SelectParameters.Clear()
                ds_t24.SelectParameters.Add("dtm", dtm)
                ds_t24.SelectParameters.Add("kon", "RMI")
                grid_mat4.DataBind()

            Case 4
                lbl_dtm5.Text = Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy")

                ds_c11.SelectParameters.Clear()
                ds_c11.SelectParameters.Add("dtm", dtm)
                ds_c11.SelectParameters.Add("shf", "1")
                grid_c11.DataBind()


                ds_c13.SelectParameters.Clear()
                ds_c13.SelectParameters.Add("dtm", dtm)
                ds_c13.SelectParameters.Add("shf", "1")
                grid_c12.DataBind()

                ds_c14.SelectParameters.Clear()
                ds_c14.SelectParameters.Add("dtm", dtm)
                ds_c14.SelectParameters.Add("shf", "1")
                char_c11.DataBind()



        End Select
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim v As String = Request.QueryString("v")

        Select Case v
            Case "0"
                '---------------- ENHANCING CHART LEGEND ---------------------------
                '' Add header separator of type line      
                'char_c11.Legends("Default").HeaderSeparator = LegendSeparatorStyle.Line
                'char_c11.Legends("Default").HeaderSeparatorColor = Color.Gray

                '' Add Color column      
                'Dim firstColumn As New LegendCellColumn()
                'firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol
                'firstColumn.HeaderText = ""
                'firstColumn.HeaderBackColor = Color.Transparent
                'char_c11.Legends("Default").CellColumns.Add(firstColumn)

                '' Add Legend Text column      
                'Dim secondColumn As New LegendCellColumn()
                'secondColumn.ColumnType = LegendCellColumnType.Text
                'secondColumn.HeaderText = ""
                'secondColumn.Text = "#LEGENDTEXT"
                'secondColumn.HeaderBackColor = Color.Transparent
                'char_c11.Legends("Default").CellColumns.Add(secondColumn)

                '' Add New cell column      
                'Dim col67 As New LegendCellColumn()
                'col67.ColumnType = LegendCellColumnType.Text
                'col67.Text = "#VALY"
                'col67.HeaderText = "06.00-07.00"
                'col67.Name = "dt"
                'col67.HeaderBackColor = Color.Transparent
                'col67.HeaderForeColor = Color.White
                'char_c11.Legends("Default").CellColumns.Add(col67)

                '' Add New cell column      
                'Dim col78 As New LegendCellColumn()
                'col78.ColumnType = LegendCellColumnType.Text
                'col78.Text = "#VALY"
                'col78.HeaderText = "07.00-08.00"
                'col78.Name = "E4000_t"
                'col78.HeaderBackColor = Color.Transparent
                'col78.HeaderForeColor = Color.White
                'char_c11.Legends("Default").CellColumns.Add(col78)

                '' Add AVG cell column      
                'Dim avgColumn As New LegendCellColumn()
                'avgColumn.Text = "#AVG{N2}"
                'avgColumn.HeaderText = "Avg"
                'avgColumn.Name = "AvgColumn"
                'avgColumn.HeaderBackColor = Color.WhiteSmoke
                'char_c11.Legends("Default").CellColumns.Add(avgColumn)

                '' Add Total cell column      
                'Dim totalColumn As New LegendCellColumn()
                'totalColumn.Text = "#TOTAL{N1}"
                'totalColumn.HeaderText = "Total"
                'totalColumn.Name = "TotalColumn"
                'totalColumn.HeaderBackColor = Color.WhiteSmoke
                'char_c11.Legends("Default").CellColumns.Add(totalColumn)

                '' Set Min cell column attributes      
                'Dim minColumn As New LegendCellColumn()
                'minColumn.Text = "#MIN{N1}"
                'minColumn.HeaderText = "Min"
                'minColumn.Name = "MinColumn"
                'minColumn.HeaderBackColor = Color.WhiteSmoke
                'char_c11.Legends("Default").CellColumns.Add(minColumn)

                TabContainer1.ActiveTabIndex = 0
                Dim dtm As String = Request.QueryString("dtm")

               

                'SqlDS_truckpass.SelectParameters.Clear()
                'SqlDS_truckpass.SelectParameters.Add("dtm", Data.DbType.String, dtm)
                'grid_pass1.DataBind()

                'ds_passed29.SelectParameters.Clear()
                'ds_passed29.SelectParameters.Add("dtm", Data.DbType.String, dtm)
                'grid_pass291.DataBind()


              

                lbl_dtm6.Text = Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy")

                lbl_title1.Text = String.Concat("Passing Report for ", Convert.ToDateTime(dtm).ToString("dddd, MMMM dd yyyy"), "<br />(web version)")

              

                GetLastSent(grid_plan1, "48")
                GetLastSent(grid_plan2, "49")


            Case "1"
                WeightBridge.ExportFile.xlsport(grid_miss1, "MissedByKm67_", Me)

            Case "2"
                WeightBridge.ExportFile.xlsport(grid_pass291, "TrucksHeadingToKelanis_", Me)

            Case "3"
                WeightBridge.ExportFile.xlsport(grid_c21, "Passing_Shf2_", Me)

            Case "4"
                WeightBridge.ExportFile.xlsport(grid_c22, "Recap_Shf2_", Me)

            Case "5"
                WeightBridge.ExportFile.xlsport(grid_c11, "Passing_Shf1_", Me)

            Case "6"
                WeightBridge.ExportFile.xlsport(grid_c12, "Recap_Shf1_", Me)

            Case "7"
                WeightBridge.ExportFile.xlsport(grid_mat1, "Hourly_Material_Pama_", Me)

            Case "8"
                WeightBridge.ExportFile.xlsport(grid_mat2, "Hourly_Material_Sis_", Me)

            Case "9"
                WeightBridge.ExportFile.xlsport(grid_mat3, "Hourly_Material_Ra_", Me)

            Case "10"
                WeightBridge.ExportFile.xlsport(grid_mat4, "Hourly_Material_Rmi_", Me)

            Case "11"
                WeightBridge.ExportFile.xlsport(grid_rit1, "Hourly_Ritase1_", Me)

            Case "12"
                WeightBridge.ExportFile.xlsport(grid_rit2, "Hourly_Ritase2_", Me)

            Case "13"
                WeightBridge.ExportFile.xlsport(grid_plan1, "Passing_Plan_Shif1_", Me)

            Case "14"
                WeightBridge.ExportFile.xlsport(grid_plan2, "Passing_Plan_Shif2_", Me)
        End Select
    End Sub

    Protected Sub grid_rit1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_rit1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim c1 As New TableCell()
            'cell.ColumnSpan = cspan
            'cell.HorizontalAlign = HorizontalAlign.Center
            'cell.VerticalAlign = VerticalAlign.Middle
            'cell.Height = 38
            'cell.Text = htx
            c1.Text = "TIME"
            c1.VerticalAlign = VerticalAlign.Bottom
            row.Cells.Add(c1)

            Dim c2 As New TableCell()
            c2.ColumnSpan = 3
            c2.HorizontalAlign = HorizontalAlign.Center
            c2.Text = "PAMA"
            row.Cells.Add(c2)

            Dim c3 As New TableCell()
            c3.ColumnSpan = 3
            c3.HorizontalAlign = HorizontalAlign.Center
            c3.Text = "SIS"
            row.Cells.Add(c3)

            Dim c4 As New TableCell()
            c4.ColumnSpan = 3
            c4.HorizontalAlign = HorizontalAlign.Center
            c4.Text = "RA"
            row.Cells.Add(c4)

            Dim c5 As New TableCell()
            c5.ColumnSpan = 2
            c5.HorizontalAlign = HorizontalAlign.Center
            c5.Text = "TOTAL TRIP PERJAM"
            row.Cells.Add(c5)

            Dim c6 As New TableCell()
            c6.ColumnSpan = 3
            c6.HorizontalAlign = HorizontalAlign.Center
            c6.Text = "TOTAL SUPPLY RTK PERJAM"
            row.Cells.Add(c6)

            grid_rit1.Controls(0).Controls.AddAt(0, row)
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then

            If checkPlanAct(DataBinder.Eval(e.Row.DataItem, "truckcount").ToString()) > 0 And _
                checkPlanAct(DataBinder.Eval(e.Row.DataItem, "totrip").ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, "truckcount").ToString()) Then
                e.Row.Cells(10).CssClass = "over"
            ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, "totrip").ToString()) > 0 And _
                checkPlanAct(DataBinder.Eval(e.Row.DataItem, "totrip").ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, "truckcount").ToString()) Then
                e.Row.Cells(10).CssClass = "less"
            End If

        End If
    End Sub

    Protected Sub grid_rit2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_rit2.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim c1 As New TableCell()
            c1.Text = "TIME"
            c1.VerticalAlign = VerticalAlign.Bottom
            row.Cells.Add(c1)

            Dim c2 As New TableCell()
            c2.ColumnSpan = 3
            c2.HorizontalAlign = HorizontalAlign.Center
            c2.Text = "PAMA"
            row.Cells.Add(c2)

            Dim c3 As New TableCell()
            c3.ColumnSpan = 3
            c3.HorizontalAlign = HorizontalAlign.Center
            c3.Text = "SIS"
            row.Cells.Add(c3)

            Dim c4 As New TableCell()
            c4.ColumnSpan = 3
            c4.HorizontalAlign = HorizontalAlign.Center
            c4.Text = "RA"
            row.Cells.Add(c4)

            Dim c5 As New TableCell()
            c5.ColumnSpan = 2
            c5.HorizontalAlign = HorizontalAlign.Center
            c5.Text = "TOTAL TRIP PERJAM"
            row.Cells.Add(c5)

            Dim c6 As New TableCell()
            c6.ColumnSpan = 3
            c6.HorizontalAlign = HorizontalAlign.Center
            c6.Text = "TOTAL SUPPLY RTK PERJAM"
            row.Cells.Add(c6)

            grid_rit2.Controls(0).Controls.AddAt(0, row)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            If checkPlanAct(DataBinder.Eval(e.Row.DataItem, "truckcount").ToString()) > 0 And _
                checkPlanAct(DataBinder.Eval(e.Row.DataItem, "totrip").ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, "truckcount").ToString()) Then
                e.Row.Cells(10).CssClass = "over"
            ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, "totrip").ToString()) > 0 And _
                checkPlanAct(DataBinder.Eval(e.Row.DataItem, "totrip").ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, "truckcount").ToString()) Then
                e.Row.Cells(10).CssClass = "less"
            End If

        End If

    End Sub

    Protected Sub grid_mat1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_mat1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            AddHeaderMaterial(grid_mat1)
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "ProductKode").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = Color.FromArgb(153, 204, 0)
                Case "E4900"
                    e.Row.Cells(0).BackColor = Color.FromArgb(255, 204, 0)
                Case "E4000"
                    e.Row.Cells(0).BackColor = Color.Gray
                    e.Row.Cells(0).ForeColor = Color.AliceBlue
            End Select

            For i As Integer = 1 To 52
                If e.Row.Cells(i).Text = "0" Then
                    e.Row.Cells(i).ForeColor = Color.Transparent
                End If
            Next

            Dim z As Integer = 1
            For x As Integer = 6 To 23
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "less"
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "over"
                End If

                z = z + 2
            Next

            z = 37
            For x As Integer = 0 To 5
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "less"
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "over"
                End If

                z = z + 2
            Next

            z = 49
            For x As Integer = 1 To 2
                Dim Ap As String : Dim Pp As String

                Ap = "shf_" & x
                Pp = "plan_totalshf" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                        checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "less"
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "over"
                End If
                z = z + 2
            Next
        End If
    End Sub

    Protected Function checkPlanAct(ByVal itm As String) As Integer
        If String.IsNullOrEmpty(itm) Or itm.Trim() = "" _
                        Or IsNumeric(itm) = False Then
            Return 0
        Else
            Return Int16.Parse(itm)
        End If

    End Function

    Protected Sub grid_mat2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_mat2.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            AddHeaderMaterial(grid_mat2)
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "ProductKode").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = Color.FromArgb(153, 204, 0)
                Case "E4900"
                    e.Row.Cells(0).BackColor = Color.FromArgb(255, 204, 0)
                Case "E4000"
                    e.Row.Cells(0).BackColor = Color.Gray
                    e.Row.Cells(0).ForeColor = Color.AliceBlue
            End Select
          
            For i As Integer = 1 To 52
                If e.Row.Cells(i).Text = "0" Then
                    e.Row.Cells(i).ForeColor = Color.Transparent
                End If
            Next

            Dim z As Integer = 1
            For x As Integer = 6 To 23
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.FromArgb(255, 199, 206)
                    e.Row.Cells(z).ForeColor = Color.FromArgb(156, 0, 6)
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.LightGreen
                End If

                z = z + 2
            Next

            z = 37
            For x As Integer = 0 To 5
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.FromArgb(255, 199, 206)
                    e.Row.Cells(z).ForeColor = Color.FromArgb(156, 0, 6)
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.LightGreen
                End If

                z = z + 2
            Next

            z = 49
            For x As Integer = 1 To 2
                Dim Ap As String : Dim Pp As String

                Ap = "shf_" & x
                Pp = "plan_totalshf" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                        checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "less"
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "over"
                End If
                z = z + 2
            Next

        End If
    End Sub

    Protected Sub grid_mat3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_mat3.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            AddHeaderMaterial(grid_mat3)
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "ProductKode").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = Color.FromArgb(153, 204, 0)
                Case "E4900"
                    e.Row.Cells(0).BackColor = Color.FromArgb(255, 204, 0)
                Case "E4000"
                    e.Row.Cells(0).BackColor = Color.Gray
                    e.Row.Cells(0).ForeColor = Color.AliceBlue
            End Select

            For i As Integer = 1 To 52
                If e.Row.Cells(i).Text = "0" Then
                    e.Row.Cells(i).ForeColor = Color.Transparent
                End If
            Next

            Dim z As Integer = 1
            For x As Integer = 6 To 23
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.FromArgb(255, 199, 206)
                    e.Row.Cells(z).ForeColor = Color.FromArgb(156, 0, 6)
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.LightGreen
                End If

                z = z + 2
            Next

            z = 37
            For x As Integer = 0 To 5
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.FromArgb(255, 199, 206)
                    e.Row.Cells(z).ForeColor = Color.FromArgb(156, 0, 6)
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.LightGreen
                End If

                z = z + 2
            Next

            z = 49
            For x As Integer = 1 To 2
                Dim Ap As String : Dim Pp As String

                Ap = "shf_" & x
                Pp = "plan_totalshf" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                        checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "less"
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "over"
                End If
                z = z + 2
            Next
        End If
    End Sub

    Protected Sub grid_mat4_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_mat4.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            AddHeaderMaterial(grid_mat4)
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "ProductKode").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = Color.FromArgb(153, 204, 0)
                Case "E4900"
                    e.Row.Cells(0).BackColor = Color.FromArgb(255, 204, 0)
                Case "E4000"
                    e.Row.Cells(0).BackColor = Color.Gray
                    e.Row.Cells(0).ForeColor = Color.AliceBlue
            End Select

            For i As Integer = 1 To 52
                If e.Row.Cells(i).Text = "0" Then
                    e.Row.Cells(i).ForeColor = Color.Transparent
                End If
            Next

            Dim z As Integer = 1
            For x As Integer = 6 To 23
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.FromArgb(255, 199, 206)
                    e.Row.Cells(z).ForeColor = Color.FromArgb(156, 0, 6)
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.LightGreen
                End If

                z = z + 2
            Next

            z = 37
            For x As Integer = 0 To 5
                Dim Ap As String : Dim Pp As String

                Ap = "A" & x
                Pp = "P" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.FromArgb(255, 199, 206)
                    e.Row.Cells(z).ForeColor = Color.FromArgb(156, 0, 6)
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).BackColor = Color.LightGreen
                End If

                z = z + 2
            Next

            z = 49
            For x As Integer = 1 To 2
                Dim Ap As String : Dim Pp As String

                Ap = "shf_" & x
                Pp = "plan_totalshf" & x

                If checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > 0 And _
                        checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) < checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "less"
                ElseIf checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) > 0 And _
                    checkPlanAct(DataBinder.Eval(e.Row.DataItem, Ap).ToString()) > checkPlanAct(DataBinder.Eval(e.Row.DataItem, Pp).ToString()) Then
                    e.Row.Cells(z).CssClass = "over"
                End If
                z = z + 2
            Next
        End If
    End Sub

    Protected Sub AddHeaderMaterial(ByVal grd As GridView)
        Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

        Dim c2 As New TableCell()
        c2.BorderStyle = BorderStyle.None
        row.Cells.Add(c2)

        Dim c3 As New TableCell()
        c3.ColumnSpan = 2
        c3.Text = "06.00 - <br />07.00"
        row.Cells.Add(c3)

        Dim c4 As New TableCell()
        c4.ColumnSpan = 2
        c4.Text = "07.00 - <br />08.00"
        row.Cells.Add(c4)

        Dim c5 As New TableCell()
        c5.ColumnSpan = 2
        c5.Text = "08.00 - <br />09.00"
        row.Cells.Add(c5)

        Dim c6 As New TableCell()
        c6.ColumnSpan = 2
        c6.Text = "09.00 - <br />10.00"
        row.Cells.Add(c6)

        Dim c7 As New TableCell()
        c7.ColumnSpan = 2
        c7.Text = "10.00 - <br />11.00"
        row.Cells.Add(c7)

        Dim c7a As New TableCell()
        c7a.ColumnSpan = 2
        c7a.Text = "11.00 - <br />12.00"
        row.Cells.Add(c7a)

        Dim c8 As New TableCell()
        c8.ColumnSpan = 2
        c8.Text = "12.00 - <br />13.00"
        row.Cells.Add(c8)

        Dim c9 As New TableCell()
        c9.ColumnSpan = 2
        c9.Text = "13.00 - <br />14.00"
        row.Cells.Add(c9)

        Dim c10 As New TableCell()
        c10.ColumnSpan = 2
        c10.Text = "14.00 - <br />15.00"
        row.Cells.Add(c10)

        Dim c11 As New TableCell()
        c11.ColumnSpan = 2
        c11.Text = "15.00 - <br />16.00"
        row.Cells.Add(c11)

        Dim c12 As New TableCell()
        c12.ColumnSpan = 2
        c12.Text = "16.00 - <br />17.00"
        row.Cells.Add(c12)

        Dim c13 As New TableCell()
        c13.ColumnSpan = 2
        c13.Text = "17.00 - <br />18.00"
        row.Cells.Add(c13)

        Dim c14 As New TableCell()
        c14.ColumnSpan = 2
        c14.Text = "18.00 - <br />19.00"
        row.Cells.Add(c14)

        Dim c15 As New TableCell()
        c15.ColumnSpan = 2
        c15.Text = "19.00 - <br />20.00"
        row.Cells.Add(c15)

        Dim c16 As New TableCell()
        c16.ColumnSpan = 2
        c16.Text = "20.00 - <br />21.00"
        row.Cells.Add(c16)

        Dim c17 As New TableCell()
        c17.ColumnSpan = 2
        c17.Text = "21.00 - <br />22.00"
        row.Cells.Add(c17)

        Dim c18 As New TableCell()
        c18.ColumnSpan = 2
        c18.Text = "22.00 - <br />23.00"
        row.Cells.Add(c18)

        Dim c19 As New TableCell()
        c19.ColumnSpan = 2
        c19.Text = "23.00 - <br />00.00"
        row.Cells.Add(c19)

        Dim c20 As New TableCell()
        c20.ColumnSpan = 2
        c20.Text = "00.00 - <br />01.00"
        row.Cells.Add(c20)

        Dim c21 As New TableCell()
        c21.ColumnSpan = 2
        c21.Text = "01.00 - <br />02.00"
        row.Cells.Add(c21)

        Dim c22 As New TableCell()
        c22.ColumnSpan = 2
        c22.Text = "02.00 - <br />03.00"
        row.Cells.Add(c22)

        Dim c23 As New TableCell()
        c23.ColumnSpan = 2
        c23.Text = "03.00 - <br />04.00"
        row.Cells.Add(c23)

        Dim c24 As New TableCell()
        c24.ColumnSpan = 2
        c24.Text = "04.00 - <br />05.00"
        row.Cells.Add(c24)

        Dim c25 As New TableCell()
        c25.ColumnSpan = 2
        c25.Text = "05.00 - <br />06.00"
        row.Cells.Add(c25)

        Dim c26 As New TableCell()
        c26.ColumnSpan = 2
        c26.Text = "Total <br />Shift 1"
        row.Cells.Add(c26)

        Dim c27 As New TableCell()
        c27.ColumnSpan = 2
        c27.Text = "Total <br />Shift 2"
        row.Cells.Add(c27)


        grd.Controls(0).Controls.AddAt(0, row)
    End Sub


    Protected Sub grid_c11_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_c11.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim c1 As New TableCell()
            c1.Text = "TIME"
            c1.VerticalAlign = VerticalAlign.Bottom
            row.Cells.Add(c1)

            Dim c2 As New TableCell()
            c2.Text = "TRIP"
            c2.VerticalAlign = VerticalAlign.Bottom
            row.Cells.Add(c2)

            Dim c3 As New TableCell()
            c3.ColumnSpan = 4
            c3.HorizontalAlign = HorizontalAlign.Center
            c3.Text = "Ritase ROM to kelanis"
            row.Cells.Add(c3)

            grid_c11.Controls(0).Controls.AddAt(0, row)
        End If
    End Sub

    Protected Sub grid_c21_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_c21.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim c1 As New TableCell()
            c1.Text = "TIME"
            c1.VerticalAlign = VerticalAlign.Bottom
            row.Cells.Add(c1)

            Dim c2 As New TableCell()
            c2.Text = "TRIP"
            c2.VerticalAlign = VerticalAlign.Bottom
            row.Cells.Add(c2)

            Dim c3 As New TableCell()
            c3.ColumnSpan = 4
            c3.HorizontalAlign = HorizontalAlign.Center
            c3.Text = "Ritase ROM to kelanis"
            row.Cells.Add(c3)

            grid_c21.Controls(0).Controls.AddAt(0, row)
        End If
    End Sub

    Protected Sub grid_c12_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_c12.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).ColumnSpan = 2
            e.Row.Cells(1).Visible = False
        End If
    End Sub

    Protected Sub grid_c22_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_c22.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).ColumnSpan = 2
            e.Row.Cells(1).Visible = False
        End If
    End Sub

    Protected Sub grid_c11a_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_c11a.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "ProductKode").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = Color.FromArgb(153, 204, 0)
                Case "E4900"
                    e.Row.Cells(0).BackColor = Color.FromArgb(255, 204, 0)
                Case "E4000"
                    e.Row.Cells(0).BackColor = Color.Gray
                    e.Row.Cells(1).ForeColor = Color.AliceBlue
            End Select
        End If
    End Sub

    Protected Sub grid_c23_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_c23.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "ProductKode").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = Color.FromArgb(153, 204, 0)
                Case "E4900"
                    e.Row.Cells(0).BackColor = Color.FromArgb(255, 204, 0)
                Case "E4000"
                    e.Row.Cells(0).BackColor = Color.Gray
                    e.Row.Cells(1).ForeColor = Color.AliceBlue
            End Select
        End If
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



    Protected Sub grid_miss1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_miss1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            WeightBridge.csclass.addHead(6, "Missed by Km. 67 Report", grid_miss1)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "series_name").ToString()
                Case "E5000"
                    e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(4).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(4).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(4).ForeColor = System.Drawing.Color.AliceBlue
            End Select
        End If
    End Sub

    Protected Sub grid_pass291_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_pass291.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            WeightBridge.csclass.addHead(3, "Total Truck(s) <br/>Heading to Kelanis", grid_pass291)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "series_name").ToString()
                Case "E5000"
                    e.Row.Cells(0).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(0).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(0).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(0).ForeColor = System.Drawing.Color.AliceBlue
            End Select

            If DataBinder.Eval(e.Row.DataItem, "MaterialKode").ToString() = "TOTAL" Then
                e.Row.Cells.RemoveAt(1)
                e.Row.Cells(0).ColumnSpan = 2

                e.Row.BackColor = Color.Yellow
                e.Row.Font.Size = 9
                e.Row.Font.Bold = True
            End If
        End If
    End Sub


    Protected Sub grid_pass1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_pass1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            'WeightBridge.csclass.addHead(5, "PAMA", grid_pass1)

            Dim row As New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            row.CssClass = "head1"

            Dim cell0 As New TableCell()
            cell0.RowSpan = 2
            cell0.Text = "No."

            Dim cell1 As New TableCell()
            cell1.RowSpan = 2
            cell1.Text = "TIME"

            Dim cell2 As New TableCell()
            cell2.ColumnSpan = 3
            cell2.Text = "PAMA"

            Dim cell3 As New TableCell()
            cell3.RowSpan = 2
            cell3.Text = "TIME"

            Dim cell4 As New TableCell()
            cell4.ColumnSpan = 3
            cell4.Text = "SIS"

            Dim cell5 As New TableCell()
            cell5.RowSpan = 2
            cell5.Text = "TIME"

            Dim cell6 As New TableCell()
            cell6.ColumnSpan = 3
            cell6.Text = "RA"

            Dim cell7 As New TableCell()
            cell7.RowSpan = 2
            cell7.Text = "TIME"

            Dim cell8 As New TableCell()
            cell8.ColumnSpan = 3
            cell8.Text = "RMI"

            row.Cells.Add(cell0)
            row.Cells.Add(cell1)
            row.Cells.Add(cell2)
            row.Cells.Add(cell3)
            row.Cells.Add(cell4)
            row.Cells.Add(cell5)
            row.Cells.Add(cell6)
            row.Cells.Add(cell7)
            row.Cells.Add(cell8)
            grid_pass1.Controls(0).Controls.AddAt(0, row)

            Dim row2 As New GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Normal)
            row2.CssClass = "head1"

            Dim cell2_1 As New TableCell()
            cell2_1.Text = "TruckNo"
            Dim cell2_2 As New TableCell()
            cell2_2.Text = "Series"
            Dim cell2_3 As New TableCell()
            cell2_3.Text = "ROM"

            Dim cell4_1 As New TableCell()
            cell4_1.Text = "TruckNo"
            Dim cell4_2 As New TableCell()
            cell4_2.Text = "Series"
            Dim cell4_3 As New TableCell()
            cell4_3.Text = "ROM"

            Dim cell6_1 As New TableCell()
            cell6_1.Text = "TruckNo"
            Dim cell6_2 As New TableCell()
            cell6_2.Text = "Series"
            Dim cell6_3 As New TableCell()
            cell6_3.Text = "ROM"

            Dim cell8_1 As New TableCell()
            cell8_1.Text = "TruckNo"
            Dim cell8_2 As New TableCell()
            cell8_2.Text = "Series"
            Dim cell8_3 As New TableCell()
            cell8_3.Text = "ROM"


            row2.Cells.Add(cell2_1)
            row2.Cells.Add(cell2_2)
            row2.Cells.Add(cell2_3)

            row2.Cells.Add(cell4_1)
            row2.Cells.Add(cell4_2)
            row2.Cells.Add(cell4_3)

            row2.Cells.Add(cell6_1)
            row2.Cells.Add(cell6_2)
            row2.Cells.Add(cell6_3)

            row2.Cells.Add(cell8_1)
            row2.Cells.Add(cell8_2)
            row2.Cells.Add(cell8_3)

            grid_pass1.Controls(0).Controls.AddAt(1, row2)
        End If

        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            'WeightBridge.csclass.addHead(5, "PAMA", grid_pass1)

            Dim row As New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            row.CssClass = "head1"

            Dim cell0 As New TableCell()
            cell0.RowSpan = 2
            cell0.Text = "No."

            Dim cell1 As New TableCell()
            cell1.RowSpan = 2
            cell1.Text = "TIME"

            Dim cell2 As New TableCell()
            cell2.ColumnSpan = 3
            cell2.Text = "PAMA"

            Dim cell3 As New TableCell()
            cell3.RowSpan = 2
            cell3.Text = "TIME"

            Dim cell4 As New TableCell()
            cell4.ColumnSpan = 3
            cell4.Text = "SIS"

            Dim cell5 As New TableCell()
            cell5.RowSpan = 2
            cell5.Text = "TIME"

            Dim cell6 As New TableCell()
            cell6.ColumnSpan = 3
            cell6.Text = "RA"

            Dim cell7 As New TableCell()
            cell7.RowSpan = 2
            cell7.Text = "TIME"

            Dim cell8 As New TableCell()
            cell8.ColumnSpan = 3
            cell8.Text = "RMI"

            row.Cells.Add(cell0)
            row.Cells.Add(cell1)
            row.Cells.Add(cell2)
            row.Cells.Add(cell3)
            row.Cells.Add(cell4)
            row.Cells.Add(cell5)
            row.Cells.Add(cell6)
            row.Cells.Add(cell7)
            row.Cells.Add(cell8)
            grid_pass1.Controls(0).Controls.AddAt(0, row)

            Dim row2 As New GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Normal)
            row2.CssClass = "head1"

            Dim cell2_1 As New TableCell()
            cell2_1.Text = "TruckNo"
            Dim cell2_2 As New TableCell()
            cell2_2.Text = "Series"
            Dim cell2_3 As New TableCell()
            cell2_3.Text = "ROM"

            Dim cell4_1 As New TableCell()
            cell4_1.Text = "TruckNo"
            Dim cell4_2 As New TableCell()
            cell4_2.Text = "Series"
            Dim cell4_3 As New TableCell()
            cell4_3.Text = "ROM"

            Dim cell6_1 As New TableCell()
            cell6_1.Text = "TruckNo"
            Dim cell6_2 As New TableCell()
            cell6_2.Text = "Series"
            Dim cell6_3 As New TableCell()
            cell6_3.Text = "ROM"

            Dim cell8_1 As New TableCell()
            cell8_1.Text = "TruckNo"
            Dim cell8_2 As New TableCell()
            cell8_2.Text = "Series"
            Dim cell8_3 As New TableCell()
            cell8_3.Text = "ROM"


            row2.Cells.Add(cell2_1)
            row2.Cells.Add(cell2_2)
            row2.Cells.Add(cell2_3)

            row2.Cells.Add(cell4_1)
            row2.Cells.Add(cell4_2)
            row2.Cells.Add(cell4_3)

            row2.Cells.Add(cell6_1)
            row2.Cells.Add(cell6_2)
            row2.Cells.Add(cell6_3)

            row2.Cells.Add(cell8_1)
            row2.Cells.Add(cell8_2)
            row2.Cells.Add(cell8_3)

            grid_pass1.Controls(0).Controls.AddAt(1, row2)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Select Case DataBinder.Eval(e.Row.DataItem, "prod_pm").ToString()
                Case "E5000"
                    e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(3).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(3).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(3).ForeColor = System.Drawing.Color.AliceBlue
            End Select

            Select Case DataBinder.Eval(e.Row.DataItem, "prod_sis").ToString()
                Case "E5000"
                    e.Row.Cells(7).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(7).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(7).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(7).ForeColor = System.Drawing.Color.AliceBlue
            End Select

            Select Case DataBinder.Eval(e.Row.DataItem, "prod_ra").ToString()
                Case "E5000"
                    e.Row.Cells(11).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(11).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(11).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(11).ForeColor = System.Drawing.Color.AliceBlue
            End Select

            Select Case DataBinder.Eval(e.Row.DataItem, "prod_rm").ToString()
                Case "E5000"
                    e.Row.Cells(15).BackColor = System.Drawing.Color.FromArgb(153, 204, 0)

                Case "E4900"
                    e.Row.Cells(15).BackColor = System.Drawing.Color.FromArgb(255, 204, 0)

                Case "E4000"
                    e.Row.Cells(15).BackColor = System.Drawing.Color.Gray
                    e.Row.Cells(15).ForeColor = System.Drawing.Color.AliceBlue
            End Select

        End If
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

    Protected Sub ibt_xls1_Click(sender As Object, e As ImageClickEventArgs) Handles ibt_xls1.Click

        Dim act As Integer = TabContainer1.ActiveTabIndex
        Dim _dtm As String = Request.QueryString("dtm").ToString()

        'WeightBridge.csclass.mymsg("==>" & act & "", "#")

        Select Case act
            Case 0
                Dim script As String = "window.open('passchart1.aspx?v=13&dtm=" & _dtm & "');window.open('passchart1.aspx?v=14&dtm=" & _dtm & "');location.href('passchart1.aspx?v=0&dtm=" & _dtm & "');"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)

            Case 1
                WeightBridge.ExportFile.xlsport(grid_pass1, "RTK_Monitoring_", Me)
                ' Response.Redirect("passchart1.aspx?v=0&dtm=" & _dtm)

            Case 2
                Dim script As String = "window.open('passchart1.aspx?v=11&dtm=" & _dtm & "');window.open('passchart1.aspx?v=12&dtm=" & _dtm & "');location.href('passchart1.aspx?v=0&dtm=" & _dtm & "');"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)
            Case 3
                Dim script As String = "window.open('passchart1.aspx?v=7&dtm=" & _dtm & "');window.open('passchart1.aspx?v=8&dtm=" & _dtm & "');" & _
                    "window.open('passchart1.aspx?v=9&dtm=" & _dtm & "');window.open('passchart1.aspx?v=10&dtm=" & _dtm & "');location.href('passchart1.aspx?v=0&dtm=" & _dtm & "');"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)

            Case 4
                Dim script As String = "window.open('passchart1.aspx?v=5&dtm=" & _dtm & "');window.open('passchart1.aspx?v=6&dtm=" & _dtm & "');location.href('passchart1.aspx?v=0&dtm=" & _dtm & "');"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)

            Case 5
                Dim script As String = "window.open('passchart1.aspx?v=3&dtm=" & _dtm & "');window.open('passchart1.aspx?v=4&dtm=" & _dtm & "');location.href('passchart1.aspx?v=0&dtm=" & _dtm & "');"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)

            Case 6
                Dim script As String = "window.open('passchart1.aspx?v=1&dtm=" & _dtm & "');window.open('passchart1.aspx?v=2&dtm=" & _dtm & "');location.href('passchart1.aspx?v=0&dtm=" & _dtm & "');"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alertscript", script, True)

            Case 7
                WeightBridge.ExportFile.xlsport(grid_dev1, "Material_Deviation_", Me)
                '   Response.Redirect("passchart1.aspx?v=0&dtm=" & _dtm)
        End Select

    End Sub

    Protected Sub grid_dev1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid_dev1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            'WeightBridge.csclass.addHead(5, "PAMA", grid_pass1)

            Dim row As New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            row.CssClass = "head1"

            Dim cell0 As New TableCell()
            cell0.RowSpan = 2
            cell0.Text = "Kontraktor"

            Dim cell1 As New TableCell()
            cell1.RowSpan = 2
            cell1.Text = "TruckNo"

            Dim cell2 As New TableCell()
            cell2.ColumnSpan = 3
            cell2.Text = "Passing Km. 67"

            Dim cell3 As New TableCell()
            cell3.ColumnSpan = 3
            cell3.Text = "Weighbridge"


            row.Cells.Add(cell0)
            row.Cells.Add(cell1)
            row.Cells.Add(cell2)
            row.Cells.Add(cell3)
            grid_dev1.Controls(0).Controls.AddAt(0, row)

            Dim row1 As New GridViewRow(1, -1, DataControlRowType.Header, DataControlRowState.Normal)
            row1.CssClass = "head1"

            Dim cell01 As New TableCell()
            cell01.Text = "Jam"

            Dim cell11 As New TableCell()
            cell11.Text = "Material"

            Dim cell21 As New TableCell()
            cell21.Text = "Rom"

            Dim cell31 As New TableCell()
            cell31.Text = "Jam"

            Dim cell41 As New TableCell()
            cell41.Text = "Material"

            Dim cell51 As New TableCell()
            cell51.Text = "Rom"

            row1.Cells.Add(cell01)
            row1.Cells.Add(cell11)
            row1.Cells.Add(cell21)
            row1.Cells.Add(cell31)
            row1.Cells.Add(cell41)
            row1.Cells.Add(cell51)
            grid_dev1.Controls(0).Controls.AddAt(1, row1)
        ElseIf e.Row.RowType = DataControlRowType.EmptyDataRow Then
            'WeightBridge.csclass.addHead(5, "PAMA", grid_pass1)

            Dim row As New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            row.CssClass = "head1"

            Dim cell0 As New TableCell()
            cell0.RowSpan = 2
            cell0.Text = "Kontraktor"

            Dim cell1 As New TableCell()
            cell1.RowSpan = 2
            cell1.Text = "TruckNo"

            Dim cell2 As New TableCell()
            cell2.ColumnSpan = 3
            cell2.Text = "Passing Km. 67"

            Dim cell3 As New TableCell()
            cell3.ColumnSpan = 3
            cell3.Text = "Weighbridge"


            row.Cells.Add(cell0)
            row.Cells.Add(cell1)
            row.Cells.Add(cell2)
            row.Cells.Add(cell3)
            grid_dev1.Controls(0).Controls.AddAt(0, row)

            Dim row1 As New GridViewRow(1, -1, DataControlRowType.Header, DataControlRowState.Normal)
            row1.CssClass = "head1"

            Dim cell01 As New TableCell()
            cell01.Text = "Jam"

            Dim cell11 As New TableCell()
            cell11.Text = "Material"

            Dim cell21 As New TableCell()
            cell21.Text = "Rom"

            Dim cell31 As New TableCell()
            cell31.Text = "Jam"

            Dim cell41 As New TableCell()
            cell41.Text = "Material"

            Dim cell51 As New TableCell()
            cell51.Text = "Rom"

            row1.Cells.Add(cell01)
            row1.Cells.Add(cell11)
            row1.Cells.Add(cell21)
            row1.Cells.Add(cell31)
            row1.Cells.Add(cell41)
            row1.Cells.Add(cell51)
            grid_dev1.Controls(0).Controls.AddAt(1, row1)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Page.Header.DataBind()
    End Sub


    Protected Sub grid_pass1_DataBound(sender As Object, e As EventArgs) Handles grid_pass1.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_pass1, 1, 1)
        WeightBridge.csclass.mergeOnDatabound(grid_pass1, 5, 5)
        WeightBridge.csclass.mergeOnDatabound(grid_pass1, 9, 9)
        WeightBridge.csclass.mergeOnDatabound(grid_pass1, 13, 13)
    End Sub

    Protected Sub grid_miss1_DataBound(sender As Object, e As EventArgs) Handles grid_miss1.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_miss1, 3, 3)
        WeightBridge.csclass.mergeOnDatabound(grid_miss1, 2, 2)
    End Sub

    Protected Sub grid_dev1_DataBound(sender As Object, e As EventArgs) Handles grid_dev1.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_dev1, 0, 0)
    End Sub

    Protected Sub grid_plan1_DataBound(sender As Object, e As EventArgs) Handles grid_plan1.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_plan1, 0, 0)
    End Sub

    Protected Sub grid_plan2_DataBound(sender As Object, e As EventArgs) Handles grid_plan2.DataBound
        WeightBridge.csclass.mergeOnDatabound(grid_plan2, 0, 0)
    End Sub
End Class
