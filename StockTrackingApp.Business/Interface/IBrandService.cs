using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Interface
{
    public interface IBrandService
    {
        ServiceResult<List<GetAllBrandsDto>> GetAll();
        ServiceResult<GetBrandByIdWithProductsDto> GetById(int id, bool withProducts = false);
        ServiceResult<GetBrandByIdWithProductsDto> GetByName(string name, bool withProducts = false);
        ServiceResult<GetBrandByIdDto> Add(CreateBrandDto createBrandDto);
        ServiceResult<GetBrandByIdDto> Update(UpdateBrandDto updateBrandDto);
        ServiceResult<GetBrandByIdDto> Delete(int id);
    }
}
