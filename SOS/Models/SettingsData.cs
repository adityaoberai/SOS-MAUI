using SQLite;

namespace SOS.Models
{
    [Table("settings")]
    public class SettingsData
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(14), Unique]
        public string PhoneNumber { get; set; }
    }
}
