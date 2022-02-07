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
        public int countPages;
        public int currentPage;
        public int countRecord = 10;

        public MainWindow()
        {
            InitializeComponent();
            context = new ClientServiceEntities();
            dgClients.ItemsSource = context.Clients.Take(countRecord).ToList();
            countPages = context.Clients.Count() / 10;
            currentPage = 1;
            CurrentPage.Text = currentPage + " из " + countPages;
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {


        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ShowTable()
        {
            dgClients.ItemsSource = context.Clients.Take(countRecord).ToList();
        }

        private void cmbSelectCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            ClientServiceEntities context2 = new ClientServiceEntities();

            switch (cmbSelectCount.SelectedIndex)
            {
                case 0:

                    countRecord = 10;
                    countPages = context2.Clients.Count() / countRecord;
                    break;
                case 1:
                    countRecord = 50;
                    countPages = context2.Clients.Count() / countRecord;

                    break;
                case 2:
                   
                    countRecord = 200;
                    countPages = context2.Clients.Count() / countRecord;
                    if (countPages == 0)
                        countPages = 1;
                    break;
                default:
                    countRecord = context2.Clients.Count();
                    countPages = 1;
                    break;
            } 
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            
            if (currentPage > 1 && currentPage <= countPages)
            {
                currentPage--;
                dgClients.ItemsSource = context.Clients.OrderBy(x => x.ID).Skip(currentPage * countRecord-countRecord).Take(countRecord).ToList();
                CurrentPage.Text = currentPage + " из " + countPages;
                NextPage.IsEnabled = true;
                if(currentPage == 1)
                {
                    PrevPage.IsEnabled = false;
                }

            } 
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < countPages)
            {
                dgClients.ItemsSource = context.Clients.OrderBy(x=>x.ID).Skip(currentPage*countRecord).Take(countRecord).ToList();
                currentPage++;
                CurrentPage.Text = currentPage + " из " + countPages;
                PrevPage.IsEnabled = true;
                if (currentPage == countPages)
                {
                    NextPage.IsEnabled = false;
                }
            }
            
        }

        private void showRecord_Click(object sender, RoutedEventArgs e)
        {
            if (countPages == 1)
            {
                PrevPage.IsEnabled = false;
                NextPage.IsEnabled = false;
            } else
            {
                PrevPage.IsEnabled = true;
                NextPage.IsEnabled = true;
            }
            CurrentPage.Text = currentPage + " из " + countPages;
            ShowTable();
        }
    }
}
