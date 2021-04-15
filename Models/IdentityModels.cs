using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace team2Geraldton.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class team2GeraldtonDbContext : IdentityDbContext<ApplicationUser>
    {
        public team2GeraldtonDbContext()

            : base("team2GeraldtonDbContextwAuth", throwIfV1Schema: false)
        {
        }

        public static team2GeraldtonDbContext Create()
        {
            return new team2GeraldtonDbContext();
        }
        //Instruction to set the models as tables in our database.
        public DbSet<Post> Posts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<doctor> doctors { get; set; }

    }
}