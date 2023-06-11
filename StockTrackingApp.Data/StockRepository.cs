using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StockTrackingApp.Data
{
    public class StockRepository : IStockRepository
    {
        private readonly StockTrackingContext _context;
        private readonly IMapper _mapper;

        public StockRepository(StockTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Stock Add(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return _context.Entry(stock).Entity;
        }

        public Stock Delete(int id)
        {
            var entity = _context.Stocks.Find(id);
            _context.Stocks.Remove(entity);

            _context.SaveChanges();

            return entity;
        }

        //page = 0 and pageSize = -1 means all
        public List<Stock> GetAll(int page = 0, int pageSize = -1)
        {
            var query = _context.Stocks.AsQueryable();

            query = query.Include(x => x.Color);
            query = query.Include(x => x.Size);
            query = query.Include(x => x.Product);
            query = query.Include(x => x.Product.Category);
            query = query.Include(x => x.Product.Brand);


            if (page == 0 && pageSize == -1)
            {
                return query.ToList();
            }

            return query.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public Stock GetById(int id)
        {
            var query = _context.Stocks.AsQueryable();

            query = query.Include(x => x.Color);
            query = query.Include(x => x.Size);
            query = query.Include(x => x.Product);
            query = query.Include(x => x.Product.Category);
            query = query.Include(x => x.Product.Brand);

            return query.FirstOrDefault(x => x.Id == id);
        }

        public List<Stock> GetAllByProductId(int productId)
        {
            var query = _context.Stocks.AsQueryable();

            query = query.Include(x => x.Color);
            query = query.Include(x => x.Size);
            query = query.Include(x => x.Product);
            query = query.Include(x => x.Product.Category);
            query = query.Include(x => x.Product.Brand);

            return query.Where(x => x.ProductId == productId).ToList();
        }

        public List<Stock> Search(string productName, int? sizeId, int? colorId, bool? getOutOfStocks)
        {
            var query = _context.Stocks.AsQueryable();

            query = query.Include(x => x.Color);
            query = query.Include(x => x.Size);
            query = query.Include(x => x.Product);
            query = query.Include(x => x.Product.Category);
            query = query.Include(x => x.Product.Brand);

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(x => x.Product.Name.Contains(productName));
            }

            if (sizeId.HasValue)
            {
                query = query.Where(x => x.SizeId == sizeId.Value);
            }

            if (colorId.HasValue)
            {
                query = query.Where(x => x.ColorId == colorId.Value);
            }

            if (getOutOfStocks.HasValue)
            {
                query = query.Where(x => x.Quantity >= 0);
            }

            return query.ToList();
        }

        public Stock Update(int id, Stock stock)
        {
            var updateRequestedStock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            _mapper.Map(stock, updateRequestedStock);

            _context.SaveChanges();

            return updateRequestedStock;
        }

        public Stock UpdateQuantity(int id, int quantityChange)
        {
            var updateRequestedStock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            updateRequestedStock.Quantity += quantityChange;

            _context.SaveChanges();

            return updateRequestedStock;
        }
    }
}
