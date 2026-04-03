using AutoMapper;
using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProdcutRepository _prodcutRepository;
        private readonly IMapper _mapper;
        public ProductService(IProdcutRepository prodcutRepository,IMapper mapper) { 
                _prodcutRepository = prodcutRepository;
                _mapper = mapper;
        }
        public async Task<IEnumerable<GetProductsDto>> GetAllAsync(CancellationToken cancellationToken)
        {
           var products = await _prodcutRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<GetProductsDto>>(products);
        }
    }
}
