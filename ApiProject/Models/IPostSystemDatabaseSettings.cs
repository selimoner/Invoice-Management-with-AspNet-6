namespace ApiProject.Models
{
    public interface IPostSystemDatabaseSettings
    {
        string PostCollectionName { get; set; }
        string ConnectionString {  get; set; }
        string DatabaseName { get; set; }
    }
}
