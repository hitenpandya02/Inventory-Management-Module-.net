using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace InventoryManagementModule
{
    public partial class Inventory : System.Web.UI.Page
    {
        string connectionString =
        ConfigurationManager.ConnectionStrings["InventoryDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayProducts();
            }
        }

        // Display all products
        void DisplayProducts()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products ORDER BY ProductID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        // Insert product with automatic ProductID
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            int quantity;
            decimal price;

            if (!int.TryParse(txtQuantity.Text, out quantity) ||
                !decimal.TryParse(txtPrice.Text, out price))
            {
                lblMessage.Text = "Enter a valid quantity and price.";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Products
                             (ProductName, Quantity, Price)
                             VALUES
                             (@ProductName, @Quantity, @Price)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductName", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Product inserted successfully.";

            ClearFields();
            DisplayProducts();
        }

        // Display records button
        protected void btnView_Click(object sender, EventArgs e)
        {
            DisplayProducts();
            lblMessage.Text = "Records displayed successfully.";
        }

        // Clear fields button
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            lblMessage.Text = "";
        }

        // Clear input fields
        void ClearFields()
        {
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }

        // Enable editing
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            DisplayProducts();
        }

        // Cancel editing
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            DisplayProducts();
        }

        // Update product
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int productID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            GridViewRow row = GridView1.Rows[e.RowIndex];

            string productName =
                ((TextBox)row.Cells[1].Controls[0]).Text.Trim();

            int quantity =
                Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text);

            decimal price =
                Convert.ToDecimal(((TextBox)row.Cells[3].Controls[0]).Text);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Products
                             SET ProductName = @ProductName,
                                 Quantity = @Quantity,
                                 Price = @Price
                             WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@ProductName", productName);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            GridView1.EditIndex = -1;
            DisplayProducts();

            lblMessage.Text = "Product updated successfully.";
        }

        // Delete product
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            DisplayProducts();
            lblMessage.Text = "Product deleted successfully.";
        }
    }

}