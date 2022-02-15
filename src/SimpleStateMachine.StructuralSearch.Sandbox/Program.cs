using System;
using System.Collections.Generic;
using Pidgin;
using Pidgin.Expression;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    internal static class Program
    {
        public class Test
        {
            
        }
        static void Main(string[] args)
        {
            Parser<char, double> r1 = Real;
            Parser<char, double> r2 = Real;
            Parser<char, IEnumerable<char>> r1r2 = Whitespaces.Then(Char('+')).Then(Whitespaces);
            Parser<char, double> rParser = Map((foo, _, bar) => bar + foo, r1, r1r2, r2);
            Parser<char, string> rParser2 = rParser.Select(x=>x.ToString());
            
            
            Parser<char, string> Var = Letter.Then(LetterOrDigit.ManyString(), (h, t) => h + t);
            Parser<char, string> sParser = Map((foo, _, bar) => foo + bar, Var, r1r2, Var);
            
            
            var js = String("js").ThenReturn(rParser2);
            var other = Whitespaces.ThenReturn(sParser);

            var result = js.Or(other).Then(CurrentPos);
            var ds = r1r2.Select(x=>x);
            var tds =  CurrentOffset;

            var input = "   +   ";
            // var t2 = result.ParseOrThrow("55 + 25").WithResult(x => x);
            var dsdfsdf = ds.ParseOrThrow(input);
            var t2 = r1r2.ParseOrThrow(input);
            var t = result.ParseOrThrow("js foo + bar");

            var t23 = PlaceholderParser.Placeholder().ParseOrThrow("$test$");

            // Parser<char, IEnumerable<char>> parser3 = Whitespaces.Then(Char('+')).Then(Whitespaces);
            // Parser<char, double> sequencedParser = Map((foo, _,bar) => bar + foo, parser1, parser3, parser2);
            //
            // var or = String("")
            //
            //
            // var t = sequencedParser.ParseOrThrow("5+5");
            // var g = t.Invoke();
        }
    }
}