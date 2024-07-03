using ProjectPRN212.Models;
using System.Linq;
using System.Windows;

namespace ProjectPRN212
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Tạo đối tượng kết nối cơ sở dữ liệu
        ThiBangLaiXeMayContext db = new ThiBangLaiXeMayContext();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy tên đăng nhập và mật khẩu từ các hộp nhập liệu
            var username = txtUsername.Text;
            var password = passwordBox.Password;

            if (passwordTextBox.Visibility == Visibility.Visible)
            {
                password = passwordTextBox.Text;
            }

            // Kiểm tra xem thông tin đăng nhập đã đầy đủ chưa
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.");
                return;
            }

            // Tìm người dùng có tên đăng nhập và mật khẩu khớp với thông tin nhập vào
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            // Nếu tìm thấy người dùng, đăng nhập thành công
            if (user != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Chuyển đến trang chính của ứng dụng
                HomeWindow homeWindow = new HomeWindow(user);
                homeWindow.Show();
                this.Close();
            }
            else
            {
                // Nếu không tìm thấy, hiển thị thông báo lỗi
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
            this.Close();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            // Hiển thị mật khẩu dưới dạng văn bản trong TextBox
            passwordTextBox.Text = passwordBox.Password;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Ẩn mật khẩu và hiển thị lại dưới dạng PasswordBox
            passwordBox.Password = passwordTextBox.Text;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
        }
    }
}