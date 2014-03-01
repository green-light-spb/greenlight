namespace GreenLight
{
    partial class UniversalQuestionaryOfferSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UniversalQuestionaryOfferSelect));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOk = new System.Windows.Forms.ToolStripButton();
            this.dgOffers = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bank_lk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Programma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOffers)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOk});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(679, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOk
            // 
            this.tsbOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOk.Image = ((System.Drawing.Image)(resources.GetObject("tsbOk.Image")));
            this.tsbOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOk.Name = "tsbOk";
            this.tsbOk.Size = new System.Drawing.Size(87, 22);
            this.tsbOk.Text = "Начать опрос";
            this.tsbOk.Click += new System.EventHandler(this.tsbOk_Click);
            // 
            // dgOffers
            // 
            this.dgOffers.AllowUserToAddRows = false;
            this.dgOffers.AllowUserToDeleteRows = false;
            this.dgOffers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOffers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.bank_lk,
            this.Programma});
            this.dgOffers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOffers.Location = new System.Drawing.Point(0, 25);
            this.dgOffers.Name = "dgOffers";
            this.dgOffers.Size = new System.Drawing.Size(679, 381);
            this.dgOffers.TabIndex = 1;
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Выбран";
            this.Selected.Name = "Selected";
            // 
            // bank_lk
            // 
            this.bank_lk.HeaderText = "Банк/ЛК";
            this.bank_lk.Name = "bank_lk";
            // 
            // Programma
            // 
            this.Programma.HeaderText = "Программа";
            this.Programma.Name = "Programma";
            // 
            // UniversalQuestionaryOfferSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 406);
            this.Controls.Add(this.dgOffers);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UniversalQuestionaryOfferSelect";
            this.Text = "Выбор программ для анкеты";
            this.Load += new System.EventHandler(this.UniversalQuestionaryOfferSelect_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOffers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOk;
        private System.Windows.Forms.DataGridView dgOffers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn bank_lk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Programma;
    }
}