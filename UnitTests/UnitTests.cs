using UnitTestHelper;

namespace UnitTests
{
    public interface ITestClass
    {
        int GetValue();
        void SetValue(int value);
    }

    public class TestClass : ITestClass
    {
        private static int _staticValue;
        private int _value;

        public int GetValue()
        {
            return _value;
        }

        public void SetValue(int value)
        {
            _value = value;
        }

        // For Unit Tests...
        private int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private void SetMethod(int value)
        {
            _value = value;
        }

        private int GetMethod()
        {
            return _value;
        }

        private Task SetMethodAsync(int value)
        {
            _value = value;
            return Task.CompletedTask;
        }

        private Task<int> GetMethodAsync()
        {
            return Task.FromResult(_value);
        }

        private static void StaticSetMethod(int value)
        {
            _staticValue = value;
        }
        private static int StaticGetMethodAsync()
        {
            return _staticValue;
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void SetTest()
        {
            int testValue1 = 23;
            int testValue2 = 76;

            ITestClass instance = new TestClass();
            Assert.Equal(0, instance.GetValue());
            instance.SetValue(testValue1);
            Assert.Equal(testValue1, instance.GetValue());

            PrivateHelper.SetProperty(instance, "Value", testValue2);
            Assert.Equal(testValue2, instance.GetValue());
        }

        [Fact]
        public void GetTest()
        {
            int testValue1 = 56;
            int testValue2 = -342;

            ITestClass instance = new TestClass();
            Assert.Equal(0, instance.GetValue());
            instance.SetValue(testValue1);
            Assert.Equal(testValue1, instance.GetValue());


            instance.SetValue(testValue2);
            Assert.Equal(testValue2, (int)PrivateHelper.GetProperty(instance, "Value"));
        }

        [Fact]
        public void GetTestByType()
        {
            int testValue = 12;

            ITestClass instance = new TestClass();
            Assert.Equal(0, instance.GetValue());
            instance.SetValue(testValue);
            Assert.Equal(testValue, instance.GetValue());


            instance.SetValue(testValue);
            Assert.Equal(testValue, PrivateHelper.GetProperty<int>(instance, "Value"));
        }

        [Fact]
        public void MethodTest()
        {
            int testValue1 = 1256;
            int testValue2 = 34653;

            ITestClass instance = new TestClass();
            instance.SetValue(testValue1);
            Assert.Equal(testValue1, instance.GetValue());

            List<object> parameters = new List<object> { testValue2 };
            PrivateHelper.CallVoidMethod(instance, "SetMethod", parameters.ToArray());
            Assert.Equal(testValue2, PrivateHelper.GetProperty<int>(instance, "Value"));
            Assert.Equal(testValue2, PrivateHelper.CallMethod<int>(instance, "GetMethod", null));
        }

        [Fact]
        public async Task AsyncMethodTest()
        {
            int testValue1 = 1256;
            int testValue2 = 34653;

            ITestClass instance = new TestClass();
            instance.SetValue(testValue1);
            Assert.Equal(testValue1, instance.GetValue());

            List<object> parameters = new List<object> { testValue2 };
            await PrivateHelper.CallMethod<Task>(instance, "SetMethodAsync", parameters.ToArray());
            Assert.Equal(testValue2, PrivateHelper.GetProperty<int>(instance, "Value"));
            Assert.Equal(testValue2, await PrivateHelper.CallMethod<Task<int>>(instance, "GetMethodAsync", null));
        }

        [Fact]
        public void StaticMethodTest()
        {
            int testValue1 = 23424;

            ITestClass instance = new TestClass();
            instance.SetValue(testValue1);
            Assert.Equal(testValue1, instance.GetValue());

            List<object> parameters = new List<object> { testValue1 };
            PrivateHelper.CallStaticVoidMethod(instance, "StaticSetMethod", parameters.ToArray());
            int result = PrivateHelper.CallStaticMethod<int>(instance, "StaticGetMethodAsync", null);
            Assert.Equal(testValue1, result);
        }
    }
}