using NUnit.Framework;
using System.Threading;

//[assembly: Parallelizable(ParallelScope.All)]

namespace Banking.NUnitTests
{
    [TestFixture]
    public class NUnitParalellTests
    {
        [Test]
        public void Test1()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test2()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test3()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test4()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test5()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test6()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test7()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test8()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }
    }
}
