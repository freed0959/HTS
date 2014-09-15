<%@ Page Title="Truck Distribution to ROM " Language="VB" MasterPageFile="~/HTS2/Site.master" AutoEventWireup="false" CodeFile="PassCon.aspx.vb" Inherits="HTS2_PassCon" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

    <script type="text/javascript">
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
        input[type=file] {
            position: relative;
            -webkit-appearance: textfield;
            -webkit-box-sizing: border-box;
            width: 200px;
        }

            input[type=file]::-webkit-file-upload-button {
                border: none;
                margin: 0;
                padding: 0;
                -webkit-appearance: none;
                width: 0;
            }
            /* "x::-webkit-file-upload-button" forces the rules to only apply to browsers that support this pseudo-element */
            x::-webkit-file-upload-button, input[type=file]:after {
                content: 'Browse...';
                display: inline-block;
                left: 100%;
                margin-left: 3px;
                padding: 3px 8px 2px;
                position: relative;
                -webkit-appearance: button;
            }

        .srcTxt {
            height: 25px;
            width: 225px;
            font-family: arial Verdana Calibri;
            font-size: 24px;
        }

        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:MultiView ID="MultiView1" runat="server">

        <asp:View ID="View1" runat="server">


            <table style="vertical-align: top; float: left; width: 50%">
                <tr>
                    <td>
                        <asp:Label ID="lbl_date" runat="server" Text="Supply Passing Plan Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_date1" runat="server" onfocus="blur();" Width="85px"></asp:TextBox>
                        <ajx:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_date1"></ajx:CalendarExtender>
                        <asp:Button ID="btn_filter1" runat="server" Text="Submit" />
                    </td>
                </tr>
            </table>

            <asp:Panel runat="server" ID="pan_tool1" Style="width: 880px; margin-top: 25px;" CssClass="pantool">
                <div class="olet">
                    <asp:ImageButton ID="img_chart1" runat="server" ImageUrl="images/kchart.png" AlternateText="Click to View Chart" Visible="False" />
                    <asp:ImageButton ID="img_xls1" runat="server" ImageUrl="images/document_microsoft_excel_01.png" AlternateText="Click to Export Data to Excel" />
                    <asp:ImageButton ID="img_pdf1" runat="server" ImageUrl="images/pdf.png" AlternateText="Click to Export Data to Pdf" />
                </div>
                <div class="orig">
                    <asp:Label ID="lbl_info1" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            <asp:GridView ID="grid_plan1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataSourceID="ds_sp_planpass_rep1" EmptyDataText="Sorry, Data not Found" OnRowDataBound="grid_plan1_RowDataBound" ShowHeaderWhenEmpty="True" Width="930px">
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
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="35px" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:SqlDataSource ID="ds_sp_planpass_rep1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_planpass_rep2" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txt_date1" Name="dtm" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Int32" />
                    <asp:Parameter DefaultValue="" Name="kon" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Panel ID="pan_tool2" runat="server" CssClass="pantool" Style="width: 880px; margin-top: 50px;">
                <div class="olet">
                    <asp:ImageButton ID="img_chart2" runat="server" AlternateText="Click to View Chart" ImageUrl="images/kchart.png" Visible="False" />
                    <asp:ImageButton ID="img_xls3" runat="server" AlternateText="Click to Export Data to Excel" ImageUrl="images/document_microsoft_excel_01.png" />
                    <asp:ImageButton ID="img_pdf3" runat="server" AlternateText="Click to Export Data to Pdf" ImageUrl="images/pdf.png" />
                </div>
                <div class="orig">
                    <asp:Label ID="lbl_info2" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            <asp:GridView ID="grid_plan2" runat="server" AutoGenerateColumns="False" CellPadding="2" DataSourceID="ds_sp_passplan_rep2" EmptyDataText="Sorry, Data not Found" OnRowDataBound="grid_plan2_RowDataBound" ShowHeaderWhenEmpty="True" Width="930px">
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
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="35px" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:SqlDataSource ID="ds_sp_passplan_rep2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_planpass_rep2" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txt_date1" Name="dtm" PropertyName="Text" Type="String" />
                    <asp:Parameter DefaultValue="2" Name="shf" Type="Int32" />
                    <asp:Parameter DefaultValue="" Name="kon" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


        </asp:View>

        <asp:View ID="View2" runat="server">


            <table style="vertical-align: top; float: left; width: 50%">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Transaction Date"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_date3" runat="server" Width="85px"></asp:TextBox>
                        <ajx:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_date3"></ajx:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Label6" runat="server" Text="TruckNo"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_truck1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label4" runat="server" Text="Seam"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="ddl_raw2" runat="server" DataSourceID="ds_raw1" DataTextField="cargo" DataValueField="kode" Width="85px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="ROM"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_rom1" runat="server" AutoPostBack="True" DataSourceID="ds_rom1" DataTextField="kode" DataValueField="kode">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btn_fil2" runat="server" Text="Submit" />
                    </td>
                </tr>
            </table>

            <asp:Panel runat="server" ID="Panel3" Style="width: 880px;" CssClass="pantool">
                <div class="olet">
                    <asp:ImageButton ID="img_char2" runat="server" ImageUrl="images/kchart.png" AlternateText="Click to View Chart" />
                    <asp:ImageButton ID="img_xls2" runat="server" ImageUrl="images/document_microsoft_excel_01.png" AlternateText="Click to Export Data to Excel" />
                    <asp:ImageButton ID="img_pdf2" runat="server" ImageUrl="images/pdf.png" AlternateText="Click to Export Data to Pdf" />
                </div>
                <div class="orig">
                    <asp:Label ID="lbl_tot2" runat="server" CssClass="totlbl"></asp:Label>
                    <asp:DropDownList ID="ddl_pag2" runat="server" AutoPostBack="True" CssClass="ddl" BackColor="#FFCC66" ForeColor="#333333"
                        Font-Names="Calibri,Arial,Verdana" Height="30px">
                        <asp:ListItem Value="25">25 Rows per Page</asp:ListItem>
                        <asp:ListItem Value="100">100 Rows per Page</asp:ListItem>
                        <asp:ListItem Value="500">500 Rows per Page</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ImageButton ID="img_tplt2" runat="server" ImageUrl="~/HTS2/images/table.png" AlternateText="Upload Template" />
                    <asp:ImageButton ID="img_upl2" runat="server" ImageUrl="~/HTS2/images/move_waiting_up.png" AlternateText="Click to Upload Data" />
                    <asp:ImageButton ID="img_add2" runat="server" ImageUrl="~/HTS2/images/file_new_01.png" AlternateText="Click to Manual Entry Data" />
                </div>
            </asp:Panel>

            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="ID_RomDist" DataSourceID="ds_dist1" EmptyDataText="Sorry Data Not Found" ForeColor="#333333" PageSize="25" ShowHeaderWhenEmpty="True" Width="860px" AllowSorting="True">
                        <AlternatingRowStyle BackColor="#D9EAE4" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID_RomDist" HeaderText="ID_RomDist" SortExpression="ID_RomDist" Visible="False" InsertVisible="False" ReadOnly="True" />
                            <asp:BoundField DataField="TransactionDate" HeaderText="TransactionDate" SortExpression="TransactionDate" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="TransactionDate" DataFormatString="{0:hh:mm tt}" HeaderText="Time" HtmlEncode="False" SortExpression="TransactionDate">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Shift" HeaderText="Shift" SortExpression="Shift" />
                            <asp:BoundField DataField="KontraktorKode" HeaderText="KontraktorKode" SortExpression="KontraktorKode" Visible="False" />
                            <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" SortExpression="TruckNo" />
                            <asp:BoundField DataField="MaterialKode" HeaderText="MaterialKode" SortExpression="MaterialKode" />
                            <asp:BoundField DataField="SourceKode" HeaderText="SourceKode" SortExpression="SourceKode" />
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/hts_nu_icons/table_edit (2).png" ShowSelectButton="True">
                                <ControlStyle Width="16px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#CCCCCC" />
                        <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                        <FooterStyle BackColor="#6ACCA3" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#336600" Font-Bold="False" Font-Names="Arial" Font-Size="7pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="#6ACCA3" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle Font-Names="Arial" Font-Size="8pt" />
                        <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#33CCCC" />
                        <SortedAscendingHeaderStyle BackColor="#33CCCC" />
                        <SortedDescendingCellStyle BackColor="#009999" />
                        <SortedDescendingHeaderStyle BackColor="#009999" />
                    </asp:GridView>


                    <asp:SqlDataSource ID="ds_dist1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                        SelectCommand="_sp_pascon_lis1" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter Name="dtm" Type="String" />
                            <asp:Parameter Name="unt" Type="String" />
                            <asp:Parameter Name="mat" Type="String" />
                            <asp:Parameter Name="rom" Type="String" />
                            <asp:Parameter Name="com" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>           

            <asp:Panel ID="pan_loadTD" runat="server">
                <div class="HellowWorldPopup">
                    <div class="PopupHeader" id="PopupHeader">
                        Upload Truck Distribution Data<br />
                        <br />
                    </div>
                    <div class="PopupBody" style="background-color: #CCCCCC">
                        <asp:FileUpload ID="FileUpload1" runat="server" class="file" /><br />
                    </div>
                    <div class="PopupBody" style="background-color: #FFFFFF">
                        <asp:LinkButton ID="lbt_uplTD" OnClientClick="$('CloseLink').click();" runat="server" CausesValidation="True" CommandName="Insert" Text="Upload"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton4" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>

        </asp:View>

        <asp:View ID="View3" runat="server">

            <asp:DetailsView ID="dtv_plan1" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="ID_RomDist" DataSourceID="ds_dist2" DefaultMode="Edit" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" GridLines="None" HeaderText="Edit Truck Distribution Data" Height="50px" Width="550px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <CommandRowStyle BackColor="#CCCCCC" Font-Bold="True" />
                <EditRowStyle BackColor="#FFCC99" Font-Bold="False" ForeColor="#333333" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <Fields>
                    <asp:BoundField DataField="id_qualwekplan" HeaderText="id_qualwekplan" InsertVisible="False" ReadOnly="True" SortExpression="id_qualwekplan" Visible="False" />
                    <asp:TemplateField HeaderText="editable" ShowHeader="False">
                        <EditItemTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="Truck No"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lbl_truck2" runat="server" Text='<%# Eval("TruckNo") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Raw Cargo"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="ds_raw1" DataTextField="cargo" DataValueField="kode" SelectedValue='<%# Bind("MaterialKode") %>'>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="ROM"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="ds_rom1" DataTextField="kode" DataValueField="kode" SelectedValue='<%# Bind("SourceKode") %>'>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Shift"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("Shift") %>'>
                                            <asp:ListItem Value="1">Shift 1</asp:ListItem>
                                            <asp:ListItem Value="2">Shift 2</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Fields>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#336600" Font-Bold="True" ForeColor="White" Height="35px" />
                <InsertRowStyle BackColor="#FFCC99" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFCC99" ForeColor="#333333" />
            </asp:DetailsView>
            <asp:SqlDataSource ID="ds_dist2" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT * FROM [tbl_TruckRomDist] WHERE ID_RomDist  = @ID_RomDist " UpdateCommand="UPDATE dbo.tbl_TruckRomDist
SET SourceKode = @SourceKode,
Shift = @Shift
WHERE ID_RomDist = @ID_RomDist">
                <SelectParameters>
                    <asp:ControlParameter ControlID="dtkeep1" Name="ID_RomDist" PropertyName="Text" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SourceKode" />
                    <asp:Parameter Name="Shift" />
                    <asp:Parameter Name="ID_RomDist" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </asp:View>

    </asp:MultiView>

    <asp:Panel ID="pan_uc1" runat="server" Visible="false">
        <div class="HellowWorldPopup">
            <div class="PopupHeader" id="PopupHeader1" style="text-align: right; width: 413px;">
                <asp:LinkButton ID="LinkButton2" OnClientClick="$('CancelLink').click();" runat="server" CausesValidation="False" CommandName="Cancel" Text="Close"></asp:LinkButton>
            </div>
            <div class="PopupBody">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/HTS2/images/under_const_diger.gif" />
            </div>
        </div>
    </asp:Panel>

    <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
        DynamicServicePath="" Enabled="True" PopupControlID="pan_loadTD" PopupDragHandleControlID="PopupHeader" OkControlID="CloseLink"
        TargetControlID="img_upl2">
    </ajx:ModalPopupExtender>

    <ajx:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="ModalPopupBG" CancelControlID="CancelLink" Drag="True"
        DynamicServicePath="" Enabled="True" PopupControlID="pan_uc1" PopupDragHandleControlID="PopupHeader" OkControlID="CancelLink"
        TargetControlID="img_char2">
    </ajx:ModalPopupExtender>

    <asp:LinkButton Style="visibility: hidden" ID="CloseLink" runat="server" OkControlID="CloseLink">Close Proxy</asp:LinkButton>
    <asp:LinkButton Style="visibility: hidden" ID="CancelLink" runat="server" CancelControlID="CancelLink">Cancel Proxy</asp:LinkButton>

    <asp:Label ID="sqlkeep1" runat="server" Text="" ForeColor="Transparent"></asp:Label>
    <asp:Label ID="xlskeep1" runat="server" ForeColor="Transparent"></asp:Label>
    <asp:Label ID="dtkeep1" runat="server" ForeColor="Transparent"></asp:Label>
    <asp:GridView ID="grid_shd1" runat="server">
    </asp:GridView>
    <asp:SqlDataSource ID="ds_raw1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select 'All' as kode, 'All' as cargo  Union ALL SELECT [Kode], [seamcargo] FROM [tbl_qualmapping]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ds_rom1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select ' All' as kode Union ALL SELECT [Kode] FROM [v_Sources]"></asp:SqlDataSource>



</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SideContent1" runat="Server">
    <div class="templatemo_section">
        <div class="templatemo_section_title">
            Current Trucks Position
        </div>
        <div class="templatemo_section_bottom">
            <asp:DataList ID="DataList1" runat="server" DataSourceID="ds_sumpos">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("pos")%>' Font-Size="13px" />
                    :                             
                            <asp:Label ID="jummatLabel" runat="server" Text='<%# String.Concat(Eval("jummat"), " trucks") %>' Font-Size="13px" />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <asp:SqlDataSource ID="ds_sumpos" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
        SelectCommand="_sp_rtk_pos1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
</asp:Content>

