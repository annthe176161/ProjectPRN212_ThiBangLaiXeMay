using ProjectPRN212.Models;
using System.Windows;
using System.Windows.Controls;

namespace ProjectPRN212
{
    public partial class AddEditUserWindow : Window
    {
        public User User { get; private set; }

        public AddEditUserWindow()
        {
            InitializeComponent();
            User = new User();
            cbUserType.SelectedIndex = 1; // Default to User
        }

        public AddEditUserWindow(User user) : this()
        {
            User = user;
            txtUsername.Text = User.Username;
            txtPassword.Password = User.Password;
            txtFullName.Text = User.FullName;
            txtEmail.Text = User.Email;
            cbUserType.SelectedIndex = User.UserType == "Admin" ? 0 : 1;
            txtPhoneNumber.Text = User.PhoneNumber;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            User.Username = txtUsername.Text;
            User.Password = txtPassword.Password;
            User.FullName = txtFullName.Text;
            User.Email = txtEmail.Text;
            User.UserType = ((ComboBoxItem)cbUserType.SelectedItem).Content.ToString();
            User.PhoneNumber = txtPhoneNumber.Text;
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}