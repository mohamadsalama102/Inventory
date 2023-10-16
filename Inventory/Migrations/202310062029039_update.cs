namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StoreTransfers", new[] { "StoreFrom_StoreID" });
            DropIndex("dbo.StoreTransfers", new[] { "StoreTo_StoreID" });
            DropColumn("dbo.StoreTransfers", "StoreFromID");
            DropColumn("dbo.StoreTransfers", "StoreToID");
            RenameColumn(table: "dbo.StoreTransfers", name: "StoreFrom_StoreID", newName: "StoreFromID");
            RenameColumn(table: "dbo.StoreTransfers", name: "StoreTo_StoreID", newName: "StoreToID");
            AlterColumn("dbo.StoreTransfers", "StoreFromID", c => c.Int(nullable: false));
            AlterColumn("dbo.StoreTransfers", "StoreToID", c => c.Int(nullable: false));
            CreateIndex("dbo.StoreTransfers", "StoreFromID");
            CreateIndex("dbo.StoreTransfers", "StoreToID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StoreTransfers", new[] { "StoreToID" });
            DropIndex("dbo.StoreTransfers", new[] { "StoreFromID" });
            AlterColumn("dbo.StoreTransfers", "StoreToID", c => c.Int());
            AlterColumn("dbo.StoreTransfers", "StoreFromID", c => c.Int());
            RenameColumn(table: "dbo.StoreTransfers", name: "StoreToID", newName: "StoreTo_StoreID");
            RenameColumn(table: "dbo.StoreTransfers", name: "StoreFromID", newName: "StoreFrom_StoreID");
            AddColumn("dbo.StoreTransfers", "StoreToID", c => c.Int(nullable: false));
            AddColumn("dbo.StoreTransfers", "StoreFromID", c => c.Int(nullable: false));
            CreateIndex("dbo.StoreTransfers", "StoreTo_StoreID");
            CreateIndex("dbo.StoreTransfers", "StoreFrom_StoreID");
        }
    }
}
