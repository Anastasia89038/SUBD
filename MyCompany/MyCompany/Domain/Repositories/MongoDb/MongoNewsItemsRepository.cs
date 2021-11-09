using MongoDB.Driver;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Domain.Repositories.MongoDb
{
    public class MongoNewsItemsRepository : IGenericRepository<NewsItem>
    {
        private readonly IMongoCollection<NewsItem> _newsItems;

        public MongoNewsItemsRepository(IServicesCompanyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _newsItems = database.GetCollection<NewsItem>(settings.NewsItemCollectionName);
        }

        public void Create(NewsItem entity)
        {
            _newsItems.InsertOne(entity);
        }

        public void Delete(string id)
        {
            _newsItems.DeleteOne(newsItem => newsItem.Id == id);
        }

        public NewsItem Get(string id)
        {
            return _newsItems.Find(newsItem => newsItem.Id == id).FirstOrDefault();
        }

        public List<NewsItem> GetAll()
        {
            return _newsItems.Find(newsItem => true).ToList();
        }

        public void Update(string id, NewsItem entity)
        {
            _newsItems.ReplaceOne(newsItem => newsItem.Id == id, entity);
        }
    }
}
