using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class LabelRepository : IRepository<CustomLabel>
    {
        private const string GetAllLabels = "SELECT * from dbo.Labels";
        private const string GetAllLabelsById = "SELECT * from dbo.Labels WHERE Id = @Id";

        private readonly IGenericDbContext _dbContext;

        public LabelRepository(IGenericDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CustomLabel> GetAll()
        {
            var output = _dbContext.LoadData<CustomLabel, dynamic>(GetAllLabels, new { });
            return output;
        }

        public CustomLabel GetById(int id)
        {
            CustomLabel label = _dbContext.LoadData<CustomLabel, dynamic>(GetAllLabelsById, new {Id = id}).FirstOrDefault();
            return label;
        }

        public bool Update(int id, CustomLabel entity)
        {
            throw new NotImplementedException();
        }

        public CustomLabel Create(CustomLabel entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
