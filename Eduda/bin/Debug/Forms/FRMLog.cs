using MaterialTextboxExample;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Eduda.Forms
{
    public partial class FRMLog : Form
    {
        string placeholder2 = "UserName";
        string placeholder = "Password";
       
        public FRMLog()
        {
            InitializeComponent();
            txtpass.Text = placeholder;
            txtpass.ForeColor = Color.Gray;

            txtpass.Enter += (s,e) =>
            {
                if (txtpass.Text == placeholder)
                {
                    txtpass.Text = "";
                    txtpass.ForeColor = Color.Black;
                   txtpass.UseSystemPasswordChar = true;
                }
            };
            txtpass.Leave += (s, e) => {
                if(string.IsNullOrWhiteSpace(txtpass.Text))
                {
                    txtpass.UseSystemPasswordChar= false;
                    txtpass.Text = placeholder;
                    txtpass.ForeColor = Color.Gray;
                }
            };

            txtUse.Text = placeholder2;
            txtUse.ForeColor = Color.Gray;

            txtUse.Enter += (s,e) =>
            {
                if (txtUse.Text == placeholder2)
                {
                    txtUse.Text = "";
                    txtUse.ForeColor = Color.Black;
                }
            };
            txtUse.Leave += (s, e) => {
                if(string.IsNullOrWhiteSpace(txtUse.Text))
                {
                    txtUse.Text = placeholder2;
                    txtUse.ForeColor = Color.Gray;
                }
            };

        }
        
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void Exit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("تواصل مع فريق الدعم" +
                "\n" +
                "                                   ali@gmail.com");
        }
        static  int count = 0;
        private void label1_Click(object sender, EventArgs e)
        { DT dt = new DT();
          
            if (count == 3)
            {
                MessageBox.Show("لقد حاولت كثيرا, الرجاء التواصل مع فريق الدعم" +
                    "" +
                    "\n" +
                    "Ali@gmail.com");
                Application.Exit();
                
            }
                if (dt.checkUser(txtUse.Text) && dt.checkPass(txtpass.Text))
            {
                
                if (dt.checkallow(txtUse.Text)) 
                {
                   Globals.typevv= dt.checktype(txtUse.Text);
                    MessageBox.Show("مرحبا");
                    this.Hide();
                    Dashboard dashboard = new Dashboard();
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
                    dashboard.Show();

                }
                else
                {
                    MessageBox.Show("وجدنا حسابك ولكن تم توقيفك من قبل الاداره");
                    Application.Exit();
                }
                
            }
            else
            {
                MessageBox.Show("Not found");
                count++;

            }
        }
        //txtUse
        //txtpass

        private void txtUse_Click(object sender, EventArgs e)
        {

        }

        private void txtpass_Click(object sender, EventArgs e)
        {

        }
    }

}
