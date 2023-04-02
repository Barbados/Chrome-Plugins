using System;
namespace capturerservice.Data
{
    public interface ISettingsConfig
    {
        string MongoDBConnectionString { get; set; }
        string CosmosDBConnectionString { get; set; }
        string DbName { get; set; }
        string WordsCollectionName { get; set; }
    }
}
