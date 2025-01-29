using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNatur_Biomarkt_GmbH
{
    public partial class ProductsScreen : Form
    {   
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\enjis\Documents\Pro-Natur Biomarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductKey;
        
        public ProductsScreen()
        {
            InitializeComponent();
            
            ShowProducts();


        }
        private void ShowProducts()
        {


            databaseConnection.Open();

            string query = "select*from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            productsDGV.DataSource = dataSet.Tables[0];
            productsDGV.Columns[0].Visible = false;

            databaseConnection.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if(textBoxProductName.Text=="" 
                || textBoxProductBrand.Text==""
                || comboBoxProductCategory.Text==""
                || textBoxProductPrice.Text=="")
            {
                MessageBox.Show("Bitte fülle alle Werte aus.");
                return;
            }
            // save product name in database
            string productName = textBoxProductName.Text; 
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("INSERT INTO Products VALUES('{0}','{1}','{2}','{3}')", productName, productBrand, productCategory, productPrice);
            ExecuteQuery(query);
            ClearAllFilds();
            ShowProducts();
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produke aus.");
                return;
            }
            string productName = textBoxProductName.Text;
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("UPDATE Products SET Name = '{0}',Brand='{1}',Category='{2}',Price='{3}' WHERE Id ={4}"
                , productName,productBrand,productCategory,productPrice,lastSelectedProductKey);

            ExecuteQuery(query);

            ShowProducts();
        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFilds();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {   
            if(lastSelectedProductKey==0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produke aus.");
                return;
            }
            string query = string.Format("DELETE FROM Products WHERE Id={0};", lastSelectedProductKey);
            ExecuteQuery(query);
            ClearAllFilds();
            ShowProducts();
        }
        private void ExecuteQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }
        private void ClearAllFilds()
        {
            textBoxProductName.Text = "";
            textBoxProductBrand.Text = "";
            textBoxProductPrice.Text = "";
            comboBoxProductCategory.Text = "";
            comboBoxProductCategory.SelectedItem = null;
        }

        private void productsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxProductName.Text= productsDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductBrand.Text = productsDGV.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxProductCategory.Text = productsDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxProductPrice.Text = productsDGV.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectedProductKey = (int)productsDGV.SelectedRows[0].Cells[0].Value;


        }
    }
}
