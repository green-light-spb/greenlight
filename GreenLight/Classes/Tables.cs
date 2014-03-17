using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace GreenLight
{
    class EditingValue: Object
    {
        public string FieldName;
        public string FieldDBName;
        public bool isReference;
        public bool isMultiRef;
        public string RefDBName;
        public Object Value;
        public string DisplayName;

        public void SetDisplayName()
        {
            if (Value == System.DBNull.Value)
            {
                DisplayName = "";
                return;
            }
            if(!isReference)
            {
                DisplayName =  Convert.ToString(Value);
            }
            else
            {
                if (isMultiRef)
                {
                    DisplayName = "";

                    
                    DataTable dt_selected_ids = DBFunctions.ReadFromDB(@"SELECT RefName
                                                FROM ref_data_" + RefDBName + @" 
                                                WHERE LOCATE(concat('{',CAST(ID AS CHAR),'}'),'" + (string)Value + "') > 0 ORDER BY RefName");
                    
                    foreach (DataRow ref_id_row in dt_selected_ids.Rows)
                    {
                        DisplayName += (string)ref_id_row["RefName"] + ", ";
                    }

                    char[] trimchars = { ',', ' ' };
                    DisplayName = DisplayName.TrimEnd(trimchars);
      
                } 
                else
                {
                    DisplayName = Tables.GetRefName(RefDBName, (int)Value);
                }
            }            
        }
    }

    class Tables
    {
        public static string GetRefName(string ref_db_name,int id)
        {
            return (string)DBFunctions.ReadScalarFromDB("SELECT RefName FROM ref_data_" + ref_db_name + " WHERE ID = " + Convert.ToString(id));
        }

        public static DataTable GetTable(string table_name, ref DataGridView dgTableData ,ref TableStruct ts,int record_id = 0)
        {
            DataTable dt_table_data;

            DataTable dt_table_structure = DBFunctions.ReadFromDB("SELECT ColumnName,ColumnDBName,ColumnType,ColumnReference,ReferenceMultiSelect,ShowInEditor FROM tableconfig WHERE TableDBName = '" + table_name + "'");
            string query_text = "SELECT table_" + table_name + ".ID" ;
            int rows_count = 0;
            foreach (DataRow row in dt_table_structure.Rows)
            {
                query_text += ",";

                rows_count++;
                query_text += row["ColumnDBName"]; 
            }
            query_text += " FROM table_" + table_name;
            if (record_id != 0)
                query_text += " WHERE table_" + table_name + ".ID=" + Convert.ToString(record_id);
            dt_table_data = DBFunctions.ReadFromDB(query_text);

            if (dgTableData != null) 
                dgTableData.DataSource = dt_table_data;

            ts = new TableStruct();
            ts.TableName = "table_" + table_name;
            string[] p_keys = { "ID" };
            ts.p_keys = p_keys;
            ts.columns = new string[rows_count];

            int curr_ts_row = 0;
            for (int i = 0; i < dt_table_structure.Rows.Count; i++)
            {
                if ((bool)dt_table_structure.Rows[i]["ShowInEditor"] == false)
                    dgTableData.Columns[i+1].Visible = false;
                else
                    dgTableData.Columns[i+1].HeaderText = (string)dt_table_structure.Rows[i]["ColumnName"];
                /*if ( (string)dt_table_structure.Rows[i]["ColumnType"] == "Справочник")
                {
                    dgTableData.Columns[curr_dg_column - 1].Visible = false;
                    dgTableData.Columns[curr_dg_column++].HeaderText = (string)dt_table_structure.Rows[i]["ColumnName"];                
                }*/
                //if ((bool)dt_table_structure.Rows[i]["ReferenceMultiSelect"] == false)
                ts.columns[curr_ts_row++] = (string)dt_table_structure.Rows[i]["ColumnDBName"];
            }
            

            return dt_table_data;
        }

        public static DataTable GetTableWODataGrid(string table_name, ref TableStruct ts, int record_id = 0)
        {
            DataTable dt_table_data;

            DataTable dt_table_structure = DBFunctions.ReadFromDB("SELECT ColumnName,ColumnDBName,ColumnType,ColumnReference,ReferenceMultiSelect FROM tableconfig WHERE TableDBName = '" + table_name + "'");
            string query_text = "SELECT table_" + table_name + ".ID";
            int rows_count = 0;
            foreach (DataRow row in dt_table_structure.Rows)
            {
                query_text += ",";

                rows_count++;
                query_text += row["ColumnDBName"]; 
            }
            query_text += " FROM table_" + table_name;
            //query_text += join_text;
            if (record_id != 0)
                query_text += " WHERE table_" + table_name + ".ID=" + Convert.ToString(record_id);
            dt_table_data = DBFunctions.ReadFromDB(query_text);
                        

            ts = new TableStruct();
            ts.TableName = "table_" + table_name;
            string[] p_keys = { "ID" };
            ts.p_keys = p_keys;
            ts.columns = new string[rows_count];

            int curr_ts_row = 0;
            for (int i = 0; i < dt_table_structure.Rows.Count; i++)
            {                
                ts.columns[curr_ts_row++] = (string)dt_table_structure.Rows[i]["ColumnDBName"];
            }
           
            return dt_table_data;
        }

    }
}
