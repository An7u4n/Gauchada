﻿using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauchada.Backend.Model.Entity
{
    public class DriverEntity : UserAbstract
    {
        public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
        public ICollection<TripEntity> Trips { get; set; } = new List<TripEntity>();
        public ICollection<DriverMessage> Messages { get; set; } = new List<DriverMessage>();


        public DriverEntity() { }
        public DriverEntity(AddUserDTO passenger, string photoSrc)
        : base(passenger.UserName, passenger.Name, passenger.LastName, passenger.Email, passenger.Birth, passenger.PhoneNumber, photoSrc)
        {
        }
    }
}
