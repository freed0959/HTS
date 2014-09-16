using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/*untuk pdf*/
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using WeightBridge;
using System.Web.UI.HtmlControls;

public partial class HTS2_pspln29 : System.Web.UI.Page
{
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    public static String ConnectionString
    {
        get { return ConfigurationManager.ConnectionStrings["HTSdbConn"].ConnectionString; }
    }
        
    /*====================================================================CONVERT TO EXCEL !!!!!================================================================ */
    /*public void xlsport(string ds, GridView grd, string _fnm)
    {
        grd.Visible = true;
        csclass.gridstrip(grd);

        grd.DataSourceID = ds;
        grd.DataBind();


        Random rnd = new Random();
        int value = Convert.ToInt32(200 * rnd.Next()) + 1;
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=" + _fnm + value + "_" + String.Format(DateTime.Now.ToString("MMddyyHHmmss")) + ".xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        EnableViewState = false;
        Response.Charset = string.Empty;

        System.IO.StringWriter stw = new System.IO.StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);

        form.Controls.Add(grd);
        this.Controls.Add(form);

        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }*/

    /*public void pdfPort(GridView grd, int lnscp, string _fnm)
    {
        grd.Visible = true;
        if (grd is System.Web.UI.WebControls.GridView)
        {
            csclass.gridstrip(grd);
        }

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + _fnm + "_" + DateTime.Today.ToString("MM/dd/yyyy") + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grd.AllowPaging = false;
        grd.DataBind();
        grd.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 50f, 30f);
        if (lnscp == 1)
        {
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A3.Rotate());
        }
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter pdfWrite = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();

        Response.Write(pdfDoc);
        Response.End();
    }*/

    public void MergeRowsWithSameContent(GridView gvw, int target, int _ref)
    {
        for (int rowIndex = gvw.Rows.Count - 2; rowIndex >= 0; rowIndex += -1)
        {
            GridViewRow gvRow = gvw.Rows[rowIndex];
            GridViewRow gvPreviousRow = gvw.Rows[rowIndex + 1];

            for (int cellCount = target; cellCount <= target; cellCount++)
            {
                if ((gvRow.Cells[_ref].Text == gvPreviousRow.Cells[_ref].Text))
                {
                    if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                    {
                        gvRow.Cells[cellCount].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[cellCount].RowSpan = gvPreviousRow.Cells[cellCount].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[cellCount].Visible = false;
                }

            }
        }
    }

    protected void GetLastSent(Label lbl, string type)
    {
        try
        {
            String connect = ConnectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(" SELECT MAX([dtm_last_sent]) as data1 FROM [HTSdb].[dbo].[tbl_sys_notification_monitor] m " +
                                  " WHERE ([id_notmonitor_type] = " + type + ")", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                           while (dr.Read())
                           {
                               DateTime _dtm = DateTime.Parse(dr["data1"].ToString());
                                lbl.Text = String.Concat("last broadcasted at: ", _dtm.ToString("dd MMM yy, HH:mm"));
                            }
                        }                       
                        dr.Close();
                    }
                }
                conn.Close();
            }
            connect = null;
        }
        catch
        {
            lbl.Text = "Data not found";
        }

    }

    protected int GetOption(string k1, string k3)
    {
        try
        {
            object _res;
            int _k1 = GetProductCode(k1);
            int _k3 = GetProductCode(k3);
            string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select mo.ID_Opsi from tbl_MappingOpsi mo where mo.K1 = " + _k1 + " and mo.K3 =" + _k3;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            _res = cmd.ExecuteScalar();
            conn.Close();

            return int.Parse(_res.ToString());
        }
        catch
        {
            return 0;
        }
    }

    protected int CheckProduct(string prd)
    {
        try
        {
            int _ret;
            String connect = ConnectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id_ref_master FROM tbl_ref_master WHERE ref_name = '" + prd + "'", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            _ret = 1;
                        }
                        else
                        {
                            _ret = 0;
                        }
                        dr.Close();
                    }
                }
                conn.Close();
            }
            connect = null;
            return _ret;
        }
        catch
        {
            return 0;
        }
    }

    protected int CheckDuplicate(DateTime dtm)
    {
        try
        {
            object _res;
            string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT TOP 1 ID_PlnBrg FROM dbo.tbl_PlanBarging WHERE (CONVERT(nvarchar, pln_date, 101) = @dtm) " +
                                            "AND (CONVERT(nvarchar, pln_date, 108) = @timex)";
            cmd.Parameters.Add("dtm", SqlDbType.VarChar).Value = dtm.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("timex", SqlDbType.VarChar).Value = dtm.ToString("HH:mm:ss");

            cmd.CommandType = CommandType.Text;
            conn.Open();
            _res = cmd.ExecuteScalar();
            conn.Close();

            if (_res != null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        catch
        {
            return 0;
        }

    }
    protected int GetProductCode(string dest)
    {
        try
        {
            object _res;
            string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select id_ref_master from tbl_ref_master where ref_name = '" + dest + "'";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            _res = cmd.ExecuteScalar();
            conn.Close();

            return int.Parse(_res.ToString());
        }
        catch
        {
            return 0;
        }
    }

    protected void GetOpsiTable()
    {
        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = " SELECT mas1.ref_name [Product K1], mas3.ref_name [Product K3], [OpsiName] [Options] FROM [HTSdb].[dbo].[tbl_MappingOpsi] m " +
                              " INNER JOIN dbo.tbl_ref_master mas1 ON m.K1 = mas1.id_ref_master " +
                              " INNER JOIN dbo.tbl_ref_master mas3 ON m.K3 = mas3.id_ref_master ";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);

            //Create a DataSet which will hold the data extracted from the worksheet.
            DataTable dt = new DataTable();

            //Fill the DataSet from the data extracted from the worksheet.
            sqlda.Fill(dt);
            conn.Close();

            grid_opsi1.DataSource = dt;
            grid_opsi1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("error " + ex.ToString());
        }
    }

    protected void insertToDB(string plandate, string K1, string K3)
    {
        String connect = ConnectionString;
        SqlConnection conn = new SqlConnection(connect);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        string k1 = GetProductCode(K1).ToString();
        //cmd.CommandText = "select id_ref_master from tbl_ref_master where ref_name = '"+K1+"'";
        //conn.Open();
        //k1 = cmd.ExecuteScalar().ToString();
        //conn.Close();

        string k3 = GetProductCode(K3).ToString();
        //cmd.CommandText = "select id_ref_master from tbl_ref_master where ref_name = '" + K3 + "'";
        //conn.Open();
        //k3 = cmd.ExecuteScalar().ToString();
        //conn.Close();

        string id = GetOption(K1, K3).ToString();
        //cmd.CommandText = "select mo.ID_Opsi from tbl_MappingOpsi mo where mo.K1 = "+ k1 +" and mo.K3 ="+ k3;
        //conn.Open();
        //id = cmd.ExecuteScalar().ToString();
        //conn.Close();

        if (id != "0")
        {
            cmd.CommandText = "INSERT INTO tbl_PlanBarging ([pln_date],[K1],[K3],[ID_Opsi]) " +
                "VALUES (@pln_date,@K1,@K3,@ID_Opsi) ";

            cmd.Parameters.Add("@pln_date", SqlDbType.DateTime).Value = Convert.ToDateTime(plandate);
            cmd.Parameters.Add("@K1", SqlDbType.BigInt).Value = Int64.Parse(k1);
            cmd.Parameters.Add("@K3", SqlDbType.BigInt).Value = Int64.Parse(k3);
            cmd.Parameters.Add("@ID_Opsi", SqlDbType.SmallInt).Value = Int16.Parse(id);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    public void delFile(string picPath)
    {
        try
        {
            FileInfo TheFile = new FileInfo(picPath);
            if (TheFile.Exists)
            {
                File.Delete(picPath);
            }
            else
            {
                throw new FileNotFoundException();
            }

        }
        catch (FileNotFoundException ex)
        {
            WeightBridge.csclass.mymsg(":: file not found", "#");
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: file deletion error", "#");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        int ugrup = 5;
        if (Request.Cookies["htspub"] != null)
        {
            ugrup = Int16.Parse(Request.Cookies["htspub"]["idgroup"].ToString());
        }

        Control maintctrl = (Control)Page.LoadControl("PassMenu.ascx");
        passmenu1.Controls.Add(maintctrl);

        Control romctrl = (Control)Page.LoadControl("RomCondition1.ascx");
        rommenu1.Controls.Add(romctrl);

        tabcons.ActiveTabIndex = 0;

        GetOpsiTable();

        // cek to determine date
        string tanggal;
        if (Request.QueryString["dtm"] != null)
        {
            tanggal = Request.QueryString["dtm"];
        }
        else
        {
            tanggal = DateTime.Today.ToString("MM/dd/yyyy");
        }
        txt_date.Text = tanggal;

        ds_sp_brgPlnShf1.SelectParameters.Clear();
        ds_sp_brgPlnShf1.SelectParameters.Add("tgl", System.Data.DbType.String, tanggal);
        grd_brgPln.DataBind();

        ds_sp_planpass_rep1.SelectParameters.Clear();
        ds_sp_planpass_rep1.SelectParameters.Add("dtm", System.Data.DbType.String, tanggal);
        ds_sp_planpass_rep1.SelectParameters.Add("shf", System.Data.DbType.String, "1");
        grid_plan1.DataBind();

        ds_sp_passplan_rep2.SelectParameters.Clear();
        ds_sp_passplan_rep2.SelectParameters.Add("dtm", System.Data.DbType.String, tanggal);
        ds_sp_passplan_rep2.SelectParameters.Add("shf", System.Data.DbType.String, "2");
        grid_plan2.DataBind();

        string qs = Request.QueryString["v"];

        switch (ugrup)
        {
            case 2:
                ibt_send1.Visible = true;
                ibt_send2.Visible = true;
                div_upload1.Visible = true;
                break;

            case 45:
                ibt_send1.Visible = true;
                ibt_send2.Visible = true;
                div_upload1.Visible = true;
                break;

            case 16:
                ibt_send1.Visible = true;
                ibt_send2.Visible = true;
                div_upload1.Visible = true;
                break;

           default:
                pan_load67.Visible = false;
                grd_brgPln.Columns[6].Visible = true;
                grd_brgPln.Columns[7].Visible = true;                
                grd_brgPln.Columns[4].Visible = false;                
                grd_brgPln.Columns[5].Visible = false;
                grd_brgPln.Columns[9].Visible = false;
                grd_brgPln.Columns[10].Visible = false;
                break;
        }

        GetLastSent(lbl_infosent1, "48");
        GetLastSent(lbl_infosent2, "49");
    }

    protected void dte_txt_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            dte_txt.Text = DateTime.Today.ToString("MM/dd/yyyy");
        }
    }
    
    
    protected void btn_brgPln_Click(object sender, EventArgs e)
    {
        string tanggal = txt_date.Text.ToString();
        ds_sp_brgPlnShf1.SelectParameters.Clear();
        ds_sp_brgPlnShf1.SelectParameters.Add("tgl", System.Data.DbType.String, tanggal);

        ds_sp_planpass_rep1.SelectParameters.Clear();
        ds_sp_planpass_rep1.SelectParameters.Add("dtm", System.Data.DbType.String, tanggal);
        ds_sp_planpass_rep1.SelectParameters.Add("shf", System.Data.DbType.String, "1");
        grid_plan1.DataBind();

        ds_sp_passplan_rep2.SelectParameters.Clear();
        ds_sp_passplan_rep2.SelectParameters.Add("dtm", System.Data.DbType.String, tanggal);
        ds_sp_passplan_rep2.SelectParameters.Add("shf", System.Data.DbType.String, "2");
        grid_plan2.DataBind();

    }

  
    protected void lbt_upl67_Click(object sender, EventArgs e)
    {
        lbt_upl67.Enabled = false;
        FileUpload PictureUpload = FileUpload1;
        string fileloc = ConfigurationManager.AppSettings["fileloc"].ToString();
        DateTime dtm = DateTime.Parse(dte_txt.Text);
        string tanggal = dtm.ToString("MM/dd/yyyy");

        //Upload file to server
        try
        {
            if (PictureUpload.HasFile)
            {
                if (string.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", true) != 0)
                {
                    lbt_upl67.Enabled = true;
                    WeightBridge.csclass.mymsg(":: alert('Only MS-Excel 2007 or above (.xlsx) files are accepted'", "#");                    
                    return;                    
                }
            }

            string fileNameWoExt = System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName);
            string filename = fileNameWoExt.Substring(0, 2);
            string ext = System.IO.Path.GetExtension(PictureUpload.FileName).ToString();
            string picPath = string.Concat(fileloc, filename, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName));

            PictureUpload.SaveAs(picPath);
            // Response.Write(@"<script language='javascript'>alert('picpath --> "+ picPath +"');</script>");

            OleDbConnection oconn = new OleDbConnection();
            oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + picPath.ToString() + ";Extended Properties=" + (char)34 + "Excel 12.0;HDR=YES" + (char)34 + ";";

            try
            {
                oconn.Open();
                DataTable dtExcelSchema = default(DataTable);
                dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                //Response.Write(@"<script language='javascript'>alert('s1 : " + SheetName + "');</script>");
                oconn.Close();

                oconn.Open();
                OleDbCommand ocmd = new OleDbCommand("select * from [" + SheetName + "]", oconn);
                //OleDbDataReader ode = ocmd.ExecuteReader();

                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = ocmd;

                //Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();

                //Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds);
                oconn.Close();

                //---- DELETE FILE EXCEL DARI SERVER ------
                delFile(picPath);

                //Check Product
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string prd1 = ds.Tables[0].Rows[i][1].ToString();
                        string prd2 = ds.Tables[0].Rows[i][2].ToString();
                        int cekvalue1 = CheckProduct(prd1);
                        int cekvalue2 = CheckProduct(prd2);
                        if (cekvalue1 == 0)
                        {
                            lbt_upl67.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + prd1 + " (line "+ i+1 +") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }

                        if (cekvalue2 == 0)
                        {
                            lbt_upl67.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + prd2 + " (line " + i+1 + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    lbt_upl67.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking product", "#");
                    return;
                }

                //Check Option
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string k1 = ds.Tables[0].Rows[i][1].ToString();
                        string k3 = ds.Tables[0].Rows[i][2].ToString();
                        int cekvalue = GetOption(k1, k3);                        
                        if (cekvalue == 0)
                        {
                            lbt_upl67.Enabled = true;
                            WeightBridge.csclass.mymsg(":: Option not found on "+ k1 +" and "+ k3 +" pairing , upload canceled", "#");
                            oconn = null;
                            return;
                        }
                        
                    }
                }
                catch
                {
                    lbt_upl67.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking option", "#");
                    return;
                }

                //Check duplication
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime tgl = DateTime.Parse(ds.Tables[0].Rows[i][0].ToString());
                        DateTime pln_date = DateTime.Parse(dtm.ToString("yyyy-MM-dd") + " " + tgl.ToString("hh:mm tt"));

                        int cekduplicate = CheckDuplicate(pln_date);
                        if (cekduplicate == 0)
                        {
                            lbt_upl67.Enabled = true;
                            WeightBridge.csclass.mymsg(":: Duplicate data detected, use edit. Upload canceled.", "#");
                            return;
                        }
                    }
                }
                catch
                {
                    lbt_upl67.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on duplicate checking", "#");
                    return;
                }

                //Insert data to table
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime tgl = DateTime.Parse(ds.Tables[0].Rows[i][0].ToString());                        
                        string pln_date = dtm.ToString("yyyy-MM-dd") + " " + tgl.ToString("HH:mm");
                        //string pln_date = tanggal + " " + tgl.ToString("HH:mm");
                        string k1 = ds.Tables[0].Rows[i][1].ToString();
                        string k3 = ds.Tables[0].Rows[i][2].ToString();

                        //WeightBridge.csclass.mymsg("" + pln_date + "/" + k1 + "/" + k3, "#");

                        insertToDB(pln_date, k1, k3);
                    }

                    WeightBridge.csclass.mymsg("Barging Plan Upload Complete", "pspln29?v=1");
                }
                catch
                {
                    lbt_upl67.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on inserting data", "#");
                    return;
                }

            }
            catch (SqlException eq)
            {
                lbt_upl67.Enabled = true;
                WeightBridge.csclass.mymsg(":: error sql", "#");
                return;
            }
            catch (DataException ee)
            {
                lbt_upl67.Enabled = true;
                WeightBridge.csclass.mymsg(":: error data", "#");
                return;
            }            
        }

        catch
        {
            lbt_upl67.Enabled = true;
            WeightBridge.csclass.mymsg(":: unknown error", "#");
            return;
        }
        lbt_upl67.Enabled = true;
    }


    protected void grd_brgPln_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header) {
            DateTime dtm = DateTime.Parse(txt_date.Text);
            string l_dtm = dtm.ToString("ddd, dd MMMM yyyy");
            WeightBridge.csclass.addHead(6, "Barging Plan for : " + l_dtm, grd_brgPln);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string _date = DataBinder.Eval(e.Row.DataItem, "dt").ToString();
            string _idp = DataBinder.Eval(e.Row.DataItem, "id_PlnBrg").ToString();

            if (_date != "")
            {
                int _idx = int.Parse(DataBinder.Eval(e.Row.DataItem, "idx").ToString());

               //  DateTime dnow = DateTime.Parse("08/22/2014 02:00:00");
                DateTime dnow = DateTime.Now;

                if (DateTime.Parse(_date).ToString("MM/dd/yyyy") == DateTime.Today.ToString("MM/dd/yyyy"))
                {
                   if (dnow.Hour < 6 && _idx <= (dnow.Hour + 19))
                    {
                         //if time between 00 and 05 and date already passed, disable edit
                        ((ImageButton)e.Row.FindControl("ibt_save1")).ImageUrl = "~/HTS2/Images/save_dis.png";
                        ((ImageButton)e.Row.FindControl("ibt_save1")).Enabled = false;
                        e.Row.FindControl("ddl_k1").Visible = false;
                        e.Row.FindControl("ddl_k3").Visible = false;
                        e.Row.FindControl("lbl_k1").Visible = true;
                        e.Row.FindControl("lbl_k3").Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "EditSMB.aspx?noedit=" + _date + "&idp=" + _idp;
                    }
                   else if (dnow.Hour >= 6 && _idx <= (dnow.Hour - 5))
                   {
                       //if time already passed, disable edit
                       ((ImageButton)e.Row.FindControl("ibt_save1")).ImageUrl = "~/HTS2/Images/save_dis.png";
                       ((ImageButton)e.Row.FindControl("ibt_save1")).Enabled = false;
                       e.Row.FindControl("ddl_k1").Visible = false;
                       e.Row.FindControl("ddl_k3").Visible = false;
                       e.Row.FindControl("lbl_k1").Visible = true;
                       e.Row.FindControl("lbl_k3").Visible = true;
                       ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "EditSMB.aspx?noedit=" + _date + "&idp=" + _idp;
                   }

                   if (Request.Cookies["htspub"]["idgroup"] != "2" && Request.Cookies["htspub"]["idgroup"] != "45" && Request.Cookies["htspub"]["idgroup"] != "16")
                   {
                       ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "EditSMB.aspx?noedit=" + _date + "&idp=" + _idp;

                   }                   
                }

                else if (DateTime.Parse(_date) < DateTime.Today)
                {
                    // if date already passed, disable edit
                    ((ImageButton)e.Row.FindControl("ibt_save1")).ImageUrl = "~/HTS2/Images/save_dis.png";
                    ((ImageButton)e.Row.FindControl("ibt_save1")).Enabled = false;
                    e.Row.FindControl("ddl_k1").Visible = false;
                    e.Row.FindControl("ddl_k3").Visible = false;
                    e.Row.FindControl("lbl_k1").Visible = true;
                    e.Row.FindControl("lbl_k3").Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "EditSMB.aspx?noedit=" + _date + "&idp=" + _idp;
                }

            }
            else {
                // if no data, disable edit
                ((ImageButton)e.Row.FindControl("ibt_save1")).ImageUrl = "~/HTS2/Images/save_dis.png";
                ((ImageButton)e.Row.FindControl("ibt_save1")).Enabled = false;
                e.Row.FindControl("ddl_k1").Visible = false;
                e.Row.FindControl("ddl_k3").Visible = false;
                e.Row.FindControl("lbl_k1").Visible = true;
                e.Row.FindControl("lbl_k3").Visible = true;
  
            }
            
           // Response.Write(@"<script language='javascript'>alert('"+_date+"')</script>");

            ImageButton deleteTemplateField = e.Row.FindControl("ibt_save1") as ImageButton;
            //Set OnClientClick attribute for Delete Button
            deleteTemplateField.OnClientClick = "return confirm('Warning: Updating product pairing, will erase all previous blending scheme. Are you sure to perform update?');";

            
        }
    }
      
   
    protected void templt1_Click(object sender, ImageClickEventArgs e)
    {
        WeightBridge.csclass.dtemplt("psPln29_upload.xlsx", Page);
    }
        
    protected void grd_brgPln_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       if (e.CommandName == "_save")
            {
                try
                {                  
                    GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;

                    ImageButton ibt = (ImageButton)row.FindControl("ibt_save1");
                    DropDownList dk1 = (DropDownList)row.FindControl("ddl_k1");
                    DropDownList dk3 = (DropDownList)row.FindControl("ddl_k3");
                    string idbrg = ibt.CommandArgument.ToString();
                    //WeightBridge.csclass.mymsg("k1:"+ dk1.SelectedValue.ToString() +" k3:"+ dk3.SelectedValue.ToString() +" idbrg: "+ idbrg , "#");
                    
                    int cekvalue = GetOption(dk1.SelectedItem.Text.ToString(), dk3.SelectedItem.Text.ToString());
                    //WeightBridge.csclass.mymsg("cek:"+ cekvalue.ToString(), "#");

                    if (cekvalue != 0)
                    {
                        String connect = ConnectionString;
                        SqlConnection conn = new SqlConnection(connect);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                                                
                        cmd.CommandText = "sp_EditPlan";
                        cmd.Parameters.Add("K1", System.Data.DbType.Int64).Value = dk1.SelectedValue.ToString();
                        cmd.Parameters.Add("K3", System.Data.DbType.Int64).Value = dk3.SelectedValue.ToString();
                        cmd.Parameters.Add("ID_PlnBrg", System.Data.DbType.Int64).Value = idbrg;
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();                        

                        WeightBridge.csclass.mymsg("Barging Plan updated", "#");
                        grd_brgPln.DataBind();
                    }
                    else {
                        WeightBridge.csclass.mymsg(":: Option not found on " + dk1.SelectedItem.Text.ToString() + " and " + dk3.SelectedItem.Text.ToString() + " pairing", "#");                    
                    }
                }
                catch (Exception ex)
                {
                    WeightBridge.csclass.mymsg(":: Error " + ex.ToString().Substring(0, 50), "#");

                }
        }
        
    }
    protected void ibt_xls1_Click(object sender, ImageClickEventArgs e)
    {
        grd_brgPln.Columns[1].Visible = true;
        grd_brgPln.Columns[4].Visible = false;
        grd_brgPln.Columns[5].Visible = false;
        grd_brgPln.Columns[6].Visible = true;
        grd_brgPln.Columns[7].Visible = true;
        grd_brgPln.Columns[8].Visible = false;
        grd_brgPln.Columns[9].Visible = false;
        grd_brgPln.Columns[10].Visible = true;
        WeightBridge.ExportFile.xlsport(grd_brgPln, "barging_plan", this);
        //xlsport("ds_sp_brgPlnShf1", grd_brgPln, "barging_plan");
    }
    protected void ibt_pdf1_Click(object sender, ImageClickEventArgs e)
    {
        grd_brgPln.Columns[1].Visible = true;
        grd_brgPln.Columns[4].Visible = false;
        grd_brgPln.Columns[5].Visible = false;
        grd_brgPln.Columns[6].Visible = false;
        grd_brgPln.Columns[7].Visible = false;
        grd_brgPln.Columns[8].Visible = true;
        grd_brgPln.Columns[9].Visible = true;
        grd_brgPln.Columns[10].Visible = true;
        WeightBridge.ExportFile.pdfport(grd_brgPln, "barging_plan", 0, this);
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("pspln29.aspx?dtm="+ txt_date.Text.ToString());
    }
    
    protected void grid_plan1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DateTime dtm = DateTime.Parse(txt_date.Text);
            WeightBridge.csclass.addHead(14, "Passing Plan For "+ dtm.ToString("ddd, dd MMM yyyy") +" :: Shift 1", grid_plan1);
        }

        if (e.Row.RowType == DataControlRowType.DataRow) {
            MergeRowsWithSameContent(grid_plan1, 0, 0);

            if (DataBinder.Eval(e.Row.DataItem, "KontraktorKode").ToString() == "TOTAL") {
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.Font.Bold = true;
                e.Row.Font.Size = 9;
            }
        }
    }
    protected void grid_plan2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DateTime dtm = DateTime.Parse(txt_date.Text);
            WeightBridge.csclass.addHead(14, "Passing Plan For " + dtm.ToString("ddd, dd MMM yyyy") + " :: Shift 2", grid_plan2);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MergeRowsWithSameContent(grid_plan2, 0, 0);

            if (DataBinder.Eval(e.Row.DataItem, "KontraktorKode").ToString() == "TOTAL")
            {
                e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.Font.Bold = true;
                e.Row.Font.Size = 9;
            }
        }
    }

    protected void ibt_xls2_Click(object sender, ImageClickEventArgs e)
    {
        WeightBridge.ExportFile.xlsport(grid_plan1, "passing_plan_shif1_", this);
        //xlsport("ds_sp_planpass_rep1", grid_plan1, "passing_plan_shif1_");
    }

    protected void ibt_pdf2_Click(object sender, ImageClickEventArgs e)
    {
        WeightBridge.ExportFile.pdfport(grid_plan1, "passing_plan_shif1_", 1, this);
        //pdfPort(grid_plan1, 1, "passing_plan_shif1_");
    }

    protected void ibt_xls3_Click(object sender, ImageClickEventArgs e)
    {
        WeightBridge.ExportFile.xlsport(grid_plan2, "passing_plan_shif2_", this);
        //xlsport("ds_sp_passplan_rep2", grid_plan2, "passing_plan_shif2_");

    }

    protected void ibt_pdf3_Click(object sender, ImageClickEventArgs e)
    {
        WeightBridge.ExportFile.pdfport(grid_plan2, "passing_plan_shif2_", 1, this);
        //pdfPort(grid_plan2, 1, "passing_plan_shif2_");
    }

    protected void ibt_send1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DateTime _dtm = DateTime.Parse(String.Concat(txt_date.Text, " ", DateTime.Now.ToString("HH:mm")));

            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "_sp_planpass_not1";
            cmd.Parameters.AddWithValue("@fulldtm", _dtm);
            cmd.Parameters.AddWithValue("@shf", "1");
            cmd.Parameters.AddWithValue("@usr", Request.Cookies["htspub"]["idusr"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            connect = null;

            WeightBridge.csclass.mymsg("Broadcast sent", "#");
        }
        catch (Exception exp)
        {
            WeightBridge.csclass.mymsg(":: error " + exp.ToString().Substring(0, 50), "#");

        }
        //WeightBridge.csclass.mymsg("" + Request.Cookies["htspub"]["idusr"].ToString(), "#");

        
    }

    protected void ibt_send2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DateTime _dtm = DateTime.Parse(String.Concat(txt_date.Text, " ", DateTime.Now.ToString("HH:mm")));

            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "_sp_planpass_not1";
            cmd.Parameters.AddWithValue("@fulldtm", _dtm);
            cmd.Parameters.AddWithValue("@shf", "2");
            cmd.Parameters.AddWithValue("@usr", Request.Cookies["htspub"]["idusr"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            connect = null;

            WeightBridge.csclass.mymsg("Broadcast sent", "#");
        }
        catch (Exception exp)
        {
            WeightBridge.csclass.mymsg(":: error " + exp.ToString().Substring(0, 50), "#");

        }

    }

    protected void ibt_chart1_Load(object sender, EventArgs e)
    {
        ibt_chart1.Attributes.Add("onClick", "window.open('passchart1.aspx?v=0&dtm=" + txt_date.Text + "', '" + ID + "','status=0,scrollbars=1,resizable=1,width=1204,height=750,left=50,top=10'); return false; window.focus();");
    }
    protected void grd_brgPln_DataBound(object sender, EventArgs e)
    {        
        WeightBridge.csclass.mergeOnDatabound(grd_brgPln, 2, 2);
    }
}