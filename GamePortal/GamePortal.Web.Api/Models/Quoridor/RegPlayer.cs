using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Web.Api.Models.Quoridor
{
    public class RegPlayer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Loggin { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
    }
}