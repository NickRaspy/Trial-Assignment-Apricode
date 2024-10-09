using Microsoft.EntityFrameworkCore;
using TA_Apricode.Data;
using TA_Apricode.Repository;

namespace TA_Apricode
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GameContext>(options =>
                options.UseInMemoryDatabase("GameLibrary"));

            services.AddScoped<IGameRepository, GameRepository>();

            services.AddRazorPages();

            services.AddRazorComponents();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
