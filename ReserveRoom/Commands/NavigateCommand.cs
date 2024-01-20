using ReserveRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly Hotel _hotel;
        public NavigateCommand(Hotel hotel) 
        { 
            _hotel = hotel;
        }
        public override void Execute(object? parameter)
        {
            _hotel.MakeReservation(new Reservation(
                "Ali",
                new RoomID(22, 22),
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 5)
                ));
            
        }
    }
}
