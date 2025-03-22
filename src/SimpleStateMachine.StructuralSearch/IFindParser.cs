using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;

namespace SimpleStateMachine.StructuralSearch;

public interface IFindParser
{
    List<FindParserResult> Parse(IInput input);
}