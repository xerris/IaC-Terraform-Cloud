using System.Threading.Tasks;

namespace DevOps.Api.Service
{
    public interface IHealthCheckService
    {
        Task<bool> Ping();
    }

    public class HealthCheckService : IHealthCheckService
    {
        public async Task<bool> Ping()
        {
            return await Task.FromResult(true);
        }
    }
}