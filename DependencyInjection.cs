using GymTrackerProject.Data;
using GymTrackerProject.Services;
using System.Reflection;

namespace GymTrackerProject;

public static class DependencyInjection
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("YourDbContextConnection");

        //добавление сервиса бд
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        //добавление Identity
        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Lockout.AllowedForNewUsers = true;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        //добавление HttpAccessor
        builder.Services.AddHttpContextAccessor();

        //добавление медиатора
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //добавление пользовательских сервисов
        builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
    }
}
