using System;
using Pidgin;
using Pidgin.Expression;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class ExprParser
    {
        private static Parser<char, T> Tok<T>(Parser<char, T> token)
            => Try(token).Before(SkipWhitespaces);
        private static Parser<char, string> Tok(string token)
            => Tok(String(token));

        private static Parser<char, T> Parenthesised<T>(Parser<char, T> parser)
            => parser.Between(Tok("("), Tok(")"));
        
        private static Parser<char, Func<IExpr, IExpr, IExpr>> Binary(Parser<char, BinaryOperatorType> op)
            => op.Select<Func<IExpr, IExpr, IExpr>>(type => (l, r) => new BinaryOp(type, l, r));
        private static Parser<char, Func<IExpr, IExpr>> Unary(Parser<char, UnaryOperatorType> op)
            => op.Select<Func<IExpr, IExpr>>(type => o => new UnaryOp(type, o));

        private static readonly Parser<char, Func<IExpr, IExpr, IExpr>> Add
            = Binary(Tok("+").ThenReturn(BinaryOperatorType.Add));
        private static readonly Parser<char, Func<IExpr, IExpr, IExpr>> Sub
            = Binary(Tok("-").ThenReturn(BinaryOperatorType.Sub));
        private static readonly Parser<char, Func<IExpr, IExpr, IExpr>> Div
            = Binary(Tok("/").ThenReturn(BinaryOperatorType.Div));
        private static readonly Parser<char, Func<IExpr, IExpr, IExpr>> Mul
            = Binary(Tok("*").ThenReturn(BinaryOperatorType.Mul));
        private static readonly Parser<char, Func<IExpr, IExpr>> Decrement
            = Unary(Tok("--").ThenReturn(UnaryOperatorType.Decrement));
        private static readonly Parser<char, Func<IExpr, IExpr>> Increment
            = Unary(Tok("++").ThenReturn(UnaryOperatorType.Increment));

        private static readonly Parser<char, Func<IExpr, IExpr>> Minus
            = Unary(Tok("-").ThenReturn(UnaryOperatorType.Minus));
        private static readonly Parser<char, Func<IExpr, IExpr>> Plus
            = Unary(Tok("+").ThenReturn(UnaryOperatorType.Plus));
        
        private static readonly Parser<char, IExpr> Identifier
            = Tok(Letter.Then(LetterOrDigit.ManyString(), (h, t) => h + t))
                .Select<IExpr>(name => new Identifier(name))
                .Labelled("identifier");
        
        private static readonly Parser<char, IExpr> Literal
            = Tok(LongNum)
                .Select<IExpr>(value => new Literal(value))
                .Labelled("integer literal");
        
        private static readonly Parser<char, IExpr> Expr = ExpressionParser.Build<char, IExpr>(
            expr => (
                OneOf(
                    Identifier,
                    Literal,
                    Parenthesised(expr).Labelled("parenthesised expression")
                ),
                new[]
                {
                    Operator.Prefix(Decrement)
                        .And(Operator.Prefix(Increment))
                        .And(Operator.Prefix(Minus))
                        .And(Operator.Prefix(Plus)),
                    Operator.InfixL(Mul),
                    Operator.InfixL(Div),
                    Operator.InfixL(Add),
                    Operator.InfixL(Sub)
                }
            )
        ).Labelled("expression");

        public static IExpr  ParseOrThrow(string input)
            =>  Expr.ParseOrThrow(input);

    }
}