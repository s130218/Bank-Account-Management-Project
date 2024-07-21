using AccountManage.Web.Enum;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using AccountManagement.Web.Models.DTO.Common;

namespace AccountManage.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string JwtToken;
        private readonly string BaseUrl = "https://localhost:7002";

        public BaseService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;

            if (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue("JwtToken", out var token) is true)
                JwtToken = token;
        }

        public async Task<ServiceResult<object>> SendAsync(RequestDto requestDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("AccountAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            requestDto.Url = BaseUrl + requestDto.Url;

            //token
            requestDto.AccessToken = JwtToken;

            message.RequestUri = new Uri(requestDto.Url);

            if (requestDto.Url != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = null;

            message.Method = requestDto.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.DELETE => HttpMethod.Delete,
                ApiType.PUT => HttpMethod.Put,
                _ => HttpMethod.Get,
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

            apiResponse = await client.SendAsync(message);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { Status = false, Message = ["Not Found"] };
                case HttpStatusCode.Forbidden:
                    return new() { Status = false, Message = ["Access Denied"] };
                case HttpStatusCode.Unauthorized:
                    return new() { Status = false, Message = ["Unauthorized"] };
                case HttpStatusCode.InternalServerError:
                    return new() { Status = false, Message = ["Internal Server Error"] };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ServiceResult<object>>(apiContent);
                    return apiResponseDto;
            }
        }
    }
}
