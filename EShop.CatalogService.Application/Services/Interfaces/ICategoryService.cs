using EShop.CatalogService.Application.Dtos.Request;
using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoriesDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<GetCategoriesDto> AddCategoryAsync(AddCategoryDto addCategoryDto, CancellationToken cancellationToken);
    }
}
