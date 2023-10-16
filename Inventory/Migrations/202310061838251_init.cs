namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 50),
                        Fax = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                        Website = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(nullable: false, maxLength: 50),
                        ProductName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductTransactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoreTransactionID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        unitOfMeasurementID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        InTransaction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.UnitOfMeasurements", t => t.unitOfMeasurementID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .ForeignKey("dbo.StoreTransactions", t => t.StoreTransactionID)
                .Index(t => t.StoreTransactionID)
                .Index(t => t.StoreID)
                .Index(t => t.unitOfMeasurementID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.UnitOfMeasurements",
                c => new
                    {
                        UnitOfMeasurementID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        UnitName = c.String(nullable: false, maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.UnitOfMeasurementID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductTransfers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoreTransferID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        unitOfMeasurementID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        InTransaction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.UnitOfMeasurements", t => t.unitOfMeasurementID)
                .ForeignKey("dbo.StoreTransfers", t => t.StoreTransferID)
                .Index(t => t.StoreTransferID)
                .Index(t => t.unitOfMeasurementID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreID = c.Int(nullable: false, identity: true),
                        StoreName = c.String(nullable: false, maxLength: 255),
                        StoreAddress = c.String(maxLength: 255),
                        ResponsiblePerson = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.StoreID);
            
            CreateTable(
                "dbo.StoreTransactions",
                c => new
                    {
                        StoreTransactionID = c.Int(nullable: false, identity: true),
                        StoreID = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        InTransaction = c.Boolean(nullable: false),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreTransactionID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.StoreTransfers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        StoreFromID = c.Int(nullable: false),
                        StoreToID = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        StoreFrom_StoreID = c.Int(),
                        StoreTo_StoreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stores", t => t.StoreFrom_StoreID)
                .ForeignKey("dbo.Stores", t => t.StoreTo_StoreID)
                .Index(t => t.StoreFrom_StoreID)
                .Index(t => t.StoreTo_StoreID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 50),
                        Fax = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                        Website = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreTransfers", "StoreTo_StoreID", "dbo.Stores");
            DropForeignKey("dbo.StoreTransfers", "StoreFrom_StoreID", "dbo.Stores");
            DropForeignKey("dbo.ProductTransfers", "StoreTransferID", "dbo.StoreTransfers");
            DropForeignKey("dbo.StoreTransactions", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.ProductTransactions", "StoreTransactionID", "dbo.StoreTransactions");
            DropForeignKey("dbo.ProductTransactions", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.ProductTransfers", "unitOfMeasurementID", "dbo.UnitOfMeasurements");
            DropForeignKey("dbo.ProductTransfers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.UnitOfMeasurements", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductTransactions", "unitOfMeasurementID", "dbo.UnitOfMeasurements");
            DropForeignKey("dbo.ProductTransactions", "ProductID", "dbo.Products");
            DropIndex("dbo.StoreTransfers", new[] { "StoreTo_StoreID" });
            DropIndex("dbo.StoreTransfers", new[] { "StoreFrom_StoreID" });
            DropIndex("dbo.StoreTransactions", new[] { "StoreID" });
            DropIndex("dbo.ProductTransfers", new[] { "ProductID" });
            DropIndex("dbo.ProductTransfers", new[] { "unitOfMeasurementID" });
            DropIndex("dbo.ProductTransfers", new[] { "StoreTransferID" });
            DropIndex("dbo.UnitOfMeasurements", new[] { "ProductID" });
            DropIndex("dbo.ProductTransactions", new[] { "ProductID" });
            DropIndex("dbo.ProductTransactions", new[] { "unitOfMeasurementID" });
            DropIndex("dbo.ProductTransactions", new[] { "StoreID" });
            DropIndex("dbo.ProductTransactions", new[] { "StoreTransactionID" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.StoreTransfers");
            DropTable("dbo.StoreTransactions");
            DropTable("dbo.Stores");
            DropTable("dbo.ProductTransfers");
            DropTable("dbo.UnitOfMeasurements");
            DropTable("dbo.ProductTransactions");
            DropTable("dbo.Products");
            DropTable("dbo.Customers");
        }
    }
}
