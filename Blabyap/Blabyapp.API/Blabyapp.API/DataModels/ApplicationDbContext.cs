using System.Data.Entity;
using Blabyapp.API.DataModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blabyapp.API.DataModels
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
       
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<MeetNow> MeetNows { get; set; }
        public DbSet<MyCV> MyCVs { get; set; }
        public DbSet<Seniority> Seniorities { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<chat> Chat { get; set; }
        
    }
}