using Newtonsoft.Json.Linq;
using Shouldly;

namespace ApiShared.Extensions;

public static class JsonAssertionsExtensions
{
    public static void ShouldContain<T>(this JObject? json, string path, T expectedValue)
    {
        var token = json.SelectToken(path);
        token.ShouldNotBeNull($"Expected path '{path}' to exist");

        T? actual;
        try
        {
            actual = token!.ToObject<T>();
        }
        catch (Exception ex)
        {
            throw new InvalidCastException(
                $"Failed to convert value at '{path}' to type '{typeof(T).Name}': {ex.Message}", ex);
        }

        actual.ShouldBe(expectedValue, $"Expected '{path}' to be '{expectedValue}'");
    }

    public static void ShouldContainValueIn(this JObject json, string arrayPath, string expectedValue)
    {
        var tokens = json.SelectTokens(arrayPath).ToList();

        tokens.ShouldNotBeNull($"Expected path '{arrayPath}' to exist");
        tokens.Count.ShouldBeGreaterThan(0, $"Expected at least one item in '{arrayPath}'");

        var stringValues = tokens.Select(t => t?.ToString()).ToList();
        stringValues.ShouldContain(expectedValue, $"Expected '{expectedValue}' to be present in '{arrayPath}'");
    }

    public static void ShouldContainMatchingObject(this JToken arrayToken, IDictionary<string, string> expectedFields)
    {
        arrayToken.Type.ShouldBe(JTokenType.Array, "Expected a JSON array to search in");

        var matched = arrayToken
            .Children<JObject>()
            .Any(obj =>
                expectedFields.All(field =>
                    obj.TryGetValue(field.Key, out var token) &&
                    token?.ToString() == field.Value
                )
            );

        matched.ShouldBeTrue(
            $"Expected at least one object in the array to contain: {string.Join(", ", expectedFields.Select(kv => $"{kv.Key} = '{kv.Value}'"))}"
        );
    }
}