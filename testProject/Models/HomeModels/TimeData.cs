namespace testProject.Models.HomeModels
{
    public class TimeData
    {
        public string City {  get; set; }
        public Dictionary<string, string> Time { get; set; }
        public TimeData()
        {
            Time = new Dictionary<string, string>();
        }
    }
}
