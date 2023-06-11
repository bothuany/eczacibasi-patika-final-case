using AutoMapper;
using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Business.Interface;
using StockTrackingApp.Data.Entity;
using StockTrackingApp.Data.Interface;
using System.Collections.Generic;
using System.Net;

namespace StockTrackingApp.Business
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            this._stockRepository = stockRepository;
            this._mapper = mapper;
        }

        public ServiceResult<GetStockByIdDto> Add(CreateStockDto createStockDto)
        {
            Stock addedStock = _stockRepository.Add(_mapper.Map<Stock>(createStockDto));

            if (addedStock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetStockByIdDto>.Success(_mapper.Map<GetStockByIdDto>(addedStock));
        }

        public ServiceResult<GetStockByIdDto> Delete(int id)
        {
            var stock = _stockRepository.GetById(id);

            if (stock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Stock not found", (int)HttpStatusCode.NotFound);
            }

            Stock deletedStock = _stockRepository.Delete(id);

            if (deletedStock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetStockByIdDto>.Success(_mapper.Map<GetStockByIdDto>(deletedStock));
        }

        public ServiceResult<List<GetAllStocksDto>> GetAll(int page, int pageSize)
        {
            var stocks = _stockRepository.GetAll(page, pageSize);

            if (stocks is null)
            {
                return ServiceResult<List<GetAllStocksDto>>.Failed(null, "Stocks not found", (int)HttpStatusCode.NotFound);
            }

            var mappedStocks = _mapper.Map<List<GetAllStocksDto>>(stocks);

            return ServiceResult<List<GetAllStocksDto>>.Success(mappedStocks);
        }

        public ServiceResult<List<GetAllStocksDto>> GetAllByProductId(int productId)
        {
            var stocks = _stockRepository.GetAllByProductId(productId);

            if (stocks is null)
            {
                return ServiceResult<List<GetAllStocksDto>>.Failed(null, "Stocks not found", (int)HttpStatusCode.NotFound);
            }

            var mappedStocks = _mapper.Map<List<GetAllStocksDto>>(stocks);

            return ServiceResult<List<GetAllStocksDto>>.Success(mappedStocks);
        }

        public ServiceResult<GetStockByIdDto> GetById(int id)
        {
            var stock = _stockRepository.GetById(id);

            if (stock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Stock not found", (int)HttpStatusCode.NotFound);
            }

            var mappedStock = _mapper.Map<GetStockByIdDto>(stock);

            return ServiceResult<GetStockByIdDto>.Success(mappedStock);
        }

        public ServiceResult<List<GetAllStocksDto>> Search(string productName, int? sizeId, int? colorId, bool? getOutOfStocks)
        {
            var stocks = _stockRepository.Search(productName, sizeId, colorId, getOutOfStocks);

            if (stocks is null)
            {
                return ServiceResult<List<GetAllStocksDto>>.Failed(null, "Stocks not found", (int)HttpStatusCode.NotFound);
            }

            var mappedStocks = _mapper.Map<List<GetAllStocksDto>>(stocks);

            return ServiceResult<List<GetAllStocksDto>>.Success(mappedStocks);
        }

        public ServiceResult<GetStockByIdDto> Update(UpdateStockDto updateStockDto)
        {
            var stock = _stockRepository.GetById(updateStockDto.Id);

            if (stock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Stock not found", (int)HttpStatusCode.NotFound);
            }

            Stock updatedStock = _stockRepository.Update(updateStockDto.Id, _mapper.Map<Stock>(updateStockDto));

            if (updatedStock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetStockByIdDto>.Success(_mapper.Map<GetStockByIdDto>(updatedStock));
        }

        public ServiceResult<GetStockByIdDto> UpdateQuantity(UpdateStockQuantityDto updateStockQuantityDto)
        {
            var stock = _stockRepository.GetById(updateStockQuantityDto.Id);

            if (stock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Stock not found", (int)HttpStatusCode.NotFound);
            }

            if(stock.Quantity+updateStockQuantityDto.QuantityChange < 0)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Stock quantity can not be less than 0", (int)HttpStatusCode.BadRequest);
            }

            Stock updatedStock = _stockRepository.UpdateQuantity(updateStockQuantityDto.Id, updateStockQuantityDto.QuantityChange);

            if (updatedStock is null)
            {
                return ServiceResult<GetStockByIdDto>.Failed(null, "Something went wrong", (int)HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetStockByIdDto>.Success(_mapper.Map<GetStockByIdDto>(updatedStock));
        }
    }
}
