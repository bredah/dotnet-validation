using System.Net;
using ApiShared.Api;
using ApiShared.Api.ReqRes;
using ApiShared.Extensions;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace ApiTests.ReqRes;

public class UsersApiTest
{
    private readonly UsersApi _api = new();
    
    [Fact]
    public async Task GetUsersByPage_WithoutPageParameter_ShouldReturnValidPagedResponse()
    {
        // Act
        var response = await _api.GetByPage();
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.ContentType.ShouldNotBeNullOrEmpty();
        response.ContentType.ShouldBe("application/json");
        var result = JsonSchemaValidator.Validate("users-list-response.schema", response.Content!);
        result.ShouldBeValid();

        var content = JObject.Parse(response.Content!);
        content.ShouldContain("page", 1);
        content.ShouldContain("per_page", 6);
        content.ShouldContain("total_pages", 2);
        content["data"]!.ShouldContainMatchingObject(new Dictionary<string, string>
        {
            ["first_name"] = "Janet",
            ["last_name"] = "Weaver",
            ["email"] = "janet.weaver@reqres.in",
            ["avatar"] = "https://reqres.in/img/faces/2-image.jpg"
        });
    }

    [Fact]
    public async Task GetUsersByPage_WithPageParameter_ShouldReturnValidPagedResponse()
    {
        // Act
        var response = await _api.GetByPage(2);
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.ContentType.ShouldNotBeNullOrEmpty();
        response.ContentType.ShouldBe("application/json");
        var result = JsonSchemaValidator.Validate("users-list-response.schema", response.Content!);
        result.ShouldBeValid();

        var content = JObject.Parse(response.Content!);
        content.ShouldContain("page", 2);
        content.ShouldContain("per_page", 6);
        content.ShouldContain("total_pages", 2);
        content["data"]!.ShouldContainMatchingObject(new Dictionary<string, string>
        {
            ["first_name"] = "Byron",
            ["last_name"] = "Fields",
            ["email"] = "byron.fields@reqres.in",
            ["avatar"] = "https://reqres.in/img/faces/10-image.jpg"
        });
    }
    
    [Fact]
    public async Task GetUsersByPage_WithOutOfRangePage_ShouldReturnNoUsers()
    {
        // Act
        var response = await _api.GetByPage(12);
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.ContentType.ShouldNotBeNullOrEmpty();
        response.ContentType.ShouldBe("application/json");
        
        var content = JObject.Parse(response.Content!);
        content.ShouldContain("page", 12);
        content["data"]!.ShouldBeEmpty();
    }

    [Fact(Skip = "Demo with invalid schema, not a valid scenario")]
    public async Task GetUsersByPage_WithInvalidSchema_ShouldFailSchemaValidation()
    {
        // Act
        var response = await _api.GetByPage(2);
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.ContentType.ShouldNotBeNullOrEmpty();
        response.ContentType.ShouldBe("application/json");
        var result = JsonSchemaValidator.Validate("users-list-response-error.schema", response.Content!);
        result.ShouldBeValid();
    }

}