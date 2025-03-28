using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch;

public interface IFindParser
{
    List<FindParserResult> Parse(IInput input, params ILogicalOperation[] findRules);
}