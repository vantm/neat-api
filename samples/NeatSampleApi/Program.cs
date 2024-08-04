var builder = WebApplication.CreateBuilder(args);

builder.AddNeatApi();

var app = builder.Build();

app.MapNeatApi();

app.Run();
