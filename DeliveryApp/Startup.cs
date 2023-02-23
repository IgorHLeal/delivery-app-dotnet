using DeliveryApp.Context;
using DeliveryApp.Models;
using DeliveryApp.Repositories;
using DeliveryApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp;
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
        // A instância AppDbContext foi utilizada como service pra que possa ser injetada em qualquer local do projeto
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Configuração dos repositórios criados;Injeção de dependência
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoryRepository, CategoriaRepository>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        // Habilita o uso dos recursos do HttpContext como request, response, autenticação
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddControllersWithViews();

        // Ativa o uso do cache
        services.AddMemoryCache();
        // Configura o serviço do middleware da Session
        services.AddSession();
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
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Ativa o middleware da Session
        app.UseSession();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
