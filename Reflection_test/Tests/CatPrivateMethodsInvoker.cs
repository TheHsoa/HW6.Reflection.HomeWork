using System;
using System.Reflection;

namespace Reflection_test.Tests
{
    public static class CatPrivateMethodsInvoker
    {
        public static bool InvokeLikeToEatMethod(this Cat cat, IFood food)
        {
            return (bool)typeof(Cat).InvokeMember("LikeToEat", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, cat, new object[] { food });
        }
    }
}
