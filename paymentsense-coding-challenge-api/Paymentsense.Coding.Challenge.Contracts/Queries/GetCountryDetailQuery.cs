using Paymentsense.Coding.Challenge.Contracts.Response;

namespace Paymentsense.Coding.Challenge.Contracts.Queries
{
    public class GetCountryDetailQuery : IRequest<GetCountryDetailResponse>
    {
        public string CountryName { get; set; }
    }
}
