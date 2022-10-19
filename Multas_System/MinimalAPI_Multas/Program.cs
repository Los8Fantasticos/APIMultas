using Microsoft.EntityFrameworkCore;
using MinimalAPI_Multas.Configurations;
using MinimalAPI_Multas.Contracts.Repositories;
using MinimalAPI_Multas.Contracts.Services;
using MinimalAPI_Multas.Endpoints.Multa;
using MinimalAPI_Multas.Infrastructure;
using MinimalAPI_Multas.Infrastructure.Repositories;
using MinimalAPI_Multas.Services;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder();

var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? builder.Configuration["ConnectionStrings"]?.ToString() ?? "";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, options =>
{
    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
}), ServiceLifetime.Singleton);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLogger(builder);
builder.Services.AddScoped<MultaEndpoint>();
builder.Services.AddScoped<IMultaService, MultaService>();
builder.Services.AddScoped<IMultaRepository, MultaRepository>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var context = app.Services.GetService<ApplicationDbContext>();
    context?.Database?.Migrate();
}

app.UseHttpsRedirection();

app.Run();