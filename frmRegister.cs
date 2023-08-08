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
    public partial class frmRegister : Form
    {

        
        Database1Entities db = new Database1Entities();
        

        public frmRegister()
        {
            InitializeComponent();
        }


        private void LoadData()
        {
            var data = db.tblUsers.OrderBy(X=> X.UserName).ToList();
            dataGridView1.DataSource = data;    
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string password = txtPassword.Text;
            bool validPassword = password.Length >= 8
                && password.Any(char.IsLower)
                && password.Any(char.IsUpper);

            if (!validPassword && password!="")
            {
                sb.AppendLine("Password is not valid");
            }

            if (txtFullName.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter user's full name.");
            }

            if (txtUserName.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter username.");
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter password.");
            }

            if (txtConfirmPassword.Text.Trim().Length == 0)
            {
                sb.AppendLine("Please enter confirm password.");
            }

            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                sb.AppendLine("Sorry, Password and Confirm Password does not match.");
            } 

            if (sb.ToString() != String.Empty)
            {
                MessageBox.Show(sb.ToString());
                return;
            }

            var dupliData = db.tblUsers.Where(u=> u.UserName == txtUserName.Text).FirstOrDefault();
            if (dupliData !=null)
            {
                MessageBox.Show("UserName Already Exists !!!", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            
            tblUser user = new tblUser();

            user.FullName = txtFullName.Text;
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.UserType = "Customer";

            db.tblUsers.Add(user);
            db.SaveChanges();

            LoadData();
            ClearFrom();

            MessageBox.Show("Data Saved Successfully ...",
               "Welcome",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);

        }

        private void chkViewPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewPass.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                txtConfirmPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFrom();
           
        }

        private void ClearFrom()
        {
            txtFullName.Clear();
            txtUserName.Clear();    
            txtPassword.Clear();
            txtConfirmPassword.Clear();

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            
            LoadData();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void lbBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }


    }
}
