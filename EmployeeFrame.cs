using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace praktikfall
{
    public partial class frameMainMainframe : Form
    {
        Controller controller = new Controller();

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
                tpBrokerBrokerTabPage.Text = "";
            }
            Populate();
            DataTable dt = controller.SearchObjectByBrokerSsnr(brokerName);
            dgvStartYourObjects.DataSource = dt;
            DataTable dt2 = controller.SearchShowingsByBrokerSsnr(brokerName);
            dgvStartYourShowings.DataSource = dt2;
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
        }

        public void Populate()                                                            //Uppdatera ALLA DGVS (utom Christians)
        {
            DataTable dtAllObjects = controller.GetAllObjectsNr();
            dgvObjectAllObjects.DataSource = dtAllObjects;                                     //Objektfliken, alla objekt i databasen          
            MakeTbReadOnly();
            btnObjectSubmit.Enabled = false;
            dgvShowingAllObjects.DataSource = dtAllObjects;                                   //Visningsfliken, alla objekt i databasen            
            DataTable dtAllProspectiveBuyers = controller.GetAllProspectiveBuyers();
            dgvShowingAllBuyers.DataSource = dtAllProspectiveBuyers;                        //Visningsfliken, alla spekulanter i databasen 
            DataTable dtAllShowings = controller.GetShowings();
            dgvShowingCurrentShowings.DataSource = dtAllShowings;                            //Visningsfliken, alla nuvarande visningar i databasen
            DataTable dtAllBrokers = controller.GetAllBrokers();                             //Mäklarfliken (admin), visa alla mäklare
            dgvBrokerAllBrokers.DataSource = dtAllBrokers;
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
                    DataTable dt = controller.SearchObjectByString(searchString);
                    dgvObjectAllObjects.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion.\n" + ex);
            }

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att ladda från listan. \n" + ex);
            }
            
        }

        private void setSelectedObjectAndOwner(string objNr, DataGridViewRow row)
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
            int priceperkvm = int.Parse(price) / int.Parse(area);

            DataRow ownerInfo = this.controller.GetObjectOwner(tbObjectOwnerSsnr.Text).Rows[0];

            tbObjectOwnerName.Text = ownerInfo[1].ToString();
            tbObjectOwnerPhoneNr.Text = ownerInfo[2].ToString();
            tbObjectOwnerEmail.Text = ownerInfo[3].ToString();

        }

        private void btnShowingRegisterShowing_Click(object sender, EventArgs e)                //Lägg till visning
        {
            try
            {
                string objNr = lblShowingSelectedObject.Text;
                string showingDate = dtpShowingShowingDate.Text;
                string buyerSsnr = lblShowingSelectedBuyer.Text;
                bool showingExists = controller.ShowingExists(objNr, buyerSsnr);        //Kontrollerar om visningen redan finns

                if (showingExists)
                {
                    MessageBox.Show("Visningen finns redan. Vänligen kontrollera dina val. Använd Uppdatera datum-knappen för att uppdatera visningsdatum.");
                }

                else if (lblShowingSelectedBuyer.Text.Equals("selectedBuyer(invisible)"))
                {
                    MessageBox.Show("Vänligen välj spekulant i listan.");
                }
                else if (lblShowingSelectedObject.Text.Equals("selectedObject(invisible)"))
                {
                    MessageBox.Show("Vänligen välj objekt i listan.");
                }
                else if (!showingExists && !(lblShowingSelectedBuyer.Text.Equals("selectedBuyer(invisible)")) && !(lblShowingSelectedObject.Text.Equals("selectedObject(invisible)"))) //Lägg till
                {
                    int nrOfRows = controller.AddShowing(objNr, buyerSsnr, showingDate);
                    MessageBox.Show("Visning registrerad.");
                    Populate();
                    lblShowingSelectedBuyer.Text = "selectedBuyer(invisible)";
                    lblShowingSelectedObject.Text = "selectedObject(invisible)";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick inte att registrera visning. \n" + ex);
            }
        }

        private void tbObjectSearch_Click(object sender, EventArgs e)
        {
            tbObjectSearch.Text = "";
            tbObjectSearch.ForeColor = Color.Black;
        }

        private void dgvShowingAllObjects_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvShowingAllObjects.Rows[e.RowIndex];
                string selectedItem = row.Cells["Objektsnummer"].Value.ToString();
                lblShowingSelectedObject.Text = selectedItem;
            }
        }

        private void dgvShowingAllBuyers_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvShowingAllBuyers.Rows[e.RowIndex];
                string selectedItem = row.Cells["Personnummer"].Value.ToString();
                lblShowingSelectedBuyer.Text = selectedItem;
                tbShowingBuyerSsnr.Text = row.Cells["Personnummer"].Value.ToString();
                tbShowingBuyerName.Text = row.Cells["Namn"].Value.ToString();
                tbShowingBuyerTel.Text = row.Cells["Telefon"].Value.ToString();
                tbShowingBuyerEmail.Text = row.Cells["Email"].Value.ToString();

            }
        }

        private void EmployeeFrame_FormClosed(object sender, FormClosedEventArgs e)         //Avslutar applikationen när användaren stänger EmployeeFrame
        {
            Application.Exit();
        }

        private void tb_clickSearch(object sender, EventArgs e)
        {
            tbShowingSearch.Text = "";
            tbShowingSearch.ForeColor = Color.Black;

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

        private void btnShowingUpdate_Click(object sender, EventArgs e)          //Uppdatera visningsdatum
        {
            try
            {
                string objNr = lblShowingSelectedObject.Text;
                string showingDate = dtpShowingShowingDate.Text;
                string buyerSsnr = lblShowingSelectedBuyer.Text;

                if (lblShowingSelectedBuyer.Text.Equals("selectedBuyer(invisible)"))
                {
                    MessageBox.Show("Vänligen välj spekulant i listan.");
                }

                else if (lblShowingSelectedObject.Text.Equals("selectedObject(invisible)"))
                {
                    MessageBox.Show("Vänligen välj objekt i listan.");
                }

                else
                {
                    int nrOfRows = controller.UpdateShowing(objNr, buyerSsnr, showingDate);
                    MessageBox.Show("Visning uppdaterad. Nytt visningsdatum " + showingDate);
                    Populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Det gick inte att uppdatra visningsdatum. \n" + ex);
            }
        }

        private void dgvShowingCurrentShowings_CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvShowingCurrentShowings.Rows[e.RowIndex];
                    string objNr = row.Cells["Objektsnummer"].Value.ToString();
                    string buyerSsnr = row.Cells["Spekulant"].Value.ToString();
                    lblShowingSelectedBuyerDelete.Text = buyerSsnr;
                    lblShowingSelectedObjNrDelete.Text = objNr;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att ladda info från listan. \n" + ex);
            }
        }

        private void btnShowingSubmitDelete_Click(object sender, EventArgs e)                         //Ta bort visning/Ta bort spekulant från visning
        {
            try
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
                        MessageBox.Show("Visning borttagen.");
                        Populate();
                        lblShowingSelectedBuyerDelete.Text = "selectedForDelete(invisible)";
                        lblShowingSelectedObjNrDelete.Text = "selectedForDelete(invisible)";
                    }
                }
                else if (!rbShowingDeleteBuyer.Checked && !rbShowingDeleteShowing.Checked)               // Om inget val gjorts, ge feedback
                {
                    MessageBox.Show("Vänligen välj ett alternativ (ta bort alla visningar eller ta bort spekulant från visning) först.");
                }

                else if (rbShowingDeleteBuyer.Checked)                                                   //Om "ta bort spekulant" är valt
                {
                    if (lblShowingSelectedBuyerDelete.Text.Equals("selectedForDelete(invisible)"))
                    {
                        MessageBox.Show("Vänligen välj en spekulant i listan först.");
                    }
                    else
                    {
                        int nrOfRows = controller.DeleteBuyerFromShowing(buyerSsnr, objNr);
                        MessageBox.Show("Spekulant borttagen från visning.");
                        Populate();
                        lblShowingSelectedBuyerDelete.Text = "selectedForDelete(invisible)";
                        lblShowingSelectedObjNrDelete.Text = "selectedForDelete(invisible)";
                    }

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

        private void btnObjectSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string objNr = tbObjectObjectNr.Text;
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
                    bool objectExists = controller.ObjectExists(objNr);
                    bool ownerExists = controller.OwnerExists(ownerSsnr);

                    if (objectExists && ownerExists)
                    {
                        int nrOfRows = controller.UpdateObjectFlap(objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, objNr, name, phoneNr, email, ownerSsnr);
                        Populate();
                        MessageBox.Show("Objekt med objektnr " + objNr + " uppdaterat.");
                    }
                    else
                    {
                        MessageBox.Show("Kan ej uppdatera objektsnummer eller ägarens personnummer.");
                    }
                }

                else if (cbObjectRegister.Checked && !cbObjectUpdate.Checked && !cbObjectDeleteObject.Checked) //Register
                {
                    bool objectExists = controller.ObjectExists(objNr);
                    bool brokerExists = controller.BrokerExists(brokerSsnr);

                    if (!objectExists && brokerExists)
                    {
                        int nrOfRows = this.controller.AddObjectAndOwner(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr, ownerSsnr, phoneNr, email, name);
                        Populate();
                        ClearObjectTb();
                        pbObjectThumbnail.Image = null;
                        pbObjectsObjectPicture.Image = null;
                        MessageBox.Show("Objekt med objnr " + objNr + " och objektägare med personnummer " + ownerSsnr + " registrerad.");
                    }
                    else if (!brokerExists)
                    {
                        MessageBox.Show("Ingen sådan mäklare finns. Vänlig ange rätt mäklare.");
                    }
                    else if (objectExists)
                    {
                        MessageBox.Show("Det finns redan ett objekt med objektnummer " + objNr + " registrerat.");
                    }
                }

                else if (cbObjectDeleteObject.Checked && !cbObjectUpdate.Checked && !cbObjectRegister.Checked) //Delete
                {

                    bool objectExists = controller.ObjectExists(objNr);
                    bool ownerHasMoreObjects = this.controller.OwnerHasOtherObjects(ownerSsnr);
                    if (objectExists && !ownerHasMoreObjects)
                    {
                        int nrOfRows = this.controller.DeleteObjectOwner(ownerSsnr);
                        int i = this.controller.DeleteObject(objNr);
                        Populate();
                        ClearObjectTb();
                        MessageBox.Show("Objekt borttaget. Ägaren är även borttagen eftersom ägaren ej äger andra objekt.");
                    }
                    else if (objectExists && ownerHasMoreObjects)
                    {
                        int nrOfRows = this.controller.DeleteObject(objNr);
                        Populate();
                        ClearObjectTb();
                        MessageBox.Show("Objekt borttaget.");
                    }
                    else
                    {
                        MessageBox.Show("Inget sådant objekt finns.");
                    }
                    btnObjectSubmit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att utföra valet. \n" + ex);
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
                    DataTable dt = controller.SearchProBuyerByString(searchString);
                    dgvShowingAllBuyers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem i sökfunktion.\n" + ex);
            }
        }

        private void tbShowingSearch_Click(object sender, EventArgs e)
        {
            tbShowingSearch.Text = "";
            tbShowingSearch.ForeColor = Color.Black;
        }

        private void dgvShowingAllObjects_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS /Marcus
        {
            dgvShowingAllObjects.ClearSelection();
        }

        private void dgvShowingAllBuyers_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS /Marcus
        {
            dgvShowingAllBuyers.ClearSelection();
        }

        private void dgvShowingCurrentShowings_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS /Marcus
        {
            dgvShowingCurrentShowings.ClearSelection();
        }

        private void dgvBrokerAllBrokers_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS
        {
            dgvBrokerAllBrokers.ClearSelection();
        }

        private void dgvObjectAllObjects_DBC(object sender, DataGridViewBindingCompleteEventArgs e) //FÖR ATT INTE VÄLJA FÖRSTA RADEN NÄR DGV LADDAS
        {
            dgvObjectAllObjects.ClearSelection();
        }

        #region MAINMENU
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

        private void mmArkivQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mmHelpFAQShowing_Click(object sender, EventArgs e)
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



        private void mmShowAdmintools_Click(object sender, EventArgs e)
        {
            //Visa/dölj en tab för Mäklare
        }
        #endregion MAINMENU

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
                    int parsedValue;
                    if (!int.TryParse(tbBrokerBrokerSsnr.Text, out parsedValue))
                    {
                        MessageBox.Show("Personnummer får endast innehålla siffror.");
                    }

                    else if (tbBrokerBrokerName.Text.Equals(""))
                    {
                        MessageBox.Show("Du har ej angivit ett namn.");
                    }

                    else if (tbBrokerBrokerAdress.Text.Equals(""))
                    {
                        MessageBox.Show("Du har ej angivit en adress.");
                    }

                    else if (tbBrokerBrokerCity.Text.Equals(""))
                    {
                        MessageBox.Show("Du har ej angivit en stad.");
                    }

                    else if (tbBrokerBrokerPhoneNumber.Text.Equals(""))
                    {
                        MessageBox.Show("Du har ej angivit ett telefonnummer.");
                    }

                    else if (tbBrokerBrokerEmail.Text.Equals(""))
                    {
                        MessageBox.Show("Du har ej angivit en email.");
                    }

                    else if (tbBrokerBrokerPw.Text.Equals(""))
                    {
                        MessageBox.Show("Du har ej angivit ett lösenord.");
                    }

                    else
                    {
                        bool brokerExists = controller.BrokerExists(brokerSsnr);

                        if (brokerExists)
                        {
                            MessageBox.Show("Det finns redan en mäklare med personnummer " + brokerSsnr);
                        }

                        else
                        {
                            controller.AddBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
                            MessageBox.Show("Mäklare med personnummer " + brokerSsnr + " registrerad.");
                            Populate();
                        }

                    }

                }

                if (!cbBrokerRegister.Checked && cbBrokerDelete.Checked && !cbBrokerUpdate.Checked)     //Delete
                {

                    int nrOfRows = controller.DeleteBroker(brokerSsnr);

                    if (nrOfRows == 0)
                    {
                        MessageBox.Show("Ingen sådan mäklare finns registrerad. Vänligen försök igen.");
                    }
                    else
                    {
                        Populate();
                        tbBrokerBrokerAdress.Text = "";
                        tbBrokerBrokerCity.Text = "";
                        tbBrokerBrokerEmail.Text = "";
                        tbBrokerBrokerName.Text = "";
                        tbBrokerBrokerPhoneNumber.Text = "";
                        tbBrokerBrokerPw.Text = "";
                        tbBrokerBrokerSsnr.Text = "";
                        MessageBox.Show("Mäklare med personnummer " + brokerSsnr + " borttagen");
                    }
                }
                if (!cbBrokerRegister.Checked && !cbBrokerDelete.Checked && cbBrokerUpdate.Checked)             //Update
                {

                    int nrOfRows = controller.UpdateBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);

                    if (nrOfRows == 0)
                    {
                        MessageBox.Show("Kan inte uppdatera en mäklares personnummer.");
                    }
                    else
                    {
                        Populate();
                        MessageBox.Show("Mäklare med personnummer " + brokerSsnr + " uppdaterad.");
                    }
                }

                if (!cbBrokerRegister.Checked && !cbBrokerDelete.Checked && !cbBrokerUpdate.Checked)
                {
                    MessageBox.Show("Vänligen gör ett val först.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att utföra ditt val. \n" + ex);
            }
        }

        private void dgvBrokerAllBrokers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dgvBrokerAllBrokers.Rows[e.RowIndex];
                    string selectedItem = row.Cells["Personnummer"].Value.ToString();
                    lblShowingSelectedBuyer.Text = selectedItem;
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

        private void btnShowingSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string buyerSsnr = tbShowingBuyerSsnr.Text;
                string name = tbShowingBuyerName.Text;
                string phoneNr = tbShowingBuyerTel.Text;
                string email = tbShowingBuyerEmail.Text;

                if (cbShowingRegisterBuyer.Checked && !cbShowingDeleteBuyer.Checked && !cbShowingUpdateBuyer.Checked) //Register
                {

                    bool buyerExists = controller.ProspectiveBuyerExists(buyerSsnr);

                    if (buyerExists)
                    {
                        MessageBox.Show("Det finns redan en spekulant med personnummer " + buyerSsnr + " registrerad.");
                    }
                    else
                    {
                        int nrOfRows = controller.AddProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                        Populate();
                        MessageBox.Show("Spekulant med personnummer " + buyerSsnr + " har lagts till.");
                    }
                }
                if (!cbShowingRegisterBuyer.Checked && cbShowingDeleteBuyer.Checked && !cbShowingUpdateBuyer.Checked) // Delete
                {
                    int nrOfRows = controller.DeleteProspectiveBuyer(buyerSsnr);
                    if (nrOfRows == 0)
                    {
                        MessageBox.Show("Ingen sådan spekulant finns. Vänligen försök igen.");
                    }
                    else
                    {
                        Populate();
                        tbShowingBuyerEmail.Text = "";
                        tbShowingBuyerName.Text = "";
                        tbShowingBuyerSsnr.Text = "";
                        tbShowingBuyerTel.Text = "";
                        MessageBox.Show("Spekulant borttagen.");
                    }

                }
                if (!cbShowingRegisterBuyer.Checked && !cbShowingDeleteBuyer.Checked && cbShowingUpdateBuyer.Checked) //Update
                {

                    int nrOfRows = controller.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                    if (nrOfRows == 0)
                    {
                        MessageBox.Show("Det går inte att uppdatera en spekulants personnummer.");
                    }
                    else
                    {
                        Populate();
                        MessageBox.Show("Spekulant med personnummer " + buyerSsnr + " uppdaterad.");
                    }


                }
                if (!cbShowingRegisterBuyer.Checked && !cbShowingDeleteBuyer.Checked && !cbShowingUpdateBuyer.Checked)
                {
                    MessageBox.Show("Vänligen gör ett val först.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att utföra ditt val. \n" + ex);
            }
        }

        private void cbObjectRegister_CheckedChanged(object sender, EventArgs e)
        {

            if (cbObjectRegister.Checked)
            {
                btnObjectSubmit.Enabled = true;
                MakeTbEditable();
                lblObjectObjAddress.Text = "";
                ClearObjectTb();
            }
            else if (cbObjectRegister.Checked == false)
            {
                btnObjectSubmit.Enabled = false;
                MakeTbReadOnly();
            }

        }

        private void cbObjectUpdate_CheckedChanged(object sender, EventArgs e)
        {

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
            if (cbShowingRegisterBuyer.Checked)
            {
                tbShowingBuyerEmail.Text = "";
                tbShowingBuyerSsnr.Text = "";
                tbShowingBuyerTel.Text = "";
                tbShowingBuyerName.Text = "";
            }
        }

        private void cbBrokerRegister_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBrokerRegister.Checked)
            {
                tbBrokerBrokerSsnr.Text = "";
                tbBrokerBrokerPw.Text = "";
                tbBrokerBrokerPhoneNumber.Text = "";
                tbBrokerBrokerName.Text = "";
                tbBrokerBrokerEmail.Text = "";
                tbBrokerBrokerCity.Text = "";
                tbBrokerBrokerAdress.Text = "";
            }
        }

        private void btnObjectBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string imgLoc;
                OpenFileDialog fDialog = new OpenFileDialog();
                fDialog.Title = "Välj bild";
                fDialog.Filter = "JPG Files|*.jpg|GIF Files|*.gif|All Files (*.*)|*.*";       //Filtrera vilka bildformat som ska kunna laddas upp väljas                                                                       // (PB hanterar bitmap, metafile, icon, JPEG, GIF, or PNG file.)
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
                    DataTable dt = controller.SearchBrokerByString(searchString);
                    dgvBrokerAllBrokers.DataSource = dt;
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

        private void btnObjectSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                string objNr = tbObjectObjectNr.Text;
                byte[] img = null;
                string fp = lblObjectFilepath.Text;
                FileStream fs = new FileStream(fp, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                int nrOfRows = controller.addObjectImage(img, objNr);
                pbObjectsObjectPicture.Image = pbObjectThumbnail.Image;
                MessageBox.Show("En bild har registrerats till objekt med objektsnummer " + objNr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilden kunde inte laddas upp. \n" + ex);
            }
        }

        private void dgvObject(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }  

    }
}


