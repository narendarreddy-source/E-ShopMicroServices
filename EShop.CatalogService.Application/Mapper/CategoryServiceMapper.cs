using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AutoMapper;
using EShop.CatalogService.Application.Dtos.Request;
using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Domain.Entities;

namespace EShop.CatalogService.Application.Mapper
{
    public class CategoryServiceMapper : Profile
    {
       public CategoryServiceMapper() {

            CreateMap<Category, GetCategoriesDto>();
            CreateMap<AddCategoryDto, Category>();
        }
    }
}
