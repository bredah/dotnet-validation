using ApiShared.Api;
using ApiShared.Api.ReqRes;
using ApiShared.Extensions;
using Newtonsoft.Json.Linq;
using Shouldly;
using TechTalk.SpecFlow;

namespace ApiTests.Features.Steps;

[Binding]
public class GetUserSteps
{
    private readonly UsersApi _api = new();
    private int? _requestedPage;
    private JObject? _content;

    [When(@"I request users from page (\d+)")]
    public async Task WhenIRequestUsersFromPage(int page)
    {
        _requestedPage = page;
        var result = await _api.GetByPage(page);
        _content = JObject.Parse(result.Content!);
    }

    [When(@"I request users without specifying a page")]
    public async Task WhenIRequestUsersWithoutSpecifyingAPage()
    {
        _requestedPage = 1;
        var result = await _api.GetByPage();
        _content = JObject.Parse(result.Content!);
    }

    [Then(@"the response page number should match the requested page")]
    public void ThenTheResponsePageNumberShouldMatchTheRequestedPage()
    {
        _content.ShouldContain("page", _requestedPage!.Value);
    }

    [Then(@"the response should contain page number (\d+)")]
    public void ThenTheResponseShouldContainPageNumber(int expectedPage)
    {
        _content.ShouldContain("page", expectedPage);
    }

    [Then(@"the response should contain an empty user list")]
    public void ThenTheResponseShouldContainAnEmptyUserList()
    {
        _content["data"]!.ShouldBeEmpty();
    }

    [Then(@"the user list should contain:")]
    public void ThenTheUserListShouldContain(Table table)
    {
        foreach (var row in table.Rows)
        {
            var expected = row.ToDictionary(k => k.Key, v => v.Value);
            _content["data"]!.ShouldContainMatchingObject(expected);
        }
    }

    [Then(@"the response should match the ""(.*)"" schema")]
    public void ThenTheResponseShouldMatchSchema(string schemaName)
    {
        var result = JsonSchemaValidator.Validate(schemaName, _content!.ToString());
        result.ShouldBeValid();
    }
}