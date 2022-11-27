namespace MinimalAPI_Multas.Contracts.Services
{
    public interface IMultaService
    {
        public Task<int> GetTotalMultasEmitidas();
        public Task<int> ModifyPrice(int nuevoPrecio);
    }
}
