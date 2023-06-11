using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id, bool withProducts = false);
        Category GetByName(string name, bool withProducts = false);
        Category Add(Category category);
        Category Update(int id, Category category);
        Category Delete(int id);
    }
}
