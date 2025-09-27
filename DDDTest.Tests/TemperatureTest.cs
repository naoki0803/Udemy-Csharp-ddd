using DDD.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.TestProject1;

[TestClass]
public class TemperatureTest
{
    [TestMethod]
    public void 小数点以下2桁でまるめて表示できる()
    {
        var t = new Temperature(12.3f);
        Assert.AreEqual(12.3f, t.Value);
        Assert.AreEqual("12.30 ℃", t.DisplayValue);
    }
    [TestMethod]
    public void 温度Equals()
    {
        // 参照形は値が同じでもfalseになる
        var t1 = new Temperature(12.3f);
        var t2 = new Temperature(12.3f);
        Assert.AreEqual(true, t1.Equals(t2));

        float t3 = 12.3f;
        float t4 = 12.3f;
        Assert.AreEqual(true, t3.Equals(t4));
    }
    [TestMethod]
    public void 温度EqualsEquals()
    {
        // 参照形は値が同じでもfalseになる
        var t1 = new Temperature(12.3f);
        var t2 = new Temperature(12.3f);
        Assert.AreEqual(true, t1 == t2);

        float t3 = 12.3f;
        float t4 = 12.3f;
        Assert.AreEqual(true, t3 == t4);
    }
}
