using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Extensions;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class StructuralSearchTests
{
    [Theory]
    [MemberData(nameof(TestCases))]
    public static void StructuralSearchShouldBeSuccess(string inputText, string template, Dictionary<string, string> expectedResult)
    {
        var results = StructuralSearch.ParseFindTemplate(template).ParseString(inputText).ToList();
        Assert.Single(results);
        var parseResult = results.Single();

        Assert.Equal(expectedResult.Count, parseResult.Placeholders.Count);
        
        foreach (var (key, value) in expectedResult)
            Assert.Equal(parseResult.Placeholders[key].Value, value);
    }
    
    public static IEnumerable<object[]> TestCases => new List<object[]>
    {
        new object[]
        {
            // Input text
            "void MyMethodName(int value1, double value2)", 
            // Template
            "void $methodName$($params$)",
            // Result placeholders
            new Dictionary<string, string>()
            {
                { "methodName", "MyMethodName" },
                { "params", "int value1, double value2" }
            }
        }
    };
}