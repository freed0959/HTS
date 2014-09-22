<%@ Page Title="QC-Mine " Language="VB" MasterPageFile="~/HTS2/Site.master" EnableEventValidation="false" AutoEventWireup="True" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="Stylesheet" href="css_ajax_tabcontainer.css" type="text/css" />
    <link href="tabSlideOut/tabSlideOut.css" rel="stylesheet" />


    <style type="text/css">
        .pnlCSS {
            font-weight: normal;
            cursor: pointer;
            border: solid 1px #c0c0c0;
            width: 355px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="Server">


    <asp:MultiView ID="MultiView1" runat="server">

        <asp:View ID="portal_View1" runat="server">


            <div style="display: inline-block;">
                <div style="margin-bottom: 20px; float: left;">
                    <asp:Panel ID="pnlClick" runat="server" CssClass="pnlCSS">
                        <div style="background-image: url('images/tabs/blueGrad.jpg'); height: 30px; vertical-align: middle; background-repeat: repeat-x;">
                            <div style="float: left; color: White; padding: 5px 5px 5px 0; margin-left: 10px;">
                                Quality Portal Data Filter
                            </div>
                            <div style="float: right; color: White; padding: 5px 5px 0 0; display: inline;">
                                <asp:Label ID="lblMessage" runat="server" Text="Label" />
                                <asp:Image ID="imgArrows" runat="server" ImageAlign="AbsMiddle" />
                            </div>
                            <div style="clear: both"></div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCollapsable" runat="server" Height="0px" CssClass="pnlCSS">
                        <table style="vertical-align: top; margin: 0 10px 0 10px">
                            <tr>
                                <td>Month </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth" runat="server">
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>Year </td>
                                <td>
                                    <asp:DropDownList ID="ddlYear" runat="server" DataSourceID="SqlDataSource4" DataTextField="tahun" DataValueField="tahun">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Kontraktor</td>
                                <td>
                                    <asp:DropDownList ID="ddl_kon1" runat="server" DataSourceID="ds_kon1" DataTextField="Kode" DataValueField="Kode">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>Product</td>
                                <td>
                                    <asp:DropDownList ID="ddl_prod1" runat="server" DataSourceID="ds_prod1" DataTextField="ref_name" DataValueField="id_ref_master">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="right">
                                    <asp:Button ID="Button2" runat="server" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajx:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                        CollapseControlID="pnlClick" Collapsed="True" ExpandControlID="pnlClick"
                        TextLabelID="lblMessage" CollapsedText="Show" ExpandedText="Hide"
                        ImageControlID="imgArrows" CollapsedImage="images/collapse.png" ExpandedImage="images/expand.png"
                        TargetControlID="pnlCollapsable" Enabled="True">
                    </ajx:CollapsiblePanelExtender>
                </div>
            </div>


            <asp:Panel ID="pan_tool1" runat="server" CssClass="pantool" Style="margin-top: 25px;" Width="655">
                <div class="olet">
                    <asp:ImageButton ID="cpChart1" runat="server" ImageUrl="~/HTS2/images/kchart.png" unat="server" Visible="False" />
                </div>
                <div class="orig">
                    <asp:Label ID="totlbl1" runat="server" CssClass="totlbl"></asp:Label>
                    <asp:CheckBox ID="chb_edit_por1" runat="server" CssClass="totlbl" Text="Edit Portal" AutoPostBack="True" Visible="False" />
                </div>
            </asp:Panel>
            <asp:GridView ID="grid_opor1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id_qualportal" DataSourceID="ds_por1" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="650px">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="productKode" HeaderText="productKode" SortExpression="productKode" Visible="False" />
                    <asp:BoundField DataField="KontraktorKode" HeaderText="Kontraktor" SortExpression="KontraktorKode">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MaterialKode" HeaderText="Weighbridge MaterialKode" SortExpression="MaterialKode" />
                    <asp:BoundField DataField="seamcargo" HeaderText="Raw Material ROM" SortExpression="seamcargo" />
                    <asp:BoundField DataField="rem" HeaderText="Seam" SortExpression="rem">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="productname" HeaderText="Product" SortExpression="productname">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Edit Product" ShowHeader="False" Visible="False">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="ds_prod2" DataTextField="ref_name" DataValueField="id_ref_master" SelectedValue='<%# Eval("productKode") %>'>
                            </asp:DropDownList>
                            <asp:Label ID="lbl_idpor1" runat="server" Text='<%# Eval("id_qualportal") %>' Visible="False"></asp:Label>
                            <asp:Label ID="lbl_porlama1" runat="server" Text='<%# Eval("productKode") %>' Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="65px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" Visible="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbRows" runat="server" />
                        </ItemTemplate>
                        <ControlStyle Width="16px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

            <ajx:ModalPopupExtender ID="ModalPE1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                Enabled="True" OkControlID="CloseLink" PopupControlID="loadPan1" PopupDragHandleControlID="PopupHeader"
                TargetControlID="ImageButton1" DynamicServicePath="">
            </ajx:ModalPopupExtender>

            <asp:Panel ID="loadPan1" runat="server">
                <div class="HellowWorldPopup">
                    <div id="PopupHeader4" class="PopupHeader">
                        Upload Quality Portal Data<br />
                        <br />
                    </div>
                    <div class="PopupBody">
                        <asp:FileUpload ID="FileUpload2" runat="server" class="file" />
                        <br />
                        <asp:CheckBox ID="chb_qpor1" runat="server" Checked="True" Text="Announce after upload" />
                        <br />
                    </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Insert" OnClick="LinkButton1_Click" OnClientClick="$('CloseLink').click();" Text="Upload"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" OnClientClick="$('CancelLink').click();" Text="Cancel"></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            <asp:SqlDataSource ID="ds_por1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_portal1" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="mon" Type="String" />
                    <asp:Parameter Name="yer" Type="String" />
                    <asp:Parameter Name="kon" Type="String" />
                    <asp:Parameter Name="prd" Type="String" />
                    <asp:Parameter Name="cql" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


            <script type="text/javascript">
                $(function () {
                    $('.slide-schemablend-ctrk1').tabSlideOut({
                        tabHandle: '.handlek1',                     //class of the element that will become your tab
                        pathToTabImage: 'tabSlideOut/images/controls.png', //path to the image for the tab //Optionally can be set using css
                        imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                        imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                        tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                        speed: 300,                               //speed of animation
                        action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                        topPos: '270px',                          //position from the top/ use if tabLocation is left or right
                        leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                        fixedPosition: true                      //options: true makes it stick(fixed position) on scroll
                    });

                });
            </script>

            <div class="slide-schemablend-ctrk1" style="height: 100px;">
                <a class="handlek1" href="http://link-for-non-js-users.html">Content</a>
                <div runat="server" id="div_btn1" visible="false">
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ibt_update_por1" runat="server" ImageUrl="~/HTS2/images/save_01.png" ToolTip="Update Data" Width="24px" />
                        </div>
                        <div class="btn_lbl">Save Update(s)</div>
                    </div>
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ibt_delete_por1" runat="server" ImageUrl="~/HTS2/images/trash_can3_delete.png" ToolTip="Delete Selected Rows" Width="26px" OnClientClick="return confirm('Are you sure you want to delete this material(s)?');" />
                        </div>
                        <div class="btn_lbl">Delete Record(s)</div>
                    </div>

                </div>
                <div runat="server" id="div_upload1" visible="false">
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="templt3" runat="server" AlternateText="Upload Template" ImageUrl="~/HTS2/images/table.png" Width="24" />
                        </div>
                        <div class="btn_lbl">Upload Template</div>
                    </div>
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/HTS2/images/move_waiting_up.png" Width="24" />
                        </div>
                        <div class="btn_lbl">Upload Data</div>
                    </div>
                </div>
            </div>

        </asp:View>

        <asp:View ID="week_View1" runat="server">

            <asp:Panel ID="pan_nlis1" runat="server">
                <div style="float: left;">
                    <asp:Panel ID="Panel4" runat="server" CssClass="pnlCSS">
                        <div style="background-image: url('images/tabs/blueGrad.jpg'); height: 30px; vertical-align: middle; background-repeat: repeat-x;">
                            <div style="float: left; color: White; padding: 5px 5px 5px 0; margin-left: 10px;">
                                Weekly Projection Data Filter
                            </div>
                            <div style="float: right; color: White; padding: 5px 5px 0 0; display: inline;">
                                <asp:Label ID="Label24" runat="server" Text="Label" />
                                <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" />
                            </div>
                            <div style="clear: both"></div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel5" runat="server" Height="0px" CssClass="pnlCSS">
                        <table style="vertical-align: top;" width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Year"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddl_cyear1" runat="server" AutoPostBack="True" DataSourceID="ds_year2" DataTextField="cotyear" DataValueField="cotyear">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="ds_year2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT DISTINCT 
                                                    CASE WHEN 
                                                    YEAR([plan_startdate]) IS NULL THEN YEAR(GETDATE())
                                                    ELSE
                                                    YEAR([plan_startdate]) END cotyear FROM [tbl_qualplan_containr]"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_date1" runat="server" Text="Weekly Plan"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddl_wpl1" runat="server" DataSourceID="ds_week1" DataTextField="week1" DataValueField="id_qualplan_containr">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="ds_week1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT id_qualplan_containr, ' (' + CONVERT(nvarchar(25), id_qualplan_containr) +') ' +CONVERT(nvarchar(25), plan_startdate, 101) + ' - ' + CONVERT(nvarchar(25), plan_enddate, 101) as week1
                                        FROM [tbl_qualplan_containr]
                                        Where (PlanTypeKode = 13) AND YEAR(plan_startdate) = @planyear
                                        ORDER BY plan_enddate DESC">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddl_cyear1" Name="planyear" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>Material</td>
                                <td>
                                    <asp:DropDownList ID="ddl_mat2" runat="server" DataSourceID="SqlDataSource3" DataTextField="seamcargo" DataValueField="Kode">
                                    </asp:DropDownList>
                                </td>
                                <td>Product</td>
                                <td>
                                    <asp:DropDownList ID="ddl_prod2" runat="server" DataSourceID="ds_prod1" DataTextField="ref_name" DataValueField="id_ref_master">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:Button ID="btn_sub2" runat="server" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajx:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                        CollapseControlID="Panel4" Collapsed="False" ExpandControlID="Panel4"
                        TextLabelID="Label24" CollapsedText="Show" ExpandedText="Hide"
                        ImageControlID="Image1" CollapsedImage="images/collapse.png" ExpandedImage="images/expand.png"
                        TargetControlID="Panel5" Enabled="True">
                    </ajx:CollapsiblePanelExtender>
                </div>

               
                <asp:Panel ID="Panel2" runat="server" CssClass="pantool" Style="margin-top: 10px; vertical-align:text-bottom;" Width="1005px">
                    <div class="olet">
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/HTS2/images/kchart.png" Visible="False" />
                    </div>
                    <div class="orig">
                        <asp:Label ID="lbl_info2" runat="server" CssClass="totlbl"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:GridView ID="grid_plan1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id_qualwekplan" DataSourceID="ds_pln1" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="1000px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id_qualwekplan" HeaderText="id_qualwekplan" InsertVisible="False" ReadOnly="True" SortExpression="id_qualwekplan" Visible="False" />
                        <asp:BoundField DataField="wpdate" HeaderText="Plan Date" SortExpression="wpdate" Visible="False" />
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Kontraktor" SortExpression="KontraktorKode">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MaterialKode1" HeaderText="Material" SortExpression="MaterialKode1">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SourceKode" HeaderText="ROM" SortExpression="SourceKode" />
                        <asp:BoundField DataField="KontraktorCap" DataFormatString="{0:N0}" HeaderText="Contractor Availability" HtmlEncode="False" SortExpression="KontraktorCap" />
                        <asp:BoundField DataField="ROMCap" DataFormatString="{0:N0}" HeaderText="Available from ROM" HtmlEncode="False" SortExpression="ROMCap" />
                        <asp:BoundField DataField="deviatCap" DataFormatString="{0:N0}" HeaderText="Deviation" HtmlEncode="False" SortExpression="deviatCap" />
                        <asp:BoundField DataField="blendNeed" DataFormatString="{0:N0}" HeaderText="Blending Need" HtmlEncode="False" SortExpression="blendNeed" />
                        <asp:BoundField DataField="dayminington" DataFormatString="{0:N0}" HeaderText="Mining TON/day" HtmlEncode="False" SortExpression="dayminington" />
                        <asp:BoundField DataField="dayhaulton" DataFormatString="{0:N0}" HeaderText="Hauling TON/day" HtmlEncode="False" SortExpression="dayhaulton" />
                        <asp:BoundField DataField="pitPercent" DataFormatString="{0:N0}" HeaderText="Pit Percentage" HtmlEncode="False" SortExpression="pitPercent" />
                        <asp:BoundField DataField="romPercent" DataFormatString="{0:N0}" HeaderText="ROM Percentage" HtmlEncode="False" SortExpression="romPercent" />
                        <asp:BoundField DataField="TM" DataFormatString="{0:N2}" HeaderText="TM" HtmlEncode="False" SortExpression="TM" />
                        <asp:BoundField DataField="IM" DataFormatString="{0:N2}" HeaderText="IM" HtmlEncode="False" SortExpression="IM" />
                        <asp:BoundField DataField="HGI" DataFormatString="{0:N2}" HeaderText="HGI" HtmlEncode="False" SortExpression="HGI" />
                        <asp:BoundField DataField="ASH_ADB" DataFormatString="{0:N2}" HeaderText="ASH ADB" HtmlEncode="False" SortExpression="ASH_ADB" />
                        <asp:BoundField DataField="ASH_AR" DataFormatString="{0:N2}" HeaderText="ASH AR" HtmlEncode="False" SortExpression="ASH_AR" />
                        <asp:BoundField DataField="TS_ADB" DataFormatString="{0:N2}" HeaderText="TS ADB" HtmlEncode="False" SortExpression="TS_ADB" />
                        <asp:BoundField DataField="TS_AR" DataFormatString="{0:N2}" HeaderText="TS AR" HtmlEncode="False" SortExpression="TS_AR" />
                        <asp:BoundField DataField="CalDaf" DataFormatString="{0:N0}" HeaderText="CalDaf" HtmlEncode="False" SortExpression="CalDaf" />
                        <asp:BoundField DataField="CalAdb" DataFormatString="{0:N0}" HeaderText="CalAdb" HtmlEncode="False" SortExpression="CalAdb" />
                        <asp:BoundField DataField="CV_AR" DataFormatString="{0:N0}" HeaderText="CV AR" HtmlEncode="False" SortExpression="CV_AR" />
                        <asp:TemplateField ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="~/Images/table_edit (1).png" Text="Select" Visible="false" />
                                <div style="text-decoration: none; border-style: none;">
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="" ImageUrl="~/Images/table_edit (1).png" Font-Underline="false" BorderStyle="None"
                                        NavigateUrl='<%# String.Concat("Default3?v=0&idwpr1=", Eval("id_qualwekplan"), "&idctr=", Eval("id_qualplan_containr"))%>'
                                        Target='<%# "wpr1det" & Eval("id_qualwekplan")%>'
                                        onClick="window.open(this.href, this.target,'status=1,scrollbars=1,resizable=1,width=700,height=600,left=10,top=10'); return false;">
                                    </asp:HyperLink>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/hts_nu_icons/file_broken.png" Text="Delete" Visible="false" />
                                <asp:CheckBox ID="cbWpr1" runat="server" />
                                <asp:Label ID="lbl_idwpr1" runat="server" Text='<%# Eval("id_qualwekplan")%>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbSelAllWpr1" runat="server" AutoPostBack="true" OnCheckedChanged="cbSelAllWpr1_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <div style="float: right; font-size: 10px; font-style: italic;">
                    *
                </div>
            </asp:Panel>

            <!--<<<<<<<<<<<<<<<<<<<<<<< PREVIOUS WEEKLY PROJECTION ----------------------->
            <asp:Panel ID="pancollaps1" runat="server" CssClass="pnlCSS" Width="1000px" style="margin-top:25px;">
                <div style="background-image: url('images/tabs/orange.jpg'); height: 30px; vertical-align: middle; background-repeat: repeat-x;">
                    <div style="float: left; color: black; padding: 5px 5px 5px 0; margin-left: 10px;">
                        Previous Weekly Projection
                     </div>
                    <div style="float: right; color: Black; padding: 5px 5px 0 0; display: inline;">
                        <asp:Label ID="Label2" runat="server" Text="Label" />
                        <asp:Image ID="Image2" runat="server" ImageAlign="AbsMiddle" />
                   </div>
                   <div style="clear: both"></div>
                   </div>
            </asp:Panel>
            <asp:Panel ID="pan_prev_wepro1" runat="server" BorderStyle="Solid" BorderColor="DarkOrange" BorderWidth="1px" Width="999px">
                <asp:GridView ID="grid_plan2" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id_qualwekplan" DataSourceID="ds_pln2" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="999px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="id_qualwekplan" HeaderText="id_qualwekplan" InsertVisible="False" ReadOnly="True" SortExpression="id_qualwekplan" Visible="False" />
                        <asp:BoundField DataField="wpdate" HeaderText="Plan Date" SortExpression="wpdate" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Kontraktor" SortExpression="KontraktorKode">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MaterialKode1" HeaderText="Material" SortExpression="MaterialKode1">
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SourceKode" HeaderText="ROM" SortExpression="SourceKode" />
                        <asp:BoundField DataField="KontraktorCap" DataFormatString="{0:N0}" HeaderText="Contractor Availability" HtmlEncode="False" SortExpression="KontraktorCap" />
                        <asp:BoundField DataField="ROMCap" DataFormatString="{0:N0}" HeaderText="Available from ROM" HtmlEncode="False" SortExpression="ROMCap" />
                        <asp:BoundField DataField="deviatCap" DataFormatString="{0:N0}" HeaderText="Deviation" HtmlEncode="False" SortExpression="deviatCap" />
                        <asp:BoundField DataField="blendNeed" DataFormatString="{0:N0}" HeaderText="Blending Need" HtmlEncode="False" SortExpression="blendNeed" />
                        <asp:BoundField DataField="dayminington" DataFormatString="{0:N0}" HeaderText="Mining TON/day" HtmlEncode="False" SortExpression="dayminington" />
                        <asp:BoundField DataField="dayhaulton" DataFormatString="{0:N0}" HeaderText="Hauling TON/day" HtmlEncode="False" SortExpression="dayhaulton" />
                        <asp:BoundField DataField="pitPercent" DataFormatString="{0:N0}" HeaderText="Pit Percentage" HtmlEncode="False" SortExpression="pitPercent" />
                        <asp:BoundField DataField="romPercent" DataFormatString="{0:N0}" HeaderText="ROM Percentage" HtmlEncode="False" SortExpression="romPercent" />
                        <asp:BoundField DataField="TM" DataFormatString="{0:N2}" HeaderText="TM" HtmlEncode="False" SortExpression="TM" />
                        <asp:BoundField DataField="IM" DataFormatString="{0:N2}" HeaderText="IM" HtmlEncode="False" SortExpression="IM" />
                        <asp:BoundField DataField="HGI" DataFormatString="{0:N2}" HeaderText="HGI" HtmlEncode="False" SortExpression="HGI" />
                        <asp:BoundField DataField="ASH_ADB" DataFormatString="{0:N2}" HeaderText="ASH ADB" HtmlEncode="False" SortExpression="ASH_ADB" />
                        <asp:BoundField DataField="ASH_AR" DataFormatString="{0:N2}" HeaderText="ASH AR" HtmlEncode="False" SortExpression="ASH_AR" />
                        <asp:BoundField DataField="TS_ADB" DataFormatString="{0:N2}" HeaderText="TS ADB" HtmlEncode="False" SortExpression="TS_ADB" />
                        <asp:BoundField DataField="TS_AR" DataFormatString="{0:N2}" HeaderText="TS AR" HtmlEncode="False" SortExpression="TS_AR" />
                        <asp:BoundField DataField="CalDaf" DataFormatString="{0:N0}" HeaderText="CalDaf" HtmlEncode="False" SortExpression="CalDaf" />
                        <asp:BoundField DataField="CalAdb" DataFormatString="{0:N0}" HeaderText="CalAdb" HtmlEncode="False" SortExpression="CalAdb" />
                        <asp:BoundField DataField="CV_AR" DataFormatString="{0:N0}" HeaderText="CV AR" HtmlEncode="False" SortExpression="CV_AR" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>                
            </asp:Panel>
            <ajx:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server"
                    CollapseControlID="pancollaps1" Collapsed="true" ExpandControlID="pancollaps1"
                    TextLabelID="Label2" CollapsedText="Show" ExpandedText="Hide"
                    ImageControlID="Image2" CollapsedImage="images/collapse.png" ExpandedImage="images/expand.png"
                    TargetControlID="pan_prev_wepro1" Enabled="True">
            </ajx:CollapsiblePanelExtender>


            <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                Enabled="True" OkControlID="CloseLink" PopupControlID="pan_wpj1" PopupDragHandleControlID="PopupHeader"
                TargetControlID="ImageButton4" DynamicServicePath="">
            </ajx:ModalPopupExtender>

            <asp:Panel ID="pan_wpj1" runat="server">
                <div class="HellowWorldPopup" style="width: 530px;">
                    <div id="PopupHeader3" class="PopupHeader">
                        Upload Weekly Projection Data<br />
                        <br />
                    </div>
                    <div class="PopupBody">
                        <table border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td style="width: 245px; vertical-align: top;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="file" />
                                    <asp:CheckBox ID="chb_weepro1" runat="server" Checked="True" Text="Announce after upload" />
                                </td>
                                <td style="width: 280px; vertical-align: top;">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Navy"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Insert" OnClientClick="$('CloseLink').click();" Text="Upload"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" CommandName="Cancel" OnClientClick="$('CancelLink').click();" Text="Cancel"></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>

            <asp:SqlDataSource ID="ds_pln1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                SelectCommand="_sp_ql_weproRtk_lis1" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="idwp" Type="String" />
                    <asp:Parameter Name="mat" Type="String" />
                    <asp:Parameter Name="prod" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


            <asp:SqlDataSource ID="ds_pln2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_weproRtk_lis1" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="idwp" Type="String" />
                    <asp:Parameter Name="mat" Type="String" />
                    <asp:Parameter Name="prod" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


            <script type="text/javascript">
                $(function () {
                    $('.slide-schemablend-ctrk1').tabSlideOut({
                        tabHandle: '.handlek1',                     //class of the element that will become your tab
                        pathToTabImage: 'tabSlideOut/images/controls.png', //path to the image for the tab //Optionally can be set using css
                        imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                        imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                        tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                        speed: 300,                               //speed of animation
                        action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                        topPos: '270px',                          //position from the top/ use if tabLocation is left or right
                        leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                        fixedPosition: true                      //options: true makes it stick(fixed position) on scroll
                    });

                });
            </script>

            <div class="slide-schemablend-ctrk1" style="height: 100px;">
                <a class="handlek1" href="http://link-for-non-js-users.html">Content</a>
                <div runat="server" id="div_uploadWpr1" visible="false">
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ibt_send_wpr1" runat="server" ImageUrl="~/HTS2/images/button_next_01.png" ToolTip="Send Quality to Alamo" Width="24px" OnClientClick="return confirm('Are you sure you want to send data to Alamo?');" />
                        </div>
                        <div class="btn_lbl">Send selected to Alamo</div>
                    </div>
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ibt_del_wpr1" runat="server" ImageUrl="~/HTS2/images/trash_can3_delete.png" ToolTip="Delete Selected Rows" Width="26px" OnClientClick="return confirm('Are you sure you want to delete this material(s)?');" />
                        </div>
                        <div class="btn_lbl">Delete Record(s)</div>
                    </div>
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="templt1" runat="server" AlternateText="Upload Template" ImageUrl="~/HTS2/images/table.png" Width="24" />
                        </div>
                        <div class="btn_lbl">Upload Template</div>
                    </div>
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/hts_nu_icons/move_waiting_up.png" Width="24" />
                        </div>
                        <div class="btn_lbl">Upload Data</div>
                    </div>
                </div>
            </div>

        </asp:View>

        <asp:View ID="his_View1" runat="server">
            <table style="vertical-align: top;" width="60%">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Year"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_year" runat="server" DataSourceID="SqlDataSource2" DataTextField="tahun" DataValueField="tahun">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Material"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_mtrl" runat="server" DataSourceID="SqlDataSource3" DataTextField="seamcargo" DataValueField="Kode">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button1" runat="server" Text="Submit" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" CssClass="pantool" Style="width: 880px; margin-top: 50px;">
                <div class="olet">
                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/HTS2/images/kchart.png" Visible="False" />
                </div>
                <div class="orig">
                    <asp:Label ID="LabelTot" runat="server" CssClass="totlbl"></asp:Label>
                    <asp:DropDownList ID="ddl_pag1" runat="server" AutoPostBack="True" BackColor="#FFCC66" CssClass="ddl" Font-Names="Calibri,Arial,Verdana" ForeColor="#333333" Height="30px">
                        <asp:ListItem Value="25">25 Rows per Page</asp:ListItem>
                        <asp:ListItem Value="100">100 Rows per Page</asp:ListItem>
                        <asp:ListItem Value="500">500 Rows per Page</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </asp:Panel>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="2" DataSourceID="SqlDataSource1" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" PageSize="25" ShowHeaderWhenEmpty="True" Width="860px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="tgl" HeaderText="tgl" ReadOnly="True" SortExpression="tgl" />
                    <asp:BoundField DataField="Kode" HeaderText="Kode Material Timbangan" SortExpression="Kode" />
                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                    <asp:BoundField DataField="TM" HeaderText="TM" SortExpression="TM" />
                    <asp:BoundField DataField="TS" HeaderText="TS" SortExpression="TS" />
                    <asp:BoundField DataField="ASH" HeaderText="ASH" SortExpression="ASH" />
                    <asp:BoundField DataField="CalDaf" HeaderText="CalDaf" SortExpression="CalDaf" />
                    <asp:BoundField DataField="CalAdb" HeaderText="CalAdb" SortExpression="CalAdb" />
                    <asp:BoundField DataField="CalAr" HeaderText="CalAr" SortExpression="CalAr" />
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select m.ID,convert(nvarchar,m.DateHistory, 111) as tgl ,m.Kode,m.Location,m.TM,m.TS,m.ASH,m.CalDaf,m.CalAdb,m.CalAr from v_MaterialHistory m, v_Material l where m.Kode = l.Kode 
                        ORDER BY M.DateHistory DESC"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select 'YEAR' as tahun union all select distinct CAST(YEAR(DateHistory) as varchar)  tahun from v_MaterialHistory"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select 'ALL' as Kode, 'ALL' as seamcargo union all SELECT [Kode], [seamcargo] FROM [tbl_qualmapping]"></asp:SqlDataSource>
        </asp:View>
        <asp:View ID="map_View1" runat="server">
            <asp:Panel ID="Panel3" runat="server" CssClass="pantool" Style="width: 880px; margin-top: 50px;">
                <div class="olet">
                </div>
                <div class="orig">
                    <asp:Literal ID="Literal1" runat="server" Text="Use this table to map and monitor &lt;br /&gt;between Alamo MaterialKode and Operational MaterialKode"></asp:Literal>
                </div>
            </asp:Panel>
            <asp:GridView ID="grid_map1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="KodeAlamo" DataSourceID="vwQual_mapmonitor1" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" PageSize="25" ShowHeaderWhenEmpty="True" Width="860px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="KodeAlamo" HeaderText="Alamo/HTS Raw Cargo" ReadOnly="True" SortExpression="KodeAlamo" />
                    <asp:BoundField DataField="KodeOperasional" HeaderText="Operational Raw Cargo" SortExpression="KodeOperasional" />
                    <asp:BoundField DataField="Deskripsi" HeaderText="Description" SortExpression="Deskripsi" />
                    <asp:BoundField DataField="LastQuality" HeaderText="Last Material Quality Setting" ReadOnly="True" SortExpression="LastQuality" />
                    <asp:BoundField DataField="isUsed" HeaderText="In Use?" ReadOnly="True" SortExpression="isUsed">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#CCCCCC" />
                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:SqlDataSource ID="vwQual_mapmonitor1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT * FROM [vwQual_mapmonitor1]"></asp:SqlDataSource>
        </asp:View>
    </asp:MultiView>

    <asp:GridView runat="server" ID="grd_pln2">
    </asp:GridView>

    <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT DISTINCT 
    CASE WHEN CAST(YEAR(dtm_qualportal) as varchar) IS NULL 
    THEN CAST(YEAR(GETDATE()) as varchar) 
    ELSE CAST(YEAR(dtm_qualportal) as varchar)  END tahun FROM dbo.tbl_qualportal"
        ID="SqlDataSource4"></asp:SqlDataSource>

    <asp:SqlDataSource ID="ds_kon1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
        SelectCommand="SELECT ' ALL' As [Kode] UNION ALL SELECT [Kode]  FROM [v_Kontraktor] where Kode != 'ADARO' ORDER BY Kode "></asp:SqlDataSource>

    <asp:SqlDataSource ID="ds_prod1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
        SelectCommand="SELECT NULL [id_ref_master], 'ALL' [ref_name] UNION ALL
                        SELECT [id_ref_master], [ref_name] FROM [tbl_ref_master] WHERE ([id_ref_type] =5)"></asp:SqlDataSource>

    <asp:SqlDataSource ID="ds_prod2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
        SelectCommand="SELECT NULL [id_ref_master], ' ' [ref_name] UNION ALL
                        SELECT [id_ref_master], [ref_name] FROM [tbl_ref_master] WHERE ([id_ref_type] =5)"></asp:SqlDataSource>

    <asp:Label ID="sqlKeeper" runat="server" Visible="False"></asp:Label>
    <asp:Label runat="server" ForeColor="Transparent" ID="sqlkeep1"></asp:Label>
    <asp:Label ID="xlskeep1" runat="server" Text="" ForeColor="Transparent"></asp:Label>

    <asp:LinkButton Style="visibility: hidden" ID="CloseLink" runat="server" OkControlID="CloseLink">Close Proxy</asp:LinkButton>
    <asp:LinkButton Style="visibility: hidden" ID="CancelLink" runat="server" CancelControlID="CancelLink">Cancel Proxy</asp:LinkButton>

    <script type="text/javascript" src="tabSlideOut/jquery.tabSlideOut.v1.3.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.slide-export1').tabSlideOut({
                tabHandle: '.handleexp1',                     //class of the element that will become your tab
                pathToTabImage: 'tabSlideOut/images/export_hrz.png', //path to the image for the tab //Optionally can be set using css
                imageHeight: '40px',                     //height of tab image           //Optionally can be set using css
                imageWidth: '122px',                       //width of tab image            //Optionally can be set using css
                tabLocation: 'bottom',                     //side of screen where tab lives, top, right, bottom, or left
                speed: 300,                               //speed of animation
                action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                topPos: '0px',                          //position from the top/ use if tabLocation is left or right
                leftPos: '200px',                          //position from left/ use if tabLocation is bottom or top
                fixedPosition: true                      //options: true makes it stick(fixed position) on scroll
            });

        });
    </script>

    <div class="slide-export1">
        <a class="handleexp1" href="http://link-for-non-js-users.html">Content</a>
        <asp:ImageButton ID="rcpXls1" runat="server" ImageUrl="~/HTS2/images/document_microsoft_excel_01.png" Width="24" />
        <asp:ImageButton ID="rcpPdf1" runat="server" ImageUrl="~/HTS2/images/pdf.png" Width="24" />
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="SideContent1" runat="Server">
    <asp:Panel ID="menuPanel1" runat="server"></asp:Panel>
</asp:Content>

