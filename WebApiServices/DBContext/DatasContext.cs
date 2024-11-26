using Microsoft.EntityFrameworkCore;
using SharedService.Models.Books;
using SharedService.Models.Card;
using SharedService.Models.Contact;
using SharedService.Models.PaymentDetail;
using SharedService.Models.RestSample;
using SharedService.Models.Student;
using SharedService.Models.Tlous;

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
        public DbSet<PaymentDetailModel> PaymentDetails { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<RestSampleModel> RestSamples { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<CharacterModel> Characters { get; set; }
        public DbSet<BackpackModel> Backpacks { get; set; }
        public DbSet<WeaponModel> Weapons { get; set; }
        public DbSet<FactionModel> Factions { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SharedService.Models.Characters.CharacterModel> Characters2 => Set<SharedService.Models.Characters.CharacterModel>();

        //===========================================================================================

        //public DbSet<PaymentDetail> PaymentDetail { get; set; }

        //public class AppDBContext : IdentityDbContext<AppUser, IdentityRole, string>
        //{
        //    public AppDBContext(DbContextOptions options) : base(options)
        //    {
        //    }
        //}

        //===========================================================================================

        //public BookContext(DbContextOptions<BookContext> options)
        //   : base(options)
        //{
        //    Database.EnsureCreated();
        //}
        //public DbSet<Book> Books { get; set; }
    }
}
