using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight.Auth
{
    public partial class AuthRoles : Form
    {
        DataTable dt_roles;
        
        public AuthRoles()
        {
            InitializeComponent();
        }



        private void FillDataGrid()
        {
            dt_roles = DBFunctions.ReadFromDB("SELECT * FROM auth_roles");

            dgRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgRoles.DataSource = dt_roles;

            dgRoles.Columns[2].Visible = false;

        }

        private void SaveData()
        {
            if (dt_roles.GetChanges() != null)
            {
                TableStruct ts = new TableStruct();
                ts.TableName = "auth_roles";
                string[] p_keys = { "id" };
                ts.p_keys = p_keys;
                string[] columns = { "name", "rights" };
                ts.columns = columns;
                DBFunctions.WriteToDB(dt_roles, ts);
            }
        }

        private bool proceedWithChanges()
        {
            Validate();
            if (dt_roles.GetChanges() != null)
            {
                DialogResult res = System.Windows.Forms.MessageBox.Show("Таблице есть изменения. Сохранить?", "Вопрос", System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveData();
                    return true;
                }
                else if (res == System.Windows.Forms.DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void AuthRoles_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            DataRow curr_row = Samoyloff.Tools.FindCurrentRow(dgRoles);

            if (curr_row == null)
                return;

            if (curr_row["rights"] == DBNull.Value)
                curr_row["rights"] = "";

            AuthRoleEdit are = new AuthRoleEdit((string)curr_row["rights"]);
            are.role_name = (string)curr_row["name"];
            if(are.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                curr_row["rights"] = are.result;
            }                 
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveData();
            FillDataGrid();
        }

        private void AuthRoles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!proceedWithChanges())
                e.Cancel = true;
        } 
    }
}
