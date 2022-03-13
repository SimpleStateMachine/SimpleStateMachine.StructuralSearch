using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var result = FindRuleParser.ParseTemplate("$var$ Equals \"125\"");
            
            var t = ExprParser.ParseOrThrow("( 2 + 2 ) * 2");
            var resw = t.Invoke();
            var test = String("return ")
                .Then(AnyCharExcept(';').ManyString())
                .Then(Char(';').AtLeastOnceString())
                .Before(Char(';'));
            
            
            
            
            // var template = StructuralSearch.ParseTemplate("");
            // var test = Parser.OneOf(String("Test"), CommonParser.Empty);
            var res = test.ParseOrThrow("return 125;;;;");
            
            
            // var lookahead = Parser.Lookahead(Char(';').Then(End).Try());
            // var t = Parser.String("return ").Then(Any.AtLeastOnceAsStringUntilNot(lookahead));
            // var res = t.ParseOrThrow("return 124;;");
            // var testTempalte = "if($test$)";
            // var testText = "if((value1)&&(value2))";
            // var testTextForMatch = "fdjkfnafdjankfjnafkajndaif((value1)&&(value2))";
            // var testTempalte2 = "return $value$;";
            // var testText2 = "return 125;;;;";
            //
            // var parser = StructuralSearch.ParseTemplate(template3);
            // var result = parser.ParseOrThrow(example3);
        }
    }
}