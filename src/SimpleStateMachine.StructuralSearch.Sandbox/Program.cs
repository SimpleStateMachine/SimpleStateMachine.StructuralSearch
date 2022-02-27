using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;
using static Pidgin.Parser;
using static SimpleStateMachine.StructuralSearch.Sandbox.Parsers;
using static Pidgin.Parser<char>;
using String = System.String;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    // var spaces = Char(' ').AtLeastOnceString();
    // var endOfLines = EndOfLine.AtLeastOnceString();
    // var whitespaces = OneOf(spaces, endOfLines);
    // var anyCharExcept = AnyCharExcept('(', ')', '[', ']', '{', '}', '$', ' ', '\n').AtLeastOnceString().Try()
    //     .WithDebug("anyCharExcept");
    // Parser<char, IEnumerable<string>> expr = null;
    //        
    //
    // var placeholder = PlaceholderParser.Identifier.Between(Char('$')).Try()
    //     .WithDebug("placeholder");
    //         
    // // var parenthesised1 = Rec(() => expr).Between(Char('('), Char(')')).WithDebug("parenthesised1");
    // var parenthesised1 = MapToMany(Stringc('('), Rec(() => expr), Stringc(')'))
    //     .WithDebug("parenthesised1");
    // // var parenthesised2 = Rec(() => expr).Between(Char('['), Char(']')).WithDebug("parenthesised1");
    // var parenthesised2 = MapToMany(Stringc('['), Rec(() => expr), Stringc(']'))
    //     .WithDebug("parenthesised2");
    // // var parenthesised3 = Rec(() => expr).Between(Char('{'), Char('}')).WithDebug("parenthesised1");
    // var parenthesised3 = MapToMany(Stringc('{'), Rec(() => expr), Stringc('}'))
    //     .WithDebug("parenthesised3");
    //
    // var parenthesised = OneOf(parenthesised1, parenthesised2, parenthesised3);
    //         
    //         
    // //don't work
    // var tokens = OneOf(anyCharExcept, placeholder, whitespaces).AtLeastOnce();
    // expr = tokens.Or(parenthesised).AtLeastOnce().MergerMany();
    internal static class Program
    {
        private static Parser<char, IEnumerable<T>> Parenthesised<T>(char left, char right,
            Func<char, Parser<char, T>> leftRight,
            Parser<char, IEnumerable<T>> expr)
            => MapToMany(leftRight(left), expr, leftRight(right));


        private static Parser<char, IEnumerable<T>> ManyParenthesised<T>(Func<char, Parser<char, T>> leftRight,
            Parser<char, IEnumerable<T>> expr, params (char, char)[] values)
        {
            return OneOf(values.Select(x =>
                MapToMany(
                    leftRight(x.Item1),
                    expr,
                    leftRight(x.Item2)))
            );
        }


        static void Main(string[] args)
        {
            var spaces = Char(Constant.Space).AtLeastOnceString();
            var endOfLines = EndOfLine.AtLeastOnceString();
            var _whitespaces = OneOf(spaces, endOfLines);
            var _anyCharExcept = AnyCharExcept(Constant.All()).AtLeastOnceString().Try();

            var _placeholder = PlaceholderParser.Identifier.Between(Char(Constant.PlaceholderSeparator)).Try();
            var _token = OneOf(_anyCharExcept, _placeholder, _whitespaces);
            var _tokens = _token.AtLeastOnce();
            Parser<char, IEnumerable<string>> _expr = null;
            _expr = _tokens.Or(ManyParenthesised(Stringc, Rec(() => _expr), Constant.AllParenthesised)).AtLeastOnce()
                .MergerMany();


            var _anyCharPlace = AnyCharExcept(Constant.AllExclude(Constant.PlaceholderSeparator)).AtLeastOnceString()
                .Try();
            var placeTokens = OneOf(_anyCharPlace, _whitespaces).AtLeastOnce();
            Parser<char, IEnumerable<string>> onPlace = null;
            onPlace = placeTokens.Or(ManyParenthesised(Stringc, Rec(() => onPlace), Constant.AllParenthesised))
                .AtLeastOnce().MergerMany();


            // var anyCharExcept = _anyCharExcept.Select(x => String(x));
            // var whitespaces = _whitespaces.Select(_ => WhitespaceString);
            // var placeholder = _placeholder.Select(_ => onPlace.JoinToString());
            // var token = OneOf(anyCharExcept, placeholder, whitespaces);
            // var tokens = token.AtLeastOnce();
            // Parser<char, IEnumerable<Parser<char, string>>> expr = null;
            // expr = OneOf(tokens, ManyParenthesised(ParserToParser.Stringc, Rec(() => expr), Constant.AllParenthesised)).AtLeastOnce().MergerMany();
            // var parser = expr.Select(x => Series(x, enumerable => enumerable.JoinToString()));


            var anyCharExcept = _anyCharExcept
                .Select(x => String(x).AsMatch());

            var whitespaces = _whitespaces
                .Select(_ => WhitespaceString.AsMatch());

            var placeholder = _placeholder
                .Select(name => new Custom.PlaceholderParser(name, onPlace.JoinToString().AsMatch()))
                .Cast<Parser<char, SourceMatch>>();

            var token = OneOf(anyCharExcept, placeholder, whitespaces);
            var tokens = token.AtLeastOnce();
            Parser<char, IEnumerable<Parser<char, SourceMatch>>> expr = null;
            var parenthesised = ManyParenthesised(ParserToParser.StringcMatch, Rec(() => expr),
                Constant.AllParenthesised);

            expr = OneOf(tokens, parenthesised)
                .AtLeastOnce().MergerMany();
            var templateParser = expr.Select(x => Series(x, enumerable => enumerable.Concatenate()));


            // var test125 = Map((u, v) => string.Join("", u) + v, onPlace, Stringc(';'));
            //
            // 
            //
            // _expr = _tokens.Or(_parenthesised).AtLeastOnce().MergerMany();
            // 
            // var t = "((test > 25) && (test < 50))";
            // var t2 = "([test > 25] && {test < 50})";


            //template parser
            // var separator = Whitespaces.AsString().Or(EndOfLine);
            // var any = AnyCharExcept('$', ' ', '\n').AtLeastOnceString();
            // var token = PlaceholderParser.Identifier.Between(Char('$')).Try();
            // var templateParser = token.Or(any).Separated(separator);

            //if with lookahead parser
            // var test341 = String("if")
            //     .Then(Char('('))
            //     .Then(Any.Until(Lookahead(Char(')').Then(End).Try())))
            //     .AsString();

            var template1 =
                "if($condition$)\n" +
                "return $value1$;\n" +
                "else\n" +
                "return $value2$;";


            var example1 =
                "if((value) = (5))\n" +
                "return \"Result1\";\n" +
                "else\n" +
                "return \"Result2\";";

            var template2 =
                "if($var$ $sign$ null)\n" +
                "{\n" +
                "$var$ = $value$;\n" +
                "}";

            var example2 =
                "if(temp == null)\n" +
                "{\n" +
                "temp = new List<string>();\n" +
                "}";

            var template3 =
                "if($value1$ $sign$ null)\n" +
                "{\n" +
                "$var$ = $value1$;\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "$var$ = $value2$;\n" +
                "}";

            // var g = test125.ParseOrThrow("\"Result1\";");

            var testTempalte = "if($test$)";
            var testText = "if((value1)&&(value2))";
            var testTextForMatch = "fdjkfnafdjankfjnafkajndaif((value1)&&(value2))";
            var testTempalte2 = "return $value$;";
            var testText2 = "return 125;;;;\n";


            // var test = _expr.ParseOrThrow(template1);
            //var testr = String("return").Then(Whitespaces).Then(Any.AtLeastOnce()).Then(String(";"));


            // var testr = String("return ")
            //     .Then(Any.ManyString())
            //     .Before(Char(';'));


            // var nextParser = Char(';');
            // Parser<char, Unit> nextNextParser = null;
            //
            // var parser = String("return ").Then(Any.AtLeastOnceUntil(
            //     Lookahead(nextParser.Then(nextNextParser))
            // ).AsString());


            // var testr = String("return ").Then(Any.ManyString().AtLeastOnceUntil(Not(Char(';')))).Then(Char(';'));
            // var testr = String("return ").Then(Any.ManyString().AtLeastOnceUntil(Not(Char(';')))).Then(Char(';'));
            // var tg = parser.ParseOrThrow(testText2);


            // var dsdsf = spaces.Map(s => s ,String("test"), spaces, CurrentSourcePosDelta, CurrentPos).ParseOrThrow("\n" +
            //     "test    ");
            // var str = "test\n\n\nt;";
            // var match = String("test").Then(AnyCharExcept(';').AtLeastOnceString().AsMatch())
            //     .ParseOrThrow(str);

            var parser = templateParser.ParseOrThrow(testTempalte);
            var skip = Any.ThenReturn(SourceMatch.Empty);
            var result = OneOf(parser.Try(), skip)
                .Many()
                .Select(x => x.Where(match => !Equals(match, SourceMatch.Empty)))
                .ParseOrThrow(testTextForMatch);
        }
    }
}