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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientServiceEntities context;
        public MainWindow()
        {
            InitializeComponent();
            context = new ClientServiceEntities();
            dgClients.ItemsSource = context.Clients.ToList();

            
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {


        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbSelectCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }
 
       
    }
}
