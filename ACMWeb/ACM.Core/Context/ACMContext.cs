using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ACM.Core.Context
{
    public partial class ACMContext : DbContext
    {
        public ACMContext()
        {
        }

        public ACMContext(DbContextOptions<ACMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<CheckInForm> CheckInForm { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<PhotoComment> PhotoComment { get; set; }
        public virtual DbSet<ProfileInfo> ProfileInfo { get; set; }
        public virtual DbSet<StoreContracts> StoreContracts { get; set; }
        public virtual DbSet<StoreInfo> StoreInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9O0T7I3\\SQLEXPRESS;Database=ACM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdminId).HasMaxLength(100);

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<CheckInForm>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Make).HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OdoMeter).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Vin).HasMaxLength(50);

                entity.Property(e => e.Year).HasMaxLength(10);
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ThumbnailImage).HasMaxLength(100);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Gallery)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gallery_AspNetUsers");
            });

            modelBuilder.Entity<PhotoComment>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.PhotoComment)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_PhotoComment_Gallery");
            });

            modelBuilder.Entity<ProfileInfo>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Photo).HasMaxLength(200);

                entity.Property(e => e.Signature).HasMaxLength(200);

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProfileInfo)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileInfo_AspNetUsers");
            });

            modelBuilder.Entity<StoreContracts>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreContracts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreContracts_AspNetUsers");
            });

            modelBuilder.Entity<StoreInfo>(entity =>
            {
                entity.HasKey(e => e.LogoId);

                entity.Property(e => e.Logo).HasMaxLength(100);

                entity.Property(e => e.StoreId).HasMaxLength(450);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreInfo)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StoreInfo_AspNetUsers");
            });
        }
    }
}
