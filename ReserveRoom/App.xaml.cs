using ReserveRoom.Exceptions;
using ReserveRoom.Models;
using ReserveRoom.Stores;
using ReserveRoom.ViewModels;
using ReserveRoom.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReserveRoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;

        public App()
        {
           _hotel = new Hotel("Some random bs");
           _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new ReservationListingViewModel(_hotel, _navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            
            MainWindow.Show();
            base.OnStartup(e);
        }
        

    }
    
}
