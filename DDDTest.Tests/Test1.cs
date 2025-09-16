using DDD.WinForm.common;

namespace DDDTest.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        var val = Class1.Add(1, 2);
        Assert.AreEqual(3, val);
    }
}
