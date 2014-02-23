using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Samoyloff;

namespace GreenLight
{
    public partial class TableStructureEdit : Form
    {
        DataTable dt_tableconfig;
        string[] table_db_names = {"Clients","CredProgr"};
        int last_table_index = -1;

        public TableStructureEdit()
        {
            InitializeComponent();            
        }

        private DataRow FindCurrentRow(DataGridView dgv)
        {
            CurrencyManager cManager =
                dgv.BindingContext[dgv.DataSource, dgv.DataMember]
                     as CurrencyManager;
            if (cManager == null || cManager.Count == 0)
                return null;

            DataRowView drv = cManager.Current as DataRowView;
            return drv.Row;
        } 

        private void FillDataGrid()
        {
            dt_tableconfig = DBFunctions.ReadFromDB("SELECT * FROM TableConfig WHERE TableDBName = '" + table_db_names[cbTables.SelectedIndex] + "'");

            dt_tableconfig.TableNewRow += new DataTableNewRowEventHandler(dt_TableNewRow); 

            dgTableConfig.Columns.Clear();

            dgTableConfig.DataSource = dt_tableconfig;

            dgTableConfig.Columns["TableConfigID"].Visible = false;
            
            dgTableConfig.Columns["TableConfigID"].SortMode = DataGridViewColumnSortMode.Automatic;

            dgTableConfig.Columns["TableName"].Visible = false;
            dgTableConfig.Columns["TableDBName"].Visible = false;
            dgTableConfig.Columns["ColumnDBName_Old"].Visible = false;

            dgTableConfig.Columns["ColumnName"].HeaderText = "Имя колонки";
            dgTableConfig.Columns["ColumnDBName"].HeaderText = "Имя колонки в БД";
            dgTableConfig.Columns["ReferenceMultiSelect"].HeaderText = "Множественный выбор";
            dgTableConfig.Columns["WebOrder"].HeaderText = "Порядок отображения в подборе";

            DataGridViewComboBoxColumn column_type = new DataGridViewComboBoxColumn();
            column_type.Items.Add("Строка50");
            column_type.Items.Add("Строка300");
            column_type.Items.Add("Число с плавающей точкой");
            column_type.Items.Add("Справочник");
            column_type.Items.Add("Целое число");
            column_type.Items.Add("Формула");
            column_type.Width = 200;
            column_type.FlatStyle = FlatStyle.Flat;
            column_type.Name = "Тип колонки";
            column_type.DataPropertyName = "ColumnType";
            dgTableConfig.Columns.Remove("ColumnType");
            dgTableConfig.Columns.Add(column_type);

            DataGridViewComboBoxColumn column_ref = new DataGridViewComboBoxColumn();
            DataTable reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceDBName FROM referencesconfig");
            foreach (DataRow ref_row in reference_list.Rows)
                column_ref.Items.Add(ref_row["ReferenceDBName"]);
            column_ref.Width = 200;
            column_ref.FlatStyle = FlatStyle.Flat;
            column_ref.Name = "Имя справочника";
            column_ref.DataPropertyName = "ColumnReference";
            dgTableConfig.Columns.Remove("ColumnReference");
            dgTableConfig.Columns.Add(column_ref);

            DataGridViewCheckBoxColumn column_show = new DataGridViewCheckBoxColumn();
            column_show.TrueValue = 1;
            column_show.FalseValue = 0;
            column_show.Name = "Показывать в предложении";
            column_show.DataPropertyName = "ShowInOffer";
            dgTableConfig.Columns.Remove("ShowInOffer");
            dgTableConfig.Columns.Add(column_show);

            DataGridViewCheckBoxColumn column_show_short = new DataGridViewCheckBoxColumn();
            column_show_short.TrueValue = 1;
            column_show_short.FalseValue = 0;
            column_show_short.Name = "Показывать в предложении(кратко)";
            column_show_short.DataPropertyName = "ShowInOfferShort";
            dgTableConfig.Columns.Remove("ShowInOfferShort");
            dgTableConfig.Columns.Add(column_show_short);

            DataGridViewCheckBoxColumn column_showInEditor = new DataGridViewCheckBoxColumn();
            column_showInEditor.TrueValue = 1;
            column_showInEditor.FalseValue = 0;
            column_showInEditor.Name = "Показывать в редакторе";
            column_showInEditor.DataPropertyName = "ShowInEditor";
            dgTableConfig.Columns.Remove("ShowInEditor");
            dgTableConfig.Columns.Add(column_showInEditor);

            DataGridViewCheckBoxColumn column_useInWhereClause = new DataGridViewCheckBoxColumn();
            column_useInWhereClause.TrueValue = 1;
            column_useInWhereClause.FalseValue = 0;
            column_useInWhereClause.Name = "Используется в условии";
            column_useInWhereClause.DataPropertyName = "UseInWhereClause";
            dgTableConfig.Columns.Remove("UseInWhereClause");
            dgTableConfig.Columns.Add(column_useInWhereClause);

        }

        private void SaveData()
        {
            TableStruct ts = new TableStruct();
            ts.TableName = "TableConfig";
            string[] p_keys = { "TableConfigID" };
            ts.p_keys = p_keys;
            string[] columns = { "TableName", "TableDBName", "ColumnName", "ColumnDBName", "ColumnType", "ColumnReference", "ReferenceMultiSelect", "ShowInOffer", "ShowInOfferShort", "ShowInEditor", "WebOrder", "UseInWhereClause" };
            ts.columns = columns;

            DBFunctions.WriteToDB(dt_tableconfig, ts);

            DBStructure.UpdateDBStructure();
        }

        private void TableStructureEdit_Load(object sender, EventArgs e)
        {
            cbTables.SelectedIndex = 0;
            FillDataGrid();
        }

        private void TableStructureEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (Tools.ProceedWithChanges(dt_tableconfig))
            {
                case Tools.ProceedWithChangesAnswers.SaveAndProceed:
                    SaveData();
                    break;
                case Tools.ProceedWithChangesAnswers.Cancel:
                    cbTables.SelectedIndex = last_table_index;
                    return;
            }
        }
  
        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (last_table_index == cbTables.SelectedIndex)
                return;

            switch (Tools.ProceedWithChanges(dt_tableconfig))
            {
                case Tools.ProceedWithChangesAnswers.SaveAndProceed:
                    SaveData();
                    break;
                case Tools.ProceedWithChangesAnswers.Cancel:
                    cbTables.SelectedIndex = last_table_index;
                    return;
            }
            FillDataGrid();
            last_table_index = cbTables.SelectedIndex;
        }

        private void dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row["TableName"] = cbTables.Items[cbTables.SelectedIndex].ToString();
            e.Row["TableDBName"] = table_db_names[cbTables.SelectedIndex].ToString();
            e.Row["ReferenceMultiSelect"] = false;
            e.Row["ShowInOffer"] = false;
        }

        private void tsbUp_Click(object sender, EventArgs e)
        {
            DataRow curr_row = FindCurrentRow(dgTableConfig);
            int curr_id = (int)curr_row["TableConfigID"];
            
            //Получим предыдущий номер
            object prev_id_obj = DBFunctions.ReadScalarFromDB("SELECT TableConfigID From TableConfig WHERE TableConfigID < " + Convert.ToString(curr_row["TableConfigID"]) +
                " AND TableDBName = '" + curr_row["TableDBName"] + "' ORDER BY TableConfigID DESC LIMIT 1");

            int prev_id;

            if(prev_id_obj != null)
                prev_id = (int)prev_id_obj;
            else
                return;
            

            DBFunctions.ExecuteScript("CALL SwapLinesInTableConfig(" + Convert.ToString(curr_id) + "," + Convert.ToString(prev_id) + ")");

            int rw = dgTableConfig.CurrentCell.RowIndex;
            int col = dgTableConfig.CurrentCell.ColumnIndex;

            FillDataGrid();            

            dgTableConfig.CurrentCell = dgTableConfig[col,rw-1];
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
            DataRow curr_row = FindCurrentRow(dgTableConfig);
            int curr_id = (int)curr_row["TableConfigID"];

            //Получим следующий номер
            object next_id_obj = DBFunctions.ReadScalarFromDB("SELECT TableConfigID From TableConfig WHERE TableConfigID > " + Convert.ToString(curr_row["TableConfigID"]) +
                " AND TableDBName = '" + curr_row["TableDBName"] + "' ORDER BY TableConfigID LIMIT 1");

            int next_id;

            if (next_id_obj != null)
                next_id = (int)next_id_obj;
            else
                return;


            DBFunctions.ExecuteScript("CALL SwapLinesInTableConfig(" + Convert.ToString(curr_id) + "," + Convert.ToString(next_id) + ")");

            int rw = dgTableConfig.CurrentCell.RowIndex;
            int col = dgTableConfig.CurrentCell.ColumnIndex;

            FillDataGrid();

            dgTableConfig.CurrentCell = dgTableConfig[col, rw + 1];
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            DataRow curr_row = FindCurrentRow(dgTableConfig);
            int curr_id = (int)curr_row["TableConfigID"];
            
            DataTable dt_columns = DBFunctions.ReadFromDB("SELECT ColumnDBName FROM tableConfig WHERE TableDBName = '" + table_db_names[cbTables.SelectedIndex] + "'");

            string fields="";

            foreach (DataRow row in dt_columns.Rows)
            {
                fields += row["ColumnDBName"];
                if (dt_columns.Rows.IndexOf(row) + 1 < dt_columns.Rows.Count)
                    fields += ",";
            }

            DBFunctions.ExecuteCommand("INSERT INTO table_clients (" + fields + ") SELECT " + fields + " FROM table_clients WHERE ID = " + Convert.ToString(curr_id));

            int col = dgTableConfig.CurrentCell.ColumnIndex;

            FillDataGrid();

            dgTableConfig.CurrentCell = dgTableConfig[col, dgTableConfig.Rows.Count - 1];            

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void tstbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tstbSearch.Text == "")
                dt_tableconfig.DefaultView.RowFilter = "";
            else
            {
                string rf_text = "";

                foreach (DataGridViewColumn clmn in dgTableConfig.Columns)
                {
                    if (!clmn.Visible)
                        continue;

                    if (clmn.ValueType == typeof(String))
                    {
                        if (rf_text != "")
                            rf_text += " OR ";
                        rf_text += "`" + clmn.DataPropertyName + "`" + " LIKE '%" + tstbSearch.Text + "%'";
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(tstbSearch.Text);
                            if (rf_text != "")
                                rf_text += " OR ";
                            rf_text += "`" + clmn.DataPropertyName + "`" + " = '" + tstbSearch.Text + "'";
                        }
                        catch (Exception) { };
                    }
                }

                dt_tableconfig.DefaultView.RowFilter = rf_text;
            }
        }
     }
}
