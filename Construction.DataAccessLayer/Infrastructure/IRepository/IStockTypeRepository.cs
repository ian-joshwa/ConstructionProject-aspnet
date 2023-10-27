using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Models;

namespace Construction.DataAccessLayer.Infrastructure.IRepository
{
    public interface IStockTypeRepository : IRepository<StockType>
    {


        void Update(StockType stockType);
    }
}
