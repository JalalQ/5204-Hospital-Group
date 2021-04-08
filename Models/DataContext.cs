using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace News_Events_Payments.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {


        }
        public DbSet<News_events> News_Events { get; set; }
        public DbSet<Payments> Payments { get; set; }

    }


}