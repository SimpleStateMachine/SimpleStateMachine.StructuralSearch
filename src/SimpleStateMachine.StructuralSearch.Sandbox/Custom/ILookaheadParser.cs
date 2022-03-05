using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Custom
{
    public interface ILookaheadParser<TToken, T>
    {
        public bool TryParse<Res1, Res2>(Parser<TToken, Res1> next, Parser<TToken, Res2> nextNext,
            ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected, out T result);
    }
}