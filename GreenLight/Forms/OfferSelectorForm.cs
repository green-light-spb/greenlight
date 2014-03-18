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
    public partial class OfferSelectorForm : Form
    {
        DataTable dt_clients;
        DataTable dt_offers;
        int client_id;
        TableStruct ts;

        private void FillClients()
        {
            ts = new TableStruct();
            dt_clients = Tables.GetTable("Clients", ref dgClients, ref ts);
        }

        private string CorrectMultirefString(string in_str)
        {
            string ref_name = in_str.Substring(in_str.IndexOf('[') + 1, in_str.IndexOf(']') - in_str.IndexOf('[') - 1);

            try
            {
                return (string)DBFunctions.ReadScalarFromDB(@"SELECT GROUP_CONCAT(RefName SEPARATOR '" + Environment.NewLine + @"')
                                            FROM ref_data_" + ref_name + @" 
                                            WHERE LOCATE(concat('{',CAST(ID AS CHAR),'}'),'" + in_str + "') > 0");
            }
            catch (Exception)
            {
                return "";
            }
        }

        private DataTable SelectOffers(int ClientID)
        {
            OfferSelector os = new OfferSelector(ClientID);

            DataTable dt = os.SelectOffers();

            if (dt == null)
            {
               return dt;
            }
            //Преобразуем данные о мультирефах в человеческий вид
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (row[column] is string && ((string)row[column]).IndexOf("%multise") != -1)
                    {                        
                        row[column] = CorrectMultirefString((string)row[column]);
                    }
                }
            }

            return dt;
        }

        public OfferSelectorForm()
        {
            InitializeComponent();
        }

        private void OfferSelectorForm_Load(object sender, EventArgs e)
        {
            dgOffers.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgOffers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            FillClients();
        }

        private void dgClients_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            client_id = Convert.ToInt32(dgClients[0, e.RowIndex].Value);
            dt_offers = SelectOffers(client_id);

            if (dt_offers == null)
            {
                Close();
                return;
            }

            dgOffers.DataSource = dt_offers;
        }

        private void tstbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tstbSearch.Text == "")
                dt_clients.DefaultView.RowFilter = "";
            else
            {
                string rf_text = "";

                foreach (DataGridViewColumn clmn in dgClients.Columns)
                {
                    if (!clmn.Visible)
                        continue;

                    if (clmn.ValueType == typeof(String))
                    {
                        if (rf_text != "")
                            rf_text += " OR ";
                        rf_text += clmn.DataPropertyName + " LIKE '%" + tstbSearch.Text + "%'";
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(tstbSearch.Text);
                            if (rf_text != "")
                                rf_text += " OR ";
                            rf_text += clmn.DataPropertyName + " = '" + tstbSearch.Text + "'";
                        }
                        catch (Exception) { };
                    }
                }

                dt_clients.DefaultView.RowFilter = rf_text;
            }
        }

        private void dgOffers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            /*TextBox TB = (TextBox)e.Control;
            TB.Multiline = true;*/
        }

        private void tsbUniversalQuestionary_Click(object sender, EventArgs e)
        {
            UniversalQuestionaryOfferSelect uqos = new UniversalQuestionaryOfferSelect(client_id, dt_offers);
            uqos.ShowDialog();
        }


    }
}
