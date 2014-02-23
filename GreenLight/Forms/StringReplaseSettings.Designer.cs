namespace GreenLight
{
    partial class StringReplaseSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringReplaseSettings));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslKeyFieldName = new System.Windows.Forms.ToolStripLabel();
            this.cbKeyFieldValues = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbReferenceNames = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.dgReplaceStrings = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReplaceStrings)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslKeyFieldName,
            this.cbKeyFieldValues,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cbReferenceNames,
            this.toolStripSeparator2,
            this.tsbSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(769, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslKeyFieldName
            // 
            this.tslKeyFieldName.Name = "tslKeyFieldName";
            this.tslKeyFieldName.Size = new System.Drawing.Size(0, 22);
            // 
            // cbKeyFieldValues
            // 
            this.cbKeyFieldValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKeyFieldValues.Name = "cbKeyFieldValues";
            this.cbKeyFieldValues.Size = new System.Drawing.Size(200, 25);
            this.cbKeyFieldValues.SelectedIndexChanged += new System.EventHandler(this.cbKeyFieldValues_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(72, 22);
            this.toolStripLabel1.Text = "Справочник:";
            // 
            // cbReferenceNames
            // 
            this.cbReferenceNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReferenceNames.Name = "cbReferenceNames";
            this.cbReferenceNames.Size = new System.Drawing.Size(200, 25);
            this.cbReferenceNames.SelectedIndexChanged += new System.EventHandler(this.cbKeyFieldValues_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(66, 22);
            this.tsbSave.Text = "Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // dgReplaceStrings
            // 
            this.dgReplaceStrings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReplaceStrings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReplaceStrings.Location = new System.Drawing.Point(0, 25);
            this.dgReplaceStrings.Name = "dgReplaceStrings";
            this.dgReplaceStrings.Size = new System.Drawing.Size(769, 388);
            this.dgReplaceStrings.TabIndex = 1;
            this.dgReplaceStrings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReplaceStrings_CellEndEdit);
            this.dgReplaceStrings.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgReplaceStrings_CellValidating);
            // 
            // StringReplaseSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 413);
            this.Controls.Add(this.dgReplaceStrings);
            this.Controls.Add(this.toolStrip1);
            this.Name = "StringReplaseSettings";
            this.Text = "Настройки замены строк";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StringReplaseSettings_FormClosing);
            this.Load += new System.EventHandler(this.StringReplaseSettings_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReplaceStrings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslKeyFieldName;
        private System.Windows.Forms.ToolStripComboBox cbKeyFieldValues;
        private System.Windows.Forms.DataGridView dgReplaceStrings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbReferenceNames;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSave;
    }
}