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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class HTS2_EditSMB : System.Web.UI.Page
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
            WeightBridge.csclass.mymsg("file not found", "#");
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg("file deletion error", "#");
        }
    }


    protected void GetBplanTable(string idp)
    {
        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = " SELECT [Plan Date], [Hour], [K1 Product], [K3 Product], [Option] FROM dbo.vwPassPlan_title1 " +
                              " WHERE ID_PlnBrg = @idp";
            cmd.Parameters.Add("@idp", System.Data.DbType.Int64).Value = idp;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);

            //Create a DataSet which will hold the data extracted from the worksheet.
            DataTable dt = new DataTable();

            //Fill the DataSet from the data extracted from the worksheet.
            sqlda.Fill(dt);
            conn.Close();

            grid_info1.DataSource = dt;
            grid_info1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("error " + ex.ToString());
        }
    }

    protected void GetSnip1(string idp, string des, GridView grid)
    {
        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "_sp_planpass_snip1";
            cmd.Parameters.Add("@id_pln", System.Data.DbType.Int64).Value = idp;
            cmd.Parameters.Add("@des", System.Data.DbType.Int64).Value = des;
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);

            //Create a DataSet which will hold the data extracted from the worksheet.
            DataTable dt = new DataTable();

            //Fill the DataSet from the data extracted from the worksheet.
            sqlda.Fill(dt);
            conn.Close();

            grid.DataSource = dt;
            grid.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("error " + ex.ToString());
        }
    }


    protected void GetSnip2(string idp)
    {
        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "_sp_planpass_snip2";
            cmd.Parameters.Add("@id_pln", System.Data.DbType.Int64).Value = idp;
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);

            //Create a DataSet which will hold the data extracted from the worksheet.
            DataTable dt = new DataTable();

            //Fill the DataSet from the data extracted from the worksheet.
            sqlda.Fill(dt);
            conn.Close();

            grid_sumtruck1.DataSource = dt;
            grid_sumtruck1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("error " + ex.ToString());
        }
    }

    protected string GetBplanInfoPttle(string idp)
    {
        object bInfo;
        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = " SELECT _ptitle FROM dbo.vwPassPlan_title1 " +
                              " WHERE ID_PlnBrg = @idp";
            cmd.Parameters.Add("@idp", System.Data.DbType.Int64).Value = idp;
            cmd.CommandType = CommandType.Text;
            conn.Open();
            bInfo = cmd.ExecuteScalar();
            conn.Close();
            cmd.Parameters.Clear();
        }
        catch
        {
            bInfo = ":: cannot found barge plan";
        }

        return bInfo.ToString();

    }

    protected int CheckMaterial(string mat)
    {
        String connect = ConnectionString;
        using (SqlConnection conn = new SqlConnection(connect))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Kode FROM v_Material WHERE kode = '" + mat + "'", conn))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    dr.Close();
                }
            }
            conn.Close();
        }
        connect = null;
    }

    protected int CheckComp(string kon)
    {
        String connect = ConnectionString;
        using (SqlConnection conn = new SqlConnection(connect))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Kode FROM dbo.v_Kontraktor WHERE Kode = '" + kon + "'", conn))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    dr.Close();
                }
            }
            conn.Close();
        }
        connect = null;
    }

    protected int cekMaterialKode(string kon, string mat, string idp, string dest)
    {
        int hasil = 0;
        String connect = ConnectionString;
        String prdcheck = "select ID_actMB from [tbl_ActMasterBlend] WHERE (MaterialKode = '" + mat + "') AND (KontraktorKode = '" + kon + "') " +
            " AND (ID_PlnBrg=" + idp + ") AND (Destination=" + dest + ")";

        using (SqlConnection conn = new SqlConnection(connect))
        {
            using (SqlCommand cmd = new SqlCommand(prdcheck, conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        hasil = 0;
                    }
                    else
                    {
                        hasil = 1;
                    }
                    dr.Close();
                }
            }
            conn.Close();
        }
        return hasil;
    }

    protected string validNumber(string stval)
    {
        //if any columns are found null then they are replaced by zero        
        object val = stval;
        string result = "0";

        try
        {
            bool c1 = val is float;
            bool c2 = val is double;
            bool c3 = val is decimal;

            if (c1 == true || c2 == true || c3 == true)
            {
                double dd1 = Convert.ToDouble(val);
                int _res1 = (int)Math.Ceiling(dd1);

                result = _res1.ToString();

                //string[] angka = val.ToString().Split('.');
                //result = angka[0];
            }
            else
            {
                int _val;
                try
                {
                    _val = int.Parse(val.ToString());
                }
                catch
                {
                    _val = 0;
                }
                result = _val.ToString();
            }
        }
        catch
        {
            result = Convert.ToString(0);
        }
        return result;
    }


    protected void insertBlend(string idp, string mat, string kon, string trc, string dest)
    {
        String connect = ConnectionString;
        SqlConnection conn = new SqlConnection(connect);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "sp_insertMaterial";
        cmd.Parameters.Add("@idpl", System.Data.DbType.Int64).Value = idp;
        cmd.Parameters.Add("@mat", System.Data.DbType.String).Value = mat;
        cmd.Parameters.Add("@kntr", System.Data.DbType.String).Value = kon;
        cmd.Parameters.Add("@trcount", System.Data.DbType.Int16).Value = int.Parse(trc);
        cmd.Parameters.Add("@dest", System.Data.DbType.Int64).Value = dest;
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        cmd.Parameters.Clear();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        string datak = Request.QueryString["idp"].ToString();
        Page.Title = GetBplanInfoPttle(datak);
        GetBplanTable(datak);
        GetSnip2(datak);

        GetSnip1(datak, "42", grid_ratiok1);
        GetSnip1(datak, "43", grid_ratiok3);
        GetSnip1(datak, "44", grid_ratiosp);

        int ugrup = 5;
        if (Request.Cookies["htspub"] != null)
        {
            ugrup = Int16.Parse(Request.Cookies["htspub"]["idgroup"].ToString());
        }

        Control maintctrl = (Control)Page.LoadControl("PassMenu.ascx");
        passmenu1.Controls.Add(maintctrl);

        Control romctrl = (Control)Page.LoadControl("RomCondition1.ascx");
        pan_rom1.Controls.Add(romctrl);

        tab29con.ActiveTabIndex = 0;

        switch (ugrup)
        {
            case 2:
                if (Request.QueryString["noedit"] != null)
                {
                    div_btn1.Visible = false;
                    div_btn3.Visible = false;
                    div_btnsp.Visible = false;

                    div_upload1.Visible = false;
                    div_upload2.Visible = false;
                    div_upload3.Visible = false;

                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    Panel5.Visible = false;
                }
                break;

            case 45:
                if (Request.QueryString["noedit"] != null)
                {
                    div_btn1.Visible = false;
                    div_btn3.Visible = false;
                    div_btnsp.Visible = false;

                    div_upload1.Visible = false;
                    div_upload2.Visible = false;
                    div_upload3.Visible = false;

                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    Panel5.Visible = false;
                }
                break;

            case 16:
                if (Request.QueryString["noedit"] != null)
                {
                    div_btn1.Visible = false;
                    div_btn3.Visible = false;
                    div_btnsp.Visible = false;

                    div_upload1.Visible = false;
                    div_upload2.Visible = false;
                    div_upload3.Visible = false;

                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    Panel5.Visible = false;
                }
                break;

            default:
                div_btn1.Visible = false;
                div_btn3.Visible = false;
                div_btnsp.Visible = false;

                div_upload1.Visible = false;
                div_upload2.Visible = false;
                div_upload3.Visible = false;

                Panel1.Visible = false;
                Panel3.Visible = false;
                Panel5.Visible = false;
                break;

        }

    }


    protected void link_save_Click(object sender, EventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();
        link_save.Enabled = false;
        FileUpload PictureUpload = fup_k1;
        string fileloc = ConfigurationManager.AppSettings["fileloc"].ToString();

        try
        {
            if (PictureUpload.HasFile)
            {
                if (string.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", true) != 0)
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: Only MS-Excel ver. 2007 or above (.xlsx) files are accepted", "#");
                    return;
                }
            }

            string fileNameWoExt = System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName);
            string filename = fileNameWoExt.Substring(0, 2);
            string ext = System.IO.Path.GetExtension(PictureUpload.FileName).ToString();
            string picPath = string.Concat(fileloc, filename, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName));

            PictureUpload.SaveAs(picPath);
            //WeightBridge.csclass.mymsg("picpath :: " + picPath , "Rom");

            OleDbConnection oconn = new OleDbConnection();
            oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + picPath.ToString() + ";Extended Properties=" + (char)34 + "Excel 12.0;HDR=YES" + (char)34 + ";";

            try
            {
                oconn.Open();
                DataTable dtExcelSchema = default(DataTable);
                dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                //WeightBridge.csclass.mymsg("sheetname :: " + SheetName, "Rom");                                             
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

                // check material name
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string mat = ds.Tables[0].Rows[i][1].ToString();
                        int cekvalue = CheckMaterial(mat);
                        if (cekvalue == 0)
                        {
                            link_save.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + mat + " (line " + (i + 1) + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking raw cargo", "#");
                    return;
                }

                // check contractor name
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        int cekvalue = CheckComp(kon);
                        if (cekvalue == 0)
                        {
                            link_save.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + kon + " (line " + (i + 1) + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking contractor", "#");
                    return;
                }

                // check material duplication
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        string mat = ds.Tables[0].Rows[i][1].ToString();

                        int cekduplicat = cekMaterialKode(kon, mat, idp, "42");
                        if (cekduplicat == 0)
                        {
                            link_save.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + mat + " (line " + (i + 1) + ") is already used in the same company. Upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on duplicate checking", "#");
                    return;
                }

                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        string mat = ds.Tables[0].Rows[i][1].ToString();
                        string trc = validNumber(ds.Tables[0].Rows[i][2].ToString());

                        int cekduplicat = cekMaterialKode(kon, mat, idp, "42");
                        if (cekduplicat == 1)
                        {
                            insertBlend(idp, mat, kon, trc, "42");
                        }
                    }
                    WeightBridge.csclass.mymsg("Blending scheme for K1 upload complete", "EditSMB?idp=" + idp);
                    grd_mtrSkmBlnd.DataBind();
                }
                catch
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on inserting data", "#");
                    return;
                }

            }
            catch (SqlException eq)
            {
                link_save.Enabled = true;
                WeightBridge.csclass.mymsg(":: error sql", "#");
                return;
            }
            catch (DataException ee)
            {
                link_save.Enabled = true;
                WeightBridge.csclass.mymsg(":: error data", "#");
                return;
            }

        }
        catch (Exception ex)
        {
            link_save.Enabled = true;
            WeightBridge.csclass.mymsg(":: " + ex.ToString().Substring(0, 50), "#");
            return;
        }
        link_save.Enabled = true;

    }


    protected void link_K3_Click(object sender, EventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        link_K3.Enabled = false;
        FileUpload PictureUpload = fup_k3;
        string fileloc = ConfigurationManager.AppSettings["fileloc"].ToString();

        try
        {
            if (PictureUpload.HasFile)
            {
                if (string.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", true) != 0)
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: Only MS-Excel ver. 2007 or above (.xlsx) files are accepted", "#");
                    return;
                }
            }

            string fileNameWoExt = System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName);
            string filename = fileNameWoExt.Substring(0, 2);
            string ext = System.IO.Path.GetExtension(PictureUpload.FileName).ToString();
            string picPath = string.Concat(fileloc, filename, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName));

            PictureUpload.SaveAs(picPath);
            //WeightBridge.csclass.mymsg("picpath :: " + picPath , "Rom");

            OleDbConnection oconn = new OleDbConnection();
            oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + picPath.ToString() + ";Extended Properties=" + (char)34 + "Excel 12.0;HDR=YES" + (char)34 + ";";

            try
            {
                oconn.Open();
                DataTable dtExcelSchema = default(DataTable);
                dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                //WeightBridge.csclass.mymsg("sheetname :: " + SheetName, "Rom");                                             
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

                // check material name
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string mat = ds.Tables[0].Rows[i][1].ToString();
                        int cekvalue = CheckMaterial(mat);
                        if (cekvalue == 0)
                        {
                            link_K3.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + mat + " (line " + (i + 1) + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_K3.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking raw cargo", "#");
                    return;
                }

                // check contractor name
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        int cekvalue = CheckComp(kon);
                        if (cekvalue == 0)
                        {
                            link_K3.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + kon + " (line " + (i + 1) + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_K3.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking contractor", "#");
                    return;
                }

                // check material duplication
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        string mat = ds.Tables[0].Rows[i][1].ToString();

                        int cekduplicat = cekMaterialKode(kon, mat, idp, "43");
                        if (cekduplicat == 0)
                        {
                            link_K3.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + mat + " (line " + (i + 1) + ") is already used in the same company. Upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_K3.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on duplicate checking", "#");
                    return;
                }

                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        string mat = ds.Tables[0].Rows[i][1].ToString();
                        string trc = validNumber(ds.Tables[0].Rows[i][2].ToString());

                        int cekduplicat = cekMaterialKode(kon, mat, idp, "43");
                        if (cekduplicat == 1)
                        {
                            insertBlend(idp, mat, kon, trc, "43");
                        }
                    }

                    WeightBridge.csclass.mymsg("Blending scheme for K3 upload complete", "EditSMB?idp=" + idp);
                    grd_mtrSkmBlnd_K3.DataBind();
                }
                catch
                {
                    link_K3.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on inserting data", "#");
                    return;
                }

            }
            catch (SqlException eq)
            {
                link_K3.Enabled = true;
                WeightBridge.csclass.mymsg(":: error sql", "#");
                return;
            }
            catch (DataException ee)
            {
                link_K3.Enabled = true;
                WeightBridge.csclass.mymsg(":: error data", "#");
                return;
            }

        }
        catch (Exception ex)
        {
            link_K3.Enabled = true;
            WeightBridge.csclass.mymsg(":: " + ex.ToString().Substring(0, 50), "#");
            return;
        }

        link_K3.Enabled = true;

    }

    protected void link_stp_Click(object sender, EventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        link_stp.Enabled = false;
        FileUpload PictureUpload = fup_stockpile;
        string fileloc = ConfigurationManager.AppSettings["fileloc"].ToString();

        try
        {
            if (PictureUpload.HasFile)
            {
                if (string.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", true) != 0)
                {
                    link_save.Enabled = true;
                    WeightBridge.csclass.mymsg(":: Only MS-Excel ver. 2007 or above (.xlsx) files are accepted", "#");
                    return;
                }
            }

            string fileNameWoExt = System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName);
            string filename = fileNameWoExt.Substring(0, 2);
            string ext = System.IO.Path.GetExtension(PictureUpload.FileName).ToString();
            string picPath = string.Concat(fileloc, filename, "_", DateTime.Now.ToString("hhmmss"), System.IO.Path.GetExtension(PictureUpload.FileName));

            PictureUpload.SaveAs(picPath);
            //WeightBridge.csclass.mymsg("picpath :: " + picPath , "Rom");

            OleDbConnection oconn = new OleDbConnection();
            oconn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + picPath.ToString() + ";Extended Properties=" + (char)34 + "Excel 12.0;HDR=YES" + (char)34 + ";";

            try
            {
                oconn.Open();
                DataTable dtExcelSchema = default(DataTable);
                dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                //WeightBridge.csclass.mymsg("sheetname :: " + SheetName, "Rom");                                             
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

                // check material name
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string mat = ds.Tables[0].Rows[i][1].ToString();
                        int cekvalue = CheckMaterial(mat);
                        if (cekvalue == 0)
                        {
                            link_stp.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + mat + " (line " + (i + 1) + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_stp.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking raw cargo", "#");
                    return;
                }

                // check contractor name
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        int cekvalue = CheckComp(kon);
                        if (cekvalue == 0)
                        {
                            link_stp.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + kon + " (line " + (i + 1) + ") is unknown, upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_stp.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on checking contractor", "#");
                    return;
                }

                // check material duplication
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        string mat = ds.Tables[0].Rows[i][1].ToString();

                        int cekduplicat = cekMaterialKode(kon, mat, idp, "44");
                        if (cekduplicat == 0)
                        {
                            link_stp.Enabled = true;
                            WeightBridge.csclass.mymsg(":: " + mat + " (line " + (i + 1) + ") is already used in the same company. Upload canceled", "#");
                            oconn = null;
                            return;
                        }
                    }
                }
                catch
                {
                    link_stp.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on duplicate checking", "#");
                    return;
                }

                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string kon = ds.Tables[0].Rows[i][0].ToString();
                        string mat = ds.Tables[0].Rows[i][1].ToString();
                        string trc = validNumber(ds.Tables[0].Rows[i][2].ToString());

                        int cekduplicat = cekMaterialKode(kon, mat, idp, "44");
                        if (cekduplicat == 1)
                        {
                            insertBlend(idp, mat, kon, trc, "44");
                        }
                    }

                    WeightBridge.csclass.mymsg("Blending scheme for Stockpile upload complete", "EditSMB?idp=" + idp);
                    grdStp.DataBind();

                }
                catch
                {
                    link_stp.Enabled = true;
                    WeightBridge.csclass.mymsg(":: error on inserting data", "#");
                    return;
                }

            }
            catch (SqlException eq)
            {
                link_stp.Enabled = true;
                WeightBridge.csclass.mymsg(":: error sql", "#");
                return;
            }
            catch (DataException ee)
            {
                link_stp.Enabled = true;
                WeightBridge.csclass.mymsg(":: error data", "#");
                return;
            }

        }
        catch (Exception ex)
        {
            link_stp.Enabled = true;
            WeightBridge.csclass.mymsg(":: " + ex.ToString().Substring(0, 50), "#");
            return;
        }

        link_stp.Enabled = true;
    }


    protected void grid_info1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            for (int i = 0; i < 5; i++)
            {
                e.Row.Cells[i].BorderStyle = BorderStyle.Solid;
                e.Row.Cells[i].BorderWidth = 1;
            }

        }

    }

    protected void grd_mtrSkmBlnd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Request.QueryString["noedit"] != null)
            {
                grd_mtrSkmBlnd.Columns[4].Visible = false;
                grd_mtrSkmBlnd.Columns[5].Visible = false;
                grd_mtrSkmBlnd.Columns[6].Visible = true;
                grd_mtrSkmBlnd.Columns[7].Visible = true;
                grd_mtrSkmBlnd.Columns[13].Visible = false;
            }

            if (DataBinder.Eval(e.Row.DataItem, "KontraktorKode").ToString() == "TOTAL")
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Transparent;

                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.Font.Bold = true;
                e.Row.Font.Size = 9;

                DropDownList ddl = (DropDownList)e.Row.FindControl("DropDownList1");
                ddl.Visible = false;

                TextBox txb = (TextBox)e.Row.FindControl("txt_trCount");
                txb.Visible = false;

                CheckBox chb = (CheckBox)e.Row.FindControl("cbRows");
                chb.Visible = false;

                Label lbl = (Label)e.Row.FindControl("lbl_trclama");
                lbl.Visible = true;

                e.Row.Cells[3].ForeColor = System.Drawing.Color.Transparent;

            }
        }
    }

    protected void grd_mtrSkmBlnd_K3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["noedit"] != null)
            {
                grd_mtrSkmBlnd_K3.Columns[4].Visible = false;
                grd_mtrSkmBlnd_K3.Columns[5].Visible = false;
                grd_mtrSkmBlnd_K3.Columns[6].Visible = true;
                grd_mtrSkmBlnd_K3.Columns[7].Visible = true;
                grd_mtrSkmBlnd_K3.Columns[13].Visible = false;
            }

            if (DataBinder.Eval(e.Row.DataItem, "KontraktorKode").ToString() == "TOTAL")
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Transparent;

                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.Font.Bold = true;
                e.Row.Font.Size = 9;

                DropDownList ddl = (DropDownList)e.Row.FindControl("ddl_mtrK3");
                ddl.Visible = false;

                TextBox txb = (TextBox)e.Row.FindControl("txt_trCountK3");
                txb.Visible = false;

                CheckBox chb = (CheckBox)e.Row.FindControl("cbRowsK3");
                chb.Visible = false;

                Label lbl = (Label)e.Row.FindControl("lbl_trcK3");
                lbl.Visible = true;

                e.Row.Cells[3].ForeColor = System.Drawing.Color.Transparent;

            }
        }
    }

    protected void grdStp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["noedit"] != null)
            {
                grdStp.Columns[4].Visible = false;
                grdStp.Columns[5].Visible = false;
                grdStp.Columns[6].Visible = true;
                grdStp.Columns[7].Visible = true;
                grdStp.Columns[13].Visible = false;
            }

            if (DataBinder.Eval(e.Row.DataItem, "KontraktorKode").ToString() == "TOTAL")
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Transparent;

                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.Font.Bold = true;
                e.Row.Font.Size = 9;

                DropDownList ddl = (DropDownList)e.Row.FindControl("ddl_mtrSt");
                ddl.Visible = false;

                TextBox txb = (TextBox)e.Row.FindControl("txt_trCountSt");
                txb.Visible = false;

                CheckBox chb = (CheckBox)e.Row.FindControl("CbRowsSp");
                chb.Visible = false;

                Label lbl = (Label)e.Row.FindControl("lbl_trcSp");
                lbl.Visible = true;

                e.Row.Cells[3].ForeColor = System.Drawing.Color.Transparent;

            }
        }
    }

    protected void ibt_update1_Click(object sender, ImageClickEventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            int i = 0;

            foreach (GridViewRow row in grd_mtrSkmBlnd.Rows)
            {
                DropDownList ddl1 = (DropDownList)row.FindControl("DropDownList1");
                Label lbl_mtrl = (Label)row.FindControl("lbl_mtLama");
                Label lbl_trc = (Label)row.FindControl("lbl_trcLama");
                Label lbl_idAc = (Label)row.FindControl("lbl_idAct");
                Label lbl_kntr = (Label)row.FindControl("lbl_kntr");
                TextBox txt_trCount = (TextBox)row.FindControl("txt_trCount");

                if (lbl_mtrl.Text.Trim().ToLower() != ddl1.SelectedValue.ToString().Trim().ToLower())
                {
                    int cek = cekMaterialKode(lbl_kntr.Text, ddl1.SelectedValue.ToString(), idp, "42");
                    //WeightBridge.csclass.mymsg(" cek ==> " + cek, "#");
                    if (cek == 1)
                    {
                        cmd.CommandText = "SP_Upd_Mtrl";
                        cmd.Parameters.AddWithValue("@idpl", idp.ToString());
                        cmd.Parameters.AddWithValue("@mtrl", ddl1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@ID_actMB", lbl_idAc.Text.ToString());
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        cmd.Parameters.Clear();

                        i += 1;
                    }
                    else
                    {
                        WeightBridge.csclass.mymsg(":: " + ddl1.SelectedValue.ToString() + " already used for " + lbl_kntr.Text + ", update cancelled.", "#");
                    }
                }
                else if (txt_trCount.Text.Trim().ToLower() != lbl_trc.Text.Trim().ToLower())
                {
                    cmd.CommandText = "SP_Upd_trCount";
                    cmd.Parameters.AddWithValue("@idpl", idp.ToString());
                    cmd.Parameters.AddWithValue("@trCount", int.Parse(txt_trCount.Text));
                    cmd.Parameters.AddWithValue("@ID_actMB", lbl_idAc.Text.ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();

                    i += 1;
                }
            }

            if (i > 0)
            {
                WeightBridge.csclass.mymsg("Data updated", "EditSMB.aspx?idp=" + idp);
                grd_mtrSkmBlnd.DataBind();
            }

        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");
        }
    }

    protected void ibt_delete1_Click(object sender, ImageClickEventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (GridViewRow row in grd_mtrSkmBlnd.Rows)
            {
                Label lbl_idAc = (Label)row.FindControl("lbl_idAct");
                CheckBox cbRows = (CheckBox)row.FindControl("cbRows");

                if (cbRows.Checked == true)
                {
                    //WeightBridge.csclass.mymsg(":: checked ==> " + lbl_idAc.Text, "#");
                    cmd.CommandText = "SP_del_SkmBlnd";
                    cmd.Parameters.Add("@idpl", System.Data.DbType.Int16).Value = idp;
                    cmd.Parameters.Add("@ID_actMB", System.Data.DbType.String).Value = lbl_idAc.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();
                }
            }
            WeightBridge.csclass.mymsg("Delete succesfull", "EditSMB.aspx?idp=" + idp);
            grd_mtrSkmBlnd.DataBind();
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 100), "#");
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox objChkAll = (CheckBox)grd_mtrSkmBlnd.HeaderRow.FindControl("chkSelectAll");
        if (objChkAll.Checked)
        {
            foreach (GridViewRow objGVR in grd_mtrSkmBlnd.Rows)
            {
                CheckBox objChkIndividual = (CheckBox)objGVR.FindControl("cbRows");
                objChkIndividual.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow objGVR in grd_mtrSkmBlnd.Rows)
            {
                CheckBox objChkIndividual = (CheckBox)objGVR.FindControl("cbRows");
                objChkIndividual.Checked = false;
            }
        }
    }

    protected void ibt_savek3_Click(object sender, ImageClickEventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            int i = 0;

            foreach (GridViewRow row in grd_mtrSkmBlnd_K3.Rows)
            {
                DropDownList ddl1 = (DropDownList)row.FindControl("ddl_mtrK3");
                Label lbl_mtrl = (Label)row.FindControl("lbl_mtLamaK3");
                Label lbl_trc = (Label)row.FindControl("lbl_trcK3");
                Label lbl_idAc = (Label)row.FindControl("lbl_idActK3");
                Label lbl_kntr = (Label)row.FindControl("lbl_kntrK3");
                TextBox txt_trCount = (TextBox)row.FindControl("txt_trCountK3");

                if (lbl_mtrl.Text.Trim().ToLower() != ddl1.SelectedValue.ToString().Trim().ToLower())
                {
                    int cek = cekMaterialKode(lbl_kntr.Text, ddl1.SelectedValue.ToString(), idp, "43");
                    //WeightBridge.csclass.mymsg(" cek ==> " + cek, "#");
                    if (cek == 1)
                    {
                        cmd.CommandText = "SP_Upd_Mtrl";
                        cmd.Parameters.AddWithValue("@idpl", idp.ToString());
                        cmd.Parameters.AddWithValue("@mtrl", ddl1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@ID_actMB", lbl_idAc.Text.ToString());
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        cmd.Parameters.Clear();

                        i += 1;
                    }
                    else
                    {
                        WeightBridge.csclass.mymsg(":: " + ddl1.SelectedValue.ToString() + " already used for " + lbl_kntr.Text + ", update cancelled.", "#");
                    }
                }
                else if (txt_trCount.Text.Trim().ToLower() != lbl_trc.Text.Trim().ToLower())
                {
                    cmd.CommandText = "SP_Upd_trCount";
                    cmd.Parameters.AddWithValue("@idpl", idp.ToString());
                    cmd.Parameters.AddWithValue("@trCount", int.Parse(txt_trCount.Text));
                    cmd.Parameters.AddWithValue("@ID_actMB", lbl_idAc.Text.ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();

                    i += 1;
                }
            }

            if (i > 0)
            {
                WeightBridge.csclass.mymsg("Data updated", "EditSMB.aspx?idp=" + idp);
                grd_mtrSkmBlnd.DataBind();
            }

        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");
        }
    }

    protected void ibt_delK3_Click(object sender, ImageClickEventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (GridViewRow row in grd_mtrSkmBlnd_K3.Rows)
            {
                Label lbl_idAc = (Label)row.FindControl("lbl_idActK3");
                CheckBox cbRows = (CheckBox)row.FindControl("cbRowsK3");

                if (cbRows.Checked == true)
                {
                    //WeightBridge.csclass.mymsg(":: checked ==> " + lbl_idAc.Text, "#");
                    cmd.CommandText = "SP_del_SkmBlnd";
                    cmd.Parameters.Add("@idpl", System.Data.DbType.Int16).Value = idp;
                    cmd.Parameters.Add("@ID_actMB", System.Data.DbType.String).Value = lbl_idAc.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();
                }
            }
            WeightBridge.csclass.mymsg("Delete succesfull", "EditSMB.aspx?idp=" + idp);
            grd_mtrSkmBlnd.DataBind();
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 100), "#");
        }
    }

    protected void CBSelectAllK3_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox objChkAll = (CheckBox)grd_mtrSkmBlnd_K3.HeaderRow.FindControl("CBSelectAllK3");
        if (objChkAll.Checked)
        {
            foreach (GridViewRow objGVR in grd_mtrSkmBlnd_K3.Rows)
            {
                CheckBox objChkIndividual = (CheckBox)objGVR.FindControl("cbRowsK3");
                objChkIndividual.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow objGVR in grd_mtrSkmBlnd_K3.Rows)
            {
                CheckBox objChkIndividual = (CheckBox)objGVR.FindControl("cbRowsK3");
                objChkIndividual.Checked = false;
            }
        }
    }


    protected void ibt_saveSp_Click(object sender, ImageClickEventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            int i = 0;

            foreach (GridViewRow row in grdStp.Rows)
            {
                DropDownList ddl1 = (DropDownList)row.FindControl("ddl_mtrSt");
                Label lbl_mtrl = (Label)row.FindControl("lbl_mtLamaSt");
                Label lbl_trc = (Label)row.FindControl("lbl_trcSp");
                Label lbl_idAc = (Label)row.FindControl("lbl_idActSt");
                Label lbl_kntr = (Label)row.FindControl("lbl_kntrSt");
                TextBox txt_trCount = (TextBox)row.FindControl("txt_trCountSt");

                if (lbl_mtrl.Text.Trim().ToLower() != ddl1.SelectedValue.ToString().Trim().ToLower())
                {
                    int cek = cekMaterialKode(lbl_kntr.Text, ddl1.SelectedValue.ToString(), idp, "44");
                    //WeightBridge.csclass.mymsg(" cek ==> " + cek, "#");
                    if (cek == 1)
                    {
                        cmd.CommandText = "SP_Upd_Mtrl";
                        cmd.Parameters.AddWithValue("@idpl", idp.ToString());
                        cmd.Parameters.AddWithValue("@mtrl", ddl1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@ID_actMB", lbl_idAc.Text.ToString());
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        cmd.Parameters.Clear();

                        i += 1;
                    }
                    else
                    {
                        WeightBridge.csclass.mymsg(":: " + ddl1.SelectedValue.ToString() + " already used for " + lbl_kntr.Text + ", update cancelled.", "#");
                    }
                }
                else if (txt_trCount.Text.Trim().ToLower() != lbl_trc.Text.Trim().ToLower())
                {
                    cmd.CommandText = "SP_Upd_trCount";
                    cmd.Parameters.AddWithValue("@idpl", idp.ToString());
                    cmd.Parameters.AddWithValue("@trCount", int.Parse(txt_trCount.Text));
                    cmd.Parameters.AddWithValue("@ID_actMB", lbl_idAc.Text.ToString());
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();

                    i += 1;
                }
            }

            if (i > 0)
            {
                WeightBridge.csclass.mymsg("Data updated", "EditSMB.aspx?idp=" + idp);
                grd_mtrSkmBlnd.DataBind();
            }

        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");
        }
    }
    protected void ibt_delSp_Click(object sender, ImageClickEventArgs e)
    {
        string idp = Request.QueryString["idp"].ToString();

        try
        {
            String connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (GridViewRow row in grdStp.Rows)
            {
                Label lbl_idAc = (Label)row.FindControl("lbl_idActSt");
                CheckBox cbRows = (CheckBox)row.FindControl("CbRowsSp");

                if (cbRows.Checked == true)
                {
                    //WeightBridge.csclass.mymsg(":: checked ==> " + lbl_idAc.Text, "#");
                    cmd.CommandText = "SP_del_SkmBlnd";
                    cmd.Parameters.Add("@idpl", System.Data.DbType.Int16).Value = idp;
                    cmd.Parameters.Add("@ID_actMB", System.Data.DbType.String).Value = lbl_idAc.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Parameters.Clear();
                }
            }
            WeightBridge.csclass.mymsg("Delete succesfull", "EditSMB.aspx?idp=" + idp);
            grd_mtrSkmBlnd.DataBind();
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");
        }
    }

    protected void CbSelectAllSp_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox objChkAll = (CheckBox)grdStp.HeaderRow.FindControl("CbSelectAllSp");
        if (objChkAll.Checked)
        {
            foreach (GridViewRow objGVR in grdStp.Rows)
            {
                CheckBox objChkIndividual = (CheckBox)objGVR.FindControl("CbRowsSp");
                objChkIndividual.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow objGVR in grdStp.Rows)
            {
                CheckBox objChkIndividual = (CheckBox)objGVR.FindControl("CbRowsSp");
                objChkIndividual.Checked = false;
            }
        }
    }
    protected void template_Click(object sender, ImageClickEventArgs e)
    {
        WeightBridge.csclass.dtemplt("blendScheme_upload.xlsx", Page);
    }

    protected void ibt_xls1_Click(object sender, ImageClickEventArgs e)
    {
        grd_mtrSkmBlnd.Columns[4].Visible = false;
        grd_mtrSkmBlnd.Columns[5].Visible = false;
        grd_mtrSkmBlnd.Columns[6].Visible = true;
        grd_mtrSkmBlnd.Columns[7].Visible = true;
        grd_mtrSkmBlnd.Columns[13].Visible = false;

        WeightBridge.ExportFile.xlsport(grd_mtrSkmBlnd, "BlendSchemeK1_", this);
    }

    protected void ibt_pdf1_Click(object sender, ImageClickEventArgs e)
    {
        grd_mtrSkmBlnd.Columns[4].Visible = false;
        grd_mtrSkmBlnd.Columns[5].Visible = false;
        grd_mtrSkmBlnd.Columns[6].Visible = true;
        grd_mtrSkmBlnd.Columns[7].Visible = true;
        grd_mtrSkmBlnd.Columns[13].Visible = false;


        WeightBridge.ExportFile.pdfport(grd_mtrSkmBlnd, "BlendSchemeK1_", 1, this);

    }

    protected void ibt_xls2_Click(object sender, ImageClickEventArgs e)
    {
        grd_mtrSkmBlnd_K3.Columns[4].Visible = false;
        grd_mtrSkmBlnd_K3.Columns[5].Visible = false;
        grd_mtrSkmBlnd_K3.Columns[6].Visible = true;
        grd_mtrSkmBlnd_K3.Columns[7].Visible = true;
        grd_mtrSkmBlnd_K3.Columns[13].Visible = false;

        WeightBridge.ExportFile.xlsport(grd_mtrSkmBlnd_K3, "BlendSchemeK3_", this);
    }

    protected void ibt_pdf2_Click(object sender, ImageClickEventArgs e)
    {
        grd_mtrSkmBlnd_K3.Columns[4].Visible = false;
        grd_mtrSkmBlnd_K3.Columns[5].Visible = false;
        grd_mtrSkmBlnd_K3.Columns[6].Visible = true;
        grd_mtrSkmBlnd_K3.Columns[7].Visible = true;
        grd_mtrSkmBlnd_K3.Columns[13].Visible = false;

        WeightBridge.ExportFile.pdfport(grd_mtrSkmBlnd_K3, "BlendSchemeK3_", 1, this);
    }

    protected void ibt_xls3_Click(object sender, ImageClickEventArgs e)
    {
        grdStp.Columns[4].Visible = false;
        grdStp.Columns[5].Visible = false;
        grdStp.Columns[6].Visible = true;
        grdStp.Columns[7].Visible = true;
        grdStp.Columns[13].Visible = false;

        WeightBridge.ExportFile.xlsport(grdStp, "BlendSchemeStockpile_", this);
    }

    protected void ibt_pdf3_Click(object sender, ImageClickEventArgs e)
    {
        grdStp.Columns[4].Visible = false;
        grdStp.Columns[5].Visible = false;
        grdStp.Columns[6].Visible = true;
        grdStp.Columns[7].Visible = true;
        grdStp.Columns[13].Visible = false;

        WeightBridge.ExportFile.pdfport(grdStp, "BlendSchemeStockpile_", 1, this);
    }

    protected void grid_sumtruck1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = 60;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        }

    }
    protected void grid_ratiok1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Width = 40;
            e.Row.Cells[1].Width = 45;
            e.Row.Cells[2].Width = 40;

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (DataBinder.Eval(e.Row.DataItem, "Product").ToString())
            {
                case "E5000":
                    e.Row.Cells[0].BackColor = System.Drawing.Color.FromArgb(153, 204, 0);
                    break;
                case "E4900":
                    e.Row.Cells[0].BackColor = System.Drawing.Color.FromArgb(255, 204, 0);
                    break;
                case "E4000":
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Tan;
                    break;
                case "TOTAL":
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    break;
            }
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Text = String.Concat((((DataRowView)e.Row.DataItem)["Ratio"]).ToString(), "%");
        }
    }
    protected void grid_ratiok3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Width = 40;
            e.Row.Cells[1].Width = 45;
            e.Row.Cells[2].Width = 40;

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (DataBinder.Eval(e.Row.DataItem, "Product").ToString())
            {
                case "E5000":
                    e.Row.Cells[0].BackColor = System.Drawing.Color.FromArgb(153, 204, 0);
                    break;
                case "E4900":
                    e.Row.Cells[0].BackColor = System.Drawing.Color.FromArgb(255, 204, 0);
                    break;
                case "E4000":
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Tan;
                    break;
                case "TOTAL":
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    break;
            }
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Text = String.Concat((((DataRowView)e.Row.DataItem)["Ratio"]).ToString(), "%");
        }
    }

    protected void grd_mtrSkmBlnd_DataBound(object sender, EventArgs e)
    {
        WeightBridge.csclass.mergeOnDatabound(grd_mtrSkmBlnd, 2, 2);
    }
    protected void grd_mtrSkmBlnd_K3_DataBound(object sender, EventArgs e)
    {
        WeightBridge.csclass.mergeOnDatabound(grd_mtrSkmBlnd_K3, 2, 2);

    }
    protected void grdStp_DataBound(object sender, EventArgs e)
    {
        WeightBridge.csclass.mergeOnDatabound(grdStp, 2, 2);
    }
}