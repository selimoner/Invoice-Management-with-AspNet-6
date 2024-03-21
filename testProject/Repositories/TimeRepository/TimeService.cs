using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using testProject.Models.HomeModels;

namespace testProject.Repositories.TimeRepository
{
    public class TimeService : ITimeService
    {
        private readonly string apiKey;

        public TimeService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<TimeData> GetTime(string city)
        {
            var timeData = new Dictionary<string, string>();

            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"https://api.api-ninjas.com/v1/worldtime?city={city}";
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                try
                { 
                    var response = await httpClient.GetStringAsync(apiUrl);

                    if (!string.IsNullOrEmpty(response))
                    {
                        using (JsonDocument doc = JsonDocument.Parse(response))
                        {
                            if (doc.RootElement.TryGetProperty("year", out JsonElement yearElement))
                            {
                                string dateTime = yearElement.ToString();
                                timeData["year"] = dateTime;
                            }
                            if (doc.RootElement.TryGetProperty("month", out JsonElement monthElement))
                            {
                                string dateTime = monthElement.ToString();
                                timeData["month"] = dateTime;
                            }
                            if (doc.RootElement.TryGetProperty("day", out JsonElement dayElement))
                            {
                                string dateTime = dayElement.ToString();
                                timeData["day"] = dateTime;
                            }
                            if (doc.RootElement.TryGetProperty("hour", out JsonElement hourElement))
                            {
                                string dateTime = hourElement.ToString();
                                timeData["hour"] = dateTime;
                            }
                            if (doc.RootElement.TryGetProperty("minute", out JsonElement minuteElement))
                            {
                                string dateTime = minuteElement.ToString();
                                timeData["minute"] = dateTime;
                            }
                            if (doc.RootElement.TryGetProperty("day_of_week", out JsonElement dayOfWeekElement))
                            {
                                string dateTime = dayOfWeekElement.ToString();
                                timeData["day_of_week"] = dateTime;
                            }
                        }
                    }
                    var timeDataModel = new TimeData()
                    {
                        City = city,
                        Time= timeData
                    };
                    return timeDataModel;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                return null;
            }
        }
    }
}
