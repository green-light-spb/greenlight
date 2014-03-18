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
    public partial class AuthUsers : Form
    {
        DataTable dt_users;

        public AuthUsers()
        {
            InitializeComponent();
        }

        private void FillDataGrid()
        {
            dt_users = DBFunctions.ReadFromDB("SELECT auth_user.id AS 'Идентификатор', login AS 'Имя пользователя', auth_roles.Name AS 'Роль' FROM auth_user LEFT JOIN auth_roles ON auth_user.role_id = auth_roles.id");
            
            dgUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;            
            dgUsers.DataSource = dt_users;            
            
        }

        private void AuthUsers_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void dgUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbEdit_Click(sender, e);
        }


        private void tsbEdit_Click(object sender, EventArgs e)
        {
            DataRow curr_row = Samoyloff.Tools.FindCurrentRow(dgUsers);

            if (curr_row == null)
                return;

            int curr_id = (int)curr_row["Идентификатор"];

            Auth.AuthUserEdit aue = new AuthUserEdit();
            aue.user_id = curr_id;

            if (aue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int col = dgUsers.CurrentCell.ColumnIndex;
                int row = dgUsers.CurrentCell.RowIndex;

                FillDataGrid();

                dgUsers.CurrentCell = dgUsers[col, row];
            }
        }
               
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            Auth.AuthUserEdit aue = new AuthUserEdit();
            if (aue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int col = dgUsers.CurrentCell.ColumnIndex;

                FillDataGrid();

                dgUsers.CurrentCell = dgUsers[col, dgUsers.Rows.Count - 1];
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            DataRow curr_row = Samoyloff.Tools.FindCurrentRow(dgUsers);

            if (curr_row == null)
                return;

            int curr_id = (int)curr_row["Идентификатор"];

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", curr_id);

            DBFunctions.ExecuteCommand("DELETE FROM auth_user WHERE id = @id", parameters);

            int col = dgUsers.CurrentCell.ColumnIndex;
            int row = dgUsers.CurrentCell.RowIndex;

            FillDataGrid();

            dgUsers.CurrentCell = dgUsers[col, Math.Min(row,dgUsers.Rows.Count-1)];

        }
    }
}
