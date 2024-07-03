using ProjectPRN212.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace ProjectPRN212
{
    public partial class AddEditQuestionWindow : Window
    {
        public Question Question { get; private set; }

        public AddEditQuestionWindow()
        {
            InitializeComponent();
            Question = new Question();
        }

        public AddEditQuestionWindow(Question question) : this()
        {
            Question = question;
            txtQuestionText.Text = Question.QuestionText;
            txtOptionA.Text = Question.OptionA;
            txtOptionB.Text = Question.OptionB;
            txtOptionC.Text = Question.OptionC;
            txtOptionD.Text = Question.OptionD;
            cbCorrectOption.SelectedItem = Question.CorrectOption;
            txtImagePath.Text = Question.ImagePath;
            if (!string.IsNullOrEmpty(Question.ImagePath))
            {
                imgQuestion.Source = new BitmapImage(new Uri(Question.ImagePath));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Question.QuestionText = txtQuestionText.Text;
            Question.OptionA = txtOptionA.Text;
            Question.OptionB = txtOptionB.Text;
            Question.OptionC = txtOptionC.Text;
            Question.OptionD = txtOptionD.Text;
            Question.CorrectOption = ((ComboBoxItem)cbCorrectOption.SelectedItem).Content.ToString();
            Question.ImagePath = txtImagePath.Text;
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtImagePath.Text = openFileDialog.FileName;
                imgQuestion.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}