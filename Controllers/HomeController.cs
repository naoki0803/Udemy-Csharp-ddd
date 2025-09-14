using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DDD.WinForm.Models;
using System.Data;
using Microsoft.Data.Sqlite;

namespace DDD.WinForm.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string ConnectionString = @"Data Source=/Users/shiratorinaoki/DataBase/sqlite/Udemy-DDD-Part1.db";
    public string RoundString(float value, int decimalPoint)
    {
        var temp = Convert.ToSingle(Math.Round(value, decimalPoint));
        return temp.ToString("F" + decimalPoint);
    }

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(int areaId = 1)
    {
        string sql_ = @"SELECT * FROM Weather WHERE AreaId = @AreaId ORDER BY DataDate DESC LIMIT 1";

        DataTable dt = new DataTable();
        try
        {
            using (var connection = new SqliteConnection(ConnectionString))
            using (var command = new SqliteCommand(sql_, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@AreaId", areaId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ViewData["datadate"] = reader["datadate"]?.ToString() ?? "データなし";
                        ViewData["condition"] = reader["Condition"]?.ToString() ?? "データなし";
                        ViewData["temperature"] = RoundString(Convert.ToSingle(reader["Temperature"]?.ToString()), 2) + "℃" ?? "データなし";
                    }
                    else
                    {
                        ViewData["datadate"] = "データなし";
                        ViewData["condition"] = "データなし";
                        ViewData["temperature"] = "データなし";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "データベース接続エラー");
            ViewData["condition"] = "接続エラー";
            ViewData["condition"] = "接続エラー";
            ViewData["temperature"] = "接続エラー";
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
