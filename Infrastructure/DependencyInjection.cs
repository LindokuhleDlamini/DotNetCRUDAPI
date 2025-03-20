using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            //services.AddScoped<IUserRepository, UserRepository>();


            //SQL
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(""));

            //MongoDb
            //services.AddSingleton<MongoDbContext>();
            //services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
            return services;
        }
    }
}

