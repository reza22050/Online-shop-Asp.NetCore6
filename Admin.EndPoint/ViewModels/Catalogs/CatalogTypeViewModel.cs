using System.ComponentModel.DataAnnotations;

namespace Admin.EndPoint.ViewModels.Catalogs
{
    public class CatalogTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Type name")]
        [Required]
        [MaxLength(100,ErrorMessage ="Type must be under 100 characters")]
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }

    }
}
