namespace Project.Application.Common.MediatorInterfaces
{
    public interface IMediatorRepository
    {
        Task<TResponse> Send<TResponse>(IRequestRepository<TResponse> request);
    }
}
