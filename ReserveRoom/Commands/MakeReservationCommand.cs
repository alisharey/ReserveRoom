using ReserveRoom.Exceptions;
using ReserveRoom.Models;
using ReserveRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace ReserveRoom.Commands
{
    internal class MakeReservationCommand : CommandBase

    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _makeReservationViewModel;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel)
        {
            _hotel = hotel;
            _makeReservationViewModel = makeReservationViewModel;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {

            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                _makeReservationViewModel.FloorNumber > 0 &&
                _makeReservationViewModel.RoomNumber > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Reservation reservation = new Reservation(
                _makeReservationViewModel.Username,
                new RoomID(_makeReservationViewModel.RoomNumber, _makeReservationViewModel.FloorNumber),
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);
            try
            {
                _hotel.MakeReservation(reservation);
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
               e.PropertyName == nameof(MakeReservationViewModel.FloorNumber) ||
               e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
