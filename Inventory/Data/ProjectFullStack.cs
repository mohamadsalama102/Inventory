using Inventory.Entitys;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Inventory
{
    public partial class ProjectFullStack : DbContext
    {
        public ProjectFullStack() : base("data source=.;initial catalog=ProjectFullStack;integrated security=True;")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectFullStack, Migrations.Configuration>());
        }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public virtual DbSet<StoreTransaction> StoreTransactions { get; set; }
        public virtual DbSet<ProductTransaction> StoreProducts { get; set; }
        public virtual DbSet<StoreTransfer> StoreTransfers { get; set; }
        public virtual DbSet<ProductTransfer> ProductTransfers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
