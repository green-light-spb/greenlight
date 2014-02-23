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
    public partial class ReferenceStructureEdit : Form
    {
        DataTable dt_ref_config,reference_list;
        public ReferenceStructureEdit()
        {
            InitializeComponent();
        }

        private void FillDataGrid()
        {
            dt_ref_config = DBFunctions.ReadFromDB("SELECT * FROM ReferencesConfig WHERE ReferenceDBName = '" + reference_list.Rows[cbCurrentReference.SelectedIndex]["ReferenceDBName"] + "'");

            dt_ref_config.TableNewRow += new DataTableNewRowEventHandler(dt_TableNewRow);

            dgRefConfig.Columns.Clear();
            
            dgRefConfig.DataSource = dt_ref_config;

            dgRefConfig.Columns["ReferenceConfigID"].Visible = false;
            dgRefConfig.Columns["ReferenceName"].Visible = false;
            dgRefConfig.Columns["ReferenceDBName"].Visible = false;
            dgRefConfig.Columns["Hierarchycal"].Visible = false;
            dgRefConfig.Columns["ColumnDBName_Old"].Visible = false;


            dgRefConfig.Columns["ColumnName"].HeaderText = "Имя колонки";
            dgRefConfig.Columns["ColumnDBName"].HeaderText = "Имя колонки в БД";

            DataGridViewComboBoxColumn column_type = new DataGridViewComboBoxColumn();
            column_type.Items.Add("Строка50");
            column_type.Items.Add("Строка300");
            column_type.Items.Add("Число с плавающей точкой");
            column_type.Items.Add("Целое число");
            column_type.Width = 200;
            column_type.FlatStyle = FlatStyle.Flat;
            column_type.Name = "Тип колонки";
            column_type.DataPropertyName = "ColumnType";
            dgRefConfig.Columns.Remove("ColumnType");
            dgRefConfig.Columns.Add(column_type);

        }

        private void ReferenceStructureEdit_Load(object sender, EventArgs e)
        {
            reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceName,ReferenceDBName,Hierarchycal FROM referencesconfig");
            foreach (DataRow row in reference_list.Rows)
            {
                cbCurrentReference.Items.Add((string)row["ReferenceName"]);
            }
            cbCurrentReference.SelectedIndex = 0;
        }

        private void cbCurrentReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dt_ref_config!=null)
                SaveData();
            FillDataGrid();
        }

        private void dgRefConfig_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (Convert.ToString(e.Row.Cells["ColumnDBName"].Value) == "RefName")
                e.Cancel = true;
        }

        private void dgRefConfig_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (Convert.ToString(dgRefConfig["ColumnDBName", e.RowIndex].Value) == "RefName")
                e.Cancel = true;
        }

        private void SaveData()
        {
            TableStruct ts = new TableStruct();
            ts.TableName = "ReferencesConfig";
            string[] p_keys = { "ReferenceConfigID" };
            ts.p_keys = p_keys;
            string[] columns = { "ReferenceName", "ReferenceDBName", "Hierarchycal", "ColumnName", "ColumnDBName", "ColumnType" };
            ts.columns = columns;

            DBFunctions.WriteToDB(dt_ref_config, ts);

            DBStructure.UpdateDBStructure();
        }

        private void dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row["ReferenceName"] = reference_list.Rows[cbCurrentReference.SelectedIndex]["ReferenceName"];
            e.Row["ReferenceDBName"] = reference_list.Rows[cbCurrentReference.SelectedIndex]["ReferenceDBName"];
            e.Row["Hierarchycal"] = 1;
        }

        private void ReferenceStructureEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
            DBStructure.UpdateDBStructure();
        }

        private void tbAddReference_Click(object sender, EventArgs e)
        {
            string ref_name = Microsoft.VisualBasic.Interaction.InputBox("Введите наименование справочника", "Ввод наименования");
            if (ref_name == "")
                return;
            string ref_db_name = Microsoft.VisualBasic.Interaction.InputBox("Введите название справочника для базы данных", "Ввод наименования");
            if (ref_db_name == "")
                return;

            SaveData();
            
            DBFunctions.ExecuteCommand("INSERT INTO ReferencesConfig SET ReferenceName = '" + ref_name + "',ReferenceDBName = '" +
                ref_db_name + "',Hierarchycal = 1,ColumnName = 'Наименование',ColumnDBName = 'RefName',ColumnType = 'Строка50'");

            DBStructure.UpdateDBStructure();

            reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceName,ReferenceDBName,Hierarchycal FROM referencesconfig");
            
            cbCurrentReference.Items.Clear();

            foreach (DataRow row in reference_list.Rows)
            {
                cbCurrentReference.Items.Add((string)row["ReferenceName"]);
            }
            
            foreach(string Item in cbCurrentReference.Items)
            {
                if(Item == ref_name)
                {
                    cbCurrentReference.SelectedIndex = cbCurrentReference.Items.IndexOf(Item);
                    break;
                }
            }           
        }

        private void tbDeleteReference_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Данные справочника будут безвозвратно удалены. Продолжить?", "Вопрос", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string ref_db_name = (string)reference_list.Rows[cbCurrentReference.SelectedIndex]["ReferenceDBName"];
            DBFunctions.ExecuteCommand("DELETE FROM ReferencesConfig WHERE ReferenceDBName='" + 
                ref_db_name + "';" +
                "DROP TABLE `ref_data_" + ref_db_name + "`;" + 
                "DROP TABLE `ref_hierarchy_" + ref_db_name + "`;");

            cbCurrentReference.Items.Clear();

            reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceName,ReferenceDBName,Hierarchycal FROM referencesconfig");
            foreach (DataRow row in reference_list.Rows)
            {
                cbCurrentReference.Items.Add((string)row["ReferenceName"]);
            }
            cbCurrentReference.SelectedIndex = 0;
        }


        
    }
}
