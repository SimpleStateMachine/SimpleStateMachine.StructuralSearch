using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            
            var pr = Parser.String("var").Trim();
            var str = pr.ParseOrThrow("var");
            str = pr.ParseOrThrow(" var");
            str = pr.ParseOrThrow("var ");
            str = pr.ParseOrThrow(" var ");
            var source = "test;;;test;;;.";
            var parser = Parser.OneOf(Parser<char>.Any.ThenReturn(Unit.Value), Parser<char>.End);
            
            
            var t = Parser<char>.Any.AtLeastOnceAsStringUntil(Lookahead(String(";").Then(Not(String(";"))).Try())).ParseOrThrow(source);


            var path = "Test.txt";
            var oldText = "0123456789";
            var text = "test";
            File.WriteAllText(path, oldText);

            using var stringReader = text.AsStream();
            using var streamWriter = File.OpenWrite(path);
            
            stringReader.CopyPartTo(streamWriter, 0, 7);
            
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