using ProjectPRN212.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ProjectPRN212
{
    public partial class TakeExamWindow : Window
    {
        private ThiBangLaiXeMayContext db;
        private List<Question> questions;
        private int currentQuestionIndex;
        private DispatcherTimer timer;
        private DateTime endTime;
        private User currentUser;
        private List<ExamDetail> examDetails;

        public TakeExamWindow(User user)
        {
            InitializeComponent();
            db = new ThiBangLaiXeMayContext();
            currentUser = user;
            LoadQuestions();
            InitializeTimer();
            examDetails = new List<ExamDetail>();
            DisplayQuestion(0);
        }

        private void LoadQuestions()
        {
            questions = db.Questions.OrderBy(q => Guid.NewGuid()).Take(10).ToList(); // Lấy ngẫu nhiên 10 câu hỏi
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            endTime = DateTime.Now.AddMinutes(30); // Thời gian làm bài là 30 phút
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var remainingTime = endTime - DateTime.Now;
            if (remainingTime <= TimeSpan.Zero)
            {
                timer.Stop();
                SubmitExam();
                return;
            }
            txtTimer.Text = $"Thời gian còn lại: {remainingTime:mm\\:ss}";
        }

        private void DisplayQuestion(int index)
        {
            if (index < 0 || index >= questions.Count) return;
            currentQuestionIndex = index;
            var question = questions[index];
            txtQuestionText.Text = question.QuestionText;
            rbOptionA.Content = question.OptionA;
            rbOptionB.Content = question.OptionB;
            rbOptionC.Content = question.OptionC;
            rbOptionD.Content = question.OptionD;
            if (!string.IsNullOrEmpty(question.ImagePath))
            {
                imgQuestion.Source = new BitmapImage(new Uri(question.ImagePath));
                imgQuestion.Visibility = Visibility.Visible;
            }
            else
            {
                imgQuestion.Visibility = Visibility.Collapsed;
            }

            // Hiển thị câu trả lời đã chọn trước đó nếu có
            var existingDetail = examDetails.FirstOrDefault(ed => ed.QuestionId == question.QuestionId);
            if (existingDetail != null)
            {
                switch (existingDetail.UserAnswer)
                {
                    case "A":
                        rbOptionA.IsChecked = true;
                        break;
                    case "B":
                        rbOptionB.IsChecked = true;
                        break;
                    case "C":
                        rbOptionC.IsChecked = true;
                        break;
                    case "D":
                        rbOptionD.IsChecked = true;
                        break;
                }
            }
            else
            {
                rbOptionA.IsChecked = false;
                rbOptionB.IsChecked = false;
                rbOptionC.IsChecked = false;
                rbOptionD.IsChecked = false;
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentAnswer();
            DisplayQuestion(currentQuestionIndex + 1);
        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentAnswer();
            DisplayQuestion(currentQuestionIndex - 1);
        }

        private void SaveCurrentAnswer()
        {
            var question = questions[currentQuestionIndex];
            var selectedOption = GetSelectedOption();
            if (selectedOption == null) return;

            var existingDetail = examDetails.FirstOrDefault(ed => ed.QuestionId == question.QuestionId);
            if (existingDetail == null)
            {
                examDetails.Add(new ExamDetail
                {
                    QuestionId = question.QuestionId,
                    UserAnswer = selectedOption,
                    IsCorrect = selectedOption == question.CorrectOption
                });
            }
            else
            {
                existingDetail.UserAnswer = selectedOption;
                existingDetail.IsCorrect = selectedOption == question.CorrectOption;
            }
        }

        private string GetSelectedOption()
        {
            if (rbOptionA.IsChecked == true) return "A";
            if (rbOptionB.IsChecked == true) return "B";
            if (rbOptionC.IsChecked == true) return "C";
            if (rbOptionD.IsChecked == true) return "D";
            return null;
        }

        private void SubmitExam_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn nộp bài không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SubmitExam();
            }
        }

        private void SubmitExam()
        {
            SaveCurrentAnswer();
            var exam = new Exam
            {
                UserId = currentUser.UserId,
                ExamDate = DateTime.Now,
                Score = examDetails.Count(ed => ed.IsCorrect)
            };
            db.Exams.Add(exam);
            db.SaveChanges();

            foreach (var detail in examDetails)
            {
                detail.ExamId = exam.ExamId;
                db.ExamDetails.Add(detail);
            }
            db.SaveChanges();

            var userHistory = new UserHistory
            {
                UserId = currentUser.UserId,
                ExamId = exam.ExamId,
                ExamDate = exam.ExamDate,
                Score = exam.Score
            };
            db.UserHistories.Add(userHistory);
            db.SaveChanges();

            MessageBox.Show($"Bài thi đã được nộp. Điểm số của bạn là {exam.Score}/{questions.Count}");
            this.Close();
        }
    }
}