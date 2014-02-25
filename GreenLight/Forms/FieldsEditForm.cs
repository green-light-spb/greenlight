using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GreenLight
{
    public struct field_struct
    {
        public string field_label, field_value, field_mask;
    }

    public partial class FieldsEditForm : Form
    {
        
        private field_struct[] fields;
        public field_struct[] Fields
        {
            get { return fields; }
            set { fields = value; }
        }
        private ArrayList tbFields;
        private ArrayList lbFields;

        public FieldsEditForm()
        {
            InitializeComponent();
        }

        private void FieldsEditForm_Load(object sender, EventArgs e)
        {
            
            tbFields = new ArrayList();
            lbFields = new ArrayList();
            foreach (field_struct field in fields)
            {
                MaskedTextBox tb = new MaskedTextBox();
                tbFields.Add(tb);

                tb.Left = 160;
                tb.Top = 10 + tbFields.IndexOf(tb) * 22;
                tb.Width = 200;
                tb.Height = 18;
                tb.Text = field.field_value;
                tb.Mask = field.field_mask;

                this.Controls.Add(tb);

                Label lb = new Label();
                lbFields.Add(lb);

                lb.Left = 10;
                lb.Top = 10 + lbFields.IndexOf(lb) * 22;
                lb.Width = 140;
                lb.Height = 18;
                lb.Text = field.field_label;

                this.Controls.Add(lb);

                
            }

            this.ClientSize = new System.Drawing.Size(370, 45+22*tbFields.Count);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (MaskedTextBox tb in tbFields)
            {
                fields[tbFields.IndexOf(tb)].field_value = tb.Text;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
