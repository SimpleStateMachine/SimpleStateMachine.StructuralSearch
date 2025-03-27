using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

internal class ReplaceRule : IReplaceRule
{
    public static readonly EmptyReplaceRule Empty = new ();
        
    public IEnumerable<ReplaceSubRule> Rules { get; }
    public IFindRule ConditionRule { get; }

    public ReplaceRule(IFindRule conditionRule, IEnumerable<ReplaceSubRule> rules)
    {
        ConditionRule = conditionRule;
        Rules = rules;
    }
        
    public bool IsMatch(ref IParsingContext context) 
        => ConditionRule.Execute(ref context);

    public override string ToString()
    {
        var builder = new StringBuilder();
        var conditionStr = ConditionRule.ToString();

        if (!string.IsNullOrEmpty(conditionStr))
            builder.Append(conditionStr).Append(Constant.Space);

        builder.AppendJoin(Constant.Comma, Rules);
        return builder.ToString();
    }
}