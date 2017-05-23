using AspNet.Identity.Repository;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
using Serilog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

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
                    var redisHostName = Environment.GetEnvironmentVariable("REDIS_HOST_NAME");

                    var redisHostPort = Environment.GetEnvironmentVariable("REDIS_HOST_PORT");

                    // Get DNS host information.
                    var hostAdressesTask = Dns.GetHostAddressesAsync(redisHostName);

                    hostAdressesTask.Wait();

                    var redisHost = hostAdressesTask.Result.First();

                    var redis = ConnectionMultiplexer.Connect($"{redisHost.MapToIPv4()}:{redisHostPort}");
                    services.AddDataProtection()
                        .PersistKeysToRedis(redis, "DataProtection-Keys")
                        .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            var mappingProvider = new MappingProvider();

            services.AddSingleton(mappingProvider.CreateMapper());
            services.AddSingleton<IUserIdentityProvider, AspNetUserIdentityProvider>();
            services.AddSingleton<IJsonSerializer, JsonSerializer>();
            //services.AddMultitenancy<TenantSettingsDto, CachingTenantResolver>();

            services.AddSingleton(new AuthMessageSenderOptions
            {
                DefaultSenderEmail = Environment.GetEnvironmentVariable("EMAIL_DEFAULT_SENDER_EMAIL") ?? "noreply@lotofideas.com",
                DefaultSenderName = Environment.GetEnvironmentVariable("EMAIL_DEFAULT_SENDER_NAME") ?? "Lot Of Ideas",
                MailJetApiKey = Environment.GetEnvironmentVariable("EMAIL_MAILJET_API_KEY"),
                MailJetApiSecret = Environment.GetEnvironmentVariable("EMAIL_MAILJET_API_SECRET")
            });

            // Add application services.
            services.AddSingleton<IEmailSender, AuthMessageSender>();
            services.AddSingleton<ISmsSender, AuthMessageSender>();

            var dbHost = Environment.GetEnvironmentVariable("DB_HOSTS");
            services.AddSingleton(new ConnectionOptions
            {
                DBName = "ideas",
                HostNames = string.IsNullOrEmpty(dbHost) ? new[] { "localhost" } : Environment.GetEnvironmentVariable("DB_HOSTS").Split(';'),
                Port = 28015
            });

            services.AddScoped<ITenantLogic, TenantLogic>();
            services.AddScoped<IIdeaLogic, IdeaLogic>();
            services.AddScoped<ITeamLogic, TeamLogic>();
            services.AddScoped<IIdeaCollaborationLogic, IdeaCollaborationLogic>();
            services.AddScoped<IUserProfileLogic, UserProfileLogic>();
            services.AddScoped<IActivityLogic, ActivityLogic>();
            services.AddScoped<ITaskBoardLogic, TaskBoardLogic>();

            services.AddScoped<IEventStore, DefaultEventStore>();

            services.AddScoped<IBus, InMemoryBus>();

            services.AddScoped<ITenantStore, LocalJsonTenantStore>();


            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IIdeaService, IdeaService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IIdeaCollaborationService, IdeaCollaborationService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<ITaskBoardService, TaskBoardService>();


            services.AddScoped<IRethinkDbConnectionProvider, DefaultRethinkDbConnectionProvider>();

            services.AddScoped<IRepository, RethinkDbRepository>();
            services.AddScoped<IFileStorage, ReGridStorageProvider>();

            services.AddSingleton<DatabaseConfigurator>();

            // Add framework services.           

            services.AddIdentity<ApplicationUser, RepositoryIdentityRole>(options =>
            {

                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

                options.SignIn.RequireConfirmedEmail = true;



            })
                .AddRoleStore<RepositoryRoleStore<RepositoryIdentityRole>>()
                .AddUserStore<RepositoryUserStore<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddMvc();




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
                       nameof(Team),
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
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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


            if (Environment.GetEnvironmentVariable("COCKPIT_AUTH_ENABLED") == "true")
            {
                var openIdConnectOptions = new OpenIdConnectOptions
                {
                    Authority = Configuration.GetValue<string>("Cockpit.Authority") ?? Environment.GetEnvironmentVariable("COCKPIT_AUTH_IDENTITY_SERVER_URL"),
                    AuthenticationScheme = "Agile Cockpit",
                    AutomaticChallenge = false,
                    ClientId = Configuration.GetValue<string>("Cockpit.ClientId") ?? Environment.GetEnvironmentVariable("COCKPIT_AUTH_CLIENT_ID"),
                    ClientSecret = Configuration.GetValue<string>("Cockpit.ClientSecret") ?? Environment.GetEnvironmentVariable("COCKPIT_AUTH_CLIENT_SECRET"),
                    DisplayName = "Agile Cockpit Identity",
                    ResponseType = OpenIdConnectResponseType.Code,
                    GetClaimsFromUserInfoEndpoint = true,                    
                    RequireHttpsMetadata = env.IsProduction()
                };
                openIdConnectOptions.Events = new OpenIdConnectEvents
                {                    
                    OnRedirectToIdentityProvider = context =>
                    {
                        
                        //send the saved id_token to identity provider so that it can detect the client and redirect after successfull logout
                        if (context.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                        {
                            var idTokenHint = context.HttpContext.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                context.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }


                        if (openIdConnectOptions.RequireHttpsMetadata)
                        {
                            var uri = new Uri(context.ProtocolMessage.RedirectUri);
                            if (uri.Scheme != "https")
                            {
                                context.ProtocolMessage.RedirectUri = $"https://{uri.Authority}{uri.PathAndQuery}";
                            }
                        }

                        return Task.FromResult(0);
                    }
                };
                openIdConnectOptions.Scope.Add("email");
                openIdConnectOptions.Scope.Add("profile");
                openIdConnectOptions.Description.DisplayName = openIdConnectOptions.DisplayName;

                app.UseOpenIdConnectAuthentication(openIdConnectOptions);
            }

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
