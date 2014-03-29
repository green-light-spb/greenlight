namespace GreenLight
{
    partial class OfferSelectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfferSelectorForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgClients = new System.Windows.Forms.DataGridView();
            this.dgOffers = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tstbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tsbUniversalQuestionary = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOffers)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgClients);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgOffers);
            this.splitContainer1.Size = new System.Drawing.Size(730, 534);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgClients
            // 
            this.dgClients.AllowUserToAddRows = false;
            this.dgClients.AllowUserToDeleteRows = false;
            this.dgClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgClients.Location = new System.Drawing.Point(0, 0);
            this.dgClients.Name = "dgClients";
            this.dgClients.ReadOnly = true;
            this.dgClients.RowHeadersVisible = false;
            this.dgClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgClients.Size = new System.Drawing.Size(730, 231);
            this.dgClients.TabIndex = 0;
            this.dgClients.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgClients_CellEnter);
            // 
            // dgOffers
            // 
            this.dgOffers.AllowUserToAddRows = false;
            this.dgOffers.AllowUserToDeleteRows = false;
            this.dgOffers.AllowUserToOrderColumns = true;
            this.dgOffers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOffers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOffers.Location = new System.Drawing.Point(0, 0);
            this.dgOffers.Name = "dgOffers";
            this.dgOffers.ReadOnly = true;
            this.dgOffers.RowHeadersVisible = false;
            this.dgOffers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOffers.Size = new System.Drawing.Size(730, 299);
            this.dgOffers.TabIndex = 0;
            this.dgOffers.RowHeightInfoNeeded += new System.Windows.Forms.DataGridViewRowHeightInfoNeededEventHandler(this.dgOffers_RowHeightInfoNeeded);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbSearch,
            this.tsbUniversalQuestionary});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(730, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tstbSearch
            // 
            this.tstbSearch.Name = "tstbSearch";
            this.tstbSearch.Size = new System.Drawing.Size(200, 25);
            this.tstbSearch.TextChanged += new System.EventHandler(this.tstbSearch_TextChanged);
            // 
            // tsbUniversalQuestionary
            // 
            this.tsbUniversalQuestionary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUniversalQuestionary.Image = ((System.Drawing.Image)(resources.GetObject("tsbUniversalQuestionary.Image")));
            this.tsbUniversalQuestionary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUniversalQuestionary.Name = "tsbUniversalQuestionary";
            this.tsbUniversalQuestionary.Size = new System.Drawing.Size(134, 22);
            this.tsbUniversalQuestionary.Text = "Универсальная анкета";
            this.tsbUniversalQuestionary.Click += new System.EventHandler(this.tsbUniversalQuestionary_Click);
            // 
            // OfferSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 559);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OfferSelectorForm";
            this.Text = "Подбор предложений";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OfferSelectorForm_FormClosing);
            this.Load += new System.EventHandler(this.OfferSelectorForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOffers)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgClients;
        private System.Windows.Forms.DataGridView dgOffers;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox tstbSearch;
        private System.Windows.Forms.ToolStripButton tsbUniversalQuestionary;
    }
}