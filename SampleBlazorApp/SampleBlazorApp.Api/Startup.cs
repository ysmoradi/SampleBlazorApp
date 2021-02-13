using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO.Compression;

namespace SampleBlazorApp.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(opts =>
            {
                opts.Providers.Add<BrotliCompressionProvider>();
                opts.Providers.Add<GzipCompressionProvider>();
            })
                .Configure<BrotliCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Fastest)
                .Configure<GzipCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Fastest);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Blazor App", Version = "v1" });
            });
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseResponseCompression();

            app.UseRouting();

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Blazor App v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
