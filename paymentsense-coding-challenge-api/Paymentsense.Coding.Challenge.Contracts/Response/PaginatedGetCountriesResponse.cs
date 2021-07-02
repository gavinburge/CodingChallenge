namespace Paymentsense.Coding.Challenge.Contracts.Response
{
    public class PaginatedGetCountriesResponse : GetCountriesResponse
    {
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
    }
}
