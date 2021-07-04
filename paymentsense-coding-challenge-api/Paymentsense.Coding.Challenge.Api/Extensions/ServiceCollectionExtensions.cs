using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Handlers;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Services;
using Paymentsense.Coding.Challenge.Core.Validators;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IO;
using System.Net.Http;

namespace Paymentsense.Coding.Challenge.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetCountriesQuery, GetCountriesResponse>, GetCountriesHandler>();
            services.AddTransient<IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse>, PaginatedGetCountriesHandler>();
            services.AddTransient<IRequestHandler<GetCountryDetailQuery, GetCountryDetailResponse>, GetCountryDetailHandler>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICountryHttpClientService, CountryHttpClientService>(c =>
                    {
                        c.BaseAddress = new Uri(configuration.GetValue<string>("RestCountriesUrl")); ;
                    })
                    .AddPolicyHandler(GetRetryPolicy());

            services.AddSingleton<ICachingService, MemoryCachingService>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<GetCountryDetailQuery>, GetCountryDetailQueryValidator>();

            return services;
        }

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Paymentsense",
                    Version = "v1"
                });

                try
                {
                    var basePath = AppContext.BaseDirectory;

                    var directoryInfo = new DirectoryInfo(basePath).GetFiles("Paymentsense.*.xml").Length > 0
                        ? new DirectoryInfo(basePath)
                        : new DirectoryInfo(Path.Combine(basePath, @"bin\Debug\netcoreapp3.1"));

                    foreach (var item in directoryInfo.EnumerateFiles("Paymentsense.*.xml"))
                    {
                        c.IncludeXmlComments(item.FullName);
                    }
                }
                catch (Exception)
                {
                }
            });

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
