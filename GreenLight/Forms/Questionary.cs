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
    public struct QuestionInfo
    {
        public string question_text;
        public int question_id;
        public string answer_text;
        public int answer_id;
    }

    public partial class Questionary : Form
    {
        string questionary_name = "Анкета клиента";
        int questionary_id;
        int current_question_id;
        List<QuestionInfo> question_history;
        string questionary_table_db_name;
        DataRow row_question;
        DataRow row_table_record;
        public int table_record_id;
        TableStruct ts;
        DataTable dt_table_data;

        //Переменные для универсальной анкеты(с фильтром вопросов)
        bool questionary_use_filter;
        DataTable dt_filter_allowed_questions;  
        int [] filter_ids;


        public Questionary(int t_r_id = -1, string q_name = "Анкета клиента", int [] filt_ids = null)
        {
            InitializeComponent();

            questionary_name = q_name;

            questionary_id = (int)DBFunctions.ReadScalarFromDB("SELECT id FROM questionary WHERE Name = '" + questionary_name + "'");
            questionary_table_db_name = (string)DBFunctions.ReadScalarFromDB("SELECT TableDBName FROM questionary WHERE Name = '" + questionary_name + "'");
            current_question_id = (int)DBFunctions.ReadScalarFromDB("SELECT first_question_id FROM questionary WHERE Name = '" + questionary_name + "'");

            questionary_use_filter = (bool)DBFunctions.ReadScalarFromDB("SELECT use_filter FROM questionary WHERE Name = '" + questionary_name + "'");

            if (questionary_use_filter)
            {
                filter_ids = (int[]) filt_ids.Clone();
                string query_text = "SELECT DISTINCT question_id FROM questionary_filter WHERE questionary_id=@q_id AND filter_id IN (";
                foreach(int filter_id in filter_ids)
                {
                    query_text+=filter_id.ToString() + ",";
                }

                query_text = query_text.TrimEnd(',') + ")";
                
                Dictionary<string,object> parameters = new Dictionary<string,object>();
                parameters.Add("q_id", questionary_id);

                dt_filter_allowed_questions = DBFunctions.ReadFromDB(query_text, parameters);

                //Дойдем до первого разрешенного вопроса
                while(current_question_id != -1)
                {
                    if (dt_filter_allowed_questions.Select("question_id = " + Convert.ToString(current_question_id)).Length > 0)
                        break;

                    parameters = new Dictionary<string, object>();
                    parameters.Add("questionary_id", questionary_id);
                    parameters.Add("q_id", current_question_id);
                    object new_question_id = DBFunctions.ReadScalarFromDB("SELECT question_next FROM questionary_questions WHERE questionary_id=@questionary_id AND question_id = @q_id",parameters);
                    if (new_question_id == DBNull.Value)
                    {
                        current_question_id = -1;
                    }
                    else
                    {
                        current_question_id = (int)new_question_id;
                    }
                }
            }

            question_history = new List<QuestionInfo>();

            table_record_id = t_r_id;
            //Подгрузим инфо о записи таблицы или пустую строку
            ts = new TableStruct();
            dt_table_data = Tables.GetTableWODataGrid(questionary_table_db_name, ref ts, table_record_id);
            if (dt_table_data.Rows.Count > 0)
            {
                row_table_record = dt_table_data.Rows[0];
            }
            else
            {
                row_table_record = dt_table_data.NewRow();
                dt_table_data.Rows.Add(row_table_record);                
            }

        }

        private void LoadQuestion()
        {
            DataTable dt_question = DBFunctions.ReadFromDB("SELECT * FROM questionary_questions WHERE question_id = " +
                Convert.ToString(current_question_id));

            if (dt_question.Rows.Count != 1)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка загрузки вопроса.", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else
            {
                row_question = dt_question.Rows[0];
            }

            lbQuestionText.Text = (string)row_question["question_text"];
            tbAnswer.Text = "";

            if ((string)row_question["question_type"] == "Справочник")
            {
                btnSelect.Visible = true;
                tbAnswer.ReadOnly = true;

                //Самойлов. Подгружаем данные из таблицы в случае их наличия
                if (row_table_record[(string)row_question["question_dest_column_db_name"]] != System.DBNull.Value)
                {
                    tbAnswer.Tag = (int)row_table_record[(string)row_question["question_dest_column_db_name"]];
                    tbAnswer.Text = Tables.GetRefName((string)row_question["question_reference"], (int)tbAnswer.Tag);
                }
            }
            else
            {
                btnSelect.Visible = false;
                tbAnswer.ReadOnly = false;

                //Самойлов. Подгружаем данные из таблицы в случае их наличия
                tbAnswer.Text = Convert.ToString(row_table_record[(string)row_question["question_dest_column_db_name"]]);
            }

            if (question_history.Count == 0)
                btnPrev.Enabled = false;
            else
                btnPrev.Enabled = true;

            if (row_question["question_next"] == DBNull.Value && !(bool)row_question["question_variation"])
                btnNext.Text = "Завершить";
            else
                btnNext.Text = "Следующий >";

        }

        private void UpdateQuestionHistory()
        {
            tbHistory.Text = "";
            foreach (QuestionInfo qi in question_history)
            {
                tbHistory.Text += qi.question_text + ": " + qi.answer_text + System.Environment.NewLine;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            HierarchicalRefEdit hre = new HierarchicalRefEdit();
            //if (current_row[(string)tb.Tag] != System.DBNull.Value)
            //    hre.selected_ids.Add((int)current_row[(string)tb.Tag]);
            hre.reference_db_name = (string)row_question["question_reference"];
            hre.select_mode = true;
            
            if (hre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAnswer.Text = Tables.GetRefName(hre.reference_db_name, hre.selected_ids[0]);
                tbAnswer.Tag = hre.selected_ids[0];
                //current_row[(string)tb.Tag] = hre.selected_ids[0];
                //current_row[(string)tb.Tag + "_Name"] = tb.Text;
            }            
        }

        private void Questionary_Load(object sender, EventArgs e)
        {
            LoadQuestion();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool is_variation = (bool)row_question["question_variation"];


            object next_question = DBNull.Value;
            //Если анкета с фильтром, то щелкаем вопросы, пока не найдем подходящий
            if (!questionary_use_filter)
            {
                
                if (is_variation && tbAnswer.Tag != null)
                {
                    next_question = DBFunctions.ReadScalarFromDB("SELECT answer_next_question_id FROM questionary_answers WHERE question_id = " + current_question_id +
                        " AND answer = " + tbAnswer.Tag);
                };

                if (next_question == DBNull.Value)
                {
                    next_question = row_question["question_next"];
                }
            }
            else
            {
                object answerTag = tbAnswer.Tag;
                
                if (is_variation && tbAnswer.Tag != null)
                {
                    next_question = DBFunctions.ReadScalarFromDB("SELECT answer_next_question_id FROM questionary_answers WHERE question_id = " + current_question_id +
                        " AND answer = " + tbAnswer.Tag);
                };

                if (next_question == DBNull.Value)
                {
                    next_question = row_question["question_next"];
                }

                Dictionary<string, object> parameters;

                while (next_question != DBNull.Value)
                {
                    if (dt_filter_allowed_questions.Select("question_id = " + Convert.ToString(next_question)).Length > 0)
                        break;

                    parameters = new Dictionary<string, object>();
                    parameters.Add("questionary_id", questionary_id);
                    parameters.Add("q_id", next_question);
                    next_question = DBFunctions.ReadScalarFromDB("SELECT question_next FROM questionary_questions WHERE questionary_id=@questionary_id AND question_id = @q_id", parameters);                    
                }

            }

            QuestionInfo qi = new QuestionInfo();
            qi.question_id = current_question_id;
            qi.answer_text = tbAnswer.Text;
            qi.question_text = lbQuestionText.Text;
            try
            {
                qi.answer_id = (int)tbAnswer.Tag;
            }
            catch (Exception) { };

            if ((string)row_question["question_type"] == "Справочник")
            {
                row_table_record[(string)row_question["question_dest_column_db_name"]] = qi.answer_id;
            }
            else
            {
                row_table_record[(string)row_question["question_dest_column_db_name"]] = qi.answer_text;
            }

            question_history.Add(qi);

            UpdateQuestionHistory();

            if (next_question == DBNull.Value)
            {
                //Завершаем анкетирование, пишем в базу
                DBFunctions.WriteToDB(dt_table_data, ts);
                Close();
                return;
            }

            current_question_id = (int)next_question;

            LoadQuestion();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            current_question_id = question_history[question_history.Count-1].question_id;
            
            LoadQuestion();
            tbAnswer.Text = question_history[question_history.Count - 1].answer_text;  

            question_history.RemoveAt(question_history.Count - 1);
            UpdateQuestionHistory();
        }
        
    }
}
