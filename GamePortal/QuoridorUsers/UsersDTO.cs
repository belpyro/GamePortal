using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoridorUsers
{
    public class UsersDTO
    {
        public static List<UserDTO> _users = new List<UserDTO>
        {
            new UserDTO { Id = 123, Name = "Ivan", Age = 30 },
            new UserDTO { Id = 999, Name = "Oleg", Age = 22 }
        };
    }
}
