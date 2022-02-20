using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    internal static class Program
    {
        public class Placeholder
        {
            public Placeholder(bool isCorrect, string value)
            {
                IsCorrect = isCorrect;
                Value = value;
            }
            
            public bool IsCorrect { get; set; }
            public string Value { get; set; }
        }
        static void Main(string[] args)
        {
            //Test
            // var abv = Common.Symbol.ManyString();
            // // AnyCharExcept(' ', '$')
            // // var any =  Parser<TToken>.Token((Func<TToken, bool>) (_ => true)).Labelled("any character")
            // var any = Common.Symbol.ManyString();
            // var placeholder = PlaceholderParser.Identifier.Between(Char('$'));
            // // var token = Char("$").Then(AnyCharExcept(' ', '$').Many())
            // // var parser = placeholder.Separated(any).Many();
            //
            // var parser = Common.Tok(placeholder).Or(Common.Tok(any));

            var any = AnyCharExcept('$').ManyString();    
            var token = Try(PlaceholderParser.Identifier.Between(Char('$')));
            var testpar = token.Or(any).Many();
            
            
        
            // var str = Any.Many().Select(x => new string(x.ToArray()));
            // var parser = placeholder.Or(str);
                
            var template =
                "if($condition$)\n" +
                "return $value1$;\n" +
                "else\n" +
                "return $value2$;";
            
         var test = testpar.Parse("abdsfdasf $ 2323");


            // // var token = Try(PlaceholderParser.Identifier.Between(Char('$').Then(Digit));
            //      // var testpar = token.Or(Any).Then.Many();
            //      
            //      var token = Try(PlaceholderParser.Identifier.Between(Char('$').Then(Digit)));
            //      Parser<char, string> expr = null;
            //      var parenthesised = Any
            //          .Then(Rec(() => expr), (c, c1) =>c + c1 );  
            //
            //      
            //      expr = Real.Select(x=>x.ToString()).Or(parenthesised);
            //      
            //      // var any = Any.Select(x=> new string(x.ToArray()));
            //      // var token = Try(PlaceholderParser.Identifier.Between(Char('$')).Then(Digit, (s, c) => s + c).Select(x=>  ));
            //      // var testpar = token.Or(any).Select().Many();
            //
            //      var test = Any.Then(CurrentPos, (c, pos) => (c, pos.Col)).Many();
            //
            //      var teest2 =  PlaceholderParser.Identifier.Test(Char('$'), Char('$'));
            //
            //      
            //      // var gh = Any.Many().Then(String("from")).ParseOrThrow("fsdkfdsfkJFLDKJFfrom");
            //      
            //      var gf = expr.Many().ParseOrThrow("$test$ 4 52");
        }
    }
}