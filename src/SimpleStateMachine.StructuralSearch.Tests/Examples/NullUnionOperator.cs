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
        
    public int Test2()
    {
        int? temp = 5;
            
        if(temp is null)
            return 7;
        else
            return 8;
    }
        
    public int Test3()
    {
        int? temp2 = 1;
            
        if(temp2 is null)
            return 3;
        else
            return 4; 
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