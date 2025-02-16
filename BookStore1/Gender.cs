﻿using System;
using System.Collections.Generic;

namespace BookStore1;

public partial class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
