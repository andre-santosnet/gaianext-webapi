using aplicacao.mdm;
using dominio.mdm.cliente.interfacemodd;
using dominio.mdm.helper;
using infra.repositorio.mdm;
using infra.repositorio.mdm.ORM;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ui_webapi
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
            var appSettings = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettings);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddControllers(options => options.EnableEndpointRouting = false);

            services.AddCors(options =>
                        options.AddPolicy("AllowAll",
                         p => p.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddDbContext<ORMContexto>
            (
                 options =>
                 options.UseSqlServer(Configuration.GetConnectionString("ORMConnectionString")),
                 ServiceLifetime.Scoped
            );

            //Aplicação
            services.AddScoped<IAplicacaoCliente, AplicacaoCliente>();

            //Repositorios
            services.AddScoped<IRepositorioCliente, RepositorioCliente>();

            //Serviços
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ui.webapi.xml");
                c.IncludeXmlComments(xmlPath);
            });

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

            app.UseHttpsRedirection();
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("en-US"),
                new CultureInfo("pt"),
                new CultureInfo("pt-BR")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseCors("AllowAll");
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API SRS");
            });
        }
    }
}
