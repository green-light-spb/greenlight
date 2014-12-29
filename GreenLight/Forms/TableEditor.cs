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
    public partial class TableEditor : Form
    {
        DataTable dt_table_data;
        TableStruct ts;
        string[] table_db_names = { "clients", "credprogr" };

        public TableEditor()
        {
            InitializeComponent();
        }

        private void TestReadRights()
        {
            if (Auth.AuthModule.rights.table_clients.read && Auth.AuthModule.rights.table_credprogr.read)
            {
                cbTables.SelectedIndex = 0;
            } else if (Auth.AuthModule.rights.table_clients.read)
            {
                cbTables.Enabled = false;
                cbTables.SelectedIndex = 0;
            }
            else if (Auth.AuthModule.rights.table_credprogr.read)
            {
                cbTables.Enabled = false;
                cbTables.SelectedIndex = 1;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Нет доступа к таблицам.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Close();
            }
        }

        private bool HaveWriteRights()
        {
            if (cbTables.SelectedIndex == 0)//Клиенты
            {
                return Auth.AuthModule.rights.table_clients.write;
            }
            else //Кредитные программы
            {
                return Auth.AuthModule.rights.table_credprogr.write;
            }
        }

        private void TestWriteRights()
        {
            dgTableData.ReadOnly = !HaveWriteRights();
            tsbAdd.Visible = HaveWriteRights();
            tsbCopy.Visible = HaveWriteRights();
            tsbDelete.Visible = HaveWriteRights();
        }
        
        private void FillDataGrid()
        {
            Cursor.Current = Cursors.WaitCursor;
            dt_table_data = Tables.GetTable(table_db_names[cbTables.SelectedIndex], ref dgTableData, ref ts);
            dgTableData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Cursor.Current = Cursors.Default;   
        }

        private void TableEditor_Load(object sender, EventArgs e)
        {
            //cbTables.SelectedIndex = 0;            
            TestReadRights();
        }
        
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            DataRow current_row = GreenLight.Tools.FindCurrentRow(dgTableData);
            if (current_row == null)
                return;
            var tre = new Forms.TableRecordEditorNew(cbTables.Text, current_row, HaveWriteRights());            
            if (tre.ShowDialog() == DialogResult.OK)
            {
                bool row_added = false;
                if (current_row.RowState == DataRowState.Added)
                {
                    row_added = true;
                }
                DBFunctions.WriteToDB(dt_table_data, ts);

                if (row_added)
                {
                   current_row["ID"] = Convert.ToInt32(DBFunctions.ReadScalarFromDB("SELECT LAST_INSERT_ID()"));
                }                
            }
            else
            {
                dt_table_data.RejectChanges();
            }

        }

        private void dgTableData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestWriteRights();
            this.Text = "Таблица: " + cbTables.SelectedItem;
            FillDataGrid();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            DataRow new_row = dt_table_data.Rows.Add();
            foreach (DataGridViewColumn dg_column in dgTableData.Columns)
            {
                if(dg_column.Visible)
                    dgTableData.CurrentCell = dgTableData[dg_column.Name, dgTableData.Rows.Count - 1];
            }            
            tsbEdit_Click(sender, e);
        }

        private void tbDelete_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Строка таблицы будет удалена безвозвратно. Продолжить?", "Вопрос", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            DataRow current_row = GreenLight.Tools.FindCurrentRow(dgTableData);
            current_row.Delete();
            DBFunctions.WriteToDB(dt_table_data, ts);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DataRow curr_row = GreenLight.Tools.FindCurrentRow(dgTableData);
            int curr_id = (int)curr_row["ID"];

            DataTable dt_columns = DBFunctions.ReadFromDB("SELECT ColumnDBName FROM tableconfig WHERE TableDBName = '" + table_db_names[cbTables.SelectedIndex] + "'");

            string fields = "";

            foreach (DataRow row in dt_columns.Rows)
            {
                fields += row["ColumnDBName"];
                if (dt_columns.Rows.IndexOf(row) + 1 < dt_columns.Rows.Count)
                    fields += ",";
            }

            DBFunctions.ExecuteCommand("INSERT INTO table_" + table_db_names[cbTables.SelectedIndex] + " (" + fields + ") SELECT " + fields + " FROM table_" + table_db_names[cbTables.SelectedIndex] + " WHERE ID = " + Convert.ToString(curr_id));
            
            
            int col = dgTableData.CurrentCell.ColumnIndex;

            FillDataGrid();

            dgTableData.CurrentCell = dgTableData[col, dgTableData.Rows.Count - 1];     

        }

        private void tstbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tstbSearch.Text == "")
                dt_table_data.DefaultView.RowFilter = "";
            else
            {
                string rf_text = "";

                foreach (DataGridViewColumn clmn in dgTableData.Columns)
                {
                    if(!clmn.Visible)
                        continue;

                    if (clmn.ValueType == typeof(String))
                    {
                        if (rf_text != "")
                            rf_text += " OR ";
                        rf_text += clmn.DataPropertyName + " LIKE '%" + tstbSearch.Text + "%'";
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(tstbSearch.Text);
                            if (rf_text != "")
                                rf_text += " OR ";
                            rf_text += clmn.DataPropertyName + " = '" + tstbSearch.Text + "'";
                        }
                        catch (Exception) { };
                    }
                }

                dt_table_data.DefaultView.RowFilter = rf_text;
            }

        }        
    }
}
