using capturerservice.Data;
using capturerservice.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace capturer_service
{
    public class Startup
    {
        private readonly string _allowOriginPolicy = "allowOriginPolicy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_allowOriginPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3007")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddRazorPages();

            services.Configure<SettingsConfig>(Configuration.GetSection("customSettings"));
            services.AddSingleton<ISettingsConfig>(s => s.GetRequiredService<IOptions<SettingsConfig>>().Value);

            services.AddSingleton<WordsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(_allowOriginPolicy);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
