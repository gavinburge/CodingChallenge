using Paymentsense.Coding.Challenge.Contracts;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Interfaces
{
    /// <summary>
    /// Interface for handling queries or commands with a response
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        Task<TResponse> Handle(TRequest request);
    }

    /// <summary>
    /// Interface for handling commands with no response
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    public interface IRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        Task Handle(TRequest request);
    }
}
