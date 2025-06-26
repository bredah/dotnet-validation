using RestSharp;

namespace ApiShared.Api.ReqRes;

public class UsersApi : BaseApi
{
    public async Task<RestResponse> GetByPage(int page = 1)
    {
        var request = new RestRequest($"/api/users?page={page}");
        return await Client.GetAsync(request);
    }

    public async Task<RestResponse> GetById(int id = 0)
    {
        var request = new RestRequest($"/api/users/{id}");
        return await Client.GetAsync(request);
    }
}