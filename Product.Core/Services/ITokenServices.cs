﻿using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Services
{
    public interface ITokenServices
    {
        //tạo token cho xác thực
        string CreateToken(AppUsers appUsers);
    }
}
