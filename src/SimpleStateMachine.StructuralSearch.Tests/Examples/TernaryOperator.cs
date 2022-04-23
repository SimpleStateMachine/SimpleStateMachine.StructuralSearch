using System;

namespace SimpleStateMachine.StructuralSearch.Tests.Examples
{
    public class TernaryOperator
    {
        public int Test1()
        {
            var temp = 1;
            
            if(temp == 2)
                return 3;
            else
                return 4;
        }
        
        public int Test2()
        {
            var temp = 5;
            
            if(temp == 6)
                return 7;
            else
                return 8;
        }
        
        public int Test3()
        {
            var temp2 = 1;
            
            if(temp2 == 2)
                return 3;
            else
                return 4; 
        }
        
        public int Test4()
        {
            var temp3 = 5;
            
            if(temp3 == 6)
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
}