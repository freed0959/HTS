<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="HTS2_Default3" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Page</title>
    <style type="text/css">
        .header {
            background-color: #507CD1;
            font-weight: bold;
            color: #FFFFFF;
            font-size: 14px;
            font-family: Arial, Helvetica, sans-serif;
            text-transform: capitalize;
            text-align: center;
            vertical-align: middle;
            padding: 8px;
        }

        input[type="text"] {
            width: 100%;
            border-style: none;
            font-family: arial, Verdana, sans-serif;
            font-size: 12px;
            height: 23px;
            vertical-align: middle;
            padding-left: 5px;
            text-align: right;
        }

        table .gridtable {
            table-layout: fixed;
            width: 100%;
            font-family: arial, Verdana, sans-serif;
            font-size: 12px;
        }

            table .gridtable td {
                text-align: center;
                padding: 5px;
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

            <asp:View ID="wpr1det_View1" runat="server">

                <asp:DetailsView ID="dtv_plan1" runat="server" AutoGenerateRows="False" CellPadding="2" DataKeyNames="id_qualwekplan" DataSourceID="ds_dtView" DefaultMode="Edit" EmptyDataText="Sorry, Data not Found" ForeColor="#333333" GridLines="None" Height="50px" Width="650px" OnModeChanging="dtv_plan1_ModeChanging" Font-Names="Arial" Font-Size="10pt" OnDataBound="dtv_plan1_DataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <CommandRowStyle BackColor="#FFFF99" Font-Bold="True" HorizontalAlign="Left" Height="25px" />
                    <EditRowStyle BackColor="#CCCCCC" Font-Bold="False" ForeColor="#333333" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <Fields>

                        <asp:BoundField DataField="id_qualwekplan" HeaderText="id_qualwekplan" InsertVisible="False" ReadOnly="True" SortExpression="id_qualwekplan" Visible="False" />
                        <asp:TemplateField HeaderText="editable" ShowHeader="False">
                            <EditItemTemplate>

                                <table class="gridtable">
                                    <tr class="header">
                                        <td colspan="7">Weekly RTK Projection Data Edit
                                            <br />
                                            <asp:Label ID="lbl_tgl1" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr class="header">
                                        <td colspan="2">Material</td>
                                        <td colspan="2">Contractor</td>
                                        <td colspan="2">Product</td>
                                        <td>Source</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label20" runat="server" Font-Size="18px" Font-Bold="true" Text='<%# Bind("MaterialKode")%>' ForeColor="yellow"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("KontraktorKode")%>'></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label21" runat="server" Text='<%# Eval("prod") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ds_rom1" DataTextField="Kode" DataValueField="Kode" SelectedValue='<%# Bind("SourceKode") %>'>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="header">
                                        <td colspan="7">Tonnage Projection</td>
                                    </tr>
                                    <tr class="header">
                                        <td>Contractor Avb</td>
                                        <td>Rom Avb</td>
                                        <td colspan="2">Blending Need</td>
                                        <td>Deviation</td>
                                        <td>Mining/day</td>
                                        <td>Hauling/day</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("KontraktorCap")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("ROMCap") %>'></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("blendNeed")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("deviatCap")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("dayminington")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("dayhaulton")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="header">
                                        <td colspan="7">Percentage</td>
                                    </tr>
                                    <tr class="header">
                                        <td colspan="4">Pit</td>
                                        <td colspan="3">Rom</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("pitPercent") %>' Width="80%"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("romPercent") %>' Width="80%"></asp:TextBox>
                                            <asp:Label ID="Label2" runat="server" Text="%"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="header">
                                        <td colspan="7">Quality Projection</td>
                                    </tr>
                                    <tr class="header">
                                        <td>TM</td>
                                        <td>IM</td>
                                        <td>HGI</td>
                                        <td>ASHAdb</td>
                                        <td>ASH AR</td>
                                        <td>TSAdb</td>
                                        <td>TS AR</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("TM")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("IM")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("HGI") %>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ASH_ADB")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("ASH_AR")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("TS_ADB")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("TS_AR")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="header">
                                        <td colspan="3">CalDaf</td>
                                        <td colspan="2">CalAdb</td>
                                        <td colspan="2">CV AR</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("CalDaf") %>'></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("CalAdb") %>'></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("CV_AR") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Fields>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="35px" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                </asp:DetailsView>
                <div style="text-align: right; padding-right: 5px; width: 100%;">
                    <asp:CheckBox ID="chb_ann1" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="arial" Font-Size="10pt" ForeColor="#0066CC" Text="Announce after update" />
                </div>
                <asp:SqlDataSource ID="ds_dtView" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                    SelectCommand="SELECT * FROM [vwqual_wekplan1] where id_qualwekplan = @id_qualwekplan "
                    UpdateCommand="Update [tbl_qualwekplan] SET 
                                      [KontraktorCap] = @KontraktorCap
                                      ,[ROMCap] = @ROMCap
                                      ,[deviatCap] = @deviatCap
                                      ,[blendNeed] = @blendNeed
                                      ,[dayminington] = @dayminington
                                      ,[dayhaulton] = @dayhaulton
                                      ,[TM] = @TM
                                      ,[IM] = @IM
                                      ,[ASH_ADB] = @ASH_ADB
                                      ,[ASH_AR] =@ASH_AR
                                      ,[TS_ADB] =@TS_ADB
                                      ,[TS_AR] =@TS_AR
                                      ,[CV_AR] =@CV_AR
                                      ,[SourceKode]=@SourceKode
                                    ,[pitPercent]=@pitPercent
                                    ,[romPercent]=@romPercent
                                    ,[HGI]=@HGI
                                    ,[CalAdb]=@CalAdb
                                    ,[CalDaf]=@CalDaf
                                where [id_qualwekplan] = @id_qualwekplan"
                    OnUpdated="ds_dtView_Updated">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="id_qualwekplan" QueryStringField="idwpr1" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="KontraktorCap" />
                        <asp:Parameter Name="ROMCap" />
                        <asp:Parameter Name="deviatCap" />
                        <asp:Parameter Name="blendNeed" />
                        <asp:Parameter Name="dayminington" />
                        <asp:Parameter Name="dayhaulton" />
                        <asp:Parameter Name="TM" />
                        <asp:Parameter Name="IM" />
                        <asp:Parameter Name="ASH_ADB" />
                        <asp:Parameter Name="ASH_AR" />
                        <asp:Parameter Name="TS_ADB" />
                        <asp:Parameter Name="TS_AR" />
                        <asp:Parameter Name="CV_AR" />
                        <asp:Parameter Name="SourceKode" />
                        <asp:Parameter Name="pitPercent" />
                        <asp:Parameter Name="romPercent" />
                        <asp:Parameter Name="HGI" />
                        <asp:Parameter Name="CalAdb" />
                        <asp:Parameter Name="CalDaf" />
                        <asp:Parameter Name="id_qualwekplan" />
                    </UpdateParameters>
                </asp:SqlDataSource>

            </asp:View>
        </asp:MultiView>

        <asp:SqlDataSource ID="ds_refName" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="select distinct NULL [id_prod], NULL [prod] union all SELECT id_ref_master, ref_name 
                                        from tbl_ref_master where id_ref_type = 5"></asp:SqlDataSource>
        <asp:SqlDataSource ID="ds_rom1" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" SelectCommand="SELECT NULL as [Kode]
                                UNION ALL
                                SELECT [Kode] 
                                FROM [v_Sources]"></asp:SqlDataSource>
    </form>
</body>
</html>
