using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Service
{
    public class ServicesCompanyDatabaseSettings : IServicesCompanyDatabaseSettings
    {
        public string TextFieldsCollectionName { get; set; }
        public string ServiceItemCollectionName { get; set; }
        public string NewsItemCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IServicesCompanyDatabaseSettings
    {
        public string TextFieldsCollectionName { get; set; }
        public string ServiceItemCollectionName { get; set; }
        public string NewsItemCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
