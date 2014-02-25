using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace GreenLight
{
    class LocalParameters
    {
        const string registry_path = "HKEY_CURRENT_USER\\Software\\GreenLight\\GreenLight";
        
        static string login = "";
        public static string Login
        {
            get { return login; }
            set { login = value; }
        }
        static string mysql_server = "";
        public static string MySQLServer
        {
            get { return mysql_server; }
            set { mysql_server = value; }
        }
        static string mysql_database = "";
        public static string MySQLDatabase
        {
            get { return mysql_database; }
            set { mysql_database = value; }
        }
        static string mysql_user = "";
        public static string MySQLUser
        {
            get { return mysql_user; }
            set { mysql_user = value; }
        }
        static string mysql_password = "";
        public static string MySQLPassword
        {
            get { return mysql_password; }
            set { mysql_password = value; }
        }

        public static void EditParameters()
        {
            field_struct[] fs = new field_struct[5];

            fs[0].field_label = "Имя пользователя";
            fs[0].field_value = login;
            fs[1].field_label = "Сервер MySQL";
            fs[1].field_value = mysql_server;
            fs[2].field_label = "База данных MySQL";
            fs[2].field_value = mysql_database;
            fs[3].field_label = "Логин MySQL";
            fs[3].field_value = mysql_user;
            fs[4].field_label = "Пароль MySQL";
            fs[4].field_value = mysql_password;

            FieldsEditForm fed = new FieldsEditForm();
            fed.Text = "Редактирование локальных параметров";
            fed.Fields = fs;

            if (fed.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                login = fed.Fields[0].field_value;
                mysql_server = fed.Fields[1].field_value;
                mysql_database = fed.Fields[2].field_value;
                mysql_user = fed.Fields[3].field_value;
                mysql_password = fed.Fields[4].field_value;

                SaveParameters();
            }

        }

        public static void SaveParameters()
        {
            Registry.SetValue(registry_path, "login", login);
            Registry.SetValue(registry_path, "mysql_server", mysql_server);
            Registry.SetValue(registry_path, "mysql_database", mysql_database);
            Registry.SetValue(registry_path, "mysql_user", mysql_user);
            Registry.SetValue(registry_path, "mysql_password", mysql_password);
        }

        public static void LoadParameters()
        {
            login = (string)Registry.GetValue(registry_path, "login", "");
            if (login == null)
                login = "";

            mysql_server = (string)Registry.GetValue(registry_path, "mysql_server", "");
            if (mysql_server == null)
                mysql_server = "";

            mysql_database = (string)Registry.GetValue(registry_path, "mysql_database", "");
            if (mysql_database == null)
                mysql_database = "";

            mysql_user = (string)Registry.GetValue(registry_path, "mysql_user", "");
            if (mysql_user == null)
                mysql_user = "";

            mysql_password = (string)Registry.GetValue(registry_path, "mysql_password", "");
            if (mysql_password == null)
                mysql_password = "";
        }
    }
}
