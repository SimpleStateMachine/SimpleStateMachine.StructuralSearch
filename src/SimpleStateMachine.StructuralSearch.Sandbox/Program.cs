using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Sandbox;

internal static class Program
{
    static void Main(string[] args)
    {
        //StructuralSearch.ParseFindTemplate()


        var path = "Test.txt";
        var oldText = "0123456789";
        var text = "test";
        File.WriteAllText(path, oldText);

        // using var stringReader = text.AsStream();
        // using var streamWriter = File.OpenWrite(path);
        //     
        // stringReader.CopyPartTo(streamWriter, 0, 7);
            
        // var config = YmlHelper.Parse(
        //     @"C:\Users\roman\GitHub\SimpleStateMachine.StructuralSearch\src\SimpleStateMachine.StructuralSearch.Tests\ConfigurationFile\FullConfig.yml");
        //
        // var parsers = config.Configurations
        //     .Select(x => new StructuralSearchParser(x));
        //
        // parsers.
    }
}