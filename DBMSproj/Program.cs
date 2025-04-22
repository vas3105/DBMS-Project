using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using DBMSproj.Components;
using DBMSproj.Services; 
using DBMSproj.Data; 
namespace DBMSproj;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add services to the container.
        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents();

        // Register AttendanceService
        builder.Services.AddScoped<AttendanceService>();
        //Register BankDetailsService
        builder.Services.AddScoped<BankDetailsService>();
        // Register EmployeeService
        builder.Services.AddScoped<EmployeeService>();
        //Register TaxesService
        builder.Services.AddScoped<TaxesService>();
        //Register PayslipService
        builder.Services.AddScoped<PayslipService>();
        //Register EmployeeStateService
        builder.Services.AddScoped<EmployeeStateService>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
