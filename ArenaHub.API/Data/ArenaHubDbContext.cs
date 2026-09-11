using ArenaHub.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArenaHub.API.Data
{
    public class ArenaHubDbContext(DbContextOptions<ArenaHubDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Game> Games { get; set; }
    }
}
