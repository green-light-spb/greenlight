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
    public partial class AuthUserEdit : Form
    {
        public int user_id;
        DataTable dt_roles;

        public AuthUserEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", user_id);
            parameters.Add("login", tbLogin.Text);
            parameters.Add("role_id", dt_roles.Rows[cbRole.SelectedIndex]["id"]);

            if (user_id != 0)
            {
                if (tbPassword.Text == "")
                {
                    DBFunctions.ExecuteCommand("UPDATE auth_user SET login=@login,role_id=@role_id WHERE id=@id", parameters);
                }
                else
                {
                    if (tbCredentials.Text == "")
                    {
                        System.Windows.Forms.MessageBox.Show("Должно быть заполнено поле \"Данные подключения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    parameters.Add("encrypted_credentials", Encryption.Encrypt(tbCredentials.Text, tbPassword.Text));
                    DBFunctions.ExecuteCommand("UPDATE auth_user SET login=@login,role_id=@role_id,encrypted_credentials=@encrypted_credentials WHERE id=@id", parameters);
                }
            }
            else
            {
                if (tbPassword.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("Должно быть заполнено поле \"Пароль\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                if (tbCredentials.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("Должно быть заполнено поле \"Данные подключения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                parameters.Add("encrypted_credentials", Encryption.Encrypt(tbCredentials.Text, tbPassword.Text));
                DBFunctions.ExecuteCommand("INSERT INTO auth_user SET login=@login,role_id=@role_id,encrypted_credentials=@encrypted_credentials", parameters);
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void AuthUserEdit_Load(object sender, EventArgs e)
        {
            dt_roles = DBFunctions.ReadFromDB("SELECT id,name FROM auth_roles ORDER BY name");

            foreach(DataRow role_row in dt_roles.Rows)
            {
                cbRole.Items.Add((string)role_row["name"]);
            }


            if (user_id != 0)
            {
                Dictionary<string,object> parameters = new Dictionary<string,object>();
                parameters.Add("id", user_id);
                
                tbLogin.Text = (string)DBFunctions.ReadScalarFromDB("SELECT login FROM auth_user WHERE id = @id",parameters);
                int role_id = (int)DBFunctions.ReadScalarFromDB("SELECT role_id FROM auth_user WHERE id = @id", parameters);

                for (int i = 0; i < dt_roles.Rows.Count; i++)
                {
                    if ((int)dt_roles.Rows[i]["id"] == role_id)
                    {
                        cbRole.SelectedIndex = i;
                        break;
                    }
                }

            }
        }

        
    }
}
