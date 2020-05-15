using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace feedback.Models
{
    public partial class FeedBackContext : DbContext
    {
        public FeedBackContext()
        {
        }

        public FeedBackContext(DbContextOptions<FeedBackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<OpinionLog> OpinionLog { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=TUTULPC\\TUTUL;Database=FeedBack;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CDislike).HasColumnName("cDislike");

                entity.Property(e => e.CLike).HasColumnName("cLike");

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comment_Post");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(300);

                entity.Property(e => e.UserType).HasMaxLength(300);
            });
        }
    }
}
