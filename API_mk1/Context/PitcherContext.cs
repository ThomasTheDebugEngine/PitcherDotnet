using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models.User;
using API_mk1.Models.Project;

namespace API_mk1.Context.PitcherContext
{
    public class PitcherContext: DbContext, IPitcherContext
    {
        public PitcherContext(DbContextOptions<PitcherContext> opt) : base(opt)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }

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
        }
    }
}
