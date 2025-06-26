using Xunit.Sdk;

namespace ApiShared.Api;

public static class JsonSchemaAssertions
{
    public static void ShouldBeValid(this JsonSchemaValidator.ValidationResult result)
    {
        if (!result.IsValid)
            throw new XunitException(result.ToString());
    }
}