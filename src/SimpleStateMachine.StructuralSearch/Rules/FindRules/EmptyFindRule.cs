using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

internal class EmptyFindRule: IFindRule
{
    public static readonly EmptyFindRule Instance = new();
    
    public bool Execute(ref IParsingContext context) 
        => true;

    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;

    public override string ToString()
        => string.Empty;
}