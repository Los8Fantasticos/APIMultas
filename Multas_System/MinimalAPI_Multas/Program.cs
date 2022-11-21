using Microsoft.EntityFrameworkCore;
using MinimalAPI_Multas.Configurations;
using MinimalAPI_Multas.Contracts.Repositories;
using MinimalAPI_Multas.Contracts.Services;
using MinimalAPI_Multas.Endpoints.Multa;
using MinimalAPI_Multas.Infrastructure;
using MinimalAPI_Multas.Infrastructure.Repositories;
using MinimalAPI_Multas.Receiver;
using MinimalAPI_Multas.Services;

using RabbitMqService.Queues;
using RabbitMqService.RabbitMq;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder();


ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var databaseContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    if (databaseContext != null)
    {
        //databaseContext.Database.EnsureCreated();
    }
    scope.ServiceProvider.GetService<MultaService>();
    scope.ServiceProvider.GetService<MultaEndpoint>()?.MapMultaEndpoints(app);
}

Configure(app, app.Environment);
app.Run();



void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{

    var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? builder.Configuration["ConnectionStrings"]?.ToString() ?? "";

    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, options =>
    {
        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
    }), ServiceLifetime.Singleton);

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.ConfigureLogger(builder);
    services.AddScoped<MultaEndpoint>();
    services.AddScoped<IMultaService, MultaService>();
    services.AddScoped<IMultaRepository, MultaRepository>();

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetValue<string>("RabbitMq:ConnectionString");
        settings.ExchangeName = configuration.GetValue<string>("AppSettings:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("AppSettings:QueuePrefetchCount");
    }, queues =>
    {
        queues.Add<Multas>();
    })
    .AddReceiver<PatenteReceiver<string>, string, MultaService>();

    //builder.Services.AddConfig<ApiReconocimientoConfig>(builder.Configuration, nameof(ApiReconocimientoConfig));
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        var context = app.ApplicationServices.GetService<ApplicationDbContext>();
        context?.Database?.Migrate();
        //context?.AddPatente(randomBoolean: true, count: 50);
    }

    app.UseHttpsRedirection();

    app.UseRouting();
}