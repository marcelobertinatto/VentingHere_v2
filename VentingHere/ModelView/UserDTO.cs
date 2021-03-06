﻿using System;
using System.Collections.Generic;

namespace VentingHere.ModelView
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin{ get; set; }
        public DateTime UserFirstRegister { get; set; }
        public bool FacebookImage { get; set; }
        public List<string> Roles { get; set; }
    }
}
