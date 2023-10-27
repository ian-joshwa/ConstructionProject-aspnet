using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DataAccessLayer.Data;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Construction.Models;

namespace Construction.DataAccessLayer.Infrastructure.Repository
{
    public class StockTypeRepository : Repository<StockType>, IStockTypeRepository
    {
        private ApplicationDbContext _context;
        public StockTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(StockType stockType)
        {
            _context.StockTypes.Update(stockType);
        }
    }
}
