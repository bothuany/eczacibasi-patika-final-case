using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Interface
{
    public interface IColorRepository
    {
        List<Color> GetAll();
        Color GetById(int id);
        Color GetByName(string name);
        Color Add(Color color);
        Color Update(int id, Color color);
        Color Delete(int id);
    }
}
