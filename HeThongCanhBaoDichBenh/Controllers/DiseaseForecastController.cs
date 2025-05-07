using HeThongCanhBaoDichBenh.Models;
using HeThongCanhBaoDichBenh.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HeThongCanhBaoDichBenh.Controllers
{
    public class DiseaseForecastController : Controller
    {
        private readonly WeatherService _weatherService;
        private readonly DiseaseForecastService _forecastService;

        public DiseaseForecastController(WeatherService weatherService, DiseaseForecastService forecastService)
        {
            _weatherService = weatherService;
            _forecastService = forecastService;
        }

        public IActionResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Forecast(string city)
        {
            var json = await _weatherService.GetWeatherForecastAsync(city);
            if (json == null)
            {
                ViewBag.Error = "Không lấy được dữ liệu từ API.";
                return View("Index");
            }

            // Dictionary mô tả tiếng Việt
            var descriptionTranslations = new Dictionary<string, string>
    {
        { "Clear Sky", "Trời quang" },
        { "Few Clouds", "Một vài đám mây" },
        { "Scattered Clouds", "Mây rải rác" },
        { "Broken Clouds", "Mây rải rác" },
        { "Shower Rain", "Mưa rào" },
        { "Rain", "Mưa" },
        { "Overcast Clouds", "Mây che phủ" },
        { "Thunderstorm", "Giông bão" },
        { "Snow", "Tuyết" },
        { "Mist", "Sương mù" },
        { "Drizzle", "Mưa phùn" },
        { "Light Rain", "Mưa nhẹ" },
        { "Moderate Rain", "Mưa vừa" },
        { "Heavy Intensity Rain", "Mưa to" },
        { "Very Heavy Rain", "Mưa rất to" },
        { "Extreme Rain", "Mưa cực kỳ to" },
        { "Freezing Rain", "Mưa tuyết" },
        { "Light Intensity Shower Rain", "Mưa rào nhẹ" },
        { "Heavy Intensity Shower Rain", "Mưa rào nặng" },
        { "Ragged Shower Rain", "Mưa rào không đều" },
        { "Thunderstorm With Light Rain", "Giông bão kèm mưa nhẹ" },
        { "Thunderstorm With Rain", "Giông bão kèm mưa" },
        { "Thunderstorm With Heavy Rain", "Giông bão kèm mưa to" },
        { "Light Thunderstorm", "Giông bão nhẹ" },
        { "Heavy Thunderstorm", "Giông bão mạnh" },
        { "Ragged Thunderstorm", "Giông bão không đều" },
        { "Thunderstorm With Drizzle", "Giông bão kèm mưa phùn" },
        { "Thunderstorm With Light Drizzle", "Giông bão kèm mưa phùn nhẹ" },
        { "Light Snow", "Tuyết nhẹ" },
        { "Snow Fall", "Tuyết rơi" },
        { "Heavy Snow", "Tuyết rơi nặng" },
        { "Sleet", "Mưa tuyết" },
        { "Shower Sleet", "Mưa tuyết rào" },
        { "Light Shower Sleet", "Mưa tuyết rào nhẹ" },
        { "Heavy Shower Sleet", "Mưa tuyết rào nặng" },
        { "Smoke", "Khói" },
        { "Haze", "Sương nù nhẹ" },
        { "Dust", "Bụi" },
        { "Fog", "Sương nù dày đặc" },
        { "Sand", "Cát" },
        { "Ash", "Tro" },
        { "Squalls", "Gió lốc" },
        { "Tornado", "Lốc xoáy" }
    };

            string CapitalizeWords(string input)
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
            }

            // Tạo danh sách thời tiết
            var list = json["list"]?
                .Take(10)
                .Select(item =>
                {
                    var rawDesc = item["weather"]?[0]?["description"]?.ToString() ?? "";
                    var capitalDesc = CapitalizeWords(rawDesc);
                    var translatedDesc = descriptionTranslations.ContainsKey(capitalDesc)
                        ? descriptionTranslations[capitalDesc]
                        : capitalDesc;

                    return new WeatherInfo
                    {
                        DateTime = DateTime.Parse(item["dt_txt"]!.ToString()),
                        Temperature = item["main"]?["temp"]?.Value<double>() ?? 0,
                        Humidity = item["main"]?["humidity"]?.Value<int>() ?? 0,
                        Description = translatedDesc,
                        WindSpeed = item["wind"]?["speed"]?.Value<double>() ?? 0,
                        Pressure = item["main"]?["pressure"]?.Value<int>() ?? 0,
                        Icon = item["weather"]?[0]?["icon"]?.ToString()
                    };
                })
                .ToList();

            ViewBag.City = city;
            var result = await _forecastService.GetDiseaseRiskAsync(city);
            ViewBag.Result = result;
            return View("Index", list);
        }

    }
}
