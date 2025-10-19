using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.WinForm.Models.ViewModel;
using Moq;

namespace Company.TestProject1;

[TestClass]
public class HomeListViewModelTest
{
    [TestMethod]
    public void 天気一覧画面シナリオ()
    {
        // Arrange
        var WeatherMock = new Mock<IWeatherRepository>();
        var entities = new List<WeatherEntity>()
        {
            new WeatherEntity(1, "東京", Convert.ToDateTime("2025/09/22 15:22:33"), 2, 33.8f),
            new WeatherEntity(2, "神戸", Convert.ToDateTime("2045/08/01 11:02:11"), 1, 29.81f)
        };
        WeatherMock.Setup(x => x.GetData()).Returns(entities);

        // Act
        var viewModel = new HomeListViewModel(WeatherMock.Object);

        // Assert
        Assert.AreEqual(2, viewModel.Weathers.Count);
        Assert.AreEqual("0001", viewModel.Weathers[0].AreaId);
        Assert.AreEqual("東京", viewModel.Weathers[0].AreaName);
        Assert.AreEqual("2025/09/22 15:22:33", viewModel.Weathers[0].DataDate);
        Assert.AreEqual("曇り", viewModel.Weathers[0].Condition);
        Assert.AreEqual("33.80 ℃", viewModel.Weathers[0].Temperature);
    }
}
