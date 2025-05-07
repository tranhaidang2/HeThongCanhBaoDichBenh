using Newtonsoft.Json.Linq;

namespace HeThongCanhBaoDichBenh.Services
{
    public class DiseaseForecastService
    {
        private readonly WeatherService _weatherService;

        public DiseaseForecastService(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<string> GetDiseaseRiskAsync(string cityName)
        {
            var forecastData = await _weatherService.GetWeatherForecastAsync(cityName);
            if (forecastData == null) return "Không thể lấy dữ liệu thời tiết.";

            var list = forecastData["list"];
            if (list == null)
                return "Không thể lấy dữ liệu dự báo thời tiết.";
            int totalScore = 0;

            double? previousTemp = null;
            double? previousHumidity = null;

            foreach (var item in list)
            {
                double temp = item["main"]?["temp"]?.Value<double>() ?? 0;
                int humidity = item["main"]?["humidity"]?.Value<int>() ?? 0;
                double wind = item["wind"]?["speed"]?.Value<double>() ?? 0;
                int pressure = item["main"]?["pressure"]?.Value<int>() ?? 0;


                int score = 0;

                // Nhiệt độ lý tưởng 28–31°C. Ngoài ngưỡng là nguy hiểm.
                if (temp < 26 || temp > 33) score += 3;
                else if (temp < 28 || temp > 31) score += 1;

                // Độ ẩm >85% thường đi kèm mưa lớn → nguy hiểm
                if (humidity > 90) score += 2;
                else if (humidity > 85) score += 1;

                // Gió >6m/s ảnh hưởng đến nền đáy ao
                if (wind > 6) score += 2;
                else if (wind > 4) score += 1;

                // Áp suất thấp → giảm oxy
                if (pressure < 1005) score += 2;
                else if (pressure < 1010) score += 1;

                // Thay đổi nhiệt độ và độ ẩm đột ngột
                if (previousTemp != null && Math.Abs(temp - previousTemp.Value) >= 4)
                    score += 2;

                if (previousHumidity != null && Math.Abs(humidity - previousHumidity.Value) >= 15)
                    score += 1;

                previousTemp = temp;
                previousHumidity = humidity;

                totalScore += score;
            }

            double avgRisk = totalScore / (double)list.Count();

            if (avgRisk >= 6)
                return "🛑 RỦI RO RẤT CAO: Điều kiện thời tiết bất lợi nghiêm trọng. Cần kiểm tra tôm và xử lý ao ngay.";
            else if (avgRisk >= 3)
                return "⚠ RỦI RO TRUNG BÌNH: Môi trường biến động, cần tăng cường theo dõi các chỉ số môi trường.";
            else
                return "✔ AN TOÀN: Điều kiện thời tiết ổn định, nguy cơ dịch bệnh thấp.";
        }
    }
}
