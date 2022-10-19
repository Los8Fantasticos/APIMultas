using MinimalAPI_Multas.Contracts.Repositories;
using MinimalAPI_Multas.Contracts.Services;

namespace MinimalAPI_Multas.Services
{
    public class MultaService : IMultaService
    {
        private readonly IMultaRepository _multaRepository;
        public MultaService(IMultaRepository multaRepository)
        {
            _multaRepository = multaRepository;
        }
    }
}
