﻿using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public interface IReplaceBuilder
    {
        IEnumerable<IRuleParameter> Steps { get; }
        string Build(ref IParsingContext context);
    }
    
    public class EmptyReplaceBuilder: IReplaceBuilder
    {
        public IEnumerable<IRuleParameter> Steps { get; } = Array.Empty<IRuleParameter>();
        
        public string Build(ref IParsingContext context) 
            => string.Empty;
    }
}