using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GreenLight
{
    public partial class MainForm : Form
    {
        public Timer update_activity_timer;
        int Session_ID;        
        DateTime session_start;

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.Dll")]
        static extern int PostMessage(IntPtr hWnd, UInt32 msg, int wParam, int lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        public void ShowAutoClosingMessageBox(string message, string caption)
        {
            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += delegate
            {
                IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, caption);
                if (hWnd.ToInt32() != 0) PostMessage(hWnd, WM_CLOSE, 0, 0);
            };
            timer.Enabled = true;
            MessageBox.Show(message, caption);
        }
                
        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateSessionInfo()
        {
            DBFunctions.ExecuteScript("DELETE FROM active_sessions WHERE SessionID = " + Convert.ToString(Session_ID));

            DateTime curr_datetime = (DateTime)DBFunctions.ReadScalarFromDB("SELECT current_timestamp()");

            DBFunctions.ExecuteScript("INSERT INTO active_sessions SET SessionID = " + Convert.ToString(Session_ID) + ","
                + "Computer_Name='" + Environment.MachineName + "',"
                + "DomainUser='" + SystemInformation.UserName + "',"
                + "LastActivity='" + curr_datetime.ToString("s") + "',"
                + "SessionStart='" + session_start.ToString("s") + "'");
        }

        private void update_activity_timer_Tick(object sender, EventArgs e)
        {
            //Проверим не пора ли нам завершить работу
            bool shut_down_needed = Convert.ToBoolean(DBFunctions.ReadScalarFromDB("SELECT shutdown FROM force_shutdown"));
            if (shut_down_needed)
                Close();

            UpdateSessionInfo();
        }

        private void UpdateVisibility()
        {

            menuTableStruct.Visible = Auth.AuthModule.rights.table_struct.read;
            menuClauseEditor.Visible = Auth.AuthModule.rights.clause_editor.read;
            menuTableEditor.Visible = Auth.AuthModule.rights.table_clients.read || Auth.AuthModule.rights.table_credprogr.read;
            menuReferenceStruct.Visible = Auth.AuthModule.rights.reference_struct.read;
            menuQuestionaryEditor.Visible = Auth.AuthModule.rights.questionary_editor.read;
            menuReferenceEditor.Visible = Auth.AuthModule.rights.references.read;
            menuOfferSelector.Visible = Auth.AuthModule.rights.offer_selector.read;
            menuQuestionary.Visible = Auth.AuthModule.rights.questionary.write;//Чтение в данном случае лишено смысла
            menuDataCopy.Visible = Auth.AuthModule.rights.data_copy.write;//Чтение в данном случае лишено смысла
            menuStringReplace.Visible = Auth.AuthModule.rights.string_replace.read;
            menuActiveSessions.Visible = Auth.AuthModule.rights.active_session.read;
            menuUserControl.Visible = Auth.AuthModule.rights.access_control.write;//Только чтение и запись в паре
            menuRoles.Visible = Auth.AuthModule.rights.access_control.write;//Только чтение и запись в паре

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TestUnit.TestReadFromDB();
            //TestUnit.TestWriteToDB();
            //TestUnit.UpdateDB();
            //TestUnit.SelectOffers();

            TestUnit.TestReflection();
            
            //TestUnit.UpdateOfferSelector();
            //Questionary qu = new Questionary(7, "Универсальная анкета");
            //qu.Show();

            //DBFunctions.GetGlobalParameter("StringReplaceKeyField");
            //TestUnit.ExecuteScriptFromFile("d:\\rocid.sql");
            //TestUnit.ReorganizeMultiref();
            //string[] n = "{1}{232}{125}".Split(new char[] { '{', '}' },StringSplitOptions.RemoveEmptyEntries);            

            //TestUnit.InitLocalParams();
            //TestUnit.TestEncryption();
        }

        private void menuTableStruct_Click(object sender, EventArgs e)
        {
            TableStructureEdit tse = new TableStructureEdit();
            tse.Show();
        }

        private void menuClauseEditor_Click(object sender, EventArgs e)
        {
            ClauseEditor ce = new ClauseEditor();
            ce.Show();
        }

        private void menuTableEditor_Click(object sender, EventArgs e)
        {
            TableEditor te = new TableEditor();
            te.Show();
        }

        private void menuOfferSelector_Click(object sender, EventArgs e)
        {
            OfferSelectorForm osf = new OfferSelectorForm();
            osf.Show();
        }

        private void menuReferenceEditor_Click(object sender, EventArgs e)
        {
            HierarchicalRefEdit hre = new HierarchicalRefEdit();
            hre.Show();
        }

        private void menuReferenceStruct_Click(object sender, EventArgs e)
        {
            ReferenceStructureEdit rse = new ReferenceStructureEdit();
            rse.Show();
        }

        private void menuQuestionaryEditor_Click(object sender, EventArgs e)
        {
            QuestionaryEditor qe = new QuestionaryEditor();
            qe.Show();
        }

        private void menuQuestionary_Click(object sender, EventArgs e)
        {
            Questionary q = new Questionary();
            q.Show();
        }

        private void menuDataCopy_Click(object sender, EventArgs e)
        {
            DataCopy dc = new DataCopy();
            dc.Show();
        }

        private void menuActiveSessions_Click(object sender, EventArgs e)
        {
            ActivityMonitor am = new ActivityMonitor();
            am.Show();
        }

        private void menuStringReplace_Click(object sender, EventArgs e)
        {
            StringReplaseSettings srs = new StringReplaseSettings();
            srs.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Загрузим параметры, проверим валидность
            LocalParameters.LoadParameters();

            string parameters_error_text = "Необходимо заполнить следующие параметры: " + Environment.NewLine;
            bool parameters_error = false;
            if (LocalParameters.MySQLServer == "")
            {
                parameters_error = true;
                parameters_error_text += "   - Сервер MySQL" + Environment.NewLine;
            }

            if (LocalParameters.MySQLDatabase == "")
            {
                parameters_error = true;
                parameters_error_text += "   - База данных MySQL" + Environment.NewLine;
            }

            if (LocalParameters.MySQLUser == "")
            {
                parameters_error = true;
                parameters_error_text += "   - Логин MySQL" + Environment.NewLine;
            }

            if (LocalParameters.MySQLPassword == "")
            {
                parameters_error = true;
                parameters_error_text += "   - Пароль MySQL" + Environment.NewLine;
            }

            if (parameters_error)
            {
                MessageBox.Show(parameters_error_text, "Необходимо заполнить параметры.");
                LocalParameters.EditParameters();
            }

            if (LocalParameters.MySQLServer == "" ||
                LocalParameters.MySQLDatabase == "" ||
                LocalParameters.MySQLUser == "" ||
                LocalParameters.MySQLPassword == "")
            {
                MessageBox.Show("Не все обязательные параметры были заполнены. Работа программы будет завершена.");
                Close();
            }

            DBFunctions.m_frm = this;

            update_activity_timer = new Timer();
            
            DBFunctions.login_from_parameters = true;
            DBFunctions.Init();

            Auth.AuthForm af = new Auth.AuthForm();
            if (af.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                Close();
                return;
            }

            //Если включен режим завершения работы, то не дадим пользователю запустить систему
            bool shut_down_needed = Convert.ToBoolean(DBFunctions.ReadScalarFromDB("SELECT shutdown FROM force_shutdown"));

            if (shut_down_needed)
            {
                ShowAutoClosingMessageBox("На сервере установлен запрет на запуск программы. Программа будет закрыта", "Запрет на запуск");//, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            UpdateVisibility();

            session_start = (DateTime)DBFunctions.ReadScalarFromDB("SELECT current_timestamp()");
            Session_ID = (int)DBFunctions.ReadScalarFromDB("SELECT new_session_id()");
            
            UpdateSessionInfo();

            update_activity_timer.Tick += new EventHandler(update_activity_timer_Tick);
            update_activity_timer.Interval = 20000;
            update_activity_timer.Start();            
        }

        private void menuLocalParameters_Click(object sender, EventArgs e)
        {
            LocalParameters.EditParameters();
        }

        private void menuUserControl_Click(object sender, EventArgs e)
        {
            Auth.AuthUsers au = new Auth.AuthUsers();
            au.Show();
        }

        private void menuRoles_Click(object sender, EventArgs e)
        {
            Auth.AuthRoles ar = new Auth.AuthRoles();
            ar.Show();
        }

        
    }
}
