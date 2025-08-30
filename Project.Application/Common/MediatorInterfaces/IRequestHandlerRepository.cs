namespace Project.Application.Common.MediatorInterfaces
{
    public interface IRequestHandlerRepository<TRequest, TRespnse> where TRequest : IRequestRepository<TRespnse>
    {
        Task<TRespnse> Handle(TRequest request);
    }
}
