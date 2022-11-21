using MinimalAPI_Multas.Models.ApplicationModel;

namespace MinimalAPI_Multas.Contracts.Repositories
{
    public interface IMultaRepository
    {
        public Task Insert(MultaModel multa);
        public Task<int> CountMultas();
    }
}
