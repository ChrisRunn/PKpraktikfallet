﻿using System;
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
        Controller c = new Controller();

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string password = textBox2.Text;


              if(textBox1.Text.ToLower().Equals("admin") && textBox2.Text.ToLower().Equals("password"))
            {
                this.Hide();
                bool b = true;
                EmployeeFrame openFrame = new EmployeeFrame(textBox1.Text, b);
                openFrame.Show();
            }

              else if (textBox2.Text.Equals(c.CheckPw(name, password)))
              {
                  this.Hide();
                  bool b = false;
                  EmployeeFrame openFrame = new EmployeeFrame(textBox1.Text, b);
                  openFrame.Show();

              }

             

          

            //Ta bort Kommentarer
            /*   if (textBox1.Text == "")
               {
                   MessageBox.Show("Ange ditt användarnamn!");
               }

               else if (textBox2.Text == "")
               {
                   MessageBox.Show("Ange ditt lösenord");
               }

               */

            /*string name = textBox1.Text;
            if 


            else
            {

                this.Hide();
                bool b = false;

                EmployeeFrame openFrame = new EmployeeFrame(textBox1.Text, b);
                openFrame.Show();
            }
*/
        }
         

           
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

            
    
             
           
        


        

        
       
    }
}
