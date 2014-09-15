<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PassMenu.ascx.vb" Inherits="HTS2_PassMenu" %>

<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">

    <asp:View runat="server" ID="view_adaro1">

        <div class="templatemo_section">
            <div class="templatemo_section_title">
                Trucks Position & Page Menu
            </div>
            <div class="templatemo_section_bottom">
                <asp:DataList ID="DataList1" runat="server" DataSourceID="ds_sumpos">
                    <ItemTemplate>
                        <asp:HyperLink ID="PositionHyp" runat="server" Text='<%# Eval("pos")%>'
                            NavigateUrl='<%# String.Concat("~/HTS2/Passing?v=", Eval("idpos"))%>'></asp:HyperLink>:
                                <asp:Label ID="jummatLabel" runat="server" Text='<%# String.Concat(Eval("jummat"), " trucks") %>' Font-Size="13px" />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>

    </asp:View>

    <asp:View runat="server" ID="view_contractor">

        <div class="templatemo_section">
            <div class="templatemo_section_title">
                Trucks to ROM
            </div>
            <div class="templatemo_section_bottom">
                <asp:DataList ID="DataList2" runat="server" DataSourceID="ds_truck_rom1">
                    <ItemTemplate>
                        <asp:HyperLink ID="PositionHyp" runat="server" Text='<%# Eval("SourceKode")%>'
                            NavigateUrl='<%# String.Concat("~/HTS2/Passing?v=", Eval("SourceKode"))%>'></asp:HyperLink>:
                                <asp:Label ID="jummatLabel" runat="server" Text='<%# String.Concat(Eval("jumtruck"), " trucks")%>' Font-Size="13px" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="ds_truck_rom1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
                    SelectCommand="_sp_pascon_pos1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            </div>
        </div>

        <div class="templatemo_section">
            <div class="templatemo_section_title">
                Trucks Position 
            </div>
            <div class="templatemo_section_bottom">
                <asp:DataList ID="DataList3" runat="server" DataSourceID="ds_sumpos">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("pos")%>'></asp:Label>:
                                <asp:Label ID="jummatLabel" runat="server" Text='<%# String.Concat(Eval("jummat"), " trucks") %>' Font-Size="13px" />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>

    </asp:View>
</asp:MultiView>





<asp:SqlDataSource ID="ds_sumpos" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>"
    SelectCommand="_sp_rtk_pos1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
