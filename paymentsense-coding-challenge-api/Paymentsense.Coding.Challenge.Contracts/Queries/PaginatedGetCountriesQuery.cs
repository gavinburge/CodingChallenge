using Paymentsense.Coding.Challenge.Contracts.Response;

namespace Paymentsense.Coding.Challenge.Contracts.Queries
{
    public class PaginatedGetCountriesQuery : IRequest<PaginatedGetCountriesResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
