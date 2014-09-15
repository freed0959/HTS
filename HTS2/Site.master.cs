using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HTS2_Site : System.Web.UI.MasterPage
{
    public static String apptitle
    {
        get { return ConfigurationManager.AppSettings["apptitle"].ToString(); }
    }

    public static String uswitch
    {
        get { return ConfigurationManager.AppSettings["uswitch"].ToString(); }
    }

    public static String appswitchtext
    {
        get { return ConfigurationManager.AppSettings["appswitchtext"].ToString(); }
    }

    public static String appswitchurl
    {
        get { return ConfigurationManager.AppSettings["appswitchurl"].ToString(); }
    }

    public static String ConnectionString
    {
        get { return ConfigurationManager.ConnectionStrings["HTSdbConn"].ConnectionString; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        AppTitle1Lit.Text = apptitle;
        appswitchHyp.Text = appswitchtext;
        appswitchHyp.NavigateUrl = appswitchurl;
        //uswitch1Hyp.NavigateUrl = uswitch;

        string filename = System.IO.Path.GetFileName(Request.Path);
        filename = filename.Trim().ToLower();     

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["htspub"] != null) {
             String usrnm = Request.Cookies["htspub"]["nameusr"].ToString();
             String grpid = Request.Cookies["htspub"]["idgroup"].ToString();
             String grpnm= Request.Cookies["htspub"]["namegroup"].ToString();
             int usrid = int.Parse(Request.Cookies["htspub"]["idusr"].ToString());

            lbl_user1.Text = usrnm;
            lbl_grup1.Text = String.Concat(grpnm, " (", grpid, ")");

            if (usrid == 1 || usrid == 2 || usrid == 3 || usrid == 4)
            {
                pan_su1.Visible = true;
                adm_Panel1.Visible = true;

                switch (grpid)
                {
                    case "14":
                        pan_pasmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                       
                        break;

                    case "16":
                        pan_pasmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                       
                        break;

                    case "17":
                        pan_pasmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                        
                        break;

                    case "38":
                        pan_conmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                        
                        break;

                    case "2":
                        pan_pubmenu1.Visible = true;          
                        break;

                    default:
                        pan_pubmenu1.Visible = true;                        
                        break;
                }
            }
            else {

                adm_Panel1.Visible = false;
                pan_su1.Visible = false;

               switch (grpid)
                {
                    case "14":                    
                        pan_pasmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                        
                        break;

                    case "16":
                        pan_pasmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                        
                        break;

                    case "17":
                        pan_pasmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                        
                        break;

                    case "38":
                        pan_conmenu1.Visible = true;
                        pan_pubmenu1.Visible = false;                        
                        break;

                    case "2":                    
                        pan_pubmenu1.Visible = true;                                                     
                        break;

                    default:
                        pan_pubmenu1.Visible = true;                        
                        break;
                }
            }
        }else{
            Response.Redirect("../usrLog.aspx");
        }

        appswitchHyp.NavigateUrl = appswitchurl;
        
        string filename = System.IO.Path.GetFileName(Request.Path);
        appswitchHyp.NavigateUrl = appswitchHyp.NavigateUrl+filename;
    }

    protected void UpdateCompany(string kon, string usr)
    {
        try
        {
            String connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = " UPDATE tbl_sys_user SET id_company = @kon " +
                              " WHERE id_user = @usr";
            cmd.Parameters.AddWithValue("@kon", kon);
            cmd.Parameters.AddWithValue("@usr", usr);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Parameters.Clear();
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");
        }
    }

    protected void UpdateGroupLevel(string lvl, string usr)
    {
        try
        {
            String connect = ConfigurationManager.ConnectionStrings["HTSdbConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = " UPDATE [tbl_sys_group_member] SET id_user_group = @lvl " +
                              " WHERE id_user = @usr";
            cmd.Parameters.AddWithValue("@lvl", lvl);
            cmd.Parameters.AddWithValue("@usr", usr);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Parameters.Clear();
        }
        catch (Exception ex)
        {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");
        }
    }

    protected void link_save_Click(object sender, EventArgs e)
    {
        try
        {
            string idusr;
            string kon = ddl_comp1.SelectedValue.ToString();
            string lvl = ddl_grup1.SelectedValue.ToString();

            if (Request.Cookies["htspub"]["idusr"] != null)
            {
                idusr = Request.Cookies["htspub"]["idusr"].ToString();
                
                UpdateCompany(kon, idusr);
                UpdateGroupLevel(lvl, idusr);

                Response.Write(@"<script type='text/javascript'>window.open('" + uswitch + "');window.close();</script>");             
            }
        }
        catch (Exception ex) {
            WeightBridge.csclass.mymsg(":: error " + ex.ToString().Substring(0, 50), "#");        
        }
    }
}
