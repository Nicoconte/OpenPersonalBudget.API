using Microsoft.EntityFrameworkCore;
using PersonalBudget.API.Models;

namespace PersonalBudget.API.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }


        public DbSet<UserModel> Users { get; set; }

    }
}
