namespace GreenLight
{
    partial class ActivityMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityMonitor));
            this.dgActiveSessions = new System.Windows.Forms.DataGridView();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgActiveSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgActiveSessions
            // 
            this.dgActiveSessions.AllowUserToAddRows = false;
            this.dgActiveSessions.AllowUserToDeleteRows = false;
            this.dgActiveSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgActiveSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgActiveSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgActiveSessions.Location = new System.Drawing.Point(0, 0);
            this.dgActiveSessions.MultiSelect = false;
            this.dgActiveSessions.Name = "dgActiveSessions";
            this.dgActiveSessions.ReadOnly = true;
            this.dgActiveSessions.RowHeadersVisible = false;
            this.dgActiveSessions.ShowCellErrors = false;
            this.dgActiveSessions.ShowCellToolTips = false;
            this.dgActiveSessions.ShowEditingIcon = false;
            this.dgActiveSessions.ShowRowErrors = false;
            this.dgActiveSessions.Size = new System.Drawing.Size(665, 362);
            this.dgActiveSessions.TabIndex = 0;
            this.dgActiveSessions.VirtualMode = true;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 5000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // ActivityMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 362);
            this.Controls.Add(this.dgActiveSessions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivityMonitor";
            this.Text = "ActivityMonitor";
            ((System.ComponentModel.ISupportInitialize)(this.dgActiveSessions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgActiveSessions;
        private System.Windows.Forms.Timer timerUpdate;
    }
}