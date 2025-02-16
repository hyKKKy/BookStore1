using System;
using System.Collections.Generic;

namespace BookStore1;

public partial class Sale
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public DateOnly SaleDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
