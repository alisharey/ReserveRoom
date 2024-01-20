using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.Models
{
    public class Reservation
    {
        public RoomID RoomID { get;}

        public string UserName { get;}

        public DateTime StartTime { get;}
        public DateTime EndTime { get;}

        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(string userName, RoomID roomID, DateTime startTime, DateTime endTime) 
        { 

            RoomID = roomID;
            StartTime = startTime;
            EndTime = endTime;
            UserName = userName;
        }

        internal bool Conflicts(Reservation reservation)
        {
           if (!this.RoomID.Equals(reservation.RoomID))
            {
                return false;
            }
           
           return reservation.StartTime < this.EndTime && reservation.EndTime > this.StartTime;
        }
    }
}
