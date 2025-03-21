﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Models.Backend.Login;

public class UserResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Token { get; set; }

}
