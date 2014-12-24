using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GreenLight.Forms
{
    public partial class ClauseTest : Form
    {
        DataTable dt_clients;
        DataTable dt_credprogr;
        DataTable dt_clauses;

        Dictionary<string, object> parameters;

        List<string> clauses;
        

        public ClauseTest()
        {
            InitializeComponent();
        }

        private void ClauseTest_Load(object sender, EventArgs e)
        {
            parameters = new Dictionary<string, object>();
            parameters.Add("client_id", 0);
            parameters.Add("credprogr_id", 0);

            dt_clauses = new DataTable();

            dt_clauses.Columns.Add("Условие", typeof(string));

            string clause_text = (string)DBFunctions.ReadScalarFromDB("SELECT Clause FROM where_clauses LIMIT 1");

            //Заменим макросы в условиях
            DataTable dtMacros = DBFunctions.ReadFromDB("SELECT * FROM macros");

            bool macros_found = true;

            while (macros_found)
            {
                macros_found = false;
                foreach (DataRow macro in dtMacros.Rows)
                {
                    if (clause_text.IndexOf("[" + (string)macro["name"] + "]") != -1)
                    {
                        macros_found = true;
                        clause_text = clause_text.Replace("[" + (string)macro["name"] + "]", (string)macro["macro"]);
                    }
                }
            }
            
            string[] clauses_to_parse = clause_text.Split(Environment.NewLine[0]);

            char[] trim = { 'A', 'N', 'D' };

            clauses = new System.Collections.Generic.List<string>();

            for (int i = 0; i < clauses_to_parse.Length; i++)
            {
                clauses_to_parse[i] = clauses_to_parse[i].Trim();

                if (clauses_to_parse[i].Substring(0, 1) == "@")
                    clauses_to_parse[i] = clauses_to_parse[i].Replace("@", "");
                else
                    continue;

                if (clauses_to_parse[i].Substring(0, 3) == "AND")
                    clauses_to_parse[i] = clauses_to_parse[i].TrimStart(trim);

                if (clauses_to_parse[i].Substring(clauses_to_parse[i].Length - 3, 3) == "AND")
                    clauses_to_parse[i] = clauses_to_parse[i].TrimEnd(trim);

                clauses.Add(clauses_to_parse[i]);

                DataRow new_row = dt_clauses.NewRow();

                new_row["Условие"] = clauses_to_parse[i];

                dt_clauses.Rows.Add(new_row);
            }

            dgClauses.DataSource = dt_clauses;

            dt_clients = DBFunctions.ReadFromDB("SELECT ID, FIO_zaem AS FIO FROM table_clients");
            dgClients.DataSource = dt_clients;

            dt_credprogr = DBFunctions.ReadFromDB("SELECT ID, Programma, Bank_LK FROM table_credprogr");
            dgCredProgr.DataSource = dt_credprogr;
                        
            this.dgClients.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgClients_CellEnter);
            this.dgCredProgr.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCredProgr_CellEnter);            
        }

        private void dgClients_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataRow curr_row = GreenLight.Tools.FindCurrentRow(dgClients);
            parameters["client_id"] = curr_row["ID"];
            DoClauseTest();
        }

        private void dgCredProgr_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataRow curr_row = GreenLight.Tools.FindCurrentRow(dgCredProgr);
            parameters["credprogr_id"] = curr_row["ID"];
            DoClauseTest();
        }

        private void DoClauseTest()
        {
            for(int i = 0 ; i < clauses.Count; i++)
            {
                DataTable test_result =
                    DBFunctions.ReadFromDB("SELECT Programma FROM (SELECT * FROM table_clients WHERE id = @client_id) AS Cl LEFT JOIN (SELECT * FROM table_credprogr WHERE id = @credprogr_id) AS Cr ON true WHERE " + clauses[i], parameters);

                if (test_result.Rows.Count == 0)
                {
                    dgClauses[0, i].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgClauses[0, i].Style.ForeColor = Color.Green;
                }
            }

            dgClauses.ClearSelection();
            
        }
    }
}
