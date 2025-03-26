using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ParametersParser
{
    internal static readonly Parser<char, PlaceholderParameter> PlaceholderParameter =
        Grammar.Placeholder.Select(x => new PlaceholderParameter(x)).Try();

    // internal static readonly Parser<char, IRuleParameter> StringFormatParameter =
    //     Parser.OneOf(PlaceholderProperty, StringParameter)
    //         .AtLeastOnce()
    //         .Between(CommonParser.DoubleQuotes)
    //         .Select(IRuleParameter (parameters)=> new StringJoinParameter(parameters.ToList()));

    internal static readonly Parser<char, IRuleParameter> Parameter = 
        Parser.OneOf
        (
            PlaceholderPropertyParser.PlaceholderPropertyParameter.Try(), 
            ChangeParameterParser.ChangeStringParameter.Cast<IRuleParameter>().Try(),
            PlaceholderParameter.Cast<IRuleParameter>()
        );

    internal static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
        Parameter.Try().Trim().SeparatedAtLeastOnce(CommonParser.Comma.Trim());
}