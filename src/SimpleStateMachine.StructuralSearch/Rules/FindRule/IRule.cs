﻿namespace SimpleStateMachine.StructuralSearch.Rules
{
    public interface IRule : IContextDependent
    {
        bool Execute();
    }
}