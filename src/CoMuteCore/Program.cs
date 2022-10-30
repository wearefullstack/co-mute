using Microsoft.EntityFrameworkCore;
using CoMuteCore.Data;
using CoMuteCore.Services;

namespace CoMuteCore
{
    public class Program
    {
        private readonly IConfiguration _config;

        public Program(IConfiguration config)
        {
            _config = config;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationContext>(x => x.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICarPoolService, CarPoolService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}