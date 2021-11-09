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
    public class MongoServiceItemsRepository : IGenericRepository<ServiceItem>
    {
        private readonly IMongoCollection<ServiceItem> _serviceItem;

        public MongoServiceItemsRepository(IServicesCompanyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _serviceItem = database.GetCollection<ServiceItem>(settings.ServiceItemCollectionName);
        }

        public void Delete(string id)
        {
            _serviceItem.DeleteOne(serviceItem => serviceItem.Id == id);
        }

        public ServiceItem Get(string id)
        {
            return _serviceItem.Find(serviceItem => serviceItem.Id == id).FirstOrDefault();
        }

        public List<ServiceItem> GetAll()
        {
            return _serviceItem.Find(service => true).ToList();
        }

        public void Update(string id, ServiceItem entity)
        {
            _serviceItem.ReplaceOne(serviceItem => serviceItem.Id == id, entity);
        }
        public void Create(ServiceItem entity)
        {
            _serviceItem.InsertOne(entity);
        }
    }
}
