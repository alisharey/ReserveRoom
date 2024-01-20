using ReserveRoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;
        public event EventHandler? ReservationsBookChanged;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservations;
        }

        public void AddReservation(Reservation reservation)
        {
            foreach (Reservation existingReservation in _reservations)
            {
                if(existingReservation.Conflicts(reservation))
                {
                    throw new ReservationConflictException(existingReservation, reservation);
                }
            }
            this._reservations.Add(reservation);
            OnReservationsBookChanged();

        }

        private void OnReservationsBookChanged()
        {
            ReservationsBookChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
