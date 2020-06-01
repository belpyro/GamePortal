using Kbalan.TouchType.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Kbalan.TouchType.Data.Contexts
{
    /// <summary>
    /// Configuration class of UserDb
    /// </summary>
    internal class UserDbConfiguration : EntityTypeConfiguration<UserDb>
    {
        public UserDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("User");

            //Each User has requred FK - Statistic from StatisticDb Table and Statistic has requred FK - User from Userdb table.
            //Statistic will be deleted when related User is deleted
            HasRequired(stat => stat.Statistic).WithRequiredPrincipal(us => us.User)
                      .WillCascadeOnDelete();
            //Each User has requred FK - Setting from SettingDb Table and Setting has requred FK - User from Userdb table.
            //Setting will be deleted when related User is deleted
            HasRequired(set => set.Setting).WithRequiredPrincipal(us => us.User)
                .WillCascadeOnDelete();

        }
    }
}