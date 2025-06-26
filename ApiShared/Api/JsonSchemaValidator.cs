using System.Reflection;
using System.Text.Json;
using Json.Schema;

namespace ApiShared.Api;

public abstract class JsonSchemaValidator
{
    public static ValidationResult Validate(string schemaName, string responseJson)
    {
        try
        {
            var schemaContent = LoadSchema(schemaName);
            var schema = JsonSchema.FromText(schemaContent);
            var json = JsonDocument.Parse(responseJson).RootElement;

            var evaluation = schema.Evaluate(json, new EvaluationOptions
            {
                OutputFormat = OutputFormat.Hierarchical
            });

            if (evaluation.IsValid)
                return new ValidationResult { IsValid = true };

            var errors = CollectErrors(evaluation);
            return new ValidationResult
            {
                IsValid = false,
                Errors = errors
            };
        }
        catch (Exception ex)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = [$"Validation error: {ex.Message}"]
            };
        }
    }

    private static string LoadSchema(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(r => r.EndsWith($"{fileName}.json"));

        if (resourceName is null)
            throw new FileNotFoundException($"Resource '{fileName}.json' not found in assembly '{assembly.FullName}'");

        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private static IEnumerable<EvaluationResults> Traverse(EvaluationResults root)
    {
        var stack = new Stack<EvaluationResults>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            yield return current;

            foreach (var child in current.Details)
                stack.Push(child);
        }
    }

    private static List<string> CollectErrors(EvaluationResults root)
    {
        return Traverse(root)
            .Where(r => r is { IsValid: false, HasErrors: true })
            .SelectMany(r => r.Errors!.Select(e =>
                $"- {r.InstanceLocation}: {e.Value}"))
            .ToList();
    }

    public class ValidationResult
    {
        public bool IsValid { get; init; }
        public List<string> Errors { get; init; } = new();
        private int ErrorCount => Errors.Count;

        public override string ToString()
        {
            return IsValid
                ? "Schema valid."
                : $"schema validation failed: {ErrorCount} violation(s) found.\n" +
                  string.Join("\n", Errors);
        }
    }
}