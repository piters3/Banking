using System.Threading;
using Xunit;

//[assembly: CollectionBehavior(MaxParallelThreads = 4)]

namespace Banking.XUnitTests
{
    public class XUnitParalellTests1
    {
        [Fact]
        public void Test1()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }

        [Fact]
        public void Test3()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }

        [Fact]
        public void Test4()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }
    }

    public class XUnitParalellTests2
    {
        [Fact]
        public void Test5()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }

        [Fact]
        public void Test6()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }

        [Fact]
        public void Test7()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }

        [Fact]
        public void Test8()
        {
            Thread.Sleep(1000);
            Assert.True(true);
        }
    }
}
