using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Interface
{
    public interface IStockService
    {
        ServiceResult<List<GetAllStocksDto>> GetAll(int page, int pageSize);
        ServiceResult<GetStockByIdDto> GetById(int id);
        ServiceResult<List<GetAllStocksDto>> GetAllByProductId(int productId);
        ServiceResult<List<GetAllStocksDto>> Search(string productName, int? sizeId, int? colorId, bool? getOutOfStocks);
        ServiceResult<GetStockByIdDto> Add(CreateStockDto createStockDto);
        ServiceResult<GetStockByIdDto> Update(UpdateStockDto updateStockDto);
        ServiceResult<GetStockByIdDto> UpdateQuantity(UpdateStockQuantityDto updateStockQuantityDto);
        ServiceResult<GetStockByIdDto> Delete(int id);
    }
}
