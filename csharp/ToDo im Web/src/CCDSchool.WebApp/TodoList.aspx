<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="CCDSchool.WebApp.TodoList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr id="trLogin" runat="server">
                    <td>Your Email Address:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoginEmail" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtLoginEmail" ErrorMessage="Required field cannot be left blank."
                            Display="Dynamic" ValidationGroup="login">
                        </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="Invalid email address." ControlToValidate="txtLoginEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic" ValidationGroup="login">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" ValidationGroup="login" Text="Login" OnClick="btnLogin_Click" />
                    </td>

                </tr>
                <tr id="trLoggedin" runat="server">
                    <td style="font-family: Verdana;">Logged in as: 
                        <asp:Label ID="lblUserName" runat="server"></asp:Label>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" OnClick="lnkLogout_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="background-color: black; line-height: 5px; margin-top: 10px;"></td>
                </tr>
            </table>
            <table style="width: 80%;margin-top:20px;font-size:17px;">
                <tr>
                    <td style="width: 15%; vertical-align: top;">Todo List<br />
                        <asp:ListBox Width="160px" style="font-size:17px;height:122px;" ID="lstTaskListV" AutoPostBack="true" runat="server" OnSelectedIndexChanged="lstTaskListV_SelectedIndexChanged"></asp:ListBox>
                    </td>
                    <td style="width: 65%; vertical-align: top;">Task<br />
                        <asp:Panel ID="pnl" runat="server">
                            <asp:GridView ID="gdvTask" runat="server" CellPadding="4" AutoGenerateColumns="false" Width="100%" ForeColor="#333333" GridLines="Both" OnRowDataBound="gdvTask_RowDataBound" OnRowCommand="gdvTask_RowCommand" OnRowDeleting="gdvTask_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Title" DataField="Title" ItemStyle-Width="20%" />
                                    <asp:BoundField HeaderText="DueDate" DataField="DueDate" DataFormatString="{0:dd.MM.yyyy HH:mm}" ItemStyle-Width="20%" />
                                    <asp:CheckBoxField HeaderText="Important" DataField="Important" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                    <asp:BoundField HeaderText="Owner" DataField="Owner" ItemStyle-Width="20%" />
                                    <asp:TemplateField ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" ForeColor="#5D7B9D" CommandArgument='<%# Eval("Id") %>' CommandName="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" ForeColor="#5D7B9D" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');" CommandArgument='<%# Eval("Id") %>' CommandName="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkArchieve" runat="server" Text="Mark Archieve" OnClientClick="javascript:return confirm('Please take a note that once the task is archieved that should not be reverted to active. would you like to continue?');" ForeColor="#5D7B9D" CommandArgument='<%# Eval("Id") %>' CommandName="Archieve"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                <EmptyDataTemplate>
                                    <div style="text-align: center; color: red;">No records to display</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr id="taskrow" runat="server" style="visibility: hidden;">
                    <td style="width: 15%; vertical-align: top;">
                        <asp:TextBox ID="txtTodoList" Width="100px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqList" runat="server" ControlToValidate="txtTodoList" ValidationGroup="addlist" ErrorMessage="Please enter List Title" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnAddList" runat="server" Text="+ " ValidationGroup="addlist" OnClick="btnAddList_Click" />

                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <asp:TextBox ID="txtTask" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqTask" runat="server" ControlToValidate="txtTask" ValidationGroup="addtask" ErrorMessage="Please enter task Title" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnAddTask" runat="server" Text="+ " ValidationGroup="addtask" OnClick="btnAddTask_Click" />
                        <span style="float: right;">
                            <asp:CheckBox ID="chkArchived" AutoPostBack="true" runat="server" OnCheckedChanged="chkArchived_CheckedChanged" />Show Archived</span>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>

</html>
