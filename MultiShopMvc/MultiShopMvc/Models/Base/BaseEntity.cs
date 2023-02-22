namespace MultiShopMvc.Models.Base;

public class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? ModifiedTime { get; set;
    }
}
