using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;

namespace Admin.EndPoint.MappingProfiles
{
    public class CatalogVMMappingProfile:Profile
    {
        public CatalogVMMappingProfile()
        {
            CreateMap<CatalogTypeDto, CatalogTypeViewModel>().ReverseMap();
        }
    }
}
