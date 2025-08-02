using System.ComponentModel;

namespace LoggableAttributeTest;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class LoggableAttribute : Attribute
{
    
}