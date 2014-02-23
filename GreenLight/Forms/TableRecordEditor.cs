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
    public partial class TableRecordEditor : Form
    {
        private string table_name;
        private string table_db_name;
        private DataRow current_row;
        Dictionary<string, Control> edit_controls;
        DataTable dt_table_structure;


        public TableRecordEditor(string tn,DataRow row)
        {
            InitializeComponent();

            table_name = tn;
            current_row = row;
            
            DataTable dt_table_db_name = DBFunctions.ReadFromDB("SELECT DISTINCT TableDBName FROM TableConfig WHERE TableName = '" + table_name + "'");
            if (dt_table_db_name.Rows.Count != 0)
                table_db_name = (string)dt_table_db_name.Rows[0]["TableDBName"];
            else
            {
                throw new Exception("Таблица не найдена в базе данных.");
            }

            dt_table_structure = DBFunctions.ReadFromDB("SELECT ColumnName,ColumnDBName,ColumnType,ColumnReference,ReferenceMultiSelect FROM tableconfig WHERE TableName = '" + table_name + "'");

            edit_controls = new Dictionary<string, Control>();

            int columns_count = 0;
            
            foreach (DataRow struct_row in dt_table_structure.Rows)
            {
                

                Label new_label = new Label();
                new_label.Text = (string)struct_row["ColumnName"];
                new_label.Left = 10;
                new_label.Height = 20;
                new_label.Width = 300;
                new_label.Top = 12 + columns_count * (new_label.Height + 5);
                

                edit_controls.Add("label_" + struct_row["ColumnDBName"], new_label);
                this.Controls.Add(edit_controls["label_" + struct_row["ColumnDBName"]]);

                if (Convert.ToString(struct_row["ColumnType"]) != "Справочник")
                {
                    TextBox new_tb = new TextBox();
                    new_tb.Tag = (string)struct_row["ColumnDBName"];
                    new_tb.Left = 310;
                    new_tb.Top = 10 + columns_count * (new_tb.Height + 5);
                    new_tb.Width = this.ClientRectangle.Width - 315;

                    new_tb.TextChanged += new System.EventHandler(this.TextBox_TextChange);

                    if (current_row[(string)new_tb.Tag] != DBNull.Value)
                        new_tb.Text = Convert.ToString(current_row[(string)new_tb.Tag]);

                    edit_controls.Add((string)struct_row["ColumnDBName"], new_tb);
                    this.Controls.Add(edit_controls[(string)struct_row["ColumnDBName"]]);

                    
                }
                else 
                {
                    TextBox new_tb = new TextBox();

                    new_tb.Tag = (string)struct_row["ColumnDBName"];
                    new_tb.Left = 310;
                    new_tb.Top = 10 + columns_count * (new_tb.Height + 5);
                    new_tb.Width = this.ClientRectangle.Width - 315 - 2 - new_tb.Height;
                    new_tb.ReadOnly = true;

                    if (current_row[(string)new_tb.Tag] != DBNull.Value)
                    {
                        if(!(bool)struct_row["ReferenceMultiSelect"])
                            new_tb.Text = Tables.GetRefName((string)struct_row["ColumnReference"], (int)current_row[(string)new_tb.Tag]);
                    }

                    edit_controls.Add((string)struct_row["ColumnDBName"], new_tb);
                    this.Controls.Add(edit_controls[(string)struct_row["ColumnDBName"]]);

                    Button new_btn = new Button();
                    new_btn.Text = "<";
                    new_btn.Top = 10 + columns_count * (new_tb.Height + 5);
                    new_btn.Width = new_tb.Height;
                    new_btn.Height = new_tb.Height;
                    new_btn.Left = new_tb.Left + new_tb.Width + 1;
                    new_btn.Tag = new_tb;

                    new_btn.Click += new System.EventHandler(this.ReferenceSelect_Click);

                    edit_controls.Add("btn_" + (string)struct_row["ColumnDBName"], new_btn);
                    this.Controls.Add(edit_controls["btn_" + (string)struct_row["ColumnDBName"]]);
                }

                columns_count += 1;
            }

            if(columns_count < 10) 
                this.ClientSize = new System.Drawing.Size( this.ClientRectangle.Width , 22 + columns_count * 30);
            else
                this.ClientSize = new System.Drawing.Size(this.ClientRectangle.Width, 22 + 10 * 30);
        }

        string GetColumnReference(string column_db_name)
        {
            DataRow[] dt_column = dt_table_structure.Select("ColumnDBName = '" + column_db_name + "'");
            return (string)dt_column[0]["ColumnReference"];
        }
         
        private void ReferenceSelect_Click(object sender, EventArgs e)
        {
            HierarchicalRefEdit hre = new HierarchicalRefEdit();
            Button snd = (Button)sender;
            TextBox tb = (TextBox)snd.Tag;
            hre.reference_db_name = GetColumnReference((string)tb.Tag);
            hre.select_mode = true;
            DataRow[] curr_struct_row = dt_table_structure.Select("ColumnDBName = '" + (string)tb.Tag +"'");
            hre.select_mode_multiselect = (bool)curr_struct_row[0]["ReferenceMultiSelect"];

            if (current_row[(string)tb.Tag] != System.DBNull.Value && !hre.select_mode_multiselect)
                hre.selected_ids.Add((int)current_row[(string)tb.Tag]);

            if (hre.select_mode_multiselect && current_row[(string)tb.Tag] != System.DBNull.Value)
            {                
                //Поставим галки у выбранных
                DataTable dt_selected_ids = DBFunctions.ReadFromDB(@"SELECT ID AS RefID
                                                FROM ref_data_" + (string)curr_struct_row[0]["ColumnReference"] + @" 
                                                WHERE LOCATE(concat('{',CAST(ID AS CHAR),'}'),'" + (string)current_row[(string)tb.Tag] + "') > 0");
                foreach (DataRow ref_id_row in dt_selected_ids.Rows)
                {
                    hre.selected_ids.Add((int)ref_id_row["RefID"]);
                }
            }
            
            if (hre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!hre.select_mode_multiselect)
                {
                    tb.Text = Tables.GetRefName(hre.reference_db_name, hre.selected_ids[0]);
                    current_row[(string)tb.Tag] = hre.selected_ids[0];
                    //current_row[(string)tb.Tag + "_Name"] = tb.Text;
                }
                else
                {
                    string result_ids = "";
                    foreach (int sel_id in hre.selected_ids)
                    {
                        result_ids += "{" + Convert.ToString(sel_id) + "}";
                    }

                    current_row[(string)tb.Tag] = result_ids;
                    
                }
            }
        }

        private void TextBox_TextChange(object sender, EventArgs e)
        {
            try
            {
                current_row[(string)((TextBox)sender).Tag] = ((TextBox)sender).Text;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Вы ввели неверное значение для этого поля.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void TableRecordEditor_Resize(object sender, EventArgs e)
        {
            if (edit_controls == null)
                return;
            foreach (KeyValuePair<string,Control> cntrl in edit_controls)
            {
                if (cntrl.Value is TextBox)
                {
                    if(edit_controls.Keys.Contains("btn_" + cntrl.Key))
                    {
                        Control btn = edit_controls["btn_" + cntrl.Key];
                        cntrl.Value.Width = this.ClientRectangle.Width - 310 - 2 - cntrl.Value.Height;
                        btn.Left = cntrl.Value.Left + cntrl.Value.Width + 1;
                    }
                    else
                    {
                        cntrl.Value.Width = this.ClientRectangle.Width - 315;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
