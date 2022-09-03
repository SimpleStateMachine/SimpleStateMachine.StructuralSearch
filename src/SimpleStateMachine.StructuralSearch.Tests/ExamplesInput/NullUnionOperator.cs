namespace SimpleStateMachine.StructuralSearch.Tests.ExamplesInput;

public class NullUnionOperator
{
    public void Test1()
    {
        int? result;
        int? temp1 = 5;
        int? temp2 = 5; 
        
        if(temp1 is null)
            result = temp2;
        else
            result =  temp1;
    }
        
    public void Test2()
    {

        int? result;
        int? temp1 = 6;
        int? temp2 = 7;
        if(temp1 is not null)
            result = temp2;
        else
            result =  temp1;
    }
    
    public int Test3()
    {
        int? temp3 = 5;
            
        if(temp3 is null)
            return 7;
        else if (temp3 == 8)
            return 9;
        else
            return 10;     
    }
}