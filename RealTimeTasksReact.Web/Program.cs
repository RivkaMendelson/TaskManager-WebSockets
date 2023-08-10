
using System.Text.Json.Serialization;

namespace RealTimeTasksReact.Web
{
    public class Program
    {
        private static string CookieScheme = "RealTimeTasks";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
                    .AddJsonOptions(opts =>
                    {
                        var enumConverter = new JsonStringEnumConverter();
                        opts.JsonSerializerOptions.Converters.Add(enumConverter);
                    });



            builder.Services.AddAuthentication(CookieScheme)
               .AddCookie(CookieScheme, options =>
               {
                   options.LoginPath = "/account/login";
               });
            builder.Services.AddSession();

            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapHub<TaskHub>("/api/test");



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}