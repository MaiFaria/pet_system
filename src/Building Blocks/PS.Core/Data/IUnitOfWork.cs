namespace PS.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
