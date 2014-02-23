using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight
{
    public partial class StringReplaseSettings : Form
    {
        private string key_field_db_name;
        //Закладываемся на справочник в качестве key field, пока банк это строка тут будут дубли из ComboBox
        private ArrayList key_field_values;

        private ArrayList reference_db_names;

        private DataTable dt_replaces;
        private DataTable dt_ref_values;

        int current_key_field_index, current_ref_db_names_index;

        public StringReplaseSettings()
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

        private void StringReplaseSettings_Load(object sender, EventArgs e)
        {
            //Получим наименование ключевого поля
            try
            {
                key_field_db_name = DBFunctions.GetGlobalParameter("StringReplaceKeyField");
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Close();
            }

            //Теперь пользовательское наименование для размещения в label на тулстрипе
            Dictionary<string,object> parameters = new Dictionary<string,object>();
            parameters.Add("coldbname", key_field_db_name);
            tslKeyFieldName.Text = (string)DBFunctions.ReadScalarFromDB("SELECT ColumnName FROM tableconfig WHERE ColumnDBName = @coldbname",parameters);

            //Получим все возможные значения ключевого поля
            cbKeyFieldValues.Items.Clear();
            key_field_values = new ArrayList();
            DataTable dt_key_field_values = DBFunctions.ReadFromDB("SELECT DISTINCT " + key_field_db_name + " AS key_field FROM table_credprogr ORDER BY " + key_field_db_name);
            
            foreach (DataRow kf_row in dt_key_field_values.Rows)
            {
                cbKeyFieldValues.Items.Add(kf_row["key_field"]);
                key_field_values.Add(kf_row["key_field"]);
            }

            //Заполним справочники
            reference_db_names = new ArrayList();

            DataTable dt_references = DBFunctions.ReadFromDB("SELECT referencesconfig.ReferenceDBName,referencesconfig.ReferenceName FROM referencesconfig ORDER BY referencename");

            foreach (DataRow ref_row in dt_references.Rows)
            {
                cbReferenceNames.Items.Add(ref_row["ReferenceName"]);
                reference_db_names.Add(ref_row["ReferenceDBName"]);
            }

            dgReplaceStrings.AutoGenerateColumns = false;
            dgReplaceStrings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataGridViewTextBoxColumn ref_id_column = new DataGridViewTextBoxColumn();

            ref_id_column.Name = "ref_id";
            ref_id_column.HeaderText = "ID элемента справочника";
            ref_id_column.DataPropertyName = "table_reference_id";
            ref_id_column.Visible = false;
            dgReplaceStrings.Columns.Add(ref_id_column);

            DataGridViewComboBoxColumn ref_name_column = new DataGridViewComboBoxColumn();

            ref_name_column.Name = "ref_name";
            ref_name_column.HeaderText = "Элемент справочника";
            ref_name_column.DataPropertyName = "table_reference_value";
            dgReplaceStrings.Columns.Add(ref_name_column);

            DataGridViewTextBoxColumn replace_string_column = new DataGridViewTextBoxColumn();

            replace_string_column.Name = "replace_string";
            replace_string_column.HeaderText = "Строка для замены";
            replace_string_column.DataPropertyName = "replace_string";
            dgReplaceStrings.Columns.Add(replace_string_column);

            dt_replaces = new DataTable();

            dgReplaceStrings.DataSource = dt_replaces;

            cbReferenceNames.SelectedIndex = 0;
            cbKeyFieldValues.SelectedIndex = 0;
        }

        private void FillDataGrid()
        {
            dt_replaces.Clear();

            if (cbKeyFieldValues.SelectedIndex == -1 || cbReferenceNames.SelectedIndex == -1)
                return;
      
            dt_ref_values = DBFunctions.ReadFromDB("SELECT id, RefName FROM ref_data_" + reference_db_names[cbReferenceNames.SelectedIndex] + " ORDER BY RefName");

            DataGridViewComboBoxColumn ref_name_column = (DataGridViewComboBoxColumn)dgReplaceStrings.Columns["ref_name"];
            ref_name_column.Items.Clear();

            foreach (DataRow ref_values_row in dt_ref_values.Rows)
            {
                ref_name_column.Items.Add(ref_values_row["RefName"]);
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tkfv", key_field_values[cbKeyFieldValues.SelectedIndex]);
            parameters.Add("trdn", reference_db_names[cbReferenceNames.SelectedIndex]);            
            dt_replaces.Merge(DBFunctions.ReadFromDB("SELECT table_reference_id,table_reference_value,replace_string FROM replace_strings WHERE table_key_field_value = @tkfv AND table_reference_db_name = @trdn",parameters));

        }

        private void SaveData()
        {
            dgReplaceStrings.EndEdit();

            Validate();
            
            dt_replaces.AcceptChanges();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tkfv", key_field_values[current_key_field_index]);
            parameters.Add("trdn", reference_db_names[current_ref_db_names_index]);            
            
            DBFunctions.ExecuteCommand("DELETE FROM replace_strings WHERE table_key_field_value = @tkfv AND table_reference_db_name = @trdn",parameters);

            foreach(DataRow replace_row in dt_replaces.Rows)
            {
                parameters = new Dictionary<string, object>();
                parameters.Add("tkfv", key_field_values[current_key_field_index]);
                parameters.Add("trdn", reference_db_names[current_ref_db_names_index]);
                parameters.Add("refid", replace_row["table_reference_id"]);
                parameters.Add("refvalue", replace_row["table_reference_value"]);
                parameters.Add("replace_string", replace_row["replace_string"]);
                DBFunctions.ExecuteCommand("INSERT INTO replace_strings VALUES(@tkfv,@trdn,@refid,@refvalue,@replace_string)",parameters);
            }

        }

        private bool proceedWithChanges()
        {
            Validate();
            if (dt_replaces.GetChanges() != null)
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

        private void cbKeyFieldValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proceedWithChanges())
            {
                current_key_field_index = cbKeyFieldValues.SelectedIndex;
                current_ref_db_names_index = cbReferenceNames.SelectedIndex;
                FillDataGrid();
            }
        }

        private void dgReplaceStrings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataRow curr_row = FindCurrentRow(dgReplaceStrings);

            DataGridViewComboBoxColumn ref_name_column = (DataGridViewComboBoxColumn)dgReplaceStrings.Columns["ref_name"];

            curr_row["table_reference_id"] = dt_ref_values.Rows[ref_name_column.Items.IndexOf(curr_row["table_reference_value"])]["id"];

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void StringReplaseSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!proceedWithChanges())
                e.Cancel = true;
        }

        private void dgReplaceStrings_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                int l = dt_replaces.Select("table_reference_value = '" + e.FormattedValue + "'").Length;
                if (FindCurrentRow(dgReplaceStrings)[1].Equals(e.FormattedValue))
                {
                    l--;
                }

                if (l > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Этот элемент уже есть в таблице", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Cancel = true;                   
                }
            }
        }

 

    }
}
