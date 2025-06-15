using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Activity = Domain.Entities.Activity;
using Group = Domain.Entities.Group;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityMember> ActivityMembers { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizItem> QuizItems { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Уникальный индекс для Group.Code
            modelBuilder.Entity<Group>()
                .HasIndex(g => g.Code)
                .IsUnique();

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithOne(m => m.Group)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            // Уникальный индекс для User.Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Явная настройка связи Wishlist -> WishlistItems без каскадного удаления
            modelBuilder.Entity<Wishlist>()
                .HasMany(w => w.Items)
                .WithOne(wi => wi.Wishlist)
                .HasForeignKey(wi => wi.WishlistId)
                .OnDelete(DeleteBehavior.NoAction);

            // Настройка связи WishlistItem -> GroupMember
            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.GroupMember)
                .WithMany(gm => gm.CreatedWishlistItems)
                .HasForeignKey(wi => wi.GroupMemberId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
