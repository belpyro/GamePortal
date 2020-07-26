using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Exceptions
{
   public  class TTGValidationException:Exception
    {
        public TTGValidationException(string message)
                        : base(message)
        { }
    }
}
