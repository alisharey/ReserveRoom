using ReserveRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly Reservation _reservation;  

        public string RoomID => _reservation.RoomID.ToString();
        public string UserName => _reservation.UserName;
        public string StartTime => _reservation.StartTime.ToShortDateString();
        public string EndTime => _reservation.EndTime.ToShortDateString();


        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
