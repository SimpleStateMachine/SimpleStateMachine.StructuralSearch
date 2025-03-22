﻿namespace SimpleStateMachine.StructuralSearch.Tests.ExamplesInput;

public class NullUnionOperator
{
    public static void Test1()
    {
        int? result;
        int? temp1 = 5;
        int? temp2 = 5; 
        
        if (temp1 is null)
            result = temp2;
        else
            result =  temp1;
    }
        
    public static void Test2()
    {

        int? result;
        int? temp1 = 6;
        int? temp2 = 7;
        if (temp1 is not null)
            result = temp2;
        else
            result =  temp1;
    }
}