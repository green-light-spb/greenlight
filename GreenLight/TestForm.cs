using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight
{
    public partial class TestForm : Form
    {
        DataTable dt;

        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("ColumnDBName", typeof(EditingValue));

            
            var ev = new EditingValue();
            ev.Value = "Вася";
            ev.isReference = false;
            ev.SetDisplayName();
            
            DataRow new_row = dt.NewRow();
            new_row["ColumnDBName"] = ev;
            dt.Rows.Add(new_row);

            ev = new EditingValue();
            ev.Value = 2;
            ev.isReference = true;
            ev.RefDBName = "branch";
            ev.SetDisplayName();

            new_row = dt.NewRow();
            new_row["ColumnDBName"] = ev;
            dt.Rows.Add(new_row);

            ev = new EditingValue();
            ev.Value = "{1}{3}";
            ev.isReference = true;
            ev.isMultiRef = true;
            ev.RefDBName = "branch";
            ev.SetDisplayName();

            new_row = dt.NewRow();
            new_row["ColumnDBName"] = ev;
            dt.Rows.Add(new_row);

            ev = new EditingValue();
            ev.Value = 2;
            ev.isReference = false ;
            ev.SetDisplayName();

            new_row = dt.NewRow();
            new_row["ColumnDBName"] = ev;
            dt.Rows.Add(new_row);
            
            dg.AutoGenerateColumns = false;

            GreenLight.Controls.SelectableTextboxColumn col = new Controls.SelectableTextboxColumn();
            col.Name = "ColumnDBName";
            col.DataPropertyName = "ColumnDBName";

            dg.Columns.Add(col);

            dg.DataSource = dt;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int i = 1;
        }
    }
}
