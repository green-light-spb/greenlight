namespace GreenLight
{
    partial class QuestionaryFilterEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionaryFilterEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslOtbor = new System.Windows.Forms.ToolStripLabel();
            this.tscbFields = new System.Windows.Forms.ToolStripComboBox();
            this.tstbOtbor = new System.Windows.Forms.ToolStripTextBox();
            this.tsbApplyOtbor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbDiscard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDuplicate = new System.Windows.Forms.ToolStripButton();
            this.dgFilter = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFilter)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslOtbor,
            this.tscbFields,
            this.tstbOtbor,
            this.tsbApplyOtbor,
            this.toolStripSeparator1,
            this.tsbSave,
            this.tsbDiscard,
            this.toolStripSeparator2,
            this.tsbDuplicate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(842, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslOtbor
            // 
            this.tslOtbor.Name = "tslOtbor";
            this.tslOtbor.Size = new System.Drawing.Size(42, 22);
            this.tslOtbor.Text = "Отбор";
            // 
            // tscbFields
            // 
            this.tscbFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFields.Name = "tscbFields";
            this.tscbFields.Size = new System.Drawing.Size(200, 25);
            // 
            // tstbOtbor
            // 
            this.tstbOtbor.Name = "tstbOtbor";
            this.tstbOtbor.Size = new System.Drawing.Size(150, 25);
            // 
            // tsbApplyOtbor
            // 
            this.tsbApplyOtbor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbApplyOtbor.Image = ((System.Drawing.Image)(resources.GetObject("tsbApplyOtbor.Image")));
            this.tsbApplyOtbor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApplyOtbor.Name = "tsbApplyOtbor";
            this.tsbApplyOtbor.Size = new System.Drawing.Size(110, 22);
            this.tsbApplyOtbor.Text = "Применить отбор";
            this.tsbApplyOtbor.Click += new System.EventHandler(this.tsbApplyOtbor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(69, 22);
            this.tsbSave.Text = "Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbDiscard
            // 
            this.tsbDiscard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDiscard.Image = ((System.Drawing.Image)(resources.GetObject("tsbDiscard.Image")));
            this.tsbDiscard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDiscard.Name = "tsbDiscard";
            this.tsbDiscard.Size = new System.Drawing.Size(128, 22);
            this.tsbDiscard.Text = "Отменить изменения";
            this.tsbDiscard.Click += new System.EventHandler(this.tsbDiscard_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDuplicate
            // 
            this.tsbDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("tsbDuplicate.Image")));
            this.tsbDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDuplicate.Name = "tsbDuplicate";
            this.tsbDuplicate.Size = new System.Drawing.Size(237, 19);
            this.tsbDuplicate.Text = "Дублировать значения текущей колонки";
            this.tsbDuplicate.Click += new System.EventHandler(this.tsbDuplicate_Click);
            // 
            // dgFilter
            // 
            this.dgFilter.AllowUserToAddRows = false;
            this.dgFilter.AllowUserToDeleteRows = false;
            this.dgFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFilter.Location = new System.Drawing.Point(0, 25);
            this.dgFilter.Name = "dgFilter";
            this.dgFilter.RowHeadersVisible = false;
            this.dgFilter.Size = new System.Drawing.Size(842, 476);
            this.dgFilter.TabIndex = 1;
            this.dgFilter.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFilter_CellContentClick);
            // 
            // QuestionaryFilterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 501);
            this.Controls.Add(this.dgFilter);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuestionaryFilterEditor";
            this.Text = "Фильтр анкеты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestionaryFilterEditor_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFilter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgFilter;
        private System.Windows.Forms.ToolStripLabel tslOtbor;
        private System.Windows.Forms.ToolStripComboBox tscbFields;
        private System.Windows.Forms.ToolStripTextBox tstbOtbor;
        private System.Windows.Forms.ToolStripButton tsbApplyOtbor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbDuplicate;
        private System.Windows.Forms.ToolStripButton tsbDiscard;
    }
}