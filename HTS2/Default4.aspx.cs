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

public partial class HTS2_Default4 : System.Web.UI.Page
{

    #region "Voids and Functions"

    protected bool CheckContainrDuplicate(string dtm)
    {
        SqlConnection.ClearAllPools();
        string ConnectionString = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
        string SqlString = "SELECT TOP 1 id_qualplan_containr FROM tbl_qualplan_containr WHERE (PlanTypeKode=13) AND ('" + dtm + "' BETWEEN plan_startdate AND plan_enddate)";
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SqlString, conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        conn.Close();
                        return false;
                    }
                    else
                    {
                        conn.Close();
                        return true;
                    }
                }

            }
        }
    }

     protected int CheckPTRDuplicate(string idctr, string mat, string kon)
    {
        SqlConnection.ClearAllPools();
        string ConnectionString = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
        string SqlString = "SELECT TOP 1 id_prtprojection FROM dbo.tbl_qual_ptrprojection WHERE (KontraktorKode='"+ kon +"') AND (MaterialKode = '" + mat + "') AND (id_qualplan_containr = "+ idctr +")";
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SqlString, conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        conn.Close();
                        return 0;
                    }
                    else
                    {
                        conn.Close();
                        return 1;
                    }
                }

            }
        }
    }

      protected void insertPtrProjection(string idctr, string area, string kon, string mat, string avb, string tm, string im, string ash, string ts, string cdaf, string cadb, string hgi, string idprod)
        {
            SqlConnection.ClearAllPools();
            string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = "SELECT TOP 1 productKode FROM dbo.tbl_qualportal Where MaterialKode = '" + mat + "' ORDER BY dtm_qualportal DESC";
            conn.Open();
            //_id = cmd.ExecuteScalar().ToString();
            conn.Close();
            conn = null;

           // return _id;
        }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        switch (Request.QueryString["v"])
        {
            case "0":
                txb_sched.Text = DateTime.Today.ToString("MM/dd/yyyy");
                break;

            default:
                txb_sched.Text = DateTime.Today.ToString("MM/dd/yyyy");
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "PTR Summary"
    protected void btn_submit_containr_Click(object sender, EventArgs e)
    {
        try
        {
            string tgl = txb_sched.Text;
            DateTime dt = Convert.ToDateTime(tgl);
            bool ck = WeightBridge.CheckParam.isDate(dt.ToString());
            bool dup = CheckContainrDuplicate(dt.ToString());

            if (ck == false || dup == false)
            {
                WeightBridge.csclass.errmsg(":: projection period already exist");
            }
            else
            {
                SqlConnection.ClearAllPools();
                string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
                SqlConnection conn = new SqlConnection(connect);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "_sp_ql_container_add1";
                cmd.Parameters.AddWithValue("@PlanTypeKode", 13);
                cmd.Parameters.AddWithValue("@plan_startdate", dt);
                cmd.Parameters.AddWithValue("@plan_enddate", dt.AddDays(6));
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                //cmd.ExecuteScalar()
                cmd.ExecuteNonQuery();
                conn.Close();
                ddl_wpl1.DataBind();

                WeightBridge.csclass.mymsg("New Projection Plan Period Inserted", "#");
            }
        }
        catch
        {
            WeightBridge.csclass.errmsg(":: bad date format");

        }
    }

    protected void ibt_upload_ptr1_Click(object sender, EventArgs e)
    {
        ibt_upload_ptr1.Enabled = false;
        string id_qualplan_containr = ddl_wpl1.SelectedValue.ToString();
        
        FileUpload PictureUpload = FileUpload1;
        string fileloc = ConfigurationManager.AppSettings["fileloc"].ToString();

        try
        {
             if (PictureUpload.HasFile)
            {
                if (string.Compare(System.IO.Path.GetExtension(PictureUpload.FileName), ".xlsx", true) != 0)
                {
                    ibt_upload_ptr1.Enabled = true;
                    WeightBridge.csclass.errmsg(":: Only MS-Excel ver. 2007 or above (.xlsx) files are accepted");                    
                    return;
                }
            }

            string fileNameWoExt = System.IO.Path.GetFileNameWithoutExtension(PictureUpload.FileName);
            string filename = fileNameWoExt.Substring(0, 3);
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
                WeightBridge.csclass.delFile(picPath);

                 try
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            string area = ds.Tables[0].Rows[i][0].ToString();
                            int cekvalue = WeightBridge.CheckParam.isArea(area);
                            if (cekvalue == 0)
                            {
                                ibt_upload_ptr1.Enabled = true;
                                WeightBridge.csclass.errmsg(":: " + area + " (line "+ i + 1 +") is unknown, upload canceled");
                                oconn = null;
                                return;
                            }
                        }
                    }
                    catch
                    {
                        ibt_upload_ptr1.Enabled = true;
                        WeightBridge.csclass.errmsg(":: error on checking area");
                        return;
                    }
   
                try
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            string kon = ds.Tables[0].Rows[i][1].ToString();
                            int cekvalue = WeightBridge.CheckParam.Contractor(kon);
                            if (cekvalue == 0)
                            {
                                ibt_upload_ptr1.Enabled = true;
                                WeightBridge.csclass.errmsg(":: " + kon + "  (line "+ i + 1 +") is unknown, upload canceled");
                                oconn = null;
                                return;
                            }
                        }
                    }
                    catch
                    {
                        ibt_upload_ptr1.Enabled = true;
                        WeightBridge.csclass.errmsg(":: error on checking company");
                        return;
                    }

                    try
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            string mat = ds.Tables[0].Rows[i][2].ToString();
                            int cekvalue = WeightBridge.CheckParam.Material(mat);
                            if (cekvalue == 0)
                            {
                                ibt_upload_ptr1.Enabled = true;
                                WeightBridge.csclass.errmsg(":: " + mat + "  (line "+ i + 1 +") is unknown, upload canceled");
                                oconn = null;
                                return;
                            }
                        }
                    }
                    catch
                    {
                        ibt_upload_ptr1.Enabled = true;
                        WeightBridge.csclass.errmsg(":: error on checking material");
                        return;
                    }

                    try
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            string kon = ds.Tables[0].Rows[i][1].ToString();
                            string mat = ds.Tables[0].Rows[i][2].ToString();
                            int cekvalue = CheckPTRDuplicate(id_qualplan_containr, mat, kon);
                            if (cekvalue == 0)
                            {
                                ibt_upload_ptr1.Enabled = true;
                                WeightBridge.csclass.errmsg(":: Duplicate data detected, use edit. Upload canceled");
                                oconn = null;
                                return;
                            }
                        }
                    }
                    catch
                    {
                        ibt_upload_ptr1.Enabled = true;
                        WeightBridge.csclass.errmsg(":: error on duplicate checking");
                        return;
                    }

                    try
                    {
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            string area = ds.Tables[0].Rows[i][0].ToString();                            
                            string kon = ds.Tables[0].Rows[i][1].ToString();
                            string mat = ds.Tables[0].Rows[i][2].ToString();
                            string tm = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][3].ToString()).ToString();
                            string im = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][4].ToString()).ToString();
                            string ash = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][5].ToString()).ToString();
                            string ts = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][6].ToString()).ToString();
                            string cdaf = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][7].ToString()).ToString();
                            string cadb = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][8].ToString()).ToString();
                            string hgi = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][9].ToString()).ToString();
                            string avb = WeightBridge.CheckParam.isFloat(ds.Tables[0].Rows[i][10].ToString()).ToString();
                            string idprod = WeightBridge.CheckParam.GetIdProd(mat);

                            int cekvalue = CheckPTRDuplicate(id_qualplan_containr, mat, kon);
                            if (cekvalue == 1)
                            {
                                //insertWeepro(KontraktorKode, MaterialKode, KontraktorCap, ROMCap, deviatCap, blendNeed, dayminington, dayhaulton, TM, IM,
                                //ASH_ADB, ASH_AR, TS_ADB, TS_AR, CV_AR, id_qualplan_containr, Rom, pit_pr, rom_pr, HGI,
                                //CalAdb, CalDaf, idprod);
                                //    'WeightBridge.csclass.mymsg("" & id_qualplan_containr, "#")    
                            }

                        }
                        WeightBridge.csclass.mymsg("Weekly Projection Data Upload Complete", "Default4?v=0");
                    }
                    catch
                    {
                        ibt_upload_ptr1.Enabled = true;
                        WeightBridge.csclass.errmsg(":: error on inserting data");
                        return;
                    }

                }
                catch (SqlException eq)
                {
                    ibt_upload_ptr1.Enabled = true;
                    WeightBridge.csclass.errmsg("::" + eq.ToString().Substring(0,50));
                    return;

                }
                catch (DataException ee)
                {
                    ibt_upload_ptr1.Enabled = true;
                    WeightBridge.csclass.errmsg("::" + ee.ToString().Substring(0,50));
                    return;

                }
        }
        catch
        {
            ibt_upload_ptr1.Enabled = true;
            WeightBridge.csclass.mymsg(":: unknown error", "#");

        }
        ibt_upload_ptr1.Enabled = true;

    }


    #endregion

    protected void cbSelallLow1_CheckedChanged(object sender, EventArgs e)
    {

    }
    
}