 using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        public  SettingDb Setting { get; set; }

        public StatisticDb Statistic { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
