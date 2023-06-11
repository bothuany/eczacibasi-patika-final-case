using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Interface
{
    public interface IColorService
    {
        ServiceResult<List<GetAllColorsDto>> GetAll();
        ServiceResult<GetColorByIdDto> GetById(int id);
        ServiceResult<GetColorByIdDto> GetByName(string name);
        ServiceResult<GetColorByIdDto> Add(CreateColorDto createColorDto);
        ServiceResult<GetColorByIdDto> Update(UpdateColorDto updateColorDto);
        ServiceResult<GetColorByIdDto> Delete(int id);
    }
}
