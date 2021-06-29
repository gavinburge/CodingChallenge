using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Handlers
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, GetCountriesResponse>
    {
        public async Task<GetCountriesResponse> Handle(GetCountriesQuery request)
        {
            return await Task.FromResult(new GetCountriesResponse());
        }
    }
}
