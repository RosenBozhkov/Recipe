using Business.Implementations;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistence.Context;
using Persistence.Implementations;
using Persistence.Interfaces;
using Recipe.AutoMapper;
using System.Reflection;

namespace Recipe
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RecipeContext>(options => options.UseSqlServer(
                                                                        Configuration.GetConnectionString("TestApiDatabase"),
                    b => b.MigrationsAssembly(typeof(RecipeContext).Assembly.FullName)));

            services.ConfigureAutomapper();

            services.AddSwaggerGen(c => c.MapType<TimeSpan?>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("00:00:00") }));
            services.AddMvc(); 

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IIngredientService, IngredientService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
                
            });
        }
    }
}
