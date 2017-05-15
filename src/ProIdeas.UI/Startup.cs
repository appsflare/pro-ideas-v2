using AspNet.Identity.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProIdeas.Authentication.Contracts;
using ProIdeas.Data.Mappings;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Commands;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.RehtinkDb.QueryTemplates;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Repositories.RethinkDb;
using ProIdeas.DTO;
using ProIdeas.Files.Contracts;
using ProIdeas.Infra.Bus;
using ProIdeas.Infra.Commands.Collaboration;
using ProIdeas.Infra.Events;
using ProIdeas.Infra.EventSourcing;
using ProIdeas.Logic;
using ProIdeas.Logic.Contracts;
using ProIdeas.Serializers;
using ProIdeas.Serializers.Contracts;
using ProIdeas.Services;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Authentication;
using ProIdeas.UI.Models;
using ProIdeas.UI.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProIdeas.UI
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            _env = env;

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

        private static void RegisterQueryTemplates(IServiceCollection services, IList<Type> exportedTypes)
        {
            var queryTemplateInterface = typeof(IQueryTemplate);

            var queryTemplateTypes = exportedTypes
                 .Where(i => !i.GetTypeInfo().IsAbstract)
                 .Where(i => queryTemplateInterface.IsAssignableFrom(i))
                 .ToList();

            queryTemplateTypes.ForEach(template =>
            {
                services.AddTransient(queryTemplateInterface, template);
            });

            services.AddSingleton<IQueryTemplateFinder, DefaultQueryTemplateFinder>();

        }

        private static void RegisterAllGenericTypeImplementations(IServiceCollection services, IList<Type> exportedTypes, Type baseGenericType, Type genericInterfaceType)
        {

            var possibleGenericTypes = exportedTypes
                .Where(baseGenericType.IsAssignableFrom)
                .Select(i => genericInterfaceType.MakeGenericType(i));



            foreach (var possibleGenericType in possibleGenericTypes)
            {
                var implementedTypes = exportedTypes.Where(i => possibleGenericType.IsAssignableFrom(i)).ToList();


                foreach (var implementedType in implementedTypes)
                {
                    services.AddScoped(possibleGenericType, implementedType);
                }
            }

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            if (_env.IsProduction())
            {
                // Connect to Redis database.
                try
                {
                    var redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_HOST"));
                    services.AddDataProtection()
                        .PersistKeysToRedis(redis, "DataProtection-Keys")
                        .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            var mappingProvider = new MappingProvider();

            services.AddSingleton(mappingProvider.CreateMapper());
            services.AddSingleton<IUserIdentityProvider, AspNetUserIdentityProvider>();
            services.AddSingleton<IJsonSerializer, JsonSerializer>();
            services.AddMultitenancy<TenantSettingsDto, CachingTenantResolver>();

            var dbHost = Environment.GetEnvironmentVariable("DB_HOSTS");
            services.AddSingleton(new ConnectionOptions
            {
                DBName = "ideas",
                HostNames = string.IsNullOrEmpty(dbHost) ? new[] { "localhost" } : Environment.GetEnvironmentVariable("DB_HOSTS").Split(';'),
                Port = 28015
            });

            services.AddScoped<ITenantLogic, TenantLogic>();
            services.AddScoped<IIdeaLogic, IdeaLogic>();
            services.AddScoped<IIdeaCollaborationLogic, IdeaCollaborationLogic>();
            services.AddScoped<IUserProfileLogic, UserProfileLogic>();
            services.AddScoped<IActivityLogic, ActivityLogic>();
            services.AddScoped<ITaskBoardLogic, TaskBoardLogic>();

            services.AddScoped<IEventStore, DefaultEventStore>();

            services.AddScoped<IBus, InMemoryBus>();

            services.AddScoped<ITenantStore, LocalJsonTenantStore>();


            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IIdeaService, IdeaService>();
            services.AddScoped<IIdeaCollaborationService, IdeaCollaborationService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<ITaskBoardService, TaskBoardService>();


            services.AddScoped<IRethinkDbConnectionProvider, DefaultRethinkDbConnectionProvider>();

            services.AddScoped<IRepository, RethinkDbRepository>();
            services.AddScoped<IFileStorage, ReGridStorageProvider>();

            services.AddSingleton<DatabaseConfigurator>();

            // Add framework services.           

            services.AddIdentity<ApplicationUser, RepositoryIdentityRole>()
                .AddRoleStore<RepositoryRoleStore<RepositoryIdentityRole>>()
                .AddUserStore<RepositoryUserStore<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();


            var type1 = typeof(FilterIdeaQueryTemplate);
            var type2 = typeof(LikeIdeaCommand);
            var type3 = typeof(IdeaLikeChangedEvent);

            var allExportedTypes = Assembly.GetEntryAssembly()
                  .GetReferencedAssemblies()
                  .Select(i => Assembly.Load(i))
                  .SelectMany(i => i.GetTypes())
                  .ToList();

            RegisterAllGenericTypeImplementations(services, allExportedTypes, typeof(Message), typeof(IMessageFilter<>));
            RegisterAllGenericTypeImplementations(services, allExportedTypes, typeof(Command), typeof(IHandler<>));
            RegisterAllGenericTypeImplementations(services, allExportedTypes, typeof(Event), typeof(IHandler<>));


            RegisterQueryTemplates(services, allExportedTypes);



            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                scope.ServiceProvider
                       .GetService<DatabaseConfigurator>()
                       .EnsureDB()
                       .EnsureTables(nameof(ApplicationUser),
                       nameof(Idea),
                       nameof(IdeaComment),
                       nameof(IdeaLike),
                       nameof(Page),
                       nameof(StoredEvent),
                       nameof(UserProfile),
                       nameof(Activity))
                       .EnsureTableIndex(nameof(Idea), nameof(IdeaComment.OwnerId))
                       .EnsureTableIndex(nameof(Idea), nameof(Idea.Comments))
                       .EnsureTableIndex(nameof(IdeaComment), nameof(IdeaComment.CreatedOn))
                       .EnsureTableIndex(nameof(IdeaComment), nameof(IdeaComment.CreatedOn))
                       .EnsureTableIndex(nameof(IdeaLike), nameof(IdeaLike.OwnerId))
                       .EnsureTableIndex(nameof(UserProfile), nameof(UserProfile.OwnerId))
                       .EnsureTableIndex(nameof(Activity), nameof(Activity.OwnerId))
                       .EnsureTableIndex(nameof(Activity), nameof(Activity.CreatedAt));
            }

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
