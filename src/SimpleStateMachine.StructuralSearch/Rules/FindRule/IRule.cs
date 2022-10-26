﻿namespace SimpleStateMachine.StructuralSearch.Rules
{
    public interface IRule
    {
        bool Execute(ref IParsingContext context);
    }
}