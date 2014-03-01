using System.Windows.Forms;
using System.Data;

namespace Samoyloff
{
    class Tools
    {
        public enum ProceedWithChangesAnswers
        {
            SaveAndProceed,
            DontSaveAndProceed,
            Cancel
        }

        public static ProceedWithChangesAnswers ProceedWithChanges(DataTable dt)
        {
            if (dt == null)
                return ProceedWithChangesAnswers.DontSaveAndProceed;

            if (dt.GetChanges() != null)
            {
                DialogResult res = System.Windows.Forms.MessageBox.Show("Таблице есть изменения. Сохранить?", "Вопрос", System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    return ProceedWithChangesAnswers.SaveAndProceed;
                }
                else if (res == System.Windows.Forms.DialogResult.No)
                {
                    return ProceedWithChangesAnswers.DontSaveAndProceed;
                }
                else
                {
                    return ProceedWithChangesAnswers.Cancel;
                }
            }
            else
            {
                return ProceedWithChangesAnswers.DontSaveAndProceed;
            }
        }

        public static DataRow FindCurrentRow(DataGridView dgv)
        {
            CurrencyManager cManager =
                dgv.BindingContext[dgv.DataSource, dgv.DataMember]
                     as CurrencyManager;
            if (cManager == null || cManager.Count == 0)
                return null;

            DataRowView drv = cManager.Current as DataRowView;
            return drv.Row;
        } 
    }
}