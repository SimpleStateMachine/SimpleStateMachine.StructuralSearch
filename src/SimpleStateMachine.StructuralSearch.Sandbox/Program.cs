using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;
using static Pidgin.Parser;
using static SimpleStateMachine.StructuralSearch.Sandbox.Parsers;
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
            var whitespaces = Char(' ').AtLeastOnceString();
            var endOfLines = EndOfLine.AtLeastOnceString();
            var anyCharExcept = AnyCharExcept('(', ')', '[', ']', '{', '}', '$', ' ', '\n').AtLeastOnceString().Try()
                .WithDebug("anyCharExcept");
            Parser<char, IEnumerable<string>> expr = null;
           

            var placeholder = PlaceholderParser.Identifier.Between(Char('$')).Try()
                .WithDebug("placeholder");
            
            // var parenthesised1 = Rec(() => expr).Between(Char('('), Char(')')).WithDebug("parenthesised1");
            var parenthesised1 = MapToMany(Stringc('('), Rec(() => expr), Stringc(')'))
                .WithDebug("parenthesised1");
            // var parenthesised2 = Rec(() => expr).Between(Char('['), Char(']')).WithDebug("parenthesised1");
            var parenthesised2 = MapToMany(Stringc('['), Rec(() => expr), Stringc(']'))
                .WithDebug("parenthesised2");
            // var parenthesised3 = Rec(() => expr).Between(Char('{'), Char('}')).WithDebug("parenthesised1");
            var parenthesised3 = MapToMany(Stringc('{'), Rec(() => expr), Stringc('}'))
                .WithDebug("parenthesised3");

            var parenthesised = OneOf(parenthesised1, parenthesised2, parenthesised3).ToIEnumerable();
            
            
            //don't work
            var parser = OneOf(anyCharExcept, placeholder, whitespaces, endOfLines).AtLeastOnce();
            expr = parser.Or(parenthesised).AtLeastOnce().MergerMany();
            
            //work
            // var parser = OneOf(anyCharExcept, placeholder).AtLeastOnce();
            // expr = parser.Or(parenthesised).Separated(Whitespaces).MergerMany();
            
            
            var t = "((test > 25) && (test < 50))";
            var t2 = "([test > 25] && {test < 50})";
            
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
                "if(($condition$) = ($test$))\n" +
                "return $value1$;\n" +
                "else\n" +
                "return $value2$;";

            var template2 = "((test)=(test2))";
            var template3 = "$test1$ test test34";
            var test = expr.ParseOrThrow(template);
        }
    }
}