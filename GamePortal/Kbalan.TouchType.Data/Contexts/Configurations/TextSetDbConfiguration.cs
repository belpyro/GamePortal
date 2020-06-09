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
    /// Configuration class of TextSetdB
    /// </summary>
    internal class TextSetDbConfiguration: EntityTypeConfiguration<TextSetDb>
    {
        public TextSetDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Text_set");
        }
        
    }
}
