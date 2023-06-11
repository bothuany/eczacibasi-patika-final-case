using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Data.Interface
{
    public interface IStockRepository
    {
        List<Stock> GetAll(int page, int pageSize);
        Stock GetById(int id);
        List<Stock> GetAllByProductId(int productId);
        List<Stock> Search(string productName, int? sizeId, int? colorId, bool? getOutOfStocks);
        Stock Add(Stock stock);
        Stock Update(int id, Stock stock);
        Stock UpdateQuantity(int id, int quantityChange);
        Stock Delete(int id);
    }
}
