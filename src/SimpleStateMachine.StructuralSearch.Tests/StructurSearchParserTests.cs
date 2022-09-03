using System;
using System.IO;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class StructuralSearchParserTests
    {
        [Theory]
        // [InlineData("AssignmentNullUnionOperator")]
        [InlineData("NullUnionOperator", "ExamplesInput/NullUnionOperator.cs", 2)]
        [InlineData("TernaryOperator", "ExamplesInput/TernaryOperator.cs", 3)]
        public static void StructuralSearchShouldBeSuccess(string exampleName, string exampleFilePath, int matchesCount)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var parser = new StructuralSearchParser(config);

            var fileInfo = new FileInfo(exampleFilePath);
            var input = Input.File(fileInfo);
            IParsingContext context = new ParsingContext(input);
           
            var matches = parser.Parse(ref context);
            Assert.Equal(matches.Count(), matchesCount);
        }
        
        [Theory]
        // [InlineData("AssignmentNullUnionOperator")]
        [InlineData("NullUnionOperator", 2)]
        [InlineData("TernaryOperator", 3)]
        public static void StructuralSearchShouldBe(string exampleName, int matchesCount)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var inputFilePath = Path.Combine($"ExamplesInput/{exampleName}.cs");
            var resultFilePath = Path.Combine($"ExamplesOutput/{exampleName}.txt");
            var outputFilePath = Path.Combine($"{exampleName}.cs");
            
            var parser = new StructuralSearchParser(config);

            var inputFileInfo = new FileInfo(inputFilePath);
            var input = Input.File(inputFileInfo);
            IParsingContext context = new ParsingContext(input);
            var matches = parser.Parse(ref context);
            matches = parser.ApplyFindRule(ref context, matches);
            matches = parser.ApplyReplaceRule(ref context, matches);
            var replaceMatches = parser.GetReplaceMatches(ref context, matches);
            var outputFileInfo = new FileInfo(outputFilePath);
            var output = Output.File(outputFileInfo);
            output.Replace(input, replaceMatches);
            startTime.Stop();
            var resultTime = startTime.Elapsed;
            
            // Assert.Equal(matches.Count(), matchesCount);
            //
            // var resultStr = File.ReadAllText(resultFilePath);
            // var outputStr = File.ReadAllText(outputFilePath);
            // Assert.Equal(resultStr, outputStr);
        }

        [Theory]
        [InlineData("ConfigurationFile/if.yml")]
        public static void StructuralSearchFromConfig(string configPath)
        {
            var config = YmlHelper.Parse(configPath).Configurations.First();
            var inputFileInfo = new FileInfo("ExamplesInput/Test.txt");

            int count = 100;
            long result = 0;
            for (int i = 0; i < count; i++)
            {
                var res = Test(config, inputFileInfo);
                Console.WriteLine(res);
                result += res;
            }
            var t = result/count;
            Console.WriteLine($"SR: {t}");
        }
        
        private static long  Test(Configuration config, FileInfo inputFileInfo)
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var input = Input.File(inputFileInfo);
            var parser = new StructuralSearchParser(config);

            IParsingContext context = new ParsingContext(input);
            var matches = parser.Parse(ref context);
            matches = parser.ApplyFindRule(ref context, matches);
            matches = parser.ApplyReplaceRule(ref context, matches);
            var replaceMatches = parser.GetReplaceMatches(ref context, matches);
            startTime.Stop();
            var resultTime = startTime.Elapsed;
            return resultTime.Ticks;
        }
        
        // private static long  Test(Configuration config, FileInfo inputFileInfo)
        // {
        //     var startTime = System.Diagnostics.Stopwatch.StartNew();
        //     var input = Input.File(inputFileInfo);
        //     var parser = new StructuralSearchParser(config);
        //
        //     IParsingContext context = new ParsingContext(input);
        //     var matches = parser.Parse(ref context);
        //     matches = parser.ApplyFindRule(ref context, matches);
        //     matches = parser.ApplyReplaceRule(ref context, matches);
        //     var replaceMatches = parser.GetReplaceMatches(ref context, matches);
        //     startTime.Stop();
        //     var resultTime = startTime.Elapsed;
        //     return resultTime.Ticks;
        // }
    }
}