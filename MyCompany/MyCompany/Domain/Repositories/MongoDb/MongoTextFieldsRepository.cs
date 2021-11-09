using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Service;

namespace MyCompany.Domain.Repositories.MongoDb
{
    public class MongoTextFieldsRepository : IGenericRepository<TextField>
    {
        readonly IMongoCollection<TextField> _textField;

        public MongoTextFieldsRepository(IServicesCompanyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _textField = database.GetCollection<TextField>(settings.TextFieldsCollectionName);
        }

        public void Create(TextField entity)
        {
            _textField.InsertOne(entity);
        }

        public void Delete(string id)
        {
            _textField.DeleteOne(textField => textField.Id == id);
        }

        public TextField Get(string codeWord)
        {
            return _textField.Find(textField => textField.CodeWord == codeWord).FirstOrDefault();
        }

        public List<TextField> GetAll()
        {
            return _textField.Find(textField => true).ToList();
        }

        public void Update(string id, TextField entity)
        {
            _textField.ReplaceOne(textField => textField.Id == id, entity);
        }
    }
}
