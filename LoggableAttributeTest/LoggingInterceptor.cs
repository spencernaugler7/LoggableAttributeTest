using Castle.DynamicProxy;

namespace LoggableAttributeTest;

public class LoggingInterceptor : IInterceptor
{

    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine("Before Invocation");
        invocation.Proceed();
        Console.WriteLine("After Invocation");
    }
}
