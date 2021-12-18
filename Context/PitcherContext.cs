using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API_mk1.Context.PitcherContext
{
    public class PitcherContext: IdentityDbContext, IPitcherContext
    {
        public PitcherContext(DbContextOptions<PitcherContext> opt) : base(opt)
        {

        }
        public DbSet<UserModel> PitcherUsers { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<LikeModel> LikeModel { get; set; }
        public DbSet<StarModel> StarModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectModel>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasPrincipalKey(u => u.UserId)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectModel>()
                .Property(p => p.likeNumber)
                .HasDefaultValue(0);

            base.OnModelCreating(modelBuilder);
        }
    }
}
