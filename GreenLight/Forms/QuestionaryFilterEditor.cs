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
    public partial class QuestionaryFilterEditor : Form
    {
        DataTable dt_filter;
        int questionary_id;

        public QuestionaryFilterEditor(int q_id)
        {
            questionary_id = q_id;

            InitializeComponent();

            LoadOtborFields();

            FillDataGrid();
        }

        private void TestRights()
        {
            tsbSave.Visible = Auth.AuthModule.rights.questionary_editor.write;
            tsbDiscard.Visible = Auth.AuthModule.rights.questionary_editor.write;
            tsbDuplicate.Visible = Auth.AuthModule.rights.questionary_editor.write;
            dgFilter.ReadOnly = !Auth.AuthModule.rights.questionary_editor.write;            
        }

        private void LoadOtborFields()
        {
            DataTable dt_otbor = DBFunctions.ReadFromDB("SELECT ColumnName FROM tableconfig WHERE tabledbname = 'credprogr' ORDER BY ColumnName");
            
            tscbFields.Items.Clear();
            tscbFields.Items.Add(" ");

            foreach (DataRow otbor_row in dt_otbor.Rows)
            {
                tscbFields.Items.Add(otbor_row["ColumnName"]);
            }
        }

        private void FillDataGrid()
        {

            dgFilter.AutoGenerateColumns = false;
            dgFilter.Columns.Clear();
            dt_filter = new DataTable();
            dt_filter.Columns.Add("ID вопроса", Type.GetType("System.Int32"));
            DataGridViewTextBoxColumn dgc = new DataGridViewTextBoxColumn();
            dgc.Name = "ID вопроса";
            dgc.DataPropertyName = "ID вопроса";
            dgc.Frozen = true;
            dgFilter.Columns.Add(dgc);
            dt_filter.Columns.Add("Текст вопроса", Type.GetType("System.String"));
            dgc = new DataGridViewTextBoxColumn();
            dgc.Name = "Текст вопроса";
            dgc.DataPropertyName = "Текст вопроса";
            dgc.Frozen = true;
            dgc.Width = dgc.Width * 4;
            dgFilter.Columns.Add(dgc);

            string ColumnDBName = (string)DBFunctions.ReadScalarFromDB("SELECT filter_name_field FROM questionary WHERE id = " + Convert.ToString(questionary_id));

            string query_text = "SELECT id, " + ColumnDBName + " AS name FROM table_credprogr";

            if (tscbFields.SelectedIndex != -1 && (string)(tscbFields.Items[tscbFields.SelectedIndex]) != " ")
            {
                query_text += " WHERE " +
                    (string)DBFunctions.ReadScalarFromDB("SELECT ColumnDBName FROM tableconfig WHERE ColumnName = '" + tscbFields.Items[tscbFields.SelectedIndex] + "'") +
                    " LIKE '%" + tstbOtbor.Text + "%'";                
            }

            DataTable dt_programs = DBFunctions.ReadFromDB(query_text);

            foreach (DataRow program_row in dt_programs.Rows)
            {
                DataColumn clm = dt_filter.Columns.Add(Convert.ToString(program_row["id"]), Type.GetType("System.Boolean"));

                DataGridViewCheckBoxColumn dg_cb_clmn = new DataGridViewCheckBoxColumn();
                dg_cb_clmn.Name = (string)program_row["name"] + " (" + Convert.ToString(program_row["id"]) + ")";
                dg_cb_clmn.DataPropertyName = Convert.ToString(program_row["id"]);
                dg_cb_clmn.TrueValue = true;
                dg_cb_clmn.FalseValue = false;                
                dgFilter.Columns.Add(dg_cb_clmn);
            }

            dgFilter.DataSource = dt_filter;

            
            DataTable dt_questions = DBFunctions.ReadFromDB("SELECT question_id,question_text FROM questionary_questions WHERE questionary_id = " + Convert.ToString(questionary_id));

            DataTable dt_filter_pairs = DBFunctions.ReadFromDB("SELECT CONCAT(question_id,'-',filter_id) FROM questionary_filter WHERE questionary_id = " + Convert.ToString(questionary_id));
            
            DataColumn[] keys = {dt_filter_pairs.Columns[0]};

            dt_filter_pairs.PrimaryKey = keys;

            foreach (DataRow q_row in dt_questions.Rows)
            {
                DataRow new_row = dt_filter.Rows.Add(new object[] { q_row["question_id"], q_row["question_text"] });
                foreach (DataColumn q_column in dt_filter.Columns)
                {
                    if (dt_filter.Columns.IndexOf(q_column) < 2)
                        continue;
                    if (dt_filter_pairs.Rows.Find(Convert.ToString(q_row["question_id"]) + "-" + q_column.ColumnName) != null)
                    {
                        new_row[q_column] = true;
                    }
                    else
                    {
                        new_row[q_column] = false;
                    }
                }
            }
            dt_filter.AcceptChanges();
        }

        private void SaveData()
        {
            if (!Auth.AuthModule.rights.questionary_editor.write)
                return;
            foreach (DataRow filter_row in dt_filter.Rows)
            {
                for (int col_num = 2; col_num < dt_filter.Columns.Count; col_num++)
                {
                    //Очистим текущие данные
                    DBFunctions.ExecuteCommand("DELETE FROM questionary_filter WHERE questionary_id = " + Convert.ToString(questionary_id) + " AND question_id = " + filter_row["ID вопроса"] + 
                        " AND filter_id = " + dt_filter.Columns[col_num].ColumnName);

                    //Занесем новые при необходимости
                    if ((bool)filter_row[col_num] == true)
                    {
                        DBFunctions.ExecuteCommand("INSERT INTO questionary_filter VALUES(" + Convert.ToString(questionary_id) + "," + dt_filter.Columns[col_num].ColumnName + "," +
                            filter_row["ID вопроса"] + ")");
                    }

                }
            }            
            dt_filter.AcceptChanges();            
        }

        private void FillTableWithValuesInCurrentColumn()
        {
            DataColumn current_column = dt_filter.Columns[dgFilter.CurrentCell.ColumnIndex];
            foreach (DataRow filter_row in dt_filter.Rows)
            {
                for (int col_num = 2; col_num < dt_filter.Columns.Count; col_num++)
                {
                    if (dt_filter.Columns[col_num] == current_column)
                    {
                        continue;
                    }

                    filter_row[dt_filter.Columns[col_num]] = filter_row[current_column];
                }
            }

            dgFilter.EndEdit();
        }

        private bool proceedWithChanges()
        {
            if (!Auth.AuthModule.rights.questionary_editor.write)
                return true;
            if (dt_filter.GetChanges() != null)
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

        private void tsbApplyOtbor_Click(object sender, EventArgs e)
        {
            if (proceedWithChanges())
                FillDataGrid();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void dgFilter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgFilter.EndEdit();
        }

        private void QuestionaryFilterEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!proceedWithChanges())
                e.Cancel = true;
        }

        private void tsbDuplicate_Click(object sender, EventArgs e)
        {
            FillTableWithValuesInCurrentColumn();
        }

        private void tsbDiscard_Click(object sender, EventArgs e)
        {
            dt_filter.RejectChanges();
        }

        private void QuestionaryFilterEditor_Load(object sender, EventArgs e)
        {
            TestRights();
        }

    }
}
