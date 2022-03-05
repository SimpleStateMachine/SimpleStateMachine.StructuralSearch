using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            
            var template1 =
                "if($condition$)\n" +
                "return $value1$;\n" +
                "else\n" +
                "return $value2$;";
            
            var example1 =
                "if((value) = (5))\n" +
                "return \"Result1\";\n" +
                "else\n" +
                "return \"Result2\";";

            var template2 =
                "if($var$ $sign$ null)\n" +
                "{\n" +
                "$var$ = $value$;\n" +
                "}";

            var example2 =
                "if(temp == null)\n" +
                "{\n" +
                "temp = new List<string>();\n" +
                "}";

            var template3 =
                "if($value1$ $sign$ null)\n" +
                "{\n" +
                "$var$ = $value1$;\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "$var$ = $value2$;\n" +
                "}";
            
            var testTempalte = "if($test$)";
            var testText = "if((value1)&&(value2))";
            var testTextForMatch = "fdjkfnafdjankfjnafkajndaif((value1)&&(value2))";
            var testTempalte2 = "return $value$;";
            var testText2 = "return 125;;;;";

            var parser = FindTemplateParser.ParseTemplate(template2);
            var result = parser.ParseOrThrow(example2);
        }
    }
}