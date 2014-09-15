<%@ Page Title="Truck ROM Distribution Data Entry Page" Language="VB" AutoEventWireup="false" CodeFile="passentry2.aspx.vb" Inherits="passentry2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />       
    <meta name="viewport" content="width=device-width, initial-scale=1" />   
    
    <title></title>
    
    <link href="css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />    
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtSearch1.ClientID%>").autocomplete('Search_VB.ashx');
        });
    </script>
        
    <script src="dtscripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="dtscripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="dtscripts/calendar-blue.css" rel="stylesheet" type="text/css" />    

      <!-- CSS goes in the document HEAD or added to your external stylesheet -->
        <style type="text/css"> 
        table.gridtable {
	        font-family: verdana,arial,sans-serif; font-size:11px; color:#333333; border-width: 1px;
	        border-color: #666666; border-collapse: collapse;
        }
        table.gridtable th {
	        border-width: 1px; padding: 8px; border-style: solid; border-color: #666666;
	        background-color:darkorange; color:blue; padding-top:20px;
        }
        table.gridtable td {
	        border-width: 1px; padding: 8px; border-style: solid; border-color: #666666; background-color: #ffffff;
        }
        table.gridtable td.tdfs {
            background-color: #dedede; font-size: 14px; 
            }
       table.gridtable td.footr {
                background-color:#CCCCCC; padding-left: 8px; height:27px; font-size:14px; word-spacing:5px;
            }  
       input[type="text"] {    
                height:25px; width:225px; font-family:arial Verdana Calibri; font-size:24px;
            }
       input[type="option"] {    
                height:25px; width:225px; font-family:arial Verdana Calibri; font-size:24px;
            }   
        </style>

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

</head>
<body>
    <form id="form1" runat="server">

        <asp:Panel ID="conPanel1" runat="server">
         
                <table style="width: 100%; padding:0px; " cellspacing="0" class="gridtable">
                    <tr>
                        <th colspan="2">
                            <h4>
                                <asp:Label ID="Label8" runat="server" Text="Entry Truck to ROM Data"></asp:Label>
                            </h4>
                        </th>
                    </tr>                                    
                    <tr>
                        <td class="tdfs">
                            <asp:Label ID="Label10" runat="server" Text="Truck No."></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch1" runat="server" Columns="15"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdfs">
                            <asp:Label ID="Label11" runat="server" Text="Raw Cargo"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_raw2" runat="server" DataSourceID="SqlDataSource7" DataTextField="seamcargo" DataValueField="Kode" Font-Names="Arial" Font-Size="18pt">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdfs">
                            <asp:Label ID="Label12" runat="server" Text="ROM"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_rom2" runat="server" DataSourceID="SqlDataSource8" DataTextField="Kode" DataValueField="Kode" Font-Names="Arial" Font-Size="18pt">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="footr">
                            <asp:LinkButton ID="btn_entry1" runat="server">Insert</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton4" runat="server">Cancel</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:CheckBox ID="chb_clos2" runat="server" Font-Names="Arial" Font-Size="11pt" Text="Close After Insert" />
                </div>
                <div style="margin-top: 10px;">
                    <asp:Label ID="lbl_info2" runat="server" ForeColor="Maroon" Font-Names="Arial"></asp:Label>
                </div>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" 
                    SelectCommand="_sp_ql_lasmap1" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:HTSdbConn %>" 
                    SelectCommand="SELECT * FROM [vwrom_active1] ORDER BY Kode"></asp:SqlDataSource>
        

        </asp:Panel>
    <div>
    
    </div>
    </form>
</body>

</html>
