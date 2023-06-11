using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Interface
{
    public interface ISizeRepository
    {
        List<Size> GetAll();
        Size GetById(int id);
        Size GetByName(string name);
        Size Add(Size size);
        Size Update(int id, Size size);
        Size Delete(int id);
    }
}
