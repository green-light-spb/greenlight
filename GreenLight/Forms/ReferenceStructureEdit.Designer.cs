namespace GreenLight
{
    partial class ReferenceStructureEdit
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferenceStructureEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbCurrentReference = new System.Windows.Forms.ToolStripComboBox();
            this.tbAddReference = new System.Windows.Forms.ToolStripButton();
            this.tbDeleteReference = new System.Windows.Forms.ToolStripButton();
            this.dgRefConfig = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRefConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbCurrentReference,
            this.tbAddReference,
            this.tbDeleteReference});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(794, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbCurrentReference
            // 
            this.cbCurrentReference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentReference.Name = "cbCurrentReference";
            this.cbCurrentReference.Size = new System.Drawing.Size(221, 25);
            this.cbCurrentReference.SelectedIndexChanged += new System.EventHandler(this.cbCurrentReference_SelectedIndexChanged);
            // 
            // tbAddReference
            // 
            this.tbAddReference.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbAddReference.Image = ((System.Drawing.Image)(resources.GetObject("tbAddReference.Image")));
            this.tbAddReference.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAddReference.Name = "tbAddReference";
            this.tbAddReference.Size = new System.Drawing.Size(132, 22);
            this.tbAddReference.Text = "Добавить справочник";
            this.tbAddReference.Click += new System.EventHandler(this.tbAddReference_Click);
            // 
            // tbDeleteReference
            // 
            this.tbDeleteReference.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbDeleteReference.Image = ((System.Drawing.Image)(resources.GetObject("tbDeleteReference.Image")));
            this.tbDeleteReference.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDeleteReference.Name = "tbDeleteReference";
            this.tbDeleteReference.Size = new System.Drawing.Size(124, 22);
            this.tbDeleteReference.Text = "Удалить справочник";
            this.tbDeleteReference.Click += new System.EventHandler(this.tbDeleteReference_Click);
            // 
            // dgRefConfig
            // 
            this.dgRefConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRefConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRefConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgRefConfig.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgRefConfig.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgRefConfig.Location = new System.Drawing.Point(0, 28);
            this.dgRefConfig.Name = "dgRefConfig";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRefConfig.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgRefConfig.Size = new System.Drawing.Size(794, 301);
            this.dgRefConfig.TabIndex = 3;
            this.dgRefConfig.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgRefConfig_CellBeginEdit);
            this.dgRefConfig.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgRefConfig_UserDeletingRow);
            // 
            // ReferenceStructureEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 329);
            this.Controls.Add(this.dgRefConfig);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReferenceStructureEdit";
            this.Text = "Структура справочников";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReferenceStructureEdit_FormClosing);
            this.Load += new System.EventHandler(this.ReferenceStructureEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRefConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cbCurrentReference;
        private System.Windows.Forms.DataGridView dgRefConfig;
        private System.Windows.Forms.ToolStripButton tbAddReference;
        private System.Windows.Forms.ToolStripButton tbDeleteReference;
    }
}