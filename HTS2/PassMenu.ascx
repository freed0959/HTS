<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PassMenu.ascx.vb" Inherits="HTS2_PassMenu" %>
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
         <asp:SqlDataSource ID="ds_sumpos" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" 
                 SelectCommand="_sp_rtk_pos1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>