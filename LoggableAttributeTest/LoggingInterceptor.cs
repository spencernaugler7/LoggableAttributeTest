using Castle.DynamicProxy;

namespace LoggableAttributeTest;

public class LoggingInterceptor : IAsyncInterceptor
{
    public void InterceptSynchronous(IInvocation invocation)
    {
        Console.WriteLine("Before Invocation");
        invocation.Proceed();
        Console.WriteLine("After Invocation");
    }

    public void InterceptAsynchronous(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
    }

    private async Task InternalInterceptAsynchronous(IInvocation invocation)
    {
        // Step 1. Do something prior to invocation.

        invocation.Proceed();
        var task = (Task)invocation.ReturnValue;
        await task;

        // Step 2. Do something after invocation.
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
    }

    private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
    {
        // Step 1. Do something prior to invocation.

        invocation.Proceed();
        var task = (Task<TResult>)invocation.ReturnValue;
        TResult result = await task;

        // Step 2. Do something after invocation.

        return result;
    }
}
