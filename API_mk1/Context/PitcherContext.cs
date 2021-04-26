using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models.User;

namespace API_mk1.Context.PitcherContext
{
    public class PitcherContext: DbContext, IPitcherContext
    {
        public PitcherContext(DbContextOptions<PitcherContext> opt) : base(opt)
        {

        }
        public DbSet<UserModel> Users { get; set; }
    }

    public interface IPitcherContext
    {
        public DbSet<UserModel> Users { get; set; }
        public int SaveChanges();
    }
}
