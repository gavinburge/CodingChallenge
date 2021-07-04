using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Contracts.Dtos
{
    public class BaseApiResponseDto<T>
    {
        public static BaseApiResponseDto<T> SuccessResult<T>(T data)
        {
            return new BaseApiResponseDto<T>
            {
                Success = true,
                Data = data
            };
        }

        public static BaseApiResponseDto<T> ErrorResult(ErrorDto errorDto)
        {
            return new BaseApiResponseDto<T>
            {
                Success = false,
                Errors = new List<ErrorDto> { errorDto }
            };
        }

        public static BaseApiResponseDto<T> ErrorResult(IList<ErrorDto> errorDtos)
        {
            return new BaseApiResponseDto<T>
            {
                Success = false,
                Errors = errorDtos
            };
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public IList<ErrorDto> Errors { get; set; } 
    }
}
