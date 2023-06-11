using Microsoft.EntityFrameworkCore;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StockTrackingContext _context;

        public BrandRepository(StockTrackingContext context)
        {
            _context = context;
        }

        public List<Brand> GetAll()
        {
            return _context.Brands.ToList();          
        }

        public Brand GetById(int id, bool withProducts = false)
        {
            var query = _context.Brands.AsQueryable();

            if (withProducts)
                query = query.Include(x => x.Products)
                    .ThenInclude(p => p.Category);

            return query.FirstOrDefault(x => x.Id == id);
        }

        public Brand GetByName(string name, bool withProducts = false)
        {
            var query = _context.Brands.AsQueryable();

            if (withProducts)
                query = query.Include(x => x.Products)
                    .ThenInclude(p => p.Category);

            return query.FirstOrDefault(x => x.Name == name);
        }

        public Brand Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();

            return _context.Entry(brand).Entity;
        }

        public Brand Update(int id, Brand brand)
        {
            var entity = _context.Brands.Find(id);
            entity.Name = brand.Name;
            _context.SaveChanges();

            return entity;
        }

        public Brand Delete(int id)
        {
            var entity = _context.Brands.Find(id);

            _context.Brands.Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
