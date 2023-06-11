using AutoMapper;
using StockTrackingApp.Business.Dto.Color;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Business
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public ServiceResult<GetColorByIdDto> Add(CreateColorDto createColorDto)
        {
            var color = _colorRepository.GetByName(createColorDto.Name);

            if (color != null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Color already exists", (int)HttpStatusCode.BadRequest);
            }

            Color addedColor = _colorRepository.Add(_mapper.Map<Color>(createColorDto));

            if (addedColor is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetColorByIdDto>.Success(_mapper.Map<GetColorByIdDto>(addedColor));
        }

        public ServiceResult<GetColorByIdDto> Delete(int id)
        {
            var color = _colorRepository.GetById(id);

            if (color is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Color not found", (int)HttpStatusCode.NotFound);
            }

            Color deletedColor = _colorRepository.Delete(id);

            if (deletedColor is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetColorByIdDto>.Success(_mapper.Map<GetColorByIdDto>(deletedColor));
        }

        public ServiceResult<List<GetAllColorsDto>> GetAll()
        {
            var colors = _colorRepository.GetAll();

            if (colors is null)
            {
                return ServiceResult<List<GetAllColorsDto>>.Failed(null, "Colors not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<List<GetAllColorsDto>>.Success(_mapper.Map<List<GetAllColorsDto>>(colors));
        }

        public ServiceResult<GetColorByIdDto> GetById(int id)
        {
            var color = _colorRepository.GetById(id);

            if (color is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Color not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetColorByIdDto>.Success(_mapper.Map<GetColorByIdDto>(color));
        }

        public ServiceResult<GetColorByIdDto> GetByName(string name)
        {
            var color = _colorRepository.GetByName(name);

            if (color is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Color not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetColorByIdDto>.Success(_mapper.Map<GetColorByIdDto>(color));
        }

        public ServiceResult<GetColorByIdDto> Update(UpdateColorDto updateColorDto)
        {
            var color = _colorRepository.GetById(updateColorDto.Id);

            if (color is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Color not found", (int)HttpStatusCode.NotFound);
            }

            Color updatedColor = _colorRepository.Update(updateColorDto.Id, _mapper.Map<Color>(updateColorDto));

            if (updatedColor is null)
            {
                return ServiceResult<GetColorByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetColorByIdDto>.Success(_mapper.Map<GetColorByIdDto>(updatedColor));
        }
    }
}
