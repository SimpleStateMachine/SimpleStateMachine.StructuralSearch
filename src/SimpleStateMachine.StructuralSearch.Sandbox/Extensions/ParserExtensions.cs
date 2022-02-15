using System;
using Pidgin;

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
    }
}