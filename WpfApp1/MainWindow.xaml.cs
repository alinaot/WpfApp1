using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            CmbFilter.ItemsSource = context.Genders.ToList();
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {


        }

        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {

        }
        public List<Client> clients;
        private void ShowTable()
        {
            if (CmbFilter.SelectedItem == null)
                return;
            
            var gender = CmbFilter.SelectedItem as Gender;
            clients = context.Clients.ToList();
            if (cmbSelectCount.SelectedIndex == 3)
            {
                clients = context.Clients.ToList();
                countPages = 1;
            }

                if (txtEmail.Text != "")
                {
                    clients = clients.Where(x => x.Email.Contains(txtEmail.Text)).ToList();
                }
                if (txtPhone.Text != "")
                {
                    clients = clients.Where(x => x.Phone.Contains(txtPhone.Text)).ToList();
                }
                if (txtName.Text != "")
                {
                    clients = clients.Where(x => x.FirstName.ToLower().Contains(txtName.Text.ToLower())
                                                ||x.LastName.ToLower().Contains(txtName.Text.ToLower()) 
                                                ||x.Patronymic.ToLower().Contains(txtName.Text.ToLower())).ToList();
                }
                clients = clients.Where(x => x.Gender.Code == gender.Code).OrderBy(x => x.ID).Skip(currentPage * countRecord - countRecord).Take(countRecord).ToList();
                countPages = context.Clients.Where(x => x.Gender.Code == gender.Code).Count() / countRecord;

            setEnableButtons();
            CurrentPage.Text = currentPage + " из " + countPages;
            dgClients.ItemsSource = clients;
        }

        private void cmbSelectCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            ClientServiceEntities context2 = new ClientServiceEntities();

            switch (cmbSelectCount.SelectedIndex)
            {
                case 0:
                    countRecord = 10;

                    break;
                case 1:
                    countRecord = 50;

                    break;
                case 2:
                   
                    countRecord = 200;
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
                ShowTable();
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
                
                currentPage++;
                ShowTable();
                PrevPage.IsEnabled = true;
                if (currentPage == countPages)
                {
                    NextPage.IsEnabled = false;
                }
            }
            
        }

        private void setEnableButtons()
        {
            if (countPages == 1 || countPages == 0)
            {
                PrevPage.IsEnabled = false;
                NextPage.IsEnabled = false;
                countPages = 1;
            }
            else
            {
                PrevPage.IsEnabled = true;
                NextPage.IsEnabled = true;
            }

                
        }
        private void showRecord_Click(object sender, RoutedEventArgs e)
        {
            setEnableButtons();
            ShowTable();
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            ShowTable();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTable();
        }

        private void BtnSortFirstName_Click(object sender, RoutedEventArgs e)
        {
            dgClients.ItemsSource = clients.OrderBy(x=>x.FirstName).ToList();
        }

        private void BtnSortLastDate_Click(object sender, RoutedEventArgs e)
        {

            dgClients.Items.SortDescriptions.Clear();
            dgClients.Items.SortDescriptions.Add(new SortDescription("lastDate", ListSortDirection.Descending));
            dgClients.Items.Refresh();
        }

        private void BtnSortCount_Click(object sender, RoutedEventArgs e)
        {
            dgClients.Items.SortDescriptions.Clear();
            dgClients.Items.SortDescriptions.Add(new SortDescription("countVisit", ListSortDirection.Descending));
            dgClients.Items.Refresh();



        }
    }
}
