using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudRunner.Commons.Core.ConstitutiveEquations.Fatigue;
using MudRunner.Commons.Core.ConstitutiveEquations.MechanicsOfMaterials;
using MudRunner.Commons.Core.GeometricProperties.CircularProfile;
using MudRunner.Commons.Core.GeometricProperties.RectangularProfile;
using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.Application.Extensions;
using MudRunner.Suspension.Core.Mapper;
using MudRunner.Suspension.Core.Operations.CalculateReactions;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue.CircularProfile;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Fatigue.RectangularProfile;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Static.CircularProfile;
using MudRunner.Suspension.Core.Operations.RunAnalysis.Static.RectangularProfile;
using Newtonsoft.Json.Converters;

namespace MudRunner.Suspension.Application
{
    /// <summary>
    /// The application startup.
    /// It configures the dependency injection and adds all necessary configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Constitutive Equations
            services.AddScoped<IMechanicsOfMaterials, MechanicsOfMaterials>();
            services.AddScoped<IFatigue<CircularProfile>, Fatigue<CircularProfile>>();
            services.AddScoped<IFatigue<RectangularProfile>, Fatigue<RectangularProfile>>();

            // Register Geometric Property calculators.
            services.AddScoped<ICircularProfileGeometricProperty, CircularProfileGeometricProperty>();
            services.AddScoped<IRectangularProfileGeometricProperty, RectangularProfileGeometricProperty>();

            // Register Mapper
            services.AddScoped<IMappingResolver, MappingResolver>();

            // Register operations.
            services.AddScoped<ICalculateReactions, CalculateReactions>();
            services.AddScoped<IRunCircularProfileStaticAnalysis, RunCircularProfileStaticAnalysis>();
            services.AddScoped<IRunRectangularProfileStaticAnalysis, RunRectangularProfileStaticAnalysis>();
            services.AddScoped<IRunCircularProfileFatigueAnalysis, RunCircularProfileFatigueAnalysis>();
            services.AddScoped<IRunRectangularProfileFatigueAnalysis, RunRectangularProfileFatigueAnalysis>();

            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerDocs();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocs();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
