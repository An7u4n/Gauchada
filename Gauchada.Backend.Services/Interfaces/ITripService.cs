﻿using Gauchada.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface ITripService
    {
        Task<List<TripDTO>> GetTripsByLocation(string origin, string destination);
        Task<List<UserDTO>> GetTripPassengers(int tripId);
        Task<List<TripDTO>> GetUserTrips(string passengerUserName);
        Task<bool> SetTrip(TripDTO trip);
        Task<bool> AddPassengerToATrip(int tripId, string passengerUserName);
        Task<bool> RemovePassengerFromATrip(int tripId, string passengerUserName);
    }
}
