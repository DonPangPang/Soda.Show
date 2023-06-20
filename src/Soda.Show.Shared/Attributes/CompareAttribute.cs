namespace Soda.Show.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CompareFuncAttribute : Attribute
{
    public CompareFuncAttribute(Operation comparer, string? propertyName = null)
    {
        Comparer = comparer;
        PropertyName = propertyName;
    }

    public Operation Comparer { get; }
    public string? PropertyName { get; }
}

public enum Operation
{
    Equal,
    Contains,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
}