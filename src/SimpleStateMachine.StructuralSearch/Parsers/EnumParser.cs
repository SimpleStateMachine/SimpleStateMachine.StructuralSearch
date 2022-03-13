using System;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch
{
    public class EnumParser<TEnum> : Parser<char, TEnum>
        where TEnum : struct, Enum
    {
        private Parser<char, TEnum> _parser;

        public EnumParser(bool ignoreCase, params TEnum [] excluded)
        {
            _parser = Parser.OneOf(EnumHelper.GetNamesExcept(excluded)
                .Select(value => Parsers.String(value, ignoreCase))
                .Select(Parser.Try))
                .AsEnum<TEnum>(ignoreCase);
        }

        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds,
            out TEnum result)
        {
            return _parser.TryParse(ref state, ref expecteds, out result);
        }
    }
}