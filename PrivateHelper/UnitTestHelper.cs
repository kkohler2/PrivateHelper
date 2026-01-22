using System;
using System.Linq;
using System.Reflection;

namespace UnitTestHelper
{
    public static class PrivateHelper
    {
        public static void SetProperty(object instance, string propertyName, object value)
        {
            Type t = instance.GetType();

            PropertyInfo propertyInfo = t.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                var properties = t.GetProperties();
                propertyInfo = properties.Where(x => x.Name == propertyName).SingleOrDefault();
            }
            if (propertyInfo.SetMethod == null)
            {
                throw new ArgumentException($"Set Property {propertyName} not found.");
            }
            else if (propertyInfo.SetMethod.IsStatic)
            {
                throw new ArgumentException($"Set Property {propertyName} cannot be static.");
            }
            propertyInfo.SetValue(instance, value);
        }

        public static object GetProperty(object instance, string propertyName)
        {
            Type t = instance.GetType();

            PropertyInfo propertyInfo = t.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                var properties = t.GetProperties();
                propertyInfo = properties.Where(x => x.Name == propertyName).SingleOrDefault();
            }
            if (propertyInfo.GetMethod == null)
            {
                throw new ArgumentException($"Get Property {propertyName} not found.");
            }
            else if (propertyInfo.SetMethod.IsStatic)
            {
                throw new ArgumentException($"Get Property {propertyName} cannot be static.");
            }
            return propertyInfo.GetValue(instance);
        }

        public static T GetProperty<T>(object instance, string propertyName)
        {
            Type returnType = typeof(T);
            Type t = instance.GetType();

            PropertyInfo propertyInfo = t.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                var properties = t.GetProperties();
                propertyInfo = properties.Where(x => x.Name == propertyName).SingleOrDefault();
            }
            if (propertyInfo.GetMethod == null)
            {
                throw new ArgumentException($"Get Property {propertyName} not found.");
            }
            else if (propertyInfo.SetMethod.IsStatic)
            {
                throw new ArgumentException($"Get Property {propertyName} cannot be static.");
            }
            var returnValue = propertyInfo.GetValue(instance);
            if (returnValue == null)
            {
                return default(T);
            }
            return (T)returnValue;
        }

        public static void CallVoidMethod(object instance, string methodName, object[] parameters)
        {
            Type t = instance.GetType();

            MethodInfo methodInfo = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Get Property {methodInfo} not found.");
            }
            else if (methodInfo.IsStatic)
            {
                throw new ArgumentException($"Get Property {methodInfo} cannot be static.");
            }
            methodInfo.Invoke(instance, parameters);
        }

        public static T CallMethod<T>(object instance, string methodName, object[] parameters)
        {
            Type returnType = typeof(T);
            Type t = instance.GetType();

            MethodInfo methodInfo = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Get Property {methodInfo} not found.");
            }
            else if (methodInfo.IsStatic)
            {
                throw new ArgumentException($"Get Property {methodInfo} cannot be static.");
            }
            var returnValue = methodInfo.Invoke(instance, parameters);
            if (returnValue == null)
            {
                return default(T);
            }
            return (T)returnValue;
        }

        public static void CallStaticVoidMethod(object instance, string methodName, object[] parameters)
        {
            Type t = instance.GetType();

            MethodInfo methodInfo = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Get Property {methodInfo} not found.");
            }
            methodInfo.Invoke(instance, parameters);
        }

        public static T CallStaticMethod<T>(object instance, string methodName, object[] parameters)
        {
            Type returnType = typeof(T);
            Type t = instance.GetType();

            MethodInfo methodInfo = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Get Property {methodInfo} not found.");
            }
            var returnValue = methodInfo.Invoke(instance, parameters);
            if (returnValue == null)
            {
                return default(T);
            }
            return (T)returnValue;
        }
    }
}