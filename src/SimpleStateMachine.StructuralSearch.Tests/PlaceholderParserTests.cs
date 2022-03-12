using System;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class PlaceholderParserTests
    {
        [Fact]
        public void FindTemplateShouldBeNotEmpty()
        {
            Assert.Throws<ParseException>(() => StructuralSearch.ParseTemplate(string.Empty));
        }
        
        
        [Theory]
        [InlineData("($test$)", "(value )", "value ")]
        [InlineData("($test$ )", "(value )", "value")]
        [InlineData("($test$)", "(value (test))", "value (test)")]
        public void TemplateParsingShouldBeSuccess(string template, string source, string result)
        {
            var templateParser = StructuralSearch.ParseTemplate(template);
            var res = templateParser.ParseOrThrow(source);
            
            // var templateStr = File.ReadAllText(templatePath);
            // var template = StructuralSearch.ParseTemplate(templateStr);
            //
            // Assert.NotNull(template);
        }
        
        [Theory]
        [InlineData("$var$;$var2$;", "test;;;test;;;",  "value ")]
        public void TemplateParsingShouldBeSuccess2(string template, string source, string result)
        {
            var templateParser = StructuralSearch.ParseTemplate(template);
            var res = templateParser.ParseOrThrow(source);
            
            // var templateStr = File.ReadAllText(templatePath);
            // var template = StructuralSearch.ParseTemplate(templateStr);
            //
            // Assert.NotNull(template);
        }
    }
}