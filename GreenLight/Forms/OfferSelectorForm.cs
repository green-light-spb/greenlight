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
            dt_clients = DBFunctions.ReadFromDB("SELECT id,fio_zaem FROM table_clients");

            dgClients.DataSource = dt_clients;

            dgClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            dgClients.Columns[0].HeaderText = "ID";
            dgClients.Columns[1].HeaderText = "ФИО";
        }

        private DataTable SelectOffers(int ClientID)
        {
            OfferSelector os = new OfferSelector(ClientID);

            return os.SelectOffers();
        }

        public OfferSelectorForm()
        {
            InitializeComponent();
        }

        private void OfferSelectorForm_Load(object sender, EventArgs e)
        {
            dgOffers.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgOffers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            FillClients();
        }

        private void dgClients_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dt_offers != null)
                GreenLight.Tools.SaveColumnOrder(dgOffers);

            DataRow current_client_row = Tools.FindCurrentRow(dgClients);

            if (current_client_row == null)
            {
                dt_offers = null;
                return;
            }

            client_id = Convert.ToInt32((int)current_client_row["id"]);
            dt_offers = SelectOffers(client_id);

            if (dt_offers == null)
            {
                Close();
                return;
            }

            dgOffers.DataSource = dt_offers;

            GreenLight.Tools.SetColumnOrder(dgOffers);                     
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

        private void tsbUniversalQuestionary_Click(object sender, EventArgs e)
        {
            UniversalQuestionaryOfferSelect uqos = new UniversalQuestionaryOfferSelect(client_id, dt_offers);
            uqos.ShowDialog();
        }

        private void OfferSelectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dt_offers != null)
                GreenLight.Tools.SaveColumnOrder(dgOffers);
        }

        private void dgOffers_RowHeightInfoNeeded(object sender, DataGridViewRowHeightInfoNeededEventArgs e)
        {
            e.Height = 60;
        }


    }
}
