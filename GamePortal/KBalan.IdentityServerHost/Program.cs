using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBalan.IdentityServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new StartOptions();
            options.Urls.Add("http://+:10000");
            using (WebApp.Start<Startup>(options))
            {
                Console.ReadKey();
            }
        }
    }
}
