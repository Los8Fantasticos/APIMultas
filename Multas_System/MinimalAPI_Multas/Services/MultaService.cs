using MinimalAPI_Multas.Contracts.Repositories;
using MinimalAPI_Multas.Contracts.Services;
using MinimalAPI_Multas.Models.ApplicationModel;

using RabbitMqService.Abstractions;

namespace MinimalAPI_Multas.Services
{
    public class MultaService : IMultaService, IMessageReceiver<string>
    {
        private readonly IMultaRepository _multaRepository;
        private readonly ILogger logger;
        public MultaService(IMultaRepository multaRepository, ILogger<MultaService> logger)
        {
            _multaRepository = multaRepository;
            this.logger = logger;
        }
        public async Task ReceiveAsync(string message, CancellationToken cancellationToken)
        {
            logger.LogInformation("Mensaje recibido para multar una patente");
           
            MultaModel multaModel = new MultaModel();
            multaModel.Patente = message;
            //traer del settings el monto de la multa
            multaModel.Monto = "1000";
            
            await _multaRepository.Insert(multaModel);                        

            logger.LogInformation($"Patente {message} multada.");
        }
    }
}
