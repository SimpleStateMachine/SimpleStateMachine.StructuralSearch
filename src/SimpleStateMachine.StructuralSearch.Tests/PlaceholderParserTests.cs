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
            var input = Input.String(source);
            IParsingContext parsingContext = new ParsingContext(input);
            var templateParser = StructuralSearch.ParseFindTemplate(template);
            var res = templateParser.Parse(ref parsingContext, input);
            var placeholder = parsingContext.GetPlaceholder("test");
            
            Assert.Equal(placeholder.Value, result);
        }
        
        [Theory]
        [InlineData("$var$;$var2$;", "test;;;test;;;",  "value ")]
        public void TemplateParsingShouldBeSuccess2(string template, string source, string result)
        {
            var input = Input.String(source);
            IParsingContext parsingContext = new ParsingContext(input);
            var templateParser = StructuralSearch.ParseFindTemplate(template);
            templateParser.Parse(ref parsingContext, input);
             
            
            // var templateStr = File.ReadAllText(templatePath);
            // var template = StructuralSearch.ParseTemplate(templateStr);
            //
            // Assert.NotNull(template);
        }
    }
}