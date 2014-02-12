using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace praktikfall
{
    class Controller
    {
        DataAccessLayer dal = new DataAccessLayer();
        #region OBJEKT
        //Lägg till OBJEKT
        public int AddObject(string objNr, string objAdress, string objCity, int objPrice, double objArea, string objRooms, string objUnitType,string objInfo, string brokerSsnr)
        {
            int nrOfRows = dal.AddObject(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr);
            return nrOfRows;
        }
        //Ta bort OBJEKT
        public int DeleteObject(string objNr)
        {
            int nrOfRows = dal.DeleteObject(objNr);
            return nrOfRows;
        }
        //Uppdatera OBJEKT
        public int UpdateObject(string objNr, string objAdress, string objCity, int objPrice, double objArea, string objRooms, string objUnitType, string objInfo, string brokerSsnr)
        {
            int nrOfRows = dal.UpdateObject(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr);
            return nrOfRows;
        }

        // Uppdaterar allt i objekt fliken !
        public int UpdateObjectFlap(string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo, string objNr, string name, string phoneNr, string email, string ownerSsnr)
        {
            int nrOfRows = dal.UpdateObjectFlap(objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, objNr, name, phoneNr, email, ownerSsnr);
            return nrOfRows;
        }

        //Registrera Objekt och ägare
        public int AddObjectAndOwner(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
            string brokerSsnr, string ownerSsnr, string phoneNr, string email, string name)
        {
            int nrOfRows = dal.AddObjectAndOwner(objNr, objAdress, objCity,
            objPrice, objArea, objRooms, objUnitType, objInfo,
            brokerSsnr, ownerSsnr, phoneNr, email, name);
            return nrOfRows;
        }

        //Sökknapp i Objekt för att visa ett objekt med en viss söksträng
        public DataTable SearchObjectByString(string searchString)
        {
            DataTable dt = dal.SearchObjectByString(searchString);
            return dt;            
        }
        //Sökknapp i Objekt för att visa ett objekt med en viss söksträng
        public DataTable SearchProBuyerByString(string searchString)
        {
            DataTable dt = dal.SearchProBuyerByString(searchString);
            return dt;
        }
        //Söka OBJEKT
        public DataTable GetObject(string objNr)
        {
            DataTable dt = dal.GetObject(objNr);
            return dt;
        }
        //Hämta alla OBJEKT
        public DataTable GetAllObjectsNr()
        {
            DataTable dt = dal.GetAllObjects();
            return dt;
        }

        ////Hämta alla objekt med angivet Brokernummer
        public DataTable SearchObjectByBrokerSsnr(string searchString)
        {
            DataTable dt = dal.SearchObjectByBrokerSsnr(searchString);
            return dt;
        }

        ////Hämta alla visningar med angivet Brokernummer
        public DataTable SearchShowingsByBrokerSsnr(string searchString)
        {
            DataTable dt = dal.SearchShowingsByBrokerSsnr(searchString);
            return dt;
        }
        //Kontrollera om OBJEKT Finns
        public bool ObjectExists(string objNr)
        {
            bool objectExists = dal.ObjectExists(objNr);
            return objectExists;
        }
        #endregion OBJEKT
        #region MÄKLARE
        //Lägg till MÄKLARE
        public int AddBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            int nrOfRows = dal.AddBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
            return nrOfRows;
        }
        //Ta bort MÄKLARE
        public int DeleteBroker(string brokerSsnr)
        {
            int nrOfRows = dal.DeleteBroker(brokerSsnr);
            return nrOfRows;
        }
        //Uppdatea MÄKLARE
        public int UpdateBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            int nrOfRows = dal.UpdateBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
            return nrOfRows;
        }
        //Söka MÄKLARE
        public DataTable GetBroker(string brokerSsnr)
        {
            DataTable dt = dal.GetBroker(brokerSsnr);
            return dt;
        }
        //Hämta ALLA MÄKLARE
        public DataTable GetAllBrokers()
        {
            DataTable dt = dal.GetAllBrokers();
            return dt;
        }

        //Kontrollera om mäklare finns
        public bool BrokerExists(string brokerSsnr)
        {
            bool brokerExists = dal.BrokerExists(brokerSsnr);
            return brokerExists;
        }

        //Sökknapp i Objekt för att visa ett objekt med en viss söksträng
        public DataTable SearchBrokerByString(string searchString)
        {
            DataTable dt = dal.SearchBrokerByString(searchString);
            return dt;
        }
        #endregion MÄKLARE
        #region SPEKULANT
        //lägga till spekulant
        public int AddProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            int nrOfRows = dal.AddProspectiveBuyer(buyerSsnr, name, phoneNr, email);
            return nrOfRows;
        }
        //Ta bort spekulant
        public int DeleteProspectiveBuyer(string buyerSsnr)
        {
            int nrOfRows = dal.DeleteProspectiveBuyer(buyerSsnr);
            return nrOfRows;
        }
        //Uppdatera spekulant
        public int UpdateProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            int nrOfRows = dal.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);
            return nrOfRows;
        }
        //Söka spekulant
        public DataTable GetProspectiveBuyer(string buyerSsnr)
        {
            DataTable dt = dal.GetProspectiveBuyer(buyerSsnr);
            return dt;
        }

        //Hämta alla spekulanter
        public DataTable GetAllProspectiveBuyers()
        {
            DataTable dt = dal.GetAllProspectiveBuyers();
            return dt;
        }

        //Kontrollera om spekulant finns
        public bool ProspectiveBuyerExists(string Ssnr)
        {
            bool showingExists = dal.ProspectiveBuyerExists(Ssnr);
            return showingExists;
        }
        #endregion SPEKULANT
        #region OBJEKTÄGARE
        //Lägg till Ägare   ___OBS___ Lägger endast till en ägare i systemet. Kopplingen mellan Object och Owner görs ej här!
        public int AddObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            int nrOfRows = dal.AddObjectOwner(ownerSsnr, phoneNr, email);
            return nrOfRows;
        }
        //Ta bort Ägare
        public int DeleteObjectOwner(string ownerSsnr)
        {
            int nrOfRows = dal.DeleteObjectOwner(ownerSsnr);
            return nrOfRows;
        }
        //Uppdatera Ägare
        public int UpdateObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            int nrOfRows = dal.UpdateObjectOwner(ownerSsnr, phoneNr, email);
            return nrOfRows;
        }
        //Söka Ägare -- BEHÖVS EVENTUELLT INTE
        public DataTable GetObjectOwner(string ownerSsnr)
        {
            DataTable dt = dal.GetObjectOwner(ownerSsnr);
            return dt;
        }
        //Hämta alla ägare
        public DataTable GetAllObjectOwners()
        {
            DataTable dt = dal.GetAllObjectOwners();
            return dt;
        }
        //Kontrollera om ÄGARE finns
        public bool OwnerExists(string ownerSsnr)
        {
            bool ownerExists = dal.OwnerExists(ownerSsnr);
            return ownerExists;
        }
        #endregion OBJEKTÄGARE
        #region HASOWNER
        //Hämta ÄGARE(hasowner)
        public DataTable GetObjOwner(string objNr)
        {
            DataTable dt = dal.GetSelectedObjectsOwner(objNr);
            return dt;
        }
        //Registrera givet OBJEKT med given ÄGARE     HÄR registreras ett objekts ägare
        public int AddObjectOwner(string objNr, string ownerSsnr)
        {
            int nrOfRows = dal.AddObjectOwner(objNr, ownerSsnr);
            return nrOfRows;
        }
        //Hämta allt från hasowner tabellen
        public DataTable GetAllHasOwner()
        {
            DataTable dt = dal.GetAllHasOwner();
            return dt;
        }
        #endregion HASOWNER
        #region SHOWING
        //Registrera VISNING
        public int AddShowing (string objNr, string buyerSsnr, string showingDate)
        {
            int nrOfRows = dal.AddShowing(objNr, buyerSsnr, showingDate);
            return nrOfRows;
        }
        //Uppdatera visningsdatum för VISNING
        public int UpdateShowing(string objNr, string buyerSsnr, string showingDate)
        {
            int nrOfRows = dal.UpdateShowing(objNr, buyerSsnr, showingDate);
            return nrOfRows;
        }
        //Hämta alla VISNINGAR
        public DataTable GetShowings()
        {
            DataTable dt = dal.GetShowings();
            return dt;
        }
        //Ta bort spekulant från VISNING
        public int DeleteBuyerFromShowing(string buyerSsnr, string objNr)
        {
            int nrOfRows = dal.DeleteBuyerFromShowing(buyerSsnr, objNr);
            return nrOfRows;
        }
        //Ta bort VISNING
        public int DeleteShowing(string objNr)
        {
            int nrOfRows = dal.DeleteShowing(objNr);
            return nrOfRows;
        }
        //Kontrollera om VISNING finns
        public bool ShowingExists(string objNr, string buyerSsnr)
        {
            bool showingExists = dal.ShowingExists(objNr, buyerSsnr);
            return showingExists;
        }
        #endregion SHOWING

        public string CheckPw(string name, string password)
        {
            string pw =  dal.CheckPw(name, password);
            return pw;
        }

     
        }
        }
    
    

