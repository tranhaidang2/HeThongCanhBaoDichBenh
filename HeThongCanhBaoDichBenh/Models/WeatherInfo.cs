namespace HeThongCanhBaoDichBenh.Models
{
    public class WeatherInfo
    {
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public string? Description { get; set; }
        public double WindSpeed { get; set; } 
        public int Pressure { get; set; }           // Áp suất khí quyển
        public string? Icon { get; set; }           // Icon thời tiết
    }
}
