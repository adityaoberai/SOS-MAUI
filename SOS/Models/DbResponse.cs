namespace SOS.Models
{
    public class DbResponse
    {
        public bool Status { get; set; }
        public string StatusMessage { get; set; }
        public SettingsData SettingsData { get; set; }
    }
}
