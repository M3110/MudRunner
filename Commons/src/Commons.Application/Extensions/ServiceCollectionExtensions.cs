using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MudRunner.Commons.Core.ConstitutiveEquations.Fatigue;
using MudRunner.Commons.Core.ConstitutiveEquations.MechanicsOfMaterials;
using MudRunner.Commons.Core.Factory.DifferentialEquationMethod;
using MudRunner.Commons.Core.GeometricProperties.CircularProfile;
using MudRunner.Commons.Core.GeometricProperties.RectangularProfile;
using MudRunner.Commons.DataContracts.Models.Enums;
using MudRunner.Commons.DataContracts.Models.Profiles;
using MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation;
using MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation.Newmark;
using MudRunner.Suspension.Core.NumericalMethods.DifferentialEquation.NewmarkBeta;
using System.Reflection;

namespace MudRunner.Commons.Application.Extensions
{
    /// <summary>
    /// It contains extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// This method adds the services present in MudRunner.Commons.Core project.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMudRunnerCommonsServices(this IServiceCollection services)
        {
            // Register constitutive equations.
            services.AddScoped<IFatigue<CircularProfile>, Fatigue<CircularProfile>>();
            services.AddScoped<IFatigue<RectangularProfile>, Fatigue<RectangularProfile>>();
            services.AddScoped<IMechanicsOfMaterials, MechanicsOfMaterials>();

            // Register geometric properties.
            services.AddScoped<ICircularProfileGeometricProperty, CircularProfileGeometricProperty>();
            services.AddScoped<IRectangularProfileGeometricProperty, RectangularProfileGeometricProperty>();

            // Register numerical methods.
            services.AddScoped<INewmarkMethod, NewmarkMethod>();
            services.AddScoped<INewmarkBetaMethod, NewmarkBetaMethod>();

            // Register factories.
            services.AddScoped<IDifferentialEquationMethodFactory, DifferentialEquationMethodFactory>(
                provider => new DifferentialEquationMethodFactory(new Dictionary<DifferentialEquationMethodEnum, IDifferentialEquationMethod>
                {
                    { DifferentialEquationMethodEnum.Newmark, provider.GetRequiredService<INewmarkMethod>() },
                    { DifferentialEquationMethodEnum.NewmarkBeta, provider.GetRequiredService<INewmarkBetaMethod>() },
                }));

            return services;
        }

        /// <summary>
        /// This method configures the documentation file for Swagger User Interface.
        /// </summary>
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, string applicationFileName, string dataContractFileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyTitle = assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
            string assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(assemblyTitle, new OpenApiInfo
                {
                    Title = assemblyTitle,
                    Description = assemblyDescription,
                    Version = "v1"
                });

                var xmlApiPath = Path.Combine(AppContext.BaseDirectory, applicationFileName);
                var xmlDataContractPath = Path.Combine(AppContext.BaseDirectory, dataContractFileName);
                options.IncludeXmlComments(xmlApiPath);
                options.IncludeXmlComments(xmlDataContractPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
