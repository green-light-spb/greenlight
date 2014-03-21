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
    public partial class ClauseEditor : Form
    {
        DataTable dt;
        DataTable dt_column_names;

        public ClauseEditor()
        {
            InitializeComponent();
        }

        private void TestRights()
        {
            tsbOk.Enabled = Auth.AuthModule.rights.clause_editor.write;
            tsbSave.Enabled = Auth.AuthModule.rights.clause_editor.write;
            tsbInvertUseInWhereClause.Enabled = Auth.AuthModule.rights.clause_editor.write;
        }
                
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbOk_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                DataRow row = dt.Rows.Add("table_credprogr", "table_clients", tbWhereClause.Text);
            }
            else
            {
                dt.Rows[0]["Clause"] = tbWhereClause.Text;
            };
            
            TableStruct ts = new TableStruct();
            ts.TableName = "where_clauses";
            string[] p_keys = { "TableDBName_Left", "TableDBName_Right"};
            ts.p_keys = p_keys;
            string[] columns = { "Clause" };
            ts.columns = columns;

            DBFunctions.WriteToDB(dt, ts);

            try
            {
                DBStructure.UpdateSelectorScript();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            Close();

        }

        private void ClauseEditor_Load(object sender, EventArgs e)
        {
            dt = DBFunctions.ReadFromDB("SELECT * FROM where_clauses");
            if (dt.Rows.Count > 0)
            {
                tbWhereClause.Text = (string)dt.Rows[0]["Clause"];
            }

            //Загрузим подсказку с именами столбцов
            dt_column_names = DBFunctions.ReadFromDB("SELECT TableConfigID, TableName AS 'Имя таблицы', ColumnName AS 'Имя колонки', ColumnDBName AS 'Наименование колонки в БД', ColumnType AS 'Тип', UseInWhereClause FROM tableconfig ORDER BY TableName");

            dgColumnNames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgColumnNames.DataSource = dt_column_names;

            TestRights();

        }

        private void tstbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tstbSearch.Text == "")
                dt_column_names.DefaultView.RowFilter = "";
            else
            {
                string rf_text = "";

                foreach (DataGridViewColumn clmn in dgColumnNames.Columns)
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

                dt_column_names.DefaultView.RowFilter = rf_text;
            }
        }

        private void tsbInvertUseInWhereClause_Click(object sender, EventArgs e)
        {
            DataRow curr_row = Samoyloff.Tools.FindCurrentRow(dgColumnNames);

            if (curr_row != null)
            {
                curr_row["UseInWhereClause"] = !((bool)curr_row["UseInWhereClause"]);

                Dictionary<string, object>  parameters = new Dictionary<string, object>();
                parameters.Add("id", (int)curr_row["TableConfigID"]);
                parameters.Add("use_in_clause", (bool)curr_row["UseInWhereClause"]);

                DBFunctions.ExecuteCommand("UPDATE tableconfig SET UseInWhereClause = @use_in_clause WHERE TableConfigID = @id",parameters);
            }
        }
              
        private void dgColumnNames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow curr_row = Samoyloff.Tools.FindCurrentRow(dgColumnNames);

            if (curr_row != null)
            {
                int ind = tbWhereClause.Text.IndexOf((string)curr_row["Наименование колонки в БД"]);

                if (ind > 0)
                {
                    tbWhereClause.SelectionStart = ind;
                    tbWhereClause.SelectionLength = ((string)curr_row["Наименование колонки в БД"]).Length;
                }
                else
                {
                    tbWhereClause.SelectionLength = 0;
                }
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                DataRow row = dt.Rows.Add("table_credprogr", "table_clients", tbWhereClause.Text);
            }
            else
            {
                dt.Rows[0]["Clause"] = tbWhereClause.Text;
            };

            TableStruct ts = new TableStruct();
            ts.TableName = "where_clauses";
            string[] p_keys = { "TableDBName_Left", "TableDBName_Right" };
            ts.p_keys = p_keys;
            string[] columns = { "Clause" };
            ts.columns = columns;

            DBFunctions.WriteToDB(dt, ts);

            try
            {
                DBStructure.UpdateSelectorScript();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
