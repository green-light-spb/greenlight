using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight.Forms
{
    public partial class TableRecordEditorNew : Form
    {
        private string table_name;
        private string table_db_name;
        private DataRow current_row;
        DataTable dt_data;
        DataTable dt_table_structure;

        public TableRecordEditorNew(string tn, DataRow row, bool write_rights)
        {
            InitializeComponent();

            dt_data = new DataTable();
            dt_data.Columns.Add("Description", typeof(String));
            dt_data.Columns.Add("Data", typeof(EditingValue));
            
            table_name = tn;
            current_row = row;

            DataTable dt_table_db_name = DBFunctions.ReadFromDB("SELECT DISTINCT TableDBName FROM TableConfig WHERE TableName = '" + table_name + "'");
            if (dt_table_db_name.Rows.Count != 0)
                table_db_name = (string)dt_table_db_name.Rows[0]["TableDBName"];
            else
            {
                throw new Exception("Таблица не найдена в базе данных.");
            }

            dt_table_structure = DBFunctions.ReadFromDB("SELECT ColumnName,ColumnDBName,ColumnType,ColumnReference,ReferenceMultiSelect FROM tableconfig WHERE TableName = '" + table_name + "'");

            tsbOk.Enabled = write_rights;
            

            foreach (DataRow struct_row in dt_table_structure.Rows)
            {
                DataRow new_row = dt_data.NewRow();

                new_row["Description"] = struct_row["ColumnName"];

                EditingValue ev = new EditingValue();
                ev.FieldDBName = (string)struct_row["ColumnDBName"];
                ev.FieldName = (string)struct_row["ColumnName"];                
                ev.isReference = (Convert.ToString(struct_row["ColumnType"]) == "Справочник");
                ev.Value = current_row[ev.FieldDBName];
                if (ev.isReference)
                {
                    ev.RefDBName = (string)struct_row["ColumnReference"];
                    ev.isMultiRef = (bool)struct_row["ReferenceMultiSelect"];
                }
                ev.SetDisplayName();

                new_row["Data"] = ev;

                dt_data.Rows.Add(new_row);
            }
        }

        private void TableRecordEditorNew_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = "Описание";
            col.DataPropertyName = "Description";
            col.ReadOnly = true;
            col.FillWeight = 100;

            dgRecord.Columns.Add(col);

            GreenLight.Controls.SelectableTextboxColumn col_data = new Controls.SelectableTextboxColumn();
            col_data.Name = "Значение";
            col_data.DataPropertyName = "Data";
            col_data.FillWeight = 100;
            dgRecord.Columns.Add(col_data);

            dgRecord.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgRecord.DataSource = dt_data;
        }

        private void tstbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tstbSearch.Text == "")
                dt_data.DefaultView.RowFilter = "";
            else
            {
                string rf_text = "";

                foreach (DataGridViewColumn clmn in dgRecord.Columns)
                {
                    if (!clmn.Visible)
                        continue;

                    if (clmn.ValueType == typeof(String))
                    {
                        if (rf_text != "")
                            rf_text += " OR ";
                        rf_text += clmn.DataPropertyName + " LIKE '%" + tstbSearch.Text + "%'";
                    }                    
                }

                dt_data.DefaultView.RowFilter = rf_text;
            }
        }

        private void tsbOk_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt_data.Rows)
            {
                EditingValue ev = (EditingValue)row["Data"];

                try
                {
                    if (ev.Value is String && (string)ev.Value == "" && current_row[ev.FieldDBName] == System.DBNull.Value)
                        continue;
                    current_row[ev.FieldDBName] = ev.Value;
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Вы ввели неверное значение для поля " + ev.FieldName, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
