﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web.Models
{
    public class SignInVM
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }
    }
}
