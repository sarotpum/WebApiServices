using Microsoft.EntityFrameworkCore;
using SharedService.Models.Card;

namespace SharedService.DBContext
{
    public class DatasContext : DbContext
    {
        public DatasContext(DbContextOptions<DatasContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CardModel> Cards { get; set; }
    }
}
