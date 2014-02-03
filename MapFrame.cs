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
    public partial class MapFrame : Form
    {

        public MapFrame()
        {

            InitializeComponent();


            EmployeeFrame tmp = new EmployeeFrame();
            string adress = tmp.TheObjAdress;
            string city = tmp.TheObjCity;
            StringBuilder queryAdress = new StringBuilder();
            queryAdress.Append("http://maps.google.com/maps?q=");
            queryAdress.Append(adress + "," + city);
            MessageBox.Show(queryAdress.ToString());
            webBrowserMap.Navigate(queryAdress.ToString());
            webBrowserMap.ScriptErrorsSuppressed = true;


        }
    }
}
                

   
    
    
