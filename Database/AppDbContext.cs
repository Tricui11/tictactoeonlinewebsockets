using Task5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task5.Database
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatUser>()
                .HasKey(x => new { x.ChatId, x.UserId });
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionstring = "Server=(localdb)\\mssqllocaldb;Database=task5Db;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(config =>
                    config.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))
                .UseSqlServer(connectionstring);

        }
        */
    }
}