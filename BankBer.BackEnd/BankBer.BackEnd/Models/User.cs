﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankBer.BackEnd.Models
{
    public class User
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}