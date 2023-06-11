using AutoMapper;
using StockTrackingApp.Business.Dto.Size;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Business
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;
        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public ServiceResult<GetSizeByIdDto> Add(CreateSizeDto createSizeDto)
        {
            var size = _sizeRepository.GetByName(createSizeDto.Name);
            if (size != null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Size already exists", (int)HttpStatusCode.BadRequest);
            }

            Size addedSize = _sizeRepository.Add(_mapper.Map<Size>(createSizeDto));

            if (addedSize is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetSizeByIdDto>.Success(_mapper.Map<GetSizeByIdDto>(addedSize));
        }

        public ServiceResult<GetSizeByIdDto> Delete(int id)
        {
            var size = _sizeRepository.GetById(id);

            if (size is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Size not found", (int)HttpStatusCode.NotFound);
            }

            Size deletedSize = _sizeRepository.Delete(id);

            if (deletedSize is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetSizeByIdDto>.Success(_mapper.Map<GetSizeByIdDto>(deletedSize));
        }

        public ServiceResult<List<GetAllSizesDto>> GetAll()
        {
            var sizes = _sizeRepository.GetAll();

            if (sizes is null)
            {
                return ServiceResult<List<GetAllSizesDto>>.Failed(null, "Sizes not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<List<GetAllSizesDto>>.Success(_mapper.Map<List<GetAllSizesDto>>(sizes));
        }

        public ServiceResult<GetSizeByIdDto> GetById(int id)
        {
            var size = _sizeRepository.GetById(id);

            if (size is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Size not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetSizeByIdDto>.Success(_mapper.Map<GetSizeByIdDto>(size));
        }

        public ServiceResult<GetSizeByIdDto> GetByName(string name)
        {
            var size = _sizeRepository.GetByName(name);

            if (size is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Size not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetSizeByIdDto>.Success(_mapper.Map<GetSizeByIdDto>(size));
        }

        public ServiceResult<GetSizeByIdDto> Update(UpdateSizeDto updateSizeDto)
        {
            var size = _sizeRepository.GetById(updateSizeDto.Id);

            if (size is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Size not found", (int)HttpStatusCode.NotFound);
            }

            Size updatedSize = _sizeRepository.Update(updateSizeDto.Id,_mapper.Map<Size>(updateSizeDto));

            if (updatedSize is null)
            {
                return ServiceResult<GetSizeByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetSizeByIdDto>.Success(_mapper.Map<GetSizeByIdDto>(updatedSize));
        }
    }
}
