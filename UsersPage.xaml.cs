using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.hotels2DataSet2TableAdapters;

namespace WpfApp1
{
    public partial class UsersPage : Page
    {
        UserViewTableAdapter userview = new UserViewTableAdapter();
        RolesTableAdapter rolesTable = new RolesTableAdapter();
        UsersTableAdapter usersTable = new UsersTableAdapter();
        public UsersPage()
        {
            InitializeComponent();
            UsersDgr.ItemsSource = userview.GetData();
            RoleCbx.ItemsSource = rolesTable.GetData();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastnameTbx.Text) ||
                string.IsNullOrWhiteSpace(firstnameTbx.Text) ||
                string.IsNullOrWhiteSpace(middlenameTbx.Text) ||
                string.IsNullOrWhiteSpace(LoginTbx.Text) ||
                string.IsNullOrWhiteSpace(PasswordTbx.Password) ||
                RoleCbx.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                string hashedPassword = HashPassword(PasswordTbx.Password);
                DataRowView selectedRole = RoleCbx.SelectedItem as DataRowView;
                int roleId = selectedRole != null ? Convert.ToInt32(selectedRole["id_role"]) : -1;

                usersTable.Insert(lastnameTbx.Text, firstnameTbx.Text, middlenameTbx.Text, LoginTbx.Text, hashedPassword, roleId);
                UsersDgr.ItemsSource = userview.GetData();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataRowView selectedUser = (DataRowView)UsersDgr.SelectedItem;
            int userid = Convert.ToInt32(selectedUser["id_userr"]);

            try
            {
                string hashedPassword = HashPassword(PasswordTbx.Password);

                DataRowView selectedRole = RoleCbx.SelectedItem as DataRowView;
                int roleId = selectedRole != null ? Convert.ToInt32(selectedRole["id_role"]) : -1;


                DataRowView currentSelectedUser = (DataRowView)UsersDgr.SelectedItem;
                int currentUserId = Convert.ToInt32(currentSelectedUser["id_userr"]);

                usersTable.UpdateQuery(lastnameTbx.Text, firstnameTbx.Text, middlenameTbx.Text, LoginTbx.Text, hashedPassword, roleId, currentUserId);

                UsersDgr.ItemsSource = userview.GetData();

                MessageBox.Show("Пользователь успешно изменен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Выберите роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (UsersDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataRowView selectedUser = (DataRowView)UsersDgr.SelectedItem;
            int userId = Convert.ToInt32(selectedUser["id_userr"]);

            usersTable.DeleteQuery(userId);

            UsersDgr.ItemsSource = userview.GetData();
        }

        private void UsersDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersDgr.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)UsersDgr.SelectedItem;
                DataRow row = rowView.Row;

                lastnameTbx.Text = row["lastname"].ToString();
                firstnameTbx.Text = row["firstname"].ToString();
                middlenameTbx.Text = row["middlename"].ToString();
                LoginTbx.Text = row["lastname"].ToString();
                PasswordTbx.Password = row["password"].ToString();

            }
        }

        private void lastnameTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                int maxLength = 50;

                if (textBox.Text.Length >= maxLength)
                {
                    e.Handled = true;
                    return;
                }

                foreach (char c in e.Text)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void firstnameTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                int maxLength = 50;

                if (textBox.Text.Length >= maxLength)
                {
                    e.Handled = true;
                    return;
                }

                foreach (char c in e.Text)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void middlenameTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                int maxLength = 50;

                if (textBox.Text.Length >= maxLength)
                {
                    e.Handled = true;
                    return;
                }

                foreach (char c in e.Text)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void LoginTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                int maxLength = 50;

                if (textBox.Text.Length >= maxLength)
                {
                    e.Handled = true;
                    return;
                }

                foreach (char c in e.Text)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void PasswordTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                int maxLength = 50;

                if (textBox.Text.Length >= maxLength)
                {
                    e.Handled = true;
                    return;
                }


            }
        }

    }
    
}
