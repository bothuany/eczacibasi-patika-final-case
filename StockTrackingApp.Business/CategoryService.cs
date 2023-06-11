using AutoMapper;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ServiceResult<GetCategoryByIdDto> Add(CreateCategoryDto createCategoryDto)
        {
            var category = _categoryRepository.GetByName(createCategoryDto.Name);

            if (category is not null)
            {
                return ServiceResult<GetCategoryByIdDto>.Failed(null, "Category already exists", (int)HttpStatusCode.BadRequest);
            }

            Category addedCategory = _categoryRepository.Add(_mapper.Map<Data.Entity.Category>(createCategoryDto));

            if (addedCategory is null)
            {
                return ServiceResult<GetCategoryByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetCategoryByIdDto>.Success(_mapper.Map<GetCategoryByIdDto>(addedCategory));

        }

        public ServiceResult<GetCategoryByIdDto> Delete(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category is null)
            {
                return ServiceResult<GetCategoryByIdDto>.Failed(null, "Category not found", (int)HttpStatusCode.NotFound);
            }

            Category deletedCategory = _categoryRepository.Delete(id);

            if (deletedCategory is null)
            {
                return ServiceResult<GetCategoryByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetCategoryByIdDto>.Success(_mapper.Map<GetCategoryByIdDto>(deletedCategory));
        }

        public ServiceResult<List<GetAllCategoriesDto>> GetAll()
        {
            var categories = _categoryRepository.GetAll();

            if (categories is null)
            {
                return ServiceResult<List<GetAllCategoriesDto>>.Failed(null, "Categories not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<List<GetAllCategoriesDto>>.Success(_mapper.Map<List<GetAllCategoriesDto>>(categories));
        }

        public ServiceResult<GetCategoryByIdWithProductsDto> GetById(int id, bool withProducts = true)
        {
            var category = _categoryRepository.GetById(id, withProducts);

            if (category is null)
            {
                return ServiceResult<GetCategoryByIdWithProductsDto>.Failed(null, "Category not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetCategoryByIdWithProductsDto>.Success(_mapper.Map<GetCategoryByIdWithProductsDto>(category));
        }

        public ServiceResult<GetCategoryByIdWithProductsDto> GetByName(string name, bool withProducts = true)
        {
            var category = _categoryRepository.GetByName(name, withProducts);

            if (category is null)
            {
                return ServiceResult<GetCategoryByIdWithProductsDto>.Failed(null, "Category not found", (int)HttpStatusCode.NotFound);
            }

            return ServiceResult<GetCategoryByIdWithProductsDto>.Success(_mapper.Map<GetCategoryByIdWithProductsDto>(category));
        }

        public ServiceResult<GetCategoryByIdDto> Update(UpdateCategoryDto updateCategoryDto)
        {
            var category = _categoryRepository.GetById(updateCategoryDto.Id);

            if (category is null)
            {
                return ServiceResult<GetCategoryByIdDto>.Failed(null, "Category not found", (int)HttpStatusCode.NotFound);
            }

            Category updatedCategory = _categoryRepository.Update(updateCategoryDto.Id, _mapper.Map<Data.Entity.Category>(updateCategoryDto));

            if (updatedCategory is null)
            {
                return ServiceResult<GetCategoryByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetCategoryByIdDto>.Success(_mapper.Map<GetCategoryByIdDto>(updatedCategory));
        }
    }
}
