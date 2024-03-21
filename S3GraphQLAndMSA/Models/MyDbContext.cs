﻿using Microsoft.EntityFrameworkCore;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> dbc) : base(dbc)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id).HasName("ProductId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Cost)
                .HasColumnName("Price")
                .IsRequired();

                entity.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId)
                .HasConstraintName("CategoryToProducts");

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.HasKey(x => x.Id).HasName("CategoryId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName(@"CategoryName")
                .HasMaxLength(255)
                .IsRequired();

            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id).HasName("StorageId");

                entity.Property(x => x.Name)
                .HasColumnName("StorageName");
                entity.Property(x => x.Count)
                .HasColumnName("ProductCount");

                entity.HasMany(x => x.Products)
                .WithMany(c => c.Storages)
                .UsingEntity(j => j.ToTable("StorageProduct"));

            });
        }
    }
}
