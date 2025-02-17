using System;
using System.Collections.Generic;

namespace BookStore1.Models;

public partial class BookDiscount
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int DiscountId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Discount Discount { get; set; } = null!;
}
