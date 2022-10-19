using MinimalAPI_Multas.Contracts.Services;
using MinimalAPI_Multas.Endpoints.Errors;
using MinimalAPI_Multas.Models.ApplicationModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace MinimalAPI_Multas.Endpoints.Multa
{
    [ExcludeFromCodeCoverage]
    public class MultaEndpoint
    {
        private readonly IMultaService _patenteService;
        private readonly ILogger<MultaEndpoint> _logger;
        public MultaEndpoint(IMultaService multaService, ILoggerFactory logger)
        {
            _patenteService = multaService;
            _logger = logger.CreateLogger<MultaEndpoint>();
        }

        public async Task MapMultaEndpoints(WebApplication app)
        {
            _ = app.MapPost(
               "/api/multa",
               async (MultaModel multaModel) =>
               {
                   try
                   {
                       multaModel.Active = true;
                       _logger.LogInformation("test");
                       string result = "asdasd";
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error en endpoint Multa.");
                       throw;
                   }
               })
           .WithTags("Multa")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<MultaModel>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

        }
    }
}
