using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using StockTradingExchange.API.Configuration;
using StockTradingExchange.Application.Configuration;
using StockTradingExchange.Application.Configuration.Data;
using StockTradingExchange.Application.Stocks.BuyStocks;
using StockTradingExchange.Application.Stocks.SellStocks;
using StockTradingExchange.Application.Stocks.SendQuotes;
using StockTradingExchange.Domain.SeedWork;
using StockTradingExchange.Domain.StockTrading;
using StockTradingExchange.Infrastructure;
using StockTradingExchange.Infrastructure.Database;
using StockTradingExchange.Infrastructure.Domain;
using StockTradingExchange.Infrastructure.Domain.StockTrading;
using System;
using System.Net;
using System.Reflection;

[assembly: UserSecretsId("99b092d4-9e6b-4edd-a49f-02d57358845e")]
namespace StockTradingExchangeAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string StockConnectionString = "StockConnectionString";
        private static ILogger _logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _logger = ConfigureLogger();
            _logger.Information("Logger configured");

            this._configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddJsonFile($"hosting.{env.EnvironmentName}.json")
                .AddUserSecrets<Startup>()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "", Version = "v1" });
            });

            services.AddMediatR(typeof(Startup));
            services.AddScoped<ISqlConnectionFactory>(x =>
            {
                var sqlService = new SqlConnectionFactory(_configuration["ConnectionStrings:StockTradingConnection"]);
                return sqlService;
            });


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            services.AddDbContext<CustomerTradingContext>(options => options.UseSqlServer(
                _configuration["ConnectionStrings:StockTradingConnection"],
                b => b.MigrationsAssembly(typeof(CustomerTradingContext).Assembly.FullName)));

            services.AddDbContext<CustomerQuotesContext>(options => options.UseSqlServer(
                _configuration["ConnectionStrings:StockTradingConnection"],
                b => b.MigrationsAssembly(typeof(CustomerQuotesContext).Assembly.FullName)));

            services.AddScoped<ICustomerTradingRepository, CustomerTradingRepository>();
            services.AddScoped<ICustomerQuotesRepository, CustomerQuotesRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BuyStockCommand).Assembly, typeof(BuyStockCommandHandler).Assembly);
            services.AddMediatR(typeof(SellStockCommand).Assembly, typeof(SellStockCommandHandler).Assembly);
            services.AddMediatR(typeof(SendQuotesCommand).Assembly, typeof(SendQuotesCommandHandler).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockTradingExchangeAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "text/html";
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (null != exceptionObject)
                    {
                        var errorMessage = $"{exceptionObject.Error.Message}";
                        await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                    }
                });

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }

        private static ILogger ConfigureLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();
        }
    }
}
