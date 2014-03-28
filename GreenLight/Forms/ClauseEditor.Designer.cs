namespace GreenLight
{
    partial class ClauseEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClauseEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbOk = new System.Windows.Forms.ToolStripButton();
            this.tstbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tsbInvertUseInWhereClause = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgColumnNames = new System.Windows.Forms.DataGridView();
            this.tbWhereClause = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgColumnNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbWhereClause)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCancel,
            this.tsbSave,
            this.tsbOk,
            this.tstbSearch,
            this.tsbInvertUseInWhereClause});
            this.toolStrip1.Location = new System.Drawing.Point(0, 403);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(798, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCancel
            // 
            this.tsbCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(53, 22);
            this.tsbCancel.Text = "Отмена";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(69, 22);
            this.tsbSave.Text = "Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbOk
            // 
            this.tsbOk.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbOk.Image = ((System.Drawing.Image)(resources.GetObject("tsbOk.Image")));
            this.tsbOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOk.Name = "tsbOk";
            this.tsbOk.Size = new System.Drawing.Size(27, 22);
            this.tsbOk.Text = "Ok";
            this.tsbOk.Click += new System.EventHandler(this.tsbOk_Click);
            // 
            // tstbSearch
            // 
            this.tstbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbSearch.Name = "tstbSearch";
            this.tstbSearch.Size = new System.Drawing.Size(200, 25);
            this.tstbSearch.TextChanged += new System.EventHandler(this.tstbSearch_TextChanged);
            // 
            // tsbInvertUseInWhereClause
            // 
            this.tsbInvertUseInWhereClause.AutoToolTip = false;
            this.tsbInvertUseInWhereClause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbInvertUseInWhereClause.Image = ((System.Drawing.Image)(resources.GetObject("tsbInvertUseInWhereClause.Image")));
            this.tsbInvertUseInWhereClause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInvertUseInWhereClause.Name = "tsbInvertUseInWhereClause";
            this.tsbInvertUseInWhereClause.Size = new System.Drawing.Size(155, 22);
            this.tsbInvertUseInWhereClause.Text = "Использование в условии";
            this.tsbInvertUseInWhereClause.Click += new System.EventHandler(this.tsbInvertUseInWhereClause_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbWhereClause);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgColumnNames);
            this.splitContainer1.Size = new System.Drawing.Size(798, 403);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgColumnNames
            // 
            this.dgColumnNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgColumnNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgColumnNames.Location = new System.Drawing.Point(0, 0);
            this.dgColumnNames.Name = "dgColumnNames";
            this.dgColumnNames.ReadOnly = true;
            this.dgColumnNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgColumnNames.Size = new System.Drawing.Size(798, 133);
            this.dgColumnNames.TabIndex = 0;
            this.dgColumnNames.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgColumnNames_CellDoubleClick);
            // 
            // tbWhereClause
            // 
            this.tbWhereClause.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.tbWhereClause.BackBrush = null;
            this.tbWhereClause.CharHeight = 14;
            this.tbWhereClause.CharWidth = 8;
            this.tbWhereClause.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbWhereClause.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbWhereClause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbWhereClause.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbWhereClause.IsReplaceMode = false;
            this.tbWhereClause.Language = FastColoredTextBoxNS.Language.SQL;
            this.tbWhereClause.LeftBracket = '(';
            this.tbWhereClause.Location = new System.Drawing.Point(0, 0);
            this.tbWhereClause.Name = "tbWhereClause";
            this.tbWhereClause.Paddings = new System.Windows.Forms.Padding(0);
            this.tbWhereClause.RightBracket = ')';
            this.tbWhereClause.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbWhereClause.Size = new System.Drawing.Size(798, 266);
            this.tbWhereClause.TabIndex = 0;
            this.tbWhereClause.Text = "";
            this.tbWhereClause.Zoom = 100;
            // 
            // ClauseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 428);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClauseEditor";
            this.Text = "Редактор условий";
            this.Load += new System.EventHandler(this.ClauseEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgColumnNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbWhereClause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripButton tsbOk;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgColumnNames;
        private System.Windows.Forms.ToolStripTextBox tstbSearch;
        private System.Windows.Forms.ToolStripButton tsbInvertUseInWhereClause;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private FastColoredTextBoxNS.FastColoredTextBox tbWhereClause;
    }
}