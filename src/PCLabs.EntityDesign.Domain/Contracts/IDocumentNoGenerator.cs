namespace PCLabs.EntityDesign.Domain.Contracts
{
    public interface IDocumentNoGenerator
    {
        Task<string> GetNewOrderNo();
    }
}
