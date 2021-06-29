namespace Paymentsense.Coding.Challenge.Contracts
{
    public interface IRequest<TResponse> where TResponse : IResponse
    {
    }

    public interface IRequest
    {
    }
}
