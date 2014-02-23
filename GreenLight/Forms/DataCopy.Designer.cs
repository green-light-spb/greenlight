namespace GreenLight
{
    partial class DataCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataCopy));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbTables = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbFrom = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbTo = new System.Windows.Forms.ToolStripTextBox();
            this.tsbFillRange = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgTableData = new System.Windows.Forms.DataGridView();
            this.dgColumns = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTableData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTables,
            this.toolStripSeparator1,
            this.tsbClear,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.tbFrom,
            this.toolStripLabel2,
            this.tbTo,
            this.tsbFillRange,
            this.toolStripSeparator2,
            this.tsbCopy});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(880, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbTables
            // 
            this.cbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTables.Items.AddRange(new object[] {
            "Клиенты",
            "Кредитные программы"});
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(221, 25);
            this.cbTables.SelectedIndexChanged += new System.EventHandler(this.cbTables_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbClear
            // 
            this.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(95, 22);
            this.tsbClear.Text = "Очистить выбор";
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "Диапазон";
            // 
            // tbFrom
            // 
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(30, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(23, 22);
            this.toolStripLabel2.Text = "-->";
            // 
            // tbTo
            // 
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(30, 25);
            // 
            // tsbFillRange
            // 
            this.tsbFillRange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFillRange.Image = ((System.Drawing.Image)(resources.GetObject("tsbFillRange.Image")));
            this.tsbFillRange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFillRange.Name = "tsbFillRange";
            this.tsbFillRange.Size = new System.Drawing.Size(116, 22);
            this.tsbFillRange.Text = "Заполнить диапазон";
            this.tsbFillRange.Click += new System.EventHandler(this.tsbFillRange_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCopy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(154, 22);
            this.tsbCopy.Text = "Выполнить копирование";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgTableData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgColumns);
            this.splitContainer1.Size = new System.Drawing.Size(880, 484);
            this.splitContainer1.SplitterDistance = 405;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgTableData
            // 
            this.dgTableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTableData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTableData.Location = new System.Drawing.Point(0, 0);
            this.dgTableData.Name = "dgTableData";
            this.dgTableData.RowHeadersVisible = false;
            this.dgTableData.Size = new System.Drawing.Size(405, 484);
            this.dgTableData.TabIndex = 0;
            this.dgTableData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTableData_CellContentClick);
            // 
            // dgColumns
            // 
            this.dgColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgColumns.Location = new System.Drawing.Point(0, 0);
            this.dgColumns.Name = "dgColumns";
            this.dgColumns.RowHeadersVisible = false;
            this.dgColumns.Size = new System.Drawing.Size(471, 484);
            this.dgColumns.TabIndex = 0;
            // 
            // DataCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 509);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "DataCopy";
            this.Text = "Копирование данных";
            this.Load += new System.EventHandler(this.DataCopy_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTableData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgTableData;
        private System.Windows.Forms.DataGridView dgColumns;
        private System.Windows.Forms.ToolStripComboBox cbTables;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbFrom;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbTo;
        private System.Windows.Forms.ToolStripButton tsbFillRange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbCopy;
    }
}