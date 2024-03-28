using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }
        private void UsersTable(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new UsersPage();
        }
        private void RoleTable(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Role();
        }
        private void ServiceTable(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ServicePage();
        }
        private void AmenitiesTable(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AmenitiesPage();
        }
    }
}
