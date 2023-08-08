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
    public partial class frmCoustomer : Form
    { 
        Database1Entities db = new Database1Entities();
        public frmCoustomer()
        {
            InitializeComponent();
        }

        private void frmCoustomer_Load(object sender, EventArgs e)
        {
            var data = db.tblCoustomers.ToList();
            dgv.DataSource = data;
        }
    }
}
