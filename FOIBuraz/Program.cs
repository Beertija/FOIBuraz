using FOIBuraz;
using FOIBuraz.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args).Build();

ConfigureLogging.Configure();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var app = new MainLogic();
app.FOIBurazApp(config);