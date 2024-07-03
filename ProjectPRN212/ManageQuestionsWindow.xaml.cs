using ProjectPRN212.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjectPRN212
{
    public partial class ManageQuestionsWindow : Window
    {
        private ThiBangLaiXeMayContext db;
        private ObservableCollection<Question> questions;

        public ManageQuestionsWindow()
        {
            InitializeComponent();
            db = new ThiBangLaiXeMayContext();
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions = new ObservableCollection<Question>(db.Questions.ToList());
            dataGridQuestions.ItemsSource = questions;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var addQuestionWindow = new AddEditQuestionWindow();
            if (addQuestionWindow.ShowDialog() == true)
            {
                db.Questions.Add(addQuestionWindow.Question);
                db.SaveChanges();
                LoadQuestions();
            }
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridQuestions.SelectedItem is Question selectedQuestion)
            {
                var editQuestionWindow = new AddEditQuestionWindow(selectedQuestion);
                if (editQuestionWindow.ShowDialog() == true)
                {
                    var question = db.Questions.FirstOrDefault(q => q.QuestionId == selectedQuestion.QuestionId);
                    if (question != null)
                    {
                        question.QuestionText = editQuestionWindow.Question.QuestionText;
                        question.OptionA = editQuestionWindow.Question.OptionA;
                        question.OptionB = editQuestionWindow.Question.OptionB;
                        question.OptionC = editQuestionWindow.Question.OptionC;
                        question.OptionD = editQuestionWindow.Question.OptionD;
                        question.CorrectOption = editQuestionWindow.Question.CorrectOption;
                        question.ImagePath = editQuestionWindow.Question.ImagePath;
                        db.SaveChanges();
                        LoadQuestions();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn câu hỏi để chỉnh sửa.");
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridQuestions.SelectedItem is Question selectedQuestion)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa câu hỏi này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    db.Questions.Remove(selectedQuestion);
                    db.SaveChanges();
                    LoadQuestions();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn câu hỏi để xóa.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}