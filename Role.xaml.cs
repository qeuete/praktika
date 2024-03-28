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
    public partial class Role : Page
    {
        RolesTableAdapter rol = new RolesTableAdapter();  
        public Role()
        {
            InitializeComponent();
            RoleDgr.ItemsSource = rol.GetData();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            object IDD = (RoleDgr.SelectedItem as DataRowView).Row[1];
            rol.UpdateQuery(RoleTbx.Text, Convert.ToString(IDD));
            RoleDgr.ItemsSource = rol.GetData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            rol.InsertQuery(RoleTbx.Text);
            RoleDgr.ItemsSource = rol.GetData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            object namee = (RoleDgr.SelectedItem as DataRowView).Row[1];
            rol.DeleteQuery(Convert.ToString(namee));
            RoleDgr.ItemsSource = rol.GetData();
        }
        private void UsersDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleDgr.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)RoleDgr.SelectedItem;
                DataRow row = rowView.Row;

                RoleTbx.Text = row["namee"].ToString();
            }
        }

        private void RoleTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
