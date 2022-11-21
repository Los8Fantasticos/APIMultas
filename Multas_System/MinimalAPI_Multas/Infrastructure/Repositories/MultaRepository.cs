using Microsoft.EntityFrameworkCore;

using MinimalAPI_Multas.Contracts.Repositories;
using MinimalAPI_Multas.Models.ApplicationModel;

namespace MinimalAPI_Multas.Infrastructure.Repositories
{
    public class MultaRepository : IMultaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MultaRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> CountMultas()
        {
            var multas = await _applicationDbContext.Multa.CountAsync();
            return multas;
        }

        public async Task Insert(MultaModel multa)
        {
            _applicationDbContext?.Multa?.Add(multa);
            await _applicationDbContext?.SaveChangesAsync();
        }
    }
}
