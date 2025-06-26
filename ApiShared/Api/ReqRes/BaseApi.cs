using RestSharp;

namespace ApiShared.Api.ReqRes;

public abstract class BaseApi
{
    private const string BaseApiUrl = "https://reqres.in";
    protected readonly IRestClient Client;

    protected BaseApi()
    {
        var options = new RestClientOptions(BaseApiUrl)
        {
            ThrowOnAnyError = true,
            Timeout = TimeSpan.FromMilliseconds(2000)
        };
        Client = new RestClient(options);
        Client.AddDefaultHeader("x-api-key", "reqres-free-v1");
    }
}