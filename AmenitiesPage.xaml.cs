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
    public partial class AmenitiesPage : Page
    {
        AmenitiesTableAdapter amen = new AmenitiesTableAdapter();
        public AmenitiesPage()
        {
            InitializeComponent();
            AmenDgr.ItemsSource = amen.GetData();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            object am = (AmenDgr.SelectedItem as DataRowView).Row[1];
            amen.UpdateQuery(nameTbx.Text, descTbx.Text, Convert.ToString(am));
            AmenDgr.ItemsSource = amen.GetData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            amen.InsertQuery(nameTbx.Text, descTbx.Text);
            AmenDgr.ItemsSource = amen.GetData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            object namee = (AmenDgr.SelectedItem as DataRowView).Row[1];
            amen.DeleteQuery(Convert.ToString(namee));
            AmenDgr.ItemsSource = amen.GetData();
        }
        private void UsersDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AmenDgr.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)AmenDgr.SelectedItem;
                DataRow row = rowView.Row;

                nameTbx.Text = row["namee"].ToString();
                descTbx.Text = row["descriptions"].ToString();
            }
        }

        private void nameTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void descTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null)
            {
                int maxLength = 200;

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
