using System.Linq;
using System.Net.Security;
using System.Reflection;
using AutoMapper;
using CqrsFramework;
using DataAccess.MsSql;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UseCases.Order;
using UseCases.Order.CheckOrder;
using UseCases.Order.UpdateOrder;
using WebApi.Order;
using WebApi.Order.CheckOrder;

namespace WebApi
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
            services.AddControllers();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddDbContext<IDbContext, AppDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddAutoMapper(typeof(OrderMappingProfile));

            services.Scan(selector => selector.FromAssemblyOf<GetOrderRequest>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.AddScoped(typeof(CheckOrderRequestDecorator<,>));
            //services.Decorate<IRequestHandler<GetOrderRequest, OrderDto>, CheckOrderRequestDecorator<GetOrderRequest, OrderDto>>();
            //services.Decorate<IRequestHandler<UpdateOrderRequest, Unit>, CheckOrderRequestDecorator<UpdateOrderRequest, Unit>>();

            services.AddScoped(typeof(IMiddleware<,>), typeof(CheckOrderMiddleware<,>));

            services.AddScoped<IHandlerDispatcher, HandlerDispatcher>();
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
