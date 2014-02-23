namespace GreenLight
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.структураТаблицToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.структураСправочниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редакторУсловийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анкетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.активныеСессииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заменаЗначенийДляАнкетБанковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редакторСправочниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редакторТаблицToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копированиеДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анкетыИПодборToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подборПредложенийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анкетированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.данныеToolStripMenuItem,
            this.анкетыИПодборToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(601, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.структураТаблицToolStripMenuItem,
            this.структураСправочниковToolStripMenuItem,
            this.редакторУсловийToolStripMenuItem,
            this.анкетыToolStripMenuItem,
            this.активныеСессииToolStripMenuItem,
            this.заменаЗначенийДляАнкетБанковToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // структураТаблицToolStripMenuItem
            // 
            this.структураТаблицToolStripMenuItem.Name = "структураТаблицToolStripMenuItem";
            this.структураТаблицToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.структураТаблицToolStripMenuItem.Text = "Структура таблиц";
            this.структураТаблицToolStripMenuItem.Click += new System.EventHandler(this.структураТаблицToolStripMenuItem_Click);
            // 
            // структураСправочниковToolStripMenuItem
            // 
            this.структураСправочниковToolStripMenuItem.Name = "структураСправочниковToolStripMenuItem";
            this.структураСправочниковToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.структураСправочниковToolStripMenuItem.Text = "Структура справочников";
            this.структураСправочниковToolStripMenuItem.Click += new System.EventHandler(this.структураСправочниковToolStripMenuItem_Click);
            // 
            // редакторУсловийToolStripMenuItem
            // 
            this.редакторУсловийToolStripMenuItem.Name = "редакторУсловийToolStripMenuItem";
            this.редакторУсловийToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.редакторУсловийToolStripMenuItem.Text = "Редактор условий";
            this.редакторУсловийToolStripMenuItem.Click += new System.EventHandler(this.редакторУсловийToolStripMenuItem_Click);
            // 
            // анкетыToolStripMenuItem
            // 
            this.анкетыToolStripMenuItem.Name = "анкетыToolStripMenuItem";
            this.анкетыToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.анкетыToolStripMenuItem.Text = "Анкеты";
            this.анкетыToolStripMenuItem.Click += new System.EventHandler(this.анкетыToolStripMenuItem_Click);
            // 
            // активныеСессииToolStripMenuItem
            // 
            this.активныеСессииToolStripMenuItem.Name = "активныеСессииToolStripMenuItem";
            this.активныеСессииToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.активныеСессииToolStripMenuItem.Text = "Активные сессии";
            this.активныеСессииToolStripMenuItem.Click += new System.EventHandler(this.активныеСессииToolStripMenuItem_Click);
            // 
            // заменаЗначенийДляАнкетБанковToolStripMenuItem
            // 
            this.заменаЗначенийДляАнкетБанковToolStripMenuItem.Name = "заменаЗначенийДляАнкетБанковToolStripMenuItem";
            this.заменаЗначенийДляАнкетБанковToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.заменаЗначенийДляАнкетБанковToolStripMenuItem.Text = "Замена значений для анкет банков";
            this.заменаЗначенийДляАнкетБанковToolStripMenuItem.Click += new System.EventHandler(this.заменаЗначенийДляАнкетБанковToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редакторСправочниковToolStripMenuItem,
            this.редакторТаблицToolStripMenuItem,
            this.копированиеДанныхToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.данныеToolStripMenuItem.Text = "Данные";
            // 
            // редакторСправочниковToolStripMenuItem
            // 
            this.редакторСправочниковToolStripMenuItem.Name = "редакторСправочниковToolStripMenuItem";
            this.редакторСправочниковToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.редакторСправочниковToolStripMenuItem.Text = "Редактор справочников";
            this.редакторСправочниковToolStripMenuItem.Click += new System.EventHandler(this.редакторСправочниковToolStripMenuItem_Click);
            // 
            // редакторТаблицToolStripMenuItem
            // 
            this.редакторТаблицToolStripMenuItem.Name = "редакторТаблицToolStripMenuItem";
            this.редакторТаблицToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.редакторТаблицToolStripMenuItem.Text = "Редактор таблиц";
            this.редакторТаблицToolStripMenuItem.Click += new System.EventHandler(this.редакторТаблицToolStripMenuItem_Click);
            // 
            // копированиеДанныхToolStripMenuItem
            // 
            this.копированиеДанныхToolStripMenuItem.Name = "копированиеДанныхToolStripMenuItem";
            this.копированиеДанныхToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.копированиеДанныхToolStripMenuItem.Text = "Копирование данных";
            this.копированиеДанныхToolStripMenuItem.Click += new System.EventHandler(this.копированиеДанныхToolStripMenuItem_Click);
            // 
            // анкетыИПодборToolStripMenuItem
            // 
            this.анкетыИПодборToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подборПредложенийToolStripMenuItem,
            this.анкетированиеToolStripMenuItem});
            this.анкетыИПодборToolStripMenuItem.Name = "анкетыИПодборToolStripMenuItem";
            this.анкетыИПодборToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.анкетыИПодборToolStripMenuItem.Text = "Анкеты и подбор";
            // 
            // подборПредложенийToolStripMenuItem
            // 
            this.подборПредложенийToolStripMenuItem.Name = "подборПредложенийToolStripMenuItem";
            this.подборПредложенийToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.подборПредложенийToolStripMenuItem.Text = "Подбор предложений";
            this.подборПредложенийToolStripMenuItem.Click += new System.EventHandler(this.подборПредложенийToolStripMenuItem_Click);
            // 
            // анкетированиеToolStripMenuItem
            // 
            this.анкетированиеToolStripMenuItem.Name = "анкетированиеToolStripMenuItem";
            this.анкетированиеToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.анкетированиеToolStripMenuItem.Text = "Анкетирование";
            this.анкетированиеToolStripMenuItem.Click += new System.EventHandler(this.анкетированиеToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Зеленый свет";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem структураТаблицToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редакторУсловийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem структураСправочниковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem анкетыИПодборToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem анкетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редакторСправочниковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редакторТаблицToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подборПредложенийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem анкетированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копированиеДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem активныеСессииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заменаЗначенийДляАнкетБанковToolStripMenuItem;
    }
}

