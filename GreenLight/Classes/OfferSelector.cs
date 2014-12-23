using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GreenLight
{
    class OfferSelector
    {
        int ClientID;

        public OfferSelector()
        {
        }

        public OfferSelector(int ID)
        {
            ClientID = ID;
        }

        public DataTable SelectOffers()
        {

            string query_text = (string)DBFunctions.ReadScalarFromDB("SELECT script FROM scripts WHERE script_name = 'OfferSelect'");

            query_text = query_text.Replace("[ClientID]", Convert.ToString(ClientID));

            DataTable dt;
            try
            {
                dt = DBFunctions.ReadFromDB(query_text);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка в условии. " + e.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            return dt;
        }
    }
}
