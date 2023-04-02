using System;
using System.Collections.Generic;
using capturerservice.Data;
using capturerservice.Helpers;
using capturerservice.Model;
using MongoDB.Driver;

namespace capturerservice.Services
{
    public class WordsService : IWordsService
    {
        private readonly string _baseUrl = @"https://www.oxfordlearnersdictionaries.com/definition/english/";
        private readonly IMongoCollection<WordElement> _wordsCollection;

        public WordsService(ISettingsConfig config)
        {
            var client = new MongoClient(config.CosmosDBConnectionString);
            var db = client.GetDatabase(config.DbName);
            _wordsCollection = db.GetCollection<WordElement>(config.WordsCollectionName);
        }

        public WordElement Get(string word)
        {
            var content = HttpHelper.GetContent(_baseUrl + word);
            var parser = new HtmlParser(content);

            return new WordElement {
                Word = parser.GetWord(),
                Definitions = parser.GetDefinitions(),
                AddedDate = DateTime.Now
            };
        }

        public WordElement Create(string name)
        {
            if (Exists(name))
                return null;

            var element = Get(name);
            _wordsCollection.InsertOne(element);

            return element;
        }

        public List<WordElement> Get()
        {
            var result = _wordsCollection.Find(r => true).ToList();
            return result;
        }

        private bool Exists(string name)
        {
            return _wordsCollection.Find(w => w.Word == name).CountDocuments() > 0;
        }
    }
}
