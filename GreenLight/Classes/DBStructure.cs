using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GreenLight
{
    class DBStructure
    {
        public static string ConvertTypeToSQL(string type)
        {
            switch (type)
            {
                case "Строка50":
                    return "varchar(50)";
                case "Строка300":
                    return "TEXT";
                case "Справочник":
                case "Целое число":
                    return "int(11)";
                case "Число с плавающей точкой":
                    return "float";
                case "Формула":
                    return "text";
                default:
                    return null;
            }

        }

        static void CreateMultiRefTable(string TableDBName, string ColumnDBName)
        {
            try//Вдруг такая таблица уже есть?
            {
                DBFunctions.ExecuteCommand("CREATE TABLE `MultiRef_"+TableDBName+"_"+ColumnDBName+"` (`TableID` int(11) NOT NULL,`RefID` int(11) NOT NULL, PRIMARY KEY (`TableID`,`RefID`)) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8");
            } catch(Exception) {}

        }

        static void DropMultiRefTable(string TableDBName, string ColumnDBName)
        {
            try
            {
                DBFunctions.ExecuteCommand("DROP TABLE `MultiRef_" + TableDBName + "_" + ColumnDBName + "`");
            }
            catch (Exception) { }
        }

        static void UpdateMainTableStructure(string TableDBName)
        {
            //Получим требуемую структуру
            DataTable neededStructure = DBFunctions.ReadFromDB("SELECT TableConfigID,ColumnDBName,ColumnDBName_Old,ColumnType,ReferenceMultiSelect FROM TableConfig WHERE TableDBName = '" + TableDBName + "'");

            //Получим текущую структуру
            DataTable CurrentStrurture = new DataTable();
            bool new_table = false;
            try
            {
                CurrentStrurture = DBFunctions.ReadFromDB("SHOW COLUMNS FROM Table_" + TableDBName);
            }
            catch (Exception)
            {
                //Таблицы не существует, будем создавать
                new_table = true;
            }

            if (new_table)
            {
                string CommandText = "CREATE TABLE `Table_" + TableDBName + "` (";

                //ПК
                CommandText += "`ID` int(11) NOT NULL AUTO_INCREMENT";

                foreach (DataRow row in neededStructure.Rows)
                {
                    if ((string)row["ColumnType"] == "Справочник" && (bool)row["ReferenceMultiSelect"] == true)
                    {
                        CommandText += ",`" + row["ColumnDBName"] + "` MEDIUMTEXT DEFAULT NULL";
                        //CreateMultiRefTable(TableDBName, (string)row["ColumnDBName"]);
                    }
                    {
                        string field_type = ConvertTypeToSQL((string)row["ColumnType"]);
                        if (field_type != null)
                        {
                            CommandText += ",`" + row["ColumnDBName"] + "` " + field_type + " DEFAULT NULL";
                        }
                    }
                }

                CommandText += ", PRIMARY KEY (`ID`)) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8";

                DBFunctions.ExecuteCommand(CommandText);

            }
            else
            {                
                //Создаем и меняем строки
                foreach (DataRow row in neededStructure.Rows)
                {
                    string col_type;
                    if ((string)row["ColumnType"] == "Справочник" && (bool)row["ReferenceMultiSelect"] == true)
                    {
                        //CreateMultiRefTable(TableDBName, (string)row["ColumnDBName"]);
                        col_type = "MEDIUMTEXT";
                    }
                    else
                    { 
                        col_type = ConvertTypeToSQL((string)row["ColumnType"]);
                    }
                    {
                        //Ищем колонку
                        DataRow[] foundRows = CurrentStrurture.Select("Field = '" + row["ColumnDBName_Old"] + "'");
                        if (foundRows.Length == 0)
                        {
                            //Добавляем колонку
                            DBFunctions.ExecuteCommand("ALTER TABLE Table_" + TableDBName + " ADD `" + row["ColumnDBName"] + "` " + col_type);
                        }
                        else
                        {
                            //Проверяем соответствие имени
                            if ((string)foundRows[0]["Field"] != (string)row["ColumnDBName"])
                            {
                                //Переименовываем колонку
                                try
                                {
                                    DBFunctions.ExecuteCommand("ALTER TABLE `Table_" + TableDBName + "` CHANGE `" + row["ColumnDBName_Old"] + "` `" + row["ColumnDBName"] + "` " + col_type);
                                    row["ColumnDBName_Old"] = row["ColumnDBName"];
                                }
                                catch (Exception)
                                {
                                    System.Windows.Forms.MessageBox.Show("Невозможно изменить тип столбца.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }

                            }
                            else if ((string)foundRows[0]["Type"] != col_type)
                            {
                                //Меняем тип
                                try
                                {
                                    DBFunctions.ExecuteCommand("ALTER TABLE `Table_" + TableDBName + "` MODIFY `" + row["ColumnDBName"] + "` " + col_type);
                                }
                                catch (Exception)
                                {
                                    System.Windows.Forms.MessageBox.Show("Невозможно изменить тип столбца.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }

                //Внесем изменения конфигурационную таблицу
                if (neededStructure.GetChanges() != null)
                {
                    TableStruct ts = new TableStruct();
                    ts.TableName = "TableConfig";
                    string[] p_keys = { "TableConfigID" };
                    ts.p_keys = p_keys;
                    string[] columns = { "ColumnDBName_Old" };
                    ts.columns = columns;

                    DBFunctions.WriteToDB(neededStructure, ts);
                }

                //Удалим лишние колонки
                foreach (DataRow row in CurrentStrurture.Rows)
                {
                    if ((string)row["Field"] == "ID")
                        continue;
                    DataRow[] foundRows = neededStructure.Select("ColumnDBName = '" + row["Field"] + "'");
                    if (foundRows.Length == 0)
                    {
                        //Удаляем колонку
                        DBFunctions.ExecuteCommand("ALTER TABLE Table_" + TableDBName + " DROP `" + row["Field"] + "`");
                    }

                }

                //Удалим лишние таблицы с множественным выбором
                //DataTable multi_ref_tables = DBFunctions.ReadFromDB("SHOW TABLES WHERE tables_in_"  + DBFunctions.db_name + " LIKE 'MultiRef_" + TableDBName + "_%'");
                //foreach(DataRow row in multi_ref_tables.Rows)
                //{
                //   DataRow[] foundRows = neededStructure.Select("'MultiRef_" + TableDBName + "_' + ColumnDBName = '" + row["tables_in_" + DBFunctions.db_name] + "' AND ReferenceMultiSelect = 1");
                /*    if (foundRows.Length == 0)
                    {
                        //Удаляем таблицу
                        DBFunctions.ExecuteCommand("DROP TABLE `" + row["tables_in_" + DBFunctions.db_name] + "`");
                        
                    }
                }*/
            }           

        }

        static void UpdateReferenceStructure(string ref_db_name)
        {
            //Получим требуемую структуру
            DataTable neededStructure = DBFunctions.ReadFromDB("SELECT * FROM referencesconfig WHERE ReferenceDBName = '" + ref_db_name + "'");

            if (neededStructure.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Неверное наименование справочника.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            bool hierarchical = Convert.ToBoolean(neededStructure.Rows[0]["Hierarchycal"]);

            //Проверим есть ли такая таблица в БД
            bool ref_data_exists = Convert.ToBoolean(DBFunctions.ReadFromDB("Show tables like 'ref_data_" + ref_db_name + "'").Rows.Count);
            bool ref_hierarchy_exists = Convert.ToBoolean(DBFunctions.ReadFromDB("Show tables like 'ref_hierarchy_" + ref_db_name + "'").Rows.Count);

            if ((hierarchical && ref_data_exists && ref_hierarchy_exists) || //Иерархический, есть обе таблицы
                (!hierarchical && ref_data_exists && !ref_hierarchy_exists) //Не иерархический, есть одна таблица
                )
            {
                //Всё ок, проверим соответствие состава колонок
                DataTable CurrentStrurture = DBFunctions.ReadFromDB("SHOW COLUMNS FROM ref_data_" + ref_db_name);

                //Создаем и меняем строки
                foreach (DataRow row in neededStructure.Rows)
                {
                    //Ищем колонку
                    DataRow[] foundRows = CurrentStrurture.Select("Field = '" + row["ColumnDBName_Old"] + "'");
                    if (foundRows.Length == 0)
                    {
                        //Добавляем колонку
                        DBFunctions.ExecuteCommand("ALTER TABLE ref_data_" + ref_db_name + " ADD `" + row["ColumnDBName"] + "` " + ConvertTypeToSQL((string)row["ColumnType"]));
                    }
                    else
                    {
                        //Проверяем соответствие имени
                        if ((string)foundRows[0]["Field"] != (string)row["ColumnDBName"])
                        {
                            //Переименовываем колонку
                            try
                            {
                                DBFunctions.ExecuteCommand("ALTER TABLE `ref_data_" + ref_db_name + "` CHANGE `" + row["ColumnDBName_Old"] + "` `" + row["ColumnDBName"] + "` " + ConvertTypeToSQL((string)row["ColumnType"]));
                                row["ColumnDBName_Old"] = row["ColumnDBName"];
                            }
                            catch (Exception)
                            {
                                System.Windows.Forms.MessageBox.Show("Невозможно изменить тип столбца.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }

                        }
                        else if ((string)foundRows[0]["Type"] != ConvertTypeToSQL((string)row["ColumnType"]))
                        {
                            //Меняем тип
                            try
                            {
                                DBFunctions.ExecuteCommand("ALTER TABLE `ref_data_" + ref_db_name + "` MODIFY `" + row["ColumnDBName"] + "` " + ConvertTypeToSQL((string)row["ColumnType"]));
                            }
                            catch (Exception)
                            {
                                System.Windows.Forms.MessageBox.Show("Невозможно изменить тип столбца.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                    }

                }

                //Внесем изменения конфигурационную таблицу
                if (neededStructure.GetChanges() != null)
                {
                    TableStruct ts = new TableStruct();
                    ts.TableName = "ReferencesConfig";
                    string[] p_keys = { "ReferenceConfigID" };
                    ts.p_keys = p_keys;
                    string[] columns = { "ColumnDBName_Old" };
                    ts.columns = columns;

                    DBFunctions.WriteToDB(neededStructure, ts);
                }

                //Удалим лишние колонки
                foreach (DataRow row in CurrentStrurture.Rows)
                {
                    if ((string)row["Field"] == "ID")
                        continue;
                    if ((string)row["Field"] == "ParentID")
                        continue;
                    DataRow[] foundRows = neededStructure.Select("ColumnDBName = '" + row["Field"] + "'");
                    if (foundRows.Length == 0)
                    {
                        //Удаляем колонку
                        DBFunctions.ExecuteCommand("ALTER TABLE ref_data_" + ref_db_name + " DROP `" + row["Field"] + "`");
                    }

                }

                //Создадим хранимые процедуры и триггеры
                string ref_create_script = (string)DBFunctions.ReadScalarFromDB("SELECT script FROM scripts WHERE script_name = 'Reference_Create'");

                ref_create_script = ref_create_script.Replace("[RefDBName]", ref_db_name.ToLower());

                DBFunctions.ExecuteScript(ref_create_script);

                return;
            }

            if (ref_data_exists || ref_hierarchy_exists)
            {
                if (System.Windows.Forms.MessageBox.Show("Сменился тип справочника " + ref_db_name + " данные справочника будут удалены. Продолжить?", "Вопрос", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            
            //Удалим таблицы
            if (ref_data_exists)
            {
                DBFunctions.ExecuteCommand("DROP TABLE `ref_data_" + ref_db_name + "`");
            }

            if (ref_hierarchy_exists)
            {
                DBFunctions.ExecuteCommand("DROP TABLE `ref_hierarchy_" + ref_db_name + "`");
            }

            //Создадим новые таблицы
            //Таблица с данными
            string CommandText = "CREATE TABLE `ref_data_" + ref_db_name + "` (";
            CommandText += "`ID` int(11) NOT NULL AUTO_INCREMENT";
            CommandText += ",`ParentID` int(11) DEFAULT 0";
            

            foreach (DataRow row in neededStructure.Rows)
            {
                if (row["ColumnType"] == System.DBNull.Value)
                    continue;
                string field_type = ConvertTypeToSQL((string)row["ColumnType"]);
                if (field_type != null)
                {
                    CommandText += ",`" + row["ColumnDBName"] + "` " + field_type + " DEFAULT NULL";
                }
            }

            CommandText += ", PRIMARY KEY (`ID`)) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8";
            DBFunctions.ExecuteCommand(CommandText);

            //Таблица с иерархией
            if (hierarchical)
            {
                CommandText = "CREATE TABLE `ref_hierarchy_" + ref_db_name + "` (";
                CommandText += "`ElemID` int(11) NOT NULL";
                CommandText += ",`ParentID` int(11) NOT NULL";
                CommandText += ",`Level` int(11) NOT NULL";
                CommandText += ", PRIMARY KEY (`ElemID`,`ParentID`,`Level`)) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8";
                DBFunctions.ExecuteCommand(CommandText);
            }

            //Создадим хранимые процедуры и триггеры
            string ref_create_script_inner = (string)DBFunctions.ReadScalarFromDB("SELECT script FROM scripts WHERE script_name = 'Reference_Create'");

            ref_create_script_inner = ref_create_script_inner.Replace("[RefDBName]", ref_db_name.ToLower());

            DBFunctions.ExecuteScript(ref_create_script_inner);

        }

        public static void UpdateDBStructure()
        {
            //Получим список главных таблиц
            DataTable MainTables = DBFunctions.ReadFromDB("SELECT DISTINCT TableDBName FROM TableConfig");
            foreach (DataRow row in MainTables.Rows)
            {
                UpdateMainTableStructure((string)row["TableDBName"]);
            }

            //Списки справочников
            DataTable References = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceDBName FROM referencesconfig");
            foreach (DataRow row in References.Rows)
            {
                UpdateReferenceStructure((string)row["ReferenceDBName"]);
            }
        }

        public static void UpdateSelectorScript()
        {
            //Получим таблицу с формулами
            //Список формульных полей
            DataTable dt_formula_fields = DBFunctions.ReadFromDB("SELECT concat('table_',TableDBName,'.',ColumnDBName) AS field FROM TableConfig WHERE ColumnType = 'Формула'");

            string formula_query_text = "SELECT ID";

            foreach (DataRow row in dt_formula_fields.Rows)
            {
                formula_query_text += "," + Convert.ToString(row["field"]).ToLower() + " AS '" + Convert.ToString(row["field"]).ToLower() + "'";
            }

            formula_query_text += " FROM table_credprogr";

            DataTable dt_formulas = DBFunctions.ReadFromDB(formula_query_text);

            //Получим текст условия
            string clause_text;
            DataTable dt_clause = DBFunctions.ReadFromDB("SELECT * FROM where_clauses");
            if (dt_clause.Rows.Count > 0)
            {
                clause_text = (string)dt_clause.Rows[0]["Clause"];
            }
            else
            {
                return;
            }

            //Соберем текст запроса
            string query_text = "SELECT table_credprogr.ID";
            DataTable all_fields = DBFunctions.ReadFromDB("SELECT concat('table_',TableDBName,'.',ColumnDBName) AS field_name,ColumnName,ColumnType,ReferenceMultiSelect,ShowInOffer,TableDBName,ColumnDBName FROM tableconfig WHERE ShowInOffer = 1 OR UseInWhereClause = 1 OR ShowInOfferShort = 1");

            if (all_fields.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Не выбраны поля для отображения", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
              
            foreach (DataRow row in all_fields.Rows)
            {
                query_text += ",";
                
                if ((string)row["ColumnType"] == "Формула")
                {
                    query_text += "CASE table_credprogr.ID ";
                    foreach (DataRow formula_row in dt_formulas.Rows)
                    {
                        query_text += " WHEN " + formula_row["ID"] + " THEN " + formula_row[(string)row["field_name"]];
                    }
                    query_text += " END AS '" + row["ColumnDBName"] + "'";
                }                
                else
                {
                    query_text += Convert.ToString(row["field_name"]).ToLower() + " AS '" + row["ColumnDBName"] + "'";
                }
            }

            query_text += " FROM table_credprogr LEFT JOIN table_clients ON table_clients.id=" + "[ClientID] ";
            
            //Здесь формируем запрос по полям с ShowInOffer = 1
            DataTable fields_to_show = DBFunctions.ReadFromDB("SELECT ColumnDBName,ColumnName,ColumnType,ColumnReference,ReferenceMultiSelect,ShowFullName FROM tableconfig WHERE ShowInOffer = 1 ORDER BY WebOrder");

            string itog_query = "SELECT DISTINCT inner_select.ID";

            string join_text = "";

            foreach (DataRow row in fields_to_show.Rows)
            {
                itog_query += ",";

                if ((string)row["ColumnType"] == "Справочник" && (bool)row["ReferenceMultiSelect"] == false)
                {
                    itog_query += "ref_name_" + Convert.ToString(row["ColumnReference"]) + "(" + row["ColumnDBName"] + ","+ Convert.ToString(row["ShowFullName"]) +") AS '" + row["ColumnName"] + "'";                    
                }
                else if ((string)row["ColumnType"] == "Справочник" && (bool)row["ReferenceMultiSelect"] == true)
                {
                    itog_query += "multiref_names_" + Convert.ToString(row["ColumnReference"]) + "(" + row["ColumnDBName"] + ")" + " AS '" + row["ColumnName"] + "'";
                }
                else
                {
                    itog_query += row["ColumnDBName"] + " AS '" + row["ColumnName"] + "'";
                }
            }
            
            itog_query += " FROM (" + query_text + ") AS inner_select " + join_text + " WHERE ";

            itog_query += clause_text.Replace("@", ""); ;

            //Занесем результат в базу

            //Удалим текущий скрипт
            DBFunctions.ExecuteCommand("DELETE FROM scripts WHERE script_name = 'OfferSelect'");

            DBFunctions.ExecuteCommand("INSERT INTO scripts VALUES('OfferSelect','" + itog_query.Replace("'", "\\'") + "')");


            //Здесь формируем запрос по полям с ShowInOfferShort = 1
            fields_to_show = DBFunctions.ReadFromDB("SELECT ColumnDBName,ColumnName,ColumnType,ColumnReference,ReferenceMultiSelect,ShowFullName FROM tableconfig WHERE ShowInOfferShort = 1 ORDER BY WebOrder");

            itog_query = "SELECT DISTINCT inner_select.ID";

            foreach (DataRow row in fields_to_show.Rows)
            {
                itog_query += ",";

                if ((string)row["ColumnType"] == "Справочник" && (bool)row["ReferenceMultiSelect"] == false)
                {
                    itog_query += "ref_name_" + Convert.ToString(row["ColumnReference"]) + "(" + row["ColumnDBName"] + "," + Convert.ToString(row["ShowFullName"]) + ") AS '" + row["ColumnName"] + "'";
                }
                else if ((string)row["ColumnType"] == "Справочник" && (bool)row["ReferenceMultiSelect"] == true)
                {
                    itog_query += "multiref_names_" + Convert.ToString(row["ColumnReference"]) + "(" + row["ColumnDBName"] + ")" + " AS '" + row["ColumnName"] + "'";
                }
                else
                {
                    itog_query += row["ColumnDBName"] + " AS '" + row["ColumnName"] + "'";
                }
            }

            itog_query += " FROM (" + query_text + ") AS inner_select  WHERE ";

            itog_query += clause_text.Replace("@", "");

            //Занесем результат в базу

            //Удалим текущий скрипт
            DBFunctions.ExecuteCommand("DELETE FROM scripts WHERE script_name = 'OfferSelectShort'");

            DBFunctions.ExecuteCommand("INSERT INTO scripts VALUES('OfferSelectShort','" + itog_query.Replace("'", "\\'") + "')");

        }
    }
}
