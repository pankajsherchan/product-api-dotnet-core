using Microsoft.EntityFrameworkCore;
using ps_product_api.Contexts;
using ps_product_api.Profiles;
using ps_product_api.Services;

namespace ps_product_api
{
  internal static class StartupHelperExtensions
  {
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
      builder.Services.AddControllers();

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      // both of these are for swagger docs
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var connectionString = builder.Configuration.GetConnectionString("ps-productDb");
      builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite(connectionString));
      builder.Services.AddScoped<IUserRepository, UserRepository>();
      builder.Services.AddScoped<IProductRepository, ProductRepository>();

      builder.Services.AddAutoMapper(options =>
      {
        options.AddProfile<UserProfile>();
        options.AddProfile<ProductProfile>();
      });

      return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      // app.UseHttpsRedirection();

      // app.UseAuthorization();

      // combines app.UseRouting() and app.UseEndpoints() to provide attribute based routing
      app.MapControllers();

      return app;
    }

  }
}