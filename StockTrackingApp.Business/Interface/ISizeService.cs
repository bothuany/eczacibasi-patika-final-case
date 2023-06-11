using StockTrackingApp.Business.Dto.Size;
using StockTrackingApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Interface
{
    public interface ISizeService
    {
        ServiceResult<List<GetAllSizesDto>> GetAll();
        ServiceResult<GetSizeByIdDto> GetById(int id);
        ServiceResult<GetSizeByIdDto> GetByName(string name);
        ServiceResult<GetSizeByIdDto> Add(CreateSizeDto createSizeDto);
        ServiceResult<GetSizeByIdDto> Update(UpdateSizeDto updateSizeDto);
        ServiceResult<GetSizeByIdDto> Delete(int id);
    }
}
