using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Repositories;
using DDD.WinForm.Models.ViewModel;
using Moq;

namespace Company.TestProject1;

[TestClass]
public class WeatherSaveViewModelTest
{
    [TestMethod]
    public void 天気登録シナリオ()
    {

        var weatherMock = new Mock<IWeatherRepository>();

        var areas = new List<AreaEntity>(){
            new AreaEntity(1, "東京"),
            new AreaEntity(2, "静岡")
        };
        var areaMock = new Mock<IAreaRepository>();
        areaMock.Setup(x => x.GetData()).Returns(areas);

        var viewModelMock = new Mock<WeatherSaveViewModel>(weatherMock.Object, areaMock.Object);
        viewModelMock.Setup(x => x.GetDateTime()).Returns(
            Convert.ToDateTime("2018/01/01 12:34:56")
        );
        var viewModel = viewModelMock.Object;

        // 初期状態
        Assert.AreEqual(null, viewModel.SelectedAreaId);
        Assert.AreEqual(Convert.ToDateTime("2018/01/01 12:34:56"), viewModel.DataDateValue);
        Assert.AreEqual(1, viewModel.SelectedCondition);
        Assert.AreEqual("", viewModel.TemperatureTextText);
        Assert.AreEqual(2, viewModel.Areas.Count);
        Assert.AreEqual(4, viewModel.Conditions.Count);
        Assert.AreEqual("℃", viewModel.TemperatureUnitName);



        // 保存処理

        /*結果は変数(ex)に格納することで、InputExceptionで指定したmessageを取得できる*/
        var ex = Assert.ThrowsExactly<InputException>(() => viewModel.Save());
        Assert.AreEqual("AreaIdを選択してください。", ex.Message);

        viewModel.SelectedAreaId = 1;
        viewModel.TemperatureTextText = null;
        ex = Assert.ThrowsExactly<InputException>(() => viewModel.Save());
        Assert.AreEqual("温度を入力してください。", ex.Message);

        viewModel.TemperatureTextText = "ああ";
        ex = Assert.ThrowsExactly<InputException>(() => viewModel.Save());
        Assert.AreEqual("有効な数値を入力してください", ex.Message);

        viewModel.TemperatureTextText = "19.345";

        // var entity = new WeatherEntity(
        //     viewModel.SelectedAreaId,
        //     viewModel.DataDateValue,
        //     viewModel.SelectedCondition,
        //     viewModel.TemperatureTextText
        // )
        // {

        // };

        weatherMock.Setup(x => x.Save(It.IsAny<WeatherEntity>())).Callback<WeatherEntity>(saveValue =>
        {
            Assert.AreEqual(1, saveValue.AreaId.Value);
            Assert.AreEqual(Convert.ToDateTime("2018/01/01 12:34:56"), saveValue.DataDate);
            Assert.AreEqual(1, saveValue.Condition.Value);
            Assert.AreEqual(19.345f, saveValue.Temperature.Value);
        });

        viewModel.Save();
        weatherMock.VerifyAll();

    }
}
