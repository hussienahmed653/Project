using Project.Application.Common.MediatorInterfaces;

namespace Project.Infrastructure.Mediator
{
    internal class MediatorRepository : IMediatorRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public MediatorRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequestRepository<TResponse> request)
        {
            var handlertype = typeof(IRequestHandlerRepository<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _serviceProvider.GetService(handlertype)!;
            return await handler.Handle((dynamic)request);
        }
    }
}
