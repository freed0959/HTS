<%@ Page Title="Passing " Language="VB" MasterPageFile="~/HTS2/Site.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="Passing.aspx.vb" Inherits="HTS2_Passing" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajx" %>
<%@ Register tagprefix="uc" TagName="pmenu" Src="~/HTS2/PassMenu.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">   

<script type="text/javascript">
<!--

    /*
    Auto Refresh Page with Time script
    By JavaScript Kit (javascriptkit.com)
    Over 200+ free scripts here!
    */

    //enter refresh time in "minutes:seconds" Minutes should range from 0 to inifinity. Seconds should range from 0 to 59
    var limit = "3:00"

    if (document.images) {
        var parselimit = limit.split(":")
        parselimit = parselimit[0] * 60 + parselimit[1] * 1
    }
    function beginrefresh() {
        if (!document.images)
            return
        if (parselimit == 1)
            window.location.reload()
        else {
            parselimit -= 1
            curmin = Math.floor(parselimit / 60)
            cursec = parselimit % 60
            if (curmin != 0)
                curtime = curmin + " minutes and " + cursec + " seconds left until page refresh!"
            else
                curtime = cursec + " seconds left until page refresh!"
            window.status = curtime
            setTimeout("beginrefresh()", 1000)
        }
    }

    window.onload = beginrefresh
    //-->
</script>

<script type="text/javascript"  >
    function TestForReturn() {
        if (event.keyCode == 13) {
            event.cancelBubble = true;
            event.returnValue = false;
            return true;
       }
       return false;       
   }
</script>

<style type="text/css">
        .pnlCSS{
            font-weight: normal;
            cursor: pointer;
            border: solid 1px #c0c0c0;
            width:465px;
        }

          .srcTxt {
             height:25px;
            width:225px;
            font-family:arial Verdana Calibri;
            font-size:24px;
        }
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:Panel ID="pan_entry1" runat="server"></asp:Panel>

    <div class="templatemo_post">          
        <div class="templatemo_post_text">
   
        <asp:Panel ID="pnlClick" runat="server" CssClass="pnlCSS" >
            <div style="background-image:url('images/tabs/blueGrad.jpg'); height:30px; vertical-align:middle; background-repeat:repeat-x;">
                <div style="float:left; color:White; padding:5px 5px 5px 0; margin-left:10px;">
                    Passing Data Filter
                </div>
                <div style="float:right; color:White; padding:5px 5px 0 0;display:inline;">
                    <asp:Image ID="imgArrows" runat="server" ImageAlign="AbsMiddle" />
                    <asp:Label ID="lblMessage" runat="server" Text="Label"/>            
                </div>
                <div style="clear:both"></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCollapsable" runat="server" Height="0" CssClass="pnlCSS">
            <table style="margin:10px 0 10px 0;padding-left:10px;" cellpadding="2" cellspacing="1">           
            <tr>
             <td>   
                 <asp:Label ID="lbl_date" runat="server" Text="Transaction Date"></asp:Label>
              </td>
                    <td>
                        <asp:TextBox ID="txt_date" runat="server" Width="85px"></asp:TextBox>
                        <ajx:CalendarExtender runat="server" Format="MM/dd/yyyy" TargetControlID="txt_date"></ajx:CalendarExtender>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Truck No."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="truckNo_Txt" runat="server" Width="85px"></asp:TextBox>
                    </td>                    
                </tr>              
                <tr>
                    <td >
                        <asp:Label ID="lbl_cargo" runat="server" Text="Raw Cargo"></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="ddl_cargo" runat="server" DataSourceID="ds_mat1" DataTextField="seamcargo" DataValueField="MaterialKode" Width="85px" AppendDataBoundItems="True">
                            <asp:ListItem Value="">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Label ID="Shift" runat="server" Text="Product"></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="ddl_prod" runat="server" Width="85px" DataSourceID="ds_prod1" DataTextField="ref_name" DataValueField="id_ref_master">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lbl_cntr" runat="server" Text="Contractor"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddl_cntr" runat="server" DataSourceID="SqlDataSource2" DataTextField="Kode" DataValueField="Kode" Width="85px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">                                       
                        <asp:RadioButtonList ID="rbl_block1" runat="server" RepeatColumns="2">
                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                            <asp:ListItem Value="67">Block Km. 67</asp:ListItem>
                            <asp:ListItem Value="29">Block Km. 29</asp:ListItem>
                            <asp:ListItem Value="02">Block Km. 2</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:Button ID="btn_process" runat="server" Text="Submit" OnClick="btn_process_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajx:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
            CollapseControlID="pnlClick" Collapsed="true" ExpandControlID="pnlClick"
            TextLabelID="lblMessage" CollapsedText="Show" ExpandedText="Hide" 
            ImageControlID="imgArrows" CollapsedImage="images/collapse.png" ExpandedImage="images/expand.png"
            ExpandDirection="Vertical" TargetControlID="pnlCollapsable" ScrollContents="false">
        </ajx:CollapsiblePanelExtender>



     <div style="float:right; margin-bottom: 50px;">
            <asp:Panel ID="pan_entry29" runat="server" Visible="false">
                <asp:Label ID="lbl_entryhead1" runat="server" Text="Enter TruckNo" ForeColor="Blue"></asp:Label><br />
                <asp:TextBox ID="txt_truck29" runat="server" CssClass="srcTxt" ToolTip="Type one full TruckNo ie.: Hxxx then press Enter or clik the &quot;Passed&quot; button"></asp:TextBox>
                <asp:Button ID="btn_src29" runat="server" Text="Pass Km. 29" Height="30px" /><br />
                            
                <asp:Label ID="lbl_info29" runat="server" ForeColor="Maroon" Font-Size="12px"></asp:Label>
             </asp:Panel>
             <asp:Panel ID="pan_entry02" runat="server" Visible="false">
                 <asp:Label ID="Label11" runat="server" Text="Enter TruckNo" ForeColor="Blue"></asp:Label><br />
                <asp:TextBox ID="txt_truckkls" runat="server" CssClass="srcTxt" ToolTip="Type one full TruckNo ie.: Hxxx then press Enter or clik the &quot;In Queue&quot; button"></asp:TextBox>
                <asp:Button ID="btn_srckls" runat="server" Text="In Queue" Height="30px" /><br />
                             
                <div runat="server" id="blinkText1"><asp:Label ID="lbl_info0" runat="server" ForeColor="Maroon" Font-Size="12px"></asp:Label></div>  
             </asp:Panel>
      </div>

         <script src="blink/jquery-1.3.2.min.js" language="javascript" type="text/javascript"></script>
            <script src="blink/jquery-blink.js" language="javscript" type="text/javascript"></script>
            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    $('.blink').blink(); // default is 500ms blink interval.
                    //$('.blink').blink({delay:100}); // causes a 100ms blink interval.
                });
            </script>

        <asp:Panel ID="pan_tool1" runat="server" CssClass="pantool" style="width: 880px; margin-top:25px;" Width="850px">
                <div class="olet">
                    <asp:ImageButton ID="rcpChart1" runat="server" AlternateText="Click to View Chart" ImageUrl="images/kchart.png" />
                    <asp:ImageButton ID="rcpXls1" runat="server" AlternateText="Click to Export Data to Excel" ImageUrl="images/document_microsoft_excel_01.png" />
                    <asp:ImageButton ID="rcpPdf1" runat="server" AlternateText="Click to Export Data to Pdf" ImageUrl="images/pdf.png" />
                </div>
                <div class="orig">
                    <asp:Label ID="totlbl1" runat="server" CssClass="totlbl"></asp:Label>
                    <asp:CheckBox ID="chb_edit1" runat="server" AutoPostBack="True" CssClass="totlbl" Text="Edit" Visible="False" />
                    <asp:DropDownList ID="ddl_pag1" runat="server" AutoPostBack="True" BackColor="#FFCC66" CssClass="ddl" Font-Names="Calibri,Arial,Verdana" ForeColor="#333333" Height="30px">
                        <asp:ListItem Value="25">25 Rows per Page</asp:ListItem>
                        <asp:ListItem Value="100">100 Rows per Page</asp:ListItem>
                        <asp:ListItem Value="500">500 Rows per Page</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ImageButton ID="templt1" runat="server" AlternateText="Upload Template" ImageUrl="~/HTS2/images/table.png" Visible="False" />
                    <asp:ImageButton ID="rcpUpd1" runat="server" AlternateText="Click to Upload Data" ImageUrl="~/HTS2/images/move_waiting_up.png" Visible="False" />
                    <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Click to Manual Entry Data" ImageUrl="~/HTS2/images/file_new_01.png" Visible="False" />
                </div>
            </asp:Panel>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="View1" runat="server">

            <asp:GridView ID="grid_pass1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="2" DataSourceID="SqlDS_truckpass" 
                EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" PageSize="25" ShowHeaderWhenEmpty="True" Width="860px" DataKeyNames="ID_Pass" AllowSorting="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_Pass" HeaderText="ID_Pass" SortExpression="ID_Pass" Visible="False" />
                    <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" SortExpression="TruckNo" />
                    <asp:BoundField DataField="seamcargo" HeaderText="Raw Cargo" SortExpression="seamcargo" />
                    <asp:BoundField DataField="SourceKode" HeaderText="ROM" SortExpression="SourceKode" />
                    <asp:BoundField DataField="alamo" HeaderText="Raw Cargo" SortExpression="alamo" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Gross" HeaderText="Nett" SortExpression="Gross" DataFormatString="{0:N0}" HtmlEncode="False" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TransactionDate" HeaderText="Km. 67" SortExpression="TransactionDate" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="TD_29Pass" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 29" HtmlEncode="False" SortExpression="TD_29Pass"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="TD_inQueue" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 2" HtmlEncode="False" SortExpression="TD_inQueue"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="DateClosed" DataFormatString="{0:hh:mm tt}" HeaderText="Closed" HtmlEncode="False" SortExpression="DateClosed"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:TemplateField HeaderText="INFO">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# cekData(Eval("blockrem"))%>' 
                                NavigateUrl='<%# String.Concat("passentry1.aspx?v=1&idp=", Eval("ID_Pass"))%>'
                                Target = '<%# "fltdev" & Eval("ID_Pass")%>' class="blink"
                                onClick="window.open(this.href, this.target,'status=1,scrollbars=1,resizable=1,width=600,height=250,left=10,top=10'); return false"></asp:HyperLink>
                            <asp:Label ID="Label6" runat="server" Text='<%# cekData(Eval("miss67"))%>'></asp:Label>                            
                            <asp:Label ID="Label3" runat="server" Text='<%# cekData(Eval("block29"))%>' class="blink" ForeColor="maroon"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text='<%# cekData(Eval("blockQ"))%>' class="blink" ForeColor="maroon"></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text='<%# cekData(Eval("uns"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MaterialKode" HeaderText="MaterialKode" SortExpression="MaterialKode" Visible="False" />
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
     
        </asp:View>

        <asp:View ID="View2" runat="server">
            
            <asp:GridView ID="grid_edit67" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="ID_Pass" DataSourceID="SqlDS_truckpass" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" PageSize="5" ShowHeaderWhenEmpty="True" Width="860px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_Pass" HeaderText="ID_Pass" SortExpression="ID_Pass" Visible="False" />
                    <asp:BoundField DataField="TransactionDate" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 67 Pass" HtmlEncode="False" SortExpression="TransactionDate">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="alamo" HeaderText="WB Raw Cargo" SortExpression="alamo" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="TruckNo" SortExpression="TruckNo">
                        <ItemTemplate>
                            <asp:TextBox ID="txb_truck1" runat="server" BackColor="#CCCCFF" BorderStyle="None" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("TruckNo") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ControlStyle Height="20px" Width="55px" />
                        <HeaderStyle Width="75px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raw Cargo" SortExpression="seamcargo">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddl_mat1" runat="server" BackColor="#CCCCFF" DataSourceID="ds_mat1" DataTextField="seamcargo" DataValueField="MaterialKode" Font-Names="arial" Font-Size="8pt" SelectedValue='<%# Bind("MaterialKode") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ROM" SortExpression="SourceKode">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddl_rom1" runat="server" BackColor="#CCCCFF" DataSourceID="SqlDataSource5" DataTextField="Kode" DataValueField="Kode" Font-Names="Arial" Font-Size="8pt" SelectedValue='<%# Bind("SourceKode") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Blok Km. 67">
                        <ItemTemplate>
                            <asp:CheckBox ID="chb_k67" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="65px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block Km. 29">
                        <ItemTemplate>
                            <asp:CheckBox ID="chb_k29" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="65px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block Km. 2">
                        <ItemTemplate>
                            <asp:CheckBox ID="chb_k02" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="65px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imb_savepas1" runat="server" CommandArgument='<%# Eval("ID_Pass") %>' CommandName="_passupdate" ImageUrl="~/HTS2/images/save_01.png" />
                        </ItemTemplate>
                        <ControlStyle Height="16px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="65px" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            
        </asp:View>

        <asp:View ID="View3" runat="server">
            
            <asp:GridView ID="grid_edit29" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="ID_Pass" DataSourceID="SqlDS_truckpass" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" PageSize="5" ShowHeaderWhenEmpty="True" Width="860px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_Pass" HeaderText="ID_Pass" SortExpression="ID_Pass" Visible="False" />
                    <asp:BoundField DataField="TransactionDate" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 67 Pass" HtmlEncode="False" SortExpression="TransactionDate">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD_29Pass" DataFormatString="{0:hh:mm tt}" HeaderText="Km.29 Pass" HtmlEncode="False" SortExpression="TD_29Pass">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="alamo" HeaderText="WB Raw Cargo" SortExpression="alamo" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="TruckNo" SortExpression="TruckNo">
                        <ItemTemplate>
                            <asp:TextBox ID="txb_truck2" runat="server" BackColor="#CCCCFF" BorderStyle="None" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("TruckNo") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ControlStyle Height="20px" Width="55px" />
                        <HeaderStyle Width="75px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raw Cargo" SortExpression="seamcargo">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddl_mat2" runat="server" BackColor="#CCCCFF" DataSourceID="ds_mat1" DataTextField="seamcargo" DataValueField="MaterialKode" Font-Names="arial" Font-Size="8pt" SelectedValue='<%# Bind("MaterialKode") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ROM" SortExpression="SourceKode">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddl_rom2" runat="server" BackColor="#CCCCFF" DataSourceID="SqlDataSource5" DataTextField="Kode" DataValueField="Kode" Font-Names="Arial" Font-Size="8pt" SelectedValue='<%# Bind("SourceKode") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block Km. 29">
                        <ItemTemplate>
                            <asp:CheckBox ID="chb_k69" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="65px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block Km. 2">
                        <ItemTemplate>
                            <asp:CheckBox ID="chb_k70" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="65px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imb_savepas2" runat="server" CommandArgument='<%# Eval("ID_Pass") %>' CommandName="_passupdate" ImageUrl="~/HTS2/images/save_01.png" />
                        </ItemTemplate>
                        <ControlStyle Height="16px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="65px" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            
        </asp:View>

        <asp:View ID="View4" runat="server">

            <asp:GridView ID="grid_edit02" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="ID_Pass" DataSourceID="SqlDS_truckpass" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" PageSize="5" ShowHeaderWhenEmpty="True" Width="860px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_Pass" HeaderText="ID_Pass" SortExpression="ID_Pass" Visible="False" />
                    <asp:BoundField DataField="TransactionDate" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 67 Pass" HtmlEncode="False" SortExpression="TransactionDate">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD_29Pass" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 29 Pass" HtmlEncode="False" SortExpression="TD_29Pass">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD_inQueue" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 02 Queue" HtmlEncode="False" SortExpression="TD_inQueue">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="alamo" HeaderText="WB Raw Cargo" SortExpression="alamo" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="TruckNo" SortExpression="TruckNo">
                        <ItemTemplate>
                            <asp:TextBox ID="txb_truck3" runat="server" BackColor="#CCCCFF" BorderStyle="None" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("TruckNo") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ControlStyle Height="20px" Width="55px" />
                        <HeaderStyle Width="75px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Raw Cargo" SortExpression="seamcargo">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddl_mat3" runat="server" BackColor="#CCCCFF" DataSourceID="ds_mat1" DataTextField="seamcargo" DataValueField="MaterialKode" Font-Names="arial" Font-Size="8pt" SelectedValue='<%# Bind("MaterialKode") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ROM" SortExpression="SourceKode">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddl_rom3" runat="server" BackColor="#CCCCFF" DataSourceID="SqlDataSource5" DataTextField="Kode" DataValueField="Kode" Font-Names="Arial" Font-Size="8pt" SelectedValue='<%# Bind("SourceKode") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block Km. 2">
                        <ItemTemplate>
                            <asp:CheckBox ID="chb_k72" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="65px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imb_savepas3" runat="server" CommandArgument='<%# Eval("ID_Pass") %>' CommandName="_passupdate" ImageUrl="~/HTS2/images/save_01.png" />
                        </ItemTemplate>
                        <ControlStyle Height="16px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="65px" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

        </asp:View>

    </asp:MultiView>

        <div style="float: right; font-size: 10px; font-style:italic;">* UNS = Unschedulled Truck (Daily Quota)</div><br />
        <div style="float: right; font-size: 10px; font-style:italic;background-color:#FF9933;">* Weighbridge Data (Auto-Synced)</div>

    </div>
    </div>

    <asp:LinkButton style="visibility:hidden" ID="CloseLink" runat="server" OkControlID="CloseLink">Close Proxy</asp:LinkButton>
    <asp:LinkButton style="visibility:hidden" ID="CancelLink" runat="server" CancelControlID="CancelLink">Cancel Proxy</asp:LinkButton> 

    <ajx:modalpopupextender ID="ModalPopupExtender1" runat="server" backgroundcssclass="ModalPopupBG" cancelcontrolid="CancelLink" drag="True" 
                    Enabled="True" okcontrolid="CloseLink" popupcontrolid="pan_load67" popupdraghandlecontrolid="PopupHeader" 
                    TargetControlID="rcpUpd1" DynamicServicePath="" ></ajx:modalpopupextender>

    <asp:Panel ID="pan_load67" runat="server">
                    <div class="HellowWorldPopup">
                        <div class="PopupHeader" id="PopupHeader1">
                             Upload Passing Km. 67 Data<br /><br />
                        </div>
                        <div class="PopupBody">
                            <asp:FileUpload ID="FileUpload1" runat="server" class="file"/><br />                        
                        </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="lbt_upl67" OnClientClick="$('CloseLink').click();" runat="server" CausesValidation="True" CommandName="Insert" Text="Upload" OnClick="lbt_upl67_Click"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton4" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>                    
                </div>
    </asp:Panel> 

   <asp:SqlDataSource ID="SqlDS_truckpass" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" OnSelected="SqlDS_truckpass_Selected" 
                SelectCommand="_sp_rtk_passdaily1" SelectCommandType="StoredProcedure">
       <SelectParameters>
           <asp:Parameter Name="dtm" Type="String" />
           <asp:Parameter Name="trc" Type="String" />
           <asp:Parameter Name="mat" Type="String" />
           <asp:Parameter Name="prod" Type="String" />
           <asp:Parameter Name="kon" Type="String" />
           <asp:Parameter Name="b67" Type="Byte" />
           <asp:Parameter Name="b29" Type="Byte" />
           <asp:Parameter Name="b02" Type="Byte" />
           <asp:Parameter Name="clos" Type="Byte" />
           <asp:Parameter Name="ord" Type="String" />
       </SelectParameters>
        </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT ' All' [Kode] union all  SELECT [Kode] FROM [v_Kontraktor] where Kode != 'ADARO' ORDER BY Kode "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT NULL as  [Kode] 
UNION ALL
SELECT [Kode] FROM [v_Sources]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_mat1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_lasmap2" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <asp:SqlDataSource ID="ds_prod1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT NULL as [id_ref_master], 'All' as [ref_name]
                UNION ALL
                SELECT  [ref_name] , [ref_name] FROM [tbl_ref_master]
                WHERE  [id_ref_type]  = 5"></asp:SqlDataSource>
 
            <asp:GridView ID="grid_shadow1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDS_truckpass" 
                EmptyDataText="Sorry, Data Not Found" PageSize="5" ShowHeaderWhenEmpty="True" DataKeyNames="ID_Pass" Visible="False">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_Pass" HeaderText="ID_Pass" SortExpression="ID_Pass" Visible="False" />
                    <asp:BoundField DataField="TransactionDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="TransactionDate" HtmlEncode="False" SortExpression="TransactionDate"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" SortExpression="TruckNo" />
                    <asp:BoundField DataField="seamcargo" HeaderText="Raw Cargo" SortExpression="seamcargo" />
                    <asp:BoundField DataField="SourceKode" HeaderText="ROM" SortExpression="SourceKode" />
                    <asp:BoundField DataField="alamo" HeaderText="WB Raw Cargo" SortExpression="alamo" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TimbROM" HeaderText="WB Rom" SortExpression="TimbROM">
                    <HeaderStyle BackColor="#FF9933" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Gross" HeaderText="WB Nett" SortExpression="Gross" DataFormatString="{0:N0}" HtmlEncode="False" >
                    <HeaderStyle BackColor="#FF9933" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TransactionDate" HeaderText="Km. 67" SortExpression="TransactionDate" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="TD_TimbPass" HeaderText="Weighbridge" SortExpression="TD_TimbPass" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" >
                    <HeaderStyle BackColor="#FF9933" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TD_29Pass" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 29" HtmlEncode="False" SortExpression="TD_29Pass"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="TD_inQueue" DataFormatString="{0:hh:mm tt}" HeaderText="Km. 2" HtmlEncode="False" SortExpression="TD_inQueue"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:BoundField DataField="DateClosed" DataFormatString="{0:hh:mm tt}" HeaderText="Closed" HtmlEncode="False" SortExpression="DateClosed"><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:TemplateField HeaderText="INFO">
                        <ItemTemplate>                           
                            <asp:Label ID="Label1" runat="server" Text='<%# cekData(Eval("blockrem"))%>'></asp:Label>
                            <asp:Label ID="Label8" runat="server" Text='<%# cekData(Eval("block29"))%>'></asp:Label>
                            <asp:Label ID="Label9" runat="server" Text='<%# cekData(Eval("blockQ"))%>'></asp:Label>
                            <asp:Label ID="Label7" runat="server" Text='<%# cekData(Eval("miss67"))%>'></asp:Label>                            
                            <asp:Label ID="Label10" runat="server" Text='<%# cekData(Eval("uns"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <HeaderStyle Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Position="TopAndBottom" />
                <RowStyle Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
            </asp:GridView>
     
        </asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SideContent1" Runat="Server">     
    <uc:pmenu runat="server" ID="ucmenu"/>

    <asp:Panel ID="passmenu1" runat="server"></asp:Panel>

     <div class="templatemo_section">
            <div class="templatemo_section_title">
                Documentation
            </div>
            <div class="templatemo_section_bottom">
                <asp:Image ID="Image2" runat="server" ImageAlign="Middle" ImageUrl="~/Images/adobe_acrobat_reader.png" />
                <asp:LinkButton ID="lbt_pasman1" runat="server">Passing Page Manual</asp:LinkButton>
            </div>
        </div>         
</asp:Content>

