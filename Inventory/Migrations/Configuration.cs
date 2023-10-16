namespace Inventory.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Inventory.ProjectFullStack>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Inventory.ProjectFullStack context)
        {
            //  This method will be called after migrating to the latest version.
            if (!context.Stores.Any()) context.Stores.Add(new Store { StoreName = "مخزن رئسي", StoreAddress = "البرج", ResponsiblePerson = "محمود" });
            if (!context.Suppliers.Any()) context.Suppliers.Add(new Supplier { SupplierName = "مورد عام" });
            if (!context.Customers.Any()) context.Customers.Add(new Customer { CustomerName = "عميل عام"});
            if (!context.Stores.Any()) context.Stores.Add(new Store { StoreName = "مخزن رئسي", StoreAddress = "البرج", ResponsiblePerson = "محمود" });

            if (!context.Products.Any()) context.Products.AddRange(new Product[]
            {
                new Product{ProductCode="001",ProductName="جبن",UnitOfMeasurements= new System.ComponentModel.BindingList<UnitOfMeasurement>(new List<UnitOfMeasurement>
                {
                    new UnitOfMeasurement{UnitName="عبلة", Price=10},
                    new UnitOfMeasurement{UnitName="كرتونه ص", Price=60},
                    new UnitOfMeasurement{UnitName="كرتونه ك", Price=120},
                }) },

                new Product{ProductCode="002",ProductName="حلاوة",UnitOfMeasurements= new System.ComponentModel.BindingList<UnitOfMeasurement>(new List<UnitOfMeasurement>
                {
                    new UnitOfMeasurement{UnitName="عبلة", Price=5},
                    new UnitOfMeasurement{UnitName="كرتونه ص", Price=30},
                    new UnitOfMeasurement{UnitName="كرتونه ك", Price=60},
                }) },

                new Product{ProductCode="003",ProductName="دقيق",UnitOfMeasurements= new System.ComponentModel.BindingList<UnitOfMeasurement>(new List<UnitOfMeasurement>
                {
                    new UnitOfMeasurement{UnitName="كيلو", Price=20},
                    new UnitOfMeasurement{UnitName="كرتونه ص", Price=120},
                    new UnitOfMeasurement{UnitName="كرتونه ك", Price=240},
                }) },

                new Product{ProductCode="004",ProductName="قهوة",UnitOfMeasurements= new System.ComponentModel.BindingList<UnitOfMeasurement>(new List<UnitOfMeasurement>
                {
                    new UnitOfMeasurement{UnitName="عبلة", Price=30},
                    new UnitOfMeasurement{UnitName="كرتونه ص", Price=360}
                }) },

                new Product{ProductCode="005",ProductName="نسكافية",UnitOfMeasurements= new System.ComponentModel.BindingList<UnitOfMeasurement>(new List<UnitOfMeasurement>
                {
                    new UnitOfMeasurement{UnitName="عبلة", Price=50},
                    new UnitOfMeasurement{UnitName="كرتونه ص", Price=600}
                }) } 
            });

            context.SaveChanges();

        }
    }
}
