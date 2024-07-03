using ProjectPRN212.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjectPRN212
{
    public partial class ViewHistoryWindow : Window
    {
        private ThiBangLaiXeMayContext db;
        private User currentUser;
        private ObservableCollection<Exam> exams;
        private ObservableCollection<dynamic> examDetails;

        public ViewHistoryWindow(User user)
        {
            InitializeComponent();
            db = new ThiBangLaiXeMayContext();
            currentUser = user;
            LoadExamHistory();
        }

        private void LoadExamHistory()
        {
            exams = new ObservableCollection<Exam>(db.Exams.Where(e => e.UserId == currentUser.UserId).ToList());
            dataGridHistory.ItemsSource = exams;
        }

        private void DataGridHistory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataGridHistory.SelectedItem is Exam selectedExam)
            {
                var details = from ed in db.ExamDetails
                              join q in db.Questions on ed.QuestionId equals q.QuestionId
                              where ed.ExamId == selectedExam.ExamId
                              select new
                              {
                                  ed.QuestionId,
                                  q.QuestionText,
                                  ed.UserAnswer,
                                  ed.IsCorrect
                              };
                examDetails = new ObservableCollection<dynamic>(details.ToList());
                dataGridExamDetails.ItemsSource = examDetails;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}