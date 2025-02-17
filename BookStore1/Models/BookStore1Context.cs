using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.Models;

public partial class BookStore1Context : DbContext
{
    public BookStore1Context()
    {
    }

    public BookStore1Context(DbContextOptions<BookStore1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookDiscount> BookDiscounts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Publishing> Publishings { get; set; }

    public virtual DbSet<Reserved> Reserveds { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WrittenOff> WrittenOffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-005JK2O\\SQLEXPRESS;Database=BookStore1;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC079398809E");

            entity.ToTable("Author");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC07213FD504");

            entity.ToTable("Book");

            entity.HasIndex(e => e.Name, "UQ__Book__737584F68221D7AC").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__AuthorId__6D0D32F4");

            entity.HasOne(d => d.ContinueToNavigation).WithMany(p => p.InverseContinueToNavigation)
                .HasForeignKey(d => d.ContinueTo)
                .HasConstraintName("FK__Book__ContinueTo__6EF57B66");

            entity.HasOne(d => d.Gender).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__GenderId__6C190EBB");

            entity.HasOne(d => d.Publishing).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__Publishing__6E01572D");
        });

        modelBuilder.Entity<BookDiscount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookDisc__3214EC07B1632BEB");

            entity.HasOne(d => d.Book).WithMany(p => p.BookDiscounts)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookDisco__BookI__71D1E811");

            entity.HasOne(d => d.Discount).WithMany(p => p.BookDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookDisco__Disco__72C60C4A");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC073185A7C0");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gender__3214EC0793DC034C");

            entity.ToTable("Gender");
        });

        modelBuilder.Entity<Publishing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishi__3214EC07E59451A9");

            entity.ToTable("Publishing");
        });

        modelBuilder.Entity<Reserved>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserved__3214EC0717AB6178");

            entity.ToTable("Reserved");

            entity.HasOne(d => d.Book).WithMany(p => p.Reserveds)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Reserved__BookId__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.Reserveds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reserved__UserId__75A278F5");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sale__3214EC07C071BE38");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleDate).HasColumnName("saleDate");

            entity.HasOne(d => d.Book).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sale__BookId__6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.Sales)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sale__UserId__70DDC3D8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC075AB6C7B1");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ__User__5E55825BB10671A2").IsUnique();

            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(8);
        });

        modelBuilder.Entity<WrittenOff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WrittenO__3214EC07896EC809");

            entity.ToTable("WrittenOff");

            entity.HasOne(d => d.Book).WithMany(p => p.WrittenOffs)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__WrittenOf__BookI__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
