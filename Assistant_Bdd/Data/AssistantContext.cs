using Assistant_Bdd.Models;
using Microsoft.EntityFrameworkCore;

namespace Assistant_Bdd.Data
{
    public class AssistantContext : DbContext
    {
        public AssistantContext(DbContextOptions<AssistantContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(120);
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Assistant> Assistant { get; set; }
        public DbSet<Discussion> Discussion { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagAssistant> TagAssistant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagAssistant>().HasKey(c => new
            {
                c.IdTag,
                c.IdAssistant
            });
        }
    }
}
