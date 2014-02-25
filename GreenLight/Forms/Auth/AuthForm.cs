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
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == "" || tbPassword.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Имя пользователя и пароль должны быть заполнены.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            Dictionary<string,object> parameters = new Dictionary<string,object>();
            parameters.Add("login", tbLogin.Text);

            string encrypted_data;
            try
            {
                encrypted_data = (string)DBFunctions.ReadScalarFromDB("SELECT encrypted_credentials FROM auth_user WHERE login = @login", parameters);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Передайте администратору следующее:" + Environment.NewLine + ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            if (encrypted_data == null)
            {
                System.Windows.Forms.MessageBox.Show("Вход не удался.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            string credentials;

            try
            {
                credentials = Encryption.Decrypt(encrypted_data, tbPassword.Text);
            } catch(Exception)
            {
                System.Windows.Forms.MessageBox.Show("Вход не удался.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            string[] cred_array = credentials.Split('%');

            DBFunctions.login = cred_array[0];
            DBFunctions.password = cred_array[1];

            DBFunctions.login_from_parameters = false;
            DBFunctions.Init();

            if (!DBFunctions.TestConnecion())
            {
                DBFunctions.login = DBFunctions.password = "";
                DBFunctions.login_from_parameters = true;
                DBFunctions.Init();
                System.Windows.Forms.MessageBox.Show("Вход прошел удачно, но не удается установить соединение с БД. Обратитесь к администратору", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);                
                return;
            }
            
            LocalParameters.Login = tbLogin.Text;
            LocalParameters.SaveParameters();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();

        }

        private void AuthForm_Shown(object sender, EventArgs e)
        {
            tbLogin.Text = LocalParameters.Login;
            tbPassword.Focus();
        }

        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            tbPassword.Text = "";
        }
    }
}
