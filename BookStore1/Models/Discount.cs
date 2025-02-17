using System;
using System.Collections.Generic;

namespace BookStore1.Models;

public partial class Discount
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<BookDiscount> BookDiscounts { get; set; } = new List<BookDiscount>();
}
