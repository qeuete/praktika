using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace WpfApp1
{
    public partial class CleanPage : Page
    {
        CleanHistoryTableAdapter cleaning = new CleanHistoryTableAdapter();
        RoomTableAdapter room = new RoomTableAdapter();
        UsersTableAdapter user = new UsersTableAdapter();

        public CleanPage()
        {
            InitializeComponent();
            CleanDgr.ItemsSource = cleaning.GetData();
            numberCbx.ItemsSource = room.GetData();
            firstnameCbx.ItemsSource = user.GetData();
            lastnameCbx.ItemsSource = user.GetData();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {


        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {


        }

        private void CleanerDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CleanDgr.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)CleanDgr.SelectedItem;
                DataRow row = rowView.Row;

                dateTbx.Text = row["date"].ToString();
                commentTbx.Text = row["comment"].ToString();
            }
        }

        private void nameeTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void commentTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
                if (textBox != null)
                {
                    int maxLength = 11;

                    if (textBox.Text.Length >= maxLength)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.,]+$"))
            {
                e.Handled = true;
            }

        }
        private bool IsDateValid(string date)
        {

            if (!DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                return false;


            if (parsedDate.Year > 2024)
                return false;


            if (parsedDate.Month > 12)
                return false;


            if (parsedDate.Day > 31)
                return false;

            return true;
        }

        private void DateTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;

            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true;
                return;
            }

            if (e.Text == "." && textBox.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.Text, 0) && (textBox.Text.Length == 2 || textBox.Text.Length == 5))
            {
                textBox.Text += ".";
                textBox.SelectionStart = textBox.Text.Length;
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length >= 10)
            {
                e.Handled = true;
                return;
            }
        }
    }
}
