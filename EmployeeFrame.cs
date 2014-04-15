using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using praktikfall.WebServiceUpg2Reference;



namespace praktikfall
{
    public partial class frameMainMainframe : Form
    {
        Controller controller = new Controller();
        private int counter;

        public frameMainMainframe(string name, bool b)
        {
            InitializeComponent();
            string brokerName = name;
            lblStartEmpName.Text = brokerName;
            if (b == true)
            {
                bgBrokerAdministrateBroker.Visible = true;
            }

            else
            {
                bgBrokerAdministrateBroker.Visible = false;
                gbBrokerAllBrokers.Visible = false;

                tpBrokerBrokerTabPage.Text = "";
            }
            Populate();
            DataTable dt = this.controller.SearchObjectByBrokerSsnr(brokerName);
            dgvStartYourObjects.DataSource = dt;
            DataTable dt2 = this.controller.SearchShowingsByBrokerSsnr(brokerName);
            dgvStartYourShowings.DataSource = dt2;

            String[] methods = {"ObjectOwner","RealEstateBroker","RealEstateObject","Showings"};
            for (int i = 0; i < methods.Length; i++)
            {
                cbWebService.Items.Add(methods[i]);
            }
            
        }

        public void MakeTbReadOnly()
        {
            tbObjectObjectNr.ReadOnly = true;
            tbObjectAddress.ReadOnly = true;
            tbObjectCity.ReadOnly = true;
            tbObjectPrice.ReadOnly = true;
            tbObjectArea.ReadOnly = true;
            tbObjectNumberOfRooms.ReadOnly = true;
            tbObjectType.ReadOnly = true;
            rtbObjectObjectInfo.ReadOnly = true;
            tbObjectBrokerSsnr.ReadOnly = true;
            tbObjectOwnerSsnr.ReadOnly = true;
            tbObjectAddress.ReadOnly = true;
            tbObjectOwnerEmail.ReadOnly = true;
            tbObjectOwnerName.ReadOnly = true;
            tbObjectOwnerPhoneNr.ReadOnly = true;
            tbObjectPricePerKvm.ReadOnly = true;

            tbShowingBuyerSsnr.ReadOnly = true;
            tbShowingBuyerName.ReadOnly = true;
            tbShowingBuyerEmail.ReadOnly = true;
            tbShowingBuyerTel.ReadOnly = true;

            tbBrokerBrokerAdress.ReadOnly = true;
            tbBrokerBrokerCity.ReadOnly = true;
            tbBrokerBrokerEmail.ReadOnly = true;
            tbBrokerBrokerName.ReadOnly = true;
            tbBrokerBrokerPhoneNumber.ReadOnly = true;
            tbBrokerBrokerPw.ReadOnly = true;
            tbBrokerBrokerSsnr.ReadOnly = true;
        }

        public void MakeTbEditable()
        {
            tbObjectObjectNr.ReadOnly = false;
            tbObjectAddress.ReadOnly = false;
            tbObjectCity.ReadOnly = false;
            tbObjectPrice.ReadOnly = false;
            tbObjectArea.ReadOnly = false;
            tbObjectNumberOfRooms.ReadOnly = false;
            tbObjectType.ReadOnly = false;
            rtbObjectObjectInfo.ReadOnly = false;
            tbObjectBrokerSsnr.ReadOnly = false;
            tbObjectOwnerSsnr.ReadOnly = false;
            tbObjectAddress.ReadOnly = false;
            tbObjectOwnerEmail.ReadOnly = false;
            tbObjectOwnerName.ReadOnly = false;
            tbObjectOwnerPhoneNr.ReadOnly = false;
            tbObjectOwnerEmail.ReadOnly = false;
            tbObjectOwnerName.ReadOnly = false;
            tbObjectOwnerPhoneNr.ReadOnly = false;

            tbShowingBuyerSsnr.ReadOnly = false;
            tbShowingBuyerName.ReadOnly = false;
            tbShowingBuyerEmail.ReadOnly = false;
            tbShowingBuyerTel.ReadOnly = false;

            tbBrokerBrokerAdress.ReadOnly = false;
            tbBrokerBrokerCity.ReadOnly = false;
            tbBrokerBrokerEmail.ReadOnly = false;
            tbBrokerBrokerName.ReadOnly = false;
            tbBrokerBrokerPhoneNumber.ReadOnly = false;
            tbBrokerBrokerPw.ReadOnly = false;
            tbBrokerBrokerSsnr.ReadOnly = false;
        }

        public void ClearObjectTb()
        {
            tbObjectObjectNr.Text = "";
            tbObjectAddress.Text = "";
            tbObjectCity.Text = "";
            tbObjectPrice.Text = "";
            tbObjectArea.Text = "";
            tbObjectNumberOfRooms.Text = "";
            tbObjectType.Text = "";
            rtbObjectObjectInfo.Text = "";
            tbObjectBrokerSsnr.Text = "";
            tbObjectOwnerSsnr.Text = "";
            tbObjectAddress.Text = "";
            tbObjectOwnerEmail.Text = "";
            tbObjectOwnerName.Text = "";
            tbObjectOwnerPhoneNr.Text = "";
            tbObjectPricePerKvm.Text = "";
        }

        public void ClearShowingBuyerTb()
        {
            tbShowingBuyerEmail.Text = "";
            tbShowingBuyerName.Text = "";
            tbShowingBuyerSsnr.Text = "";
            tbShowingBuyerTel.Text = "";
        }

        public void ClearBrokerTb()
        {
            tbBrokerBrokerAdress.Text = "";
            tbBrokerBrokerCity.Text = "";
            tbBrokerBrokerEmail.Text = "";
            tbBrokerBrokerName.Text = "";
            tbBrokerBrokerPhoneNumber.Text = "";
            tbBrokerBrokerPw.Text = "";
            tbBrokerBrokerSsnr.Text = "";
        }
        public void Populate()
        {
            DataTable dtAllObjects = this.controller.GetAllObjectsNr();
            dgvObjectAllObjects.DataSource = dtAllObjects;

            btnObjectSubmit.Enabled = false;
            dgvShowingAllObjects.DataSource = dtAllObjects;
            DataTable dtAllProspectiveBuyers = this.controller.GetAllProspectiveBuyers();
            dgvShowingAllBuyers.DataSource = dtAllProspectiveBuyers;
            DataTable dtAllShowings = this.controller.GetShowings();
            dgvShowingCurrentShowings.DataSource = dtAllShowings;
            DataTable dtAllBrokers = this.controller.GetAllBrokers();
            dgvBrokerAllBrokers.DataSource = dtAllBrokers;
            MakeTbReadOnly();
        }

        private void btnObjectSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbObjectSearch.Text.Equals("") || tbObjectSearch.Text.Equals("Sökord"))
                {
                    tbObjectSearch.Text = "";
                    tbObjectSearch.ForeColor = Color.LightSlateGray;
                    tbObjectSearch.Text = "Sökord";
                    Populate();
                }

                else
                {
                    string searchString = tbObjectSearch.Text;
                    DataTable dt = this.controller.SearchObjectByString(searchString);
                    dgvObjectAllObjects.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion.\n" + ex);
            }

        }


        private void btnBrokerSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbBrokerSearch.Text.Equals("") || tbBrokerSearch.Text.Equals("Sökord"))
                {
                    tbObjectSearch.Text = "";
                    tbObjectSearch.ForeColor = Color.LightSlateGray;
                    tbObjectSearch.Text = "Sökord";
                    Populate();
                }

                else
                {
                    string searchString = tbBrokerSearch.Text;
                    DataTable dt = this.controller.SearchBrokerByString(searchString);
                    dgvBrokerAllBrokers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion.\n" + ex);
            }

        }

        private void btnShowingSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbShowingSearch.Text.Equals("") || tbShowingSearch.Text.Equals("Sökord"))
                {
                    tbShowingSearch.Text = "";
                    tbShowingSearch.ForeColor = Color.LightSlateGray;
                    tbShowingSearch.Text = "Sökord";
                    Populate();
                }

                else
                {
                    string searchString = tbShowingSearch.Text;
                    DataTable dt = this.controller.SearchProBuyerByString(searchString);
                    dgvShowingAllBuyers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion.\n" + ex);
            }
        }

        private void tbBrokerSearch_Click(object sender, EventArgs e)
        {
            tbBrokerSearch.Text = "";
            tbBrokerSearch.ForeColor = Color.Black;
        }

        private void tbObjectSearch_Click(object sender, EventArgs e)
        {
            tbObjectSearch.Text = "";
            tbObjectSearch.ForeColor = Color.Black;
        }

        private void tb_clickSearch(object sender, EventArgs e)
        {
            tbShowingSearch.Text = "";
            tbShowingSearch.ForeColor = Color.Black;
        }

        private void tbShowingSearch_Click(object sender, EventArgs e)
        {
            tbShowingSearch.Text = "";
            tbShowingSearch.ForeColor = Color.Black;
        }

        private void dgvObjectAllObjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvObjectAllObjects.Rows[e.RowIndex];
                    string objNr = row.Cells["Objektsnummer"].Value.ToString();
                    this.setSelectedObjectAndOwner(objNr, row);
                    pbObjectThumbnail.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att ladda från listan. \n" + ex);
            }

        }

        private void dgvShowingAllBuyers_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvShowingAllBuyers.Rows[e.RowIndex];
                    tbShowingBuyerSsnr.Text = row.Cells["Personnummer"].Value.ToString();
                    tbShowingBuyerName.Text = row.Cells["Namn"].Value.ToString();
                    tbShowingBuyerTel.Text = row.Cells["Telefon"].Value.ToString();
                    tbShowingBuyerEmail.Text = row.Cells["Email"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att ladda från listan.\n" + ex);
            }
        }

        private void dgvBrokerAllBrokers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvBrokerAllBrokers.Rows[e.RowIndex];
                    tbBrokerBrokerSsnr.Text = row.Cells["Personnummer"].Value.ToString();
                    tbBrokerBrokerName.Text = row.Cells["Namn"].Value.ToString();
                    tbBrokerBrokerAdress.Text = row.Cells["Adress"].Value.ToString();
                    tbBrokerBrokerCity.Text = row.Cells["Stad"].Value.ToString();
                    tbBrokerBrokerPhoneNumber.Text = row.Cells["Telefon"].Value.ToString();
                    tbBrokerBrokerEmail.Text = row.Cells["Email"].Value.ToString();
                    tbBrokerBrokerPw.Text = row.Cells["Lösenord"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att ladda från listan. \n" + ex);
            }
        }

        private void setSelectedObjectAndOwner(string objNr, DataGridViewRow row)
        {
            try
            {
                DataRow objectInfo = this.controller.GetObject(objNr).Rows[0];
                tbObjectObjectNr.Text = objectInfo[0].ToString();
                tbObjectAddress.Text = objectInfo[1].ToString();
                tbObjectCity.Text = objectInfo[2].ToString();
                tbObjectPrice.Text = objectInfo[3].ToString();
                lblObjectObjAddress.Text = objectInfo[1].ToString();
                tbObjectArea.Text = objectInfo[4].ToString();
                tbObjectNumberOfRooms.Text = objectInfo[5].ToString();
                tbObjectType.Text = objectInfo[6].ToString();
                rtbObjectObjectInfo.Text = objectInfo[7].ToString();
                tbObjectBrokerSsnr.Text = objectInfo[8].ToString();
                tbObjectOwnerSsnr.Text = objectInfo[9].ToString();
                byte[] img = (byte[])objectInfo[10];

                if (img != null && img.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(img);
                    pbObjectsObjectPicture.Image = Image.FromStream(ms);
                }

                else
                {
                    pbObjectsObjectPicture.Image = null;
                    pbObjectThumbnail.Image = null;
                }

                string price = objectInfo[3].ToString();
                string area = objectInfo[4].ToString();

                double pricePerKvm = this.controller.CalculateObjectPricePerKvm(price, area);
                tbObjectPricePerKvm.Text = pricePerKvm.ToString();

                DataRow ownerInfo = this.controller.GetObjectOwner(tbObjectOwnerSsnr.Text).Rows[0];
                tbObjectOwnerName.Text = ownerInfo[1].ToString();
                tbObjectOwnerPhoneNr.Text = ownerInfo[2].ToString();
                tbObjectOwnerEmail.Text = ownerInfo[3].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick inte att ladda från listan.\n" + ex);
            }
        }

        private void btnShowMap_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbObjectAddress.Text.Equals("") || tbObjectCity.Text.Equals(""))
                {
                    MessageBox.Show("Du har ej valt ett objekt.");
                }
                else
                {
                    MapFrame mf = new MapFrame(tbObjectAddress.Text, tbObjectCity.Text);
                    mf.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick ej att ladda kartfunktionen. \n" + ex);
            }
        }

        private void btnShowingRegisterShowing_Click(object sender, EventArgs e)                //Add showing
        {
            try
            {
                if (!(dgvShowingAllBuyers.SelectedCells.Count == 0) && !(dgvShowingAllObjects.SelectedCells.Count == 0))
                {
                    string showingDate = dtpShowingShowingDate.Text;
                    string buyerSsnr = dgvShowingAllBuyers.SelectedCells[0].Value.ToString();
                    string objNr = dgvShowingAllObjects.SelectedCells[0].Value.ToString();

                    string feedback = this.controller.AddShowing(objNr, buyerSsnr, showingDate);
                    Populate();
                    MessageBox.Show(feedback);
                }
                else
                {
                    MessageBox.Show("Kontrollera om du valt en spekulant och ett objekt i listan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick inte att registrera visning. \n" + ex);
            }
        }

        private void btnShowingUpdate_Click(object sender, EventArgs e)          //Update showing date
        {
            try
            {
                if (!(dgvShowingAllBuyers.SelectedCells.Count == 0) && !(dgvShowingAllObjects.SelectedCells.Count == 0))
                {
                    string objNr = dgvShowingAllObjects.SelectedCells[0].Value.ToString();
                    string buyerSsnr = dgvShowingAllBuyers.SelectedCells[0].Value.ToString();
                    string showingDate = dtpShowingShowingDate.Text;

                    string feedback = this.controller.UpdateShowing(objNr, buyerSsnr, showingDate);
                    MessageBox.Show(feedback);
                    Populate();
                }
                else
                {
                    MessageBox.Show("Kontrollera om du valt en spekulant och ett objekt i listan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick inte att uppdatra visningsdatum. \n" + ex);
            }
        }

        private void btnShowingSubmitDelete_Click(object sender, EventArgs e)   //Delete showing/delete buyer from showing
        {
            try
            {
                if (rbShowingDeleteShowing.Checked)     //Delete an objects showing
                {
                    if (dgvShowingCurrentShowings.SelectedCells.Count == 0)
                    {
                        MessageBox.Show("Vänligen välj ett objekt i listan först.");
                    }
                    else
                    {
                        string objNr = dgvShowingCurrentShowings.SelectedCells[0].Value.ToString();
                        this.controller.DeleteShowing(objNr);
                        Populate();
                        MessageBox.Show("Visning borttagen.");
                    }
                }

                else if (rbShowingDeleteBuyer.Checked)     //Delete a buyer from showing
                {
                    if (dgvShowingCurrentShowings.SelectedCells.Count == 0)
                    {
                        MessageBox.Show("Vänligen välj en spekulant i listan först.");
                    }
                    else
                    {
                        string objNr = dgvShowingCurrentShowings.SelectedCells[0].Value.ToString();
                        string buyerSsnr = dgvShowingCurrentShowings.SelectedCells[1].Value.ToString();

                        this.controller.DeleteBuyerFromShowing(buyerSsnr, objNr);
                        Populate();
                        MessageBox.Show("Spekulant borttagen från visning.");
                    }
                }
                else if ((dgvShowingCurrentShowings.SelectedCells.Count == 0) && (dgvShowingCurrentShowings.SelectedCells.Count == 0))   //No selection made
                {
                    MessageBox.Show("Vänligen gör ett val först.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att utföra valet. \n" + ex);
            }

        }

        private void cbObjUpdateClick(object sender, EventArgs e)
        {
            dgvObjectAllObjects_CellClick(dgvObjectAllObjects, new DataGridViewCellEventArgs(0, 0));
        }

        private void btnObjectSubmit_Click(object sender, EventArgs e)              //Object and object owner
        {
            try
            {
                string objNr = tbObjectObjectNr.Text.ToString();
                string objAdress = tbObjectAddress.Text;
                string objCity = tbObjectCity.Text;
                string objPrice = tbObjectPrice.Text;
                string objArea = tbObjectArea.Text;
                string objRooms = tbObjectNumberOfRooms.Text;
                string objUnitType = tbObjectType.Text;
                string objInfo = rtbObjectObjectInfo.Text;
                string brokerSsnr = tbObjectBrokerSsnr.Text;
                string ownerSsnr = tbObjectOwnerSsnr.Text;
                string phoneNr = tbObjectOwnerPhoneNr.Text;
                string email = tbObjectOwnerEmail.Text;
                string name = tbObjectOwnerName.Text;

                if (cbObjectUpdate.Checked && !cbObjectRegister.Checked && !cbObjectDeleteObject.Checked) //Update
                {
                    string feedback = this.controller.UpdateObjectFlap(objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, objNr, name, phoneNr, email, ownerSsnr);
                    cbObjectUpdate.Checked = false;
                    MessageBox.Show(feedback);
                }
                else if (cbObjectRegister.Checked && !cbObjectUpdate.Checked && !cbObjectDeleteObject.Checked) //Register
                {

                    string feedback = this.controller.AddObjectAndOwner(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr, ownerSsnr, phoneNr, email, name);
                    pbObjectThumbnail.Image = null;
                    pbObjectsObjectPicture.Image = null;
                    cbObjectRegister.Checked = false;
                    MessageBox.Show(feedback);
                }
                else if (cbObjectDeleteObject.Checked && !cbObjectUpdate.Checked && !cbObjectRegister.Checked) //Delete
                {
                    string feedback = this.controller.DeleteObject(objNr, ownerSsnr);
                    cbObjectDeleteObject.Checked = false;
                    ClearObjectTb();
                    pbObjectsObjectPicture.Image = null;
                    MessageBox.Show(feedback);
                }
                Populate();
                MakeTbEditable();
                btnObjectSubmit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uppgifterna är inte korrekt ifyllda.\n" + ex);
            }
        }

        //Methods to clear DGV selection on data binding complete
        private void dgvShowingAllObjects_DBC(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvShowingAllObjects.ClearSelection();
        }

        private void dgvShowingAllBuyers_DBC(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvShowingAllBuyers.ClearSelection();
        }

        private void dgvShowingCurrentShowings_DBC(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvShowingCurrentShowings.ClearSelection();
        }

        private void dgvBrokerAllBrokers_DBC(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvBrokerAllBrokers.ClearSelection();
        }

        private void dgvObjectAllObjects_DBC(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvObjectAllObjects.ClearSelection();
        }

        private void btnBrokerSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string brokerSsnr = tbBrokerBrokerSsnr.Text;
                string pw = tbBrokerBrokerPw.Text;
                string name = tbBrokerBrokerName.Text;
                string phoneNr = tbBrokerBrokerPhoneNumber.Text;
                string email = tbBrokerBrokerEmail.Text;
                string city = tbBrokerBrokerCity.Text;
                string brokerAddress = tbBrokerBrokerAdress.Text;

                if (cbBrokerRegister.Checked && !cbBrokerDelete.Checked && !cbBrokerUpdate.Checked) //Register
                {
                    string feedback = this.controller.AddBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
                    Populate();
                    cbBrokerRegister.Checked = false;
                    MessageBox.Show(feedback);
                }

                else if (!cbBrokerRegister.Checked && cbBrokerDelete.Checked && !cbBrokerUpdate.Checked)     //Delete
                {
                    string feedback = this.controller.DeleteBroker(brokerSsnr);
                    Populate();
                    ClearBrokerTb();
                    cbBrokerDelete.Checked = false;
                    MessageBox.Show(feedback);
                }

                else if (!cbBrokerRegister.Checked && !cbBrokerDelete.Checked && cbBrokerUpdate.Checked)             //Update
                {
                    string feedback = this.controller.UpdateBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
                    Populate();
                    cbBrokerUpdate.Checked = false;
                    MessageBox.Show(feedback);
                }

                else if (!cbBrokerRegister.Checked && !cbBrokerDelete.Checked && !cbBrokerUpdate.Checked)
                {
                    MessageBox.Show("Vänligen gör ett val först.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att utföra valet. \n" + ex);
            }
            Populate();
            MakeTbEditable();
            btnBrokerSubmit.Enabled = true;
        }

        private void btnShowingSubmit_Click(object sender, EventArgs e) //Prospective buyer
        {
            try
            {
                string buyerSsnr = tbShowingBuyerSsnr.Text;
                string name = tbShowingBuyerName.Text;
                string phoneNr = tbShowingBuyerTel.Text;
                string email = tbShowingBuyerEmail.Text;

                if (cbShowingRegisterBuyer.Checked && !cbShowingDeleteBuyer.Checked && !cbShowingUpdateBuyer.Checked) //Register
                {
                    string feedback = this.controller.AddProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                    Populate();
                    cbShowingRegisterBuyer.Checked = false;
                    MessageBox.Show(feedback);
                }

                else if (!cbShowingRegisterBuyer.Checked && cbShowingDeleteBuyer.Checked && !cbShowingUpdateBuyer.Checked) // Delete
                {
                    string feedback = this.controller.DeleteProspectiveBuyer(buyerSsnr);
                    Populate();
                    ClearShowingBuyerTb();
                    cbShowingDeleteBuyer.Checked = false;
                    MessageBox.Show(feedback);
                }
                else if (!cbShowingRegisterBuyer.Checked && !cbShowingDeleteBuyer.Checked && cbShowingUpdateBuyer.Checked) //Update
                {
                    string feedback = this.controller.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                    Populate();
                    cbShowingUpdateBuyer.Checked = false;
                    MessageBox.Show(feedback);
                }
                else if (!cbShowingRegisterBuyer.Checked && !cbShowingDeleteBuyer.Checked && !cbShowingUpdateBuyer.Checked)
                {
                    MessageBox.Show("Vänligen gör ett val först.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att utföra valet. \n" + ex);
            }
            Populate();
            btnShowingSubmit.Enabled = true;
            MakeTbEditable();
        }

        private void OnCheckChanged(object sender, EventArgs e)
        {

            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                counter++;
            }
            else
            {
                counter--;
            }
            if (counter == 2)
            {
                cb.Checked = false;
                MessageBox.Show("Välj endast ett alternativ.");
            }

        }
        private void cbObjectRegister_CheckedChanged(object sender, EventArgs e)
        {

            OnCheckChanged(sender, e);
            if (cbObjectRegister.Checked)
            {
                btnObjectSubmit.Enabled = true;
                MakeTbEditable();
                lblObjectObjAddress.Text = "";
                ClearObjectTb();
                pbObjectsObjectPicture.Image = null;
            }
            else if (cbObjectRegister.Checked == false)
            {
                btnObjectSubmit.Enabled = false;
                MakeTbReadOnly();
            }
        }

        private void cbObjectUpdate_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);
            if (cbObjectUpdate.Checked)
            {
                btnObjectSubmit.Enabled = true;
                MakeTbEditable();
            }
            else if (cbObjectUpdate.Checked == false)
            {
                btnObjectSubmit.Enabled = false;
                MakeTbReadOnly();
            }
        }

        private void cbObjectDeleteObject_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);
            btnObjectSubmit.Enabled = true;
            if (cbObjectDeleteObject.Checked)
            {
                btnObjectSubmit.Enabled = true;
                MakeTbReadOnly();

            }
            else if (cbObjectDeleteObject.Checked == false)
            {
                btnObjectSubmit.Enabled = false;
                MakeTbReadOnly();
            }
        }

        private void cbShowingRegister_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);
            if (cbShowingRegisterBuyer.Checked)
            {
                ClearShowingBuyerTb();
                MakeTbEditable();
                btnShowingSubmit.Enabled = true;
            }
            else if (cbShowingRegisterBuyer.Checked == false)
            {

                MakeTbReadOnly();
                btnShowingSubmit.Enabled = false;
            }
        }

        private void cbShowingUpdate_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);
            if (cbShowingUpdateBuyer.Checked)
            {
                MakeTbEditable();
                btnShowingSubmit.Enabled = true;
            }
            else if (cbShowingUpdateBuyer.Checked == false)
            {
                MakeTbReadOnly();
                btnShowingSubmit.Enabled = false;
            }
        }
        private void cbShowingDelete_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);
            if (cbShowingDeleteBuyer.Checked)
            {
                MakeTbReadOnly();
                btnShowingSubmit.Enabled = true;
            }
            else if (cbShowingDeleteBuyer.Checked == false)
            {
                btnShowingSubmit.Enabled = false;
            }
        }


        private void cbBrokerRegister_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);
            if (cbBrokerRegister.Checked)
            {
                ClearBrokerTb();
                MakeTbEditable();
                btnBrokerSubmit.Enabled = true;
            }
            else
            {
                btnBrokerSubmit.Enabled = false;
                MakeTbReadOnly();
            }
        }

        private void cbBrokerUpdate_CheckedChange(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);

            if (cbBrokerUpdate.Checked)
            {
                MakeTbEditable();
                btnBrokerSubmit.Enabled = true;
            }
            else
            {
                MakeTbReadOnly();
                btnBrokerSubmit.Enabled = false;
            }
        }

        private void cbBrokerDelete_CheckedChange(object sender, EventArgs e)
        {
            OnCheckChanged(sender, e);

            if (cbBrokerDelete.Checked)
            {
                btnBrokerSubmit.Enabled = true;
            }
            else
            {
                btnBrokerSubmit.Enabled = false;
            }

        }

        private void btnObjectBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string imgLoc;
                OpenFileDialog fDialog = new OpenFileDialog();
                fDialog.Title = "Välj bild";
                fDialog.Filter = "JPG Files|*.jpg|GIF Files|*.gif|All Files (*.*)|*.*";  //File format filtering                                                                   
                fDialog.InitialDirectory = @"C:\";
                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = fDialog.FileName.ToString();
                    Image image = Image.FromFile(fDialog.FileName.ToString());
                    pbObjectThumbnail.Image = image;
                    tbObjectImageURL.Text = imgLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick inte att välja en bild. \n" + ex);

            }
        }

        private void btnObjectSaveImage_Click(object sender, EventArgs e) //Save image 
        {
            try
            {
                if (pbObjectThumbnail.Image == null)
                {
                    MessageBox.Show("Vänlig välj en bild att ladda upp först.");
                }
                else
                {
                    string objNr = tbObjectObjectNr.Text;
                    byte[] img = null;
                    string fp = tbObjectImageURL.Text;
                    FileStream fs = new FileStream(fp, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);
                    this.controller.addObjectImage(img, objNr);
                    pbObjectsObjectPicture.Image = pbObjectThumbnail.Image;
                    tbObjectImageURL.Text = "";
                    pbObjectThumbnail.Image = null;
                    MessageBox.Show("En bild har registrerats till objekt med objektsnummer " + objNr);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilden kunde inte laddas upp. \n" + ex);
            }
        }

        private void EmployeeFrame_FormClosed(object sender, FormClosedEventArgs e)         //Exit application
        {
            Application.Exit();
        }

        private void mmHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Version 1.0 BrokerApplication\n\n\n"
                + "Denna mjukvara är utvecklad av\n\n"
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

        private void mmArkivQuit_Click(object sender, EventArgs e) //Menuflap, arkiv quit
        {
            Application.Exit();
        }

        private void btnWSbrowse_Click(object sender, EventArgs e)
        {
            string filepath;

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Välj fil";
            fDialog.InitialDirectory = @"C:\";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = fDialog.FileName.ToString();
                tbWSfilepath.Text = filepath;
            }
        }

        private void btnWSsubmit_Click(object sender, EventArgs e)
        {
            string input = tbWSfilepath.Text;
            try
            {
                rtbWSoutput.Text = this.controller.GetFileContent(input);
            }
            catch (Exception )
            {
                MessageBox.Show("Kunde inte utföra valet.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbWebService.SelectedIndex)
            { 
                    
                case 0:
                    listViewWeb.Clear();
                    List<ObjectOwner> objectOwners = controller.GetObjectOwners();
                    listViewWeb.Columns.Add("Personnummer");
                    listViewWeb.Columns.Add("Namn");
                    listViewWeb.Columns.Add("Telefonnummer");
                    listViewWeb.Columns.Add("Email");
                    
                    
                    foreach (ObjectOwner o in objectOwners)
                    {
                        
                        ListViewItem lvi = new ListViewItem(o.OwnerSsnr);
                        lvi.SubItems.Add(o.Name);
                        lvi.SubItems.Add(o.PhoneNr);
                        lvi.SubItems.Add(o.Email);
                        listViewWeb.Items.Add(lvi);

                    }
                    break;
                case 1:
                    
                    listViewWeb.Clear();
                    List<RealEstateBroker> realEstateBrokers = controller.GetRealEstateBrokers();
                    listViewWeb.Columns.Add("Personnummer");
                    listViewWeb.Columns.Add("Namn");
                    listViewWeb.Columns.Add("Adress");
                    listViewWeb.Columns.Add("Stad");
                    listViewWeb.Columns.Add("Telefonnummer");
                    listViewWeb.Columns.Add("Email");


                    foreach (RealEstateBroker rb in realEstateBrokers)
                    {
                        
                        ListViewItem lvi = new ListViewItem(rb.BrokerSsnr);
                        lvi.SubItems.Add(rb.Name);
                        lvi.SubItems.Add(rb.BrokerAddress);
                        lvi.SubItems.Add(rb.City);
                        lvi.SubItems.Add(rb.PhoneNr);
                        lvi.SubItems.Add(rb.Email);
                        listViewWeb.Items.Add(lvi);

                    }
                    break;

                case 2:

                    break;
                case 3:

                    break;
            }
        }
    }
}


