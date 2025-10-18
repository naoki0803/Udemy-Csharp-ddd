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
            new WeatherEntity(2, Convert.ToDateTime("2025/09/22 15:22:33"), 2, 33.8f),
            new WeatherEntity(1, Convert.ToDateTime("2045/08/01 11:02:11"), 1, 29.81f)
        };
        WeatherMock.Setup(x => x.GetData()).Returns(entities);

        // Act
        var viewModel = new HomeListViewModel(WeatherMock.Object);

        // Assert
        Assert.AreEqual(2, viewModel.Weathers.Count);
    }
}
