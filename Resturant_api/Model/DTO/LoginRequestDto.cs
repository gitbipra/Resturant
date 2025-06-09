﻿using System.ComponentModel.DataAnnotations;

namespace Resturant_api.Model.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required] 
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
