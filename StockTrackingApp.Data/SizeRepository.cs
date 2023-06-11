using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data
{
    public class SizeRepository : ISizeRepository
    {
        private readonly StockTrackingContext _context;

        public SizeRepository(StockTrackingContext context)
        {
            _context = context;
        }
        public Size Add(Size size)
        {
            _context.Sizes.Add(size);
            _context.SaveChanges();

            return _context.Entry(size).Entity;
        }

        public Size Delete(int id)
        {
            var entity = _context.Sizes.Find(id);
            _context.Sizes.Remove(entity);

            _context.SaveChanges();

            return entity;
        }

        public List<Size> GetAll()
        {
            return _context.Sizes.ToList();
        }

        public Size GetById(int id)
        {
            return _context.Sizes.Find(id);
        }

        public Size GetByName(string name)
        {
            return _context.Sizes.FirstOrDefault(x => x.Name == name);
        }

        public Size Update(int id, Size size)
        {
            var entity = _context.Sizes.Find(id);
            entity.Name = size.Name;
            _context.SaveChanges();

            return entity;
        }
    }
}
