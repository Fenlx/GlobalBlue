using GlobalBlue.Services.Business;
using GlobalBlue.Services.Common;
using GlobalBlue.Services.Contracts.Business;
using GlobalBlue.Services.Contracts.Common;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Host.UseNLog();

builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var _myAllowedOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(_myAllowedOrigins, corsPolicyBuilder => { corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(_myAllowedOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();