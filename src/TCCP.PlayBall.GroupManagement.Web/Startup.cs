﻿using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Tccp.PlayBall.GroupManagement.Web.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tccp.PlayBall.GroupManagement.Web.Demo.Middlewares;
using Tccp.PlayBall.GroupManagement.Web.Demo.Filters;

namespace Tccp.PlayBall.GroupManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //registro das classes de Filters e Action Filter
            services.AddMvc(options =>
            {
                options.Filters.Add<DemoActionFilter>();
            });

            services.AddTransient<RequestTimingFactoryMiddleware>();

            //Classe de Exceptions customizadas com Actions Filter
            services.AddTransient<DemoExceptionFilter>();
            services.AddBusiness();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.MapWhen(
                context => context.Request.Headers.ContainsKey("ping"),
                builder =>
                {
                    builder.UseMiddleware<RequestTimingAdHocMiddleware>();
                    builder.Run(async (context) => { await context.Response.WriteAsync("pong from header"); });
                });
            
            app.Map("/ping", builder =>
            {
                builder.UseMiddleware<RequestTimingFactoryMiddleware>();
                builder.Run(async (context) => { await context.Response.WriteAsync("pong from path"); });
            });

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-Powered-By", "ASP.NET Core");
                    return Task.CompletedTask;
                });

                await next.Invoke();
            });

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("No middlewares could handle the request");
            });
        }
    }
}