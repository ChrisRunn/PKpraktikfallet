using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace praktikfall
{
    class Controller
    {
        DataAccessLayer dal = new DataAccessLayer();
        #region OBJEKT

        //This method deletes an object
        public string DeleteObject(string objNr, string ownerSsnr)
        {
            bool objectExists = ObjectExists(objNr);
            bool ownerHasMoreObjects = OwnerHasOtherObjects(ownerSsnr);

            if (objectExists && !ownerHasMoreObjects)
            {
                dal.DeleteObjectOwner(ownerSsnr);
                dal.DeleteObject(objNr);
                return "Objekt borttaget. Ägaren är även borttagen eftersom ägaren ej äger andra objekt.";
            }
            else if (objectExists && ownerHasMoreObjects)
            {
                dal.DeleteObject(objNr);
                return "Objekt borttaget.";
            }
            else
            {
                return "Inget sådant objekt finns.";
            }

        }

        // This method updates everything in the object tab
        public string UpdateObjectFlap(string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo, string objNr, string name, string phoneNr, string email, string ownerSsnr)
        {
            bool objectExists = ObjectExists(objNr);
            bool ownerExists = OwnerExists(ownerSsnr);

            if (!(Regex.IsMatch(objPrice, "^[0-9]+$")))
            {
                return "Pris får bara innehålla siffror.";
            }

            else if (!(Regex.IsMatch(objArea, "^[0-9]+$")))
            {
                return "Boarean får bara innehålla siffror.";
            }

            else if (objectExists && ownerExists)
            {
                dal.UpdateObjectFlap(objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, objNr, name, phoneNr, email, ownerSsnr);
                return "Objekt med objektnr " + objNr + " uppdaterat.";
            }
            else
            {
                return "Kan ej uppdatera objektsnummer eller ägarens personnummer.";
            }

        }

        //This metod adds an object and an owner
        public string AddObjectAndOwner(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
            string brokerSsnr, string ownerSsnr, string phoneNr, string email, string name)
        {

            bool brokerExists = BrokerExists(brokerSsnr);
            bool ownerExists = OwnerExists(ownerSsnr);

            if (!(Regex.IsMatch(objNr, "^[0-9]+$")))
            {
                return "Objektnummer får endast innehålla siffror.";
            }

            if (!(Regex.IsMatch(objPrice, "^[0-9]+$")))
            {
                return "Pris får bara innehålla siffror.";
            }

            else if (!(Regex.IsMatch(objArea, "^[0-9]+$")))
            {
                return "Bostadsarea får bara innehålla siffror.";
            }

            else if (string.IsNullOrEmpty(ownerSsnr) || string.IsNullOrEmpty(objNr))
            {
                return "Ägare kan inte registreras utan objekt, och objekt kan inte registreras utan ägare.";
            }

            bool objectExists = ObjectExists(objNr);
            if (!objectExists && brokerExists && !ownerExists)
            {
                this.dal.AddObjectAndOwner(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr, ownerSsnr, phoneNr, email, name);
                return "Objekt med objnr " + objNr + " och objektägare med personnummer " + ownerSsnr + " registrerad.";
            }
            else if (!objectExists && brokerExists && ownerExists)
            {
                this.dal.AddObject(objNr, objAdress, objCity, objPrice, objArea, objRooms, objUnitType, objInfo, brokerSsnr, ownerSsnr);
                return "Objekt med objnr " + objNr + " och objektägare med personnummer " + ownerSsnr + " registrerad.";
            }
            else if (!brokerExists)
            {
                return "Ingen sådan mäklare finns. Vänlig ange rätt mäklare.";
            }
            else if (objectExists)
            {
                return "Det finns redan ett objekt med objektnummer " + objNr + " registrerat.";
            }
            return "DETTA BORDE INTE KUNNA HÄNDA!";    
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
        public string AddBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {

            if (!(Regex.IsMatch(brokerSsnr, "^[0-9]+$")))
            {
                return "Personnummer får endast innehålla siffror.";
            }

            else if (name.Equals(""))
            {
                return "Du har ej angivit ett namn.";
            }

            else if (brokerAddress.Equals(""))
            {
                return "Du har ej angivit en adress.";
            }

            else if (city.Equals(""))
            {
                return "Du har ej angivit en stad.";
            }

            else if (phoneNr.Equals(""))
            {
                return "Du har ej angivit ett telefonnummer.";
            }

            else if (email.Equals(""))
            {
                return "Du har ej angivit en email.";
            }

            else if (pw.Equals(""))
            {
                return "Du har ej angivit ett lösenord.";
            }

            else
            {
                bool brokerExists = BrokerExists(brokerSsnr);

                if (brokerExists)
                {
                    return "Det finns redan en mäklare med personnummer " + brokerSsnr;
                }

                else
                {
                    dal.AddBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
                    return "Mäklare med personnummer " + brokerSsnr + " registrerad.";
                }
            }
        }
        //This method deletes a realestate broker from the database
        public string DeleteBroker(string brokerSsnr)
        {
            bool brokerExists = BrokerExists(brokerSsnr);

            if (!brokerExists)
            {
                return "Ingen sådan mäklare finns registrerad. Vänligen försök igen.";
            }
            else
            {
                dal.DeleteBroker(brokerSsnr);
                return "Mäklare med personnummer " + brokerSsnr + " borttagen";
            }
        }
        //This method updates a realestate broker in the database
        public string UpdateBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            bool brokerExists = BrokerExists(brokerSsnr);
            if (!brokerExists)
            {
                return "Kan inte uppdatera en mäklares personnummer.";
            }
            else
            {
                dal.UpdateBroker(brokerSsnr, name, brokerAddress, city, phoneNr, email, pw);
                return "Mäklare med personnummer " + brokerSsnr + " uppdaterad.";
            }

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
        public string AddProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            bool buyerExists = ProspectiveBuyerExists(buyerSsnr);

            if (buyerExists)
            {
                return "Det finns redan en spekulant med personnummer " + buyerSsnr + " registrerad.";
            }
            else
            {
                if (!(Regex.IsMatch(buyerSsnr, "^[0-9]+$")))
                {
                    return "Personnummer får endast innehålla siffror.";
                }
                else if (name.Equals(""))
                {
                    return "Du har ej angivit ett namn.";
                }
                else
                {
                    dal.AddProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                    return "Spekulant med personnummer " + buyerSsnr + " har lagts till.";
                }
            }

        }

        //This method deletes a prospective buyer in the database
        public string DeleteProspectiveBuyer(string buyerSsnr)
        {
            bool buyerExists = ProspectiveBuyerExists(buyerSsnr);

            if (!buyerExists)
            {
                return "Ingen sådan spekulant finns. Vänligen försök igen.";
            }
            else
            {
                dal.DeleteProspectiveBuyer(buyerSsnr);
                return "Spekulant borttagen.";
            }
        }
        //This method updates information for a prospective buyer in the database
        public string UpdateProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            bool buyerExists = ProspectiveBuyerExists(buyerSsnr);
            if (!buyerExists)
            {
                return "Det går inte att uppdatera en spekulants personnummer.";
            }
            else
            {
                dal.UpdateProspectiveBuyer(buyerSsnr, name, phoneNr, email);
                return "Spekulant med personnummer " + buyerSsnr + " uppdaterad.";
            }
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
        public string AddShowing(string objNr, string buyerSsnr, string showingDate)
        {
            bool showingExists = ShowingExists(objNr, buyerSsnr);

            if (showingExists)
            {
                return "Visningen finns redan. Var vänlig kontorllera dina val.";
            }
            else
            {
                dal.AddShowing(objNr, buyerSsnr, showingDate);
                return "Visning registrerad.";
            }
        }

        //This method updates a showingdate for a showing
        public string UpdateShowing(string objNr, string buyerSsnr, string showingDate)
        {
            bool showingExists = ShowingExists(objNr, buyerSsnr);
            if (showingExists)
            {
                dal.UpdateShowing(objNr, buyerSsnr, showingDate);
                return "Visningsdatum uppdaterat. Nytt visningsdatum " + showingDate;
            }
            else
            {
                return "Visningen finns ej. Vänligen kontrollera dina val.";
            }

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

        //This method calculates the objects area / the object price
        public double CalculateObjectPricePerKvm(string price, string area)
        {
            double pricePerKvm = Math.Round(double.Parse(price) / double.Parse(area), 0);
            return pricePerKvm;
        }

    }
}



