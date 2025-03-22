using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

internal class EmptySubRule : IFindRule
{
    public bool Execute(ref IParsingContext context) 
        => true;
    
    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;

    public override string ToString() 
        => $"{Constant.Underscore}";
}