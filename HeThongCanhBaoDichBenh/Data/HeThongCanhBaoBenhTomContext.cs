using System;
using System.Collections.Generic;
using HeThongCanhBaoDichBenh.Models;
using Microsoft.EntityFrameworkCore;

namespace HeThongCanhBaoDichBenh.Data;

public partial class HeThongCanhBaoBenhTomContext : DbContext
{
    public HeThongCanhBaoBenhTomContext()
    {
    }

    public HeThongCanhBaoBenhTomContext(DbContextOptions<HeThongCanhBaoBenhTomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Prevention> Preventions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HP\\SQLEXPRESS;Initial Catalog=HeThongCanhBaoBenhTom;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCA11D9D651");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentCreateAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Comment_Post");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PK__Disease__69B533893F643E68");

            entity.ToTable("Disease");

            entity.Property(e => e.DiseaseName).HasMaxLength(100);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Image__7516F4EC895D0F19");

            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.DiseaseId).HasColumnName("DiseaseID");
            entity.Property(e => e.ImageCreateAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.Disease).WithMany(p => p.Images)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("FK_Image_Disease");

            entity.HasOne(d => d.News).WithMany(p => p.Images)
                .HasForeignKey(d => d.NewsId)
                .HasConstraintName("FK_Image_News");

            entity.HasOne(d => d.Post).WithMany(p => p.Images)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Image_Post");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__954EBDF31589AA8C");

            entity.Property(e => e.NewsCreateAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NewsShortDescription).HasMaxLength(300);
            entity.Property(e => e.NewsTitle).HasMaxLength(255);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.News)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_News_User");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA126018BA0F9AAE");

            entity.ToTable("Post");

            entity.Property(e => e.PostCreateAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PostTitle).HasMaxLength(255);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Post_User");
        });

        modelBuilder.Entity<Prevention>(entity =>
        {
            entity.HasKey(e => e.PreventionId).HasName("PK__Preventi__0DB6911F90BBCB14");

            entity.ToTable("Prevention");

            entity.HasOne(d => d.Disease).WithMany(p => p.Preventions)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("FK_Prevention_Disease");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A5906C50A");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B61607B515FFA").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C4842DDD2");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E423462825").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534B63E76F6").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.Username).HasMaxLength(30);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
