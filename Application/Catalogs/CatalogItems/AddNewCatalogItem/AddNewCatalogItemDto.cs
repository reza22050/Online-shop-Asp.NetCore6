using FluentValidation;

namespace Application.Catalogs.CatalogItems.AddNewCatalogItem
{
    public class AddNewCatalogItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }

        public List<AddNewCatalogItemFeature_Dto> Features { get; set; }
        public List<AddNewCatalogItemImage_Dto>? Images { get; set; }

    }


    public class AddNewCatalogItemDtoValidator:AbstractValidator<AddNewCatalogItemDto>
    {
        public AddNewCatalogItemDtoValidator()
        {
            RuleFor(x => x.CatalogTypeId).NotNull().WithMessage("Enter you catalogItem name!");
            RuleFor(x => x.Name).Length(2, 100);
            RuleFor(x => x.Description).NotNull().WithMessage("Enter description please!");
            RuleFor(x => x.AvailableStock).InclusiveBetween(0, int.MaxValue);
            RuleFor(x=>x.Price).InclusiveBetween(0, decimal.MaxValue);
            RuleFor(x => x.Price).NotNull();
        }
    }

}

