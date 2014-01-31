using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace praktikfall
{
    public partial class EmployeeFrame : Form
    {
        public EmployeeFrame()
        {
            InitializeComponent();
            DataTable dt = controller.GetAllObjectsNr();
            dgvObject.DataSource = dt;
            dgvObjectShowing.DataSource = dt;
            DataTable dt2 = controller.GetAllProspectiveBuyers();
            dgvProspectiveBuyerShowing.DataSource = dt2;
        }

        Controller controller = new Controller();


        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbObjSearch.Text == "" || tbObjSearch.Text == "Sökord")
                {
                    tbObjSearch.Text = "";
                    tbObjSearch.ForeColor = Color.LightSlateGray;
                    tbObjSearch.Text = "Sökord";
                    MessageBox.Show("Du har ej angivit ett sökord"); 
                }

                else 
                {
                    string searchString = tbObjSearch.Text;
                    DataTable dt = controller.SearchObjectByString(searchString);
                    dgvObject.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion\n" + ex);
            }
            
        }

        private void btnAddProspectiveBuyer_Click(object sender, EventArgs e)
        {
            string ssnr = tbBuyerSsn.Text;
            string name = tbBuyerName.Text;
            string phonenr = tbBuyerTel.Text;
            string email = tbProspectiveBuyerEmail.Text;
            controller.AddProspectiveBuyer(ssnr, name, phonenr, email);

        }

        private void lblObjAddress_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvObject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvObject.Rows[e.RowIndex];
                
                lblObjAddress.Text = row.Cells["objAdress"].Value.ToString();
                lblObjCity.Text = row.Cells["objCity"].Value.ToString();
                lblPrice.Text = row.Cells["objPrice"].Value.ToString() + " kr";
                tbObjectArea.Text = row.Cells["objArea"].Value.ToString();
                tbNrOfRooms.Text = row.Cells["objRooms"].Value.ToString();
                tbUnitType.Text = row.Cells["objUnitType"].Value.ToString();
                richTextBox1.Text = row.Cells["objInfo"].Value.ToString();

                string price = row.Cells["objPrice"].Value.ToString();
                string area = row.Cells["objArea"].Value.ToString();
                int priceperkvm = int.Parse(price)/int.Parse(area);
                tbPricePerKvm.Text = priceperkvm.ToString();

            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddShowing_Click(object sender, EventArgs e)
        {
            
            
        }

        private void dgvObjectShowing_CellClicked(object sender, DataGridViewCellEventArgs e)
        {

        }

      
        private void tbObjSearchClick(object sender, EventArgs e)
        {
            tbObjSearch.Text = "";
            tbObjSearch.ForeColor = Color.Black;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string adress = tbObjAdress.Text;
            StringBuilder queryAddress = new StringBuilder();
            queryAddress.Append("https://maps.google.se/");
            queryAddress.Append(adress + "," + "+");
            webBrowser1.Navigate(queryAddress.ToString());

        }




    }
}
