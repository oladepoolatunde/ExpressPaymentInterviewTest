using ExpressPaymentTest.Data.IRepository;
using ExpressPaymentTest.Data.Repository;
using ExpressPaymentTest.Services.ILogic;
using ExpressPaymentTest.Services.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.Collections.Concurrent;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ExpressCon")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(x =>
{
    x.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    x.AddPolicy("fixed", HttpContext => RateLimitPartition.GetFixedWindowLimiter(partitionKey: HttpContext.Connection.RemoteIpAddress?.ToString(), factory: _ => new FixedWindowRateLimiterOptions
    {
        PermitLimit = 10,
        Window = TimeSpan.FromSeconds(10)
    }));
});
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
