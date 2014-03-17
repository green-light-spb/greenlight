using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace GreenLight.Controls
{    
    public class SelectableTextboxColumn : DataGridViewColumn
    {
        public SelectableTextboxColumn()
            : base(new SelectableTextboxCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(SelectableTextBox)))
                {
                    throw new InvalidCastException("Must be a Selectable cell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class SelectableTextboxCell : DataGridViewTextBoxCell
    {

        public SelectableTextboxCell()
            : base()
        {           
        }

        public override Type FormattedValueType { get { return typeof(EditingValue); } }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value. 
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            SelectableTextBox ctl =
                DataGridView.EditingControl as SelectableTextBox;
            // Use the default row value when Value property is null. 
            if (this.Value == null || this.Value == System.DBNull.Value)
            {
                ctl.Value = this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = this.Value;
            }
        }

        protected override Object GetFormattedValue(
        Object value,
        int rowIndex,
        ref DataGridViewCellStyle cellStyle,
        System.ComponentModel.TypeConverter valueTypeConverter,
        System.ComponentModel.TypeConverter formattedValueTypeConverter,
        DataGridViewDataErrorContexts context)
        {
            if (value is EditingValue)
            {
                return ((EditingValue)value).DisplayName;
            }
            else
            {                
                throw new Exception("Ошибка в данных таблицы");
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(SelectableTextBox);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(object);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return System.DBNull.Value;
            }
        }
    }


}
