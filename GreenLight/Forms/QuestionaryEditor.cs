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
    public partial class QuestionaryEditor : Form
    {
        string[] questionary_names = { "Анкета клиента", "Универсальная анкета" };
        int questionary_id;
        string questionary_table_db_name;
        bool questionary_use_filter;
        DataTable dt_questions;
        DataTable dt_answers;

        public QuestionaryEditor()
        {
            InitializeComponent();
        }

        private void QuestionaryEditor_Load(object sender, EventArgs e)
        {
            cbQuestionaries.SelectedIndex = 0;
        }
        
        private void LoadData()
        {
            questionary_id = (int)DBFunctions.ReadScalarFromDB("SELECT id FROM questionary WHERE Name = '" + questionary_names[cbQuestionaries.SelectedIndex] + "'");
            questionary_table_db_name = (string)DBFunctions.ReadScalarFromDB("SELECT TableDBName FROM questionary WHERE Name = '" + questionary_names[cbQuestionaries.SelectedIndex] + "'");
            questionary_use_filter = (bool)DBFunctions.ReadScalarFromDB("SELECT use_filter FROM questionary WHERE Name = '" + questionary_names[cbQuestionaries.SelectedIndex] + "'");

            tsbEditFilter.Enabled = questionary_use_filter;

            dt_questions = DBFunctions.ReadFromDB("SELECT * FROM questionary_questions WHERE questionary_id = " + Convert.ToString(questionary_id));

            dt_questions.TableNewRow += new DataTableNewRowEventHandler(dt_TableNewRow);

            dgQuestions.Columns.Clear();
            dgQuestions.DataSource = dt_questions;

            dgQuestions.Columns["question_id"].HeaderText = "ID";
            dgQuestions.Columns["questionary_id"].Visible = false;
            dgQuestions.Columns["question_text"].HeaderText = "Текст вопроса";
            dgQuestions.Columns["question_variation"].HeaderText = "Разветвление";
            dgQuestions.Columns["question_type"].HeaderText = "Тип";
            dgQuestions.Columns["question_next"].HeaderText = "ID след. вопроса";
            dgQuestions.Columns["question_dest_column_db_name"].HeaderText = "Поле таблицы";
            dgQuestions.Columns["question_hint"].HeaderText = "Подсказка";

            dgQuestions.Columns["question_input_type"].HeaderText = "Тип элемента ввода(веб)";
            dgQuestions.Columns["question_web_position"].HeaderText = "Положение на странице(веб)";

            DataGridViewComboBoxColumn column_type = new DataGridViewComboBoxColumn();
            column_type.Items.Add("Строка50");
            column_type.Items.Add("Строка300");
            column_type.Items.Add("Число с плавающей точкой");
            column_type.Items.Add("Справочник");
            column_type.Items.Add("Целое число");
            column_type.Items.Add("Формула");
            column_type.Width = 200;
            column_type.FlatStyle = FlatStyle.Flat;
            column_type.Name = "Тип";
            column_type.DataPropertyName = "question_type";
            dgQuestions.Columns.Remove("question_type");
            dgQuestions.Columns.Add(column_type);

            DataGridViewComboBoxColumn column_ref = new DataGridViewComboBoxColumn();
            DataTable reference_list = DBFunctions.ReadFromDB("SELECT DISTINCT ReferenceDBName FROM referencesconfig");
            foreach (DataRow ref_row in reference_list.Rows)
                column_ref.Items.Add(ref_row["ReferenceDBName"]);
            column_ref.Width = 200;
            column_ref.FlatStyle = FlatStyle.Flat;
            column_ref.Name = "Имя справочника";
            column_ref.DataPropertyName = "question_reference";
            dgQuestions.Columns.Remove("question_reference");
            dgQuestions.Columns.Add(column_ref);

            DataGridViewComboBoxColumn column_table_column = new DataGridViewComboBoxColumn();
            DataTable columns_list = DBFunctions.ReadFromDB("SELECT DISTINCT ColumnDBName FROM tableconfig WHERE ReferenceMultiSelect = 0 AND TableDBName = '" + 
                questionary_table_db_name + "' ORDER BY ColumnDBName");
            foreach (DataRow col_row in columns_list.Rows)
                column_table_column.Items.Add(col_row["ColumnDBName"]);
            column_table_column.Width = 200;
            column_table_column.FlatStyle = FlatStyle.Flat;
            column_table_column.Name = "Поле таблицы";
            column_table_column.DataPropertyName = "question_dest_column_db_name";
            dgQuestions.Columns.Remove("question_dest_column_db_name");
            dgQuestions.Columns.Add(column_table_column);
        }

        private void SaveData()
        {
            TableStruct ts = new TableStruct();
            ts.TableName = "questionary_questions";
            string[] p_keys = { "question_id" };
            ts.p_keys = p_keys;
            string[] columns = { "questionary_id", "question_text", "question_variation","question_reference", 
                                   "question_type", "question_next", "question_dest_column_db_name", "question_hint","question_input_type","question_web_position" };
            ts.columns = columns;

            DBFunctions.WriteToDB(dt_questions, ts);

            DBStructure.UpdateDBStructure();
        }

        private void LoadAnswers(int question_id)
        {
            dt_answers = DBFunctions.ReadFromDB("SELECT * FROM questionary_answers WHERE question_id = " + Convert.ToString(question_id));

            dt_answers.TableNewRow += new DataTableNewRowEventHandler(dt_TableAnswersNewRow);

            dgAnswers.DataSource = dt_answers;
        }

        private void dt_TableAnswersNewRow(object sender, DataTableNewRowEventArgs e)
        {
            DataRow row = Samoyloff.Tools.FindCurrentRow(dgQuestions);

            e.Row["question_id"] = (int)row["question_id"];            
        }

        private void QuestionaryEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row["questionary_id"] = questionary_id;
            e.Row["question_variation"] = false;
        }

        private void dgQuestions_CurrentCellChanged(object sender, EventArgs e)
        {
            DataRow row = Samoyloff.Tools.FindCurrentRow(dgQuestions);

            if (splitContainer.Panel2Collapsed == false)
            {
                //Значит есть ветвление, значит надо сохранить
                TableStruct ts = new TableStruct();
                ts.TableName = "questionary_answers";
                string[] p_keys = { "question_id","answer" };
                ts.p_keys = p_keys;
                string[] columns = { "answer_next_question_id" };
                ts.columns = columns;

                DBFunctions.WriteToDB(dt_answers, ts);
            }

            if ((bool)row["question_variation"] == false)
            {
                splitContainer.Panel2Collapsed = true;
            }
            else
            {
                splitContainer.Panel2Collapsed = false;
                LoadAnswers((int)row["question_id"]);
            }
        }

        private void cbQuestionaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tsbEditFilter_Click(object sender, EventArgs e)
        {            
            QuestionaryFilterEditor qfe = new QuestionaryFilterEditor(questionary_id);
            qfe.Show();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveData();
            LoadData();
        }
     
    }
}
