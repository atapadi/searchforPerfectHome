namespace SearchPerfectHome.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SearchHomeEntities : DbContext
    {
        public SearchHomeEntities()
            : base("name=SearchHomeEntities")
        {
            //***
            //*** The following line will make sure that the Entity Framework will not push Model Database changes
            //*** to the database
            //***
            Database.SetInitializer<SearchHomeEntities>(null);
        }

        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
