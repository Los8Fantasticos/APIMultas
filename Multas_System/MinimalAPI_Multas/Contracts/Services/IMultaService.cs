﻿namespace MinimalAPI_Multas.Contracts.Services
{
    public interface IMultaService
    {
        public Task<int> GetTotalMultasEmitidas();
    }
}
