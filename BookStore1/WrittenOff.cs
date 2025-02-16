using System;
using System.Collections.Generic;

namespace BookStore1;

public partial class WrittenOff
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int Amount { get; set; }

    public DateOnly WrittenOffDate { get; set; }

    public virtual Book Book { get; set; } = null!;
}
