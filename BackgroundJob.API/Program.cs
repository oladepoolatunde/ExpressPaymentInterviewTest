using ExpressPaymentTest.Data.Repository;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ExpressCon")));


builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration["ConnectionStrings:ExpressCon"]));

builder.Services.AddHangfireServer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard(
    "/hangfire", new DashboardOptions
    {
        DashboardTitle = "ExpressPaymentBackJob",
        Authorization = new[]
        {
            new HangfireCustomBasicAuthenticationFilter{
                    User = builder.Configuration.GetSection("HangfireSettings:UserName").Value,
                    Pass = builder.Configuration.GetSection("HangfireSettings:Password").Value
                }
        }
    });
app.Run();
