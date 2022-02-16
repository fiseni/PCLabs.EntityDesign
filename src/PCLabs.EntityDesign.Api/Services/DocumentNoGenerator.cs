using Microsoft.EntityFrameworkCore;
using PCLabs.EntityDesign.Api.DataAccess;
using PCLabs.EntityDesign.Domain.Contracts;

namespace PCLabs.EntityDesign.Api.Services
{
    public class DocumentNoGenerator : IDocumentNoGenerator
    {
        private readonly AppDbContext _dbContext;

        public DocumentNoGenerator(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetNewOrderNo()
        {
            var lastNo = await _dbContext.Orders
                        .Select(x => x.OrderNo)
                        .OrderByDescending(x => x.Length)
                        .ThenByDescending(x => x)
                        .FirstOrDefaultAsync();

            return (Convert.ToInt32(lastNo) + 1).ToString("D6");
        }
    }
}
