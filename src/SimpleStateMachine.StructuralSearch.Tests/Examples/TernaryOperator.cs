using System;

namespace SimpleStateMachine.StructuralSearch.Tests.Examples
{
    public class TernaryOperator
    {
        public int Test1()
        {
            var temp = 150;
            
            if(temp == 125)
                return 12;
            else
                return 15;
        }
        
        public int Test2()
        {
            var temp = 150;
            
            if(temp == 125)
                return 12;
            else
                return 15;
        }
        
        public int Test3()
        {
            var temp2 = 150;
            
            if(temp2 == 125)
                return 12;
            else
                return 15; 
        }
        
        public int Test4()
        {
            var temp3 = 150;
            
            if(temp3 == 125)
                return 12;
            else if (temp3 == 135)
                return 15;
            else
                return 0;
        }
        
        public void Test5()
        {
            
        }
    }
}