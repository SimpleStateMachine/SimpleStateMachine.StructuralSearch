using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static Pidgin.Parser;
using String = System.String;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // var config = YmlHelper.Parse(
            //     @"C:\Users\roman\GitHub\SimpleStateMachine.StructuralSearch\src\SimpleStateMachine.StructuralSearch.Tests\ConfigurationFile\FullConfig.yml");
            //
            // var parsers = config.Configurations
            //     .Select(x => new StructuralSearchParser(x));
            //
            // parsers.
        }
    }
}