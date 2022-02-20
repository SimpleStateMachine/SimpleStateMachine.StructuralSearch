using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using String = System.String;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    internal static class Program
    {
        private static Parser<char, T> Parenthesised<T>(Parser<char, T> parser)
            => parser.Between(String("("), String(")"));
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
            var placeholder = PlaceholderParser.Identifier.Between(Char('$'));
            
            //expressions parser
            Parser<char, string> expr = null;
            Parser<char, string> parenthesised = Rec(() => expr).Between(Char('('), Char(')'));
            expr = AnyCharExcept('(', ')').ManyString().Or(parenthesised);

            
            var t = "((test > 25) && (test < 50))";
            
            //template parser
            var separator = Whitespaces.AsString().Or(EndOfLine);
            var any = AnyCharExcept('$', ' ', '\n').AtLeastOnceString();    
            var token = PlaceholderParser.Identifier.Between(Char('$')).Try();
            var templateParser = token.Or(any).Separated(separator);

            //if with lookahead parser
            var test341 = String("if")
                .Then(Char('('))
                .Then(Any.Until(Lookahead(Char(')').Then(End).Try())))
                .AsString();
            
            var template =
                "if($condition$)\n" +
                "return $value1$;\n" +
                "else\n" +
                "return $value2$;";
            var test = templateParser.ParseOrThrow(template);
            
        }
    }
}