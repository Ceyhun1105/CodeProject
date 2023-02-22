using MultiShopMvc.Models.Base;
using MultiShopMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Models;

public class Product : BaseEntity
{

    public int CategoryId { get; set; }

    [StringLength(maximumLength: 100)]

    public string Name { get; set; }
    public Gender Gender { get; set; }
    [MinLength(1)]
    public int Stock { get; set; }

    public double SalePrice { get; set; }
    public int DiscountPercent { get; set; }
    public double CostPrice { get; set; }



    public bool IsFeatured { get; set; }

    public Category? Category { get; set; }
    public List<ProductImages>? Images { get; set; }
    public List<Review>? Reviews { get; set; }
    public List<Color>? Colors { get; set; }
    public List<Size>? Sizes { get; set; }
}
