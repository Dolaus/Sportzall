﻿using System.ComponentModel.DataAnnotations;

namespace Sportzall.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(1940,2022,ErrorMessage ="Неправильний рік")]
        public int? Year { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Image { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }
    }
}