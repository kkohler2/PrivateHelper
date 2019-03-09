using UnitTestHelper;
using Xunit;

namespace UnitTests
{
    public interface ITestClass
    {
        int GetValue();
        void SetValue(int value);
    }

    public class TestClass : ITestClass
    {
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
    }
}
