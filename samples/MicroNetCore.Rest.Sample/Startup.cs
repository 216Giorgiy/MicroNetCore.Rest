﻿using System;
using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Rest.Extensions;
using MicroNetCore.Rest.Sample.Data;
using MicroNetCore.Rest.Sample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Sample
{
    public sealed class Startup
    {
        private static readonly IEnumerable<Type> RestTypes = new[] {typeof(User), typeof(Role)};

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcWithRestControllers(RestTypes);
            services.AddTransient<IRepositoryFactory, FakeRepositoryFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}