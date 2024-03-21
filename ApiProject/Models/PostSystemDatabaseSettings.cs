namespace ApiProject.Models
{
    public class PostSystemDatabaseSettings : IPostSystemDatabaseSettings
    {
        public string PostCollectionName { get; set; } =String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
