using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Thread_.NET.DAL.Entities;
using Thread_.NET.DAL.Entities.Abstract;

namespace Thread_.NET.DAL.Context
{
    public class ThreadContext : DbContext
    {
        public DbSet<Comment> Comments { get; private set; }
        public DbSet<CommentReaction> CommentReactions { get; private set; }
        public DbSet<Image> Images { get; private set; }
        public DbSet<Post> Posts { get; private set; }
        public DbSet<PostReaction> PostReactions { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }
        public DbSet<User> Users { get; private set; }

        public ThreadContext(DbContextOptions<ThreadContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting up entities using extension method
            modelBuilder.Configure();

            // Seeding data using extension method
            // NOTE: this method will be called every time after adding a new migration, cuz we use Bogus for seed data
            modelBuilder.Seed();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is ISoftDeletable entity)
                {
                    item.State = EntityState.Unchanged;
                    entity.IsDeleted = true;
                }
            }
        }
    }
}
