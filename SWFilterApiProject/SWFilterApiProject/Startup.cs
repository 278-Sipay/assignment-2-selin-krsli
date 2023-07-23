using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SWFilterProject.Data.Repository;
using SWFilterProject.Data;
using AutoMapper;
using SWFilterApiProject.Schema;

namespace SWFilterApiProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SWFilterApi", Version = "v1" });
            });

            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<FilterApiDbContext>(options => options.UseSqlServer(dbConfig));

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            services.AddSingleton(config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SWFilterApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
