namespace Twitter.Web.Infrastructure
{
    using System;

    using Ninject;

    using Twitter.Web.App_Start;

    public class ObjectFactory
    {
        private static readonly IKernel kernel = NinjectWebCommon.Kernel;

        public static T GetInstance<T>()
        {
            return kernel.Get<T>();
        }

        public static object GetInstance(Type type)
        {
            return kernel.Get(type);
        }
    }
}