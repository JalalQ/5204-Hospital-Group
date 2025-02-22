﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospitalPrj.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       
            public string Name { get; set; }
            public string lName { get; set; }
            public string healthNumber { get; set; }
            
            
     


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class hospitalDbContext : IdentityDbContext<ApplicationUser>
    {
        public hospitalDbContext()
            : base("Hospital", throwIfV1Schema: false)
        {
        }

        public static hospitalDbContext Create()
        {
            return new hospitalDbContext();
        }
        public DbSet<appointment> Appointments { get; set; }
    }
}