# Private Helper

## 1.0 Release Notes

Initial Version - supports private properties



### Master build

[![Build Status](https://kkohler.visualstudio.com/PrivateHelper/_apis/build/status/kkohler2.PrivateHelper?branchName=master)](https://kkohler.visualstudio.com/PrivateHelper/_build/latest?definitionId=1&branchName=master)

PrivateHelper is a library intended for unit test purposes to allow access to private properties.  This allows for a clean public interface, while allowing unit tests to get to internal values via private properties.

## Getting Started

DOWNLOADS TBD
NUGET TBD

Install the package from nuget.org TBD

```ps
Install-Package PrivateHelper
```

Example Code Project

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


In your unit test project

    using UnitTestHelper;

## Usage In your unit tests

        [Fact]
        public void SetTest()
        {
            int testValue = 23;

            ITestClass instance = new TestClass();
            PrivateHelper.SetProperty(instance, "Value", testValue);
            Assert.Equal(testValue, instance.GetValue());
	}

        [Fact]
        public void Test2()
        {
            int testValue = 56;

            ITestClass instance = new TestClass();
            instance.SetValue(testValue);
            Assert.Equal(testValue, (int)PrivateHelper.GetProperty(instance, "Value"));
        }

