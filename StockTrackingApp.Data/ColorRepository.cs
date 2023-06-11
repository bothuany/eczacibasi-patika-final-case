using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data
{
    public class ColorRepository : IColorRepository
    {
        private readonly StockTrackingContext _context;

        public ColorRepository(StockTrackingContext context)
        {
            _context = context;
        }

        public Color Add(Color color)
        {
            _context.Colors.Add(color);
            _context.SaveChanges();

            return _context.Entry(color).Entity;
        }

        public Color Delete(int id)
        {
            var entity = _context.Colors.Find(id);
            _context.Colors.Remove(entity);
            _context.SaveChanges();

            return entity;
        }

        public List<Color> GetAll()
        {
            return _context.Colors.ToList();
        }

        public Color GetById(int id)
        {
            return _context.Colors.Find(id);
        }

        public Color GetByName(string name)
        {
            return _context.Colors.FirstOrDefault(x => x.Name == name);
        }

        public Color Update(int id, Color color)
        {
            var entity = _context.Colors.Find(id);
            entity.Name = color.Name;
            _context.SaveChanges();

            return entity;
        }
    }
}
