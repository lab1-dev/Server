#region Imports
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
#endregion

namespace Server {
    public class Program {
        #region Main
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }
        #endregion

        #region CreateHostBuilder
        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
#if DEBUG
                    webBuilder.UseUrls("http://*:22000", "https://*:22001"); 
#else
                    webBuilder.UseUrls("http://*:22000");
#endif
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseStartup<Startup>();
                });
        }
        #endregion
    }
}