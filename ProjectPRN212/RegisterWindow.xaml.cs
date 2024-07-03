using ProjectPRN212.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProjectPRN212
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        ThiBangLaiXeMayContext db = new ThiBangLaiXeMayContext();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = txtRegisterUsername.Text;
            var password = txtRegisterPassword.Password;
            if (txtRegisterPasswordVisible.Visibility == Visibility.Visible)
            {
                password = txtRegisterPasswordVisible.Text;
            }

            var confirmPassword = txtConfirmPassword.Password;
            if (txtConfirmPasswordVisible.Visibility == Visibility.Visible)
            {
                confirmPassword = txtConfirmPasswordVisible.Text;
            }

            var fullName = txtFullName.Text;
            var email = txtEmail.Text;
            var dateOfBirth = dpDateOfBirth.SelectedDate;
            var phoneNumber = txtPhoneNumber.Text;

            bool isValid = true;

            // Reset thông báo lỗi
            txtUsernameError.Text = string.Empty;
            txtPasswordError.Text = string.Empty;
            txtConfirmPasswordError.Text = string.Empty;
            txtFullNameError.Text = string.Empty;
            txtEmailError.Text = string.Empty;
            txtDateOfBirthError.Text = string.Empty;
            txtPhoneNumberError.Text = string.Empty;

            // Kiểm tra tính hợp lệ của từng trường nhập liệu
            if (!ValidateUsername(username, out string usernameError))
            {
                txtUsernameError.Text = usernameError;
                isValid = false;
            }

            if (!ValidatePassword(password, out string passwordError))
            {
                txtPasswordError.Text = passwordError;
                isValid = false;
            }

            if (!ValidateConfirmPassword(password, confirmPassword, out string confirmPasswordError))
            {
                txtConfirmPasswordError.Text = confirmPasswordError;
                isValid = false;
            }

            if (!ValidateFullName(fullName, out string fullNameError))
            {
                txtFullNameError.Text = fullNameError;
                isValid = false;
            }

            if (!ValidateEmail(email, out string emailError))
            {
                txtEmailError.Text = emailError;
                isValid = false;
            }

            if (!ValidateDateOfBirth(dateOfBirth, out string dateOfBirthError))
            {
                txtDateOfBirthError.Text = dateOfBirthError;
                isValid = false;
            }

            if (!ValidatePhoneNumber(phoneNumber, out string phoneNumberError))
            {
                txtPhoneNumberError.Text = phoneNumberError;
                isValid = false;
            }

            if (!isValid) return;

            // Kiểm tra xem tên đăng nhập đã tồn tại hay chưa
            if (db.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.");
                return;
            }

            // Kiểm tra xem email đã tồn tại hay chưa
            if (db.Users.Any(u => u.Email == email))
            {
                MessageBox.Show("Email đã được sử dụng.");
                return;
            }

            // Tạo đối tượng User mới
            var newUser = new User
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Email = email,
                UserType = "User",
                DateOfBirth = DateOnly.FromDateTime(dateOfBirth.Value),
                PhoneNumber = phoneNumber
            };

            // Thêm người dùng mới vào cơ sở dữ liệu
            db.Users.Add(newUser);
            db.SaveChanges();

            MessageBox.Show("Đăng ký thành công!");

            // Quay trở lại trang Đăng nhập
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // Kiểm tra tính hợp lệ của tên đăng nhập
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

        // Kiểm tra tính hợp lệ của mật khẩu
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

        // Kiểm tra tính hợp lệ của xác nhận mật khẩu
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

        // Kiểm tra tính hợp lệ của họ và tên
        private bool ValidateFullName(string fullName, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                error = "Họ và tên không được để trống.";
                return false;
            }

            // Loại bỏ khoảng trắng không cần thiết
            fullName = fullName.Trim();

            if (!Regex.IsMatch(fullName, @"^[\p{L}\s]+$"))
            {
                error = "Họ và tên không được chứa ký tự đặc biệt hoặc số.";
                return false;
            }

            if (Regex.IsMatch(fullName, @"(^\s)|(\s$)|(\s\s+)"))
            {
                error = "Họ và tên không được chứa khoảng trắng ở đầu, cuối hoặc giữa.";
                return false;
            }

            return true;
        }

        // Kiểm tra tính hợp lệ của email
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

        // Kiểm tra tính hợp lệ của ngày sinh
        private bool ValidateDateOfBirth(DateTime? dateOfBirth, out string error)
        {
            error = string.Empty;

            if (dateOfBirth == null)
            {
                error = "Ngày sinh không được để trống.";
                return false;
            }

            if (dateOfBirth > DateTime.Now)
            {
                error = "Ngày sinh không được là ngày trong tương lai.";
                return false;
            }

            var age = DateTime.Now.Year - dateOfBirth.Value.Year;
            if (age > 200)
            {
                error = "Ngày sinh không được cách hiện tại hơn 200 năm.";
                return false;
            }

            if (age < 16 || (age == 16 && DateTime.Now.DayOfYear < dateOfBirth.Value.DayOfYear))
            {
                error = "Tuổi phải trên 16.";
                return false;
            }

            return true;
        }

        // Kiểm tra tính hợp lệ của số điện thoại
        private bool ValidatePhoneNumber(string phoneNumber, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                error = "Số điện thoại không được để trống.";
                return false;
            }

            if (!Regex.IsMatch(phoneNumber, @"^0[1-9]\d{8}$"))
            {
                error = "Số điện thoại không hợp lệ. Phải bắt đầu bằng số 0 và có đúng 10 chữ số.";
                return false;
            }

            return true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ShowRegisterPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtRegisterPasswordVisible.Text = txtRegisterPassword.Password;
            txtRegisterPasswordVisible.Visibility = Visibility.Visible;
            txtRegisterPassword.Visibility = Visibility.Collapsed;
        }

        private void ShowRegisterPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtRegisterPassword.Password = txtRegisterPasswordVisible.Text;
            txtRegisterPasswordVisible.Visibility = Visibility.Collapsed;
            txtRegisterPassword.Visibility = Visibility.Visible;
        }

        private void ShowConfirmPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtConfirmPasswordVisible.Text = txtConfirmPassword.Password;
            txtConfirmPasswordVisible.Visibility = Visibility.Visible;
            txtConfirmPassword.Visibility = Visibility.Collapsed;
        }

        private void ShowConfirmPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtConfirmPassword.Password = txtConfirmPasswordVisible.Text;
            txtConfirmPasswordVisible.Visibility = Visibility.Collapsed;
            txtConfirmPassword.Visibility = Visibility.Visible;
        }
    }
}