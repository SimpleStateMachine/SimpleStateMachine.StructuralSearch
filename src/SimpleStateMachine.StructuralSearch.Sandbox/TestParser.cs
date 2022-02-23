using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public class TestParser<TToken, T>:Parser<TToken, T>
    {
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out T result)
        {
            throw new System.NotImplementedException();
        }
    }
}