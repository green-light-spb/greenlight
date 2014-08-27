using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GreenLight
{
    public partial class HierarchicalRefEdit : Form
    {
        public string reference_db_name;
        public bool select_mode = false;
        public bool select_mode_multiselect = false;
        DataTable reference_list;
        Dictionary<string,Control> edit_controls;
        long current_elem_id;
        DataTable dt_elem;
        TableStruct ref_table_struct;
        TreeNode new_node;
        public List<int> selected_ids;
        //Костылёк. Убираем селект из treeview, если ничего не выбрано
        private bool after_load = true;

        public HierarchicalRefEdit()
        {
            InitializeComponent();
            selected_ids = new List<int>();
        }

        private void TestRights()
        {
            tsbAdd.Visible = Auth.AuthModule.rights.references.write && !select_mode;
            tsbDelete.Visible = Auth.AuthModule.rights.references.write && !select_mode;
            tsbSave.Visible = Auth.AuthModule.rights.references.write && !select_mode;

            if(Auth.AuthModule.rights.references.write && !select_mode)
                this.tvReference.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvReference_ItemDrag);
            
        }

        #region Обработчики событий

        private void HierarchicalRefEdit_Load(object sender, EventArgs e)
        {
            //Получим список справочников
            if (!select_mode)
            {
                reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceName,ReferenceDBName,Hierarchycal FROM referencesconfig ORDER BY ReferenceName");
                tsbSelect.Visible = false;
            }
            else
            {
                reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceName,ReferenceDBName,Hierarchycal FROM referencesconfig WHERE ReferenceDBName = '" + reference_db_name + "'");
                cbCurrentReference.Visible = false;
                tsbAdd.Visible = tsbDelete.Visible = tsbSave.Visible = false;
                if (select_mode_multiselect)
                    tvReference.CheckBoxes = true;
            }
            foreach (DataRow row in reference_list.Rows)
            {
                cbCurrentReference.Items.Add((string)row["ReferenceName"]);
            }
            cbCurrentReference.SelectedIndex = 0;

            TestRights();
        }

        //Смена справочника
        private void cbCurrentReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "Справочник: " + cbCurrentReference.SelectedItem;
            reference_db_name = (string)reference_list.Rows[cbCurrentReference.SelectedIndex]["ReferenceDBName"];
            CreateEditControls();
            LoadData();
            
            tsbSave.Enabled = false;
        }

        //Ресайзим текстбоксы
        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            if (edit_controls == null)
                return;
            foreach (Control cntrl in edit_controls.Values)
            {
                if (cntrl is TextBox)
                {
                    cntrl.Width = splitContainer.Panel2.Width - 160;
                }
            }
        }

        //Смена элемента справочника
        private void tvReference_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (after_load)
            {
                after_load = false;
                if (select_mode && selected_ids.Count == 0 && !select_mode_multiselect)
                {
                    tvReference.SelectedNode = null;
                    tsbSelect.Enabled = false;
                    return;
                }
            }

            if (e.Node.Tag != null)
            {
                current_elem_id = (int)e.Node.Tag;
                FillEditForm();
                tsbSelect.Enabled = true;
            }
            if (new_node != null && e.Node != new_node)
            {
                new_node.Remove();
                new_node = null;
            }
            tsbSave.Enabled = true;
        }
        
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveElement();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            AddElement();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            DeleteElement();
        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            if (tvReference.SelectedNode == null)
                return;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            if (!select_mode_multiselect)
            {
                selected_ids.Clear();
                selected_ids.Add((int)tvReference.SelectedNode.Tag);
            }
            Close();
        }

        private void tvReference_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (select_mode)
                tsbSelect_Click(sender, e);

        }

        #endregion Обработчики событий

        #region Drag&Drop в TreeView + Общая работа с TreeView

        private TreeNode GetHoveringNode(int screen_x, int screen_y, bool client_coords = false)
        {
            Point pt;
            if(!client_coords)
                pt = tvReference.PointToClient(new Point(screen_x, screen_y));
            else
                pt = new Point(screen_x, screen_y);
            TreeViewHitTestInfo hitInfo = tvReference.HitTest(pt);
            return hitInfo.Node;
        }

        private void tvReference_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tvReference.DoDragDrop(e.Item, DragDropEffects.All);
        }

        private void tvReference_DragOver(object sender, DragEventArgs e)
        {
            TreeNode hoveringNode = GetHoveringNode(e.X, e.Y);
            TreeNode draggingNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (hoveringNode == null)
            {
                e.Effect = DragDropEffects.Move;
                tvReference.SelectedNode = null;
            }
            else
                if (hoveringNode != draggingNode && draggingNode != hoveringNode.Parent)
                {
                    e.Effect = DragDropEffects.Move;
                    hoveringNode.TreeView.SelectedNode = hoveringNode;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
        }

        private void tvReference_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                TreeNode hoveringNode = GetHoveringNode(e.X, e.Y);
                if (hoveringNode != null)
                {
                    TreeNode draggingNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
                    if (draggingNode != null)
                    {
                        /*if (draggingNode.Level == hoveringNode.Level)
                        {
                            if (draggingNode.Level != 0)
                            {
                                draggingNode.Remove();
                                hoveringNode.Nodes.Insert(hoveringNode.Index + 1, draggingNode);
                            }
                            if (draggingNode.Level == 0)
                            {
                                
                            }


                        }*/
                        if (!ContainsNode(draggingNode, hoveringNode))
                        {

                            draggingNode.Remove();
                            hoveringNode.Nodes.Insert(0, draggingNode);
                            DBFunctions.ExecuteCommand("UPDATE ref_data_" + reference_db_name + " SET ParentID = " + Convert.ToString(hoveringNode.Tag) + " WHERE ID = " + Convert.ToString(draggingNode.Tag) + ";");
                        }
                    }
                }
                else
                {
                    TreeNode draggingNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
                    if (draggingNode != null)
                    {
                        draggingNode.Remove();
                        tvReference.Nodes.Add(draggingNode);
                        DBFunctions.ExecuteCommand("UPDATE ref_data_" + reference_db_name + " SET ParentID ='0'  WHERE ID = " + Convert.ToString(draggingNode.Tag) + ";");
                    }
                }

            }
        }


        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {            
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;
            return ContainsNode(node1, node2.Parent);
        }

        //Поиск по тегу
        public static TreeNode FindTag(TreeNodeCollection tcol, object tag)
        {
            foreach (TreeNode tn in tcol)
            {
                if ((int)tn.Tag == (int)tag)
                    return tn;

                TreeNode res = FindTag(tn.Nodes, tag);
                if (res != null)
                    return res;
            }
            return null;
        }

        private void tvReference_MouseDown(object sender, MouseEventArgs e)
        {
            if (!select_mode)
            {
                if (GetHoveringNode(e.X, e.Y, true) == null)
                {
                    tvReference.SelectedNode = null;
                    tsbSave.Enabled = false;
                }
            }
        }

        private void SetCheckWithChildren(TreeNode node, bool CheckStatus)
        {
            node.Checked = CheckStatus;
            foreach (TreeNode child in node.Nodes)
            {
                SetCheckWithChildren(child, CheckStatus);
            }
        }

        private void tvReference_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //Отредактируем selected_ids
            int id = (int)e.Node.Tag;
            if (selected_ids.Contains(id) && !e.Node.Checked)
                selected_ids.Remove(id);

            if (!selected_ids.Contains(id) && e.Node.Checked)
                selected_ids.Add(id);

            //Почекаем или наоборот снимем флаг со всех подчиненных
            foreach (TreeNode child in e.Node.Nodes)
            {
                child.Checked = e.Node.Checked;
            }
        } 

        #endregion Drag&Drop в TreeView

        //Заменено на более производительный способ
        /*void LoadNode(TreeNodeCollection nodes, int ID)
        {
            DataTable childrens_dt = DBFunctions.ReadFromDB("SELECT ID,RefName FROM ref_data_"+reference_db_name+" INNER JOIN ref_hierarchy_"+reference_db_name+" ON ID = ElemID AND ref_hierarchy_"+reference_db_name+".Level = 1 AND ref_hierarchy_"+reference_db_name+".ParentID = " + Convert.ToString(ID));

            foreach (DataRow row in childrens_dt.Rows)
            { 
                TreeNode new_node = nodes.Add(Convert.ToString(row["RefName"]));
                new_node.Tag = row["ID"];
                LoadNode(new_node.Nodes,Convert.ToInt32(row["ID"]));
            }
        }*/

        void LoadData()
        {
            tvReference.Nodes.Clear();
            /*LoadNode(tvReference.Nodes, 0);*/

            DataTable dt_elements = DBFunctions.ReadFromDB(@"
                SELECT abs_levels.ElemID,Absolute_Level,parents.ParentID,RefName FROM
                (SELECT ElemID,`Level` AS Absolute_Level 
                FROM ref_hierarchy_"+reference_db_name+@" WHERE ParentID = 0) AS abs_levels
                LEFT JOIN 
                (SELECT ElemID,`ParentID` 
                FROM ref_hierarchy_" + reference_db_name + @" WHERE `Level` = 1) AS parents
                ON abs_levels.ElemID = parents.ElemID 
                LEFT JOIN ref_data_" + reference_db_name + @" ON abs_levels.ElemID = ref_data_" + reference_db_name + @".ID
                ORDER BY Absolute_Level,ParentID,RefName");

            int current_parent_id = 0;
            TreeNode current_parent_node = null;

            foreach (DataRow row in dt_elements.Rows)
            {
                if (current_parent_id != (int)row["ParentID"])
                {
                    current_parent_id = (int)row["ParentID"];
                    current_parent_node = FindTag(tvReference.Nodes, current_parent_id);
                }

                TreeNode new_node;
                if(current_parent_id == 0)
                    new_node = tvReference.Nodes.Add(Convert.ToString(row["RefName"]));
                else
                    new_node = current_parent_node.Nodes.Add(Convert.ToString(row["RefName"]));

                new_node.Tag = (int)row["ElemID"];

            }

            if (select_mode && selected_ids.Count > 0 && !select_mode_multiselect)
                tvReference.SelectedNode = FindTag(tvReference.Nodes, selected_ids[0]);            

            if (select_mode_multiselect)
            {
                List<int> ids_for_remove = new List<int>() ;
                tvReference.CheckBoxes = true;
                foreach (int sel_id in selected_ids)
                {
                    TreeNode found_node = FindTag(tvReference.Nodes, sel_id);
                    if (found_node != null)
                        found_node.Checked = true;
                    else
                        ids_for_remove.Add(sel_id);
                };

                foreach(int sel_id in ids_for_remove)
                {
                    selected_ids.Remove(sel_id);
                };


                //Теперь можно добавить обработчик
                this.tvReference.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvReference_AfterCheck);
            }
        }

        void FillEditForm()
        {
            dt_elem = DBFunctions.ReadFromDB("SELECT * FROM `ref_data_" + reference_db_name + "` WHERE ID = " + Convert.ToString(current_elem_id));
            if (dt_elem.Rows.Count != 1)
                return;
            foreach (DataColumn dc in dt_elem.Columns)
            {
                try
                {
                    edit_controls[dc.ColumnName].Text = Convert.ToString(dt_elem.Rows[0][dc]);
                }
                catch (Exception) { };
            }
        }

        void SaveElement()
        {
            if (tvReference.SelectedNode == null)
                return;

            foreach (DataColumn dc in dt_elem.Columns)
            {
                try
                {
                    if(dc.DataType.Name == "String")
                        dt_elem.Rows[0][dc] = edit_controls[dc.ColumnName].Text;
                    else if (dc.DataType.Name == "Int32")
                        dt_elem.Rows[0][dc] = Convert.ToInt32(edit_controls[dc.ColumnName].Text);
                    else if (dc.DataType.Name == "Single")
                        dt_elem.Rows[0][dc] = Convert.ToSingle(edit_controls[dc.ColumnName].Text);
                }
                catch (Exception) { };
            }

            DBFunctions.WriteToDB(dt_elem, ref_table_struct);

            if (tvReference.SelectedNode.Tag == null)
            {
                tvReference.SelectedNode.Tag = Convert.ToInt32(DBFunctions.ReadScalarFromDB("SELECT LAST_INSERT_ID()"));
            }

            tvReference.SelectedNode.Text = (string)dt_elem.Rows[0]["RefName"];

            new_node = null;
        }

        void AddElement()
        {
            
            dt_elem = DBFunctions.ReadFromDB("SELECT * FROM `ref_data_" + reference_db_name + "` WHERE ID = null");

            DataRow new_elem = dt_elem.Rows.Add();

            new_elem["ParentID"] = tvReference.SelectedNode != null ? tvReference.SelectedNode.Tag : 0 ;
            new_elem["RefName"] = "Новый элемент";

            foreach (Control cntrl in edit_controls.Values)
            {
                if (cntrl is TextBox)
                    ((TextBox)cntrl).Text = "";
            }

            edit_controls["RefName"].Text = "Новый элемент";
            
            ((TextBox)edit_controls["RefName"]).SelectionStart = 0;
            ((TextBox)edit_controls["RefName"]).SelectionLength = ((TextBox)edit_controls["RefName"]).TextLength;
            edit_controls["RefName"].Select();

            new_node = new TreeNode("Новый элемент");
            if(tvReference.SelectedNode != null)
                tvReference.SelectedNode.Nodes.Add(new_node);
            else
                tvReference.Nodes.Add(new_node);
            
            tvReference.SelectedNode = new_node;
            
        }

        void DeleteElement()
        {
            if (tvReference.SelectedNode == null)
                return;
            DBFunctions.ExecuteScript("CALL sp_deletetree_" + reference_db_name + "(" + Convert.ToString(tvReference.SelectedNode.Tag) + ")");
            tvReference.SelectedNode.Remove();
        }
         
       
        private void CreateEditControls()
        {
            splitContainer.Panel2.Controls.Clear();
            edit_controls = new Dictionary<string,Control>();
            ref_table_struct = new TableStruct();
            
            DataTable dt_ref_columns = DBFunctions.ReadFromDB("SELECT ColumnName,ColumnDBName,ColumnType FROM referencesconfig WHERE ReferenceDBName = '" + reference_db_name + "'");



            DataRow id_row = dt_ref_columns.NewRow();
            id_row["ColumnName"] = "Код";
            id_row["ColumnDBName"] = "ID";
            id_row["ColumnType"] = "Строка50";

            dt_ref_columns.Rows.InsertAt(id_row, 0);

            ref_table_struct.TableName = "ref_data_" + reference_db_name;
            string[] p_keys = { "ID" };
            ref_table_struct.p_keys = p_keys;
            ref_table_struct.columns = new string[dt_ref_columns.Rows.Count];
            ref_table_struct.columns[dt_ref_columns.Rows.Count-1] = "ParentID";

            foreach (DataRow column_row in dt_ref_columns.Rows)
            {
                if(column_row["ColumnName"] == DBNull.Value)
                {
                    column_row["ColumnName"] = "Наименование";
                    column_row["ColumnDBName"] = "RefName";
                }
               
                Label new_label = new Label();
                new_label.Text = (string)column_row["ColumnName"];
                new_label.Left = 10;
                new_label.Height = 20;
                new_label.Top = 12 + Convert.ToInt32(edit_controls.Count / 2) * (new_label.Height + 5);
                
                edit_controls.Add("label_" + column_row["ColumnDBName"], new_label);
                splitContainer.Panel2.Controls.Add(edit_controls["label_" + column_row["ColumnDBName"]]);

                TextBox new_tb = new TextBox();
                
                new_tb.Tag = (string)column_row["ColumnDBName"];
                new_tb.Left = 150;
                new_tb.Top = 10 + Convert.ToInt32(edit_controls.Count / 2) * (new_tb.Height + 5);
                new_tb.Width = splitContainer.Panel2.Width - 160;
                if (select_mode)
                    new_tb.ReadOnly = true;
                if ((string)new_tb.Tag == "ID")
                    new_tb.ReadOnly = true;
                else
                {
                    ref_table_struct.columns[dt_ref_columns.Rows.IndexOf(column_row) - 1] = (string)column_row["ColumnDBName"];
                    //Проверим право на запись
                    new_tb.ReadOnly = !Auth.AuthModule.rights.references.write;
                }

                
                
                                
                edit_controls.Add((string)column_row["ColumnDBName"], new_tb);
                splitContainer.Panel2.Controls.Add(edit_controls[(string)column_row["ColumnDBName"]]);
            }
        }

                   
    }
}
