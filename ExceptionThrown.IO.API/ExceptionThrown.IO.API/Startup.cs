using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using ExceptionThrown.IO.API.Application.Queries;
using ExceptionThrown.IO.API.BuildingBlocks;
using ExceptionThrown.IO.API.Domain.PostAggregate;
using ExceptionThrown.IO.API.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;

namespace ExceptionThrown.IO.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddSingleton(_ => {
                var connection = EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@localhost:1113");

                connection.ConnectAsync().Wait();

                return connection;
            });
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostQueries, PostQueries>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton(_ =>
            {
                var settings = new Nest.ConnectionSettings(new Uri("http://localhost:9200"))
                    .DefaultMappingFor<Application.Models.Post>(m => m
                        .IndexName("posts")
                    );

                return new ElasticClient(settings);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
