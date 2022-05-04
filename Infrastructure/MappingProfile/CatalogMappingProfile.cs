using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Catalogs.CatalogTypes;
using Application.Catalogs.GetMenuItem;
using AutoMapper;
using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MappingProfile
{
    public class CatalogMappingProfile:Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();
            CreateMap<CatalogType, CatalogTypeListDto>().ForMember(dest => dest.SubTypeCount, option =>option.MapFrom(src=>src.SubType.Count));
            
            CreateMap<CatalogType,MenuItemDo>()
                .ForMember(dest=>dest.Name , option => option.MapFrom(src=>src.Type))
                .ForMember(dest=>dest.ParentId, option => option.MapFrom(src=>src.ParentCatalogTypeId))
                .ForMember(dest=> dest.SubMenu, option=>option.MapFrom(src=>src.SubType));

            CreateMap<CatalogItemFeature, AddNewCatalogItemFeature_Dto>().ReverseMap();
            CreateMap<CatalogItemImage, AddNewCatalogItemImage_Dto>().ReverseMap();
            CreateMap<CatalogItem, AddNewCatalogItemDto>()
                .ForMember(dest=>dest.Features, opt=>opt.MapFrom(src=>src.CatalogItemFeatures))
                .ForMember(dest=>dest.Images, opt=>opt.MapFrom(src=>src.CatalogItemImages))
                .ReverseMap();

            CreateMap<CatalogBrand, CatalogBrandDto>().ReverseMap();
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();

        }
    }
}
