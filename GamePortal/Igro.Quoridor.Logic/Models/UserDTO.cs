using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamePortal.Logic.Igro.Quoridor.Logic.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
     
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
    }
}