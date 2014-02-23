using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace GreenLight
{
    class TestUnit
    {
        

        public static void UpdateDB()
        {
            DBStructure.UpdateDBStructure();
        }

        public static void UpdateOfferSelector()
        {
            DBStructure.UpdateSelectorScript();
        }

        public static void SelectOffers()
        {
            OfferSelector os = new OfferSelector(6);

            DataTable dt = os.SelectOffers();
        }

        public static void ExecuteScriptFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "Database=addr;Data Source=192.168.0.90;Port=3306;User Id=root;Password=Ijhrby22";
            conn.Open();
            MySqlTransaction tran = conn.BeginTransaction();

            string curr_script = "";

            while (!sr.EndOfStream)
            {
                string curr_str = sr.ReadLine();
                if (!curr_str.Contains(";"))
                {
                    curr_script += curr_str + Environment.NewLine;
                }
                else
                {
                    curr_script += curr_str;
                    MySqlScript script = new MySqlScript(conn, curr_script);
                    script.Execute();
                    curr_script = "";
                }
            }
        }

        public static void ReorganizeMultiref()
        {

            DataTable dt_multirefcolumns = DBFunctions.ReadFromDB("SELECT TableDBName,ColumnDBName FROM TableConfig WHERE ReferenceMultiSelect = 1");

            DBFunctions.ExecuteCommand("SET group_concat_max_len = 32000");

            foreach (DataRow row_multirefcolumn in dt_multirefcolumns.Rows)
            {
                DataTable dt_values = DBFunctions.ReadFromDB(
                @"SELECT TableID, GROUP_CONCAT(CONCAT('{',CONVERT(RefID, CHAR),'}') SEPARATOR '') AS RefID 
                FROM multiref_"+row_multirefcolumn["TableDBName"]+"_"+row_multirefcolumn["ColumnDBName"] + @"
                GROUP BY TableID");

                foreach (DataRow row_value in dt_values.Rows)
                {
                    DBFunctions.ExecuteCommand(@"UPDATE table_" + row_multirefcolumn["TableDBName"] + @" 
                    SET " + row_multirefcolumn["ColumnDBName"] + " = '"+ row_value["RefID"] + @"'
                    WHERE id = " + Convert.ToString(row_value["TableID"])); 
                }
            }

        }
    }
}
