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
    public partial class DataCopy : Form
    {
        DataTable dt_columns;
        DataTable dt_table;
        string[] table_db_names = {"clients","credprogr"};
        string[] data_field_names = { "FIO_zaem", "Programma" };

        public DataCopy()
        {
            InitializeComponent();
        }

        private void FillDataGrid()
        {
            dt_columns = DBFunctions.ReadFromDB("SELECT FALSE AS Checked,ColumnName,ColumnDBName FROM tableconfig WHERE TableDBName = '" + table_db_names[cbTables.SelectedIndex] + "'");
            
            dgColumns.Columns.Clear();
            dgColumns.DataSource = dt_columns;
           
            dgColumns.Columns["ColumnName"].HeaderText = "Имя колонки";
            dgColumns.Columns["ColumnName"].ReadOnly = true;
            dgColumns.Columns["ColumnName"].FillWeight = 150;


            dgColumns.Columns["ColumnDBName"].HeaderText = "Имя колонки в базе данных";
            dgColumns.Columns["ColumnDBName"].ReadOnly = true;
            dgColumns.Columns["ColumnDBName"].FillWeight = 50;


            DataGridViewCheckBoxColumn column_checked = new DataGridViewCheckBoxColumn();
            column_checked.TrueValue = true;
            column_checked.FalseValue = false;
            column_checked.Name = "Копировать";
            column_checked.DataPropertyName = "Checked";
            column_checked.FillWeight = 50;
            dgColumns.Columns.Remove("Checked");
            dgColumns.Columns.Insert(0,column_checked);

            dgColumns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

            dt_table = DBFunctions.ReadFromDB("SELECT FALSE AS Source, FALSE AS Destination, ID, " + data_field_names[cbTables.SelectedIndex] +
                " AS data_field FROM table_" + table_db_names[cbTables.SelectedIndex]);

 
            dgTableData.Columns.Clear();
            dgTableData.DataSource = dt_table;

            dgTableData.Columns["data_field"].HeaderText = "Данные строки";
            dgTableData.Columns["data_field"].ReadOnly = true;
            dgTableData.Columns["data_field"].FillWeight = 300;

            dgTableData.Columns["ID"].ReadOnly = true;
            dgTableData.Columns["ID"].FillWeight = 50;

            DataGridViewCheckBoxColumn column_source = new DataGridViewCheckBoxColumn();
            column_source.TrueValue = true;
            column_source.FalseValue = false;
            column_source.Name = "Источник";
            column_source.DataPropertyName = "Source";
            column_source.FillWeight = 30;
            dgTableData.Columns.Remove("Source");
            dgTableData.Columns.Insert(0, column_source);

            DataGridViewCheckBoxColumn column_destination = new DataGridViewCheckBoxColumn();
            column_destination.TrueValue = true;
            column_destination.FalseValue = false;
            column_destination.Name = "Приемники";
            column_destination.DataPropertyName = "Destination";
            column_destination.FillWeight = 30;
            dgTableData.Columns.Remove("Destination");
            dgTableData.Columns.Insert(1, column_destination);

            dgTableData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void DataCopy_Load(object sender, EventArgs e)
        {
            cbTables.SelectedIndex = 0;
        }

        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void tsbFillRange_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt_table.Rows)
            {
                if ((int)(row["ID"]) >= Convert.ToInt32(tbFrom.Text) &&
                    (int)(row["ID"]) <= Convert.ToInt32(tbTo.Text))
                {
                    row["Destination"] = true;
                }
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt_table.Rows)
            {
                row["Destination"] = false;                
            }
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            dgColumns.EndEdit();
            dgTableData.EndEdit();
            string query_text = "UPDATE ";

            query_text += "table_" + table_db_names[cbTables.SelectedIndex] + " AS dest, ";
            query_text += "table_" + table_db_names[cbTables.SelectedIndex] + " AS src SET ";

            bool first = true;
            foreach (DataRow column_row in dt_columns.Rows)
            {
                if (Convert.ToBoolean(column_row["Checked"]))
                {
                    if(first)
                        first = false;                                                                    
                    else 
                        query_text += " , ";

                    query_text += "dest.`" + column_row["ColumnDBName"] + "` = src.`" + column_row["ColumnDBName"] + "`";
                }
            }

            int source_id = -1;
            foreach (DataRow row in dt_table.Rows)
            {
                if (Convert.ToBoolean(row["Source"]))
                {
                    if (source_id == -1)
                        source_id = (int)row["ID"];
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Выбрано больше одного источника", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (source_id == -1)
            {
                System.Windows.Forms.MessageBox.Show("Не выбрано ни одного источника", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            query_text += " WHERE dest.id = [d_id] AND src.id = " + Convert.ToString(source_id) ;

            foreach (DataRow row in dt_table.Rows)
            {
                if (Convert.ToBoolean(row["Destination"]))
                {
                    DBFunctions.ExecuteCommand(query_text.Replace("[d_id]",Convert.ToString(row["ID"])));
                }
            }

            System.Windows.Forms.MessageBox.Show("Копирование завершено.", "Успешное завершение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                
            
        }

        private void dgTableData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
