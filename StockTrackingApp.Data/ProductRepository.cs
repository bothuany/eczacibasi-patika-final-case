using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTrackingApp.Data
{

    public class ProductRepository : IProductRepository
    {
        private readonly StockTrackingContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(StockTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Product GetById(int id, bool withBrand = true, bool withCategory = true, bool withStocks = false)
        {
            var query = _context.Products.AsQueryable();

            if (withCategory)
                query = query.Include(x => x.Category);

            if (withCategory)
                query = query.Include(x => x.Category);

            if (withStocks)
            {
                query = query.Include(x => x.Stocks);
                query = query.Include(x => x.Stocks).ThenInclude(x => x.Size);
                query = query.Include(x => x.Stocks).ThenInclude(x => x.Color);
            }

            return query.FirstOrDefault(x => x.Id == id);
        }

        //page = 0 and pageSize = -1 means all
        public List<Product> Search(string? name, int? categoryId, int? brandId, double? minPrice, int? sizeId, int? colorId, bool withStocks = false, int page = 0, int pageSize = -1)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId);

            if (brandId.HasValue)
                query = query.Where(x => x.BrandId == brandId);

            if (minPrice.HasValue)
                query = query.Where(x => x.Price > minPrice);

            if (sizeId.HasValue)
                query = query.Where(x => x.Stocks.Any(x => x.SizeId == sizeId));

            if (colorId.HasValue)
                query = query.Where(x => x.Stocks.Any(x => x.ColorId == colorId));

            if (withStocks)
            {
                query = query.Include(x => x.Stocks);
                query = query.Include(x => x.Stocks).ThenInclude(x => x.Size);
                query = query.Include(x => x.Stocks).ThenInclude(x => x.Color);
            }
                
                

            query = query.Include(x => x.Brand);
            query = query.Include(x => x.Category);

            if (page == 0 && pageSize == -1)
            {
                return query.ToList();
            }

            return query.Skip(page * pageSize).Take(pageSize).ToList();
        }
        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); 

            return _context.Entry(product).Entity;
        }

        public Product Update(int id, Product product)
        {
            var updateRequestedProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (updateRequestedProduct != null)
            {
                _mapper.Map(product, updateRequestedProduct);
                _context.SaveChanges();
            }

            return updateRequestedProduct;
        }

        public Product Delete(int id)
        {
            var deleteRequestedProduct = _context.Products.Find(id);
            _context.Products.Remove(deleteRequestedProduct);

            _context.SaveChanges();

            return deleteRequestedProduct;
        }
    }
}
