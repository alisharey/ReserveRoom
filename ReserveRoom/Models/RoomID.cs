using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.Models
{
    public class RoomID
    {
        public int RoomNumber  { get; }
        public int FloorNumber { get; }

        public RoomID(int roomNumber, int floorNumber)
        {
            this.RoomNumber = roomNumber;
            this.FloorNumber = floorNumber;

        }

        public override string ToString()
        {
            return $"{this.FloorNumber}, {this.RoomNumber}";
        }
        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID &&
                this.FloorNumber == roomID.FloorNumber &&
                this.RoomNumber == roomID.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoomNumber, FloorNumber);
        }
    }
}
