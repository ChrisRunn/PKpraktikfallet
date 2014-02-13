using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;



namespace praktikfall
{
    class DataAccessLayer
    {
        #region GENERISKA METODER
        string connectionString = "server=localhost; Trusted_Connection=yes; database=PK Praktikfallet;";

        //Generic method for sending a query that updates or adds a new object
        private int ExecuteUpdate(string sqlStr) 
        {
            SqlConnection con = new SqlConnection(connectionString);
            Debug.WriteLine(sqlStr);
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(sqlStr, con);
                int nrOfRows = com.ExecuteNonQuery();
                return nrOfRows;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //Generic method for sending questions to the database. Returns a DataTable
        private DataTable ExecuteQuery(string sqlStr)
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStr, con);
                dataAdapter.Fill(dataTable);
            }

            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dataTable;
        }

        #endregion GENERISKA METODER

        #region OBJEKT
        //Delete a real estate object
        public int DeleteObject(string objNr) 
        {
            string sqlStr = "delete from RealEstateObject where objNr = '" + objNr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Update a a real estate object
        public int UpdateObject(string objNr, string objAdress, string objCity, int objPrice, double objArea, string objRooms, string objUnitType, string objInfo, string brokerSsnr)
        {
            string sqlStr = "update RealEstateObject set objAdress = '" + objAdress + "',objCity = '" + objCity +
                "', objPrice = '" + objPrice + "', objArea = '" + objArea +
                "', objRooms = '" + objRooms + "', objUnitType = '" + objUnitType +
                "', objInfo = '" + objInfo + "',brokerSsnr = '" + brokerSsnr + "'" + " where objNr = '" + objNr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Search for a selected real estate object
        public DataTable GetObject(string objNr)
        {
            string sqlStr = "select * from RealEstateObject where objNr = '" + objNr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        // Get all real estate objects from the database
        public DataTable GetAllObjects()
        {
            string sqlStr = "select objNr as Objektsnummer,objAdress as Adress, brokerSsnr as Mäklare, ownerSsnr as Ägare from RealEstateObject";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Method for finding real estate objects with a search string
        public DataTable SearchObjectByString(string searchString)
        {
            string sqlStr = "select objNr as Objektsnummer, objAdress as Adress, brokerSsnr as Mäklare, ownerSsnr as Ägare from RealEstateObject where objNr like '%" + searchString + "%' or objAdress like '%" + searchString + "%' or objCity like '%" + searchString + "%' or objPrice like '%" + searchString + "%' or objArea like '%" + searchString + "%' or objRooms like '%" + searchString + "%' or objUnitType like '%" + searchString + "%' or brokerSsnr like '%" + searchString + "%'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Get all objects for a specified broker
        public DataTable SearchObjectByBrokerSsnr(string searchString)
        {
            string sqlStr = "select objNr as Objektnummer, objAdress as Adress, objCity as Stad, objPrice as Pris, objArea as Area, objRooms as 'Antal rum', objUnitType as Typ, objInfo as Beskrivning from RealEstateObject, RealEstateBroker where name = '" + searchString + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Get all showings for a specified broker
        public DataTable SearchShowingsByBrokerSsnr(string searchString)
        {
            string sqlStr = "select s.objNr as Objektsnummer, objAdress as Adress, showingDate as Datum from Showing s, RealEstateBroker, RealEstateObject o where s.objNr = o.objNr and name = '" + searchString + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Checks if a object already exists in the database
        public bool ObjectExists(string objNr)
        {
            bool objectExists = false;
            string sqlStr = "select * from RealEstateObject where objNr = '" + objNr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            if (dt.Rows.Count > 0)
            {
                objectExists = true;
                return objectExists;
            }
            return objectExists;
        }
        //Adds a real estate object and its owner

        public int AddObjectAndOwner(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
            string brokerSsnr, string ownerSsnr, string phoneNr, string email, string name)
        {
            string sqlStr = "insert into ObjectOwner values ('" + ownerSsnr + "','" + phoneNr + "','" + email + "','" + name + "')";
            sqlStr += "insert into RealEstateObject values (" + objNr + ",'" + objAdress + "','" + objCity + "'," + objPrice +
                "," + objArea + ",'" + objRooms + "','" + objUnitType + "','" + objInfo + "','" + brokerSsnr + "','" + ownerSsnr + "','')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }

        //Saves an image for a specified real estate object
        public int addObjectImage(byte[] img, string objNr)
        {
            string sqlStr = "update RealEstateObject set objImage = @img where objNr = '" + objNr + "'";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand com = new SqlCommand(sqlStr, con);
            com.Parameters.Add(new SqlParameter("@img", img));
            int nrOfRows = com.ExecuteNonQuery();
            con.Close();
            return nrOfRows;
        }
        #endregion OBJEKT
        #region MÄKLARE
        //Adds a broker
        public int AddBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            string sqlStr = "insert into RealEstateBroker values ('" + brokerSsnr + "','" + name + "','" + brokerAddress + "','" + city + "','" + phoneNr + "','" + email + "','" + pw + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Delete a broker
        public int DeleteBroker(string brokerSsnr)
        {
            string sqlStr = "delete from RealEstateBroker where brokerSsnr = '" + brokerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Update Broker
        public int UpdateBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            string sqlStr = "update RealEstateBroker set name = '" + name + "', brokerAddress = '" + brokerAddress + "', city = '" + city + "', phoneNr = '" + phoneNr + "', email = '" + email + "'," + "pw = '" + pw + "' where brokerSsnr = '" + brokerSsnr +"'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //search an get the information for a specified broker
        public DataTable GetBroker(string brokerSsnr)
        {
            string sqlStr = "select * from RealEstateBroker where brokerSsnr = '" + brokerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Gets all brokers in the databse and their information
        public DataTable GetAllBrokers()
        {
            string sqlStr = "select brokerSsnr as Personnummer, name as Namn, brokerAddress as Adress, city as Stad, phoneNr as Telefon, email as Email, pw as Lösenord from RealEstateBroker";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }

        // Checks if a broker already exists
        public bool BrokerExists(string brokerSsnr)
        {
            bool brokerExists = false;
            string sqlStr = "select * from RealEstateBroker where brokerSsnr = '" + brokerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            if (dt.Rows.Count > 0)
            {
                brokerExists = true;
                return brokerExists;
            }
            return brokerExists;
        }

        //Method for finding a broker with a searchstring
        public DataTable SearchBrokerByString(string searchString)
        {
            string sqlStr = "select brokerSsnr as Personnummer, name as Namn, brokerAddress as Adress, city as Stad, phoneNr as Telefon, email as Email, pw as Lösenord from RealEstateBroker where brokerSsnr like '%" + searchString + "%' or name like '%" + searchString + "%' or brokerAddress like '%" + searchString + "%' or city like '%" + searchString + "%' or phoneNr like '%" + searchString + "%' or email like '%" + searchString + "%'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }

        #endregion MÄKLARE
        #region SPEKULANT
        //Adds a prospective buyer
        public int AddProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            string sqlStr = "insert into ProspectiveBuyer values('" + buyerSsnr + "','" + name + "','" + phoneNr + "','" + email + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Delete a prospective buyer
        public int DeleteProspectiveBuyer(string buyerSsnr)
        {
            string sqlStr = "delete from ProspectiveBuyer where buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Updates the information for a selected prospective buyer
        public int UpdateProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            string sqlStr = "update ProspectiveBuyer set name = '" + name + "', phoneNr = '" + phoneNr + "', email = '" + email + "' where buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Gets information about a specified prospective buyer
        public DataTable GetProspectiveBuyer(string buyerSsnr)
        {
            string sqlStr = "select * from ProspectiveBuyer where buyerSsnr = '" + buyerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Gets all prospective buyers and their information
        public DataTable GetAllProspectiveBuyers()
        {
            string sqlStr = "select buyerSsnr as Personnummer, name as Namn, phoneNr as Telefon, email as Email from Prospectivebuyer";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Method for finding a prospective buyer with a search string
        public DataTable SearchProBuyerByString(string searchString)
        {
            string sqlStr = "select buyerSsnr as Personnummer, name as Namn, phoneNr as Telefon, email as Email from ProspectiveBuyer where buyerSsnr like '%" + searchString + "%' or name like '%" + searchString + "%' or phoneNr like '%" + searchString + "%' or email like '%" + searchString + "%'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }

        //Checks if prospective buyer already exists
        public bool ProspectiveBuyerExists(string Ssnr)
        {
            bool prospectiveBuyerExists = false;
            string sqlStr = "select * from ProspectiveBuyer where buyerSsnr = '" + Ssnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            if (dt.Rows.Count > 0)
            {
                prospectiveBuyerExists = true;
                return prospectiveBuyerExists;
            }
            return prospectiveBuyerExists;
        }
        #endregion SPEKULANT
        #region OBJEKTÄGARE
        //Adds a owner to the database. Note that this method does not connect an owner to an object
        public int AddObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            string sqlStr = "insert into ObjectOwner values ('" + ownerSsnr + "','" + phoneNr + "','" + email + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Deletes an owner
        public int DeleteObjectOwner(string ownerSsnr)
        {
            string sqlStr = "delete from ObjectOwner where ownerSsnr = '" + ownerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Update information for a specified owner
        public int UpdateObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            string sqlStr = "update ObjectOwner set phoneNr = '" + phoneNr + "', email = '" + email + "' where ownerSsnr = '" + ownerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }

        // Update information for a object and owner for a specified object
        public int UpdateObjectFlap(string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo, string objNr, string name, string phoneNr, string email, string ownerSsnr)
        {
            string sqlStr = "update RealEstateObject set objAdress = '" + objAdress + "', objCity = '" + objCity + "', objPrice = '" + objPrice + "', objArea = '" + objArea + "', objRooms = '" + objRooms + "', objUnitType = '" + objUnitType + "', objInfo = '" + objInfo + "' where objNr = '" + objNr +"' ";
            sqlStr += "update ObjectOwner set name = '" + name + "', phoneNr = '" + phoneNr + "', email = '" + email + "' where ownerSsnr = '" + ownerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }

        //Gets information about a specified owner
        public DataTable GetObjectOwner(string ownerSsnr)
        {
            string sqlStr = "select * from ObjectOwner where ownerSsnr = '" + ownerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        // Get information about all owners
        public DataTable GetAllObjectOwners()
        {
            string sqlStr = "select * from ObjectOwner";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Check if a owner exists
        public bool OwnerExists(string ownerSsnr)
        {
            bool ownerExists = false;
            string sqlStr = "select * from ObjectOwner where ownerSsnr = '" + ownerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            if (dt.Rows.Count > 0)
            {
                ownerExists = true;
                return ownerExists;
            }
            return ownerExists;
        }
        // Checks if a owner owns more than one object
        public bool OwnerHasOtherObjects(string ownerSsnr)
        {
            bool ownerHasMoreObjects = true;
            string sqlStr = "select * from RealEstateObject where ownerSsnr = '" + ownerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);

            if (dt.Rows.Count == 1)
            {
                ownerHasMoreObjects = false;
                return ownerHasMoreObjects;
            }
            else
            {
                return ownerHasMoreObjects;
            }
        }
        #endregion OBJEKTÄGARE
        #region SHOWING
        //Add showing
        public int AddShowing(string objNr, string buyerSsnr, string showingDate)
        {
            string sqlStr = "insert into Showing values ('" + objNr + "','" + buyerSsnr + "','" + showingDate + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Update showing date
        public int UpdateShowing(string objNr, string buyerSsnr, string showingDate)
        {
            string sqlStr = "update Showing set showingDate = '" + showingDate + "' where objNr = '" + objNr + "' and buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Show all showings
        public DataTable GetShowings()
        {
            string sqlStr = "select objNr as Objektsnummer, buyerSsnr as Spekulant, showingDate as Visningsdatum from Showing";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Delete prospective buyer from showing
        public int DeleteBuyerFromShowing(string buyerSsnr, string objNr)
        {
            string sqlStr = "delete from Showing where objNr = '" + objNr + "' and buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Delete showing 
        public int DeleteShowing(string objNr)
        {
            string sqlStr = "delete from Showing where objNr = '" + objNr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Check if showing exists
        public bool ShowingExists(string objNr, string buyerSsnr)
        {
            bool showingExists = false;
            string sqlStr = "select * from Showing where objNr = '" + objNr + "' and buyerSsnr = '" + buyerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            if (dt.Rows.Count > 0)
            {
                showingExists = true;
                return showingExists;
            }
            return showingExists;
        }
        #endregion SHOWING
        // Check password for login
        public String CheckPw(string name, string password)
        {
            try
            {
            string sqlStr = "Select pw from RealEstateBroker where pw = '" + password + "' and name = '" + name + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            string pw = dt.Rows[0][0].ToString();
            return pw;
            }
            catch (Exception ex)
            {
                
            MessageBox.Show("Det finns ingen användare med detta lösenord, var god försök igen. \n" + ex);
            return null;
            }          
        }
    }
}
