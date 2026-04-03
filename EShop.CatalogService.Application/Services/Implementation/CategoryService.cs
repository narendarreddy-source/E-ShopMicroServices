using AutoMapper;
using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public CategoryService (ICategoryRepository categoriesRepository,IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCategoriesDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoriesRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<GetCategoriesDto>>(categories);
        }
    }
}
