using ReserveRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel(Hotel _hotel) 
        {
            CurrentViewModel = new ReservationListingViewModel(_hotel);
        }
    }
}
