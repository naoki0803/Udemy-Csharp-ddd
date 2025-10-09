using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.WinForm.Models;
using Moq;

namespace DDDTest.Tests;

[TestClass]
public class HomeViewModelTest
{
    // 【学習メモ】最初のテスト実装 - Moqを導入する前の手動モック実装
    // public void シナリオ()
    // {
    //     var viewModel = new HomeViewModel(new WeatherMock());
    //     {
    //         Assert.AreEqual("", viewModel.AreaId);
    //         Assert.AreEqual("", viewModel.DataDate);
    //         Assert.AreEqual("", viewModel.Condition);
    //         Assert.AreEqual("", viewModel.Temperature);

    //         viewModel.AreaId = "2";
    //         viewModel.Search(viewModel.AreaId);

    //         Assert.AreEqual("2", viewModel.AreaId);
    //         Assert.AreEqual("2025/09/22 15:22:33", viewModel.DataDate);
    //         Assert.AreEqual("曇り", viewModel.Condition);
    //         Assert.AreEqual("33.80 ℃", viewModel.Temperature);
    //     }
    // }

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
        weatherMock.Setup(x => x.GetLatest(2)).Returns(new WeatherEntity(2, Convert.ToDateTime("2025/09/22 15:22:33"), 2, 33.8f));
        weatherMock.Setup(x => x.GetLatest(1)).Returns(new WeatherEntity(1, Convert.ToDateTime("2045/08/01 11:02:11"), 1, 29.81f));
        weatherMock.Setup(x => x.GetLatest(3)).Returns(new WeatherEntity(3, Convert.ToDateTime("2019/01/01 9:02:01"), 3, 16.81f));
        weatherMock.Setup(x => x.GetLatest(4)).Returns(new WeatherEntity(5, Convert.ToDateTime("2019/01/01 9:02:01"), 3, 16.81f));
        var areas = new List<AreaEntity>(){
            new AreaEntity(1, "東京"),
            new AreaEntity(2, "静岡"),
            new AreaEntity(3, "神戸")
        };
        var areaMock = new Mock<IAreaRepository>();
        areaMock.Setup(x => x.GetData()).Returns(areas);

        var viewModel = new HomeViewModel(weatherMock.Object, areaMock.Object);
        Assert.AreEqual("", viewModel.SelectedAreaIdText);
        Assert.AreEqual("", viewModel.DataDateText);
        Assert.AreEqual("", viewModel.ConditionText);
        Assert.AreEqual("", viewModel.TemperatureText);
        Assert.AreEqual(3, viewModel.Areas.Count);

        viewModel.SelectedAreaIdText = "2";
        viewModel.Search(viewModel.SelectedAreaIdText.ToString());

        Assert.AreEqual("2", viewModel.SelectedAreaIdText);
        Assert.AreEqual("2025/09/22 15:22:33", viewModel.DataDateText);
        Assert.AreEqual("曇り", viewModel.ConditionText);
        Assert.AreEqual("33.80 ℃", viewModel.TemperatureText);

        viewModel.SelectedAreaIdText = "1";
        viewModel.Search(viewModel.SelectedAreaIdText.ToString());

        Assert.AreEqual("1", viewModel.SelectedAreaIdText);
        Assert.AreEqual("2045/08/01 11:02:11", viewModel.DataDateText);
        Assert.AreEqual("晴れ", viewModel.ConditionText);
        Assert.AreEqual("29.81 ℃", viewModel.TemperatureText);

        viewModel.SelectedAreaIdText = "3";
        viewModel.Search(viewModel.SelectedAreaIdText.ToString());

        Assert.AreEqual("3", viewModel.SelectedAreaIdText);
        Assert.AreEqual("2019/01/01 9:02:01", viewModel.DataDateText);
        Assert.AreEqual("雨", viewModel.ConditionText);
        Assert.AreEqual("16.81 ℃", viewModel.TemperatureText);
    }
}