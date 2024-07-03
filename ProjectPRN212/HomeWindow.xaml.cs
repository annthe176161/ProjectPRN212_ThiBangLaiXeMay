using ProjectPRN212.Models;
using System.Windows;

namespace ProjectPRN212
{
    public partial class HomeWindow : Window
    {
        private User currentUser;

        public HomeWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            txtWelcome.Text = $"Chào mừng {currentUser.FullName} đến với ứng dụng thi bằng lái xe máy!";

            // Phân quyền hiển thị các chức năng dựa trên UserType
            if (currentUser.UserType == "Admin")
            {
                adminPanel.Visibility = Visibility.Visible;
            }
            else
            {
                userPanel.Visibility = Visibility.Visible;
            }
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersWindow manageUsersWindow = new ManageUsersWindow();
            manageUsersWindow.Show();
        }

        private void ManageQuestions_Click(object sender, RoutedEventArgs e)
        {
            ManageQuestionsWindow manageQuestionsWindow = new ManageQuestionsWindow();
            manageQuestionsWindow.Show();
        }

        private void ViewAdminActions_Click(object sender, RoutedEventArgs e)
        {
            ViewAdminActionsWindow viewAdminActionsWindow = new ViewAdminActionsWindow();
            viewAdminActionsWindow.Show();
        }

        private void TakeExam_Click(object sender, RoutedEventArgs e)
        {
            TakeExamWindow takeExamWindow = new TakeExamWindow(currentUser);
            takeExamWindow.Show();
        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            ViewHistoryWindow viewHistoryWindow = new ViewHistoryWindow(currentUser);
            viewHistoryWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}