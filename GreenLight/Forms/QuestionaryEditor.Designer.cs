namespace GreenLight
{
    partial class QuestionaryEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionaryEditor));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgQuestions = new System.Windows.Forms.DataGridView();
            this.dgAnswers = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbQuestionaries = new System.Windows.Forms.ToolStripComboBox();
            this.tsbEditFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAnswers)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 28);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgQuestions);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgAnswers);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Size = new System.Drawing.Size(606, 318);
            this.splitContainer.SplitterDistance = 184;
            this.splitContainer.TabIndex = 0;
            // 
            // dgQuestions
            // 
            this.dgQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgQuestions.Location = new System.Drawing.Point(0, 0);
            this.dgQuestions.Name = "dgQuestions";
            this.dgQuestions.Size = new System.Drawing.Size(606, 318);
            this.dgQuestions.TabIndex = 0;
            this.dgQuestions.CurrentCellChanged += new System.EventHandler(this.dgQuestions_CurrentCellChanged);
            // 
            // dgAnswers
            // 
            this.dgAnswers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAnswers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAnswers.Location = new System.Drawing.Point(0, 0);
            this.dgAnswers.Name = "dgAnswers";
            this.dgAnswers.Size = new System.Drawing.Size(150, 46);
            this.dgAnswers.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbQuestionaries,
            this.tsbEditFilter,
            this.toolStripSeparator1,
            this.tsbSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(606, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbQuestionaries
            // 
            this.cbQuestionaries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuestionaries.Items.AddRange(new object[] {
            "Анкета клиента",
            "Универсальная анкета"});
            this.cbQuestionaries.Name = "cbQuestionaries";
            this.cbQuestionaries.Size = new System.Drawing.Size(221, 25);
            this.cbQuestionaries.SelectedIndexChanged += new System.EventHandler(this.cbQuestionaries_SelectedIndexChanged);
            // 
            // tsbEditFilter
            // 
            this.tsbEditFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEditFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditFilter.Image")));
            this.tsbEditFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditFilter.Name = "tsbEditFilter";
            this.tsbEditFilter.Size = new System.Drawing.Size(131, 22);
            this.tsbEditFilter.Text = "Редактировать фильтр";
            this.tsbEditFilter.Click += new System.EventHandler(this.tsbEditFilter_Click);
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
            this.tsbSave.Size = new System.Drawing.Size(66, 22);
            this.tsbSave.Text = "Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // QuestionaryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 348);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuestionaryEditor";
            this.Text = "Редактор анкет";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestionaryEditor_FormClosing);
            this.Load += new System.EventHandler(this.QuestionaryEditor_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgQuestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAnswers)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgQuestions;
        private System.Windows.Forms.DataGridView dgAnswers;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cbQuestionaries;
        private System.Windows.Forms.ToolStripButton tsbEditFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSave;
    }
}