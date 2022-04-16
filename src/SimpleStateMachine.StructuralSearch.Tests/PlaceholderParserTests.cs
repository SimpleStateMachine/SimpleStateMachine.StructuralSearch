using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class PlaceholderParserTests
    {
        [Fact]
        public void FindTemplateShouldBeNotEmpty()
        {
            Assert.Throws<ParseException>(() => StructuralSearch.ParseFindTemplate(string.Empty));
        }
        
        [Theory]
        [InlineData("($test$)", "(value )", "value ")]
        [InlineData("($test$ )", "(value )", "value")]
        [InlineData("($test$)", "(value (test))", "value (test)")]
        [InlineData("($test$)", "(value (test) )", "value (test) ")]
        public void TemplateParsingShouldBeSuccess(string template, string source, string result)
        {
            IParsingContext parsingContext = new ParsingContext();
            var templateParser = StructuralSearch.ParseFindTemplate(template);
            var res = templateParser.Parse(ref parsingContext, source);
            var placeholder = parsingContext.GetPlaceholder("test");
            
            Assert.Equal(placeholder.Value, result);
        }
        
        [Theory]
        [InlineData("$var$;$var2$;", "test;;;test;;;",  "value ")]
        public void TemplateParsingShouldBeSuccess2(string template, string source, string result)
        {
            IParsingContext parsingContext = new ParsingContext();
            var templateParser = StructuralSearch.ParseFindTemplate(template);
            templateParser.Parse(ref parsingContext, source);
             
            
            // var templateStr = File.ReadAllText(templatePath);
            // var template = StructuralSearch.ParseTemplate(templateStr);
            //
            // Assert.NotNull(template);
        }
    }
}