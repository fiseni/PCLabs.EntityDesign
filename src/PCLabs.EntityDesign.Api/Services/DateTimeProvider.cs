using PCLabs.EntityDesign.Domain.Contracts;

namespace PCLabs.EntityDesign.Api.Services
{
    public class DateTimeProvider : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
