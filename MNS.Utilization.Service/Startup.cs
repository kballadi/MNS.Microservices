using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MNS.Services.Utilization.Api.Messaging;
using MNS.Services.Utilization.Infrastructre.Data;
using MNS.Services.Utilization.Infrastructure.Repos;
using MNS.Services.Utilization.Infrastructure.Services;
using MNS.Services.Utilization.Services;
using MNS.Utilization.Service.Api.Messaging;
using MNS.Utilization.Service.Api.Middlewares;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Polly;
using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;

namespace MNS.Utilization.Service.Api
{
    /// <summary>
    /// Startup class required to configure the respective pipeline to run the services.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Ctor for Startup class
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Config Property
        /// </summary>
        public IConfiguration Configuration { get; }

        // 
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddlerWare>();
            services.AddControllers().AddNewtonsoftJson(services => services.SerializerSettings.ContractResolver =
                                                              new CamelCasePropertyNamesContractResolver())
                    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                    .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);

                        /// TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                        return result;
                    });

            services.AddDbContext<UtilizationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUtilizationRepository, UtilizationRepository>();
            services.AddSingleton<IAzServiceBusConsumer, AzServiceBusConsumer>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MNS.Utilization.Service",
                    Version = "v1",
                    Description = "A Mobile Network Service Management API to get the utilization of customers mobile plan.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Mobile Network Service Management Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Mobile Network Service Management License",
                        Url = new Uri("https://example.com/license")
                    }
                }
                );

                /// using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                c.UseInlineDefinitionsForEnums();
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient<ICustomerService, CustomerService>(c => c.BaseAddress = new Uri(Configuration["ApiConfigs:Customer:Uri"]))
                    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(10, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp))))
                    .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)));
            services.AddHttpClient<IMobilePlanService, MobilePlanService>(c => c.BaseAddress = new Uri(Configuration["ApiConfigs:Plans:Uri"]))
                    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(10, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp))))
                    .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)));
            services.AddHttpClient<ICustomerMobilePlanService, CustomerMobilePlanService>(c => c.BaseAddress = new Uri(Configuration["ApiConfigs:CustomerMobilePlan:Uri"]))
                    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(10, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp))))
                    .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)));
        }

        // 
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MNS.Utilization.Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddlerWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
