<%@ Page Title="" Language="C#" MasterPageFile="~/HTS2/Site.master" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="HTS2_Default4" MaintainScrollPositionOnPostback="true" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

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
                    <asp:DropDownList ID="ddl_cyear1" runat="server" AutoPostBack="True" DataSourceID="ds_year" DataTextField="cotyear" DataValueField="cotyear">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_date1" runat="server" Text="Weekly Plan"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddl_wpl1" runat="server" DataSourceID="ds_plan_containr" DataTextField="week1" DataValueField="id_qualplan_containr">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Material</td>
                <td>
                    <asp:DropDownList ID="ddl_mat1" runat="server" DataSourceID="ds_material1" DataTextField="seamcargo" DataValueField="Kode">
                    </asp:DropDownList>
                </td>
                <td>Product</td>
                <td>
                    <asp:DropDownList ID="ddl_prod1" runat="server" DataSourceID="ds_product1" DataTextField="ref_name" DataValueField="id_ref_master">
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

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="sum_View1" runat="server">

            <asp:SqlDataSource ID="ds_ptr_sum1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT * FROM [tbl_qual_ptrprojection]"></asp:SqlDataSource>

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
                <div runat="server" id="div_setContainr_btn" visible="true">
                    <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/HTS2/images/date_add.png" Width="24" />
                        </div>
                        <div class="btn_lbl">Add Schedule Container</div>
                    </div>
                     <div class="container">
                        <div style="float: left">
                            <asp:ImageButton ID="ibt_upload_call1" runat="server" ImageUrl="~/HTS2/images/move_waiting_up.png" Width="24" />
                        </div>
                        <div class="btn_lbl">Upload PTR Data</div>
                    </div>
                </div>
            </div>

            <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                Enabled="True" OkControlID="CloseLink" PopupControlID="addContainer1" PopupDragHandleControlID="PopupHeader"
                TargetControlID="ImageButton1" DynamicServicePath="">
            </ajx:ModalPopupExtender>

            <div id="addContainer1" runat="server" >
                <asp:Panel ID="Panel6" runat="server">
                    <div class="PopupHeader" style="color: black; padding: 4px 0 0 5px;">
                        Add New Projection Plan Period
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel7" runat="server">
                    <div class="PopupBody" style="background-color: #CCCCCC; vertical-align: middle; padding: 4px 0 0 5px; width: inherit; height: 35px;">
                        <asp:TextBox ID="txb_sched" runat="server" Height="20px" ToolTip="Set Weekly Schedule Start Date"></asp:TextBox>
                        <asp:Button ID="btn_submit_containr" runat="server" Text="Submit" OnClick="btn_submit_containr_Click" />
                        <ajx:CalendarExtender ID="txt_sched_CalendarExtender" runat="server" Enabled="True" TargetControlID="txb_sched"></ajx:CalendarExtender>

                    </div>
                </asp:Panel>
            </div>

              <ajx:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                Enabled="True" OkControlID="CloseLink" PopupControlID="pan_wpj1" PopupDragHandleControlID="PopupHeader"
                TargetControlID="ibt_upload_call1" DynamicServicePath="">
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
                                </td>
                                <td style="width: 280px; vertical-align: top;">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Navy"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="ibt_upload_ptr1" runat="server" CommandName="Insert" OnClientClick="$('CloseLink').click();" Text="Upload" OnClick="ibt_upload_ptr1_Click"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" CommandName="Cancel" OnClientClick="$('CancelLink').click();" Text="Cancel"></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>

        </asp:View>

        <asp:View ID="low_View1" runat="server">





            <asp:GridView ID="grid_ptr_low1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id_prtprojection" DataSourceID="ds_ptr_low1" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="1000px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="id_prtprojection" HeaderText="id_prtprojection" InsertVisible="False" ReadOnly="True" SortExpression="id_prtprojection" Visible="False" />
                    <asp:BoundField DataField="id_ptr_area" HeaderText="Area" SortExpression="id_ptr_area" />
                    <asp:BoundField DataField="MaterialKode1" HeaderText="Material" SortExpression="MaterialKode1">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ptr_ton_available" HeaderText="Tons" SortExpression="ptr_ton_available" />
                    <asp:BoundField DataField="ptr_TM" DataFormatString="{0:N2}" HeaderText="TM" HtmlEncode="False" SortExpression="ptr_TM" />
                    <asp:BoundField DataField="ptr_IM" DataFormatString="{0:N2}" HeaderText="IM" HtmlEncode="False" SortExpression="ptr_IM" />
                    <asp:BoundField DataField="ptr_AshAdb" DataFormatString="{0:N2}" HeaderText="Ash adb" HtmlEncode="False" SortExpression="ptr_AshAdb" />
                    <asp:BoundField DataField="ptr_AshAdb" DataFormatString="{0:N2}" HeaderText="Ash ar" HtmlEncode="False" SortExpression="ptr_AshAdb" />
                    <asp:BoundField DataField="ptr_TSAdb" DataFormatString="{0:N2}" HeaderText="TS adb" HtmlEncode="False" SortExpression="ptr_TSAdb" />
                    <asp:BoundField DataField="ptr_TSAdb" DataFormatString="{0:N2}" HeaderText="TS ar" HtmlEncode="False" SortExpression="ptr_TSAdb" />
                    <asp:BoundField DataField="ptr_CVDaf" DataFormatString="{0:N0}" HeaderText="CalDaf" HtmlEncode="False" SortExpression="ptr_CVDaf" />
                    <asp:BoundField DataField="ptr_CVadb" DataFormatString="{0:N0}" HeaderText="Cal adb" HtmlEncode="False" SortExpression="ptr_CVadb" />
                    <asp:BoundField DataField="ptr_CVadb" DataFormatString="{0:N0}" HeaderText="CV (ar)" HtmlEncode="False" SortExpression="ptr_CVadb" />
                    <asp:BoundField DataField="ptr_HGI" DataFormatString="{0:N0}" HeaderText="HGI" HtmlEncode="False" SortExpression="ptr_HGI" />
                    <asp:BoundField DataField="ptr_ton_available" HeaderText="Percentage" SortExpression="ptr_ton_available" />
                    <asp:BoundField DataField="ptr_ton_available" HeaderText="Contractor Ability" SortExpression="ptr_ton_available" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <div style="text-decoration: none; border-style: none;">
                                <asp:HyperLink ID="HyperLink1" runat="server" BorderStyle="None" Font-Underline="false" ImageUrl="~/Images/table_edit (1).png" NavigateUrl='<%# String.Concat("Default3?v=0&idwpr1=", Eval("id_ptrprojection"), "&idctr=", Eval("id_qualplan_containr"))%>' onClick="window.open(this.href, this.target,'status=1,scrollbars=1,resizable=1,width=700,height=600,left=10,top=10'); return false;" Target='<%# "wpr1det" + Eval("id_ptrprojection") %>' Text="">
                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbWpr1" runat="server" />
                            <asp:Label ID="lbl_idwpr1" runat="server" Text='<%# Eval("id_prtprojection")%>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSelallLow1" runat="server" AutoPostBack="true" OnCheckedChanged="cbSelallLow1_CheckedChanged" />
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





            <asp:SqlDataSource ID="ds_ptr_low1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT * FROM [tbl_qual_ptrprojection]"></asp:SqlDataSource>





        </asp:View>
        <asp:View ID="med_View1" runat="server">
        </asp:View>
        <asp:View ID="hi_View1" runat="server">
        </asp:View>
    </asp:MultiView>



    <div id="datasourcesection">
        <asp:SqlDataSource ID="ds_year" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT DISTINCT 
                                                    CASE WHEN 
                                                    YEAR([plan_startdate]) IS NULL THEN YEAR(GETDATE())
                                                    ELSE
             
                                             YEAR([plan_startdate]) END cotyear FROM [tbl_qualplan_containr]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="ds_plan_containr" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT id_qualplan_containr, ' (' + CONVERT(nvarchar(25), id_qualplan_containr) +') ' +CONVERT(nvarchar(25), plan_startdate, 101) + ' - ' + CONVERT(nvarchar(25), plan_enddate, 101) as week1
                                        FROM [tbl_qualplan_containr]
                                        Where (PlanTypeKode = 13) AND YEAR(plan_startdate) = @planyear
                                        ORDER BY plan_enddate DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddl_cyear1" Name="planyear" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ds_material1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select 'ALL' as Kode, 'ALL' as seamcargo union all SELECT [Kode], [seamcargo] FROM [tbl_qualmapping]"></asp:SqlDataSource>

        <asp:SqlDataSource ID="ds_product1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
            SelectCommand="SELECT NULL [id_ref_master], 'ALL' [ref_name] UNION ALL
                        SELECT [id_ref_master], [ref_name] FROM [tbl_ref_master] WHERE ([id_ref_type] =5)"></asp:SqlDataSource>

    </div>

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
        <asp:ImageButton ID="ibt_xls1" runat="server" ImageUrl="~/HTS2/images/document_microsoft_excel_01.png" Width="24" />
        <asp:ImageButton ID="ibt_pdf1" runat="server" ImageUrl="~/HTS2/images/pdf.png" Width="24" />
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SideContent1" runat="Server">
</asp:Content>

