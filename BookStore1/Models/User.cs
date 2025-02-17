using System;
using System.Collections.Generic;

namespace BookStore1.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Reserved> Reserveds { get; set; } = new List<Reserved>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
