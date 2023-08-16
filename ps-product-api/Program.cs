using ps_product_api;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices()
            .ConfigurePipeline();

app.Run();