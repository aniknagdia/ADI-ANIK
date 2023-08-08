using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthEx
{
    public partial class frmLogin : Form
    {
        Database1Entities db = new Database1Entities();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void lbBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmRegister frm = new frmRegister();
            frm.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var data = db.tblUsers.Where(u => u.UserName == txtUserName.Text
                          && u.Password == txtPassword.Text).FirstOrDefault(); //Chekeing UserName & Password In Database

            if (data != null)
            {   

                UserInfo.Id = data.Id;
                UserInfo.FullName = data.FullName;
                UserInfo.UserName = data.UserName;
                UserInfo.Password = data.Password;

                //Add logged in user detail in UserClass
                this.Hide();

                frmMainMenu frm = new frmMainMenu();
                frm.ShowDialog();
            }
            else
            {
                 MessageBox.Show("Invalid User !!!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
