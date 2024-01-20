using OfficeOpenXml;
using ReserveRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReserveRoom.Models
{
   
    public class Hotel
    {
        private readonly string _filename = "C:\\Users\\alica\\source\\repos\\ReserveRoom\\ReserveRoom\\Data\\ReservationData.xlsx";
        
        private readonly ReservationBook _reservationBook;        
        public event EventHandler? ReservationsChanged;
        public string Name { get; }

        

        public Hotel(string name) 
        {
            Name = name;
            _reservationBook = new ReservationBook();
            ReadExistingReservations();
            _reservationBook.ReservationsBookChanged += WriteToXL;
            _reservationBook.ReservationsBookChanged += OnReservationsChanged;
        }

        

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationBook.GetAllReservations();
        }

        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);

        }

        private void ReadExistingReservations()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage(_filename);
            var worksheet = excel.Workbook.Worksheets.First();
            int row = 2;
            for (int i = row; i <= worksheet.Dimension.End.Row; i++)
            {

                string name = (string)worksheet.Cells[i, 1].Value;
                string[] roomID = ((string)worksheet.Cells[i, 2].Value).Split(',');
                int roomNumber = int.Parse(roomID[1]);
                int floorNumber = int.Parse(roomID[0]);
                DateTime startDate = DateTime.Parse((string)worksheet.Cells[i, 3].Value);
                DateTime endDate = DateTime.Parse((string)worksheet.Cells[i, 4].Value);

                var reservation = new Reservation(
                    name,
                    new RoomID(roomNumber, floorNumber),
                    startDate,
                    endDate
                    );
                _reservationBook.AddReservation(reservation);               

            }
            excel.Dispose();

        }
        
        private void WriteToXL(object? sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage(_filename);
            var workSheet = excel.Workbook.Worksheets.First();
            string filename = string.Concat(_filename.AsSpan(0, _filename.Length - 5), "_output.xlsx");

            int row = 2;
            foreach (Reservation reservation in _reservationBook.GetAllReservations())
            {
                workSheet.Cells[row, 1].Value = reservation.UserName;
                workSheet.Cells[row, 2].Value = reservation.RoomID;
                workSheet.Cells[row, 3].Value = reservation.StartTime.ToShortDateString();
                workSheet.Cells[row, 4].Value = reservation.EndTime.ToShortDateString();
                row++;
            }

            if (File.Exists(filename))
                File.Delete(filename);

            // Create excel file on physical disk  
            FileStream objFileStrm = File.Create(filename);
            objFileStrm.Close();

            // Write content to excel file  
            File.WriteAllBytes(filename, excel.GetAsByteArray());
            //Close Excel package 
            excel.Dispose();

        }

        private void OnReservationsChanged(object? sender, EventArgs e)
        {
            ReservationsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
