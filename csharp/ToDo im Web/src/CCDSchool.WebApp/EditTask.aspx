<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="CCDSchool.WebApp.EditTask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" media="all" type="text/css" href="jquery-ui.css" />
	
 <link rel="stylesheet" media="all" type="text/css" href="jquery-ui-timepicker-addon.css" />
	
 
    <style type="text/css">
        .left {
            width:25%;
            vertical-align:top;
        }
         .right {
            width:75%;
            vertical-align:top;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:35%">
                <tr>
                    <td class="left">Title
                    </td>
                    <td class="right">
                        <asp:TextBox ID="txtTaskTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="left">Description
                    </td>
                    <td class="right">
                        <asp:TextBox ID="txtDesc" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="left">Important
                    </td>
                    <td class="right">
                        <asp:CheckBox ID="chkImportant" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td class="left">DueDate
                    </td>
                    <td class="right">
                        <asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td class="left"> 
                    </td>
                    <td class="right">
                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
    <script type="text/javascript" src="jquery-1.10.1.min.js"></script>
		<script type="text/javascript" src="jquery-ui.min.js"></script>
		<script type="text/javascript" src="jquery-ui-timepicker-addon.js"></script>
		<script type="text/javascript" src="jquery-ui-sliderAccess.js"></script>
		<script type="text/javascript" src="script.js"></script>
</html>
