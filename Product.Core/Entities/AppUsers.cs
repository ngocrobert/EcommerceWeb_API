﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    public class AppUsers : IdentityUser
    {
        public string DisplayName { get; set; }
        //public string AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
