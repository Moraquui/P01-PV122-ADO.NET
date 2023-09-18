using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using DemoClients;
namespace OrganizationManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async Task UpdateGrid()
        {
            var r = await App.HttpClient.GetAsync("api/organizations");
            var responce = r;
            if (responce.IsSuccessStatusCode)
            {
                var r1 = await responce.Content.ReadFromJsonAsync<QueryResult<Organization>>();

                dg.ItemsSource = new ObservableCollection<Organization>(r1?.Items);
            }
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateGrid();
        }

        private async void ButtonAddRand_Click(object sender, RoutedEventArgs e)
        {
            var organization = new Organization() {
                Name = "Organization",
                FullName = "Full Name Organization"
            };
            var responce = App.HttpClient.PostAsJsonAsync("api/organizations", organization);

            await UpdateGrid();
        }
        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            await UpdateGrid();
        }
        private async Task UpdateOrganization(int organizationId, Organization updatedOrganization)
        {
            var response = await App.HttpClient.PutAsJsonAsync($"api/organizations/{organizationId}", updatedOrganization);
            if (response.IsSuccessStatusCode)
            {
                await UpdateGrid();
            }
            else
            {
                MessageBox.Show("Failed to update the organization.");
            }
        }
        private async void dg_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editedItem = e.Row.Item as Organization;
            var updatedOrganization = new Organization()
            {
                Id = editedItem.Id,
                Name = editedItem.Name,
                FullName = editedItem.FullName
            };

            await UpdateOrganization(editedItem.Id, updatedOrganization);

            
        }
        
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
