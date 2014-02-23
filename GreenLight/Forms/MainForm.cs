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
        Timer update_activity_timer;
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
                        
            session_start = (DateTime)DBFunctions.ReadScalarFromDB("SELECT current_timestamp()");
            Session_ID = (int)DBFunctions.ReadScalarFromDB("SELECT new_session_id()");
            UpdateSessionInfo();

            update_activity_timer = new Timer();
            update_activity_timer.Tick += new EventHandler(update_activity_timer_Tick);
            update_activity_timer.Interval = 20000;
            update_activity_timer.Start();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //TestUnit.TestReadFromDB();
            //TestUnit.TestWriteToDB();
            //TestUnit.UpdateDB();
            //TestUnit.SelectOffers();
            //TestUnit.UpdateOfferSelector();
            //Questionary qu = new Questionary(7, "Универсальная анкета");
            //qu.Show();

            //DBFunctions.GetGlobalParameter("StringReplaceKeyField");
            //TestUnit.ExecuteScriptFromFile("d:\\rocid.sql");
            //TestUnit.ReorganizeMultiref();
            //string[] n = "{1}{232}{125}".Split(new char[] { '{', '}' },StringSplitOptions.RemoveEmptyEntries);            
        }

        private void структураТаблицToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableStructureEdit tse = new TableStructureEdit();
            tse.Show();
        }

        private void редакторУсловийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClauseEditor ce = new ClauseEditor();
            ce.Show();
        }

        private void редакторТаблицToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableEditor te = new TableEditor();
            te.Show();
        }

        private void подборПредложенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OfferSelectorForm osf = new OfferSelectorForm();
            osf.Show();
        }

        private void редакторСправочниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HierarchicalRefEdit hre = new HierarchicalRefEdit();
            hre.Show();
        }

        private void структураСправочниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReferenceStructureEdit rse = new ReferenceStructureEdit();
            rse.Show();
        }

        private void анкетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestionaryEditor qe = new QuestionaryEditor();
            qe.Show();
        }

        private void анкетированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Questionary q = new Questionary();
            q.Show();
        }

        private void копированиеДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataCopy dc = new DataCopy();
            dc.Show();
        }

        private void активныеСессииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivityMonitor am = new ActivityMonitor();
            am.Show();
        }

        private void заменаЗначенийДляАнкетБанковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringReplaseSettings srs = new StringReplaseSettings();
            srs.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Если включен режим завершения работы, то не дадим пользователю запустить систему
            bool shut_down_needed = Convert.ToBoolean(DBFunctions.ReadScalarFromDB("SELECT shutdown FROM force_shutdown"));

            if (shut_down_needed)
            {
                ShowAutoClosingMessageBox("На сервере установлен запрет на запуск программы. Программа будет закрыта", "Запрет на запуск");//, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        
    }
}
