using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Capex.API;
using Capex.Business.Common.DBLogger;
using Serilog;
using System.Net;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
//builder.Host.ConfigureLogging((hostBuilderContext, logging) => logging.AddDbLogger(hostBuilderContext.Configuration));
var startup = new Startup(builder.Configuration); // My custom startup class.

startup.ConfigureServices(builder.Services); // Add services to the container.

var app = builder.Build();
startup.Configure(app, app.Environment); // Configure the HTTP request pipeline.

app.Run();
