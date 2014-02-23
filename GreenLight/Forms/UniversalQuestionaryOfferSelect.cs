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
    public partial class UniversalQuestionaryOfferSelect : Form
    {
        DataTable dt_offers;
        int client_id;
        public UniversalQuestionaryOfferSelect(int c_id, DataTable dt)
        {
            InitializeComponent();

            client_id = c_id;

            dt_offers = new DataTable();

            dt_offers.Columns.Add("Selected",Type.GetType("System.Boolean"));
            dt_offers.Columns.Add("ID", Type.GetType("System.Int32"));
            dt_offers.Columns.Add("Bank_LK", Type.GetType("System.String"));
            dt_offers.Columns.Add("Programma", Type.GetType("System.String"));

            foreach (DataRow row in dt.Rows)
            {
                DataRow newrow = dt_offers.Rows.Add();
                newrow["Selected"] = false;
                newrow["ID"] = row["ID"];
                newrow["Bank_LK"] = row["Банк,ЛК(ф)(ю)"];
                newrow["Programma"] = row["Программа(ф)(ю)"];                
            }
        }

        private void UniversalQuestionaryOfferSelect_Load(object sender, EventArgs e)
        {
            dgOffers.AutoGenerateColumns = false;
            dgOffers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgOffers.Columns["Selected"].DataPropertyName = "Selected";
            dgOffers.Columns["Selected"].FillWeight = 50;
            dgOffers.Columns["Bank_LK"].DataPropertyName = "Bank_LK";
            dgOffers.Columns["Programma"].DataPropertyName = "Programma";

            dgOffers.DataSource = dt_offers;
        }

        private void tsbOk_Click(object sender, EventArgs e)
        {
            dgOffers.EndEdit();
            Validate();

            DataRow [] selected_rows = dt_offers.Select("selected = 1");

            int [] ids = new int[selected_rows.Length];

            for(int i=0;i<selected_rows.Length;i++)
                ids[i] = (int)(selected_rows[i]["ID"]);

            

            Questionary qe = new Questionary(client_id, "Универсальная анкета", ids);
            qe.ShowDialog();

            Close();
            
        }
    }
}
