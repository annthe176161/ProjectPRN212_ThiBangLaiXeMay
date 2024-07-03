using ProjectPRN212.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjectPRN212
{
    public partial class ForgotPasswordWindow : Window
    {
        ThiBangLaiXeMayContext db = new ThiBangLaiXeMayContext();

        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var username = txtForgotUsername.Text;
            var email = txtForgotEmail.Text;
            var newPassword = txtForgotPassword.Password;
            if (txtForgotPasswordVisible.Visibility == Visibility.Visible)
            {
                newPassword = txtForgotPasswordVisible.Text;
            }
            var confirmPassword = txtForgotConfirmPassword.Password;
            if (txtForgotConfirmPasswordVisible.Visibility == Visibility.Visible)
            {
                confirmPassword = txtForgotConfirmPasswordVisible.Text;
            }

            bool isValid = true;

            // Reset thông báo lỗi
            txtForgotUsernameError.Text = string.Empty;
            txtForgotEmailError.Text = string.Empty;
            txtForgotPasswordError.Text = string.Empty;
            txtForgotConfirmPasswordError.Text = string.Empty;

            // Kiểm tra tính hợp lệ của từng trường nhập liệu
            if (!ValidateUsername(username, out string usernameError))
            {
                txtForgotUsernameError.Text = usernameError;
                isValid = false;
            }

            if (!ValidateEmail(email, out string emailError))
            {
                txtForgotEmailError.Text = emailError;
                isValid = false;
            }

            if (!ValidatePassword(newPassword, out string passwordError))
            {
                txtForgotPasswordError.Text = passwordError;
                isValid = false;
            }

            if (!ValidateConfirmPassword(newPassword, confirmPassword, out string confirmPasswordError))
            {
                txtForgotConfirmPasswordError.Text = confirmPasswordError;
                isValid = false;
            }

            if (!isValid) return;

            // Kiểm tra xem tên đăng nhập và email có khớp không
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Email == email);

            if (user == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc email không đúng.");
                return;
            }

            // Cập nhật mật khẩu mới cho người dùng
            user.Password = newPassword;
            db.SaveChanges();

            MessageBox.Show("Đặt lại mật khẩu thành công!");

            // Quay trở lại trang Đăng nhập
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private bool ValidateUsername(string username, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(username))
            {
                error = "Tên đăng nhập không được để trống.";
                return false;
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                error = "Tên đăng nhập không được chứa ký tự đặc biệt hoặc khoảng trắng.";
                return false;
            }

            return true;
        }

        private bool ValidateEmail(string email, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(email))
            {
                error = "Email không được để trống.";
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$"))
            {
                error = "Email không hợp lệ. Chỉ chấp nhận email có đuôi @gmail.com.";
                return false;
            }

            return true;
        }

        private bool ValidatePassword(string password, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(password))
            {
                error = "Mật khẩu không được để trống.";
                return false;
            }

            if (password.Length < 6)
            {
                error = "Mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }

            if (password.Contains(" "))
            {
                error = "Mật khẩu không được chứa khoảng trắng.";
                return false;
            }

            return true;
        }

        private bool ValidateConfirmPassword(string password, string confirmPassword, out string error)
        {
            error = string.Empty;

            if (password != confirmPassword)
            {
                error = "Mật khẩu và xác nhận mật khẩu không khớp.";
                return false;
            }

            return true;
        }

        private void ShowForgotPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtForgotPasswordVisible.Text = txtForgotPassword.Password;
            txtForgotPasswordVisible.Visibility = Visibility.Visible;
            txtForgotPassword.Visibility = Visibility.Collapsed;
        }

        private void ShowForgotPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtForgotPassword.Password = txtForgotPasswordVisible.Text;
            txtForgotPasswordVisible.Visibility = Visibility.Collapsed;
            txtForgotPassword.Visibility = Visibility.Visible;
        }

        private void ShowForgotConfirmPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtForgotConfirmPasswordVisible.Text = txtForgotConfirmPassword.Password;
            txtForgotConfirmPasswordVisible.Visibility = Visibility.Visible;
            txtForgotConfirmPassword.Visibility = Visibility.Collapsed;
        }

        private void ShowForgotConfirmPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtForgotConfirmPassword.Password = txtForgotConfirmPasswordVisible.Text;
            txtForgotConfirmPasswordVisible.Visibility = Visibility.Collapsed;
            txtForgotConfirmPassword.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}