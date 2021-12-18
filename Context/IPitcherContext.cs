using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Models;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Context
{
    public interface IPitcherContext
    {
        public DbSet<UserModel> PitcherUsers { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<LikeModel> LikeModel { get; set; }
        public DbSet<StarModel> StarModel { get; set; }
        public int SaveChanges();

        //public object Entry();
    }
}
