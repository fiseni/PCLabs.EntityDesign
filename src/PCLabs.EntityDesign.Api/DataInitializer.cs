using Microsoft.EntityFrameworkCore;
using PCLabs.EntityDesign.Api.DataAccess;
using PCLabs.EntityDesign.Api.DataAccess.Seeds;
using PCLabs.EntityDesign.Domain.Contracts;

namespace PCLabs.EntityDesign.Api
{
    public class DataInitializer
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<DataInitializer> log;
        private readonly IDateTime dateTimeService;

        public DataInitializer(AppDbContext dbContext,
                               ILogger<DataInitializer> logger,
                               IDateTime dateTimeService)
        {
            this.dbContext = dbContext;
            this.log = logger;
            this.dateTimeService = dateTimeService;
        }

        public async Task SeedAsync(int retry = 0)
        {
            try
            {
                dbContext.Database.Migrate();

                if (await dbContext.Orders.CountAsync() == 0)
                {
                    dbContext.Orders.AddRange(OrderSeed.GetOrders(dateTimeService));
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                log.LogError("Error Occured while migrating/seeding.", ex);

                if (retry > 0)
                {
                    log.LogError("Retrying");
                    await SeedAsync(retry - 1);
                }
            }
        }
    }
}
