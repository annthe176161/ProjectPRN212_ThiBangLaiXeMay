using ProjectPRN212.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjectPRN212
{
    public partial class ManageUsersWindow : Window
    {
        private ThiBangLaiXeMayContext db;
        private ObservableCollection<User> users;

        public ManageUsersWindow()
        {
            InitializeComponent();
            db = new ThiBangLaiXeMayContext();
            LoadUsers();
        }

        private void LoadUsers()
        {
            users = new ObservableCollection<User>(db.Users.ToList());
            dataGridUsers.ItemsSource = users;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddEditUserWindow();
            if (addUserWindow.ShowDialog() == true)
            {
                db.Users.Add(addUserWindow.User);
                db.SaveChanges();
                LoadUsers();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedItem is User selectedUser)
            {
                var editUserWindow = new AddEditUserWindow(selectedUser);
                if (editUserWindow.ShowDialog() == true)
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId);
                    if (user != null)
                    {
                        user.Username = editUserWindow.User.Username;
                        user.Password = editUserWindow.User.Password;
                        user.FullName = editUserWindow.User.FullName;
                        user.Email = editUserWindow.User.Email;
                        user.UserType = editUserWindow.User.UserType;
                        user.PhoneNumber = editUserWindow.User.PhoneNumber;
                        db.SaveChanges();
                        LoadUsers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để chỉnh sửa.");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedItem is User selectedUser)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    db.Users.Remove(selectedUser);
                    db.SaveChanges();
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}