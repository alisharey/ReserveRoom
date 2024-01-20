using ReserveRoom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using ReserveRoom.Commands;
using System.ComponentModel;
using System.Windows.Data;
using ReserveRoom.Stores;

namespace ReserveRoom.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {

        public ICommand MakeReservationCommand { get; }
        private ObservableCollection<ReservationViewModel> _reservations;
        private readonly Hotel _hotel;



        public IEnumerable<ReservationViewModel> Reservations => _reservations;



        public ReservationListingViewModel(Hotel hotel, NavigationStore navigationStore)
        {

            MakeReservationCommand = new NavigateCommand(hotel, navigationStore);

            _hotel = hotel;
            _reservations = new ObservableCollection<ReservationViewModel>();
            UpdateReservationsListView(null, EventArgs.Empty);
            _hotel.ReservationsChanged += UpdateReservationsListView;


        }

        private void UpdateReservationsListView(object? sender, EventArgs e)
        {
            IEnumerable<Reservation> reservations = _hotel.GetAllReservations();

            if (sender is not null)
            {
                var reservation = reservations.LastOrDefault();
                _reservations.Add(new ReservationViewModel(reservation));
            }
            else
            {
                foreach (Reservation reservation in reservations)
                {
                    _reservations.Add(new ReservationViewModel(reservation));
                }
            }
        }



    }
}
