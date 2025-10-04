using DDD.Domain.TestLib;

namespace Company.TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual(3, Class1.Add(1, 2));
    }

    [TestMethod]
    [ExpectedException(typeof(InputException))]
    public void ExpectedExceptionを用いた例外テスト()
    {
        Assert.AreEqual(3, Class1.Add(-1, 2));
    }

    [TestMethod]
    public void ThrowsExceptionを用いた例外テスト()
    {
        Assert.AreEqual(3, Class1.Add(1, 2));
        var ex = Assert.ThrowsException<InputException>(() => Class1.Add(-1, 2));
        Assert.AreEqual("マイナスの値は入力できません", ex.Message);
    }

    [TestMethod]
    public void AssertThrowsExactlyを用いた例外テスト()
    {
        Assert.AreEqual(3, Class1.Add(1, 2));
        var ex = Assert.ThrowsExactly<InputException>(() => Class1.Add(-1, 2));
        Assert.AreEqual("マイナスの値は入力できません", ex.Message);
    }
}
