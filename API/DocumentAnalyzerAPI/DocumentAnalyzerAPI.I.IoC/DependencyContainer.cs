using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.Services;
using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.I.D.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentAnalyzerAPI.I.IoC
{
    public class DependencyContainer
    {
        /// <summary>
        /// Method in charge of creating dependencies of the MVC project
        /// </summary>
        /// <param name="services">
        /// Services that will be added the new dependencies
        /// </param>
        public static void RegisterServices(IServiceCollection services)
        {
            //DocumentAnalyzerAPI.A
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<INLPService, NLPService>();
            services.AddScoped<IAzureBlobAuthService, AzureBlobAuthService>();

            //DocumentAnalyzerAPI.D.Interfaces | DocumentAnalyzerAPI.I.D.Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
        }
    }
}
