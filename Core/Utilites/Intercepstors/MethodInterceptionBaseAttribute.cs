using Castle.DynamicProxy;
using System;

namespace Core.Utilites.Intercepstors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }//Öncelik

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
