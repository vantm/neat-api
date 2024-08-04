using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddModules(Assembly.GetEntryAssembly()!);

var app = builder.Build();

app.MapModuleRoutes();

app.Run();
