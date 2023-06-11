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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StockTrackingContext _context;

        public CategoryRepository(StockTrackingContext context)
        {
            _context = context;
        }
        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id, bool withProducts = false)
        {
            var query = _context.Categories.AsQueryable();

            if (withProducts)
                query = query.Include(x => x.Products)
                    .ThenInclude(p => p.Brand);

            return query.FirstOrDefault(x => x.Id == id);
        }

        public Category GetByName(string name, bool withProducts = false)
        {
            var query = _context.Categories.AsQueryable();

            if (withProducts)
                query = query.Include(x => x.Products)
                    .ThenInclude(p => p.Brand);

            return query.FirstOrDefault(x => x.Name == name);
        }
        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return _context.Entry(category).Entity;
        }
        public Category Update(int id, Category category)
        {
            var entity = _context.Categories.Find(id);
            entity.Name = category.Name;
            _context.SaveChanges();

            return entity;
        }
        public Category Delete(int id)
        {
            var entity = _context.Categories.Find(id);
            _context.Categories.Remove(entity);
            _context.SaveChanges();

            return entity;
        }   
    }
}
