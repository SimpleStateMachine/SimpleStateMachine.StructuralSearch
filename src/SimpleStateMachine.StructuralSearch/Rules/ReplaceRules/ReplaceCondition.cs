using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

public class ReplaceCondition : IReplaceCondition
{
    private readonly IFindRule _findRule;

    public ReplaceCondition(IFindRule findRule)
    {
        _findRule = findRule;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;

    public bool Execute(ref IParsingContext context)
        => _findRule.Execute(ref context);

    public override string ToString()
        => $"{Constant.If}{Constant.Space}{_findRule}{Constant.Space}{Constant.Then}";
}