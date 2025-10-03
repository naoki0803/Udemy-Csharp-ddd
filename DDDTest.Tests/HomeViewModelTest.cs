using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.WinForm.Models;
using Moq;

namespace DDDTest.Tests;

[TestClass]
public class HomeViewModelTest
{
    [TestMethod]
    public void シナリオ()
    {
        var viewModel = new HomeViewModel(new WeatherMock());
        {
            Assert.AreEqual("", viewModel.AreaId);
            Assert.AreEqual("", viewModel.DataDate);
            Assert.AreEqual("", viewModel.Condition);
            Assert.AreEqual("", viewModel.Temperature);

            viewModel.AreaId = "2";
            viewModel.Search(viewModel.AreaId);

            Assert.AreEqual("2", viewModel.AreaId);
            Assert.AreEqual("2025/09/22 15:22:33", viewModel.DataDate);
            Assert.AreEqual("曇り", viewModel.Condition);
            Assert.AreEqual("33.80 ℃", viewModel.Temperature);
        }
    }

    internal class WeatherMock : IWeatherRepository
    {

        public WeatherEntity GetLatest(int areaId)
        {
            return new WeatherEntity(2, Convert.ToDateTime("2025/09/22 15:22:33"), 2, 33.8f);

            // newRow["AreaId"] = 2;
            // newRow["DataDate"] = Convert.ToDateTime("2025/09/22 15:22:33");
            // newRow["Condition"] = 5;
            // newRow["Temperature"] = 33.8f;

            // dt.Rows.Add(newRow);
            // return dt;
            // throw new NotImplementedException();
        }
    }

    [TestMethod]
    public void Moqを用いたテスト()
    {
        var weatherMock = new Mock<IWeatherRepository>();
        var viewModel = new HomeViewModel(weatherMock.Object);
        weatherMock.Setup(x => x.GetLatest(2)).Returns(new WeatherEntity(2, Convert.ToDateTime("2025/09/22 15:22:33"), 2, 33.8f));

        Assert.AreEqual("", viewModel.AreaId);
        Assert.AreEqual("", viewModel.DataDate);
        Assert.AreEqual("", viewModel.Condition);
        Assert.AreEqual("", viewModel.Temperature);

        viewModel.AreaId = "2";
        viewModel.Search(viewModel.AreaId);

        Assert.AreEqual("2", viewModel.AreaId);
        Assert.AreEqual("2025/09/22 15:22:33", viewModel.DataDate);
        Assert.AreEqual("曇り", viewModel.Condition);
        Assert.AreEqual("33.80 ℃", viewModel.Temperature);
    }
}