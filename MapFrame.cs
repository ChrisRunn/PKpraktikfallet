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



            //string adress = "Erikslustvägen";
            //string city = "Malmö";
            StringBuilder queryAddress = new StringBuilder();
            queryAddress.Append("maps.google.com");
            //queryAddress.Append(adress + "," + city);
            webBrowserMap.Navigate(queryAddress.ToString());
            webBrowserMap.ScriptErrorsSuppressed = true;
        }
    }
}
