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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableStruct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReferenceStruct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClauseEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuestionaryEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStringReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReferenceEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTableEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDataCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAnketyAndPodbor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOfferSelector = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuestionary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuActiveSessions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocalParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUserControl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuData,
            this.menuAnketyAndPodbor,
            this.menuSystem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(601, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTableStruct,
            this.menuReferenceStruct,
            this.menuClauseEditor,
            this.menuQuestionaryEditor,
            this.menuStringReplace});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(79, 20);
            this.menuSettings.Text = "Настройки";
            // 
            // menuTableStruct
            // 
            this.menuTableStruct.Name = "menuTableStruct";
            this.menuTableStruct.Size = new System.Drawing.Size(267, 22);
            this.menuTableStruct.Text = "Структура таблиц";
            this.menuTableStruct.Click += new System.EventHandler(this.menuTableStruct_Click);
            // 
            // menuReferenceStruct
            // 
            this.menuReferenceStruct.Name = "menuReferenceStruct";
            this.menuReferenceStruct.Size = new System.Drawing.Size(267, 22);
            this.menuReferenceStruct.Text = "Структура справочников";
            this.menuReferenceStruct.Click += new System.EventHandler(this.menuReferenceStruct_Click);
            // 
            // menuClauseEditor
            // 
            this.menuClauseEditor.Name = "menuClauseEditor";
            this.menuClauseEditor.Size = new System.Drawing.Size(267, 22);
            this.menuClauseEditor.Text = "Редактор условий";
            this.menuClauseEditor.Click += new System.EventHandler(this.menuClauseEditor_Click);
            // 
            // menuQuestionaryEditor
            // 
            this.menuQuestionaryEditor.Name = "menuQuestionaryEditor";
            this.menuQuestionaryEditor.Size = new System.Drawing.Size(267, 22);
            this.menuQuestionaryEditor.Text = "Анкеты";
            this.menuQuestionaryEditor.Click += new System.EventHandler(this.menuQuestionaryEditor_Click);
            // 
            // menuStringReplace
            // 
            this.menuStringReplace.Name = "menuStringReplace";
            this.menuStringReplace.Size = new System.Drawing.Size(267, 22);
            this.menuStringReplace.Text = "Замена значений для анкет банков";
            this.menuStringReplace.Click += new System.EventHandler(this.menuStringReplace_Click);
            // 
            // menuData
            // 
            this.menuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReferenceEditor,
            this.menuTableEditor,
            this.menuDataCopy});
            this.menuData.Name = "menuData";
            this.menuData.Size = new System.Drawing.Size(62, 20);
            this.menuData.Text = "Данные";
            // 
            // menuReferenceEditor
            // 
            this.menuReferenceEditor.Name = "menuReferenceEditor";
            this.menuReferenceEditor.Size = new System.Drawing.Size(206, 22);
            this.menuReferenceEditor.Text = "Редактор справочников";
            this.menuReferenceEditor.Click += new System.EventHandler(this.menuReferenceEditor_Click);
            // 
            // menuTableEditor
            // 
            this.menuTableEditor.Name = "menuTableEditor";
            this.menuTableEditor.Size = new System.Drawing.Size(206, 22);
            this.menuTableEditor.Text = "Редактор таблиц";
            this.menuTableEditor.Click += new System.EventHandler(this.menuTableEditor_Click);
            // 
            // menuDataCopy
            // 
            this.menuDataCopy.Name = "menuDataCopy";
            this.menuDataCopy.Size = new System.Drawing.Size(206, 22);
            this.menuDataCopy.Text = "Копирование данных";
            this.menuDataCopy.Click += new System.EventHandler(this.menuDataCopy_Click);
            // 
            // menuAnketyAndPodbor
            // 
            this.menuAnketyAndPodbor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOfferSelector,
            this.menuQuestionary});
            this.menuAnketyAndPodbor.Name = "menuAnketyAndPodbor";
            this.menuAnketyAndPodbor.Size = new System.Drawing.Size(114, 20);
            this.menuAnketyAndPodbor.Text = "Анкеты и подбор";
            // 
            // menuOfferSelector
            // 
            this.menuOfferSelector.Name = "menuOfferSelector";
            this.menuOfferSelector.Size = new System.Drawing.Size(196, 22);
            this.menuOfferSelector.Text = "Подбор предложений";
            this.menuOfferSelector.Click += new System.EventHandler(this.menuOfferSelector_Click);
            // 
            // menuQuestionary
            // 
            this.menuQuestionary.Name = "menuQuestionary";
            this.menuQuestionary.Size = new System.Drawing.Size(196, 22);
            this.menuQuestionary.Text = "Анкетирование";
            this.menuQuestionary.Click += new System.EventHandler(this.menuQuestionary_Click);
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuActiveSessions,
            this.menuLocalParameters,
            this.menuUserControl,
            this.menuRoles});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(66, 20);
            this.menuSystem.Text = "Система";
            // 
            // menuActiveSessions
            // 
            this.menuActiveSessions.Name = "menuActiveSessions";
            this.menuActiveSessions.Size = new System.Drawing.Size(234, 22);
            this.menuActiveSessions.Text = "Активные сессии";
            this.menuActiveSessions.Click += new System.EventHandler(this.menuActiveSessions_Click);
            // 
            // menuLocalParameters
            // 
            this.menuLocalParameters.Name = "menuLocalParameters";
            this.menuLocalParameters.Size = new System.Drawing.Size(234, 22);
            this.menuLocalParameters.Text = "Локальные параметры";
            this.menuLocalParameters.Click += new System.EventHandler(this.menuLocalParameters_Click);
            // 
            // menuUserControl
            // 
            this.menuUserControl.Name = "menuUserControl";
            this.menuUserControl.Size = new System.Drawing.Size(234, 22);
            this.menuUserControl.Text = "Управление пользователями";
            this.menuUserControl.Click += new System.EventHandler(this.menuUserControl_Click);
            // 
            // menuRoles
            // 
            this.menuRoles.Name = "menuRoles";
            this.menuRoles.Size = new System.Drawing.Size(234, 22);
            this.menuRoles.Text = "Роли";
            this.menuRoles.Click += new System.EventHandler(this.menuRoles_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 262);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuTableStruct;
        private System.Windows.Forms.ToolStripMenuItem menuClauseEditor;
        private System.Windows.Forms.ToolStripMenuItem menuReferenceStruct;
        private System.Windows.Forms.ToolStripMenuItem menuData;
        private System.Windows.Forms.ToolStripMenuItem menuAnketyAndPodbor;
        private System.Windows.Forms.ToolStripMenuItem menuQuestionaryEditor;
        private System.Windows.Forms.ToolStripMenuItem menuReferenceEditor;
        private System.Windows.Forms.ToolStripMenuItem menuTableEditor;
        private System.Windows.Forms.ToolStripMenuItem menuOfferSelector;
        private System.Windows.Forms.ToolStripMenuItem menuQuestionary;
        private System.Windows.Forms.ToolStripMenuItem menuDataCopy;
        private System.Windows.Forms.ToolStripMenuItem menuStringReplace;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuActiveSessions;
        private System.Windows.Forms.ToolStripMenuItem menuLocalParameters;
        private System.Windows.Forms.ToolStripMenuItem menuUserControl;
        private System.Windows.Forms.ToolStripMenuItem menuRoles;
    }
}

