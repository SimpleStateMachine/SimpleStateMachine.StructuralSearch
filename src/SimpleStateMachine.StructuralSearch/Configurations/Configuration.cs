using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class Configuration
    {
        public string FindTemplate { get; set; }

        public List<string> FindRules { get; set; }

        public string ReplaceTemplate { get; set; }

        public List<string> ReplaceRules { get; set; }
    }
}