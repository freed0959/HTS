<%@ Page Language="VB" CodeFile="passchart1.aspx.vb" EnableEventValidation="false" AutoEventWireup="false" Inherits="HTS2_passchart1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Passing Reports</title>

    <link href="css_ajax_tabcontainer.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="templatemo_style.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <style type="text/css">
        @media all {
            .page-break {
                display: none;
            }
        }

        @media print {
            .page-break {
                display: block;
                page-break-before: always;
            }

            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
            }
        }

        @page {
            size: A4;
            margin: 0;
        }

        /*
        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }
        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1em rgba(0, 0, 0, 0.1) thin;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }
        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        body {
          background: linear-gradient(#ccc, #fff);
          font: 14px sans-serif;
          padding: 20px;
          margin: 0;
        }
        .letter {
          background: #fff;
          box-shadow: 0 0 10px rgba(0,0,0,0.3);
          margin: 26px auto 0;
          max-width: 550px;
          min-height: 300px;
          padding: 24px;
          position: relative;
          width: 80%;
        }
        .letter:before, .letter:after {
          content: "";
          height: 98%;
          position: absolute;
          width: 100%;
          z-index: -1;
        }
        .letter:before {
          background: #fafafa;
          box-shadow: 0 0 8px rgba(0,0,0,0.2);
          left: -5px;
          top: 4px;
          transform: rotate(-2.5deg);
        }
        .letter:after {
          background: #f6f6f6;
          box-shadow: 0 0 3px rgba(0,0,0,0.2);
          right: -3px;
          top: 1px;
          transform: rotate(1.4deg);
        }
     */
        .over {
            background-color: rgb(255, 199, 206);
            color: rgb(156, 0, 6);
        }

        .less {
            background-color: lightgreen;
        }

        .pageTitle {
            width: 100%;
            height: 75px;
            font-size: 18px;
            text-align: center;
            padding-top: 15px;
            font-weight: bold;
            color: #FFCC00;
        }
    </style>

    <script src="scripts/1.8.3/jquery.min.js"></script>
    <script src="tabSlideOut/jquery.tabSlideOut.v1.3.js"></script>
    <link href="tabSlideOut/tabSlideOut.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scmanager1" runat="server">
        </asp:ScriptManager>

        <script type="text/javascript">
            $(function () {
                $('.slide-out-div').tabSlideOut({
                    tabHandle: '.handle',                     //class of the element that will become your tab
                    pathToTabImage: 'tabSlideOut/images/export.png', //path to the image for the tab //Optionally can be set using css
                    imageHeight: '122px',                     //height of tab image           //Optionally can be set using css
                    imageWidth: '40px',                       //width of tab image            //Optionally can be set using css
                    tabLocation: 'right',                     //side of screen where tab lives, top, right, bottom, or left
                    speed: 300,                               //speed of animation
                    action: 'click',                          //options: 'click' or 'hover', action to trigger animation
                    topPos: '80px',                          //position from the top/ use if tabLocation is left or right
                    leftPos: '20px',                          //position from left/ use if tabLocation is bottom or top
                    fixedPosition: false                      //options: true makes it stick(fixed position) on scroll
                });

            });
        </script>

    <div class="slide-out-div">
        <a class="handle" href="http://link-for-non-js-users.html">Content</a>            
        <asp:ImageButton ID="ibt_xls1" runat="server" ImageUrl="~/HTS2/images/document_microsoft_excel_01.png" ImageAlign="AbsMiddle" />           
    </div>
        
        <div class="templatemo_post_text">
            <div class="pageTitle">
                <asp:Label ID="lbl_title1" runat="server" Text=""></asp:Label>
            </div>

            <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="4" CssClass="ajax__tab_blueGrad-theme" Width="1380px">

                <ajx:TabPanel runat="server" HeaderText="Supply Passing Plan" ID="TabPanel8">
                    <ContentTemplate>

                        <asp:GridView ID="grid_plan1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataSourceID="ds_sp_planpass_rep1" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="930px" CaptionAlign="Right">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="KontraktorKode" HeaderText="Kontraktor" SortExpression="KontraktorKode" ReadOnly="True" />
                                <asp:BoundField DataField="MaterialKode" HeaderText="Seam" SortExpression="MaterialKode" ReadOnly="True" />
                                <asp:BoundField DataField="06.00-07.00" HeaderText="06.00-07.00" ReadOnly="True" SortExpression="06.00-07.00" />
                                <asp:BoundField DataField="07.00-08.00" HeaderText="07.00-08.00" SortExpression="07.00-08.00" ReadOnly="True" />
                                <asp:BoundField DataField="08.00-09.00" HeaderText="08.00-09.00" SortExpression="08.00-09.00" ReadOnly="True" />
                                <asp:BoundField DataField="09.00-10.00" HeaderText="09.00-10.00" ReadOnly="True" SortExpression="09.00-10.00" />
                                <asp:BoundField DataField="10.00-11.00" HeaderText="10.00-11.00" SortExpression="10.00-11.00" ReadOnly="True" />
                                <asp:BoundField DataField="11.00-12.00" HeaderText="11.00-12.00" SortExpression="11.00-12.00" ReadOnly="True" />
                                <asp:BoundField DataField="12.00-13.00" HeaderText="12.00-13.00" ReadOnly="True" SortExpression="12.00-13.00" />
                                <asp:BoundField DataField="13.00-14.00" HeaderText="13.00-14.00" ReadOnly="True" SortExpression="13.00-14.00" />
                                <asp:BoundField DataField="14.00-15.00" HeaderText="14.00-15.00" ReadOnly="True" SortExpression="14.00-15.00" />
                                <asp:BoundField DataField="15.00-16.00" HeaderText="15.00-16.00" ReadOnly="True" SortExpression="15.00-16.00" />
                                <asp:BoundField DataField="16.00-17.00" HeaderText="16.00-17.00" ReadOnly="True" SortExpression="16.00-17.00" />
                                <asp:BoundField DataField="17.00-18.00" HeaderText="17.00-18.00" ReadOnly="True" SortExpression="17.00-18.00" />
                            </Columns>
                            <EditRowStyle BackColor="#CCCCCC" />
                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>

                        <div style="height: 25px;"></div>
                        <asp:GridView ID="grid_plan2" runat="server" AutoGenerateColumns="False" CaptionAlign="Right" CellPadding="2" DataSourceID="ds_sp_planpass_rep2" EmptyDataText="Sorry, Data not Found" ShowHeaderWhenEmpty="True" Width="930px">
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

                    </ContentTemplate>
                </ajx:TabPanel>

                <ajx:TabPanel runat="server" HeaderText="Passing Monitor" ID="TabPanel5">
                    <ContentTemplate>

                        <div style="padding: 10px; text-align: center; font-family: 'arial narrow'; font-weight: bold; font-size: 14px; vertical-align: middle;">
                            <div style="float: left; width: 250px;">Date
                                <asp:Label ID="lbl_dtm1" runat="server"></asp:Label></div>
                            <div style="float: left;">MONITORING SUPPLY PASSING RECORD DISPTACH ROM KM 67</div>
                        </div>

                        <asp:GridView ID="grid_pass1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataSourceID="SqlDS_truckpass"
                            EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" ShowHeader="False">
                            <Columns>
                                <asp:BoundField DataField="PK" HeaderText="No." SortExpression="PK" />
                                <asp:BoundField DataField="time_pm" HeaderText="Time" SortExpression="time_pm">
                                    <ItemStyle BackColor="Silver" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="trc_pm" HeaderText="TruckNo" SortExpression="trc_pm" />
                                <asp:BoundField DataField="mat_pm" HeaderText="Raw Cargo" SortExpression="mat_pm" />
                                <asp:BoundField DataField="rom_pm" HeaderText="Rom" SortExpression="rom_pm" />
                                <asp:BoundField DataField="time_sis" HeaderText="Time" SortExpression="time_sis">
                                    <ItemStyle BackColor="Silver" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="trc_sis" HeaderText="TruckNo" SortExpression="trc_sis" />
                                <asp:BoundField DataField="mat_sis" HeaderText="Raw Cargo" SortExpression="mat_sis" />
                                <asp:BoundField DataField="rom_sis" HeaderText="Rom" SortExpression="rom_sis" />
                                <asp:BoundField DataField="time_ra" HeaderText="Time" SortExpression="time_ra">
                                    <ItemStyle BackColor="Silver" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="trc_ra" HeaderText="TruckNo" SortExpression="trc_ra" />
                                <asp:BoundField DataField="mat_ra" HeaderText="Raw Cargo" SortExpression="mat_ra" />
                                <asp:BoundField DataField="rom_ra" HeaderText="Rom" SortExpression="rom_ra" />
                                <asp:BoundField DataField="time_rm" HeaderText="Time" SortExpression="time_rm">
                                    <ItemStyle BackColor="Silver" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="trc_rm" HeaderText="TruckNo" SortExpression="trc_rm" />
                                <asp:BoundField DataField="mat_rm" HeaderText="Raw Cargo" SortExpression="mat_rm" />
                                <asp:BoundField DataField="rom_rm" HeaderText="Rom" SortExpression="rom_rm" />
                            </Columns>
                            <EditRowStyle BackColor="#CCCCCC" />
                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>

                    </ContentTemplate>
                </ajx:TabPanel>

                <ajx:TabPanel runat="server" HeaderText="Hourly Ritase" ID="TabPanel1">
                    <ContentTemplate>
                        <div style="padding: 10px; text-align: center; font-family: 'arial narrow'; font-weight: bold; font-size: 14px; vertical-align: middle;">
                            PRODUCT ASSEMBLY DEPT - COAL HAULING OPERATION SECTION<br />DAILY PASSING RECORD _ DISPATCH ROM 67<br />NIGHT SHIFTNIGHT SHIFTDAY SHIFT 
                            <asp:Label ID="lbl_shf1" runat="server"></asp:Label>
                            <asp:Label ID="lbl_dtm4" runat="server"></asp:Label>
                        </div>
                        <div style="padding: 8px; font-family: 'arial rounded MT Bold'; font-size: 14px; background-color: #DAEEF3; width: 1300px;">
                            DAY SHIFT PRODUCT ASSEMBLY DEPT - COAL HAULING OPERATION SECTIONPRODUCT ASSEMBLY DEPT - COAL HAULING OPERATION SECTION<br />DAILY PASSING RECORD _ DISPATCH ROM 67<br />
                            <asp:Label ID="lbl_dtm2" runat="server"></asp:Label>
                            <asp:Label ID="lbl_dtm4a" runat="server"></asp:Label>
                        </div>
                        <asp:GridView ID="grid_rit1" runat="server" AutoGenerateColumns="False" Width="1300px"
                            EmptyDataText="Sorry Data Not Found" PageSize="25" ShowHeaderWhenEmpty="True" DataSourceID="ds_t11">
                            <Columns>
                                <asp:BoundField DataField="dt" ReadOnly="True" SortExpression="dt" ShowHeader="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAMA_E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="PAMA_E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAMA_E4900" HeaderText="E4900" ReadOnly="True" SortExpression="PAMA_E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAMA_E4000" HeaderText="E4000" ReadOnly="True" SortExpression="PAMA_E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIS_E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="SIS_E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIS_E4900" HeaderText="E4900" ReadOnly="True" SortExpression="SIS_E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIS_E4000" HeaderText="E4000" ReadOnly="True" SortExpression="SIS_E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RA_E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="RA_E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RA_E4900" HeaderText="E4900" ReadOnly="True" SortExpression="RA_E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RA_E4000" HeaderText="E4000" ReadOnly="True" SortExpression="RA_E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totrip" HeaderText="Actual" ReadOnly="True" SortExpression="totrip">
                                    <HeaderStyle BackColor="#DCE6F1" />
                                </asp:BoundField>
                                <asp:BoundField DataField="truckcount" HeaderText="Plan" ReadOnly="True" SortExpression="truckcount">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="E4900" HeaderText="E4900" ReadOnly="True" SortExpression="E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="E4000" HeaderText="E4000" ReadOnly="True" SortExpression="E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                        </asp:GridView>

                        <div style="padding: 8px; font-family: Arial Rounded MT Bold; font-size: 14px; background-color: #FFFF99; width: 1300px;">
                            NIGHT SHIFT PRODUCT ASSEMBLY DEPT - COAL HAULING OPERATION SECTION
                                    DAY SHIFT PRODUCT ASSEMBLY DEPT - COAL HAULING OPERATION SECTION<br />DAILY PASSING RECORD _ DISPATCH ROM 67<br />
                            <asp:Label ID="lbl_dtm3" runat="server"></asp:Label>
                        </div>
                        <asp:GridView ID="grid_rit2" runat="server" AutoGenerateColumns="False" DataSourceID="ds_t12"
                            EmptyDataText="Sorry Data Not Found" PageSize="25" ShowHeaderWhenEmpty="True" Width="1300px">
                            <Columns>
                                <asp:BoundField DataField="dt" ReadOnly="True" SortExpression="dt" ShowHeader="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAMA_E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="PAMA_E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAMA_E4900" HeaderText="E4900" ReadOnly="True" SortExpression="PAMA_E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAMA_E4000" HeaderText="E4000" ReadOnly="True" SortExpression="PAMA_E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIS_E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="SIS_E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIS_E4900" HeaderText="E4900" ReadOnly="True" SortExpression="SIS_E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SIS_E4000" HeaderText="E4000" ReadOnly="True" SortExpression="SIS_E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RA_E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="RA_E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RA_E4900" HeaderText="E4900" ReadOnly="True" SortExpression="RA_E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RA_E4000" HeaderText="E4000" ReadOnly="True" SortExpression="RA_E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totrip" HeaderText="Actual" ReadOnly="True" SortExpression="totrip">
                                    <HeaderStyle BackColor="#DCE6F1" />
                                </asp:BoundField>
                                <asp:BoundField DataField="truckcount" HeaderText="Plan" ReadOnly="True" SortExpression="truckcount">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="E5000">
                                    <HeaderStyle BackColor="#99CC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="E4900" HeaderText="E4900" ReadOnly="True" SortExpression="E4900">
                                    <HeaderStyle BackColor="#FFCC00" />
                                </asp:BoundField>
                                <asp:BoundField DataField="E4000" HeaderText="E4000" ReadOnly="True" SortExpression="E4000">
                                    <HeaderStyle BackColor="Gray" ForeColor="#004274" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" />
                        </asp:GridView>
                        <div class="page-break">
                            NIGHT SHIFTNIGHT SHIFTDAY SHIFTNIGHT SHIFT PRODUCT ASSEMBLY DEPT - COAL HAULING OPERATION SECTION
                                    <br />
                            DAILY PASSING RECORD _ DISPATCH ROM 67<br />
                            <asp:Label ID="lbl_dtm3a" runat="server"></asp:Label>
                        </div>
                    </ContentTemplate>
                </ajx:TabPanel>
                <ajx:TabPanel runat="server" HeaderText="Hourly Material" ID="TabPanel2">
                    <ContentTemplate>
                        <div id="tabs2">
                            <div style="text-align: center; background-color: #B8CCE4; font-weight: bold; font-family: Calibri; font-size: 14px; vertical-align: middle; padding-top: 10px; padding-bottom: 10px; width: 1300px;">
                                ACTUAL DETAIL SUPPLY Raw Material ROM TO KELANIS ( RTK ) _ PER HOURS
                            </div>
                            <div style="padding: 5px; font-family: calibri; font-size: 16px; font-weight: bold; vertical-align: middle; text-align: center; background-color: #EBF1DE; width: 1300px;">
                                PAMA
                            </div>
                            <asp:GridView ID="grid_mat1" runat="server" AutoGenerateColumns="False" DataSourceID="ds_t21" EmptyDataText="Sorry Data Not Found"
                                PageSize="25" ShowHeaderWhenEmpty="True" Width="1300px">
                                <Columns>
                                    <asp:BoundField DataField="seamcargo" HeaderText="Material" ReadOnly="True" SortExpression="seamcargo">
                                        <HeaderStyle BorderStyle="None" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="A6" HeaderText="A" ReadOnly="True" SortExpression="A6" />
                                    <asp:BoundField DataField="P6" HeaderText="P" SortExpression="P6" />
                                    <asp:BoundField DataField="A7" HeaderText="A" ReadOnly="True" SortExpression="A7" />
                                    <asp:BoundField DataField="P7" HeaderText="P" SortExpression="P7" />
                                    <asp:BoundField DataField="A8" HeaderText="A" ReadOnly="True" SortExpression="A8" />
                                    <asp:BoundField DataField="P8" HeaderText="P" SortExpression="P8" />
                                    <asp:BoundField DataField="A9" HeaderText="A" ReadOnly="True" SortExpression="A9" />
                                    <asp:BoundField DataField="P9" HeaderText="P" ReadOnly="True" SortExpression="P9" />
                                    <asp:BoundField DataField="A10" HeaderText="A" ReadOnly="True" SortExpression="A10" />
                                    <asp:BoundField DataField="P10" HeaderText="P" ReadOnly="True" SortExpression="P10" />
                                    <asp:BoundField DataField="A11" HeaderText="A" ReadOnly="True" SortExpression="A11" />
                                    <asp:BoundField DataField="P11" HeaderText="P" ReadOnly="True" SortExpression="P11" />
                                    <asp:BoundField DataField="A12" HeaderText="A" ReadOnly="True" SortExpression="A12" />
                                    <asp:BoundField DataField="P12" HeaderText="P" ReadOnly="True" SortExpression="P12" />
                                    <asp:BoundField DataField="A13" HeaderText="A" ReadOnly="True" SortExpression="A13" />
                                    <asp:BoundField DataField="P13" HeaderText="P" ReadOnly="True" SortExpression="P13" />
                                    <asp:BoundField DataField="A14" HeaderText="A" ReadOnly="True" SortExpression="A14" />
                                    <asp:BoundField DataField="P14" HeaderText="P" ReadOnly="True" SortExpression="P14" />
                                    <asp:BoundField DataField="A15" HeaderText="A" ReadOnly="True" SortExpression="A15" />
                                    <asp:BoundField DataField="P15" HeaderText="P" ReadOnly="True" SortExpression="P15" />
                                    <asp:BoundField DataField="A16" HeaderText="A" ReadOnly="True" SortExpression="A16" />
                                    <asp:BoundField DataField="P16" HeaderText="P" ReadOnly="True" SortExpression="P16" />
                                    <asp:BoundField DataField="A17" HeaderText="A" ReadOnly="True" SortExpression="A17" />
                                    <asp:BoundField DataField="P17" HeaderText="P" ReadOnly="True" SortExpression="P17" />
                                    <asp:BoundField DataField="A18" HeaderText="A" ReadOnly="True" SortExpression="A18" />
                                    <asp:BoundField DataField="P18" HeaderText="P" ReadOnly="True" SortExpression="P18" />
                                    <asp:BoundField DataField="A19" HeaderText="A" ReadOnly="True" SortExpression="A19" />
                                    <asp:BoundField DataField="P19" HeaderText="P" ReadOnly="True" SortExpression="P19" />
                                    <asp:BoundField DataField="A20" HeaderText="A" ReadOnly="True" SortExpression="A20" />
                                    <asp:BoundField DataField="P20" HeaderText="P" ReadOnly="True" SortExpression="P20" />
                                    <asp:BoundField DataField="A21" HeaderText="A" ReadOnly="True" SortExpression="A21" />
                                    <asp:BoundField DataField="P21" HeaderText="P" ReadOnly="True" SortExpression="P21" />
                                    <asp:BoundField DataField="A22" HeaderText="A" ReadOnly="True" SortExpression="A22" />
                                    <asp:BoundField DataField="P22" HeaderText="P" ReadOnly="True" SortExpression="P22" />
                                    <asp:BoundField DataField="A23" HeaderText="A" ReadOnly="True" SortExpression="A23" />
                                    <asp:BoundField DataField="P23" HeaderText="P" ReadOnly="True" SortExpression="P23" />
                                    <asp:BoundField DataField="A0" HeaderText="A" ReadOnly="True" SortExpression="A0" />
                                    <asp:BoundField DataField="P0" HeaderText="P" ReadOnly="True" SortExpression="P0" />
                                    <asp:BoundField DataField="A1" HeaderText="A" ReadOnly="True" SortExpression="A1" />
                                    <asp:BoundField DataField="P1" HeaderText="P" ReadOnly="True" SortExpression="P1" />
                                    <asp:BoundField DataField="A2" HeaderText="A" ReadOnly="True" SortExpression="A2" />
                                    <asp:BoundField DataField="P2" HeaderText="P" ReadOnly="True" SortExpression="P2" />
                                    <asp:BoundField DataField="A3" HeaderText="A" ReadOnly="True" SortExpression="A3" />
                                    <asp:BoundField DataField="P3" HeaderText="P" ReadOnly="True" SortExpression="P3" />
                                    <asp:BoundField DataField="A4" HeaderText="A" ReadOnly="True" SortExpression="A4" />
                                    <asp:BoundField DataField="P4" HeaderText="P" ReadOnly="True" SortExpression="P4" />
                                    <asp:BoundField DataField="A5" HeaderText="A" ReadOnly="True" SortExpression="A5" />
                                    <asp:BoundField DataField="P5" HeaderText="P" ReadOnly="True" SortExpression="P5" />
                                    <asp:BoundField DataField="shf_1" HeaderText="A" ReadOnly="True" SortExpression="shf_1" />
                                    <asp:BoundField DataField="plan_totalshf1" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf1" />
                                    <asp:BoundField DataField="shf_2" HeaderText="A" ReadOnly="True" SortExpression="shf_2" />
                                    <asp:BoundField DataField="plan_totalshf2" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf2" />
                                </Columns>
                                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" Width="75px" />
                                <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                            </asp:GridView>

                            <div style="padding: 5px; font-family: calibri; font-size: 16px; font-weight: bold; vertical-align: middle; text-align: center; background-color: #EBF1DE; width: 1300px;">
                                SIS
                            </div>
                            <asp:GridView ID="grid_mat2" runat="server" AutoGenerateColumns="False" DataSourceID="ds_t22" EmptyDataText="Sorry Data Not Found"
                                PageSize="25" ShowHeaderWhenEmpty="True" Width="1300px">
                                <Columns>
                                    <asp:BoundField DataField="seamcargo" HeaderText="Material" ReadOnly="True" SortExpression="seamcargo">
                                        <HeaderStyle BorderStyle="None" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="A6" HeaderText="A" ReadOnly="True" SortExpression="A6" />
                                    <asp:BoundField DataField="P6" HeaderText="P" SortExpression="P6" />
                                    <asp:BoundField DataField="A7" HeaderText="A" ReadOnly="True" SortExpression="A7" />
                                    <asp:BoundField DataField="P7" HeaderText="P" SortExpression="P7" />
                                    <asp:BoundField DataField="A8" HeaderText="A" ReadOnly="True" SortExpression="A8" />
                                    <asp:BoundField DataField="P8" HeaderText="P" SortExpression="P8" />
                                    <asp:BoundField DataField="A9" HeaderText="A" ReadOnly="True" SortExpression="A9" />
                                    <asp:BoundField DataField="P9" HeaderText="P" ReadOnly="True" SortExpression="P9" />
                                    <asp:BoundField DataField="A10" HeaderText="A" ReadOnly="True" SortExpression="A10" />
                                    <asp:BoundField DataField="P10" HeaderText="P" ReadOnly="True" SortExpression="P10" />
                                    <asp:BoundField DataField="A11" HeaderText="A" ReadOnly="True" SortExpression="A11" />
                                    <asp:BoundField DataField="P11" HeaderText="P" ReadOnly="True" SortExpression="P11" />
                                    <asp:BoundField DataField="A12" HeaderText="A" ReadOnly="True" SortExpression="A12" />
                                    <asp:BoundField DataField="P12" HeaderText="P" ReadOnly="True" SortExpression="P12" />
                                    <asp:BoundField DataField="A13" HeaderText="A" ReadOnly="True" SortExpression="A13" />
                                    <asp:BoundField DataField="P13" HeaderText="P" ReadOnly="True" SortExpression="P13" />
                                    <asp:BoundField DataField="A14" HeaderText="A" ReadOnly="True" SortExpression="A14" />
                                    <asp:BoundField DataField="P14" HeaderText="P" ReadOnly="True" SortExpression="P14" />
                                    <asp:BoundField DataField="A15" HeaderText="A" ReadOnly="True" SortExpression="A15" />
                                    <asp:BoundField DataField="P15" HeaderText="P" ReadOnly="True" SortExpression="P15" />
                                    <asp:BoundField DataField="A16" HeaderText="A" ReadOnly="True" SortExpression="A16" />
                                    <asp:BoundField DataField="P16" HeaderText="P" ReadOnly="True" SortExpression="P16" />
                                    <asp:BoundField DataField="A17" HeaderText="A" ReadOnly="True" SortExpression="A17" />
                                    <asp:BoundField DataField="P17" HeaderText="P" ReadOnly="True" SortExpression="P17" />
                                    <asp:BoundField DataField="A18" HeaderText="A" ReadOnly="True" SortExpression="A18" />
                                    <asp:BoundField DataField="P18" HeaderText="P" ReadOnly="True" SortExpression="P18" />
                                    <asp:BoundField DataField="A19" HeaderText="A" ReadOnly="True" SortExpression="A19" />
                                    <asp:BoundField DataField="P19" HeaderText="P" ReadOnly="True" SortExpression="P19" />
                                    <asp:BoundField DataField="A20" HeaderText="A" ReadOnly="True" SortExpression="A20" />
                                    <asp:BoundField DataField="P20" HeaderText="P" ReadOnly="True" SortExpression="P20" />
                                    <asp:BoundField DataField="A21" HeaderText="A" ReadOnly="True" SortExpression="A21" />
                                    <asp:BoundField DataField="P21" HeaderText="P" ReadOnly="True" SortExpression="P21" />
                                    <asp:BoundField DataField="A22" HeaderText="A" ReadOnly="True" SortExpression="A22" />
                                    <asp:BoundField DataField="P22" HeaderText="P" ReadOnly="True" SortExpression="P22" />
                                    <asp:BoundField DataField="A23" HeaderText="A" ReadOnly="True" SortExpression="A23" />
                                    <asp:BoundField DataField="P23" HeaderText="P" ReadOnly="True" SortExpression="P23" />
                                    <asp:BoundField DataField="A0" HeaderText="A" ReadOnly="True" SortExpression="A0" />
                                    <asp:BoundField DataField="P0" HeaderText="P" ReadOnly="True" SortExpression="P0" />
                                    <asp:BoundField DataField="A1" HeaderText="A" ReadOnly="True" SortExpression="A1" />
                                    <asp:BoundField DataField="P1" HeaderText="P" ReadOnly="True" SortExpression="P1" />
                                    <asp:BoundField DataField="A2" HeaderText="A" ReadOnly="True" SortExpression="A2" />
                                    <asp:BoundField DataField="P2" HeaderText="P" ReadOnly="True" SortExpression="P2" />
                                    <asp:BoundField DataField="A3" HeaderText="A" ReadOnly="True" SortExpression="A3" />
                                    <asp:BoundField DataField="P3" HeaderText="P" ReadOnly="True" SortExpression="P3" />
                                    <asp:BoundField DataField="A4" HeaderText="A" ReadOnly="True" SortExpression="A4" />
                                    <asp:BoundField DataField="P4" HeaderText="P" ReadOnly="True" SortExpression="P4" />
                                    <asp:BoundField DataField="A5" HeaderText="A" ReadOnly="True" SortExpression="A5" />
                                    <asp:BoundField DataField="P5" HeaderText="P" ReadOnly="True" SortExpression="P5" />
                                    <asp:BoundField DataField="shf_1" HeaderText="A" ReadOnly="True" SortExpression="shf_1" />
                                    <asp:BoundField DataField="plan_totalshf1" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf1" />
                                    <asp:BoundField DataField="shf_2" HeaderText="A" ReadOnly="True" SortExpression="shf_2" />
                                    <asp:BoundField DataField="plan_totalshf2" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf2" />

                                </Columns>
                                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                            </asp:GridView>

                            <div style="padding: 5px; font-family: calibri; font-size: 16px; font-weight: bold; vertical-align: middle; text-align: center; background-color: #EBF1DE; width: 1300px;">
                                RA
                            </div>
                            <asp:GridView ID="grid_mat3" runat="server" AutoGenerateColumns="False" DataSourceID="ds_t23" EmptyDataText="Sorry Data Not Found"
                                PageSize="25" ShowHeaderWhenEmpty="True" Width="1300px">
                                <Columns>
                                    <asp:BoundField DataField="seamcargo" HeaderText="Material" ReadOnly="True" SortExpression="seamcargo">
                                        <HeaderStyle BorderStyle="None" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="A6" HeaderText="A" ReadOnly="True" SortExpression="A6" />
                                    <asp:BoundField DataField="P6" HeaderText="P" SortExpression="P6" />
                                    <asp:BoundField DataField="A7" HeaderText="A" ReadOnly="True" SortExpression="A7" />
                                    <asp:BoundField DataField="P7" HeaderText="P" SortExpression="P7" />
                                    <asp:BoundField DataField="A8" HeaderText="A" ReadOnly="True" SortExpression="A8" />
                                    <asp:BoundField DataField="P8" HeaderText="P" SortExpression="P8" />
                                    <asp:BoundField DataField="A9" HeaderText="A" ReadOnly="True" SortExpression="A9" />
                                    <asp:BoundField DataField="P9" HeaderText="P" ReadOnly="True" SortExpression="P9" />
                                    <asp:BoundField DataField="A10" HeaderText="A" ReadOnly="True" SortExpression="A10" />
                                    <asp:BoundField DataField="P10" HeaderText="P" ReadOnly="True" SortExpression="P10" />
                                    <asp:BoundField DataField="A11" HeaderText="A" ReadOnly="True" SortExpression="A11" />
                                    <asp:BoundField DataField="P11" HeaderText="P" ReadOnly="True" SortExpression="P11" />
                                    <asp:BoundField DataField="A12" HeaderText="A" ReadOnly="True" SortExpression="A12" />
                                    <asp:BoundField DataField="P12" HeaderText="P" ReadOnly="True" SortExpression="P12" />
                                    <asp:BoundField DataField="A13" HeaderText="A" ReadOnly="True" SortExpression="A13" />
                                    <asp:BoundField DataField="P13" HeaderText="P" ReadOnly="True" SortExpression="P13" />
                                    <asp:BoundField DataField="A14" HeaderText="A" ReadOnly="True" SortExpression="A14" />
                                    <asp:BoundField DataField="P14" HeaderText="P" ReadOnly="True" SortExpression="P14" />
                                    <asp:BoundField DataField="A15" HeaderText="A" ReadOnly="True" SortExpression="A15" />
                                    <asp:BoundField DataField="P15" HeaderText="P" ReadOnly="True" SortExpression="P15" />
                                    <asp:BoundField DataField="A16" HeaderText="A" ReadOnly="True" SortExpression="A16" />
                                    <asp:BoundField DataField="P16" HeaderText="P" ReadOnly="True" SortExpression="P16" />
                                    <asp:BoundField DataField="A17" HeaderText="A" ReadOnly="True" SortExpression="A17" />
                                    <asp:BoundField DataField="P17" HeaderText="P" ReadOnly="True" SortExpression="P17" />
                                    <asp:BoundField DataField="A18" HeaderText="A" ReadOnly="True" SortExpression="A18" />
                                    <asp:BoundField DataField="P18" HeaderText="P" ReadOnly="True" SortExpression="P18" />
                                    <asp:BoundField DataField="A19" HeaderText="A" ReadOnly="True" SortExpression="A19" />
                                    <asp:BoundField DataField="P19" HeaderText="P" ReadOnly="True" SortExpression="P19" />
                                    <asp:BoundField DataField="A20" HeaderText="A" ReadOnly="True" SortExpression="A20" />
                                    <asp:BoundField DataField="P20" HeaderText="P" ReadOnly="True" SortExpression="P20" />
                                    <asp:BoundField DataField="A21" HeaderText="A" ReadOnly="True" SortExpression="A21" />
                                    <asp:BoundField DataField="P21" HeaderText="P" ReadOnly="True" SortExpression="P21" />
                                    <asp:BoundField DataField="A22" HeaderText="A" ReadOnly="True" SortExpression="A22" />
                                    <asp:BoundField DataField="P22" HeaderText="P" ReadOnly="True" SortExpression="P22" />
                                    <asp:BoundField DataField="A23" HeaderText="A" ReadOnly="True" SortExpression="A23" />
                                    <asp:BoundField DataField="P23" HeaderText="P" ReadOnly="True" SortExpression="P23" />
                                    <asp:BoundField DataField="A0" HeaderText="A" ReadOnly="True" SortExpression="A0" />
                                    <asp:BoundField DataField="P0" HeaderText="P" ReadOnly="True" SortExpression="P0" />
                                    <asp:BoundField DataField="A1" HeaderText="A" ReadOnly="True" SortExpression="A1" />
                                    <asp:BoundField DataField="P1" HeaderText="P" ReadOnly="True" SortExpression="P1" />
                                    <asp:BoundField DataField="A2" HeaderText="A" ReadOnly="True" SortExpression="A2" />
                                    <asp:BoundField DataField="P2" HeaderText="P" ReadOnly="True" SortExpression="P2" />
                                    <asp:BoundField DataField="A3" HeaderText="A" ReadOnly="True" SortExpression="A3" />
                                    <asp:BoundField DataField="P3" HeaderText="P" ReadOnly="True" SortExpression="P3" />
                                    <asp:BoundField DataField="A4" HeaderText="A" ReadOnly="True" SortExpression="A4" />
                                    <asp:BoundField DataField="P4" HeaderText="P" ReadOnly="True" SortExpression="P4" />
                                    <asp:BoundField DataField="A5" HeaderText="A" ReadOnly="True" SortExpression="A5" />
                                    <asp:BoundField DataField="P5" HeaderText="P" ReadOnly="True" SortExpression="P5" />
                                    <asp:BoundField DataField="shf_1" HeaderText="A" ReadOnly="True" SortExpression="shf_1" />
                                    <asp:BoundField DataField="plan_totalshf1" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf1" />
                                    <asp:BoundField DataField="shf_2" HeaderText="A" ReadOnly="True" SortExpression="shf_2" />
                                    <asp:BoundField DataField="plan_totalshf2" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf2" />
                                </Columns>
                                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                            </asp:GridView>

                            <div style="padding: 5px; font-family: calibri; font-size: 16px; font-weight: bold; vertical-align: middle; text-align: center; background-color: #EBF1DE; width: 1300px;">
                                RMI
                            </div>
                            <asp:GridView ID="grid_mat4" runat="server" AutoGenerateColumns="False" DataSourceID="ds_t24" EmptyDataText="Sorry Data Not Found"
                                PageSize="25" ShowHeaderWhenEmpty="True" Width="1300px">
                                <Columns>
                                    <asp:BoundField DataField="seamcargo" HeaderText="Material" ReadOnly="True" SortExpression="seamcargo">
                                        <HeaderStyle BorderStyle="None" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="A6" HeaderText="A" ReadOnly="True" SortExpression="A6" />
                                    <asp:BoundField DataField="P6" HeaderText="P" SortExpression="P6" />
                                    <asp:BoundField DataField="A7" HeaderText="A" ReadOnly="True" SortExpression="A7" />
                                    <asp:BoundField DataField="P7" HeaderText="P" SortExpression="P7" />
                                    <asp:BoundField DataField="A8" HeaderText="A" ReadOnly="True" SortExpression="A8" />
                                    <asp:BoundField DataField="P8" HeaderText="P" SortExpression="P8" />
                                    <asp:BoundField DataField="A9" HeaderText="A" ReadOnly="True" SortExpression="A9" />
                                    <asp:BoundField DataField="P9" HeaderText="P" ReadOnly="True" SortExpression="P9" />
                                    <asp:BoundField DataField="A10" HeaderText="A" ReadOnly="True" SortExpression="A10" />
                                    <asp:BoundField DataField="P10" HeaderText="P" ReadOnly="True" SortExpression="P10" />
                                    <asp:BoundField DataField="A11" HeaderText="A" ReadOnly="True" SortExpression="A11" />
                                    <asp:BoundField DataField="P11" HeaderText="P" ReadOnly="True" SortExpression="P11" />
                                    <asp:BoundField DataField="A12" HeaderText="A" ReadOnly="True" SortExpression="A12" />
                                    <asp:BoundField DataField="P12" HeaderText="P" ReadOnly="True" SortExpression="P12" />
                                    <asp:BoundField DataField="A13" HeaderText="A" ReadOnly="True" SortExpression="A13" />
                                    <asp:BoundField DataField="P13" HeaderText="P" ReadOnly="True" SortExpression="P13" />
                                    <asp:BoundField DataField="A14" HeaderText="A" ReadOnly="True" SortExpression="A14" />
                                    <asp:BoundField DataField="P14" HeaderText="P" ReadOnly="True" SortExpression="P14" />
                                    <asp:BoundField DataField="A15" HeaderText="A" ReadOnly="True" SortExpression="A15" />
                                    <asp:BoundField DataField="P15" HeaderText="P" ReadOnly="True" SortExpression="P15" />
                                    <asp:BoundField DataField="A16" HeaderText="A" ReadOnly="True" SortExpression="A16" />
                                    <asp:BoundField DataField="P16" HeaderText="P" ReadOnly="True" SortExpression="P16" />
                                    <asp:BoundField DataField="A17" HeaderText="A" ReadOnly="True" SortExpression="A17" />
                                    <asp:BoundField DataField="P17" HeaderText="P" ReadOnly="True" SortExpression="P17" />
                                    <asp:BoundField DataField="A18" HeaderText="A" ReadOnly="True" SortExpression="A18" />
                                    <asp:BoundField DataField="P18" HeaderText="P" ReadOnly="True" SortExpression="P18" />
                                    <asp:BoundField DataField="A19" HeaderText="A" ReadOnly="True" SortExpression="A19" />
                                    <asp:BoundField DataField="P19" HeaderText="P" ReadOnly="True" SortExpression="P19" />
                                    <asp:BoundField DataField="A20" HeaderText="A" ReadOnly="True" SortExpression="A20" />
                                    <asp:BoundField DataField="P20" HeaderText="P" ReadOnly="True" SortExpression="P20" />
                                    <asp:BoundField DataField="A21" HeaderText="A" ReadOnly="True" SortExpression="A21" />
                                    <asp:BoundField DataField="P21" HeaderText="P" ReadOnly="True" SortExpression="P21" />
                                    <asp:BoundField DataField="A22" HeaderText="A" ReadOnly="True" SortExpression="A22" />
                                    <asp:BoundField DataField="P22" HeaderText="P" ReadOnly="True" SortExpression="P22" />
                                    <asp:BoundField DataField="A23" HeaderText="A" ReadOnly="True" SortExpression="A23" />
                                    <asp:BoundField DataField="P23" HeaderText="P" ReadOnly="True" SortExpression="P23" />
                                    <asp:BoundField DataField="A0" HeaderText="A" ReadOnly="True" SortExpression="A0" />
                                    <asp:BoundField DataField="P0" HeaderText="P" ReadOnly="True" SortExpression="P0" />
                                    <asp:BoundField DataField="A1" HeaderText="A" ReadOnly="True" SortExpression="A1" />
                                    <asp:BoundField DataField="P1" HeaderText="P" ReadOnly="True" SortExpression="P1" />
                                    <asp:BoundField DataField="A2" HeaderText="A" ReadOnly="True" SortExpression="A2" />
                                    <asp:BoundField DataField="P2" HeaderText="P" ReadOnly="True" SortExpression="P2" />
                                    <asp:BoundField DataField="A3" HeaderText="A" ReadOnly="True" SortExpression="A3" />
                                    <asp:BoundField DataField="P3" HeaderText="P" ReadOnly="True" SortExpression="P3" />
                                    <asp:BoundField DataField="A4" HeaderText="A" ReadOnly="True" SortExpression="A4" />
                                    <asp:BoundField DataField="P4" HeaderText="P" ReadOnly="True" SortExpression="P4" />
                                    <asp:BoundField DataField="A5" HeaderText="A" ReadOnly="True" SortExpression="A5" />
                                    <asp:BoundField DataField="P5" HeaderText="P" ReadOnly="True" SortExpression="P5" />
                                    <asp:BoundField DataField="shf_1" HeaderText="A" ReadOnly="True" SortExpression="shf_1" />
                                    <asp:BoundField DataField="plan_totalshf1" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf1" />
                                    <asp:BoundField DataField="shf_2" HeaderText="A" ReadOnly="True" SortExpression="shf_2" />
                                    <asp:BoundField DataField="plan_totalshf2" HeaderText="P" ReadOnly="True" SortExpression="plan_totalshf2" />
                                </Columns>
                                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="7pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                            </asp:GridView>

                        </div>
                        <div class="page-break">
                        </div>
                    </ContentTemplate>
                </ajx:TabPanel>

                <ajx:TabPanel runat="server" HeaderText="Shift 1" ID="TabPanel3">
                    <ContentTemplate>
                        <div id="tabs3">
                            <table style="width: 100%;">
                                <caption style="text-align: left; font-family: Calibri; font-size: 11px; font-weight: 600;">
                                    Dear Rekans,
                                    <br />
                                    Berikut kami informasikan Supply Passing Dispatch ROM 67 update perjam ROM to Kelanis ( RTK )<br />
                                    <asp:Label ID="lbl_dtm5" runat="server"></asp:Label>
                                    <br />
                                    Day Shift
                                </caption>
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="grid_c11" runat="server" AutoGenerateColumns="False"
                                            EmptyDataText="Sorry Data Not Found" PageSize="25" ShowHeaderWhenEmpty="True" DataSourceID="ds_c11" Width="500px">
                                            <Columns>
                                                <asp:BoundField DataField="dt" ReadOnly="True" SortExpression="dt">
                                                    <ItemStyle BackColor="#EBF1DE" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="triptot" ReadOnly="True" SortExpression="triptot">
                                                    <ItemStyle BackColor="#DCE6F1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E5000_t" HeaderText="E5000P" ReadOnly="True" SortExpression="E5000_t">
                                                    <HeaderStyle BackColor="#99CC00" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4900_t" HeaderText="E4900" ReadOnly="True" SortExpression="E4900_t">
                                                    <HeaderStyle BackColor="#FF9100" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4000_t" HeaderText="E4000" ReadOnly="True" SortExpression="E4000_t">
                                                    <HeaderStyle BackColor="Gray" ForeColor="#336880" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Others" HeaderText="Others" ReadOnly="True" SortExpression="Others">
                                                    <HeaderStyle ForeColor="Black" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:GridView ID="grid_c12" runat="server" AutoGenerateColumns="False" DataSourceID="ds_c13" EmptyDataText="Sorry Data Not Found"
                                            PageSize="25" ShowHeaderWhenEmpty="True" Width="500px">
                                            <Columns>
                                                <asp:BoundField DataField="KontraktorKode" HeaderText="Detail Ritase contractor" ReadOnly="True" SortExpression="KontraktorKode">
                                                    <HeaderStyle BackColor="#DCE6F1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="totrip" ReadOnly="True" ShowHeader="False" SortExpression="totrip">
                                                    <HeaderStyle BackColor="#DCE6F1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="E5000">
                                                    <HeaderStyle BackColor="#99CC00" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4900" HeaderText="E4900" ReadOnly="True" SortExpression="E4900">
                                                    <HeaderStyle BackColor="#FFCC00" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4000" HeaderText="E4000" ReadOnly="True" SortExpression="E4000">
                                                    <HeaderStyle BackColor="Gray" ForeColor="#003366" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                            <RowStyle Font-Bold="False" Font-Names="Arial" Font-Size="8pt" />
                                        </asp:GridView>
                                    </td>
                                    <td valign="top">
                                        <asp:Chart ID="char_c11" runat="server" BackColor="Black" DataSourceID="ds_c14" Width="600px">
                                            <Series>
                                                <asp:Series Name="E5000" Color="0, 192, 0" CustomProperties="PixelPointWidth=35, DrawSideBySide=True, DrawingStyle=Cylinder, LabelStyle=Top"
                                                    YValueMembers="E5000_t" XValueMember="dt" ChartArea="ChartArea1">
                                                </asp:Series>
                                                <asp:Series Name="E4900" Color="Orange" CustomProperties="PixelPointWidth=35, DrawSideBySide=True, DrawingStyle=Cylinder, LabelStyle=Top"
                                                    YValueMembers="E4900_t" XValueMember="dt" ChartArea="ChartArea1">
                                                </asp:Series>
                                                <asp:Series Name="E4000" Color="LightGray" CustomProperties="PixelPointWidth=35, DrawSideBySide=True, DrawingStyle=Cylinder, LabelStyle=Top"
                                                    YValueMembers="E4000_t" XValueMember="dt" ChartArea="ChartArea1">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea BackColor="Black" BorderColor="White" Name="ChartArea1">
                                                    <AxisY IsLabelAutoFit="False" Interval="25">
                                                        <MajorGrid LineColor="White" />
                                                        <LabelStyle ForeColor="White" Font="Arial, 7pt" />
                                                    </AxisY>
                                                    <AxisX Interval="1" IntervalOffsetType="Hours" IsLabelAutoFit="False">
                                                        <MajorGrid Enabled="False" />
                                                        <LabelStyle ForeColor="White" Font="Arial, 7pt" />
                                                    </AxisX>
                                                    <Area3DStyle Enable3D="True" LightStyle="Realistic" IsClustered="True" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                        <asp:GridView ID="grid_c11a" runat="server" AutoGenerateColumns="False" DataSourceID="ds_c12" EmptyDataText="Sorry Data Not Found"
                                            PageSize="25" ShowHeaderWhenEmpty="True" Width="600px">
                                            <Columns>
                                                <asp:BoundField DataField="productKode" SortExpression="productKode" />
                                                <asp:BoundField DataField="06.00-07.00" HeaderText="06.00-07.00" ReadOnly="True" SortExpression="06.00-07.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="07.00-08.00" HeaderText="07.00-08.00" ReadOnly="True" SortExpression="07.00-08.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="08.00-09.00" HeaderText="08.00-09.00" ReadOnly="True" SortExpression="08.00-09.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="09.00-10.00" HeaderText="09.00-10.00" ReadOnly="True" SortExpression="09.00-10.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="10.00-11.00" HeaderText="10.00-11.00" ReadOnly="True" SortExpression="10.00-11.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="11.00-12.00" HeaderText="11.00-12.00" ReadOnly="True" SortExpression="11.00-12.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="12.00-13.00" HeaderText="12.00-13.00" ReadOnly="True" SortExpression="12.00-13.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="13.00-14.00" HeaderText="13.00-14.00" ReadOnly="True" SortExpression="13.00-14.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="14.00-15.00" HeaderText="14.00-15.00" ReadOnly="True" SortExpression="14.00-15.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="15.00-16.00" HeaderText="15.00-16.00" ReadOnly="True" SortExpression="15.00-16.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="16.00-17.00" HeaderText="16.00-17.00" ReadOnly="True" SortExpression="16.00-17.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="17.00-18.00" HeaderText="17.00-18.00" ReadOnly="True" SortExpression="17.00-18.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Names="Arial" Font-Size="8pt" Height="35px" HorizontalAlign="Center" Wrap="True" />
                                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Chart ID="char_c12" runat="server" BackColor="Black" DataSourceID="ds_c13" Width="500px">
                                            <Series>
                                                <asp:Series ChartType="Pie" Font="Microsoft Sans Serif, 8pt, style=Bold" IsValueShownAsLabel="True" Label="#VALX, #VALY" LabelForeColor="White" Name="Series1" XValueMember="KontraktorKode" YValueMembers="totrip" ChartArea="ChartArea1">
                                                    <SmartLabelStyle Enabled="False" MovingDirection="TopLeft, TopRight" />
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea BackColor="Black" Name="ChartArea1">
                                                    <Area3DStyle Enable3D="True" Inclination="60" IsRightAngleAxes="False" Rotation="60" WallWidth="10" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <Titles>
                                                <asp:Title Font="Microsoft Sans Serif, 8pt, style=Bold" ForeColor="White" Name="Title1" Text="Total Ritase all Contractor ROM \nTo Kelanis ( RTK )"></asp:Title>
                                            </Titles>
                                        </asp:Chart>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="page-break"></div>
                    </ContentTemplate>
                </ajx:TabPanel>

                <ajx:TabPanel runat="server" HeaderText="Shift 2" ID="TabPanel4">
                    <ContentTemplate>
                        <div id="tabs4">
                            <table style="width: 100%;">
                                <caption style="text-align: left; font-family: Calibri; font-size: 11px; font-weight: 600;">
                                    Dear Rekans,
                                    <br />
                                    Berikut kami informasikan Supply Passing Dispatch ROM 67 update perjam ROM to Kelanis ( RTK )<br />
                                    <asp:Label ID="lbl_dtm6" runat="server" Text="Label"></asp:Label><br />
                                    Night Shift
                                </caption>
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="grid_c21" runat="server" AutoGenerateColumns="False" Width="500px"
                                            EmptyDataText="Sorry Data Not Found" PageSize="25" ShowHeaderWhenEmpty="True" DataSourceID="ds_c21">
                                            <Columns>
                                                <asp:BoundField DataField="dt" ReadOnly="True" SortExpression="dt">
                                                    <ItemStyle BackColor="#EBF1DE" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="triptot" ReadOnly="True" SortExpression="triptot">
                                                    <ItemStyle BackColor="#DCE6F1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E5000_t" HeaderText="E5000P" ReadOnly="True" SortExpression="E5000_t">
                                                    <HeaderStyle BackColor="#99CC00" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4900_t" HeaderText="E4900" ReadOnly="True" SortExpression="E4900_t">
                                                    <HeaderStyle BackColor="#FF9100" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4000_t" HeaderText="E4000" ReadOnly="True" SortExpression="E4000_t">
                                                    <HeaderStyle BackColor="Gray" ForeColor="#336880" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Others" HeaderText="Others" SortExpression="Others" />
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:GridView ID="grid_c22" runat="server" AutoGenerateColumns="False" DataSourceID="ds_c24"
                                            EmptyDataText="Sorry Data Not Found" PageSize="25" ShowHeaderWhenEmpty="True" Width="500px">
                                            <Columns>
                                                <asp:BoundField DataField="KontraktorKode" HeaderText="Detail Ritase contractor" ReadOnly="True" SortExpression="KontraktorKode">
                                                    <HeaderStyle BackColor="#DCE6F1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="totrip" ReadOnly="True" ShowHeader="False" SortExpression="totrip">
                                                    <HeaderStyle BackColor="#DCE6F1" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E5000" HeaderText="E5000P" ReadOnly="True" SortExpression="E5000">
                                                    <HeaderStyle BackColor="#99CC00" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4900" HeaderText="E4900" ReadOnly="True" SortExpression="E4900">
                                                    <HeaderStyle BackColor="#FFCC00" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="E4000" HeaderText="E4000" ReadOnly="True" SortExpression="E4000">
                                                    <HeaderStyle BackColor="Gray" ForeColor="#003366" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#DDD9C4" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                    <td valign="top">
                                        <asp:Chart ID="char_c21" runat="server" DataSourceID="ds_c22" Width="600px" BackColor="Black">
                                            <Series>
                                                <asp:Series Name="E5000" Color="0, 192, 0" CustomProperties="PixelPointWidth=35, DrawSideBySide=True, DrawingStyle=Cylinder, LabelStyle=Top"
                                                    YValueMembers="E5000_t" XValueMember="dt" ChartArea="ChartArea1">
                                                </asp:Series>
                                                <asp:Series Name="E4900" Color="Orange" CustomProperties="PixelPointWidth=35, DrawSideBySide=True, DrawingStyle=Cylinder, LabelStyle=Top"
                                                    YValueMembers="E4900_t" XValueMember="dt" ChartArea="ChartArea1">
                                                </asp:Series>
                                                <asp:Series Name="E4000" Color="LightGray" CustomProperties="PixelPointWidth=35, DrawSideBySide=True, DrawingStyle=Cylinder, LabelStyle=Top"
                                                    YValueMembers="E4000_t" XValueMember="dt" ChartArea="ChartArea1">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BackColor="Black" BorderColor="White">
                                                    <AxisY IsLabelAutoFit="False" Interval="25">
                                                        <MajorGrid LineColor="White" />
                                                        <LabelStyle Font="Arial, 7pt" ForeColor="White" />
                                                    </AxisY>
                                                    <AxisX Interval="1" IsLabelAutoFit="False" IntervalOffsetType="Hours">
                                                        <MajorGrid Enabled="False" />
                                                        <LabelStyle Font="Arial, 7pt" ForeColor="White" />
                                                    </AxisX>
                                                    <Area3DStyle Enable3D="True" LightStyle="Realistic" IsClustered="True" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                        <asp:GridView ID="grid_c23" runat="server" AutoGenerateColumns="False" DataSourceID="ds_c23"
                                            EmptyDataText="Sorry Data Not Found" PageSize="25" ShowHeaderWhenEmpty="True" Width="600px">
                                            <Columns>
                                                <asp:BoundField DataField="productKode" SortExpression="productKode" ShowHeader="False" />
                                                <asp:BoundField DataField="18.00-19.00" HeaderText="18.00-19.00" SortExpression="18.00-19.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="19.00-20.00" HeaderText="19.00-20.00" SortExpression="19.00-20.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="20.00-21.00" HeaderText="20.00-21.00" SortExpression="20.00-21.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="21.00-22.00" HeaderText="21.00-22.00" SortExpression="21.00-22.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="22.00-23.00" HeaderText="22.00-23.00" SortExpression="22.00-23.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="23.00-00.00" HeaderText="23.00-00.00" SortExpression="23.00-00.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="00.00-01.00" HeaderText="00.00-01.00" SortExpression="00.00-01.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="01.00-02.00" HeaderText="01.00-02.00" SortExpression="01.00-02.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="02.00-03.00" HeaderText="02.00-03.00" SortExpression="02.00-03.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="03.00-04.00" HeaderText="03.00-04.00" SortExpression="03.00-04.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="04.00-05.00" HeaderText="04.00-05.00" SortExpression="04.00-05.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="05.00-06.00" HeaderText="05.00-06.00" SortExpression="05.00-06.00">
                                                    <HeaderStyle Width="10px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Names="Arial" Font-Size="8pt" Height="35px" HorizontalAlign="Center" Wrap="True" />
                                            <RowStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:Chart ID="char_c22" runat="server" BackColor="Black" DataSourceID="ds_c24" Width="500px">
                                            <Series>
                                                <asp:Series ChartType="Pie" Font="Microsoft Sans Serif, 8pt, style=Bold" IsValueShownAsLabel="True" Label="#VALX, #VALY" LabelForeColor="White" Name="Series1" XValueMember="KontraktorKode" YValueMembers="totrip" ChartArea="ChartArea1">
                                                    <SmartLabelStyle Enabled="False" MovingDirection="TopLeft, TopRight" />
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea BackColor="Black" Name="ChartArea1">
                                                    <Area3DStyle Enable3D="True" Inclination="60" IsRightAngleAxes="False" Rotation="60" WallWidth="10" />
                                                </asp:ChartArea>
                                            </ChartAreas>
                                            <Titles>
                                                <asp:Title Font="Microsoft Sans Serif, 8pt, style=Bold" ForeColor="White" Name="Title1" Text="Total Ritase all Contractor ROM \nTo Kelanis ( RTK )"></asp:Title>
                                            </Titles>
                                        </asp:Chart>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="page-break"></div>
                    </ContentTemplate>
                </ajx:TabPanel>

                <ajx:TabPanel runat="server" HeaderText="Additional Info" ID="TabPanel6">
                    <ContentTemplate>

                        <div style="float: left;">

                            <asp:GridView ID="grid_miss1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="ID_Pass" DataSourceID="ds_missed1" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID_Pass" HeaderText="ID_Pass" SortExpression="ID_Pass" Visible="False" />
                                    <asp:BoundField DataField="KontraktorKode" HeaderText="KontraktorKode" SortExpression="KontraktorKode">
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Top" BackColor="#FFFFCC" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="passtime" HeaderText="Passing Time" HtmlEncode="False" SortExpression="passtime">
                                        <ItemStyle HorizontalAlign="Center" BackColor="Silver" Font-Bold="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" SortExpression="TruckNo">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="seamcargo" HeaderText="Raw Cargo" SortExpression="seamcargo">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SourceKode" HeaderText="ROM" SortExpression="SourceKode">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="#CCCCCC" />
                                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                        </div>

                        <div style="float: left; display: inline-block; padding-left: 20px;">

                            <asp:GridView ID="grid_pass291" runat="server" AutoGenerateColumns="False" CellPadding="2" DataSourceID="ds_passed29" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="300px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="MaterialKode" HeaderText="Raw Cargo" SortExpression="MaterialKode" />
                                    <asp:BoundField DataField="calofatic" DataFormatString="{0:N0}" HeaderText="CV" HtmlEncode="False" NullDisplayText="-" SortExpression="calofatic">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="jum" HeaderText="Total Trucks" ReadOnly="True" SortExpression="jum">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="#CCCCCC" />
                                <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                        </div>

                    </ContentTemplate>

                </ajx:TabPanel>

                <ajx:TabPanel runat="server" HeaderText="Material Deviation" ID="TabPanel7">
                    <ContentTemplate>



                        <asp:GridView ID="grid_dev1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataSourceID="ds_sp_matdeviation1" EmptyDataText="Sorry, Data Not Found" ForeColor="#333333" ShowHeaderWhenEmpty="True" Width="500px" ShowHeader="False">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="KontraktorKode" HeaderText="KontraktorKode" SortExpression="KontraktorKode" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" SortExpression="TruckNo" />
                                <asp:BoundField DataField="jamlewat" HeaderText="Hour" ReadOnly="True" SortExpression="jamlewat" />
                                <asp:BoundField DataField="MaterialKode" HeaderText="Material" SortExpression="MaterialKode" />
                                <asp:BoundField DataField="SourceKode" HeaderText="Rom" SortExpression="SourceKode" />
                                <asp:BoundField DataField="jamtimbang" HeaderText="Hour" ReadOnly="True" SortExpression="jamtimbang" />
                                <asp:BoundField DataField="TimbMaterial" HeaderText="Material" SortExpression="TimbMaterial" />
                                <asp:BoundField DataField="TimbROM" HeaderText="Rom" SortExpression="TimbROM" />
                            </Columns>
                            <EditRowStyle BackColor="#CCCCCC" />
                            <EmptyDataRowStyle BackColor="White" Height="100px" VerticalAlign="Middle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" Height="35px" HorizontalAlign="Center" Wrap="False" />
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="8pt" VerticalAlign="Top" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>



                    </ContentTemplate>
                </ajx:TabPanel>

            </ajx:TabContainer>



            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_monitor1" SelectCommandType="StoredProcedure" ID="SqlDS_truckpass">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prod4" SelectCommandType="StoredProcedure" ID="ds_t11">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prod4" SelectCommandType="StoredProcedure" ID="ds_t12">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="2" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_hourmat1" SelectCommandType="StoredProcedure" ID="ds_t21">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="PAMA" Name="kon" Type="String"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_hourmat1" SelectCommandType="StoredProcedure" ID="ds_t22">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="SIS" Name="kon" Type="String"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_hourmat1" SelectCommandType="StoredProcedure" ID="ds_t23">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="RA" Name="kon" Type="String"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_hourmat1" SelectCommandType="StoredProcedure" ID="ds_t24">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="RMI" Name="kon" Type="String"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prod2" SelectCommandType="StoredProcedure" ID="ds_c11">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prodpas2" SelectCommandType="StoredProcedure" ID="ds_c12">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prodpas3" SelectCommandType="StoredProcedure" ID="ds_c13">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prod2ch" SelectCommandType="StoredProcedure" ID="ds_c14">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prod2" SelectCommandType="StoredProcedure" ID="ds_c21">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="2" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prod2ch" SelectCommandType="StoredProcedure" ID="ds_c22">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="2" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prodpas2" SelectCommandType="StoredProcedure" ID="ds_c23">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="2" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_ql_prodpas3" SelectCommandType="StoredProcedure" ID="ds_c24">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="DateTime"></asp:QueryStringParameter>
                    <asp:Parameter DefaultValue="2" Name="shf" Type="Byte"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT *, dbo.fn_fullHour(TransactionDate) as passtime FROM [vwpass_daily1] WHERE miss67 IS NOT NULL 
AND (CONVERT(nvarchar, TransactionDate, 101) = @dtm)
ORDER BY KontraktorKode ASC, TransactionDate ASC"
                ID="ds_missed1">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_passed29" SelectCommandType="StoredProcedure" ID="ds_passed29">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="_sp_rtk_mat_deviation1" SelectCommandType="StoredProcedure" ID="ds_sp_matdeviation1">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="dtm" Name="dtm" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:SqlDataSource>


            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                SelectCommand="_sp_planpass_rep1" SelectCommandType="StoredProcedure" ID="ds_sp_planpass_rep1">
                <SelectParameters>
                    <asp:QueryStringParameter Name="dtm" QueryStringField="dtm" Type="String" />
                    <asp:Parameter DefaultValue="1" Name="shf" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>


            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                SelectCommand="_sp_planpass_rep1" SelectCommandType="StoredProcedure" ID="ds_sp_planpass_rep2">
                <SelectParameters>
                    <asp:QueryStringParameter Name="dtm" QueryStringField="dtm" Type="String" />
                    <asp:Parameter Name="shf" Type="Int32" DefaultValue="2"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>


            <asp:GridView ID="grid_xls1" runat="server" Visible="False">
            </asp:GridView>
        </div>


    </form>
</body>
</html>
