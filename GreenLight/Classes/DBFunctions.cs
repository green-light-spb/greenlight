using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace GreenLight
{
    class TableStruct
    {
        public string TableName;
        public string[] p_keys;
        public string[] columns;
    }

    class DBFunctions
    {
        public static MainForm m_frm;
        static string connection_string;
        public static string login;
        public static string password;
        public static bool login_from_parameters = true;
        
        public static void Init()
        {
            try
            {
                connection_string = LoadConnString();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка чтения настроек подключения к базе данных. Работа программы будет завершена.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                m_frm.Close();
            }
        }       

        private static string LoadConnString()
        {
            if (login_from_parameters)
            {
                return "Database=" + LocalParameters.MySQLDatabase + ";Data Source=" + LocalParameters.MySQLServer +
                    ";Port=3306;User Id=" + LocalParameters.MySQLUser + ";Password=" + LocalParameters.MySQLPassword +
                    ";CharSet=utf8";
            }
            else
            {
                return "Database=" + LocalParameters.MySQLDatabase + ";Data Source=" + LocalParameters.MySQLServer +
                    ";Port=3306;User Id=" + login + ";Password=" + password +
                    ";CharSet=utf8";            
            }

        }

        public static bool TestConnecion()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connection_string);
                connection.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static MySqlConnection Connect()
        {
            while (true)
            {
                try
                {
                    //Костылёк. Приостановим на время попытки соединения таймер в mainform. На случай ошибки соединения, чтобы не плодить эти ошибки
                    bool timer_enabled = m_frm.update_activity_timer.Enabled;
                    m_frm.update_activity_timer.Enabled = false;
                    MySqlConnection connection = new MySqlConnection(connection_string);
                    connection.Open();
                    m_frm.update_activity_timer.Enabled = timer_enabled;
                    return connection;
                }
                catch (Exception)
                {
                    GreenLight.MiniForms.DBConnectionErrorForm err_frm = new MiniForms.DBConnectionErrorForm();

                    if (err_frm.ShowDialog() == System.Windows.Forms.DialogResult.No)
                    {
                        m_frm.Close();
                        throw (new Exception("База данных недоступна. Программа будет закрыта."));
                    }
                    else
                    {
                        connection_string = LoadConnString();
                    }
                }
            }
            
        }

        private static void Disconnect(MySqlConnection connection)
        {
            connection.Close();
        }

        public static DataTable ReadFromDB(string query, Dictionary<string, object> parameters = null)
        {
            MySqlConnection connection = Connect();

            MySqlCommand command = new MySqlCommand(query.ToLower(), connection);
            if (parameters != null)
            {
                foreach (string param_name in parameters.Keys)
                {
                    command.Parameters.AddWithValue(param_name, parameters[param_name]);
                }
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);

            Disconnect(connection);

            return table;
        }

        public static object ReadScalarFromDB(string query, Dictionary<string,object> parameters = null)
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = new MySqlCommand(query.ToLower(), connection);
            if (parameters != null)
            {
                foreach (string param_name in parameters.Keys)
                {
                    command.Parameters.AddWithValue(param_name, parameters[param_name]);
                }
            }

            object value = command.ExecuteScalar();

            Disconnect(connection);

            return value;
        }

        public static void ExecuteCommand(string CommandText, Dictionary<string, object> parameters = null)
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = new MySqlCommand(CommandText.ToLower(), connection);

            if (parameters != null)
            {
                foreach (string param_name in parameters.Keys)
                {
                    command.Parameters.AddWithValue(param_name, parameters[param_name]);
                }
            }

            command.ExecuteNonQuery();

            Disconnect(connection);

        }

        public static void ExecuteScript(string CommandText)
        {
            MySqlConnection connection = Connect();
            MySqlScript script = new MySqlScript(connection, CommandText.ToLower());
            script.Execute();

            Disconnect(connection);

        }

        static MySqlDbType ColumnType(DataColumn data_column)
        {
            if (data_column.DataType.Name == "String")
            {
                return MySqlDbType.String;
            }
            else if (data_column.DataType.Name == "Int32")
            {
                return MySqlDbType.Int32;
            }
            else if (data_column.DataType.Name == "Single")
            {
                return MySqlDbType.Float;
            } 
            else
                return MySqlDbType.Int32;
        }

        public static void WriteToDB(DataTable dt, TableStruct table_struct)
        {
            MySqlConnection connection = Connect();
            MySqlDataAdapter data_adapter = new MySqlDataAdapter("", connection) ;

            data_adapter.DeleteCommand = new MySqlCommand("",connection);
            string DeleteCommandText = "DELETE FROM " + table_struct.TableName.ToLower() + " WHERE";

            foreach (string pk in table_struct.p_keys)
            {

                DeleteCommandText += " " + pk + " = @" + pk;
                data_adapter.DeleteCommand.Parameters.Add("@" + pk, ColumnType(dt.Columns[pk]), 50, pk);
                if (table_struct.p_keys[table_struct.p_keys.Length - 1] != pk)
                    DeleteCommandText += " AND";
            };

            data_adapter.DeleteCommand.CommandText = DeleteCommandText;

            data_adapter.UpdateCommand = new MySqlCommand("",connection);
            string UpdateCommandText = "UPDATE " + table_struct.TableName.ToLower() + " SET";

            foreach (string column in table_struct.columns)
            {
                UpdateCommandText += " " + column + " = @" + column;
                data_adapter.UpdateCommand.Parameters.Add("@" + column, ColumnType(dt.Columns[column]), 50, column);
                if (table_struct.columns[table_struct.columns.Length - 1] != column)
                    UpdateCommandText += ", ";
            };

            UpdateCommandText += " WHERE";

            foreach (string pk in table_struct.p_keys)
            {
                UpdateCommandText += " " + pk + " = @" + pk;
                data_adapter.UpdateCommand.Parameters.Add("@" + pk, ColumnType(dt.Columns[pk]), 50, pk);
                if (table_struct.p_keys[table_struct.p_keys.Length - 1] != pk)
                    UpdateCommandText += " AND";
            };

            data_adapter.UpdateCommand.CommandText = UpdateCommandText;

            data_adapter.InsertCommand = new MySqlCommand("", connection);
            string InsertCommandText = "INSERT INTO " + table_struct.TableName.ToLower() + " SET";

            foreach (string column in table_struct.columns)
            {
                InsertCommandText += " " + column + " = @" + column;
                data_adapter.InsertCommand.Parameters.Add("@" + column, ColumnType(dt.Columns[column]), 50, column);
                InsertCommandText += ", ";
            };
                       
            foreach (string pk in table_struct.p_keys)
            {
                InsertCommandText += " " + pk + " = @" + pk;
                data_adapter.InsertCommand.Parameters.Add("@" + pk, ColumnType(dt.Columns[pk]), 50, pk);
                if (table_struct.p_keys[table_struct.p_keys.Length - 1] != pk)
                    InsertCommandText += ", ";
            };

            data_adapter.InsertCommand.CommandText = InsertCommandText;

            data_adapter.Update(dt);
            dt.AcceptChanges();

            Disconnect(connection);
        }

        public static string GetGlobalParameter(string parameter_name)
        {
            MySqlConnection connection = Connect();
            MySqlCommand command = new MySqlCommand("SELECT parameter_value FROM global_parameters WHERE parameter_name = @parameter_name", connection);
            command.Parameters.AddWithValue("parameter_name", parameter_name);

            object result = command.ExecuteScalar();

            if ((result is string) && ((string)result).Length > 0 )
            {
                return (string)result;
            }
            else
            {
                throw new Exception("Не удалось прочитать глобальный параметр " + parameter_name);
            }
        }


    }
}
