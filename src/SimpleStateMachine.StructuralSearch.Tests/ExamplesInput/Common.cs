namespace SimpleStateMachine.StructuralSearch.Tests.Examples;

public class Common
{
    public int Method1()
    {
        var temp = 5;
            
        if (temp == 6)
            return 7;
        else
            return 8;
    }

    public static void Method2()
    {
        int? result;
        int? temp1 = 5;
        int? temp2 = 5; 
        if (temp1 is null)
            result = temp2;
        else
            result =  temp1;
    }
        
    public static void Method3()
    {
        int result;
        int? temp1 = 5;
        int? temp2 = 5; 
        if (temp1 is null)
            result = temp2.Value;
        else
            result =  temp1.Value;
    }
}