using System.Collections.Generic;
using System.IO;
using SimpleStateMachine.StructuralSearch.Configurations;

namespace SimpleStateMachine.StructuralSearch.Tests.Mock;

public static class ConfigurationMock
{
    public static Configuration GetConfigurationFromFiles(string name)
    {
        var fileName = $"{name}.txt";
        var findTemplate = FileOrNull("FindTemplate", fileName);
        var fileRule = FileOrNull("FindRule", fileName) ;
        var replaceTemplate = FileOrNull("ReplaceTemplate", fileName);
        var replaceRule = FileOrNull("ReplaceRule", fileName);
        var fileRules = fileRule is null ? null : new List<string>(fileRule.Split(Constant.LineFeed.ToString()));
        var replaceRules = replaceRule is null ? null : new List<string>(replaceRule.Split(Constant.LineFeed.ToString()));
        var config = new Configuration
        {
            FindTemplate = findTemplate,
            FindRules = fileRules,
            ReplaceTemplate = replaceTemplate,
            ReplaceRules = replaceRules
        };
            
        return config;
            
        string? FileOrNull(string folder, string name)
        {
            var path = Path.Combine(folder, name);
            if (!File.Exists(path))
                return null;

            var file = File.ReadAllText(path);
            return file.Replace("\r\n", "\n");
        }
    }
}