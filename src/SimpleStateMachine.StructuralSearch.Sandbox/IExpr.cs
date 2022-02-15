using System;
using System.Collections.Immutable;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public interface IExpr
    {
        public double Invoke();
    }

    public class Identifier : IExpr
    {
        public string Name { get; }

        public Identifier(string name)
        {
            Name = name;
        }

        public bool Equals(IExpr? other)
            => other is Identifier i && this.Name == i.Name;

        public double Invoke()
        {
            return 0;
        }
    }

    public class Literal : IExpr
    {
        public double Value { get; }

        public Literal(double value)
        {
            Value = value;
        }

        public bool Equals(IExpr? other)
            => other is Literal l && this.Value == l.Value;

        public double Invoke()
        {
            return Value;
        }
    }

    public class Call : IExpr
    {
        public IExpr Expr { get; }
        public ImmutableArray<IExpr> Arguments { get; }

        public Call(IExpr expr, ImmutableArray<IExpr> arguments)
        {
            Expr = expr;
            Arguments = arguments;
        }

        public bool Equals(IExpr? other)
            => other is Call c
            && ((Object)this.Expr).Equals(c.Expr)
            && this.Arguments.SequenceEqual(c.Arguments);

        public double Invoke()
        {
            throw new NotImplementedException();
        }
    }

    public enum UnaryOperatorType
    {
        Increment,
        Decrement,
        Plus,
        Minus
    }
    public class UnaryOp : IExpr
    {
        public UnaryOperatorType Type { get; }
        public IExpr Expr { get; }

        public UnaryOp(UnaryOperatorType type, IExpr expr)
        {
            Type = type;
            Expr = expr;
        }

        public bool Equals(IExpr? other)
            => other is UnaryOp u
            && this.Type == u.Type
            && ((Object)this.Expr).Equals(u.Expr);

        public double Invoke()
        {
            return Type switch
            {
                UnaryOperatorType.Increment => Expr.Invoke() + 1,
                UnaryOperatorType.Decrement => Expr.Invoke() - 1,
                UnaryOperatorType.Plus => - Expr.Invoke(),
                UnaryOperatorType.Minus => + Expr.Invoke(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum BinaryOperatorType
    {
        Add, // +
        Sub, // -
        Mul, // *
        Div, // /
    }
    public class BinaryOp : IExpr
    {
        public BinaryOperatorType Type { get; }
        public IExpr Left { get; }
        public IExpr Right { get; }

        public BinaryOp(BinaryOperatorType type, IExpr left, IExpr right)
        {
            Type = type;
            Left = left;
            Right = right;
        }

        public bool Equals(IExpr? other)
            => other is BinaryOp b
            && this.Type == b.Type
            && ((Object)this.Left).Equals(b.Left)
            && ((Object)this.Right).Equals(b.Right);

        public double Invoke()
        {
            return Type switch
            {
                BinaryOperatorType.Add => Left.Invoke() + Right.Invoke(),
                BinaryOperatorType.Mul => Left.Invoke() * Right.Invoke(),
                BinaryOperatorType.Div => Left.Invoke() / Right.Invoke(),
                BinaryOperatorType.Sub => Left.Invoke() - Right.Invoke(),
            };
        }
    }
}