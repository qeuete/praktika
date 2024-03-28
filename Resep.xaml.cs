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

namespace WpfApp1
{
    public partial class Resep : Page
    {
        BookingViewTableAdapter adapter = new BookingViewTableAdapter();   
        RoomTableAdapter roomTableAdapter = new RoomTableAdapter(); 
        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();  
        public Resep()
        {
            InitializeComponent();
            ResepDgr.ItemsSource = adapter.GetData();
            typeCbx.ItemsSource = roomTableAdapter.GetData();
            numberCbx.ItemsSource = roomTableAdapter.GetData();
            middlenameCbx.ItemsSource = usersTableAdapter.GetData();
            lastnameCbx.ItemsSource = usersTableAdapter.GetData();
            firstnameCbx.ItemsSource = usersTableAdapter.GetData();
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

        private void ResepDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResepDgr.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)ResepDgr.SelectedItem;
                DataRow row = rowView.Row;

                dateoutTbx.Text = row["Дата выезда"].ToString();
                dateinTbx.Text = row["Дата въезда"].ToString();
            }
        }

        private bool IsDateValid(string date)
        {
            return DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private void DateTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

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