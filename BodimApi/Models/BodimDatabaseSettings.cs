namespace BodimApi.Models
{
    public class BodimDatabaseSettings : IBodimDatabaseSettings
    {
        public string UserCollection { get; set; }
        public string PostCollection { get; set; }
        public string BodimDataCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBodimDatabaseSettings
    {
        string UserCollection { get; set; }
        string PostCollection { get; set; }
        string BodimDataCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}