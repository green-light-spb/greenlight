using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace GreenLight
{
    class TestUnit
    {
        public static void TestReflection()
        {
            Type t = typeof(Auth.Rights);
            FieldInfo[] fi = t.GetFields();

            Auth.Rights rt = new Auth.Rights();
            rt.references.read = true;

            string string_rt = rt.Serialize();

            Auth.Rights r2 = new Auth.Rights();
            r2.Deserialize("1101");

        }

        public static void TestEncryption()
        {
            string str = Encryption.Encrypt("root%Ijhrby222","Pr0gress");
            string str2 = Encryption.Decrypt(str, "Pr0gress");
        }

        public static void InitLocalParams()
        {
            //LocalParameters.SaveParameters();
            LocalParameters.LoadParameters();
            LocalParameters.EditParameters();
        }

        public static void UpdateDB()
        {
            DBStructure.UpdateDBStructure();
        }

        public static void RecreateRefStatements()
        {
            DataTable References = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceDBName FROM referencesconfig");
            foreach (DataRow row in References.Rows)
            {
                //Создадим хранимые процедуры и триггеры
                string ref_create_script = (string)DBFunctions.ReadScalarFromDB("SELECT script FROM scripts WHERE script_name = 'Reference_Create'");

                ref_create_script = ref_create_script.Replace("[RefDBName]", (string)row["ReferenceDBName"]);

                DBFunctions.ExecuteScript(ref_create_script);
            }

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
