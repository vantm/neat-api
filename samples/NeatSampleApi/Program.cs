using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddNeatApi(Assembly.GetEntryAssembly()!);

var app = builder.Build();

app.MapNeatApi();

app.Run();
