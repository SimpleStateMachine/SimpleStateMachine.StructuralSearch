using System.Threading;
using System.Threading.Tasks;

namespace SimpleStateMachine.StructuralSearch.Tests.ExamplesInput;

public class Test
{
    public int Method1(int value1, int value2)
    {
        return value1 + value2;
    }
    
    public string Method2(int value1, string value2)
    {
        // some comments
        return value2 + value1;
    }
    
    public void Method3()
    {

    }
    
    public Task Method4Async(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    public int Test2(int Test2)
    {
        // some comments
        return Test2;
    }
}