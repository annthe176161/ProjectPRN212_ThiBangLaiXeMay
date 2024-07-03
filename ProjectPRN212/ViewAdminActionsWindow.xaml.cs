using ProjectPRN212.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjectPRN212
{
    public partial class ViewAdminActionsWindow : Window
    {
        private ThiBangLaiXeMayContext db;
        private ObservableCollection<AdminAction> adminActions;

        public ViewAdminActionsWindow()
        {
            InitializeComponent();
            db = new ThiBangLaiXeMayContext();
            LoadAdminActions();
        }

        private void LoadAdminActions()
        {
            adminActions = new ObservableCollection<AdminAction>(db.AdminActions.ToList());
            dataGridAdminActions.ItemsSource = adminActions;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}