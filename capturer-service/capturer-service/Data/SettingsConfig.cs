using System;
namespace capturerservice.Data
{
    public class SettingsConfig : ISettingsConfig
    {
        public string MongoDBConnectionString { get; set; }
        public string CosmosDBConnectionString { get; set; }
        public string DbName { get; set; }
        public string WordsCollectionName { get; set; }
    }
}
