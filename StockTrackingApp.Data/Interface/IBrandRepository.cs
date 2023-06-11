using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Interface
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
        Brand GetById(int id, bool withProducts = false);
        Brand GetByName(string name, bool withProducts = false);
        Brand Add(Brand brand);
        Brand Update(int id, Brand brand);
        Brand Delete(int id);
    }
}
