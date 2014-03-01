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
            dt_column_names = DBFunctions.ReadFromDB("SELECT TableName AS 'Имя таблицы', ColumnName AS 'Имя колонки', ColumnDBName AS 'Наименование колонки в БД', ColumnType AS 'Тип' FROM tableconfig ORDER BY TableName");

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
    }
}
