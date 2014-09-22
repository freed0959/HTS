using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HTS2_Default3 : System.Web.UI.Page
{

    #region "function and subs"

    public string getwpdate(string idctr)
    {
        string a;

        SqlConnection.ClearAllPools();
        string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
        SqlConnection conn = new SqlConnection(connect);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT CONVERT(nvarchar, plan_startdate, 106) + ' s/d ' + " +
                        " CONVERT(nvarchar, plan_enddate, 106) FROM dbo.tbl_qualplan_containr WHERE id_qualplan_containr = @idctr ";
        cmd.Parameters.AddWithValue("@idctr", idctr);

        cmd.CommandType = CommandType.Text;
        conn.Open();
        a = cmd.ExecuteScalar().ToString();
        //cmd.ExecuteNonQuery();
        conn.Close();

        return a;
    }

    public void ann(string idp)
    {
        try
        {
            if (!string.IsNullOrEmpty(idp))
            {
                SqlConnection.ClearAllPools();
                string connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ToString();
                SqlConnection conn = new SqlConnection(connect);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "_sp_ql_uwpnot3";
                cmd.Parameters.AddWithValue("@idp", System.Data.DbType.Int64).Value = Int64.Parse(idp);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                WeightBridge.csclass.mymsg(":: failed to send email", "#");
            }
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString(), "#");

        }

    }

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        switch (Request.QueryString["v"])
        {
            case "0":
                Page.Title = "Weekly Projection RTK ::" + Request.QueryString["idwpr1"].ToString() + " Data Edit";
                MultiView1.ActiveViewIndex = 0;
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "weekly projection rtk details"
    protected void dtv_plan1_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        if (e.NewMode == DetailsViewMode.Edit)
        {
            if (e.CancelingEdit == true)
            {
                //window.close();
                string scrpt = "window.close();";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", scrpt, true);
            }
        }


    }
    protected void ds_dtView_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        //alert then close then update parent();
        string scrpt;        
                
        if (chb_ann1.Checked == true)
        {
            ann(Request.QueryString["idwpr1"].ToString());
            scrpt = "alert('Changes has been announced');window.opener.location.reload(true);window.close();";           
        }
        else
        {
            scrpt = "alert('Data updated');window.opener.location.reload(true);window.close();";            
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "close", scrpt, true);

    }

    protected void dtv_plan1_DataBound(object sender, EventArgs e)
    {
        if (dtv_plan1.CurrentMode == DetailsViewMode.Edit)
        {
            Label lb = (Label)dtv_plan1.FindControl("lbl_tgl1");
            if (lb != null)
            {
                lb.Text = getwpdate(Request.QueryString["idctr"].ToString());
            }
        }
    }

    #endregion
    
}