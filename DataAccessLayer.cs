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

        //generisk metod för att skicka query som uppdaterar eller lägger till nya objekt
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
            catch (SqlException ex)
            {
                Debug.WriteLine("{0} SqlException caught.", ex);
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("{0} Exception caught.", ex);
                return 0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //Generisk metod för att exempelvis söka objekt
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
            catch (SqlException ex)
            {
                Debug.WriteLine("{0} Exception caught.", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("{0} Exception caught.", ex);
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
        //Lägg till OBJEKT -- BEHÖVS INTE
        public int AddObject(string objNr, string objAdress, string objCity, int objPrice, double objArea, string objRooms, string objUnitType, string objInfo, string brokerSsnr)
        {
            string sqlStr = "insert into RealEstateObject values ('";
            sqlStr += objNr + "','" + objAdress + "','" + objCity + "','" + objPrice +
                "','" + objArea + "','" + objRooms + "','" + objUnitType + "','" + objInfo + "','" + brokerSsnr + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;

        }
        //Ta bort OBJEKT
        public int DeleteObject(string objNr)
        {
            string sqlStr = "delete from RealEstateObject where objNr = '" + objNr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Uppdatera OBJEKT
        public int UpdateObject(string objNr, string objAdress, string objCity, int objPrice, double objArea, string objRooms, string objUnitType, string objInfo, string brokerSsnr)
        {
            string sqlStr = "update RealEstateObject set objAdress = '" + objAdress + "',objCity = '" + objCity +
                "', objPrice = '" + objPrice + "', objArea = '" + objArea +
                "', objRooms = '" + objRooms + "', objUnitType = '" + objUnitType +
                "', objInfo = '" + objInfo + "',brokerSsnr = '" + brokerSsnr + "' where objNr = '" + objNr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Söka OBJEKT
        public DataTable GetObject(string objNr)
        {
            string sqlStr = "select * from RealEstateObject where objNr = '" + objNr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        // Hämta alla OBJEKT
        public DataTable GetAllObjectsNr()
        {
            string sqlStr = "select * from RealEstateObject";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Sökknapp i Objekt för att visa objekt med viss sträng
        public DataTable SearchObjectByString(string searchString)
        {
            string sqlStr = "Select * from RealEstateObject where objNr like '%" + searchString + "%' or objAdress like '%" + searchString + "%' or objCity like '%" + searchString + "%' or objPrice like '%" + searchString + "%' or objArea like '%" + searchString + "%' or objRooms like '%" + searchString + "%' or objUnitType like '%" + searchString + "%' or brokerSsnr like '%" + searchString + "%'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Hämta alla objekt med angivet Brokernummer
        public DataTable SearchObjectByBrokerSsnr(string searchString)
        {
            string sqlStr = "select objNr as Objektnummer, objAdress as Adress, objCity as Stad, objPrice as Pris, objArea as Area, objRooms as 'Antal rum', objUnitType as Typ, objInfo as Beskrivning from RealEstateObject, RealEstateBroker where name = '" + searchString + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Hämta alla visningar med angivet Brokernummer
        public DataTable SearchShowingsByBrokerSsnr(string searchString)
        {
            string sqlStr = "select s.objNr as Objektsnummer, objAdress as Adress, showingDate as Datum from Showing s, RealEstateBroker, RealEstateObject o where s.objNr = o.objNr and name = '" + searchString + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        #endregion OBJEKT
        #region MÄKLARE
        //Lägg till MÄKLARE
        public int AddBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            string sqlStr = "insert into RealEstateBroker values ('" + brokerSsnr + "','" + name + "','" + brokerAddress + "','" + city + "','" + phoneNr + "','" + email + "','" + pw + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Ta bort MÄKLARE
        public int DeleteBroker(string brokerSsnr)
        {
            string sqlStr = "delete from RealEstateBroker where brokerSsnr = '" + brokerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Uppdatera MÄKLARE
        public int UpdateBroker(string brokerSsnr, string name, string brokerAddress, string city, string phoneNr, string email, string pw)
        {
            string sqlStr = "update RealEstateBroker set name = '" + name + "', brokerAddress = '" + brokerAddress + "', city = '" + city + "', phoneNr = '" + phoneNr + "', email = '" + email + "'," + "pw = '" + pw + "' where brokerSsnr = '" + brokerSsnr +"'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Söka MÄKLARE
        public DataTable GetBroker(string brokerSsnr)
        {
            string sqlStr = "select * from RealEstateBroker where brokerSsnr = '" + brokerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Hämta ALLA MÄKLARE
        public DataTable GetAllBrokers()
        {
            string sqlStr = "select * from RealEstateBroker";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }

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
        #endregion MÄKLARE
        #region SPEKULANT
        //Lägga till spekulant
        public int AddProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            string sqlStr = "insert into ProspectiveBuyer values('" + buyerSsnr + "','" + name + "','" + phoneNr + "','" + email + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Ta bort spekulant
        public int DeleteProspectiveBuyer(string buyerSsnr)
        {
            string sqlStr = "delete from ProspectiveBuyer where buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Uppdatera spekulant
        public int UpdateProspectiveBuyer(string buyerSsnr, string name, string phoneNr, string email)
        {
            string sqlStr = "update ProspectiveBuyer set name = '" + name + "', phoneNr = '" + phoneNr + "', email = '" + email + "' where buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Söka spekulant
        public DataTable GetProspectiveBuyer(string buyerSsnr)
        {
            string sqlStr = "select * from ProspectiveBuyer where buyerSsnr = '" + buyerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Hämta alla spekulanter
        public DataTable GetAllProspectiveBuyers()
        {
            string sqlStr = "select * from Prospectivebuyer";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Sökknapp i Spekulant för att visa objekt med viss sträng
        public DataTable SearchProBuyerByString(string searchString)
        {
            string sqlStr = "Select * from ProspectiveBuyer where buyerSsnr like '%" + searchString + "%' or name like '%" + searchString + "%' or phoneNr like '%" + searchString + "%' or email like '%" + searchString + "%'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }


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
        //Lägg till Ägare   ___OBS___ Lägger endast till en ägare i systemet. Kopplingen mellan Object och Owner görs ej här!
        public int AddObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            string sqlStr = "insert into ObjectOwner values ('" + ownerSsnr + "','" + phoneNr + "','" + email + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Ta bort ägare
        public int RemoveObjectOwner(string ownerSsnr)
        {
            string sqlStr = "delete from ObjectOwner where ownerSsnr = '" + ownerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Uppdatera Ägare -- Behövs eventuellt inte
        public int UpdateObjectOwner(string ownerSsnr, string phoneNr, string email)
        {
            string sqlStr = "update ObjectOwner set phoneNr = '" + phoneNr + "', email = '" + email + "' where ownerSsnr = '" + ownerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }

        // Uppdaterar allt i objekt fliken ! MÅSTE PLACERAS I RÄTT FOLDER
        public int UpdateObjectFlik(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo, string ownerSsnr, string phoneNr, string email, string name)
        {
            string sqlStr = "update RealEstateObject set objAdress ='" + objAdress + "',objArea =" + objArea + ",objCity ='"
                + objCity + "',objInfo ='" + objInfo + "',objPrice ="
                + objPrice + ",objRooms ='" + objRooms + "',objUnitType ='"
                + objUnitType + "' where objNr =" + objNr;
            sqlStr += "update ObjectOwner set phoneNr ='" + phoneNr + "',email ='" + email + "',name ='" + name + "'";
            MessageBox.Show(sqlStr);
            //sqlStr += "update HasOwner set ownerSsnr = '" + ownerSsnr + "'where objNr ='" + objNr + "'";



            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;

        }

        //Registrera Objekt och dess ägare MÅSTE PLACERAS I RÄTT FOLDER

        public int RegisterObjectAndOwner(string objNr, string objAdress, string objCity,
            string objPrice, string objArea, string objRooms, string objUnitType, string objInfo,
            string brokerSsnr, string ownerSsnr, string phoneNr, string email, string name)
        {
            string sqlStr = "insert into RealEstateObject values (";
            sqlStr += objNr + ",'" + objAdress + "','" + objCity + "'," + objPrice +
                "," + objArea + ",'" + objRooms + "','" + objUnitType + "','" + objInfo + "','" + brokerSsnr + "')";
            sqlStr += "insert into ObjectOwner values ('" + ownerSsnr + "','" + phoneNr + "','" + email + "','" + name + "')";
            sqlStr += "insert into HasOwner values(" + objNr + ",'" + ownerSsnr + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            MessageBox.Show(sqlStr);
            return nrOfRows;
        }


        //Söka Ägare -- BEHÖVS EVENTUELLT INTE
        public DataTable GetObjectOwner(string ownerSsnr)
        {
            string sqlStr = "select * from ObjectOwner where ownerSsnr = '" + ownerSsnr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        // Hämtar alla ägare (objectOwner)
        public DataTable GetAllObjectOwners()
        {
            string sqlStr = "select * from ObjectOwner";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        #endregion OBJEKTÄGARE
        #region HASOWNER
        //Hämta ÄGARE (hasowner)
        public DataTable GetObjOwner(string objNr)
        {
            string sqlStr = "select * from HasOwner where objNr ='" + objNr + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Registrera givet OBJEKT med given ÄGARE     HÄR registreras ett objekts ägare
        public int RegisterObjectOwner(string objNr, string ownerSsnr)
        {
            string sqlStr = "insert into HasOwner values ('" + objNr + "','" + ownerSsnr + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Hämtar allt från hasowner tabellen
        public DataTable GetAllHasOwner()
        {
            string sqlStr = "select * from HasOwner";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        #endregion HASOWNER
        #region SHOWING
        //Registrera VISNING
        public int RegisterShowing(string objNr, string buyerSsnr, string showingDate)
        {
            string sqlStr = "insert into Showing values ('" + objNr + "','" + buyerSsnr + "','" + showingDate + "')";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Uppdatera visningsdatum för VISNING
        public int UpdateShowing(string objNr, string buyerSsnr, string showingDate)
        {
            string sqlStr = "update Showing set showingDate = '" + showingDate + "' where objNr = '" + objNr + "' and buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Visa alla VISNINGAR
        public DataTable GetShowings()
        {
            string sqlStr = "select * from Showing";
            DataTable dt = ExecuteQuery(sqlStr);
            return dt;
        }
        //Ta bort spekulant från VISNING
        public int DeleteBuyerFromShowing(string buyerSsnr, string objNr)
        {
            string sqlStr = "delete from Showing where objNr = '" + objNr + "' and buyerSsnr = '" + buyerSsnr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Ta bort VISNING 
        public int DeleteShowing(string objNr)
        {
            string sqlStr = "delete from Showing where objNr = '" + objNr + "'";
            int nrOfRows = ExecuteUpdate(sqlStr);
            return nrOfRows;
        }
        //Kontrollera om VISNING finns
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

        public String CheckPw(string name, string password)
        {
            try
            {
            string sqlStr = "Select pw from RealEstateBroker where pw = '" + password + "' and name = '" + name + "'";
            DataTable dt = ExecuteQuery(sqlStr);
            string pw = dt.Rows[0][0].ToString();

            return pw;
            }
            catch (Exception e)
            {
                
            MessageBox.Show("Det finns ingen användare med detta lösenord, var god försök igen");
            return null;
            }
            



        }
    }
}
