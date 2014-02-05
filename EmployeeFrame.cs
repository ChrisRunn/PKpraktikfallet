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
        public EmployeeFrame(string name, bool b)
        {
           
            InitializeComponent();
            string brokerName = name;
            labelEmpName.Text = brokerName;

            if (b == true)
            {
                groupBox5.Visible = true;
            }

            else
            {
                groupBox5.Visible = false;
                tabPage3.Text = "";
            }

           

            Populate();

            DataTable dt = controller.SearchObjectByBrokerSsnr(brokerName);
            dataGridView1.DataSource = dt;

            DataTable dt2 = controller.SearchShowingsByBrokerSsnr(brokerName);
            dataGridView2.DataSource = dt2;
        }

        Controller controller = new Controller();

        public void Populate()                                                            //Uppdatera ALLA DGVS (utom Christians)
        {
            DataTable dtAllObjects = controller.GetAllObjectsNr(); 
            dgvObject.DataSource = dtAllObjects;                                          //Objektfliken, alla objekt i databasen          
            dgvObjectShowing.DataSource = dtAllObjects;                                   //Visningsfliken, alla objekt i databasen            
            DataTable dtAllProspectiveBuyers = controller.GetAllProspectiveBuyers();           
            dgvProspectiveBuyerShowing.DataSource = dtAllProspectiveBuyers;               //Visningsfliken, alla spekulanter i databasen 
            DataTable dtAllShowings = controller.GetShowings();
            dgvShowingCurrentShowings.DataSource = dtAllShowings;                         //Visningsfliken, alla nuvarande visningar i databasen
            DataTable dtAllBrokers = controller.GetAllBrokers();                          //Mäklarfliken (admin), visa alla mäklare
            dgvBrokerAllBrokers.DataSource = dtAllBrokers;
        }



        /*private void btnObjAdd_Click(object sender, EventArgs e)
        {
            try
            {

                int parsedValue;
                if (!int.TryParse(tbObjNr.Text, out parsedValue))
                {
                    MessageBox.Show("Objektnummer får endast innehålla siffror");

                }

                else if (tbObjAdress.Text == "")
                {
                    MessageBox.Show("Du har ej angivit en adress.");
                }

                else if (tbObjPrice.Text == "")
                {
                    MessageBox.Show("Du har ej angivit ett pris.");
                }

                else if (tbObjArea.Text == "")
                {
                    MessageBox.Show("Du har ej angivit area");
                }              

                else if (tbObjRoom.Text == "")
                {
                    MessageBox.Show("Du har ej angivit antal rum");
                }

                else if (tbObjUnitType.Text == "")
                {
                    MessageBox.Show("Du har ej angivit bostadstyp");
                }

                else if (rtbObjInfo.Text == "")
                {
                    MessageBox.Show("Du har ej angivit någon beskrivning");
                }

                else if (tbBrokerNr.Text == "")
                {
                    MessageBox.Show("Du har ej angivit mäklarnummer.");
                }

                else
                {
                    string objNr = tbObjNr.Text;
                    string objAdress = tbObjAdress.Text;
                    int objPrice = int.Parse(tbObjPrice.Text);
                    string objRooms = tbObjRoom.Text;
                    string objUnitType = tbObjUnitType.Text;
                    double objArea = double.Parse(tbObjArea.Text);
                    string objInfo = rtbObjInfo.Text;
                    string brokerSsnr = tbBrokerNr.Text;
                    string objCity = tbObjCity.Text;

                    Controller controller = new Controller();

                    // DateTime result = dateTimePicker1.Value;
                    //string date = result.ToString();

                    controller.AddObject(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr);
                    MessageBox.Show("Objekt registrerat!");
                    DataTable dt = controller.GetAllObjectsNr();
                    dgvObject.DataSource = dt;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Det går inte att registrera objektet\n" + ex);
            }

        }*/

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
            try
            {

                int parsedValue;
                if (!int.TryParse(tbBuyerSsn.Text, out parsedValue))
                {
                    MessageBox.Show("Personnummer får endast innehålla siffror");

                }

                else if (tbBuyerName.Text == "")
                {
                    MessageBox.Show("Du har ej angivit ett namn");
                }

                else if (tbBuyerTel.Text == "")
                {
                    MessageBox.Show("Du har ej angivit ett telefonnummer");
                }

                else if (tbProspectiveBuyerEmail.Text == "")
                {
                    MessageBox.Show("Du har ej angivit en email");
                }

                else
                {
                    string ssnr = tbBuyerSsn.Text;
                    string name = tbBuyerName.Text;
                    string phonenr = tbBuyerTel.Text;
                    string email = tbProspectiveBuyerEmail.Text;
                    controller.AddProspectiveBuyer(ssnr, name, phonenr, email);
                    MessageBox.Show("Ny spekulant registrerad");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Det går inte att registrera en spekulant/n" + ex);
            }


        }

        private void lblObjAddress_Click(object sender, EventArgs e)
        {

        }



        private void dgvObject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbObjOwnerSsnr.Text = "";
            tbObjOwnerPhoneNr.Text = "";
            tbObjOwnerEmail.Text = "";
            tbObjOwnerName.Text = "";

            if (e.RowIndex >= 0)
            {  
                DataGridViewRow row = this.dgvObject.Rows[e.RowIndex];
                string objNr = row.Cells["objNr"].Value.ToString();
                DataTable ownerSsnr = this.controller.GetObjOwner(objNr);
                foreach (DataRow row1 in ownerSsnr.Rows)
                {
                var text = row1[1].ToString();
                tbObjOwnerSsnr.Text = text;
                }
                DataTable ownerInfo = this.controller.GetObjectOwner(tbObjOwnerSsnr.Text);
                foreach (DataRow row1 in ownerInfo.Rows)
                {
                        tbObjOwnerPhoneNr.Text = row1[1].ToString();
                        tbObjOwnerEmail.Text = row1[2].ToString();
                        tbObjOwnerName.Text = row1[3].ToString();   
                    
                }
                lblObjAddress.Text = row.Cells["objAdress"].Value.ToString();
                lblObjCity.Text = row.Cells["objCity"].Value.ToString();
                lblPrice.Text = row.Cells["objPrice"].Value.ToString() + " kr";
                tbObjectArea.Text = row.Cells["objArea"].Value.ToString();
                tbNrOfRooms.Text = row.Cells["objRooms"].Value.ToString();
                tbUnitType.Text = row.Cells["objUnitType"].Value.ToString();
                richTextBox1.Text = row.Cells["objInfo"].Value.ToString();
                string price = row.Cells["objPrice"].Value.ToString();
                string area = row.Cells["objArea"].Value.ToString();
                int priceperkvm = int.Parse(price) / int.Parse(area);
                tbPricePerKvm.Text = priceperkvm.ToString();
                tbObjBrokerSsnr.Text = row.Cells["brokerSsnr"].Value.ToString();
                tbObjNr.Text = row.Cells["objNr"].Value.ToString();
                tbObjCity.Text = row.Cells["objCity"].Value.ToString();
                tbObjPrice.Text = row.Cells["objPrice"].Value.ToString();
                tbObjAddress.Text = row.Cells["objAdress"].Value.ToString();
        
            }

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddShowing_Click(object sender, EventArgs e)                //Lägg till visning
        {
            string objNr = lblSelectedObjectShowing.Text;
            string showingDate = dtpVisningsdatumVisning.Text;
            string buyerSsnr = lblSelectedBuyerShowing.Text;
            bool showingExists = controller.ShowingExists(objNr, buyerSsnr);        //Kontrollerar om visningen redan finns

            if (showingExists)
            {
                MessageBox.Show("Visningen finns redan. Vänligen kontrollera dina val. Använd Uppdatera datum-knappen för att uppdatera visningsdatum.");
            }

            if (lblSelectedBuyerShowing.Text.Equals("selectedBuyer(invisible)"))
            {
                MessageBox.Show("Vänligen välj spekulant i listan.");
            }
            if (lblSelectedObjectShowing.Text.Equals("selectedObject(invisible)"))
            {
                MessageBox.Show("Vänligen välj objekt i listan.");
            }
            if(!showingExists && !(lblSelectedBuyerShowing.Text.Equals("selectedBuyer(invisible)")) && !(lblSelectedObjectShowing.Text.Equals("selectedObject(invisible)")))
            {              
            int nrOfRows = controller.RegisterShowing(objNr, buyerSsnr, showingDate);
            MessageBox.Show("Visning registrerad!");
                Populate();
        }
        }




        private void tbObjSearchClick(object sender, EventArgs e)
        {
            tbObjSearch.Text = "";
            tbObjSearch.ForeColor = Color.Black;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //string adress = tbObjAdress.Text;
            StringBuilder queryAddress = new StringBuilder();
            queryAddress.Append("https://maps.google.se/");
            // queryAddress.Append(adress + "," + "+");
            // webBrowser1.Navigate(queryAddress.ToString());

        }

        private void groupBoxVisning_Enter(object sender, EventArgs e)
        {

        }

        private void dgvObjectShowing_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvObjectShowing.Rows[e.RowIndex];
                string selectedItem = row.Cells["objNr"].Value.ToString();
                lblSelectedObjectShowing.Text = selectedItem;

            }
        }

        private void dgvProspectiveBuyerShowing_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvProspectiveBuyerShowing.Rows[e.RowIndex];
                string selectedItem = row.Cells["buyerSsnr"].Value.ToString();
                lblSelectedBuyerShowing.Text = selectedItem;              
                tbBuyerSsn.Text = row.Cells["buyerSsnr"].Value.ToString();
                tbBuyerName.Text = row.Cells["name"].Value.ToString();
                tbBuyerTel.Text = row.Cells["phoneNr"].Value.ToString();
                tbProspectiveBuyerEmail.Text = row.Cells["email"].Value.ToString();
                
            }
        }
       
        private void EmployeeFrame_FormClosed(object sender, FormClosedEventArgs e)         //Avslutar applikationen när användaren stänger EmployeeFrame
        {
            Application.Exit();
        }

        private void lblSelectedObjectShowing_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchProBuyer_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbSearchProBuyer.Text == "" || tbSearchProBuyer.Text == "Sökord")
                {
                    tbSearchProBuyer.Text = "";
                    tbSearchProBuyer.ForeColor = Color.LightSlateGray;
                    tbSearchProBuyer.Text = "Sökord";
                    MessageBox.Show("Du har ej angivit ett sökord");
                }

                else
                {
                    string searchString = tbSearchProBuyer.Text;
                    DataTable dt = controller.SearchProBuyerByString(searchString);
                    dgvProspectiveBuyerShowing.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion\n" + ex);
            }
        }

        private void tb_clickSearch(object sender, EventArgs e)
        {
            tbSearchProBuyer.Text = "";
            tbSearchProBuyer.ForeColor = Color.Black;

        }

        private void dgvProBuyer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvProspectiveBuyerShowing.Rows[e.RowIndex];

                tbBuyerSsn.Text = row.Cells["buyerSsnr"].Value.ToString();
                tbBuyerName.Text = row.Cells["name"].Value.ToString();
                tbBuyerTel.Text = row.Cells["phoneNr"].Value.ToString();
                tbProspectiveBuyerEmail.Text = row.Cells["email"].Value.ToString();


            }
        }




        private void btnShowMap_Click(object sender, EventArgs e)
        {

            if (tbObjAddress.Text == "" || tbObjCity.Text == "")
            {
                MessageBox.Show("Du har ej valt ett objekt");
            }
            else
            {
                MapFrame mf = new MapFrame(tbObjAddress.Text, tbObjCity.Text);
                mf.Show();
            }
        }

        private void btnShowingUpdate_Click(object sender, EventArgs e)
        {

            string objNr = lblSelectedObjectShowing.Text;
            string showingDate = dtpVisningsdatumVisning.Text;
            string buyerSsnr = lblSelectedBuyerShowing.Text;

            if (lblSelectedBuyerShowing.Text.Equals("selectedBuyer(invisible)"))
            {
                MessageBox.Show("Vänligen välj spekulant i listan.");
            }

            if (lblSelectedObjectShowing.Text.Equals("selectedObject(invisible)"))
            {
                MessageBox.Show("Vänligen välj objekt i listan.");
            }

            else
            {
            int nrOfRows = controller.UpdateShowing(objNr, buyerSsnr, showingDate);
                MessageBox.Show("Visning uppdaterad. Nytt visningsdatum "+showingDate);
                Populate();
            }
            
        }
           
        private void dgvShowingCurrentShowings_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvShowingCurrentShowings.Rows[e.RowIndex];

            string objNr = row.Cells["objNr"].Value.ToString();
            string buyerSsnr = row.Cells["buyerSsnr"].Value.ToString();
            lblShowingSelectedBuyerDelete.Text = buyerSsnr;
            lblShowingSelectedObjNrDelete.Text = objNr;
          
        }

        private void btnShowingDelete_Click(object sender, EventArgs e)                         //Ta bort visning/Ta bort spekulant från visning
        {
            string buyerSsnr = lblShowingSelectedBuyerDelete.Text;
            string objNr = lblShowingSelectedObjNrDelete.Text;

            if (rbShowingDeleteShowing.Checked)                                                  //Om "ta bort hela visningen" är valt
            {
                if (lblShowingSelectedObjNrDelete.Text.Equals("selectedForDelete(invisible)"))
                {
                    MessageBox.Show("Vänligen välj ett objekt i listan först.");
                }
                else
                {
                    int nrOfRows = controller.DeleteShowing(objNr);
                    MessageBox.Show("Visning borttagen");
                    Populate();
                    lblShowingSelectedBuyerDelete.Text = "selectedForDelete(invisible)";
                    lblShowingSelectedObjNrDelete.Text = "selectedForDelete(invisible)";
                }                 
            }                                               
            if (!rbShowingDeleteBuyer.Checked && !rbShowingDeleteShowing.Checked)               // Om inget val gjorts, ge feedback
            {
                MessageBox.Show("Vänligen välj ett alternativ (ta bort alla visningar eller ta bort spekulant från visning) först.");
            }       

            if (rbShowingDeleteBuyer.Checked)                                                   //Om "ta bort spekulant" är valt
            {
                if (lblShowingSelectedBuyerDelete.Text.Equals("selectedForDelete(invisible)"))
                {
                    MessageBox.Show("Vänligen välj en spekulant i listan först.");
                }
                else
                {
                    int nrOfRows = controller.DeleteBuyerFromShowing(buyerSsnr, objNr);
                    MessageBox.Show("Spekulant borttagen från visning");
                    Populate();
                    lblShowingSelectedBuyerDelete.Text = "selectedForDelete(invisible)";
                    lblShowingSelectedObjNrDelete.Text = "selectedForDelete(invisible)";
                }
                
            }                                   
        }
 
        private void cbObjUpdateClick(object sender, EventArgs e)
        {
            dgvObject_CellClick(dgvObject, new DataGridViewCellEventArgs(0, 0));
        }

        private void btnObjSubmit_Click(object sender, EventArgs e)
        {
            
                MessageBox.Show("knappen är itryckt");
                string objArea = tbObjectArea.Text;
                string objRooms = tbNrOfRooms.Text;
                string objUnitType = tbUnitType.Text;
                string objInfo = richTextBox1.Text;
                string objNr = tbObjNr.Text;
                string objCity = tbObjCity.Text;
                string objPrice = tbObjPrice.Text;
                string objAdress = tbObjAddress.Text;
                string ownerSsnr = tbObjOwnerSsnr.Text;
                string phoneNr = tbObjOwnerPhoneNr.Text;
                string email = tbObjOwnerEmail.Text;
                string name = tbObjOwnerName.Text;
                int nrOfRows = this.controller.UpdateObjectFlik(objNr, objAdress, objArea, objCity, objInfo, objPrice, objRooms, objUnitType, phoneNr, email, name, ownerSsnr);
                MessageBox.Show(nrOfRows.ToString());
                Populate();
                

            
             if (cbObjRegister.Checked && !cbObjUpdate.Checked && !cbObjDelete.Checked)
            {

            }
            else if (cbObjDelete.Checked && !cbObjUpdate.Checked && !cbObjRegister.Checked)
            {
                
            }
        }

        private void btnAddProspectiveBuyer_Click_1(object sender, EventArgs e)
        {
            try
            {

                int parsedValue;
                if (!int.TryParse(tbBuyerSsn.Text, out parsedValue))
                {
                    MessageBox.Show("Personnummer får endast innehålla siffror");

                }

                else if (tbBuyerName.Text == "")
                {
                    MessageBox.Show("Du har ej angivit ett namn");
                }

                else if (tbBuyerTel.Text == "")
                {
                    MessageBox.Show("Du har ej angivit ett telefonnummer");
                }

                else if (tbProspectiveBuyerEmail.Text == "")
                {
                    MessageBox.Show("Du har ej angivit en email");
                }

                else
                {  
                    
                    string ssnr = tbBuyerSsn.Text;
                    string name = tbBuyerName.Text;
                    string phonenr = tbBuyerTel.Text;
                    string email = tbProspectiveBuyerEmail.Text;
                    bool prospectiveBuyerExists = controller.ProspectiveBuyerExists(ssnr);

                    if (prospectiveBuyerExists)
                    {
                        MessageBox.Show("Det finns redan en spekulant med personnummer: " + ssnr);
                    }

                    else
                    {
                        controller.AddProspectiveBuyer(ssnr, name, phonenr, email);
                        MessageBox.Show("Ny spekulant med personnummer " + ssnr + " har registrerats");
                        Populate();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Det går inte att registrera en spekulant/n" + ex);
            }
        }

        private void btnSearchProBuyer_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tbSearchProBuyer.Text == "" || tbSearchProBuyer.Text == "Sökord")
                {
                    tbSearchProBuyer.Text = "";
                    tbSearchProBuyer.ForeColor = Color.LightSlateGray;
                    tbSearchProBuyer.Text = "Sökord";
                    MessageBox.Show("Du har ej angivit ett sökord");
                }

                else
                {
                    string searchString = tbSearchProBuyer.Text;
                    DataTable dt = controller.SearchProBuyerByString(searchString);
                    dgvProspectiveBuyerShowing.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion\n" + ex);
            }
        }

        private void btnUpdateProspectiveBuyer_Click(object sender, EventArgs e)
        {

            if (tbBuyerSsn.Text == "")
            {
                MessageBox.Show("Du har ej valt eller angivit ett personnummer");
        }
            else
            {

                string buyerSsnr = tbBuyerSsn.Text;
                string name = tbBuyerName.Text;
                string phoneNr = tbBuyerTel.Text;
                string email = tbProspectiveBuyerEmail.Text;
                int nrOfRows = controller.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                MessageBox.Show("Spekulant " + buyerSsnr + " uppdaterad!");
                Populate();
            }
        }

        private void btnDeleteProspectiveBuyer_Click(object sender, EventArgs e)
        {
            if (tbBuyerSsn.Text == "")
            {
                MessageBox.Show("Du har ej valt eller angivit ett personnummer");
            }
            else
            {
                string buyerSsnr = tbBuyerSsn.Text;
                int nrOfRows = controller.DeleteProspectiveBuyer(buyerSsnr);
                MessageBox.Show("Spekulant " + buyerSsnr + " raderad!");
                Populate();
            }
        }

        private void tbSearchProBuyer_Click(object sender, EventArgs e)
        {
            tbSearchProBuyer.Text = "";
            tbSearchProBuyer.ForeColor = Color.Black;
        }

        private void dgvShowingObject_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS /Marcus
        {
            dgvObjectShowing.ClearSelection();
        }

        private void dgvShowingProspecitveBuyer_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS /Marcus
        {
            dgvProspectiveBuyerShowing.ClearSelection();
        }

        private void dgvShowingCurrentShowings_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS /Marcus
        {
            dgvShowingCurrentShowings.ClearSelection();
        }
        #region MAINMENU
        private void menuItem7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Version 1.0 BrokerApplication\n\n\n"
                + "Denna mjukvara är skapad av\n\n"
                + "Marcus Jacobsson, marcus.jacobsson@student.lu.se\n"
                + "Christian Runnström, christian.runnstrom@student.lu.se\n"
                + "William Svedström, william.svedstrom@student.lu.se\n"
                + "August Ransnäs, august.ransnas@student.lu.se\n\n"
                + "Copyright © 2014 Mäklarfirman", 
                "Om",               
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );            
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Visningar hanteras under fliken Visning. Här kan användaren lägga till, ta bort eller uppdatera visningar. \n\n"
                + "Hur lägger jag till en visning för ett objekt?\n"
                + "För att lägga till en visning för ett objekt, gör följande:\n"
                + "(1) välj ett objekt i listan under \"välj objekt\".\n"
                + "(2) välj vilken spekulant i listan \"Välj spekulant\" som ska gå på visningen.\n"
                + "(3) välj vilket datum under \"välj datum\" som visningen kommer äga rum.\n"
                + "(4) tryck på knappen \"Lägg till\" för att registrera din visning.\n\n"
                + "Hur tar jag bort en visning för ett objekt?\n"
                + "För att ta bort en visning från ett objekt, gör följande:\n"
                + "(1) välj ditt objekts objektnummer i listan under \"Ta bort visningar\".\n"
                + "(2) välj alternativet \"Ta bort alla visningar\" till höger om listan.\n"
                + "(3) tryck på knappen \"Ta bort\" för att ta bort alla visningar för objektet.\n\n"
                + "Hur tar jag bort endast en spekulant från ett objekts visning?\n"
                + "För att ta bort endast en spekulant från en visning, gör följande:\n"
                + "(1) välj en spekulanten i listan under \"Ta bort visningar\".\n"
                + "(2) välj alternativet \"Ta bort spekulant från visning\" till höger om listan.\n"
                + "(3) tryck på knappen \"Ta bort\" för att ta bort spekulanten från objektets visning.\n\n"
                + "Hur uppdaterar jag ett objekts visningsdatum?\n"
                + "För att uppdatera en visnings visningsdatum, gör följande:\n"
                + "(1) välj ett objekt i listan under \"välj objekt\".\n"
                + "(2) välj vilken spekulant i listan \"Välj spekulant\" som ska uppdateras.\n"
                + "(3) välj ett nytt datum under \"välj datum\"\n"
                + "(4) tryck på knappen \"Uppdatera datum\".",
                "Visning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }      

        private void menuItem12_Click(object sender, EventArgs e)
        {
            //Visa/dölj en tab för Mäklare
        }
        #endregion MAINMENU

    }
}
