using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace InfiCoreDojo.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            BuildWebHost(args).Run();
            return 0;
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();        
    }
}
