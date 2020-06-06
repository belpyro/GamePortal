using Kbalan.TouchType.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Contexts.Configurations
{
    /// <summary>
    /// Configuration class of StatisticDb
    /// </summary>
    internal class StatisticDbConfiguration: EntityTypeConfiguration<StatisticDb>
    {
        public StatisticDbConfiguration()
        {
            HasKey(x => x.StatisticId).ToTable("Statistic");
        }
    }
}
