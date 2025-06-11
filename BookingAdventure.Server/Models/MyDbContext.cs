using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingAdventure.Server.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adventure> Adventures { get; set; }

    public virtual DbSet<AdventureCategory> AdventureCategories { get; set; }

    public virtual DbSet<AdventureImage> AdventureImages { get; set; }

    public virtual DbSet<AdventureType> AdventureTypes { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5AU1IL4;Database=Adventures;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adventure>(entity =>
        {
            entity.HasKey(e => e.AdventureId).HasName("PK__Adventur__D721A00E0514424D");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.AdventureType).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.AdventureTypeId)
                .HasConstraintName("FK__Adventure__Adven__5441852A");

            entity.HasOne(d => d.Category).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Adventure__Categ__403A8C7D");

            entity.HasOne(d => d.Destination).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.DestinationId)
                .HasConstraintName("FK__Adventure__Desti__5165187F");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK__Adventure__Instr__3F466844");
        });

        modelBuilder.Entity<AdventureCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Adventur__19093A0BE2155214");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<AdventureImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Adventur__7516F70C4E050123");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);

            entity.HasOne(d => d.Adventure).WithMany(p => p.AdventureImages)
                .HasForeignKey(d => d.AdventureId)
                .HasConstraintName("FK__Adventure__Adven__4316F928");
        });

        modelBuilder.Entity<AdventureType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Adventur__516F03B5B2238051");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED12A7D4C1");

            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(50);
            entity.Property(e => e.ScheduledDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Adventure).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AdventureId)
                .HasConstraintName("FK__Bookings__Advent__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Bookings__UserId__46E78A0C");
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ContactM__C87C0C9CCA663EE4");

            entity.Property(e => e.DateSent)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(1000);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.DestinationId).HasName("PK__Destinat__DB5FE4CCA85CDAC3");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Categories).WithMany(p => p.Destinations)
                .UsingEntity<Dictionary<string, object>>(
                    "DestinationCategory",
                    r => r.HasOne<AdventureCategory>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Destinati__Categ__5812160E"),
                    l => l.HasOne<Destination>().WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Destinati__Desti__571DF1D5"),
                    j =>
                    {
                        j.HasKey("DestinationId", "CategoryId").HasName("PK__Destinat__7ACF776CBA37112A");
                        j.ToTable("DestinationCategories");
                    });
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__9D010A9BB0614926");

            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CEF660F795");

            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Adventure).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AdventureId)
                .HasConstraintName("FK__Reviews__Adventu__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C4863399C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E86275BB").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Img)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
