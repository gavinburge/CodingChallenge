using Paymentsense.Coding.Challenge.Contracts.Dtos;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Paymentsense.Coding.Challenge.Core.Mappers
{
    public static class CountryMapper
    {
        public static IEnumerable<CountryDto> ToContractCountries(this IEnumerable<Country> countries)
        {
            return countries?.Select(c =>
                new CountryDto
                {
                    Name = c.Name,
                    Flag = c.Flag
                })
                ?? new List<CountryDto>();
        }

        public static GetCountryDetailResponse ToGetCountryDetailResponse(this CountryDetail countryDetail)
        {
            return new GetCountryDetailResponse
            {
                Name = countryDetail.Name,
                Flag = countryDetail.Flag,
                BorderingCountries = countryDetail.BorderingCountries,
                CapitalCity = countryDetail.CapitalCity,
                Currencies = countryDetail.Currencies.Select(c => c.ToCurrencyDto()).ToList(),
                Languages = countryDetail.Languages.Select(l => l.ToLanguageDto()).ToList(),
                Population = countryDetail.Population,
                TimeZones = countryDetail.TimeZones
            };
        }

        public static CurrencyDto ToCurrencyDto(this Currency currency)
        {
            return new CurrencyDto
            {
                Code = currency.Code,
                Name = currency.Name,
                Symbol = currency.Symbol
            };
        }

        public static LanguageDto ToLanguageDto(this Language language)
        {
            return new LanguageDto
            {
                Iso639_1 = language.Iso639_1,
                Iso639_2 = language.Iso639_2,
                Name = language.Name,
                NativeName = language.NativeName
            };
        }
    }
}
