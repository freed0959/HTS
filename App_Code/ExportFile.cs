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
    public static class ExportFile
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

        public static void xlsport(GridView grd, string _fnm, Page p)
        {
            grd.Visible = true;
            gridstrip(grd);    
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

        public static void pdfport(GridView gv, string fileName, int lans, Page p)
        {
            gv.Visible = true;
            gridstrip(gv);
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
    }
}