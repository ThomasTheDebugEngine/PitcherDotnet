using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Models.Project;
using API_mk1.Models.User;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Context
{
    public interface IPitcherContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public int SaveChanges();

        //public object Entry();
    }
}
