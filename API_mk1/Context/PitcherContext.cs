using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models.User;
using API_mk1.Models.Project;

namespace API_mk1.Context.PitcherContext
{
    public class PitcherContext: DbContext
    {
        public PitcherContext(DbContextOptions<PitcherContext> opt) : base(opt)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
    }

    public interface IPitcherContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public int SaveChanges();

        public object Entry();
    }
}
