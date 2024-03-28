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
    public partial class ServicePage : Page
    {
        ServiceeTableAdapter serviceview = new ServiceeTableAdapter();
        public ServicePage()
        {
            InitializeComponent();
            ServiceDgr.ItemsSource = serviceview.GetData();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(priceTbx.Text, out decimal price))
            {
                DataRowView selectedRow = (DataRowView)ServiceDgr.SelectedItem;
                string servicename = Convert.ToString(selectedRow["namee"]);
                serviceview.UpdateQuery(nameeTbx.Text, descTbx.Text, price, Convert.ToString(selectedRow["namee"]));
                ServiceDgr.ItemsSource = serviceview.GetData();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(priceTbx.Text, out decimal price))
            {  
                serviceview.InsertQuery(nameeTbx.Text, descTbx.Text, price);
                ServiceDgr.ItemsSource = serviceview.GetData();
            }
            else
            {
                MessageBox.Show("Неверный формат цены");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            DataRowView selectedRow = (DataRowView)ServiceDgr.SelectedItem;
            string servicename = Convert.ToString(selectedRow["namee"]);
            serviceview.DeleteQuery(servicename);
            ServiceDgr.ItemsSource = serviceview.GetData();

        }        
       
        private void ServiceDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceDgr.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)ServiceDgr.SelectedItem;
                DataRow row = rowView.Row;

                nameeTbx.Text = row["namee"].ToString();
                priceTbx.Text = row["price"].ToString();
                descTbx.Text = row["descriptions"].ToString();
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

        private void priceTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
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

        private void descTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
    }
    
    }


