using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for CheckParameter
/// </summary>
namespace WeightBridge
{
  
    public static class CheckParam
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["HTSdbConn"].ConnectionString; }
        } 


        public static int Contractor(string kon)
        {
            SqlConnection.ClearAllPools();
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

        public static int Material(string mat)
        {
            SqlConnection.ClearAllPools();
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

        public static int ROM(string rom)
        {
            SqlConnection.ClearAllPools();
            String connect = ConnectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 Kode FROM v_Sources WHERE kode = '" + rom + "'", conn))
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

        public static string GetShift(string dtm)
        {
            try
            {
                bool cdtm = isDate(dtm);
                DateTime _dtm;
                _dtm = DateTime.Parse(dtm);

                if (_dtm.Hour >= 0 && _dtm.Hour < 15)
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch
            {
                return "1";
            }
        }

        public static bool isDate(string dtm)
        {
            try
            {
                if (dtm != null)
                {
                    DateTime dt = DateTime.Parse(dtm);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public static double isFloat(string strvalue)
        {
            try
            {
                if (!string.IsNullOrEmpty(strvalue))
                {
                    double _val = 0;
                    _val = double.Parse(strvalue);

                    return _val;
                }
                else
                {
                    return 0;
                }
            }
            catch 
            {
                return 0;
            }
        }


        public static string GetIdProd(string mat)
        {
            string _id = null;

            SqlConnection.ClearAllPools();
            string connect = ConnectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = "SELECT TOP 1 productKode FROM dbo.tbl_qualportal Where MaterialKode = '" + mat + "' ORDER BY dtm_qualportal DESC";
            conn.Open();
            _id = cmd.ExecuteScalar().ToString();
            conn.Close();
            conn = null;

            return _id;
        }


        public static int isProduct(string prod)
        {
            SqlConnection.ClearAllPools();
            string connect = ConnectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select top 1 id_ref_master from tbl_ref_master where ref_name = '" + prod + "'", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Close();
                            return 1;
                        }
                        else
                        {
                            dr.Close();
                            return 0;
                        }                        
                    }
                }
                conn.Close();
            }
            connect = null;
        }

        public static int isArea(string area)
        {
            SqlConnection.ClearAllPools();
            string connect = ConnectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select top 1 id_ref_master from tbl_ref_master where ref_name = '" + area + "'", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Close();
                            return 1;
                        }
                        else
                        {
                            dr.Close();
                            return 0;
                        }
                    }
                }
                conn.Close();
            }
            connect = null;
        }

        


    }

    
}