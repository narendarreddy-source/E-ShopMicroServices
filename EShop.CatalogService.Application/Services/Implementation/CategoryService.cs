using AutoMapper;
using EShop.CatalogService.Application.Dtos.Request;
using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Application.Services.Interfaces;
using EShop.CatalogService.Domain.Entities;
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

        public async Task<GetCategoriesDto> AddCategoryAsync(AddCategoryDto addCategoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(addCategoryDto);
            await _categoriesRepository.AddCategoryAsync(category, cancellationToken);
            return _mapper.Map<GetCategoriesDto>(category);
        }

        public async Task<IEnumerable<GetCategoriesDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoriesRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<GetCategoriesDto>>(categories);
        }
    }
}
