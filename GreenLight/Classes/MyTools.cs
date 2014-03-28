using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System;


namespace GreenLight
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

        public static void SetColumnOrder(DataGridView dg)
        {
            if (!DataGridViewSetting.Default.ColumnOrder.ContainsKey(dg.Name))
                return;

            List<ColumnOrderItem> columnOrder =
                DataGridViewSetting.Default.ColumnOrder[dg.Name];

            if (columnOrder != null)
            {
                var sorted = columnOrder.OrderBy(i => i.DisplayIndex);
                foreach (var item in sorted)
                {
                    dg.Columns[item.ColumnIndex].DisplayIndex = item.DisplayIndex;
                    dg.Columns[item.ColumnIndex].Visible = item.Visible;
                    dg.Columns[item.ColumnIndex].Width = item.Width;
                }
            }
        }
        //---------------------------------------------------------------------
        public static void SaveColumnOrder(DataGridView dg)
        {
            if (dg.AllowUserToOrderColumns)
            {
                List<ColumnOrderItem> columnOrder = new List<ColumnOrderItem>();
                DataGridViewColumnCollection columns = dg.Columns;
                for (int i = 0; i < columns.Count; i++)
                {
                    columnOrder.Add(new ColumnOrderItem
                    {
                        ColumnIndex = i,
                        DisplayIndex = columns[i].DisplayIndex,
                        Visible = columns[i].Visible,
                        Width = columns[i].Width
                    });
                }

                DataGridViewSetting.Default.ColumnOrder[dg.Name] = columnOrder;
                DataGridViewSetting.Default.Save();
            }
        }

    }

    internal sealed class DataGridViewSetting : ApplicationSettingsBase
    {
        private static DataGridViewSetting _defaultInstace =
            (DataGridViewSetting)ApplicationSettingsBase.Synchronized(new DataGridViewSetting());
        //---------------------------------------------------------------------
        public static DataGridViewSetting Default
        {
            get { return _defaultInstace; }
        }
        //---------------------------------------------------------------------

        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        [DefaultSettingValue("")]

        public Dictionary<string, List<ColumnOrderItem>> ColumnOrder
        {
            get { return this["ColumnOrder"] as Dictionary<string, List<ColumnOrderItem>>; }
            set { this["ColumnOrder"] = value; }
        }
    }
    //-------------------------------------------------------------------------
    [Serializable]
    public sealed class ColumnOrderItem
    {
        public int DisplayIndex { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public int ColumnIndex { get; set; }
    }   
}