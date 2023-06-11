using AutoMapper;
using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Business
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public ServiceResult<GetBrandByIdDto> Add(CreateBrandDto createBrandDto)
        {
            var brand = _brandRepository.GetByName(createBrandDto.Name);

            if (brand is not null)
            {
                return ServiceResult<GetBrandByIdDto>.Failed(null, "Brand already exists", (int)HttpStatusCode.BadRequest);
            }

            Brand addedBrand = _brandRepository.Add(_mapper.Map<Brand>(createBrandDto));

            if (addedBrand is null)
            {
                return ServiceResult<GetBrandByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetBrandByIdDto>.Success(_mapper.Map<GetBrandByIdDto>(addedBrand));
        }

        public ServiceResult<GetBrandByIdDto> Delete(int id)
        {
            var brand = _brandRepository.GetById(id);

            if (brand is null)
            {
                return ServiceResult<GetBrandByIdDto>.Failed(null, "Brand not found", (int)HttpStatusCode.NotFound);
            }

            Brand deletedBrand = _brandRepository.Delete(id);

            if (deletedBrand is null)
            {
                return ServiceResult<GetBrandByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetBrandByIdDto>.Success(_mapper.Map<GetBrandByIdDto>(deletedBrand));
        }

        public ServiceResult<List<GetAllBrandsDto>> GetAll()
        {
            var brands = _brandRepository.GetAll();

            if (brands is null)
            {
                return ServiceResult<List<GetAllBrandsDto>>.Failed(null, "Brands not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<List<GetAllBrandsDto>>.Success(_mapper.Map<List<GetAllBrandsDto>>(brands));
        }

        public ServiceResult<GetBrandByIdWithProductsDto> GetById(int id, bool withProducts=false)
        {
            var brand = _brandRepository.GetById(id, withProducts);

            if (brand is null)
            {
                return ServiceResult<GetBrandByIdWithProductsDto>.Failed(null, "Brand not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetBrandByIdWithProductsDto>.Success(_mapper.Map<GetBrandByIdWithProductsDto>(brand));
        }

        public ServiceResult<GetBrandByIdWithProductsDto> GetByName(string name, bool withProducts = false)
        {
            var brand = _brandRepository.GetByName(name, withProducts);

            if (brand is null)
            {
                return ServiceResult<GetBrandByIdWithProductsDto>.Failed(null, "Brand not found", (int)HttpStatusCode.NotFound);
            }
        
            return ServiceResult<GetBrandByIdWithProductsDto>.Success(_mapper.Map<GetBrandByIdWithProductsDto>(brand));
        }

        public ServiceResult<GetBrandByIdDto> Update(UpdateBrandDto updateBrandDto)
        {
            var brand = _brandRepository.GetById(updateBrandDto.Id);

            if (brand is null)
            {
                return ServiceResult<GetBrandByIdDto>.Failed(null, "Brand not found", (int)HttpStatusCode.NotFound);
            }

            Brand updatedBrand = _brandRepository.Update(updateBrandDto.Id, _mapper.Map<Brand>(updateBrandDto));

            if (updatedBrand is null)
            {
                return ServiceResult<GetBrandByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetBrandByIdDto>.Success(_mapper.Map<GetBrandByIdDto>(updatedBrand));
        } 
    }
}
