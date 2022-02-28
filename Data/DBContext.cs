using Microsoft.EntityFrameworkCore;
using OpenPersonalBudget.API.Models;

namespace OpenPersonalBudget.API.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }


        public DbSet<UserModel> Users { get; set; }

    }
}
