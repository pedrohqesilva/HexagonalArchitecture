using Core.Domain.Interfaces;
using Core.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure.Data.Repository.Repositories
{
    public abstract class SequenceRepository : ISequenceRepository
    {
        private readonly BaseContext _contexto;

        public SequenceRepository(BaseContext context)
        {
            _contexto = context;
        }

        public async virtual Task<long> ObterProxima(string sequenceName, CancellationToken cancellationToken)
        {
            var connection = _contexto.Database.GetDbConnection();

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT {sequenceName}.NEXTVAL FROM DUAL";

            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                return reader.GetInt64(0);
            }

            return default;
        }
    }
}