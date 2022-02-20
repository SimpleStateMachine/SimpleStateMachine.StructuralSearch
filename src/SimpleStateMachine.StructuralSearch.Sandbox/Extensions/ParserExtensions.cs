using System;
using Pidgin;
using Pidgin.Configuration;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Extensions
{
    public static class ParserExtensions
    {
        // public static Parser<TToken, TOut> WithResult<TToken, TOut>(this Parser<TToken, TOut> parser, Func<TToken, SourcePos, TOut> transformResult)
        // {
        //     = Parser<TToken>.CurrentSourcePosDelta.Select<SourcePos>((Func<SourcePosDelta, SourcePos>) (d => new SourcePos(1, 1) + d));
        //     parser.Select()
        //     return this.Select<U>((Func<T, U>)(_ => result));
        // }
        
        // public static Parser<TToken, T> BetweenWithLookahead<TToken, T, U, V>(this Parser<TToken, T> parser, Parser<TToken, U> parser1, Parser<TToken, V> parser2)
        // {
        //     if (parser1 == null)
        //         throw new ArgumentNullException(nameof (parser1));
        //     if (parser2 == null)
        //         throw new ArgumentNullException(nameof (parser2));
        //     
        //     return Parser.Map((Func<U, T, V, T>) ((_, t, _) => t), parser1, parser, parser2);
        // }
        
        public static Parser<TToken, T> Try<TToken, T>(this Parser<TToken, T> parser)
        {
            return Parser.Try(parser);
        }
        
        
        // public static Parser<TToken, T> Between<TToken, T, U,V>(this Parser<TToken, T> parser,
        //     Parser<TToken, U> parser1,
        //     Parser<TToken, V> parser2)
        // {
        //     if (parser1 == null)
        //         throw new ArgumentNullException(nameof (parser1));
        //     if (parser2 == null)
        //         throw new ArgumentNullException(nameof (parser2));
        //     return Parser.Map<TToken, U, T, V, T>((Func<U, T, V, T>) ((u, t, v) => t), parser1, this, parser2);
        // }
        
        // public static T ParseOrThrow<T>(this Parser<char, T> parser,
        //     string input,
        //     IConfiguration<char>? configuration = null)
        // {
        //     return ParserExtensions.GetValueOrThrow<char, T>(parser.Parse<T>(input, configuration));
        // }
    }
}