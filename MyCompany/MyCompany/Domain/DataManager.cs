using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain
{
    public class DataManager
    {
        public IGenericRepository<TextField> TextFields { get; set; }
        public IGenericRepository<ServiceItem> ServiceItems { get; set; }
        public IGenericRepository<NewsItem> NewsItems { get; set; }

        public DataManager(IGenericRepository<TextField> textFieldsRepository,
            IGenericRepository<ServiceItem> serviceItemsRepository,
            IGenericRepository<NewsItem> newsItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
            NewsItems = newsItemsRepository;
        }
    }
}
