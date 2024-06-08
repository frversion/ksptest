using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppLib1.Interfaces;
using WebAppLib1.Models;
using WebAppLib1.Repository;
using WebAppLib1.Services;

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

        services.AddControllers();
        services.AddSwaggerGen();
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
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
