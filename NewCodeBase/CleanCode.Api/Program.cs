using CleanCode.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapperProfileServices();

builder.Services.AddAppDiServices();
builder.Services.AddSqlDatabaseServices();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();


