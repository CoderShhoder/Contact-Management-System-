<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.aspx.cs" Inherits="Assessment.ContactForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <h2>Contact Form</h2>
                <hr />
        <div class="d-flex flex-row">
            
            <div class="d-flex flex-column px-5 py-3" id="formContainer">
                <div>
                    <div class="form-group">
                        <label for="txtCompanyName">Company Name:</label>
                        <asp:TextBox ID="txtCompanyName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <div class="form-group">
                        <label for="txtFirstName">First Name:</label>
                        <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <div class="form-group">
                        <label for="txtLastName">Last Name:</label>
                        <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <div class="form-group">
                        <label for="txtEmail">Email:</label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <div class="form-group">
                        <label for="ddlCategory">Category:</label>
                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Client" Value="Client"></asp:ListItem>
                            <asp:ListItem Text="Vendor" Value="Vendor"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div>
                    <div class="form-group">
                        <label for="ddlProfession">Profession:</label>
                        <asp:DropDownList ID="ddlProfession" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="py-2">
                    <asp:Button ID="btnAddContact" CssClass="btn btn-primary" runat="server" Text="Add Contact" OnClick="btnAddContact_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Update" CssClass="btn btn-secondary" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                </div>

                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </div>
            <div id="gridConatiner" >
                <asp:GridView ID="GridView1" runat="server" CssClass="py-9" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnEdit" runat="server" Height="25px" ImageUrl="~/imgs/edit.png" CommandName="EditContact" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
