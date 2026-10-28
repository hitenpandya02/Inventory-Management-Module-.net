<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="InventoryManagementModule.Inventory" %>

<!DOCTYPE html><html>
<head runat="server">
    <title>Inventory Management</title>
</head>
<body>
    <form id="form1" runat="server">    <h2>Inventory Management System</h2>

    <asp:Button ID="btnView" runat="server"
        Text="Display Records"
        OnClick="btnView_Click" />

    <asp:Button ID="btnClear" runat="server"
        Text="Clear Fields"
        OnClick="btnClear_Click" />

    <br /><br />

    <h3>Add Product</h3>

    Product Name:
    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    <br /><br />

    Quantity:
    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
    <br /><br />

    Price:
    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnAdd" runat="server"
        Text="Insert Product"
        OnClick="btnAdd_Click" />

    <br /><br />

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <h3>Product Records</h3>

    <asp:GridView ID="GridView1" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ProductID"
        OnRowEditing="GridView1_RowEditing"
        OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowUpdating="GridView1_RowUpdating"
        OnRowDeleting="GridView1_RowDeleting">

        <Columns>
            <asp:BoundField DataField="ProductID"
                HeaderText="Product ID"
                ReadOnly="True" />

            <asp:BoundField DataField="ProductName"
                HeaderText="Product Name" />

            <asp:BoundField DataField="Quantity"
                HeaderText="Quantity" />

            <asp:BoundField DataField="Price"
                HeaderText="Price" />

            <asp:CommandField ShowEditButton="True"
                ShowDeleteButton="True" />
        </Columns>

    </asp:GridView>

</form>

</body>
</html>