using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using feedback.Models;
using feedback.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace feedback
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
            services.AddCors(o => o.AddPolicy("AppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
            services.AddDbContext<FeedBackContext>(opt =>
               opt.UseInMemoryDatabase("FeedBack"));
            //services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<FeedBackContext>(options => options.UseSqlServer(dataUtilities.conString)); //ERP+Cloud

            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = Convert.ToInt64(Configuration.GetSection("FileUploadLimit").GetSection("MultipartBodyLengthLimit").Value);
                x.BufferBody = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AppPolicy");
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                 //   context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseFileServer(enableDirectoryBrowsing: false);

        }
    }
}
