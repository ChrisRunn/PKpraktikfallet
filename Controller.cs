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

        //This method deletes an object
        public int DeleteObject(string objNr)
        {
            int nrOfRows = dal.DeleteObject(objNr);
            return nrOfRows;
        }
        /*//Uppdatera OBJEKT
        public int UpdateObject(string objNr, string objAdress, string objCity, int objPrice, double objArea, string objRooms, string objUnitType, string objInfo, string brokerSsnr)
        {
            int nrOfRows = dal.UpdateObject(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr);
            return nrOfRows;
        }*/

        // This method updates everything in the object tab
        public int UpdateObjectFlap(string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo, string objNr, string name, string phoneNr, string email, string ownerSsnr)
        {
            int nrOfRows = dal.UpdateObjectFlap(objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, objNr, name, phoneNr, email, ownerSsnr);
            return nrOfRows;
        }

        //This metod adds an object and an owner
        public int AddObjectAndOwner(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
            string brokerSsnr, string ownerSsnr, string phoneNr, string email, string name)
        {
            int nrOfRows = dal.AddObjectAndOwner(objNr, objAdress, objCity,
            objPrice, objArea, objRooms, objUnitType, objInfo,
            brokerSsnr, ownerSsnr, phoneNr, email, name);
            return nrOfRows;
        }
        // Adds a real estate object
        public int AddObject(string objNr, string objAdress, string objCity,
           string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
           string brokerSsnr, string ownerSsnr)
        {
            int nrOfrows = this.dal.AddObject(objNr, objAdress, objCity,
                objPrice, objArea, objRooms, objUnitType, objInfo,
                brokerSsnr, ownerSsnr);
            return nrOfrows;
        }

        //This method works as a search function for objects
        public DataTable SearchObjectByString(string searchString)
        {
            DataTable dt = dal.SearchObjectByString(searchString);
            return dt;
        }


        //This function works as a search function for objects  ??????????????????????????? Kolla upp vad denn gör !
        public DataTable GetObject(string objNr)
        {
            DataTable dt = dal.GetObject(objNr);
            return dt;
        }

        //This method shows all objects
        public DataTable GetAllObjectsNr()
        {
            DataTable dt = dal.GetAllObjects();
            return dt;
        }

        //This method shows all objects for a specific realestate broker
        public DataTable SearchObjectByBrokerSsnr(string searchString)
        {
            DataTable dt = dal.SearchObjectByBrokerSsnr(searchString);
            return dt;
        }

        //This method shows all showings for a realestate broker
        public DataTable SearchShowingsByBrokerSsnr(string searchString)
        {
            DataTable dt = dal.SearchShowingsByBrokerSsnr(searchString);
            return dt;
        }

        //This method works as a search function to see if an object with the same object number exists
        public bool ObjectExists(string objNr)
        {
            bool objectExists = dal.ObjectExists(objNr);
            return objectExists;
        }

        //This method saves an image for a specific object
        public int addObjectImage(byte[] img, string objNr)
        {
            int nrOfRows = dal.addObjectImage(img, objNr);
            return nrOfRows;
        }


        #endregion OBJEKT
        #region MÄKLARE
        //This method adds a realestate broker to the database
        public int AddBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            int nrOfRows = dal.AddBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
            return nrOfRows;
        }
        //This method deletes a realestate broker from the database
        public int DeleteBroker(string brokerSsnr)
        {
            int nrOfRows = dal.DeleteBroker(brokerSsnr);
            return nrOfRows;
        }
        //This method updates a realestate broker in the database
        public int UpdateBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            int nrOfRows = dal.UpdateBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
            return nrOfRows;
        }
        //This method shows a specific realestate broker in the database
        public DataTable GetBroker(string brokerSsnr)
        {
            DataTable dt = dal.GetBroker(brokerSsnr);
            return dt;
        }
        //This method shows all existing realestate brokers in the database
        public DataTable GetAllBrokers()
        {
            DataTable dt = dal.GetAllBrokers();
            return dt;
        }

        //This method checks if a realestate broker with a specific ssnr already exists in the database
        public bool BrokerExists(string brokerSsnr)
        {
            bool brokerExists = dal.BrokerExists(brokerSsnr);
            return brokerExists;
        }

        //This method works as search function in the realestate broker database
        public DataTable SearchBrokerByString(string searchString)
        {
            DataTable dt = dal.SearchBrokerByString(searchString);
            return dt;
        }
        #endregion MÄKLARE
        #region SPEKULANT


        //This method registers a prospective buyer to the database
        public int AddProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            int nrOfRows = dal.AddProspectiveBuyer(buyerSsnr, name, phoneNr, email);
            return nrOfRows;
        }

        //This method deletes a prospective buyer in the database
        public int DeleteProspectiveBuyer(string buyerSsnr)
        {
            int nrOfRows = dal.DeleteProspectiveBuyer(buyerSsnr);
            return nrOfRows;
        }
        //This method updates information for a prospective buyer in the database
        public int UpdateProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            int nrOfRows = dal.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);
            return nrOfRows;
        }
        //This method works as a search function for prospective buyers
        public DataTable GetProspectiveBuyer(string buyerSsnr)
        {
            DataTable dt = dal.GetProspectiveBuyer(buyerSsnr);
            return dt;
        }

        //This method shows all the prospective buyers
        public DataTable GetAllProspectiveBuyers()
        {
            DataTable dt = dal.GetAllProspectiveBuyers();
            return dt;
        }

        //This method checks if a prospective buyer with a specific ssnr already exists 
        public bool ProspectiveBuyerExists(string Ssnr)
        {
            bool showingExists = dal.ProspectiveBuyerExists(Ssnr);
            return showingExists;
        }


        //This method works as a search function for prospective buyers
        public DataTable SearchProBuyerByString(string searchString)
        {
            DataTable dt = dal.SearchProBuyerByString(searchString);
            return dt;
        }
        #endregion SPEKULANT
        #region OBJEKTÄGARE
        //This method adds an owner to the database.                                        Används inte...
        /*public int AddObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            int nrOfRows = dal.AddObjectOwner(ownerSsnr, phoneNr, email);
            return nrOfRows;
        }
        
        //This method updates an owner in the database                                       Används inte...
        public int UpdateObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            int nrOfRows = dal.UpdateObjectOwner(ownerSsnr, phoneNr, email);
            return nrOfRows;
        }*/

        //This method deletes an owner in the database
        public int DeleteObjectOwner(string ownerSsnr)
        {
            int nrOfRows = dal.DeleteObjectOwner(ownerSsnr);
            return nrOfRows;
        }

        //This method shows object owner                          
        public DataTable GetObjectOwner(string ownerSsnr)
        {
            DataTable dt = dal.GetObjectOwner(ownerSsnr);
            return dt;
        }
        
        /*This method shows all owners                                          Används inte...
        public DataTable GetAllObjectOwners()
        {
            DataTable dt = dal.GetAllObjectOwners();
            return dt;
        }*/

        //This method checks if an owner exists. 
        public bool OwnerExists(string ownerSsnr)
        {
            bool ownerExists = dal.OwnerExists(ownerSsnr);
            return ownerExists;
        }
        
        //This method checks if an owner has got other objects
        public bool OwnerHasOtherObjects(string ownerSsnr)
        {
            bool ownerHasMoreObjects = this.dal.OwnerHasOtherObjects(ownerSsnr);
            return ownerHasMoreObjects;
        }
        #endregion OBJEKTÄGARE
        #region SHOWING
        //This method registers a showing
        public int AddShowing(string objNr, string buyerSsnr, string showingDate)
        {
            int nrOfRows = dal.AddShowing(objNr, buyerSsnr, showingDate);
            return nrOfRows;
        }

        //This method updates a showingdate for a showing
        public int UpdateShowing(string objNr, string buyerSsnr, string showingDate)
        {
            int nrOfRows = dal.UpdateShowing(objNr, buyerSsnr, showingDate);
            return nrOfRows;
        }
        //This method displays all showings
        public DataTable GetShowings()
        {
            DataTable dt = dal.GetShowings();
            return dt;
        }
        //This method deletes a prospective buyer from a showing
        public int DeleteBuyerFromShowing(string buyerSsnr, string objNr)
        {
            int nrOfRows = dal.DeleteBuyerFromShowing(buyerSsnr, objNr);
            return nrOfRows;
        }
        //This method deletes a showing
        public int DeleteShowing(string objNr)
        {
            int nrOfRows = dal.DeleteShowing(objNr);
            return nrOfRows;
        }
        //This method checks if showing exists
        public bool ShowingExists(string objNr, string buyerSsnr)
        {
            bool showingExists = dal.ShowingExists(objNr, buyerSsnr);
            return showingExists;
        }
        #endregion SHOWING

        //This method checks if a password matches a user in the database
        public string CheckPw(string name, string password)
        {
            string pw = dal.CheckPw(name, password);
            return pw;
        }


    }
}



