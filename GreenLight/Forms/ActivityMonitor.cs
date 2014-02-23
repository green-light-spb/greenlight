using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreenLight
{
    public partial class ActivityMonitor : Form
    {
        DataTable dt_active_sessions;
        private void FillDataGrid()
        {
            dt_active_sessions = DBFunctions.ReadFromDB("SELECT SessionID AS 'Номер сессии', Computer_Name AS 'Имя компьютера', DomainUser AS 'Доменный пользователь',LastActivity AS 'Последняя активность',SessionStart AS 'Начало сессии' " +
                "FROM active_sessions");

            dgActiveSessions.DataSource = dt_active_sessions;
        }
        
        public ActivityMonitor()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            FillDataGrid();
        }
    }
}
