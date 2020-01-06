using CorrelationId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleApi
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
            //services.AddDefaultCorrelationId(options =>
            //{
            //    options.CorrelationIdGenerator = () => "Foo";
            //    options.AddToLoggingScope = true;
            //    options.EnforceHeader = true;
            //    options.IgnoreRequestHeader = false;
            //    options.IncludeInResponse = true;
            //    options.RequestHeader = "My-Custom-Correlation-Id";
            //    options.ResponseHeader = "X-Correlation-Id";
            //    options.UpdateTraceIdentifier = true;
            //});
            //services.AddCorrelationId();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IValuesService, ValuesService>();
            services.AddScoped<IQueueClientFactory, QueueClientFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseCorrelationId();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<CurrentRequestMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
