using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace GreenLight.Auth
{
    public partial class AuthRoleEdit : Form
    {
        Auth.Rights rt;
        CheckBox[] checkboxes;
        public string role_name;
        public string result;
        
        public AuthRoleEdit()
        {
            InitializeComponent();
            rt = new Rights();
        }

        public AuthRoleEdit(string r_string):this()
        {            
            rt.Deserialize(r_string);
        }

        public void CreateControls()
        {
            Type r_type = typeof(Rights);

            FieldInfo[] fi_array = r_type.GetFields();

            checkboxes = new CheckBox[fi_array.Length * 2];

            int fi_index = 0;
            int cb_index = 0;
            
            foreach (FieldInfo fi in fi_array)
            {
                int x_coord = 20;
                int label_width = 120;
                int y_coord = 20 + fi_index * 25;
                Label new_label = new Label();

                new_label.Text = fi.Name;
                new_label.Top = y_coord;
                new_label.Left = x_coord;

                Controls.Add(new_label);

                AccessLevel al = (AccessLevel)r_type.InvokeMember(fi.Name, BindingFlags.GetField, null, rt, null);

                checkboxes[cb_index] = new CheckBox();
                checkboxes[cb_index].Text = "Read";
                checkboxes[cb_index].Top = y_coord-4;
                checkboxes[cb_index].Left = x_coord + label_width;
                checkboxes[cb_index].Width = 70;
                checkboxes[cb_index].Checked = al.read;
                checkboxes[cb_index].Tag = cb_index;
                checkboxes[cb_index].CheckedChanged+= new EventHandler(ReadCBOff);
                Controls.Add(checkboxes[cb_index++]);

                checkboxes[cb_index] = new CheckBox();
                checkboxes[cb_index].Text = "Write";
                checkboxes[cb_index].Top = y_coord-4;
                checkboxes[cb_index].Left = x_coord + label_width + 80;
                checkboxes[cb_index].Width = 70;
                checkboxes[cb_index].Checked = al.write;
                checkboxes[cb_index].Tag = cb_index;
                checkboxes[cb_index].CheckedChanged += new EventHandler(WriteCBOn);
                Controls.Add(checkboxes[cb_index++]);

                fi_index++;                
            }

            this.Height = 70 + (fi_index + 1) * 25;
            this.Width = 40 + 120 + 80 + 80;
        }

        private void AuthRoleEdit_Load(object sender, EventArgs e)
        {
            Text = "Роль: " + role_name;
            CreateControls();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            result = "";
            foreach (CheckBox cb in checkboxes)
            {
                if (cb.Checked)
                    result += "1";
                else
                    result += "0";
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void ReadCBOff(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked == false)
                checkboxes[(int)(((CheckBox)sender).Tag) + 1].Checked = false;
        }

        private void WriteCBOn(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked == true)
                checkboxes[(int)(((CheckBox)sender).Tag) - 1].Checked = true;
        }
    }
}
