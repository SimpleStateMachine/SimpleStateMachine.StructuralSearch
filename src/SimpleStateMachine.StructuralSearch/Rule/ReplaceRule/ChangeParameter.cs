using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ChangeParameter : IRuleParameter
    {
        public IRuleParameter Parameter { get; }
        public ChangeType Type { get; }

        public ChangeParameter(IRuleParameter parameter, ChangeType type)
        {
            Parameter = parameter;
            Type = type;
        }

        public string GetValue()
        {
            throw new System.NotImplementedException();
        }
    }
}