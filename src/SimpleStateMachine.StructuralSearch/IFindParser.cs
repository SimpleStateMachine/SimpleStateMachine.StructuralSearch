using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch;

public interface IFindParser
{
    List<FindParserResult> Parse(IInput input, params IFindRule[] findRules);
}