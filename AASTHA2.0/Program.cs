using AASTHA2;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

CreateWebHostBuilder(args).UseSerilog().Build().Run();
static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
