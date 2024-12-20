﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Sixoclock.Onyx.API.Json.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseUrls("http://*:5000;")
                 .UseKestrel()
                 .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseIISIntegration()
                 .UseStartup<Startup>()
                 .Build();

            host.Run();
        }
    }
}
