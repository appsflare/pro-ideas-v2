﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProIdeas.UI.Models;
using ProIdeas.UI.Services;
using ProIdeas.Data.Mappings;
using ProIdeas.DTO;
using AspNet.Identity.Repository;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Repositories.RethinkDb;
using ProIdeas.Services;
using ProIdeas.Services.Contracts;
using ProIdeas.Logic.Contracts;
using ProIdeas.Logic;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Idea;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Bus;
using ProIdeas.Infra.EventSourcing;

namespace ProIdeas.UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingProvider = new MappingProvider();

            services.AddSingleton(mappingProvider.CreateMapper());

            services.AddMultitenancy<TenantSettingsDto, CachingTenantResolver>();

            services.AddSingleton(new ConnectionOptions
            {
                DBName = "ideas",
                HostNames = new[] { "localhost" },
                Port = 28015
            });

            services.AddScoped<ITenantLogic, TenantLogic>();
            services.AddScoped<IIdeaLogic, IdeaLogic>();
            services.AddScoped<IEventStore, DefaultEventStore>();

            services.AddScoped<IBus, InMemoryBus>();


            services.AddScoped<ITenantStore, LocalJsonTenantStore>();
            services.AddScoped<IHandler<CreateIdeaCommand>, IdeaLogic>();
            services.AddScoped<IHandler<UpdateIdeaCommand>, IdeaLogic>();
            services.AddScoped<IHandler<DeleteIdeaCommand>, IdeaLogic>();

            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IIdeaService, IdeaService>();

            services.AddScoped<IRethinkDbConnectionProvider, DefaultRethinkDbConnectionProvider>();

            services.AddScoped<IRepository, RethinkDbRepository>();

            // Add framework services.           

            services.AddIdentity<ApplicationUser, RepositoryIdentityRole>()
                .AddRoleStore<RepositoryRoleStore<RepositoryIdentityRole>>()
                .AddUserStore<RepositoryUserStore<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UsePerTenant<TenantSettingsDto>((context, appBuilder) =>
            //{
            app.UseIdentity();
            //});



            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
