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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            
            this.Hide();

            if (textBox1.Text == "Admin")
            {
                bool b = true;
                EmployeeFrame openFrame = new EmployeeFrame(textBox1.Text, b);
                openFrame.Show();
            }

            else
            {
                bool b = false;

                EmployeeFrame openFrame = new EmployeeFrame(textBox1.Text, b);
                openFrame.Show();
            }
            }

            
          
             
           
        


        

        
       
    }
}
