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
        public void DeleteObject(string objNr)
        {
            dal.DeleteObject(objNr);

        }
        /*//Uppdatera OBJEKT
        public  UpdateObject(string objNr, string objAdress, string objCity,  objPrice, double objArea, string objRooms, string objUnitType, string objInfo, string brokerSsnr)
        {
             dal.UpdateObject(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr);
            
        }*/

        // This method updates everything in the object tab
        public void UpdateObjectFlap(string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo, string objNr, string name, string phoneNr, string email, string ownerSsnr)
        {
            dal.UpdateObjectFlap(objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, objNr, name, phoneNr, email, ownerSsnr);

        }

        //This metod adds an object and an owner
        public void AddObjectAndOwner(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
            string brokerSsnr, string ownerSsnr, string phoneNr, string email, string name)
        {
            dal.AddObjectAndOwner(objNr, objAdress, objCity,
           objPrice, objArea, objRooms, objUnitType, objInfo,
           brokerSsnr, ownerSsnr, phoneNr, email, name);

        }
        // Adds a real estate object
        public void AddObject(string objNr, string objAdress, string objCity,
           string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
           string brokerSsnr, string ownerSsnr)
        {
            this.dal.AddObject(objNr, objAdress, objCity,
               objPrice, objArea, objRooms, objUnitType, objInfo,
               brokerSsnr, ownerSsnr);

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
        public void addObjectImage(byte[] img, string objNr)
        {
            dal.addObjectImage(img, objNr);

        }


        #endregion OBJEKT
        #region MÄKLARE
        //This method adds a realestate broker to the database
        public void AddBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            dal.AddBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);

        }
        //This method deletes a realestate broker from the database
        public void DeleteBroker(string brokerSsnr)
        {
            dal.DeleteBroker(brokerSsnr);

        }
        //This method updates a realestate broker in the database
        public void UpdateBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            dal.UpdateBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);

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
        public void AddProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            dal.AddProspectiveBuyer(buyerSsnr, name, phoneNr, email);

        }

        //This method deletes a prospective buyer in the database
        public void DeleteProspectiveBuyer(string buyerSsnr)
        {
            dal.DeleteProspectiveBuyer(buyerSsnr);

        }
        //This method updates information for a prospective buyer in the database
        public void UpdateProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            dal.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);

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
        //This method adds an owner to the database.                                        Används voide...
        /*public void AddObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
             dal.AddObjectOwner(ownerSsnr, phoneNr, email);
            
        }
        
        //This method updates an owner in the database                                       Används voide...
        public void UpdateObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
             dal.UpdateObjectOwner(ownerSsnr, phoneNr, email);
            
        }*/

        //This method deletes an owner in the database
        public void DeleteObjectOwner(string ownerSsnr)
        {
            dal.DeleteObjectOwner(ownerSsnr);

        }

        //This method shows object owner                          
        public DataTable GetObjectOwner(string ownerSsnr)
        {
            DataTable dt = dal.GetObjectOwner(ownerSsnr);
            return dt;
        }

        /*This method shows all owners                                          Används voide...
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
        public void AddShowing(string objNr, string buyerSsnr, string showingDate)
        {
            dal.AddShowing(objNr, buyerSsnr, showingDate);

        }

        //This method updates a showingdate for a showing
        public void UpdateShowing(string objNr, string buyerSsnr, string showingDate)
        {
            dal.UpdateShowing(objNr, buyerSsnr, showingDate);

        }
        //This method displays all showings
        public DataTable GetShowings()
        {
            DataTable dt = dal.GetShowings();
            return dt;
        }
        //This method deletes a prospective buyer from a showing
        public void DeleteBuyerFromShowing(string buyerSsnr, string objNr)
        {
            dal.DeleteBuyerFromShowing(buyerSsnr, objNr);

        }
        //This method deletes a showing
        public void DeleteShowing(string objNr)
        {
            dal.DeleteShowing(objNr);

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



