using MinimalAPI_Multas.Contracts.Repositories;

namespace MinimalAPI_Multas.Infrastructure.Repositories
{
    public class MultaRepository : IMultaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MultaRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

    }
}
