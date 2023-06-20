using Xunit.Abstractions;

namespace Soda.Show.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var res = DeepProperty("Tags".Split('.'));

        _testOutputHelper.WriteLine(res);
    }

    public static string DeepProperty(string[] property, int deep = 0)
    {
        if (deep >= property.Length) return "";

        if (deep == property.Length - 1)
        {
            return $"{property[deep]}.Contains(@0)";
        }
        else
        {
            return $"{property[deep]}.Any({DeepProperty(property, ++deep)})";
        }
    }
}