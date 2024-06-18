using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Repository;
using WebAppLib1.Services;
using WebAppLib1.DB;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        SeedDatabase(host);
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void SeedDatabase(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<LibraryContext>();
            DBInitializer.Initialize(context);
        }
    }
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<LibraryContext>(options =>
            options.UseInMemoryDatabase("LibraryDB"));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ILibroRepository, LibroRepository>();
        services.AddScoped<IPrestamoRepository, PrestamoRepository>();

        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ILibroService, LibroService>();
        services.AddScoped<IPrestamoService, PrestamoService>();

        //services.AddControllers();
        services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "kspapp",
                    ValidAudience = "kspapp",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CustomClaveSecretaSuperSegura123!ForAuthentication%KSP"))
                };
            });

        services.AddAuthorization(
            options => {
                options.AddPolicy("Authenticated", policy => { policy.RequireAuthenticatedUser(); });
            }
            );

        // CORS - Allow calling the API from WebBrowsers
        services.AddCors();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Agrega la palabra 'Bearer' seguida de un espacio y el valor de token JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins seperated with comma
           .SetIsOriginAllowed(origin => true));// Allow any origin  

        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllers();
        //});
        app.UseEndpoints(endpoints =>
        {
            //endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home/{action=HomePage}/{id?}");

            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization("Authenticated");
        });
        
    }
}
