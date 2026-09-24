using ArenaHub.API.Constants;
using ArenaHub.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArenaHub.API.Data
{
    public class ArenaHubDbContext(DbContextOptions<ArenaHubDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentParticipant> TournamentParticipants { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TournamentParticipant>().HasIndex(t => new { t.TournamentId, t.UserId }).IsUnique();

            builder.Entity<TournamentParticipant>()
                .HasOne(tp => tp.User)
                .WithMany()
                .HasForeignKey(tp => tp.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
