using AutoMapper;
using EShop.CatalogService.Application.Dtos.Request;
using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Mapper
{
    public class ProductServiceMapper : Profile
    {
        public ProductServiceMapper() {
            CreateMap<Product, GetProductsDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
