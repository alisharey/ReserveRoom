using ReserveRoom.Models;
using ReserveRoom.Stores;
using ReserveRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Hotel _hotel;


        public NavigateCommand(Hotel hotel, NavigationStore navigationstore)
        {
            _navigationStore = navigationstore;
            _hotel = hotel;
        }
        public override void Execute(object? parameter)
        {
           
            if(_navigationStore.CurrentViewModel is MakeReservationViewModel)
            {
                _navigationStore.CurrentViewModel = new ReservationListingViewModel(_hotel, _navigationStore);
            }

            else _navigationStore.CurrentViewModel = new MakeReservationViewModel(_hotel, _navigationStore);

        }

        
    }
}
