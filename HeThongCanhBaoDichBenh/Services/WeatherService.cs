﻿using HeThongCanhBaoDichBenh.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "7f11429d8d13a446bc679f2fd07b35f3";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JObject> GetWeatherForecastAsync(string cityName)
    {
        var url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName},VN&units=metric&appid={_apiKey}";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JObject.Parse(content);
    }
}