using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Interface
{
    public interface ICategoryService
    {
        ServiceResult<List<GetAllCategoriesDto>> GetAll();
        ServiceResult<GetCategoryByIdWithProductsDto> GetById(int id, bool withProducts = true);
        ServiceResult<GetCategoryByIdWithProductsDto> GetByName(string name, bool withProducts = true);
        ServiceResult<GetCategoryByIdDto> Add(CreateCategoryDto createCategoryDto);
        ServiceResult<GetCategoryByIdDto> Update(UpdateCategoryDto updateCategoryDto);
        ServiceResult<GetCategoryByIdDto> Delete(int id);
    }
}
