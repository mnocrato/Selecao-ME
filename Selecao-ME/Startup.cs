using IoC;
using MediatR;
using SimpleInjector;

namespace Selecao_ME
{
    public class Startup
    {
        private readonly Container _container = IoCBoot.Start();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddMvc();

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation()
                    .AddPageModelActivation()
                    .AddTagHelperActivation();
            });

            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
