using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campaign.Infrastructure.Repositories.Interfaces
{
    public interface ICampaignRepository : IRepository<Domain.Entities.Campaign>
    {
        Task<string> InsertAsync(Domain.Entities.Campaign campaign);
        Task<bool> UpdateAsync(Domain.Entities.Campaign campaign);
        Task<List<Domain.Entities.Campaign>> GetCampaignsByName(List<string> names);
    }
}