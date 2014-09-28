<%@ Page Title="Passing Plan Km. 29 " Language="C#" MasterPageFile="~/HTS2/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="pspln29.aspx.cs" Inherits="HTS2_pspln29" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="Stylesheet" href="css_ajax_tabcontainer.css" type="text/css" />
    <style type="text/css">
        .pnlCSS {
            font-weight: normal;
            cursor: pointer;
            border: solid 1px #c0c0c0;
            width: 250px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <div style="float: left; margin-bottom: 25px;">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Barging Plan Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_date" runat="server"></asp:TextBox>
                    <ajx:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" TargetControlID="txt_date" Format="MM/dd/yyyy"></ajx:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btn_brgPln" runat="server" Text="Submit" OnClick="btn_brgPln_Click" />
                </td>
            </tr>
        </table>
    </div>



    <div style="float: right; display: inline-block; margin-right: 90px;">
        <div style="margin-bottom: 20px; float: left;">
            <asp:Panel ID="pnlClick" runat="server" CssClass="pnlCSS">
                <div style="background-image: url('images/tabs/blueGrad.jpg'); height: 30px; vertical-align: middle; background-repeat: repeat-x;">
                    <div style="float: left; color: White; padding: 5px 5px 5px 0; margin-left: 10px;">
                        Option Mapping
                    </div>
                    <div style="float: right; color: White; padding: 5px 5px 0 0; display: inline;">
                        <asp:Image ID="imgArrows" runat="server" ImageAlign="AbsMiddle" />
                        <asp:Label ID="lblMessage" runat="server" Text="Label" />
                    </div>
                    <div style="clear: both"></div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlCollapsable" runat="server" Height="0px" CssClass="pnlCSS">
                <asp:GridView ID="grid_opsi1" runat="server" ForeColor="#333333" Style="margin-right: 0px" Width="250px">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="35px" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:Panel>
            <ajx:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
                CollapseControlID="pnlClick" Collapsed="True" ExpandControlID="pnlClick"
                TextLabelID="lblMessage" CollapsedText="Show" ExpandedText="Hide"
                ImageControlID="imgArrows" CollapsedImage="images/collapse.png" ExpandedImage="images/expand.png"
                TargetControlID="pnlCollapsable" Enabled="True">
            </ajx:CollapsiblePanelExtender>
        </div>
    </div>

    <ajx:TabContainer ID="tabcons" runat="server" Width="1060px" ActiveTabIndex="0" CssClass="ajax__tab_blueGrad-theme">

        <ajx:TabPanel ID="bargetab1" runat="server" HeaderText="Barge Planning">
            <ContentTemplate>

                <asp:Panel runat="server" ID="pan_tool1" Style="width: 510px; margin-top: 25px;" CssClass="pantool">
                    <div class="olet">
                        <asp:ImageButton ID="ibt_chart1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/HTS2/images/kchart.png" OnLoad="ibt_chart1_Load" />
                        <asp:ImageButton ID="ibt_xls1" runat="server" ImageUrl="~/Images/hts_nu_icons/document_microsoft_excel_01.png" OnClick="ibt_xls1_Click" ImageAlign="AbsMiddle" />
                        <asp:ImageButton ID="ibt_pdf1" runat="server" ImageUrl="~/Images/hts_nu_icons/pdf.png" OnClick="ibt_pdf1_Click" ImageAlign="AbsMiddle" />
                    </div>
                    <div style="float: right" class="orig" runat="server" id="div_upload1" visible="False">
                        <asp:ImageButton ID="templt1" runat="server" ImageUrl="~/HTS2/images/table.png" AlternateText="Upload Template" OnClick="templt1_Click" ImageAlign="AbsMiddle" />
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/hts_nu_icons/move_waiting_up.png" ImageAlign="AbsMiddle" />
                    </div>
                </asp:Panel>

                <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                    Enabled="True" OkControlID="CloseLink" PopupControlID="pan_load67" PopupDragHandleControlID="PopupHeader"
                    TargetControlID="ImageButton4" DynamicServicePath="">
                </ajx:ModalPopupExtender>

                <asp:GridView ID="grd_brgPln" runat="server" AutoGenerateColumns="False" DataSourceID="ds_sp_brgPlnShf1"
                    ForeColor="#333333" Width="500px" DataKeyNames="id_PlnBrg" OnRowDataBound="grd_brgPln_RowDataBound" OnRowCommand="grd_brgPln_RowCommand" Style="margin-right: 0px" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id_PlnBrg" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" Visible="False" />
                        <asp:BoundField DataField="dt" HeaderText="Date" InsertVisible="False" ReadOnly="True" SortExpression="dt" Visible="False" />
                        <asp:BoundField DataField="brg_shift" HeaderText="Shift" SortExpression="brg_shift" />
                        <asp:BoundField DataField="tgl" HeaderText="Hour" InsertVisible="False" ReadOnly="True" SortExpression="Hour" />
                        <asp:TemplateField HeaderText="Product K1" SortExpression="jettyk1">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl_k1" runat="server" SelectedValue='<%# Eval("idk1") %>' DataSourceID="ds_prod_master" DataTextField="ref_name" DataValueField="id_ref_master"></asp:DropDownList>
                                <asp:Label ID="lbl_k1" runat="server" Text='<%# Eval("K1") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="100px" />
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product K3" SortExpression="jettyk3">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl_k3" runat="server" SelectedValue='<%# Eval("idk3") %>' DataSourceID="ds_prod_master" DataTextField="ref_name" DataValueField="id_ref_master"></asp:DropDownList>
                                <asp:Label ID="lbl_k3" runat="server" Text='<%# Eval("K3") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="100px" />
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="K1" HeaderText="Product K1" SortExpression="K1" Visible="False" />
                        <asp:BoundField DataField="K3" HeaderText="Product K3" SortExpression="K3" Visible="False" />
                        <asp:TemplateField HeaderText="Blending Options">
                            <HeaderTemplate>
                                Blending Options<br />
                                (Click to see details)
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="false"
                                    NavigateUrl='<%# String.Concat("EditSMB.aspx?idp=", Eval("id_PlnBrg")) %>'
                                    onClick="window.open(this.href, this.target); window.focus();"
                                    Target='<%# String.Concat("_blend", Eval("id_PlnBrg")) %>'
                                    Text='<%# Eval("opsiName") %>'>
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibt_save1" runat="server" ImageUrl="~/HTS2/images/save_01.png" CommandName="_save" CommandArgument='<%# Eval("id_PlnBrg") %>' />
                            </ItemTemplate>
                            <ControlStyle Font-Underline="False" Width="16px" />
                            <ItemStyle Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="opsiName" HeaderText="Blending Options" SortExpression="opsiName" Visible="False" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt" Height="35px" Font-Names="Arial" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                <asp:SqlDataSource ID="ds_sp_brgPlnShf1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                    SelectCommand="sp_brgPlnShf1" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txt_date" Name="tgl" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="ds_prod_master" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                    SelectCommand="SELECT NULL as [id_ref_master], '-' as [ref_name]
                                UNION ALL
                                SELECT [id_ref_master], [ref_name] FROM [tbl_ref_master] WHERE ([id_ref_type] = @id_ref_type)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="5" Name="id_ref_type" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>


            </ContentTemplate>

        </ajx:TabPanel>

        <ajx:TabPanel ID="plantab1" runat="server" HeaderText="Passing Plan Shift 1">

            <ContentTemplate>

                <asp:Panel runat="server" ID="pan_tool2" Style="width: 928px; margin-top: 25px;" CssClass="pantool">
                    <div class="olet">
                        <asp:ImageButton ID="ibt_xls2" runat="server" ImageUrl="~/Images/hts_nu_icons/document_microsoft_excel_01.png" OnClick="ibt_xls2_Click" />
                        <asp:ImageButton ID="ibt_pdf2" runat="server" ImageUrl="~/Images/hts_nu_icons/pdf.png" OnClick="ibt_pdf2_Click" />
                    </div>
                    <div class="orig">
                        <asp:Label ID="lbl_infosent1" runat="server" ForeColor="Maroon" Font-Size="9px"></asp:Label>
                        <asp:ImageButton ID="ibt_send1" runat="server" ImageUrl="~/HTS2/images/mail_replied.png" AlternateText="Click to send broadcast" OnClick="ibt_send1_Click" Visible="False" />
                    </div>
                </asp:Panel>

                <asp:GridView ID="grid_plan1" runat="server" AutoGenerateColumns="False" DataSourceID="ds_sp_planpass_rep1" CellPadding="2" ShowHeaderWhenEmpty="True" Width="930px" OnRowDataBound="grid_plan1_RowDataBound" EmptyDataText="Sorry, Data not Found">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Kontraktor" ReadOnly="True" SortExpression="KontraktorKode" />
                        <asp:BoundField DataField="MaterialKode" HeaderText="Seam" ReadOnly="True" SortExpression="MaterialKode" />
                        <asp:BoundField DataField="06.00-07.00" HeaderText="06.00-07.00" ReadOnly="True" SortExpression="06.00-07.00" />
                        <asp:BoundField DataField="07.00-08.00" HeaderText="07.00-08.00" ReadOnly="True" SortExpression="07.00-08.00" />
                        <asp:BoundField DataField="08.00-09.00" HeaderText="08.00-09.00" ReadOnly="True" SortExpression="08.00-09.00" />
                        <asp:BoundField DataField="09.00-10.00" HeaderText="09.00-10.00" ReadOnly="True" SortExpression="09.00-10.00" />
                        <asp:BoundField DataField="10.00-11.00" HeaderText="10.00-11.00" ReadOnly="True" SortExpression="10.00-11.00" />
                        <asp:BoundField DataField="11.00-12.00" HeaderText="11.00-12.00" ReadOnly="True" SortExpression="11.00-12.00" />
                        <asp:BoundField DataField="12.00-13.00" HeaderText="12.00-13.00" ReadOnly="True" SortExpression="12.00-13.00" />
                        <asp:BoundField DataField="13.00-14.00" HeaderText="13.00-14.00" ReadOnly="True" SortExpression="13.00-14.00" />
                        <asp:BoundField DataField="14.00-15.00" HeaderText="14.00-15.00" ReadOnly="True" SortExpression="14.00-15.00" />
                        <asp:BoundField DataField="15.00-16.00" HeaderText="15.00-16.00" ReadOnly="True" SortExpression="15.00-16.00" />
                        <asp:BoundField DataField="16.00-17.00" HeaderText="16.00-17.00" ReadOnly="True" SortExpression="16.00-17.00" />
                        <asp:BoundField DataField="17.00-18.00" HeaderText="17.00-18.00" ReadOnly="True" SortExpression="17.00-18.00" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt" Height="35px" Font-Names="Arial" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                <asp:SqlDataSource ID="ds_sp_planpass_rep1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_planpass_rep1" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="dtm" Type="String" />
                        <asp:Parameter Name="shf" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </ContentTemplate>

        </ajx:TabPanel>

        <ajx:TabPanel ID="plantab2" runat="server" HeaderText="Passing Plan Shift 2">

            <ContentTemplate>

                <asp:Panel runat="server" ID="Panel1" Style="width: 928px; margin-top: 25px;" CssClass="pantool">
                    <div class="olet">
                        <asp:ImageButton ID="ibt_xls3" runat="server" ImageUrl="~/Images/hts_nu_icons/document_microsoft_excel_01.png" OnClick="ibt_xls3_Click" />
                        <asp:ImageButton ID="ibt_pdf3" runat="server" ImageUrl="~/Images/hts_nu_icons/pdf.png" OnClick="ibt_pdf3_Click" />
                    </div>
                    <div class="orig">
                        <asp:Label ID="lbl_infosent2" runat="server" ForeColor="Maroon" Font-Size="9px"></asp:Label>
                        <asp:ImageButton ID="ibt_send2" runat="server" ImageUrl="~/HTS2/images/mail_replied.png" AlternateText="Click to send broadcast" OnClick="ibt_send2_Click" Visible="False" />
                    </div>
                </asp:Panel>

                <asp:GridView ID="grid_plan2" runat="server" AutoGenerateColumns="False" DataSourceID="ds_sp_passplan_rep2" CellPadding="2" ShowHeaderWhenEmpty="True" Width="930px" OnRowDataBound="grid_plan2_RowDataBound" EmptyDataText="Sorry, Data not Found">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Kontraktor" ReadOnly="True" SortExpression="KontraktorKode" />
                        <asp:BoundField DataField="MaterialKode" HeaderText="Seam" ReadOnly="True" SortExpression="MaterialKode" />
                        <asp:BoundField DataField="18.00-19.00" HeaderText="18.00-19.00" ReadOnly="True" SortExpression="18.00-19.00" />
                        <asp:BoundField DataField="19.00-20.00" HeaderText="19.00-20.00" ReadOnly="True" SortExpression="19.00-20.00" />
                        <asp:BoundField DataField="20.00-21.00" HeaderText="20.00-21.00" ReadOnly="True" SortExpression="20.00-21.00" />
                        <asp:BoundField DataField="21.00-22.00" HeaderText="09.00-10.00" ReadOnly="True" SortExpression="09.00-10.00" />
                        <asp:BoundField DataField="22.00-23.00" HeaderText="22.00-23.00" ReadOnly="True" SortExpression="22.00-23.00" />
                        <asp:BoundField DataField="23.00-00.00" HeaderText="23.00-00.00" ReadOnly="True" SortExpression="23.00-00.00" />
                        <asp:BoundField DataField="00.00-01.00" HeaderText="00.00-01.00" ReadOnly="True" SortExpression="00.00-01.00" />
                        <asp:BoundField DataField="01.00-02.00" HeaderText="01.00-02.00" ReadOnly="True" SortExpression="01.00-02.00" />
                        <asp:BoundField DataField="02.00-03.00" HeaderText="02.00-03.00" ReadOnly="True" SortExpression="02.00-03.00" />
                        <asp:BoundField DataField="03.00-04.00" HeaderText="03.00-04.00" ReadOnly="True" SortExpression="03.00-04.00" />
                        <asp:BoundField DataField="04.00-05.00" HeaderText="04.00-05.00" ReadOnly="True" SortExpression="04.00-05.00" />
                        <asp:BoundField DataField="05.00-06.00" HeaderText="05.00-06.00" ReadOnly="True" SortExpression="05.00-06.00" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt" Height="35px" Font-Names="Arial" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

                <asp:SqlDataSource ID="ds_sp_passplan_rep2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_planpass_rep1" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="dtm" Type="String" />
                        <asp:Parameter Name="shf" Type="Int32" DefaultValue="2" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </ContentTemplate>

        </ajx:TabPanel>

    </ajx:TabContainer>




    <asp:LinkButton Style="visibility: hidden" ID="CloseLink" runat="server" OkControlID="CloseLink">Close Proxy</asp:LinkButton>
    <asp:LinkButton Style="visibility: hidden" ID="CancelLink" runat="server" CancelControlID="CancelLink">Cancel Proxy</asp:LinkButton>


    <asp:Panel ID="pan_load67" runat="server">
        <div class="HellowWorldPopup">
            <div class="PopupHeader" id="PopupHeader">
                Upload Barging Plan Data
            </div>
            <div class="PopupBody" style="background-color: #CCCCCC">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="dte_txt" runat="server"></asp:TextBox>
                            <ajx:CalendarExtender ID="TextBox1_CalendarExtender0" runat="server" TargetControlID="dte_txt" Format="MM/dd/yyyy">
                            </ajx:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="File Location"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" class="file" /><br />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="PopupCmd">
                <asp:LinkButton ID="lbt_upl67" OnClientClick="$('CloseLink').click();" runat="server" CommandName="Insert" Text="Upload" OnClick="lbt_upl67_Click"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="LinkButton4_Click"></asp:LinkButton>
            </div>
        </div>
    </asp:Panel>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SideContent1" runat="Server">
    <asp:Panel ID="passmenu1" runat="server"></asp:Panel>
    <asp:Panel ID="rommenu1" runat="server"></asp:Panel>

</asp:Content>

