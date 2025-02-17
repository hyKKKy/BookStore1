using System;
using System.Collections.Generic;

namespace BookStore1.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Pages { get; set; }

    public int Price { get; set; }

    public int Amount { get; set; }

    public int Selfprice { get; set; }

    public int PublishingId { get; set; }

    public int AuthorId { get; set; }

    public int? ContinueTo { get; set; }

    public int GenderId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookDiscount> BookDiscounts { get; set; } = new List<BookDiscount>();

    public virtual Book? ContinueToNavigation { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<Book> InverseContinueToNavigation { get; set; } = new List<Book>();

    public virtual Publishing Publishing { get; set; } = null!;

    public virtual ICollection<Reserved> Reserveds { get; set; } = new List<Reserved>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<WrittenOff> WrittenOffs { get; set; } = new List<WrittenOff>();
}
