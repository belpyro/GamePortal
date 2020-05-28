using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{

    public class UserDb: Entity
    {

        public string NickName { get; set; }

        public UserStatisticDb UserStatistic { get; set; }

        public UserSettingDb UserSetting { get; set; }

    }
}
