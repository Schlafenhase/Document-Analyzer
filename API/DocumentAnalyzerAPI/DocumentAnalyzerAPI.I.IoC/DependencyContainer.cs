using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.Services;
using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.I.D.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.I.IoC
{
    public class DependencyContainer
    {
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
