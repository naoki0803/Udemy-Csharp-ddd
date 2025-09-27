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
}
