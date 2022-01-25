﻿using Microsoft.AspNetCore.Identity;
using System;

namespace RestaurantAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PassswordHash { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}