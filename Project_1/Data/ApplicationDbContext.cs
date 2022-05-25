using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_1.Infra;

namespace Project_1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);
            initializeTables(b);
        }
        private static void initializeTables(ModelBuilder b)
        {
            UniversityDb.InitializeTables(b);
        }

    }
}