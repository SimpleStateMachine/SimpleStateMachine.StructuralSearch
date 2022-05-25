﻿using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SimpleStateMachine.StructuralSearch.Tests.Examples;

public class Common2
{
    public int Method1()
    {
        var temp = 5;

        return temp == 6 ? 7 : 8;
    }

    public void Method2()
    {
        int? result;
        int? temp1 = 5;
        int? temp2 = 5; 
        result = temp1 ?? temp2;
    }
        
    public void Method3()
    {
        int result;
        int? temp1 = 5;
        int? temp2 = 5; 
        result = temp1 is null ? temp2.Value : temp1.Value;
    }
}