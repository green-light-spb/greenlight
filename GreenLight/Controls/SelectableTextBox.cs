using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight.Controls
{

    public partial class SelectableTextBox : UserControl, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        EditingValue ev;
        

        public object Value
        {
            get { return ev; }
            set 
            {
                if (value is EditingValue)
                {
                    ev = (EditingValue)value;
                    tbText.Text = ev.DisplayName;                    
                }
                else
                {
                    throw new Exception("Ошибка в данных таблицы");
                }            
            }
        }

        public SelectableTextBox()
        {
            this.TabStop = false;
            InitializeComponent();
            
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return ev;
            }
            set
            {
                if (value is String)
                {
                    try
                    {
                        // This will throw an exception of the string is  
                        // null, empty, or not in the format of a date. 
                        this.Value = (String)value;
                    }
                    catch
                    {
                        // In the case of an exception, just use the  
                        // default value so we're not left with a null 
                        // value. 
                        this.Value = "";
                    }
                }
            }
        }

        // Implements the  
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method. 
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the  
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method. 
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex  
        // property. 
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey  
        // method. 
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed. 
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit  
        // method. 
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (ev.isReference)
            {
                tbText.Width = this.Width - 20;
                btnSelect.Visible = true;
                tbText.ReadOnly = true;
            }
            else
            {
                tbText.Width = this.Width;
                btnSelect.Visible = false;
                tbText.ReadOnly = false;
            }

            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl 
        // .RepositionEditingControlOnValueChange property. 
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl 
        // .EditingControlDataGridView property. 
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl 
        // .EditingControlValueChanged property. 
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl 
        // .EditingPanelCursor property. 
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            if (ev.isReference)
                return;
            if (ev.Value == tbText.Text || (ev.Value == System.DBNull.Value && tbText.Text == ""))
                return;
            ev.DisplayName = tbText.Text;
            ev.Value = tbText.Text;
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            HierarchicalRefEdit hre = new HierarchicalRefEdit();
            hre.reference_db_name = ev.RefDBName;
            hre.select_mode = true;

            hre.select_mode_multiselect = ev.isMultiRef;

            if (ev.Value != System.DBNull.Value && !hre.select_mode_multiselect)
                hre.selected_ids.Add((int)ev.Value);

            if (ev.Value != System.DBNull.Value && hre.select_mode_multiselect)
            {
                //Поставим галки у выбранных
                DataTable dt_selected_ids = DBFunctions.ReadFromDB(@"SELECT ID AS RefID
                                                FROM ref_data_" + ev.RefDBName + @" 
                                                WHERE LOCATE(concat('{',CAST(ID AS CHAR),'}'),'" + (string)ev.Value+ "') > 0");
                foreach (DataRow ref_id_row in dt_selected_ids.Rows)
                {
                    hre.selected_ids.Add((int)ref_id_row["RefID"]);
                }
            }

            if (hre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!hre.select_mode_multiselect)
                {
                    ev.Value = hre.selected_ids[0];
                    ev.SetDisplayName();
                    tbText.Text = ev.DisplayName;
                }
                else
                {
                    string result_ids = "";
                    foreach (int sel_id in hre.selected_ids)
                    {
                        result_ids += "{" + Convert.ToString(sel_id) + "}";
                    }

                    ev.Value = result_ids;
                    ev.SetDisplayName();
                    tbText.Text = ev.DisplayName;
                }
            }
            
        }

    }
}
