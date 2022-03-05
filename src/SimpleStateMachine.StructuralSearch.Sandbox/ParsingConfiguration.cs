using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Sandbox.Custom;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class ParsingConfiguration
    {
        public static Parser<char, string> Comment = EmptyParser.AlwaysNotCorrectString;
        // public static Action<Parser<>> OnDebug;
    }
}