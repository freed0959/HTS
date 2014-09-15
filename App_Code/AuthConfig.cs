using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


namespace WeightBridge
{
    public static class AuthConfig
    {
        public static void RegisterOpenAuth()
        {
            // See http://go.microsoft.com/fwlink/?LinkId=252803 for details on setting up this ASP.NET
            // application to support logging in via external services.

            //OpenAuth.AuthenticationClients.AddTwitter(
            //    consumerKey: "your Twitter consumer key",
            //    consumerSecret: "your Twitter consumer secret");

            //OpenAuth.AuthenticationClients.AddFacebook(
            //    appId: "your Facebook app id",
            //    appSecret: "your Facebook app secret");

            //OpenAuth.AuthenticationClients.AddMicrosoft(
            //    clientId: "your Microsoft account client id",
            //    clientSecret: "your Microsoft account client secret");

            //OpenAuth.AuthenticationClients.AddGoogle();
        }
    }

    public static class csclass
    {

        public static void gridstrip(GridView grd) 
        { 
            grd.AllowPaging = false;
            grd.AllowSorting = false;

            grd.Font.Size = 8;
            grd.Font.Name = "Arial";
            grd.Font.Bold = false;
            grd.GridLines = GridLines.Both;
            grd.RowStyle.ForeColor = Color.Black;
            grd.Columns[0].HeaderStyle.Width = 55;

            grd.HeaderStyle.Height = 35;
            grd.HeaderStyle.BackColor = Color.LightGray;
            grd.HeaderStyle.ForeColor = Color.Black;
            grd.HeaderStyle.BorderStyle = BorderStyle.NotSet;

            grd.RowStyle.BackColor = Color.Transparent;
            grd.RowStyle.ForeColor = Color.Black;

            grd.FooterStyle.Height = 23;
            grd.FooterStyle.BackColor = Color.LightGray;
            grd.FooterStyle.ForeColor = Color.Black;
            grd.FooterStyle.BorderStyle = BorderStyle.NotSet;

            grd.AlternatingRowStyle.BackColor = Color.Transparent;
            grd.AlternatingRowStyle.ForeColor = Color.Black;
            grd.SelectedRowStyle.BackColor = Color.Transparent;
            grd.SelectedRowStyle.ForeColor = Color.Black;
            grd.SortedAscendingCellStyle.BackColor = Color.Transparent;
            grd.SortedAscendingCellStyle.ForeColor = Color.Black;
            grd.SortedAscendingHeaderStyle.BackColor = Color.Transparent;
            grd.SortedAscendingHeaderStyle.ForeColor = Color.Black;
            grd.SortedDescendingCellStyle.BackColor = Color.Transparent;
            grd.SortedDescendingCellStyle.ForeColor = Color.Black;
            grd.SortedDescendingHeaderStyle.BackColor = Color.Transparent;
            grd.SortedDescendingHeaderStyle.ForeColor = Color.Black;
        }

        public static void mymsg(string strMessage, string nuloc)
        {
            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');location.href='{1}'", strMessage, nuloc);

            // Only show the alert if it's not already added to the 
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
            }
        }

        public static void dtemplt(string tname, Page pg)
        { 
             string _fName = "E:\\Websites\\pts_online\\storage\\"+ tname;
             FileInfo fi = new FileInfo(_fName);
             long sz = fi.Length;

              pg.Response.ClearContent();
              pg.Response.ContentType = "application/octet-stream";
              pg.Response.AddHeader("Content-Disposition", String.Format("attachment; filename = {0}", System.IO.Path.GetFileName(_fName)));
              pg.Response.AddHeader("Content-Length", sz.ToString("F0"));
              pg.Response.TransmitFile(_fName);
              pg.Response.End();
        }

        public static void addHead(int cspan, string htx, GridView grd)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell cell = new TableCell();
            cell.ColumnSpan = cspan;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.Height = 40;
            cell.Text = htx;
            row.Cells.Add(cell);

            grd.Controls[0].Controls.AddAt(0, row);
        }

        public static void changePz(GridView grd,int amt)
        {
            // ds.SelectCommand = sq1
            grd.PageSize = amt;
            grd.DataBind();
        }

        public static void xlsport(GridView grd, string _fnm, Page p)
        {
            grd.Visible = true;
            csclass.gridstrip(grd);    
            grd.DataBind();

           

            HtmlForm form = new HtmlForm();
            string attachment = "attachment; filename=" + _fnm + csclass.ranfn() + ".xls";

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            p.EnableViewState = false;
            HttpContext.Current.Response.Charset = string.Empty;

            System.IO.StringWriter stw = new System.IO.StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);

            form.Controls.Add(grd);
            p.Controls.Add(form);

            form.RenderControl(htextw);
            HttpContext.Current.Response.Write(stw.ToString());
            HttpContext.Current.Response.End();
        }

        public static void pdfport(string fileName, GridView gv, int lans, Page p)
        {
            gv.Visible = true;
            csclass.gridstrip(gv);
            gv.DataBind();

            string attachment = "attachment; filename=" + fileName + csclass.ranfn() + ".pdf";

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();
            gv.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gv);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
            if (lans == 1)
            {
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.A3.Rotate());
            }

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();
        }

       public static string ranfn()
       {           
           Random rnd = new Random();
           int value = Convert.ToInt32(200 * rnd.Next()) + 1;

           string filename = String.Concat(value.ToString(), "_", DateTime.Now.ToString("MHHmmss"));

           return filename;  
       }

       public static void MergeRowsWithSameContent(GridView gvw, int target, int _ref)
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

       public static void mergeOnDatabound(GridView gv, int _target, int _ref) {
           for (int i = gv.Rows.Count - 1; i > 0; i--)
           {
               GridViewRow row = gv.Rows[i];
               GridViewRow previousRow = gv.Rows[i - 1];
               for (int j = _target; j <= _target; j++)
               {
                   if (row.Cells[_ref].Text == previousRow.Cells[_ref].Text)
                   {
                       if (previousRow.Cells[j].RowSpan == 0)
                       {
                           if (row.Cells[j].RowSpan == 0)
                           {
                               previousRow.Cells[j].RowSpan += 2;
                           }
                           else
                           {
                               previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                           }
                           row.Cells[j].Visible = false;
                           row.Cells[j].BorderStyle = BorderStyle.Solid;
                       }
                   }
               }
           }
       }
        
   
    
               
    }
}