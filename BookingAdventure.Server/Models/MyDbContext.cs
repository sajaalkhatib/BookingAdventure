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

    public virtual DbSet<AdventureDetail> AdventureDetails { get; set; }

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
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DH1T2CV;Database=Adventure;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adventure>(entity =>
        {
            entity.HasKey(e => e.AdventureId).HasName("PK__Adventur__D721A00E851B8694");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.AdventureType).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.AdventureTypeId)
                .HasConstraintName("FK__Adventure__Adven__5629CD9C");

            entity.HasOne(d => d.Category).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Adventure__Categ__4222D4EF");

            entity.HasOne(d => d.Destination).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.DestinationId)
                .HasConstraintName("FK__Adventure__Desti__534D60F1");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Adventures)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK__Adventure__Instr__412EB0B6");
        });

        modelBuilder.Entity<AdventureCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Adventur__19093A0B615A291C");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<AdventureDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Adventur__3214EC07446586E0");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FaqsJson).HasColumnName("FAQsJson");
            entity.Property(e => e.Overview).HasColumnType("text");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Adventure).WithMany(p => p.AdventureDetails)
                .HasForeignKey(d => d.AdventureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Adventure__Adven__5EBF139D");
        });

        modelBuilder.Entity<AdventureImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Adventur__7516F70C1C6A4FE8");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);

            entity.HasOne(d => d.Adventure).WithMany(p => p.AdventureImages)
                .HasForeignKey(d => d.AdventureId)
                .HasConstraintName("FK__Adventure__Adven__44FF419A");
        });

        modelBuilder.Entity<AdventureType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Adventur__516F03B59200FE15");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<AdventureType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Adventur__516F03B5B2238051");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AEDF14691BF");

            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(50);
            entity.Property(e => e.ScheduledDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Adventure).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AdventureId)
                .HasConstraintName("FK__Bookings__Advent__49C3F6B7");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Bookings__UserId__48CFD27E");
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ContactM__C87C0C9C8B78A2DC");

            entity.Property(e => e.DateSent)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(1000);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.DestinationId).HasName("PK__Destinat__DB5FE4CC287F23B6");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Categories).WithMany(p => p.Destinations)
                .UsingEntity<Dictionary<string, object>>(
                    "DestinationCategory",
                    r => r.HasOne<AdventureCategory>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Destinati__Categ__59FA5E80"),
                    l => l.HasOne<Destination>().WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Destinati__Desti__59063A47"),
                    j =>
                    {
                        j.HasKey("DestinationId", "CategoryId").HasName("PK__Destinat__7ACF776CDC625CDE");
                        j.ToTable("DestinationCategories");
                    });
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__9D010A9B175F3931");

            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CE3E5159DF");

            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Adventure).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AdventureId)
                .HasConstraintName("FK__Reviews__Adventu__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CE1824200");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346C4E98A1").IsUnique();

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
