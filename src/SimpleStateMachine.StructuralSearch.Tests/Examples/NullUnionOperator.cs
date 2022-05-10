namespace SimpleStateMachine.StructuralSearch.Tests.Examples;

public class NullUnionOperator
{
    public int Test1()
    {
        int? temp = 1;
            
        if(temp is null)
            return 3;
        else
            return 4;
    }
        
    public void Test2()
    {
        int? result;
        int? temp1 = 5;
        int? temp2 = 5; 
        if(temp1 is null)
            result = temp2;
        else
            result =  temp1;
    }
        
    public void Test3()
    {
        int result;
        int? temp1 = 5;
        int? temp2 = 5; 
        if(temp1 is null)
            result = temp2.Value;
        else
            result =  temp1.Value;
    }
        
    public int Test4()
    {
        int? temp3 = 5;
            
        if(temp3 is null)
            return 7;
        else if (temp3 == 8)
            return 9;
        else
            return 10;
    }
        
    public void Test5()
    {
            
    }
}