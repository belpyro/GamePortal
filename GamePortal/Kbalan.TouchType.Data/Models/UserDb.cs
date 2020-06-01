using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Model for User Table. User table has PK - Id from Entity class and two FKs - Statistic(one-to-one with StatisticDb), Setting(one-to-one with 
    /// SettingDb) . Each User has one requred Setting Set and one Statistic Set. 
    /// Statistic and Setting sets have one user whom this setting and statistic set belong to.
    /// </summary>
    public class UserDb: Entity
    {

        public string NickName { get; set; }

        public string Password { get; set; }

        public StatisticDb Statistic { get; set; }

        public SettingDb Setting { get; set; }

    }
}
