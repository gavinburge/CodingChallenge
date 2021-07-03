using Paymentsense.Coding.Challenge.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Interfaces
{
    public interface ICountryHttpClientService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<CountryDetail> GetCountryDetailAsync(string country);
    }
}