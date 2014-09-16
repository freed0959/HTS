<%@ Page Title="Page" Language="C#" MasterPageFile="~/HTS2/Site.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="EditSMB.aspx.cs" Inherits="HTS2_EditSMB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">

    <div style="float: right; display: inline-block; margin-right: 90px;">
        <div style="margin-bottom: 20px; float: left;">
            <asp:Panel ID="pnlClick" runat="server" CssClass="pnlCSS">
                <div style="background-image: url('images/tabs/blueGrad.jpg'); height: 30px; vertical-align: middle; background-repeat: repeat-x;">
                    <div style="float: left; color: White; padding: 5px 5px 5px 0; margin-left: 10px;">
                        Barging Plan Info
                    </div>
                    <div style="float: right; color: White; padding: 5px 5px 0 0; display: inline;">
                        <asp:Image ID="imgArrows" runat="server" ImageAlign="AbsMiddle" />
                        <asp:Label ID="lblMessage" runat="server" Text="Label" />
                    </div>
                    <div style="clear: both"></div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlCollapsable" runat="server" Height="0px" CssClass="pnlCSS">
                <asp:GridView ID="grid_info1" runat="server" CellPadding="4" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="350px" GridLines="Vertical" OnRowDataBound="grid_info1_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006666" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" ForeColor="White" Height="15px" HorizontalAlign="Center" Wrap="False" />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript" src="tabSlideOut/jquery.tabSlideOut.v1.3.js"></script>

    <ajx:TabContainer runat="server" Width="1060px" ActiveTabIndex="1" CssClass="ajax__tab_blueGrad-theme" ID="tab29con">

        <ajx:TabPanel ID="tab1" runat="server" HeaderText="K1 Blending Scheme">
            <ContentTemplate>

                <asp:GridView ID="grd_mtrSkmBlnd" runat="server" DataSourceID="ds_SP_MtrSkmBlndg" AutoGenerateColumns="False" DataKeyNames="ID_actMB" Width="880px"
                    CellPadding="2" ShowHeaderWhenEmpty="True" GridLines="Horizontal" OnRowDataBound="grd_mtrSkmBlnd_RowDataBound" EmptyDataText="Sorry, Data not Found" OnDataBound="grd_mtrSkmBlnd_DataBound">
                    <Columns>
                        <asp:BoundField HeaderText="ID_actMB" SortExpression="ID_actMB" Visible="False" />
                        <asp:BoundField DataField="ID_PlnBrg" HeaderText="ID_PlnBrg" SortExpression="ID_PlnBrg" Visible="False" />
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Contractor" SortExpression="KontraktorKode">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material" SortExpression="MaterialKode">
                            <ItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ds_mtrl" DataTextField="Kode" DataValueField="Kode" SelectedValue='<%# Bind("MaterialKode") %>'></asp:DropDownList>
                                <asp:Label ID="lbl_mtLama" runat="server" Text='<%# Eval("MaterialKode") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="150px" Font-Names="Arial" Font-Size="8pt" />
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Truck Count" SortExpression="TruckCount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_trclama" runat="server" Text='<%# Eval("TruckCount") %>' Visible="False" Font-Bold="true" Font-Size="9px" Font-Names="Arial"></asp:Label>
                                <asp:TextBox ID="txt_trCount" runat="server" Text='<%# Bind("TruckCount") %>' CausesValidation="True"></asp:TextBox>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_trCount" Display="Dynamic" ErrorMessage="Must a number"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"></asp:CompareValidator>
                            </ItemTemplate>
                            <ControlStyle Width="50px" Font-Names="Arial" Font-Size="8pt" />
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="MaterialKode" HeaderText="Material" SortExpression="MaterialKode" Visible="False" />
                        <asp:BoundField DataField="TruckCount" HeaderText="Truck Count" SortExpression="TruckCount" DataFormatString="{0:N0}" Visible="False" />

                        <asp:BoundField DataField="ton" HeaderText="Tonnage" SortExpression="ton" NullDisplayText="-" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="TM" HeaderText="TM" SortExpression="TM" NullDisplayText="-" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="ASH" HeaderText="ASH" SortExpression="ASH" NullDisplayText="-" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="TS" HeaderText="TS" SortExpression="TS" NullDisplayText="-" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="CalAr" HeaderText="CalAr" SortExpression="CalAr" NullDisplayText="-" DataFormatString="{0:N0}" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbRows" runat="server" />
                                <asp:Label ID="lbl_idAct" runat="server" Text='<%# Eval("ID_actMB") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_kntr" runat="server" Text='<%# Eval("KontraktorKode") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="16px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#CCCCCC" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="ds_SP_MtrSkmBlndg" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                    SelectCommand="SP_MtrSkmBlndg" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="id_pln" QueryStringField="idp" Type="Int64" />
                        <asp:Parameter DefaultValue="42" Name="des" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:Panel ID="Panel1" runat="server">
                    <div class="PopupHeader" id="PopupHeader">Upload Blending Scheme for K1 </div>
                    <div class="PopupBody">
                        <asp:FileUpload ID="fup_k1" runat="server" />
                    </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="link_save" OnClientClick="$('CloseLink').click();" runat="server" CommandName="Insert" Text="Save" OnClick="link_save_Click"></asp:LinkButton><asp:LinkButton ID="LinkButton1" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </asp:Panel>

                <ajx:ModalPopupExtender ID="ModalPE1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                    Enabled="True" OkControlID="CloseLink" PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader"
                    TargetControlID="btn_addMat" DynamicServicePath="">
                </ajx:ModalPopupExtender>

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
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-schemablend-ctrk1">
                    <a class="handlek1" href="http://link-for-non-js-users.html">Content</a>
                    <div runat="server" id="div_btn1">
                         <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ibt_update1" runat="server" ImageUrl="~/HTS2/images/save_01.png" OnClick="ibt_update1_Click" ToolTip="Update Data" Width="24px" />
                                </div>
                            <div class="btn_lbl">Save Update(s)</div>
                        </div>
                        <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ibt_delete1" runat="server" ImageUrl="~/HTS2/images/trash_can3_delete.png" OnClick="ibt_delete1_Click" ToolTip="Delete Selected Rows" Width="26px" OnClientClick="return confirm('Are you sure you want to delete this material(s)?');" />
                                </div>
                            <div class="btn_lbl">Delete Record(s)</div>
                        </div>

                    </div>
                    <div runat="server" id="div_upload1">
                         <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ibt_templ1" runat="server" AlternateText="Upload Template" ImageUrl="~/HTS2/images/table.png" OnClick="template_Click" Width="24px" />
                                  </div>
                            <div class="btn_lbl">Upload Template</div>
                        </div>
                        <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="btn_addMat" runat="server" ImageUrl="~/HTS2/images/move_waiting_up.png" AlternateText="Click To Add New Material" Width="24px" />
                                </div>
                            <div class="btn_lbl">Upload Data</div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-schemablend-sumk1').tabSlideOut({
                            tabHandle: '.handlesumk1',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/k1_summary.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '390px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-schemablend-sumk1">
                    <a class="handlesumk1" href="http://link-for-non-js-users.html">Content</a>
                    <asp:GridView ID="grid_ratiok1" runat="server" Width="105px" OnRowDataBound="grid_ratiok1_RowDataBound" CellPadding="2">
                        <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" ForeColor="#333333" />
                        <HeaderStyle BackColor="Silver" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Wrap="True" />
                    </asp:GridView>
                </div>

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
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-export1">
                    <a class="handleexp1" href="http://link-for-non-js-users.html">Content</a>
                    <asp:ImageButton ID="ibt_xls1" runat="server" ImageUrl="~/Images/hts_nu_icons/document_microsoft_excel_01.png" OnClick="ibt_xls1_Click" Width="24px" />
                    <asp:ImageButton ID="ibt_pdf1" runat="server" ImageUrl="~/Images/hts_nu_icons/pdf.png" OnClick="ibt_pdf1_Click" Width="24px" />
                </div>

            </ContentTemplate>
        </ajx:TabPanel>

        <ajx:TabPanel ID="tab2" runat="server" HeaderText="K3 Blending Scheme">
            <ContentTemplate>

                <asp:GridView ID="grd_mtrSkmBlnd_K3" runat="server" DataSourceID="ds_SP_MtrSkmBlndg_K3" AutoGenerateColumns="False"
                    DataKeyNames="ID_actMB" Width="880px" CellPadding="2" EmptyDataText="Sorry, Data not Found" GridLines="Horizontal"
                    OnRowDataBound="grd_mtrSkmBlnd_K3_RowDataBound" ShowHeaderWhenEmpty="True" OnDataBound="grd_mtrSkmBlnd_K3_DataBound">
                    <Columns>
                        <asp:BoundField DataField="ID_actMB" HeaderText="ID_actMB" SortExpression="ID_actMB" Visible="False" />
                        <asp:BoundField DataField="ID_PlnBrg" HeaderText="ID_PlnBrg" SortExpression="ID_PlnBrg" Visible="False" />
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Contractor" SortExpression="KontraktorKode">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material" SortExpression="MaterialKode">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl_mtrK3" runat="server" DataSourceID="ds_mtrl" DataTextField="Kode" DataValueField="Kode" SelectedValue='<%# Bind("MaterialKode") %>'></asp:DropDownList>
                                <asp:Label ID="lbl_mtLamaK3" runat="server" Text='<%# Bind("MaterialKode") %>' Visible="false"></asp:Label>

                            </ItemTemplate>
                            <ControlStyle Width="150px" Font-Names="Arial" Font-Size="8pt" />
                            <HeaderStyle Width="150px" />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Truck Count" SortExpression="TruckCount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_trcK3" runat="server" Text='<%# Eval("TruckCount") %>' Visible="false" Font-Bold="true" Font-Size="9px" Font-Names="Arial"></asp:Label>
                                <asp:TextBox ID="txt_trCountK3" runat="server" Text='<%# Bind("TruckCount") %>'></asp:TextBox>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_trCountK3" Display="Dynamic" ErrorMessage="Must a number"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"></asp:CompareValidator>

                            </ItemTemplate>
                            <ControlStyle Width="50px" Font-Names="Arial" Font-Size="8pt" />
                            <HeaderStyle Width="80px" />

                        </asp:TemplateField>

                        <asp:BoundField DataField="MaterialKode" HeaderText="Material" SortExpression="MaterialKode" Visible="False" />
                        <asp:BoundField DataField="TruckCount" HeaderText="Truck Count" SortExpression="TruckCount" DataFormatString="{0:N0}" Visible="False" />

                        <asp:BoundField DataField="ton" HeaderText="Tonnage" SortExpression="TruckCount" NullDisplayText="-" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="TM" HeaderText="TM" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="ASH" HeaderText="ASH" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="TS" HeaderText="TS" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="CalAr" HeaderText="CalAr" SortExpression="TruckCount" NullDisplayText="-" DataFormatString="{0:N0}" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CBSelectAllK3" runat="server" AutoPostBack="True" OnCheckedChanged="CBSelectAllK3_CheckedChanged" />


                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbRowsK3" runat="server" />
                                <asp:Label ID="lbl_idActK3" runat="server" Text='<%# Bind("ID_actMB") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_kntrK3" runat="server" Text='<%# Bind("KontraktorKode") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="16px" />
                            <ItemStyle HorizontalAlign="Center" />

                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle BackColor="#CCCCCC" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="ds_SP_MtrSkmBlndg_K3" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SP_MtrSkmBlndg"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="id_pln" QueryStringField="idp" Type="Int64" />
                        <asp:Parameter DefaultValue="43" Name="des" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:Panel ID="Panel3" runat="server" Width="290px">
                    <div class="PopupHeader" id="PopupHeader1">Upload Blending Scheme for K3</div>
                    <div class="PopupBody">
                        <asp:FileUpload ID="fup_k3" runat="server" />
                    </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="link_K3" OnClientClick="$('CloseLink').click();" runat="server" CommandName="Insert" Text="Save" OnClick="link_K3_Click"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton5" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </asp:Panel>

                <ajx:ModalPopupExtender ID="Modalpopupextender1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                    Enabled="True" OkControlID="CloseLink" PopupControlID="Panel3" PopupDragHandleControlID="PopupHeader1"
                    TargetControlID="add_K3" DynamicServicePath="">
                </ajx:ModalPopupExtender>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-schemablend-ctrk1').tabSlideOut({
                            tabHandle: '.handlek3',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/controls.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '270px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-schemablend-ctrk1">
                    <a class="handlek3" href="http://link-for-non-js-users.html">Content</a>
                    <div runat="server" id="div_btn3">
                        <div class="container">
                            <div style="float: left">
                                <asp:ImageButton ID="ibt_savek3" runat="server" ImageUrl="~/HTS2/images/save_01.png" OnClick="ibt_savek3_Click" ToolTip="Update Data" Width="24px" />
                            </div>
                            <div class="btn_lbl">Save Update(s)</div>
                        </div>
                        <div class="container">
                            <div style="float: left">
                                <asp:ImageButton ID="ibt_delK3" runat="server" ImageUrl="~/HTS2/images/trash_can3_delete.png" OnClick="ibt_delK3_Click" ToolTip="Delete Selected Rows" Width="26px"
                                    OnClientClick="return confirm('Are you sure you want to delete this material(s)?');" />
                            </div>
                            <div class="btn_lbl">Delete Record(s)</div>
                        </div>

                    </div>
                    <div runat="server" id="div_upload2">
                        <div class="container">
                            <div style="float: left">
                                <asp:ImageButton ID="ibt_templ2" runat="server" AlternateText="Upload Template" ImageUrl="~/HTS2/images/table.png" OnClick="template_Click" Width="24px" />
                            </div>
                            <div class="btn_lbl">Upload Template</div>
                        </div>
                        <div class="container">
                            <div style="float: left">
                                <asp:ImageButton ID="add_K3" runat="server" ImageUrl="~/HTS2/images/move_waiting_up.png" AlternateText="Click To Upload New Blending" Width="24px" />
                            </div>
                            <div class="btn_lbl">Upload Data</div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-schemablend-sumk1').tabSlideOut({
                            tabHandle: '.handlesumk3',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/k3_summary.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '390px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-schemablend-sumk1" style="height: 100px;">
                    <a class="handlesumk3" href="http://link-for-non-js-users.html">Content</a>
                    <asp:GridView ID="grid_ratiok3" runat="server" Width="105px" OnRowDataBound="grid_ratiok3_RowDataBound" CellPadding="2">
                        <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" ForeColor="#333333" />
                        <HeaderStyle BackColor="Silver" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Wrap="True" />
                    </asp:GridView>
                </div>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-export1').tabSlideOut({
                            tabHandle: '.handleexp2',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/export_hrz.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '40px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '122px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'bottom',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '0px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '200px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-export1">
                    <a class="handleexp2" href="http://link-for-non-js-users.html">Content</a>
                    <asp:ImageButton ID="ibt_xls2" runat="server" ImageUrl="~/Images/hts_nu_icons/document_microsoft_excel_01.png" OnClick="ibt_xls2_Click" Width="24px" />
                    <asp:ImageButton ID="ibt_pdf2" runat="server" ImageUrl="~/Images/hts_nu_icons/pdf.png" OnClick="ibt_pdf2_Click" Width="24px" />
                </div>


            </ContentTemplate>
        </ajx:TabPanel>

        <ajx:TabPanel ID="tab3" runat="server" HeaderText="Stockpile Blending Scheme">
            <ContentTemplate>

                <asp:GridView ID="grdStp" runat="server" DataSourceID="ds_SP_MtrSkmBlndg_st"
                    AutoGenerateColumns="False" DataKeyNames="ID_actMB" Width="880px" EmptyDataText="Sorry Data Not Found" ShowHeaderWhenEmpty="True"
                    OnRowDataBound="grdStp_RowDataBound" CellPadding="2" GridLines="Horizontal" OnDataBound="grdStp_DataBound">
                    <Columns>
                        <asp:BoundField DataField="ID_actMB" HeaderText="ID_actMB" SortExpression="ID_actMB" Visible="False" />
                        <asp:BoundField DataField="ID_PlnBrg" HeaderText="ID_PlnBrg" InsertVisible="False" ReadOnly="True" SortExpression="ID_PlnBrg" Visible="False" />
                        <asp:BoundField DataField="KontraktorKode" HeaderText="Contractor" SortExpression="KontraktorKode">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material" SortExpression="MaterialKode">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddl_mtrSt" runat="server" DataSourceID="ds_mtrl" DataTextField="Kode" DataValueField="Kode" SelectedValue='<%# Bind("MaterialKode") %>'></asp:DropDownList>
                                <asp:Label ID="lbl_mtLamaSt" runat="server" Text='<%# Bind("MaterialKode") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="150px" Font-Names="Arial" Font-Size="8pt" />
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Truck Count" SortExpression="TruckCount">
                            <ItemTemplate>
                                <asp:Label ID="lbl_trcSp" runat="server" Text='<%# Eval("TruckCount") %>' Visible="false" Font-Bold="true" Font-Size="9px" Font-Names="Arial"></asp:Label>
                                <asp:TextBox ID="txt_trCountSt" runat="server" Text='<%# Bind("TruckCount") %>'></asp:TextBox>
                                <br />
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_trCountSt" Display="Dynamic" ErrorMessage="Must a number"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"></asp:CompareValidator>
                            </ItemTemplate>
                            <ControlStyle Width="50px" Font-Names="Arial" Font-Size="8pt" />
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="MaterialKode" HeaderText="Material" SortExpression="MaterialKode" Visible="False" />
                        <asp:BoundField DataField="TruckCount" HeaderText="Truck Count" SortExpression="TruckCount" DataFormatString="{0:N0}" Visible="False" />

                        <asp:BoundField DataField="ton" HeaderText="Tonnage" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="TM" HeaderText="TM" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="ASH" HeaderText="ASH" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="TS" HeaderText="TS" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:BoundField DataField="CalAr" HeaderText="CalAr" SortExpression="TruckCount" NullDisplayText="-" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CbSelectAllSp" runat="server" AutoPostBack="True" OnCheckedChanged="CbSelectAllSp_CheckedChanged" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CbRowsSp" runat="server" />
                                <asp:Label ID="lbl_idActSt" runat="server" Text='<%# Bind("ID_actMB") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbl_kntrSt" runat="server" Text='<%# Bind("KontraktorKode") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="16px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#CCCCCC" />
                    <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />

                </asp:GridView>

                <asp:SqlDataSource ID="ds_SP_MtrSkmBlndg_st" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                    SelectCommand="SP_MtrSkmBlndg" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="id_pln" QueryStringField="idp" Type="Int64" />
                        <asp:Parameter DefaultValue="44" Name="des" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:Panel ID="Panel5" runat="server" Width="290px">
                    <div class="PopupHeader" id="PopupHeader2">Upload Blending Scheme for Stockpile</div>
                    <div class="PopupBody">
                        <asp:FileUpload ID="fup_stockpile" runat="server" />
                    </div>
                    <div class="PopupCmd">
                        <asp:LinkButton ID="link_stp" OnClientClick="$('CloseLink').click();" runat="server" CommandName="Insert" Text="Save" OnClick="link_stp_Click"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton8" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </asp:Panel>

                <ajx:ModalPopupExtender ID="Modalpopupextender2" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
                    Enabled="True" OkControlID="CloseLink" PopupControlID="Panel5" PopupDragHandleControlID="PopupHeader2"
                    TargetControlID="ImageButton3" DynamicServicePath="">
                </ajx:ModalPopupExtender>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-schemablend-ctrk1').tabSlideOut({
                            tabHandle: '.handlestck',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/controls.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '270px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-schemablend-ctrk1">
                    <a class="handlestck" href="http://link-for-non-js-users.html">Content</a>
                    <div runat="server" id="div_btnsp">
                           <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ibt_saveSp" runat="server" ImageUrl="~/HTS2/images/save_01.png" OnClick="ibt_saveSp_Click" ToolTip="Update Data" Width="24px" />
                                </div>
                            <div class="btn_lbl">Save Update(s)</div>
                        </div>
                        <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ibt_delSp" runat="server" ImageUrl="~/HTS2/images/trash_can3_delete.png" OnClick="ibt_delSp_Click" ToolTip="Delete Selected Rows" Width="26px" OnClientClick="return confirm('Are you sure you want to delete this material(s)?');" />
                                </div>
                            <div class="btn_lbl">Delete Record(s)</div>  
                            </div>                      
                    </div>
                    <div runat="server" id="div_upload3">
                         <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ibt_templ3" runat="server" AlternateText="Upload Template" ImageUrl="~/HTS2/images/table.png" OnClick="template_Click" Width="24px" />
                                 </div>
                            <div class="btn_lbl">Upload Template</div>
                        </div>
                        <div class="container">
                            <div style="float: left">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/HTS2/images/move_waiting_up.png" AlternateText="Click To Add New Material" Width="24px" />
                                 </div>
                            <div class="btn_lbl">Upload Data</div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-schemablend-sumk1').tabSlideOut({
                            tabHandle: '.handlesumstck',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/spck_summary.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '390px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-schemablend-sumk1">
                    <a class="handlesumstck" href="http://link-for-non-js-users.html">Content</a>
                    <asp:GridView ID="grid_ratiosp" runat="server" Width="105px" OnRowDataBound="grid_ratiok3_RowDataBound" CellPadding="2">
                        <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" ForeColor="#333333" />
                        <HeaderStyle BackColor="Silver" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black" HorizontalAlign="Center" Wrap="True" />
                    </asp:GridView>
                </div>

                <script type="text/javascript">
                    $(function () {
                        $('.slide-export1').tabSlideOut({
                            tabHandle: '.handleexp3',                     //class of the element that will become your tab
                            pathToTabImage: 'tabSlideOut/images/export_hrz.png', //path to the image for the tab //Optionally can be set using css
                            imageHeight: '40px',                     //height of tab image           //Optionally can be set using css
                            imageWidth: '122px',                       //width of tab image            //Optionally can be set using css
                            tabLocation: 'bottom',                     //side of screen where tab lives, top, right, bottom, or left
                            speed: 300,                               //speed of animation
                            action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                            topPos: '0px',                          //position from the top/ use if tabLocation is left or right
                            leftPos: '200px',                          //position from left/ use if tabLocation is bottom or top
                            fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                        });

                    });
                </script>

                <div class="slide-export1">
                    <a class="handleexp3" href="http://link-for-non-js-users.html">Content</a>
                    <asp:ImageButton ID="ibt_xls3" runat="server" ImageUrl="~/Images/hts_nu_icons/document_microsoft_excel_01.png" OnClick="ibt_xls3_Click" Width="24px" />
                    <asp:ImageButton ID="ibt_pdf3" runat="server" ImageUrl="~/Images/hts_nu_icons/pdf.png" OnClick="ibt_pdf3_Click" Width="24px" />
                </div>

            </ContentTemplate>
        </ajx:TabPanel>

    </ajx:TabContainer>


    <asp:LinkButton Style="visibility: hidden" ID="CloseLink" runat="server" OkControlID="CloseLink">Close Proxy</asp:LinkButton>
    <asp:LinkButton Style="visibility: hidden" ID="CancelLink" runat="server" CancelControlID="CancelLink">Cancel Proxy</asp:LinkButton>


    <asp:SqlDataSource ID="ds_kontraktor" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT [Kode] FROM [v_Kontraktor] where kode != 'ADARO'"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_mtrl" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
        SelectCommand="SELECT '' [Kode] UNION ALL
                        SELECT [Kode] FROM [v_Material]"></asp:SqlDataSource>


    <script type="text/javascript">
        $(function () {
            $('.slide-schemablend-sum1').tabSlideOut({
                tabHandle: '.handle',                     //class of the element that will become your tab
                pathToTabImage: 'tabSlideOut/images/total_trucks.png', //path to the image for the tab //Optionally can be set using css
                imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                speed: 300,                               //speed of animation
                action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                topPos: '150px',                          //position from the top/ use if tabLocation is left or right
                leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
            });

        });
    </script>

    <div class="slide-schemablend-sum1">
        <a class="handle" href="http://link-for-non-js-users.html">Content</a>
        <asp:GridView ID="grid_sumtruck1" runat="server" ShowHeader="False" Width="120px" cell-padding="2"
            OnRowDataBound="grid_sumtruck1_RowDataBound" CaptionAlign="Left" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" VerticalAlign="Top" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SideContent1" runat="Server">
    <asp:Panel ID="passmenu1" runat="server"></asp:Panel>
    <asp:Panel ID="pan_rom1" runat="server"></asp:Panel>
</asp:Content>

