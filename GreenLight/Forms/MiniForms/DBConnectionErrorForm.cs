using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight.MiniForms
{
    public partial class DBConnectionErrorForm : Form
    {
        public DBConnectionErrorForm()
        {
            InitializeComponent();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }

        private void btnChangeParams_Click(object sender, EventArgs e)
        {
            LocalParameters.EditParameters();
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            Close();
        }
    }
}
