using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Sandbox;

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