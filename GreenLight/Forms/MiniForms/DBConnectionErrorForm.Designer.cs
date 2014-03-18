namespace GreenLight.MiniForms
{
    partial class DBConnectionErrorForm
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
            this.lbErrorText = new System.Windows.Forms.Label();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnChangeParams = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbErrorText
            // 
            this.lbErrorText.AutoSize = true;
            this.lbErrorText.Location = new System.Drawing.Point(13, 13);
            this.lbErrorText.Name = "lbErrorText";
            this.lbErrorText.Size = new System.Drawing.Size(342, 65);
            this.lbErrorText.TabIndex = 0;
            this.lbErrorText.Text = "Возникла ошибка подключения к базе данных.\r\nВы можете:\r\n   - повторить попытку по" +
                "дключения с имеющимися параметрами\r\n   - изменить параметры подключения\r\n   - за" +
                "вершить работу программы\r\n";
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(442, 9);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(152, 23);
            this.btnRetry.TabIndex = 1;
            this.btnRetry.Text = "Повторить попытку";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnChangeParams
            // 
            this.btnChangeParams.Location = new System.Drawing.Point(442, 38);
            this.btnChangeParams.Name = "btnChangeParams";
            this.btnChangeParams.Size = new System.Drawing.Size(152, 23);
            this.btnChangeParams.TabIndex = 2;
            this.btnChangeParams.Text = "Изменить параметры...";
            this.btnChangeParams.UseVisualStyleBackColor = true;
            this.btnChangeParams.Click += new System.EventHandler(this.btnChangeParams_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(442, 67);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Закрыть программу";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // DBConnectionErrorForm
            // 
            this.AcceptButton = this.btnRetry;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(613, 101);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnChangeParams);
            this.Controls.Add(this.btnRetry);
            this.Controls.Add(this.lbErrorText);
            this.Name = "DBConnectionErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ошибка подключения к БД";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbErrorText;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnChangeParams;
        private System.Windows.Forms.Button btnExit;
    }
}