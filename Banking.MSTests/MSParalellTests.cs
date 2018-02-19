using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

//[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Banking.MSTests
{
    [TestClass]
    public class MSParalellTests
    {
        [TestMethod]
        public void Test1()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test2()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test3()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test4()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test5()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test6()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test7()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test8()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }
    }
}
