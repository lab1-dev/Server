#region Imports
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
#endregion

namespace Server {
    public class Startup {
        #region Fields
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructor
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        #endregion

        #region ConfigureServices
        public void ConfigureServices(IServiceCollection services) {
            SetupCors(services);
            services.AddControllers();
            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab1 Server", Version = "v1" });
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }
        #endregion

        #region Configure
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            //if (env.IsDevelopment()) { //todo uncomment this line to disable Swagger UI  and Exception Page in Release Mode
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab1 Server v1"));//Swagger UI is available at http://localhost:22000/swagger
            //}
            //app.UseHttpsRedirection();
            app.UseCors("_myAllowSpecificOrigins");
            app.UseResponseCompression();
            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions {//this is necessary to serve static files under wwwroot
                ServeUnknownFileTypes = true,
                DefaultContentType = "text/plain"
            });
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseSpaStaticFiles();
            app.UseSpa(builder => { });
        }
        #endregion

        #region SetupCors
        static private void SetupCors(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("_myAllowSpecificOrigins",
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
        #endregion
    }
}